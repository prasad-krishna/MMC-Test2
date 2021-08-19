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
using System.Globalization;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using MFAUtil;
//GAMM
using MMC.Seguridad.Utilerias; 

namespace TPA
{
    /// <summary>
    /// Página de ingreso al sistema
    /// </summary>
    public partial class AE_login_admin : System.Web.UI.Page
    {

        #region Attributes
        public string errflag = "";
        private string message = "";
        //Bhushan MFA - start
        //private MFAUtil mfautil;
        string userId = "";
        string username = "";
        string userEmail = ""; //required
        string devicePrint = null;//optional
        string deviceTokenCookie = null;//optional
        string clientGenCookie = "";//optional
        string ipAddress = "";//required
        //string orgName = "Marsh_Dev";//required
        string otp = null; //optional
        string sessionId = null; //optional
        string transactionId = null; //optional
        string userAgent = null;//optional
        //string groups = "Clients,MFA";//required
        private MFA.RSA rsaresponse;
        private string serverAddr = ConfigurationManager.AppSettings["RSA_AA_URI"];
        private string orgName = ConfigurationManager.AppSettings["RSA_AA_ORGNAME"];
        private string groups = ConfigurationManager.AppSettings["RSA_AA_GROUPS"];
        private string securitykey = ConfigurationManager.AppSettings["RSA_AA_SECURITYKEY"];
        //Bhushan MFA - end

        #endregion

        #region Initializing

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //ValidarVersionExplorador();

            try
            {
                if (!this.Page.IsPostBack)
                {
                    //GAMM
                    AntiHack.LimpiaSession();
                    AntiHack.RegenerarSessionId();

                    //this.Session.Clear();
                    //this.Session.Abandon();
                    //FormsAuthentication.SignOut();//Elimina el vale de autenticación

                                        
                }
            }
            catch (Exception ex)
            {
                string message = "";
                message = ex.Message.Replace("'", "");
                message = message.Replace("\r", "");
                message = message.Replace("\n", "");
                message = "<script language='javascript'>alert('Exception :" + message + "')</script>";

                //Inicio 13/10/09 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin 


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
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado para la validación de ingreso al sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIngresar_Click(object sender, System.EventArgs e)
        {
            try
            {

                bool userValid = false;

                /* Inicio Marco A. Herrera Gabriel MAHG 20/01/10 */
                //Valida si existen caracteres especiales en los campos

                if (ValidarCaracteresEspeciales())
                {
                    message = "<script language='javascript'>alert('No se permite introducir caracteres especiales')</script>";
                }//Fin MAHG 20/01/10
                else
                {
                    userValid = this.ValidateUser();
                }
                //Bhushan - The MFA token logic to be written here and call verifyToken aspx page  - start
                //string bypassmfa = Session["bypassMFA"].ToString();
                if (userValid)
                {
                    string bypassmfa = Session["bypassMFA"].ToString();
                    if (bypassmfa != "Yes")
                    {
                        userEmail = (string)Session["userEmail"].ToString().ToLower();
                        //mfa 26 sep - start
                        string usercookie = string.Empty;
                        usercookie = userEmail.ToLower();
                        usercookie = usercookie.Replace("@", "");
                        usercookie = usercookie.Replace(".com", "");
                        string devcookieage = "deviceTokenCookieage" + usercookie;
                        string devcookie = "deviceTokenCookie" + usercookie;
                        //mfa 26 sep - end
                        LogMessage("Checking MFA Cookies");
                        if (Request.Cookies[devcookieage] == null)
                        {
                            clientGenCookie = "noclientgencookie";
                        }
                        else
                        {
                            clientGenCookie = Request.Cookies[devcookieage].Value;

                        }

                        if (Request.Cookies[devcookie] == null)
                        {
                            deviceTokenCookie = String.Empty;
                        }
                        else
                        {
                            deviceTokenCookie = Request.Cookies[devcookie].Value;
                        }

                        devicePrint = Request.Form["deviceprintid"];
                        userAgent = Request.Form["useragentid"];
                        //userEmail = (string)Session["userEmail"];

                        LogMessage("Device Print-" + devicePrint + " UserAgent-" + userAgent + " UserEmail-" + userEmail);
                        errflag = "";
                        MFA mfa = new MFA();
                        ipAddress = getClientIP(HttpContext.Current.Request);
                        LogMessage("Ipaddress-" + ipAddress);
                        MFA.MFAUser user = new MFA.MFAUser();
                        user.userEmail = userEmail.ToLower();
                        user.orgName = orgName;
                        user.ipAddress = ipAddress;
                        user.clientGenCookie = clientGenCookie;
                        user.groups = groups.Split(',').ToList();
                        user.devicePrint = devicePrint;
                        user.deviceTokenCookie = deviceTokenCookie;
                        user.userAgent = userAgent;

                        string hashedId = mfa.myGetHashedId(user.userEmail);
                        user.username = hashedId;
                        LogMessage("Hashedid-" + hashedId);
                        string payloadEnc = mfa.GetEncryptedPayload(securitykey, user);
                        LogMessage("PayloadEnc-" + payloadEnc);
                        LogMessage("Security key-" + securitykey);
                        string hmac = mfa.GetHMAC(securitykey, payloadEnc);
                        LogMessage("Hmackey-" + hmac);
                        LogMessage("ServerAddress-" + serverAddr);
                        Session["userStatus"] = "";
                        try
                        {
                            rsaresponse = mfa.GetOtp(user, serverAddr, securitykey);
                            LogMessage("RSA response  UserStatus- " + rsaresponse.userStatus + " Actioncode-" + rsaresponse.actionCode + " OTP-" + rsaresponse.otp);


                            Session["userStatus"] = rsaresponse.userStatus.ToString();

                            if (rsaresponse.userStatus.ToUpper() != "LOCKOUT" && rsaresponse.userStatus.ToUpper() != "DELETE")
                            {
                                if (rsaresponse.actionCode != null)
                                {
                                    if (rsaresponse.actionCode.ToUpper() == "ALLOW")
                                    {
                                        HttpCookie appCookie = Request.Cookies[devcookie];
                                        appCookie.Value = rsaresponse.deviceTokenCookie;
                                        appCookie.Expires = DateTime.MaxValue;
                                        Response.Cookies.Add(appCookie);
                                    }
                                }

                                if (!string.IsNullOrEmpty(rsaresponse.otp))
                                {

                                    try
                                    {
                                        LogMessage("Sending OTP email to the user");
                                        rsaresponse.userEmail = userEmail.ToLower();
                                        rsaresponse.ipAddress = ipAddress;
                                        rsaresponse.clientGenCookie = clientGenCookie;
                                        Session["rsaobj"] = rsaresponse;
                                        //Session["userStatus"] = rsaresponse.userStatus;
                                        Mail objMail = new Mail();
                                        string strMailFrom = System.Configuration.ConfigurationManager.AppSettings["RSA_MAIL_FROM"].ToString();

                                        objMail.EmailFrom = strMailFrom;

                                        objMail.MailSubject = "Token de seguridad para verificación de identidad otorgado por Marsh.";
                                        objMail.MailBody = "Nombre de usuario: " + userEmail + "<br/><br/>" +
                                        "Por favor utilice el siguiente token de seguridad: " + rsaresponse.otp +
                                        "<br/>***Este token será válido por los siguientes 10 minutos.*** <br/><br/>" +
                                        "Para evitar errores, puede copiar y pegar el token de seguridad provisto directamente en el campo Token de Seguridad. <br/>" +
                                        "Después de ingresar el token de seguridad, puede comenzar a usar el sistema de inmediato. <br/><br/>" +
                                        "***Este es un correo generado de forma automática. Favor de no responder al remitente. ";
                                        objMail.EmailTo = userEmail.ToLower();


                                        objMail.SendNow();
                                        LogMessage("OTP email sent to the user");

                                        try
                                        {
                                            Response.Redirect("verifyToken.aspx");
                                        }
                                        catch (System.Threading.ThreadAbortException et)
                                        {
                                            errflag = "No";
                                            LogMFAError(et);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogMFAError(ex);
                                        if (errflag == "No")
                                        { }
                                        else
                                        {
                                            mfaExceptionEmail(ex, userEmail.ToLower(), serverAddr);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                try
                                {
                                    //Session["userStatus"] = rsaresponse.userStatus;
                                    Response.Redirect("verifyToken.aspx");
                                }
                                catch (System.Threading.ThreadAbortException et)
                                {
                                    errflag = "No";
                                    LogMFAError(et);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogMFAError(ex);
                            if (errflag == "No")
                            { }
                            else
                            {
                                mfaExceptionEmail(ex, userEmail.ToLower(), serverAddr);
                            }

                        }
                    } // session bypassmfa 
                }
                //Bhushan - The MFA token logic to be written here and call verifyToken aspx page  - end
                if (userValid && Session["Company"] != null)
                {
                    //Inicio
                    //Autor:Diego Montejano Avila
                    //Proyecto:Auditoria 2014
                    //Fecha:23/09/2014
                    //Observaciones:Redirigir al disclaimer despues de logueo
                    //Response.Redirect("interfaz_empleado/forma/Home.aspx");
                    Session["urlRedirectDisclaimer"] = "interfaz_empleado/forma/Home.aspx";
                    Response.Redirect("AEDisclaimer.aspx", true);
                    return;
                    //FIN
                }
                else
                {

                    if (userValid && Session["Company"] == null)
                    {
                        //Inicio
                        //Autor:Diego Montejano Avila
                        //Proyecto:Auditoria 2014
                        //Fecha:23/09/2014
                        //Observaciones:Redirigir al disclaimer despues de logueo
                        //Response.Redirect("AE_Empresa.aspx");
                        //Response.Write("<script language='javascript'>fnOpenDisclaimer('AE_Empresa.aspx')</script>");
                        Session["urlRedirectDisclaimer"] = "AE_Empresa.aspx";
                        Response.Redirect("AEDisclaimer.aspx", true);
                        return;
                        //FIN
                    }
                    else
                    {
                        /*if(objUser.empresa_id != 0)
                        {
                            EmpresaDatos objEmpresa = new EmpresaDatos();
                            objEmpresa.Empresa_id = objUser.empresa_id;
                            objEmpresa.GetEmpresaDatos();

                            if(objEmpresa.IntentosBloqueaPassword > 0)
                            {
                                if(objEmpresa.IntentosBloqueaPassword > objUser.AccessUnsuccessful)
                                {
                                    message = "";
                                    message = "<script language='javascript'>alert('Usted supero el número de intentos fallidos para acceder, su contraseña ha sido bloqueada, consulte con el administrador del sistema')</script>";
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"Contraseña Bloqueda", message);
                                }
                            }
                        }*/


                        //Registrar error en log de errores
                        LogErrors objError = new LogErrors();
                        objError.MessageError = "Ingreso al sistema incorrecto, nombre usuario: " + this.txtUser.Text + " contraseña: " + this.txtPassword.Text;
                        objError.IP = Request.UserHostAddress;
                        objError.PageError = Request.RawUrl;
                        objError.InsertLogErrors();

                        if (message == string.Empty)
                        {
                            message = "<script language='javascript'>alert('Usuario y/o contraseña inválida, usuario bloqueado o inactivo')</script>";
                        }

                        //Inicio 13/10/09 MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Contraseña Inválida", message, false);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Contraseña Inválida", message);
                        }
                        //Fin 13/10/09 MAHG  
                    }
                }


            }
            catch (Exception ex)
            {
                string message = "";
                message = ex.Message.Replace("'", "");
                message = message.Replace("\r", "");
                message = message.Replace("\n", "");
                message = "<script language='javascript'>alert('Exception :" + message + "')</script>";

                //Inicio 13/10/09 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin 


            }
        }

        private static void mfaExceptionEmail(Exception ex, string userEmail, string serverAddr)
        {
            Mail objMail = new Mail();
            string strMailFrom = System.Configuration.ConfigurationManager.AppSettings["RSA_MAIL_FROM"].ToString();
            string strMailTo = System.Configuration.ConfigurationManager.AppSettings["RSA_SUPPORT_MAIL"].ToString();
            objMail.EmailFrom = strMailFrom;
            objMail.MailSubject = "Alert - Historias Clinicas MFA exception occured";
            objMail.MailBody = "User Email - " + userEmail + "<br/>" +
                "Server Address - " + serverAddr + "<br/>" +
                "The exception details are as : <br/> Ex - " + ex + "<br/>" +
                " Ex message - " + ex.Message + "<br/> " +
                " Ex stacktrace - " + ex.StackTrace + "<br/>" +
                "---------------------------";
            objMail.EmailTo = strMailTo;

            objMail.SendNow();

        }

        private void LogMFAError(Exception ex)
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            message += Environment.NewLine;

            message += "------Error log-----------------------------------------------------";

            message += Environment.NewLine;

            message += string.Format("Message: {0}", ex.Message);

            message += Environment.NewLine;

            message += string.Format("StackTrace: {0}", ex.StackTrace);

            message += Environment.NewLine;

            message += string.Format("Source: {0}", ex.Source);

            message += Environment.NewLine;

            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());

            message += Environment.NewLine;

            message += "-----------------------------------------------------------";

            message += Environment.NewLine;

            string path = Server.MapPath("~/Logs/Log.txt");

            using (StreamWriter writer = new StreamWriter(path, true))
            {

                writer.WriteLine(message);

                writer.Close();

            }

        }
        private void LogMessage(string logmsg)
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            message += Environment.NewLine;

            message += "------Log message-----------------------------------------------------";

            message += Environment.NewLine;

            message += string.Format("Message: {0}", logmsg.ToString());


            message += Environment.NewLine;

            message += "-----------------------------------------------------------";

            message += Environment.NewLine;

            string path = Server.MapPath("~/Logs/Log.txt");

            using (StreamWriter writer = new StreamWriter(path, true))
            {

                writer.WriteLine(message);

                writer.Close();

            }

        }
        //MFA - Bhushan 
        private String getClientIP(HttpRequest Request)
        {

            //String remoteIP = "";
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //ipList = "205.156.84.229";
            LogMessage("Iplist -" + ipList);

            if (!string.IsNullOrEmpty(ipList))
            {
                int portStart = ipList.LastIndexOf(':');
                if (portStart < 0)
                { }
                else
                {
                    ipList = ipList.Substring(0, portStart);
                }
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
        /// <summary>
        /// Evento, despliega la página que contien los términos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkTerminos_Click(object sender, System.EventArgs e)
        {
            string script;
            script = "<script language='javascript'>window.open('" + "https://co.mercer.com/termsofuse.htm?siteLanguage=102" + "')</script>";



            //Inicio 13/10/09 MAHG Se verifica si la solicitud es Asincrona
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
        protected void lnkPrivacidad_Click(object sender, System.EventArgs e)
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", script);
            }
            //Fin 

        }

        #endregion

        #region Methods

        /// <summary>
        /// Método, valida si el usuario con el login y contraseña ingresado es correcto
        /// </summary>
        /// <returns></returns>
        public bool ValidateUser()
        {
            #region Declaración de variables

            Users objUser;
            Security objSecurity;
            EmpresaDatos objEmpresa;
            //Inicio MAHG Marco A. Herrera G. 19/01/10
            bool bolPassword;//Identifica si el password ingresado es correcto
            //Fin MAHG Marco A. Herrera G. 19/01/10

            #endregion

            #region Inicialización de variables

            objUser = new Users();
            objSecurity = new Security();
            objEmpresa = new EmpresaDatos();
            objUser.Login = this.txtUser.Text;
            //Inicio MAHG Marco A. Herrera G. 19/01/10
            bolPassword = false;
            //Fin MAHG Marco A. Herrera G. 19/01/10

            #endregion

            try
            {
                /*Inicio MAHG Marco A. Herrera Gabriel 19/01/10 
                 Se verifica si la contraseña esta encriptada con SHA256 ó MD5
                */

                objUser.GetUsers();
                // NFA code added - start
                if (String.IsNullOrEmpty(objUser.Email))
                { }
                else
                {
                    Session["userEmail"] = objUser.Email.ToLower();
                    if (objUser.BypassMFA == null)
                    {
                        Session["bypassMFA"] = "No";
                    }
                    else
                    {
                        Session["bypassMFA"] = objUser.BypassMFA;
                    }
                }
                // NFA code added - end
                if (objUser.NewPassword)
                {
                    bolPassword = Security.VerifyHash(this.txtPassword.Text.Trim(), objUser.Password.Trim());
                }
                else
                {
                    bolPassword = (objUser.Password == objSecurity.EncryptString(this.txtPassword.Text.Trim()));
                }

                //Inicio MAHG  19/01/10

                if (objUser.IdUser != 0 && objUser.Active && !objUser.Blocking && bolPassword)
                {
                    //GAMM Regeneramos session
                    //AntiHack.LimpiaSession();
                    //AntiHack.RegenerarSessionId();

                    //Inicio 21/01/10 Marco A. Herrera G. MAHG
                    GenerarTicket(objUser);//Se genera el ticket de autenticación
                    //Fin MAHG 21/01/10


                    //GAMM Insertamos Session ligada al usuario
                    PB_PaginaBase objPagina = new PB_PaginaBase();
                    objPagina.RegisterSession(objUser.IdUser, Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value, HttpContext.Current.Request.Browser.Browser);

                    Session["IdUser"] = objUser.IdUser;
                    Session["IPUser"] = Request.UserHostAddress;

                    Session["NameUser"] = objUser.NameUser;
                    Session["IdPrestador"] = objUser.IdPrestador;
                    Session["Administrador"] = "";

                    /* Proyecto: PenTest
                     * GAMM
                     * Fecha: 24/04/2019
                     * Funcionalidad: Sesión para manejar el acceso al modulo de usuarios
                     */
                    Session["MostrarOpcionUsuarios"] = objUser.MostrarOpcionUsuarios;

                    //PB_PaginaBase objPagina = new PB_PaginaBase();
                    objPagina.RegisterLog(Log.EnumActionsLog.IngresarSistema, objUser.IdUser, "IdUser:" + objUser.IdUser + " Usuario:" + objUser.Login);

                    if (objUser.IdUser != 0)
                    {
                        //Inicio MAHG 12/07/10
                        //Se comenta el código
                        /*Session["Company"] = objUser.empresa_id;
                        objEmpresa.Empresa_id = objUser.empresa_id;
                        objEmpresa.GetEmpresaDatos();
                        */

                        EmpresaUsers objEmpresaUsers = new EmpresaUsers();

                        objEmpresaUsers.IdUser = objUser.IdUser;
                        DataSet dsDatos = objEmpresaUsers.GetEmpresasUser();

                        if (dsDatos.Tables[0].Rows.Count <= 0)
                        {
                            message = "<script language='javascript'>alert('No tiene permisos en ninguna empresa, por favor contacte al administrador')</script>";
                            return false;
                        }
                        else
                        {
                            objEmpresa.Empresa_id = int.Parse(dsDatos.Tables[0].Rows[0]["empresa_id"].ToString());
                            objEmpresa.GetEmpresaDatos();

                            if (dsDatos.Tables[0].Rows.Count == 1)
                            {
                                Session["Company"] = objEmpresa.Empresa_id;
                            }
                        }

                        //Fin MAHG 12/07/10


                        //Inicio Marco A. Herrrera Gabriel MAHG 21/01/10
                        //Se restablece en cero los intentos de acceso al sistema
                        objUser.UpdateUsersAccess(0, null, null);
                        //Fin Marco A. Herrrera Gabriel MAHG 21/01/10

                        /* Proyecto: AMEX
                         * Autor: Marco A. Herrera Gabriel
                         * Fecha: 22/02/10
                         * Funcionalidad: Se valida si la contraseña temporal no ha superado las 72 Horas después de la creación del usuario
                        */
                        #region Se valida si la contraseña temporal no ha superado las 72 Horas después de la creación del usuario

                        objUser.Password = this.txtPassword.Text.Trim(); //Se asigna la contraseña

                        if (objUser.PasswordInicial())//Se valida que el usuario aún tenga asignado la contraseña temporal
                        {
                            if (Validaciones.ObtenerHoras(objUser.DateLastPassword, DateTime.Now) >= objEmpresa.HorasCaducaPassword)
                            {
                                message = "<script language='javascript'>alert('Su contraseña temporal expiró, por favor informe al administrador para generar una nueva.')</script>";

                                return false;
                            }
                            else
                            {
                                Response.Redirect("AE_Contrasena.aspx", true);
                            }
                        }

                        #endregion


                        if (objEmpresa.DiasCaducaPassword > 0 || objUser.ExpiredPassword)
                        {

                            if (objUser.DateLastPassword == new DateTime(1, 1, 1)
                                || objUser.DateLastPassword.AddDays(Convert.ToDouble(objEmpresa.DiasCaducaPassword)) <= DateTime.Now
                                || objUser.ExpiredPassword)
                            {
                                Response.Redirect("AE_Contrasena.aspx", true);
                            }


                        }
                    }

                    return true;
                }
                else
                {

                    /*Inicio 21/01/10 Marco A. Herrera Gabriel 
                     Se actualiza el intento de autenticación de acuerdo a la empresa.
                     */

                    if (objUser.Blocking)
                    {
                        message = "<script language='javascript'>alert('Usted supero el número de intentos fallidos para acceder, su contraseña ha sido bloqueada, consulte con el administrador del sistema')</script>";
                    }
                    else
                    {

                        //Inicio MAHG 12/07/10
                        //Se comenta el código
                        /*
                        objEmpresa.Empresa_id = objUser.empresa_id;
                        objEmpresa.GetEmpresaDatos();
                        */

                        if (objUser.IdUser != 0 && bolPassword)
                        {

                            EmpresaUsers objEmpresaUsers = new EmpresaUsers();

                            objEmpresaUsers.IdUser = objUser.IdUser;
                            DataSet dsDatos = objEmpresaUsers.GetEmpresasUser();

                            if (dsDatos.Tables[0].Rows.Count <= 0)
                            {
                                message = "<script language='javascript'>alert('No tienes permisos en ninguna empresa, por favor contacte al administrador')</script>";
                                return false;
                            }
                            else
                            {
                                objEmpresa.Empresa_id = int.Parse(dsDatos.Tables[0].Rows[0]["empresa_id"].ToString());
                                objEmpresa.GetEmpresaDatos();

                                if (dsDatos.Tables[0].Rows.Count == 1)
                                {
                                    Session["Company"] = objEmpresa.Empresa_id;
                                }
                            }
                        }
                        else
                        {
                            if (objUser.IdUser != 0 && !bolPassword)
                            {
                                EmpresaUsers objEmpresaUsers = new EmpresaUsers();

                                objEmpresaUsers.IdUser = objUser.IdUser;
                                DataSet dsDatos = objEmpresaUsers.GetEmpresasUser();

                                if (dsDatos.Tables[0].Rows.Count <= 0)
                                {
                                    message = "<script language='javascript'>alert('No tienes permisos en ninguna empresa, por favor contacte al administrador')</script>";
                                    return false;
                                }
                                else
                                {
                                    objEmpresa.Empresa_id = int.Parse(dsDatos.Tables[0].Rows[0]["empresa_id"].ToString());
                                    objEmpresa.GetEmpresaDatos();

                                    if (objEmpresa.IntentosBloqueaPassword > 0)
                                    {
                                        //Se actualiza el intento fallido
                                        objUser.AccessUnsuccessful++;
                                        objUser.UpdateUsersAccess(objUser.AccessUnsuccessful, null, null);

                                        //Se verifica si el usuario ha superado los intentos permitidos
                                        if (objEmpresa.IntentosBloqueaPassword <= objUser.AccessUnsuccessful)
                                        {
                                            objUser.Blocking = true;
                                            objUser.UpdateUsersAccess(null, null, objUser.Blocking);//Se bloquea el usuario                        

                                            message = "<script language='javascript'>alert('Usted superó el número de intentos fallidos para acceder, su contraseña ha sido bloqueada, consulte con el administrador del sistema')</script>";
                                        }
                                        else
                                        {
                                            //GAMM.
                                            //message = "<script language='javascript'>alert('Usuario y/o contraseña inválida')</script>";
                                            if (objUser.AccessUnsuccessful == 1) {
                                                message = "<script language='javascript'>alert('Los datos de acceso proporcionados no son correctos, intente nuevamente')</script>";
                                            }else if (objUser.AccessUnsuccessful == 2)
                                            {
                                                message = "<script language='javascript'>alert('Los datos de acceso proporcionados no son correctos, intente nuevamente. Dispone de UN intento más para ingresar los datos correctamente')</script>";
                                            }

                                        }
                                    }
                                }

                                //Fin MAHG 12/07/10

                            }
                            else
                            {
                                 //GAMMM
                                //message = "<script language='javascript'>alert('Usuario y/o contraseña inválida')</script>";
                                message = "<script language='javascript'>alert('Los datos de acceso proporcionados no son correctos, intente nuevamente.')</script>";
                            }
                        }

                        /*Fin 21/01/10 Marco A. Herrera Gabriel*/
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objUser = null;
            }

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

            if (Validaciones.ExistenCaracteresEspeciales(txtUser.Text.Trim()))
            {
                return true;
            }
            //Inicio
            //Autor:Diego Montejano Avila
            //Proyecto:Auditoria 2014
            //Fecha:23/09/2014
            //Observaciones:Se comenta la validación para restringir la escritura de caracteres especiales.
            //if (Validaciones.ExistenCaracteresEspeciales(txtPassword.Text.Trim()))
            //{
            //    return true;
            //}
            //FIN

            return false;
        }

        private void GenerarTicket(Users objUser)
        {
            #region Declaraciones de variables

            FormsAuthenticationTicket authTicket;
            HttpCookie authCookie;
            string strEncrypted;
            Constante objConstante;

            #endregion

            try
            {
                objConstante = new Constante();
                objConstante.GetConstante(Constante.EnumConstantes._TimeOutFormsAuthenticationTicket);

                authTicket = new FormsAuthenticationTicket(1, objUser.Login, DateTime.Now,
                    DateTime.Now.AddMinutes(int.Parse(objConstante.ConValor)), false, objUser.IdUser.ToString());

                strEncrypted = FormsAuthentication.Encrypt(authTicket);
                authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncrypted);

                Response.Cookies.Add(authCookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AUTOR:Diego Montejano Avila
        /// Fecha:2014/09/24
        /// Observaciones:Se clono de ae_password.aspx.cs
        /// Método, registra en el log de usuario la acción y su detalle
        /// </summary>
        /// <param name="p_idAction">Id de la acción</param>
        /// <param name="p_mainId">Id principal sobre el que se realiza la acción</param>
        /// <param name="p_detail">Detalle de la acción</param>		
        public void RegisterLog(Log.EnumActionsLog p_idAction, int p_mainId, string p_detail)
        {
            try
            {
                Log objLog = new Log();
                objLog.IdUser = p_mainId;
                objLog.usuario_id = p_mainId;
                objLog.IP = Request.UserHostAddress;
                objLog.IdActionLog = p_idAction;
                objLog.MainId = p_mainId;
                objLog.Detail = p_detail;
                objLog.InsertLog();
            }
            catch
            {
            }
        }
        #endregion
        /// <summary>
        ///Inicio
        ///Auto:Diego Montejano Avila
        ///Proyecto: Auditoria 2014
        ///Fecha: 2014/09/19
        ///Observaciones: evento de botón para recuperar contraseña.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRecuperarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                trContrasena.Style.Add("display", "none");
                trContrasenaAnterior.Style.Add("display", "none");
                trNuevaContrasena.Style.Add("display", "none");
                trConfirmaNuevaContrasena.Style.Add("display", "none");
                trAceptar.Style.Add("display", "");
                trIngresar.Style.Add("display", "none");
                btnAceptar.Attributes.Add("Resetea", "1");
                txtUser.Text = "";
                trRecuperaContrasena.Style.Add("display", "none");
                txtUser.Focus();
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        ///Inicio
        ///Auto:Diego Montejano Avila
        ///Proyecto: Auditoria 2014
        ///Fecha: 2014/09/19
        ///Observaciones:Evento de botón para cambiar contraseña, esta funcionalidad queda fuera por que el control para cambiar contraseña existe dentro del sistema.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                trContrasena.Style.Add("display", "none");
                trContrasenaAnterior.Style.Add("display", "");
                trNuevaContrasena.Style.Add("display", "");
                trConfirmaNuevaContrasena.Style.Add("display", "");
                trAceptar.Style.Add("display", "");
                trIngresar.Style.Add("display", "none");
                btnAceptar.Attributes.Add("Resetea", "0");
                txtUser.Text = "";
                txtUser.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///Inicio
        ///Auto:Diego Montejano Avila
        ///Proyecto: Auditoria 2014
        ///Fecha: 2014/09/19
        ///Observaciones:Cancela el reseteo de contraseña.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lblCancelar_Click(object sender, EventArgs e)
        {
            trContrasena.Style.Add("display", "");
            trContrasenaAnterior.Style.Add("display", "none");
            trNuevaContrasena.Style.Add("display", "none");
            trConfirmaNuevaContrasena.Style.Add("display", "none");
            trAceptar.Style.Add("display", "none");
            trIngresar.Style.Add("display", "");
            btnAceptar.Attributes.Add("Resetea", "0");
            trRecuperaContrasena.Style.Add("display", "");
            txtUser.Text = "";
            txtUser.Focus();
        }
        /// <summary>
        ///Inicio
        ///Auto:Diego Montejano Avila
        ///Proyecto: Auditoria 2014
        ///Fecha: 2014/09/19
        ///Observaciones:Ejecuta el reseteo de contraseña o el cambio de contraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Users objUser = new Users();
            Security objSecurity = new Security();
            EmpresaDatos objEmpresa = new EmpresaDatos();
            objUser.Login = this.txtUser.Text;
            bool bolPassword = false;
            UsersPasswords validaUltimosPasswords = new UsersPasswords();
            try
            {
                if (!this.ValidaUsuario(this.txtUser.Text))
                    return;

                objUser.GetUsers();
                if (objUser.IdUser == 0)
                {
                    Response.Write("<script language='javascript'>alert('El usuario no es valido');</script>");
                    return;
                }
                validaUltimosPasswords.IdUser = objUser.IdUser;
                if (!string.IsNullOrEmpty(btnAceptar.Attributes["Resetea"]))
                {
                    switch (Convert.ToInt16(btnAceptar.Attributes["Resetea"]))
                    {
                        //Cambio de contraseña
                        case 0:
                            if (objUser.NewPassword)
                            {
                                bolPassword = Security.VerifyHash(this.txtContrasenaAnterior.Text.Trim(), objUser.Password.Trim());
                            }
                            else
                            {
                                bolPassword = (objUser.Password == objSecurity.EncryptString(this.txtContrasenaAnterior.Text.Trim()));
                            }
                            if (!bolPassword)
                            {
                                Response.Write("<script language='javascript'>alert('La contraseña anterior no es valida');</script>");
                                return;
                            }
                            else if (!this.ValidaContrasena(this.txtUser.Text,
                                this.txtContrasenaAnterior.Text, this.txtNuevaContrasena.Text,
                                this.txtConfirmaNuevaContrasena.Text))
                                return;
                            else if (!this.ValidaContrasena(this.txtNuevaContrasena.Text))
                                return;
                            else if (validaUltimosPasswords.ValidaUltimosPasswords(this.txtNuevaContrasena.Text))
                            {
                                Response.Write("<script language='javascript'>alert('La contraseña ya ha sido usada en sus últimos 8 cambios de contraseña');</script>");
                                return;
                            }
                            //Actualizamos el password
                            objUser.Password = Security.SHA256_Encrypt(this.txtNuevaContrasena.Text, null);
                            objUser.NewPassword = true;
                            objUser.ExpiredPassword = false;
                            objUser.UpdateUsers(null, null, null);

                            this.lblCancelar_Click(sender, e);
                            Response.Write("<script language='javascript'>alert('Su contraseña ha sido cambiada con exito');</script>");
                            break;
                        case 1:
                            //Reseteo de contraseña
                            //dentro del sp UpdateUsers se valida que solo se resetee una vez al día la contraseña
                            string NuevaContrasena = "";
                            objUser.ResetPassword = true;
                            objUser.NewPassword = true;
                            objUser.ExpiredPassword = true;
                            //Se genera una contraseña aleatoria
                            NuevaContrasena = AuxiliarSeguridad.GeneraContrasena();
                            objUser.Password = Security.SHA256_Encrypt(NuevaContrasena, null);
                            //No se debe guardar en el historico de contraseñas la contraseña generada
                            objUser.GuardarHistoricoContrasena = false;
                            //Se actuliza el password
                            //La validación para el resteo de solo una vez por día esta en bd
                            objUser.UpdateUsers(null, null, null);
                            //Se registra log

                            this.RegisterLog(Log.EnumActionsLog.Reseteodecontrasena, objUser.IdUser, "Id Usuario:" + objUser.IdUser.ToString() + " Usuario:" + objUser.NameUser);
                            //Se envía el mail siempre 
                            try
                            {
                                Mail objMail = new Mail();
                                //Obtenemos la información para el envío del mail desde la base
                                objMail.TipoMail = Mail.TiposMail.ResetPassword;
                                objMail.CargaParametros();
                                objMail.MailBody = String.Format(objMail.MailBody, NuevaContrasena);
                                objMail.EmailTo = objUser.Email;

                                objMail.SendNow();
                            }

                            catch (Exception ex)
                            {
                                Response.Write("<script language='javascript'>alert('Ocurrió un error al mandar el mail');</script>");
                                return;
                            }
                            this.lblCancelar_Click(sender, e);
                            Response.Write("<script language='javascript'>alert('Su contraseña ha sido resetada con exito, se le enviará un correo con la nueva contraseña');</script>");

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblCancelar_Click(sender, e);
                Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
                return;
            }
        }


        /// <summary>
        /// Marsh - JFEE - 2015/02/09 - Verificar la version del explorador y que este sea un explorador válido
        /// </summary>
        /// <returns>True si es un navegador valido</returns>
        public void ValidarVersionExplorador()
        {
            bool esValido = false;

            //se comenta el codigo para no detecte el navegador
            //if (HttpContext.Current.Request.Browser.Type.ToUpper().Contains("IE"))
            //{
            //    if (HttpContext.Current.Request.Browser.MajorVersion == 8 ||
            //        HttpContext.Current.Request.Browser.MajorVersion == 9)
            //    {
            //        esValido = true;
            //    }
            //}
            //else if (HttpContext.Current.Request.Browser.Type.ToUpper().Contains("FIREFOX"))
            //{
            //    if (HttpContext.Current.Request.Browser.MajorVersion > 10)
            //    {
            //        esValido = true;
            //    }
            //}

            //if (!esValido)
            //{
            //    lblPanelLogin.Text = "Le recordamos que los navegadores compatibles para utilizar el sistema son: </ br></ br><b><ul><li>Internet Explorer 8</li><li>Internet Explorer 9</li><li>FireFox 10 o mayor</li></ul></ br></ br></b>Por favor ingrese utilizando alguno de estos navegadores.".ToString();
            //    tblLogin.Visible = false;
            //}
        }
    }
}
