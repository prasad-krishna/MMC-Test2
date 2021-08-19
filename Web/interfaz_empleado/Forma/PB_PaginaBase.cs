using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Mercer.Medicines.Logic;
using System.Web.Security;
using System.Linq;
using System.IO;
using MMC.Seguridad.Utilerias;

namespace TPA
{
    /// <summary>
    /// Página de ingreso al sistema
    /// </summary>
    public partial class PB_PaginaBase : System.Web.UI.Page
    {
        #region Atributos


        #endregion

        #region Inicialización

        protected virtual void Page_Load(object sender, System.EventArgs e)
        {
            try
            {             
                    //Inicio 22/01/10 MAHG Marco A. Herrera G.
                    /* Se verifica que el ticket (Cookie) y la sesión no hayan expirado.*/

                    if (Session["IdUser"] == null || FormsAuthentication.FormsCookieName == "")
                    {
                    ////GAMM Actulizamos sesión Activa
                    //DataTable dt;
                    //Permissions objExpire = new Permissions();

                    //dt = objExpire.ExpireSesionUser((int)Session["IdUser"], Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value).Tables[0];
                    ////

                    //this.Session.Clear();
                    //    this.Session.Abandon();
                    //    FormsAuthentication.SignOut();//Elimina el vale de autenticación                        

                    //    //GAMM.
                    //    // clear authentication cookie
                    //    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                    //    cookie1.Expires = DateTime.Now.AddYears(-1);
                    //    Response.Cookies.Add(cookie1);
                    //    FormsAuthentication.RedirectToLoginPage();

                    //    // Clear session cookie 
                    //    HttpCookie rSessionCookie = new HttpCookie("{24618D5F-65A9-43cf-A40B-CB15DC3328DA}", "" );
                    //    rSessionCookie.Expires = DateTime.Now.AddYears( -1 );
                    //    Response.Cookies.Add( rSessionCookie );

                    AntiHack.LimpiaSession();
                    AntiHack.RegenerarSessionId();

                    //Fin 22/01/10 MAHG
                    string message = "";
                        message = "<script language='javascript'>alert('Su sesión a caducado, ingrese nuevamente');window.parent.location ='../../AE_login_admin.aspx'</script>";

                        //Inicio MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, false);
                        }
                        else
                        {
                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", message);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", message, true);
                    }

                        Response.End();
                    }
                    else
                    {
                        if (!this.Page.IsPostBack)
                        {
                        #region ValidaSession
                        
                        bool existe = ConsultSession(Convert.ToInt32(Session["IdUser"].ToString()), Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value);

                            if (existe)
                            {
                                //Fin 22/01/10 MAHG
                                string message = "";
                                message = "<script language='javascript'>alert('Su sesión a caducado, ingrese nuevamente');window.parent.location ='../../AE_login_admin.aspx'</script>";

                                //Inicio MAHG Se verifica si la solicitud es Asincrona
                                if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                                {
                                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, false);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", message);
                                }

                                return;
                            }
                        #endregion

                        //GAMM. I. 20190422 Valida menus
                        #region ValidaMenu

                        if (Session["dtPadres"] != null)
                        {

                            string paginaActual = Page.Request.FilePath.ToString();
                            string valida = "";

                            valida = ValidaPantalla(paginaActual);

                            if (valida != "")
                            {

                                AntiHack.LimpiaSession();
                                AntiHack.RegenerarSessionId();

                                //Fin 22/01/10 MAHG
                                string message = "";
                                //message = "<script>alert('Su sesión a caducado, ingrese nuevamente');window.top.close();</script>";
                                message = "<script>" + valida + "</script>";

                                ////Inicio MAHG Se verifica si la solicitud es Asincrona
                                if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                                {
                                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, false);
                                }
                                else
                                {
                                    Response.Write(message);
                                }
                            }
                        }
                        #endregion
                        //GAMM. F. 20190422 Valida menus

                        System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
                            string FormatoFecha = configurationAppSettings.GetValue("FormatoFecha", typeof(string)).ToString();
                            string SeparadorDecimales = configurationAppSettings.GetValue("SeparadorDecimales", typeof(string)).ToString();
                            string SeparadorMiles = configurationAppSettings.GetValue("SeparadorMiles", typeof(string)).ToString();
                            string NumeroDecimales = configurationAppSettings.GetValue("NumeroDecimales", typeof(string)).ToString();
                            string FormatoHora = configurationAppSettings.GetValue("FormatoHora", typeof(string)).ToString();

                            // Establecer propiedades del CultureInfo de Idioma y Separador Decimal y de Miles
                            System.Globalization.CultureInfo objCultureInfo = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                            objCultureInfo.DateTimeFormat.ShortDatePattern = FormatoFecha;
                            objCultureInfo.DateTimeFormat.ShortTimePattern = FormatoHora;
                            objCultureInfo.NumberFormat.NumberDecimalSeparator = SeparadorDecimales;
                            objCultureInfo.NumberFormat.NumberGroupSeparator = SeparadorMiles;
                            objCultureInfo.NumberFormat.NumberDecimalDigits = Convert.ToInt32(NumeroDecimales);
                            System.Threading.Thread.CurrentThread.CurrentCulture = objCultureInfo;
                            System.Threading.Thread.CurrentThread.CurrentUICulture = objCultureInfo;
                        }                   
                }
            }
            catch (Exception ex)
            {
                string message = "";
                message = "<script language='javascript'>alert('Exception :" + ex.Message + "')</script>";
                //Inicio MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(),"error", message);
                }
			}

            //

        }

        #endregion

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            //InitializeComponent();
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region Methods

        /// <summary>
        /// Método, valida opciones del menu.
        /// </summary>
        /// <param name="paginaActual">pagina actual</param>
        public string ValidaPantalla(string paginaActual)
        {

            //DataTable dtPadres;
            Permissions objMenu = new Permissions();

            objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
            objMenu.Parent = true;

            //dtPadres = objMenu.ConsultPermissionsUserGeneral((int)Session["IdUser"]).Tables[0];

            //Cargamos permisos en base a la variabl de session.
            DataTable dtPadres = (DataTable)Session["dtPadres"];

            string mensaje = "";
            bool validaMenu;

            //Padres
            validaMenu = (dtPadres.AsEnumerable().Where(row => row["IdPermissionType"].ToString() != "3").AsEnumerable().Any(row => Path.GetFileName(row.Field<String>("URL")).ToLower() == Path.GetFileName(paginaActual).ToLower()));
            if (validaMenu == true)
            {
                mensaje = "";
                return mensaje;
            }
            else if (validaMenu == false)
            {
                mensaje = "alert('No tiene privilegios para ingresar a esta pantalla.');window.parent.location ='../../AE_login_admin.aspx';";

                //Emergentes
                validaMenu = (dtPadres.AsEnumerable().Where(row => row["IdPermissionType"].ToString() == "3").AsEnumerable().Any(row => Path.GetFileName(row.Field<String>("URL")).ToLower() == Path.GetFileName(paginaActual).ToLower()));
                if (validaMenu == true)
                {
                    mensaje = "";
                }
                else if (validaMenu == false)
                {
                    mensaje = "alert('No tiene privilegios para ingresar a esta pantalla');window.top.close();";
                }

                return mensaje;
            }

            return "";
        }

        /// <summary>
        /// Método, despliega un mensaje
        /// </summary>
        /// <param name="message">Mensaje a desplegar</param>
        public void DisplayMessage(string p_message)
        {
            //Registrar error en log de errores
            LogErrors objError = new LogErrors();
            objError.MessageError = p_message;           

            //Desplegar error 
            string script;
            string message = "";
            message = p_message.Replace("'", "");
            message = message.Replace("\r", "");
            message = message.Replace("\n", "");
            script = "<script language='javascript'>alert('" + message + "')</script>";
            //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Mensaje", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"Mensaje", script);
            }
            //Fin 12/01/10 MAHG 

		}


        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 21/01/10
        /// Funcionalidad: Muestra un mensaje en pantalla y redirecciona a la paginada pasada por parámetro
        /// </summary>
        /// <param name="p_message">Mensaje a mostrar</param>
        /// <param name="strPage">Pagina a la que se redireccionara</param>
        public void DisplayMessage(string p_message, string strPage)
        {
            //Registrar error en log de errores
            LogErrors objError = new LogErrors();
            objError.MessageError = p_message;
            if (Session["SICAU"] == null)
                objError.IdUser = (int)Session["IdUser"];
            else
                objError.Usuario_id = (int)Session["IdUser"];
            objError.IP = (string)Session["IPUser"];
            objError.PageError = Request.RawUrl;
            objError.InsertLogErrors();

            //Desplegar error 
            string script;
            string message = "";
            message = p_message.Replace("'", "");
            message = message.Replace("\r", "");
            message = message.Replace("\n", "");
            script = "<script language='javascript'>alert('" + message + "');";
            script += "window.location = '"+strPage+"';</script>";
            //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Mensaje", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Mensaje", script);
            }
            //Fin 12/01/10 MAHG 

        }

        /// <summary>
        /// Método, despliega un mensaje
        /// </summary>
        /// <param name="message">Mensaje a desplegar</param>
        public void DisplayMessageExito(string p_message)
        {
            //Desplegar mensaje 
            string script;
            string message = "";
            message = p_message.Replace("'", "");
            message = message.Replace("\r", "");
            message = message.Replace("\n", "");
            script = "<script language='javascript'>alert('" + message + "')</script>";
            //Inicio MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Mensaje", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"Mensaje", script);
            }
		}

        /// <summary>
        /// Método, registra en el log de usuario la acción y su detalle
        /// </summary>
        /// <param name="p_idAction">Id de la acción</param>
        /// <param name="p_mainId">Id principal sobre el que se realiza la acción</param>
        /// <param name="p_detail">Detalle de la acción</param>		
        public void RegisterLog(Log.EnumActionsLog p_idAction, long p_mainId, string p_detail)
        {
            Log objLog = new Log();

            if (Session["SICAU"] == null)
                objLog.IdUser = (int)Session["IdUser"];
            else
                objLog.usuario_id = (int)Session["IdUser"];
            objLog.IP = (string)Session["IPUser"];
            objLog.IdActionLog = p_idAction;
            objLog.MainId = p_mainId;
            objLog.Detail = p_detail;
            objLog.InsertLog();
        }


        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillList(string p_tableName, string p_columnName, System.Web.UI.WebControls.ListControl p_objListControl, string p_messageItem)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTable();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillList(string p_tableName, string p_columnName, int p_empresa_id, System.Web.UI.WebControls.ListControl p_objListControl, string p_messageItem)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.empresa_id = p_empresa_id;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableEmpresa();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            else
                p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillListActivos(string p_tableName, string p_columnName, int p_empresa_id, System.Web.UI.WebControls.ListControl p_objListControl, string p_messageItem)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.empresa_id = p_empresa_id;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableEmpresaActivos();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            else
                p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }


        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillListUser(string p_tableName, string p_columnName, int p_idUsuario, object p_sicau, int p_empresa_id, System.Web.UI.WebControls.ListControl p_objListControl, string p_messageItem)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.empresa_id = p_empresa_id;
            if (p_sicau != null && Convert.ToBoolean(p_sicau))
                objGeneralTable.Usuario_id = p_idUsuario;
            else
                objGeneralTable.IdUser = p_idUsuario;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableUsuario();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            else
                p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>	
        public void FillList(string p_tableName, string p_columnName, System.Web.UI.WebControls.ListControl p_objListControl)
        {
            GeneralTable objGeneralTable = new GeneralTable();

            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTable();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema, permitiendo ordenar y condicionar la consulta
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// HC-New functionalities-003, PORTOMX, RAM, 15/09/2015
        public void FillList(string p_tableName, string p_columnName, string p_sort, string p_where, System.Web.UI.WebControls.ListControl p_objListControl)
        {
            GeneralTable objGeneralTable = new GeneralTable();

            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.Sort = p_sort;
            objGeneralTable.Where = p_where;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTable();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>	
        public void FillList(string p_tableName, string p_columnName, int p_empresa_id, System.Web.UI.WebControls.ListControl p_objListControl)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.empresa_id = p_empresa_id;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableEmpresa();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>	
        public void FillListActivos(string p_tableName, string p_columnName, int p_empresa_id, System.Web.UI.WebControls.ListControl p_objListControl)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;
            objGeneralTable.empresa_id = p_empresa_id;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableEmpresaActivos();
            p_objListControl.DataBind();
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            objGeneralTable = null;
        }


        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillList(System.Web.UI.WebControls.ListControl p_objListControl, string p_nameStoreProcedure, string p_messageItem, params object[] p_arParams)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableSP(p_nameStoreProcedure, p_arParams);
            p_objListControl.DataBind();
            p_objListControl.DataSource = null;
            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;
            else
                p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void FillList(System.Web.UI.WebControls.ListControl p_objListControl, string p_nameStoreProcedure, params object[] p_arParams)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableSP(p_nameStoreProcedure, p_arParams);
            p_objListControl.DataBind();

            if (p_objListControl.Items.Count == 1)
                p_objListControl.SelectedIndex = 0;

            objGeneralTable = null;
        }


        /// <summary>
        /// Método, llena un control de tipo listado con todos los registros de una determinada
        /// tabla en el sistema
        /// </summary>
        /// <param name="p_tableName"></param>
        /// <param name="p_columnName"></param>
        /// <param name="p_columnId"></param>
        /// <param name="p_objListControl"></param>
        /// <param name="p_messageItem"></param>
        public void FillListTableIdText(string p_tableName, string p_columnName, string p_columnId, System.Web.UI.WebControls.ListControl p_objListControl, string p_messageItem)
        {
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = p_tableName;
            objGeneralTable.ColumnName = p_columnName;

            objGeneralTable.ColumnId = p_columnId;
            p_objListControl.DataTextField = "Nombre";
            p_objListControl.DataValueField = "Id";
            p_objListControl.DataSource = objGeneralTable.ConsultGeneralTableIdTexto();
            p_objListControl.DataBind();

            p_objListControl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(p_messageItem, "0"));
            objGeneralTable = null;
        }

        /// <summary>
        /// Método, carga principal para determinar el inicio de un reporte
        /// </summary>
        public void LoadReport()
        {
            if (Request.QueryString["exportar"] != null && Request.QueryString["exportar"] == "S")
            {
                Response.ClearContent();
                Response.ContentEncoding = System.Text.UTF7Encoding.Default;
                Response.ContentType = "application/vnd.ms-excel";
            }
            else
            {
                string script = "";
                script = "<script language='javascript'>window.print();</script>";
                //Inicio MAHG Se verifica si la solicitud es Asíncrona
                if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "print", script, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"print", script);
                }
			
			}
		}


        /// <summary>
        /// Método, abre una ventana emergente con la url especificada
        /// </summary>
        public void OpenWindow(string p_url, int width, int height)
        {
            string script = "";
            script = "<script language='javascript'>openPopUp('" + p_url + "'," + width + "," + height + ");</script>";
            //Inicio MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "open", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"open", script);
            }

		}
        /// <summary>
        /// Método, abre una ventana emergente con la url especificada
        /// </summary>
        public void OpenWindow(string p_url, int width, int height, int consecutivo)
        {
            string script = "";
            script = "<script language='javascript'>openPopUp('" + p_url + "'," + width + "," + height + ");</script>";
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "open" + consecutivo, script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "open" + consecutivo, script, false);
            }  		
		}


        /// <summary>
        /// Método, vuelve a cambiar tamaño de iframe
        /// </summary>
        public void ResizePage(string nameControl)
        {
            string script = "";
            script = "<script language='javascript'>CargarConfiguracion();</script>";
            //Inicio MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Resize" + nameControl, script, false);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Resize" + nameControl, script);
            }
		}


        /// <summary>
        /// Método, vuelve a cambiar tamaño de iframe
        /// </summary>
        public void SetFocus(string nameControl)
        {
            string script = "";
            script = "<script language='javascript'>document.getElementById('" + nameControl + "').focus();</script>";
            //Inicio MAHG Se verifica si la solicitud es Asíncrona
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Focus" + nameControl, script, false);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Focus" + nameControl, script);
            }
		}

        /// <summary>
        /// Método, marca un combo con el valor seleccionado a partir del request correspondiente
        /// </summary>
        /// <param name="NombreTabla">Nombre de la tabla que se va a traer la lista</param>
        /// <param name="objListControl">Control para llenar</param>
        /// <param name="MensajeItem">Mensaje que se muestra en el item cero</param>
        public void SelectList(System.Web.UI.WebControls.ListControl p_objListControl, string p_requestName)
        {
            int index = 0;
            bool checked_item = false;
            string value = Request[p_requestName].ToString().Trim();
            while (index < p_objListControl.Items.Count && !checked_item)
            {
                if (p_objListControl.Items[index].Value.ToString().Trim() == value)
                {
                    p_objListControl.SelectedIndex = index;
                }
                index = index + 1;
            }
        }




        #endregion

        #region ValidaSession
        public void RegisterSession(int IdUser, string strSession, string strBrowser)
        {
            Users user = new Users();
            user.UsersSession(IdUser, strSession, strBrowser);
        }

        public bool ConsultSession(int IdUser, string strSession)
        {
            DataSet ds = new DataSet();
            Users user = new Users();

            ds = user.ConsultSession(IdUser, strSession);

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Clave"].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateSession(int IdUser, string strSession, int origen)
        {
            Users user = new Users();
            user.UpdateUsersSession(IdUser, strSession, origen);
        }
        #endregion


    }
}