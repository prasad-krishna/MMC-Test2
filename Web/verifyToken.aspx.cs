using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Medicines.Logic;
using System.Configuration;
using MFAUtil;
using System.IO;
using System.Text;

namespace Web
{
    public partial class verifyToken : System.Web.UI.Page
    {
        private string message = "";
        public string errflag = "";
        public string usrStat = "";
        private MFA.RSA rsaresponse;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usrStat = Session["userStatus"].ToString();
            }
            catch (NullReferenceException et)
            {
                LogMFAError(et);
            }
            
            if (!IsPostBack)
            {
                string usrEmail = (string)Session["userEmail"];
                string maskedEmail = MaskEmailID(usrEmail);
                lblOtptitle.Text = "Verifique su identidad";
                lblheading.Text = "Marsh protege su cuenta mediante una autenticación adicional. Le hemos enviado a su correo electrónico <span class='headhighlight'>"+maskedEmail+"</span> con un token de seguridad el cual debe ingresar a continuación:";
                if (usrStat.ToUpper() == "LOCKOUT" || usrStat.ToUpper() == "DELETE")
                {
                    lblOtptitle.Text = "Identidad no verificada";
                    lblmessage.Text = "Para su protección, su cuenta ha sido bloqueada debido a múltiples intentos fallidos. Para ayuda en el desbloqueo de su cuenta por favor comuníquese con su ejecutivo de soporte";
                    btnSubmit.Visible = false;
                    Label1.Visible = false;
                    txtToken.Visible = false;
                }
            }
            else
            {
                if (txtToken.Text == "")
                {
                    lblmessage.Text = "";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                lblmessage.Text = "";
                string serverAddr = ConfigurationManager.AppSettings["RSA_AA_URI"];
                string orgName = ConfigurationManager.AppSettings["RSA_AA_ORGNAME"];
                string groups = ConfigurationManager.AppSettings["RSA_AA_GROUPS"];
                string securitykey = ConfigurationManager.AppSettings["RSA_AA_SECURITYKEY"];
                int cookiedays = Convert.ToInt32(ConfigurationManager.AppSettings["RSA_AA_COOKIEDAYS"]);
                int testcookiemin = Convert.ToInt32(ConfigurationManager.AppSettings["RSA_AA_TEST_COOKIE_MINUTES"]);

                MFA mfa = new MFA();
                if (Session["rsaobj"] == null)
                {
                    userlogin();
                }
                else
                {
                    rsaresponse = (MFA.RSA)Session["rsaobj"];
                    MFA.MFAUser user = new MFA.MFAUser();
                    user.userEmail = rsaresponse.userEmail;

                    user.orgName = orgName;
                    user.ipAddress = rsaresponse.ipAddress;
                    user.clientGenCookie = rsaresponse.clientGenCookie;
                    user.groups = groups.Split(',').ToList();
                    user.otp = txtToken.Text;
                    LogMessage("Verifying the OTP...");
                    //mfa 26 sep - start
                    string usercookie = string.Empty;
                    usercookie = user.userEmail;
                    usercookie = usercookie.Replace("@", "");
                    usercookie = usercookie.Replace(".com", "");
                    string devcookieage = "deviceTokenCookieage" + usercookie;
                    string devcookie = "deviceTokenCookie" + usercookie;

                    rsaresponse = mfa.verifyOTP(user, rsaresponse, serverAddr, securitykey);
                    LogMessage("OTP verification completed.. Status code-" + rsaresponse.statusCode);
                    if (rsaresponse.statusCode == null)
                    {
                    }
                    else if (rsaresponse.statusCode.ToUpper() == "SUCCESS")
                    {
                        LogMessage("Writing deviceTokenCookieage & deviceTokenCookie..");

                        if (Request.Cookies[devcookieage] == null)
                        {
                            HttpCookie appCookieage = new HttpCookie(devcookieage);

                            appCookieage.Value = rsaresponse.deviceTokenCookie;
                            if (testcookiemin > 0)
                            {
                                appCookieage.Expires = DateTime.Now.AddMinutes(testcookiemin);
                            }
                            else
                            {
                                appCookieage.Expires = DateTime.Now.AddDays(cookiedays);   ///////
                            }
                            Response.Cookies.Add(appCookieage);
                        }

                        if (Request.Cookies[devcookie] == null)
                        {
                            HttpCookie appCookie = new HttpCookie(devcookie);
                            appCookie.Value = rsaresponse.deviceTokenCookie;
                            appCookie.Expires = DateTime.MaxValue;
                            Response.Cookies.Add(appCookie);
                        }
                        else
                        {
                            HttpCookie appCookie = Request.Cookies[devcookie];
                            appCookie.Value = rsaresponse.deviceTokenCookie;
                            appCookie.Expires = DateTime.MaxValue;
                            Response.Cookies.Add(appCookie);
                        }

                        LogMessage("Writing deviceTokenCookieage & deviceTokenCookie.. completed");

                    }
                    if (rsaresponse.statusCode != null)
                    {
                        if (rsaresponse.statusCode.ToUpper() == "SUCCESS")
                        {
                            userlogin();
                        }
                        else if (rsaresponse.statusCode.ToUpper() == "FAIL")
                        {
                            //Response.Write("<script>alert('FAIL')</script>");
                            if (txtToken.Text == "")
                            {
                                lblmessage.Text = "";
                            }
                            else
                            {
                                lblmessage.Text = "Token de seguridad inválido. Vuelva a intentarlo.";
                                txtToken.Text = "";
                            }
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Your security token does not match. Please login again to get a new token.')", true);

                        }
                    }
                    if (rsaresponse.userStatus != null)
                    {
                        if (rsaresponse.userStatus.ToUpper() == "LOCKOUT")
                        {
                            //Response.Write("<script>alert('LOCKOUT')</script>");
                            lblOtptitle.Text = "Identidad no verificada";
                            lblmessage.Text = "Para su protección, su cuenta ha sido bloqueada debido a múltiples intentos fallidos. Para ayuda en el desbloqueo de su cuenta por favor comuníquese con su ejecutivo de soporte";
                            btnSubmit.Visible = false;
                            Label1.Visible = false;
                            txtToken.Visible = false;

                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Your security token has LOCKEDOUT. Please login again to get a new token.')", true);
                        }
                        else if (rsaresponse.userStatus.ToUpper() == "INVALID/EXPIRED/RESET SESSION ID ERROR")
                        {

                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Su Token de seguridad ha expirado. Ingrese de nuevo al sistema para obtener un nuevo token.');window.location='AE_login_admin.aspx';", true);
                            //Response.Write("<script>alert('Your security token has expired. Please login again to get a new token.')</script>");
                            //Response.Redirect("AE_login_admin.aspx");
                            //lblmessage.Text = "Your security token has expired. Please login again to get a new token.";
                        }
                    }
                }
                btnSubmit.Enabled = true;
            }
            catch (Exception ex)
            {
                LogMFAError(ex);

                if (errflag == "No")
                { }
                else
                {
                    mfaExceptionEmail(ex);
                }
                userlogin();
            }



        }

        private static void mfaExceptionEmail(Exception ex)
        {


            Mail objMail = new Mail();
            string strMailFrom = System.Configuration.ConfigurationManager.AppSettings["RSA_MAIL_FROM"].ToString();
            string strMailTo = System.Configuration.ConfigurationManager.AppSettings["RSA_SUPPORT_MAIL"].ToString();
            objMail.EmailFrom = strMailFrom;
            objMail.MailSubject = "Alert - Historias Clinicas MFA exception occured";
            objMail.MailBody = "The exception details are as : <br/> Ex - " + ex + "<br/>" +
                " Ex message - " + ex.Message + "<br/> " +
                " Ex stacktrace - " + ex.StackTrace + "<br/>" +
                "---------------------------";
            objMail.EmailTo = strMailTo;

            objMail.SendNow();



        }
        private void userlogin()
        {
            try
            {
                if (Session["Company"] != null)
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

                    if (Session["Company"] == null)
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
                        //objError.MessageError = "Ingreso al sistema incorrecto, nombre usuario: " + this.txtUser.Text + " contraseña: " + this.txtPassword.Text;
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
            catch (System.Threading.ThreadAbortException et)
            {
                errflag = "No";
            }
        }

        public string MaskEmailID(string usrEmail)
        {
            //string EmailID = txtemail.Text;
            string domain = string.Empty;
            string username = string.Empty;
            int index = usrEmail.IndexOf('@', 0);
            if (index != -1)
            {
                domain = usrEmail.Substring(index);
                username = usrEmail.Substring(0, index);
            }
            string newstr = string.Empty;
            if (username.Length <= 0)
            {
                newstr = "***";
            }
            else if (username.Length <= 5)
            {

                newstr = username.Substring(0, 1);
                for (int i = 0; i < username.Length - 1; i++)
                {
                    newstr = newstr + "*";
                }
            }
            else
            {
                newstr = username.Substring(0, 5);
                for (int i = 0; i < username.Length - 5; i++)
                {
                    newstr = newstr + "*";
                }
            }
            return newstr + domain;
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


    }
}