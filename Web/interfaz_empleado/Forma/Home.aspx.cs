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
using System.Web.Security;
using Mercer.Medicines.Logic;
//GAMM
using MMC.Seguridad.Utilerias; 

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Página principal
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 6 de Octubre de 2008</remarks>
    public partial class Home : PB_PaginaBase
    {

        #region Attributes
       
        #endregion

        #region Initializing

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                //base.Page_Load(sender, e);

                //Fin MAHG 22/01/10
                //GAMM
                //Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = ConfirmClose();");


                if (!this.Page.IsPostBack)
                {
                    this.lblUsuario.Text = Session["NameUser"].ToString();
                    this.lblFecha.Text = DateTime.Now.ToLongDateString();

                    //Cargar las características de formatos para el manejo de javascripts
                    System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
                    this.hdnSeparadorDecimales.Value = configurationAppSettings.GetValue("SeparadorDecimales", typeof(string)).ToString();
                    this.hdnSeparadorMiles.Value = configurationAppSettings.GetValue("SeparadorMiles", typeof(string)).ToString();
                    this.hdnNumeroDecimales.Value = configurationAppSettings.GetValue("NumeroDecimales", typeof(string)).ToString();

                    /*
                     I. GAMM. 20200206 Cargamos variable de session con los permisos asignados al usuario.
                     */
                    if (Session["dtPadres"] == null)
                    {
                        DataTable dtPadres;
                        Permissions objMenu = new Permissions();

                        objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
                        objMenu.Parent = true;

                        dtPadres = objMenu.ConsultPermissionsUserGeneral((int)Session["IdUser"]).Tables[0];

                        Session.Add("dtPadres", dtPadres);
                    }
                    /*
                     F. GAMM. 20200206 Cargamos variable de session con los permisos asignados al usuario.
                     */

                    if (Session["URLRedirect"] != null)
                    {
                        ifrPageContent.Attributes.Add("Src", Session["URLRedirect"].ToString());
                        Session["URLRedirect"] = null;
                    }

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Events

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lnkInicio.Click += new System.EventHandler(this.lnkInicio_Click);
            this.lnkSalir.Click += new System.EventHandler(this.lnkSalir_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, redirecciona a la página principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkInicio_Click(object sender, System.EventArgs e)
        {
            try
            {
                Response.Redirect("~/interfaz_empleado/forma/Home.aspx");
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }


        }

        /// <summary>
        /// Evento, elimina la sesión y envía a página inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSalir_Click(object sender, System.EventArgs e)
        {
            try
            {
                //GAMM Actulizamos sesión Activa
                UpdateSession(Convert.ToInt32(Session["IdUser"].ToString()), Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value, 0);

                //GAMM Regeneramos session
                //AntiHack.RegenerarSessionId();

                //this.Session.Clear();
                //this.Session.Abandon();
                //FormsAuthentication.SignOut();//Elimina el vale de autenticación
                
                Response.Redirect("../../AE_login_admin.aspx");

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }
        /// <summary>
        /// Evento, despliega la página que contien los términos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTerminos_Click(object sender, System.EventArgs e)
        {
            string script;
            script = "<script language='javascript'>window.open('" + "https://co.mercer.com/termsofuse.htm?siteLanguage=102" + "')</script>";

            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Ventaja", script);
            }
            //Fin 


        }

        /// <summary>
        /// Evento, despliega la página que contien las políticas de privacidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkPrivacidad_Click(object sender, System.EventArgs e)
        {
            string script;
            script = "<script language='javascript'>window.open('" + "https://co.mercer.com/privacy.htm?siteLanguage=102" + "')</script>";


            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Ventaja", script);
            }
            //Fin 


        }

        #endregion


    }
}
