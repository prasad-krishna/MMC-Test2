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
using Microsoft.Security.Application;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza el ingreso o actualización de usuarios
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 16 de Octubre de 2008</remarks>
    public partial class AE_user : PB_PaginaBase
    {

        #region Atributtes


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
                txtPrestador.Attributes.Add("readonly", "true");
                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10
                if (!this.Page.IsPostBack)
                {
                    /*Proyecto: AMEX
                    Requerimiento: Permisos de usuarios por empresa
                    Funcionalidad: Oculta el campo empresa para México
                    Autor: Marco A. Herrera Gabriel
                    Fecha: 09/07/2010                     
                    */
                    //Inicio MAHG 09/07/2010

                    int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

                    if (intPais != 1)
                    {
                        SIC_EMPRESA objEmpresa = new SIC_EMPRESA();

                    }

                    //Fin MAHG 09/07/2010

                    if (Session["Administrador"].ToString() == string.Empty)
                    {
                        this.ddlCompania.SelectedValue = Session["Company"].ToString();
                        this.ddlCompania.Enabled = false;
                    }


                    this.FillList("Ciudades", "Ciudad", this.ddlCiudad, "--Ciudad--");

                    if (Request.QueryString["IdUser"] != null && Request.QueryString["IdUser"] != string.Empty)
                    {
                        this.loadFormUser(Convert.ToInt32(Request.QueryString["IdUser"]));
                        //this.rfvContrasena.Enabled = false;
                        //this.rfvConfContrasena.Enabled = false;
                        //this.cmvConfirmacion.Enabled = false;
                        this.loadPermissions(Convert.ToInt32(Request.QueryString["IdUser"]));
                        this.loadReportes(Convert.ToInt32(Request.QueryString["IdUser"]));
                        this.loadTipoServicios(Convert.ToInt32(Request.QueryString["IdUser"]));
                        if (txtIdPrestador.Text != "" || txtIdPrestador.Text != "0")
                        {
                            this.loadFormPrestador(Convert.ToInt32(txtIdPrestador.Text));
                        }

                    }
                    else
                    {
                        loadPermissions(0);
                        loadReportes(0);
                        loadTipoServicios(0);
                    }

                    /*Proyecto: AMEX
                     Requerimiento: Permisos de usuarios por empresa
                     Funcionalidad: Oculta el campo empresa para México
                     Autor: Marco A. Herrera Gabriel
                     Fecha: 09/07/2010                     
                   */
                    //Inicio MAHG 09/07/2010

                    intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

                    if (intPais == 1)
                    {
                        pnlEmpresa.Visible = false;
                    }

                    //Fin MAHG 09/07/2010
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
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        /// <summary>
        /// Evento, Realiza el llamado para la modificación o inserción del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, System.EventArgs e)
        {
            try
            {
                //FIN
                //Auto:Diego Montejano Avila
                //Proyecto: Auditoria 2014
                //Fecha: 2014/09/19
                //Observaciones:Se valida la complejidad de la contraseña 
                Users objUser = new Users();
                bool bolPassword = false;
                Security objSecurity = new Security();

                /*
                    GAMM. NOTA: Verificar la generacion de la contraseña a este nivel. No es necesario.
                */
                string contrasenia = AuxiliarSeguridad.GeneraContrasena(15,2).ToString();


                //Solo se valida contra la contraseña actual a los usuarios existentes
                if (!string.IsNullOrEmpty(contrasenia) && Request.QueryString["IdUser"] != null)
                {
                    objUser.Login = this.txtUsuario.Text.Trim();
                                        
                    objUser.GetUsers();
                    if (objUser.NewPassword)
                    {
                        bolPassword = Security.VerifyHash(contrasenia.Trim(), objUser.Password.Trim());
                    }
                    else
                    {
                        bolPassword = (objUser.Password == objSecurity.EncryptString(contrasenia.Trim()));
                    }
                    if (bolPassword)
                    {
                        Response.Write("<script language='javascript'>alert('La contraseña nueva no puede ser igual a la contraseña anterior.');</script>");
                        return;
                    }
                }
                if (ValidarCaracteresEspeciales())
                {
                    DisplayMessage("No se permite introducir caracteres especiales");
                }
                else if (!string.IsNullOrEmpty(contrasenia) && !this.ValidaContrasena(this.txtUsuario.Text.Trim(),
                            contrasenia,
                             contrasenia))
                    return;
                else if (!string.IsNullOrEmpty(contrasenia) && !this.ValidaContrasena(contrasenia))
                    return;
                //FIN
                else
                {

                    /*
                     GAMM.
                     */
                    Mail objMail = new Mail();
                    int UserId = 0;

                    if (Request.QueryString["IdUser"] != null && Request.QueryString["IdUser"] != string.Empty)
                    {
                        this.updateUser(Convert.ToInt32(Request.QueryString["IdUser"]),contrasenia);
                        this.RegisterLog(Log.EnumActionsLog.ModificarUsuario, Convert.ToInt32(Request.QueryString["IdUser"]), "Id Usuario:" + Convert.ToInt32(Request.QueryString["IdUser"]) + " Usuario:" + this.txtNombre.Text.ToUpper());
                        this.DisplayMessageExito("El usuario fue modificado exitosamente");

                        //Obtenemos la información para el envío del mail desde la base
                        objMail.TipoMail = Mail.TiposMail.ResetPassword;
                        UserId = Convert.ToInt32(Request.QueryString["IdUser"]);
                    }
                    else
                    {
                        int idUser = this.insertUser(contrasenia);
                        this.RegisterLog(Log.EnumActionsLog.InsertarUsuario, idUser, "Id Usuario:" + idUser + " Usuario:" + this.txtNombre.Text.ToUpper());
                        this.DisplayMessageExito("El usuario fue ingresado exitosamente");

                        //Obtenemos la información para el envío del mail desde la base
                        objMail.TipoMail = Mail.TiposMail.SendUser;
                        UserId = idUser;

                    }

                    /*
                     I. GAMM 20200211 Al grabar cargamos nuevamente los permisos de usuario.
                    */
                    DataTable dtPadres;
                    Permissions objMenu = new Permissions();

                    objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
                    objMenu.Parent = true;

                    dtPadres = objMenu.ConsultPermissionsUserGeneral((int)Session["IdUser"]).Tables[0];

                    Session["dtPadres"] = dtPadres;
                    //Session.Add("dtPadres", dtPadres);
                    /*
                    I. GAMM 20200211 Al grabar cargamos nuevamente los permisos de usuario.
                    */


                    /*
                    GAMMM. 
                    */
                    string resEnvio = "";

                    try
                    {
                        //Enviamos usuario
                        if (objMail.TipoMail.ToString() == Mail.TiposMail.SendUser.ToString())
                        {
                            resEnvio = envioCorreos(objMail, txtEmail.Text.Trim().ToString(), txtUsuario.Text.Trim().ToString());
                            this.RegisterLog(Log.EnumActionsLog.EnvioCorreoUsuario, UserId, "Id Usuario:" + UserId.ToString() + " Usuario:" + this.txtNombre.Text.ToUpper());


                            //Enviamos Contraseña
                            resEnvio = "";
                            objMail.TipoMail = Mail.TiposMail.SendPassword;
                            resEnvio = envioCorreos(objMail, txtEmail.Text.Trim().ToString(), contrasenia);
                            this.RegisterLog(Log.EnumActionsLog.EnvioCorreoContrasena, UserId, "Id Usuario:" + UserId.ToString() + " Usuario:" + this.txtNombre.Text.ToUpper());
                        }
                        else {
                            resEnvio = "";
                            resEnvio = envioCorreos(objMail, txtEmail.Text.Trim().ToString(), contrasenia);
                        }


                        
                    }

                    catch (Exception ex)
                    {
                        Response.Write("<script language='javascript'>alert('"+ resEnvio + "');</script>");
                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /*Proyecto: AMEX
                     Requerimiento: Permisos de crear usuarios
                     Funcionalidad: Permite asociar permisos y reportes a los usuarios
                     Autor: Ricardo Jose Silva Gomez
                     Fecha: 07/06/2011                     
                   */
        //Inicio RJSG 07/06/2011

        protected void LnkNuevoMedico_Click(object sender, EventArgs e)
        {
            string NombrePrestador = Encoder.HtmlEncode(txtNombre.Text);
            string Email = Encoder.HtmlEncode(txtEmail.Text);
            string Ciudad = Encoder.HtmlEncode(ddlCiudad.SelectedValue);
            //string Empresa = ddlCompania.SelectedValue;

            string IdPrestador = Encoder.HtmlEncode(txtIdPrestador.Text);

            this.OpenWindow("AE_proveedorprestador.aspx?NombrePrestador=" + NombrePrestador + "&Email=" + Email + "&Ciudad=" + Ciudad + "&Validar=Valido" + "&IdPrestador=" + IdPrestador, 750, 550);

        }

        protected void LnkModificar_Click(object sender, EventArgs e)
        {
            string NombrePrestador = Encoder.HtmlEncode(txtNombre.Text);
            string Email = Encoder.HtmlEncode(txtEmail.Text);
            string Ciudad = Encoder.HtmlEncode(ddlCiudad.SelectedValue);
            string IdProveedor ="";
            //string Empresa = ddlCompania.SelectedValue;

            string IdPrestador = Encoder.HtmlEncode(txtIdPrestador.Text);
            if (txtIdProveedor.Text != "0")
                IdProveedor = Encoder.HtmlEncode(txtIdProveedor.Text);

            this.OpenWindow("AE_proveedorprestador.aspx?NombrePrestador=" + NombrePrestador + "&Email=" + Email + "&Ciudad=" + Ciudad + "&Validar=Valido" + "&IdPrestador=" + IdPrestador + "&IdProveedor=" + IdProveedor, 750, 500);

        }
        //Fin RJSG 07/06/2011

        protected static string  envioCorreos(Mail objMail, string correo, string usuPass) {
            try
            {
                //Enviamos correo
                objMail.CargaParametros();
                objMail.MailBody = String.Format(objMail.MailBody, usuPass);
                objMail.EmailTo = correo;

                objMail.SendNow();

                return "";
            }

            catch (Exception ex)
            {
                //Response.Write("<script language='javascript'>alert('Ocurrió un error al mandar el mail');</script>");
                return "Ocurrió un error al mandar el mail";
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Método, realiza la inserción del usuario
        /// </summary>
        /// <returns></returns>
        public int insertUser(string contrasenia)
        {
            /*Proyecto: AMEX
                     Requerimiento: Permisos de crear usuarios
                     Funcionalidad: Permite asociar permisos y reportes a los usuarios
                     Autor: Ricardo Jose Silva Gomez
                     Fecha: 07/06/2011                     
                   */
            //Inicio RJSG 07/06/2011

            Users objUser = new Users();
            this.loadObjectUser(objUser, contrasenia);
            ArrayList arrPermissions = this.loadObjectsPermissions();
            ArrayList arrReportes = this.loadObjectsReportes();
            ArrayList arrTipoServicios = this.loadObjectsTipoServicios();
            int idUser = objUser.InsertUsers(arrPermissions, arrReportes, arrTipoServicios);
            return idUser;

            //Fin RJSG 07/06/2011
        }


        /// <summary>
        /// Método, realiza la modificación del usuario
        /// </summary>
        /// <param name="p_idUser"></param>
        public void updateUser(int p_idUser, string contrasenia)
        {
            Users objUser = new Users();
            this.loadObjectUser(objUser, contrasenia);
            objUser.IdUser = p_idUser;
            ArrayList arrPermissions = this.loadObjectsPermissions();
            ArrayList arrReportes = this.loadObjectsReportes();
            ArrayList arrTipoServicios = this.loadObjectsTipoServicios();
            //Inicio
            //Autor:Diego Montejano Avila
            //Fecha:24/09/2014
            //Proyecto:Auditoria 2014
            //Observaciones:No debe guardar la contraseña generada en el historico de contraseñas
            objUser.GuardarHistoricoContrasena = false;
            objUser.UpdateUsers(arrPermissions, arrReportes, arrTipoServicios);
        }

        /// <summary>
        /// Método, carga un objeto usuario con los datos del formulario
        /// </summary>
        /// <param name="objUser"></param>
        public void loadObjectUser(Users objUser, string contrasenia)
        {
            if (contrasenia != string.Empty && contrasenia.Length < 15)
                throw new Exception("La contraseña debe contener mas de 15 caracteres");
            //Validaciones
            Security objSecurity;
            objSecurity = new Security();
            objUser.NameUser = this.txtNombre.Text.ToUpper();

            /*Proyecto: AMEX
            Requerimiento: Permisos de usuarios por empresa
            Funcionalidad: Oculta el campo empresa para México
            Autor: Marco A. Herrera Gabriel
            Fecha: 09/07/2010                     
            */
            //<--Inicio MAHG 09/07/2010
            if (ddlCompania.SelectedValue != null && ddlCompania.SelectedValue != "")
            {
                objUser.empresa_id = Convert.ToInt32(this.ddlCompania.SelectedValue);
            }
            //Fin MAHG 09/07/2010 -->

            objUser.Email = this.txtEmail.Text;
            objUser.Login = this.txtUsuario.Text.Trim();
            objUser.IdCiudad = Convert.ToInt32(this.ddlCiudad.SelectedValue);
            objUser.Active = Convert.ToBoolean(this.rblEstado.SelectedValue);

            if (this.txtIdPrestador.Text != "")
            {
                objUser.IdPrestador = Convert.ToInt32(this.txtIdPrestador.Text);
            }

            if (contrasenia != string.Empty)
            {

                /* Proyecto: AMEX
                   * Autor: Marco A. Herrera Gabriel
                   * Fecha: 22/02/10
                   * Funcionalidad: Se valida que el password temporal no se asigne más de una vez al usuario
                */
                #region Que el password temporal no sea asignado más de una vez

                Users objUserPasswordInicial = new Users();
                objUserPasswordInicial.Login = this.txtUsuario.Text.Trim();
                objUserPasswordInicial.GetUsers();

                //objUserPasswordInicial.Password = contrasenia.Trim();

                if (objUserPasswordInicial.PasswordInicial())
                {
                    throw new Exception("Esta contraseña ya fue utilizada, por favor introduzca otra contraseña.");
                }

                #endregion

                /*Inicio MAHG Marco A. Herrera Gabriel 19/01/10 
                  Se cambia el método de encriptación a SHA256
                */
                //objUser.Password = objSecurity.EncryptString(this.txtContrasena.Text);

                objUser.Password = Security.SHA256_Encrypt(contrasenia, null);
                objUser.NewPassword = true;
                objUser.ExpiredPassword = true;

                //Fin MAHG 19/01/10
                
            }
            else
            {
                #region Tipo de Password

                Users objUserTipoPassword = new Users();
                objUserTipoPassword.Login = this.txtUsuario.Text.Trim();
                objUserTipoPassword.GetUsers();

                objUser.NewPassword = objUserTipoPassword.NewPassword;

                #endregion
            }

            //MFA code added Bhushan
            if (chkBypassMfa.Checked == true)
            {
                objUser.BypassMFA = "Yes";
            }
            else
            {
                objUser.BypassMFA = "No";
            }
        }

        /// <summary>
        /// Método, consulta el usuario y carga el formulario a partir del objeto
        /// </summary>
        /// <param name="p_idUser"></param>
        public void loadFormUser(int p_idUser)
        {
            Users objUser = new Users();
            objUser.IdUser = p_idUser;
            objUser.GetUsers();
            this.txtNombre.Text = objUser.NameUser;
            //this.ddlCompania.SelectedValue = objUser.empresa_id.ToString();
            this.rblEstado.SelectedValue = objUser.Active.ToString();
            this.txtUsuario.Text = objUser.Login;
            this.txtEmail.Text = objUser.Email;
            this.ddlCiudad.SelectedValue = objUser.IdCiudad.ToString();
            this.txtIdPrestador.Text = objUser.IdPrestador.ToString();
            // MFA coded added Bhushan
            if (objUser.BypassMFA != null)
            {
                if (objUser.BypassMFA == "Yes")
                {
                    this.chkBypassMfa.Checked = true;
                }
                else
                {
                    this.chkBypassMfa.Checked = false;
                }
            }
        }

        /// <summary>
        /// Método, carga el listado de los tipos de servicio
        /// </summary>
        public void loadTipoServicios(int p_idUser)
        {
            DataTable dtTipoServicios;
            TipoServicios objTipoServicios = new TipoServicios();

            dtTipoServicios = objTipoServicios.ConsultTipoServicioEmpresa(Convert.ToInt16(Session["Company"])).Tables[0];

            this.ChlTipoServicios.DataSource = dtTipoServicios;
            this.ChlTipoServicios.DataTextField = "NombreTipoServicio";
            this.ChlTipoServicios.DataValueField = "IdTipoServicio";
            this.ChlTipoServicios.DataBind();

            if (p_idUser != 0)
            {
                DataTable dtTipoServiciosAsignados;
                dtTipoServiciosAsignados = objTipoServicios.ConsultTipoServiciosAsignados(p_idUser).Tables[0];

                for (int i = 0; i < dtTipoServiciosAsignados.Rows.Count; i++)
                {
                    this.ChlTipoServicios.Items.FindByValue(Convert.ToString(dtTipoServiciosAsignados.Rows[i]["IdTipoServicio"])).Selected = true;
                }
            }

        }

        /// <summary>
        /// Método, carga el listado de permisos
        /// </summary>
        public void loadPermissions(int p_idUser)
        {
            DataTable dtHijos;
            DataTable dtPadres;
            DataTable dtPadresUsuario = new DataTable();
            DataTable dtHijosUsuario = new DataTable();
            Permissions objPermission = new Permissions();

            objPermission.Parent = true;

            dtPadres = objPermission.ConsultPermission().Tables[0];

            //Si se recibe id del usuario se consultan los permisos que tiene actualmente
            if (p_idUser != 0)
            {
                dtPadresUsuario = objPermission.ConsultPermissionsUser(p_idUser).Tables[0];
                dtPadresUsuario.PrimaryKey = new DataColumn[] { dtPadresUsuario.Columns["IdPermission"] };
            }

            for (int i = 0; i < dtPadres.Rows.Count; i++)
            {
                if (Convert.IsDBNull(dtPadres.Rows[i]["IdPermissionParent"]))
                {
                    dtHijos = this.loadChilds(Convert.ToInt32(dtPadres.Rows[i]["IdPermission"]), 0);

                    //Si se recibe el id del usuario, se consultan los permisos que tiene actualmente
                    if (p_idUser != 0)
                    {
                        dtHijosUsuario = this.loadChilds(Convert.ToInt32(dtPadres.Rows[i]["IdPermission"]), p_idUser);
                        dtHijosUsuario.PrimaryKey = new DataColumn[] { dtHijosUsuario.Columns["IdPermission"] };
                    }

                    //Se adiciona el item del padre
                    this.chlPermisos.Items.Add(new ListItem("<b>" + dtPadres.Rows[i]["NamePermission"].ToString() + "</b>", dtPadres.Rows[i]["IdPermission"].ToString()));

                    //Si el permiso se encuentra en el listado de permisos del usuario se seleciona				
                    if (p_idUser != 0 && dtPadresUsuario.Rows.Contains(dtPadres.Rows[i]["IdPermission"]))
                    {
                        this.chlPermisos.Items[this.chlPermisos.Items.Count - 1].Selected = true;
                    }

                    //Recorrer los hijos
                    foreach (DataRow row in dtHijos.Rows)
                    {

                        this.chlPermisos.Items.Add(new ListItem("--" + row["NamePermission"].ToString(), row["IdPermission"].ToString()));

                        //Si el permiso se encuentra en el listado del usuario se selecciona
                        if (p_idUser != 0 && dtHijosUsuario.Rows.Contains(row["IdPermission"]))
                        {
                            this.chlPermisos.Items[this.chlPermisos.Items.Count - 1].Selected = true;
                        }
                    }

                }
            }
        }


        /// <summary>
        /// Método, realiza la búsqueda de los hijos del permisos
        /// </summary>
        /// <param name="p_idParent">Id del pernmiso padre</param>
        /// <returns></returns>
        public DataTable loadChilds(int p_idParent, int p_idUsuario)
        {
            DataTable dtHijos;
            Permissions objMenu = new Permissions();
            objMenu.IdPermissionParent = p_idParent;
            objMenu.Parent = false;
            //Si no envian id de usuario se consultan todos los permisos, de lo contrario se listan los permisos de el usuario específico
            if (p_idUsuario == 0)
            {
                dtHijos = objMenu.ConsultPermission().Tables[0];
            }
            else
            {
                dtHijos = objMenu.ConsultPermissionsUser(p_idUsuario).Tables[0];
            }
            return dtHijos;

        }

        /*Proyecto: AMEX
                     Requerimiento: Permisos de crear usuarios
                     Funcionalidad: Permite asociar permisos y reportes a los usuarios
                     Autor: Ricardo Jose Silva Gomez
                     Fecha: 07/06/2011                     
                   */
        //Inicio RJSG 07/06/2011

        public void loadFormPrestador(int p_IdPrestador)
        {
            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = p_IdPrestador;
            objPrestador.Empresa_id = Convert.ToInt32(Session["Company"]);
            objPrestador.GetPrestadores();
            this.txtPrestador.Text = objPrestador.NombrePrestador;

        }

        public ArrayList loadObjectsTipoServicios()
        {
            ArrayList lstTipoServicios = new ArrayList();

            foreach (ListItem item in this.ChlTipoServicios.Items)
            {
                if (item.Selected)
                {
                    lstTipoServicios.Add(Convert.ToInt32(item.Value));
                }
            }

            return lstTipoServicios;

        }

        //Fin RJSG 07/06/2011


        /*Proyecto: AMEX
                     Requerimiento: Permisos de crear usuarios
                     Funcionalidad: Permite asociar permisos y reportes a los usuarios
                     Autor: Ricardo Jose Silva Gomez
                     Fecha: 07/06/2011                     
                   */
        //Inicio RJSG 07/06/2011

        /// <summary>
        /// Método, carga el listado de reportes
        /// </summary>
        public void loadReportes(int p_idUser)
        {
            DataTable dtReporte;
            Reportes objReportes = new Reportes();

            dtReporte = objReportes.ConsultReportes().Tables[0];

            this.ChlReportes.DataSource = dtReporte;
            this.ChlReportes.DataTextField = "NombreReporte";
            this.ChlReportes.DataValueField = "IdReporte";
            this.ChlReportes.DataBind();


            if (p_idUser != 0)
            {
                DataTable dtReportesAsignados;
                dtReportesAsignados = objReportes.ConsultReportesUserAsignados(p_idUser).Tables[0];

                for (int i = 0; i < dtReportesAsignados.Rows.Count; i++)
                {
                    this.ChlReportes.Items.FindByValue(Convert.ToString(dtReportesAsignados.Rows[i]["IdReporte"])).Selected = true;
                }
            }

        }


        public ArrayList loadObjectsReportes()
        {
            ArrayList lstReportes = new ArrayList();

            foreach (ListItem item in this.ChlReportes.Items)
            {
                if (item.Selected)
                {
                    lstReportes.Add(Convert.ToInt32(item.Value));
                }
            }

            return lstReportes;

        }

        //Fin RJSG 07/06/2011


        /// <summary>
        /// Método, carga una arreglo con los ids de los permisos seleccionados
        /// </summary>
        /// <returns></returns>
        public ArrayList loadObjectsPermissions()
        {
            ArrayList lstPermissions = new ArrayList();

            foreach (ListItem item in this.chlPermisos.Items)
            {
                if (item.Selected)
                {
                    lstPermissions.Add(Convert.ToInt32(item.Value));
                }
            }

            return lstPermissions;

        }

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 20/01/10
        /// Funcionalidad: Valida los caracteres de los campos del formulario, los caracteres se consultan de la tabla 
        /// constantes, registro _CaracteresEspecialesLogin
        /// </summary>
        /// <returns>Booleano, Indica si encontro o no caracteres especiales en los campos</returns>
        private bool ValidarCaracteresEspeciales()
        {

            if (Validaciones.ExistenCaracteresEspeciales(txtNombre.Text.Trim()))
            {
                return true;
            }

            if (Validaciones.ExistenCaracteresEspeciales(txtUsuario.Text.Trim()))
            {
                return true;
            }
            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/19
            //Observaciones:Se comenta códig, las validaciones se aplican con extension de funciones
            //if (Validaciones.ExistenCaracteresEspeciales(txtContrasena.Text.Trim()))
            //{
            //    return true;
            //}

            //if (Validaciones.ExistenCaracteresEspeciales(txtConfContrasena.Text.Trim()))
            //{
            //    return true;
            //}
            //FIN
            return false;
        }


        #endregion


    }
}
