using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;
using System.Web.UI;
using TPA;


using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            string SesId = "";
            if (Session.SessionID != null)
            {
                SesId = Session.SessionID.ToString();
            }

            string SesionBorrar = "";
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                SesionBorrar = Request.Cookies[FormsAuthentication.FormsCookieName].Value.ToString();
            }
            
            string UrlReferrerHost = "";
            if (Request.UrlReferrer != null)
            {
                UrlReferrerHost = Request.UrlReferrer.Host.ToString();
            }

            string RUA = "";
            if (Request.Url.AbsoluteUri != null)
            {
                RUA = Request.Url.AbsoluteUri.ToString();
            }

            string RURQ = "";
            string valWizard = "";

            if (Request.Params != null)
            {
                //RURQ = Request.UrlReferrer.Query.ToString();
                //RURQ = Request.QueryString.ToString();

                string val = "";
                string Valores = "";

                foreach (var Key in Request.Params.AllKeys)
                {
                    //val = item.ToString(); WizardEmpresa$fckEditorDetalle

                    //if (Key == "WizardEmpresa$fckEditorDetalle") {
                    if(Key == "HTTP_X_FORWARDED_HOST") { 
                        valWizard = Request.Params[Key].ToString();
                    }

                    val = Key + " = " + Request.Params[Key].ToString();

                    Valores += val + " ";
                }

                RURQ = Valores;

            }

            string Dir = "";
            if (Request.Url.OriginalString != null)
            {
                Dir = Request.Url.OriginalString.ToString();
            }

            string aspx = "";
            if (Request.Url.Segments != null)
            {
                //aspx = Request.Url.Segments[3].ToString();
                aspx = Request.Url.Segments[Request.Url.Segments.GetUpperBound(0)].ToString();

            }

            string RQS = "";
            if (Request.Url.Query != null)
            {
                //RQS = Request.QueryString.ToString();
                RQS = Request.Url.Query.ToString();
            }

            //if (Request.Params["HOST_HEADER"] != null)
            //{
            //    bError = Request.Params["HOST_HEADER"].Equals("X-FORWARDED-HOST", StringComparison.InvariantCultureIgnoreCase) ||
            //        !Request.Params["HOST_HEADER"].Equals(Request.Url.Authority, StringComparison.InvariantCultureIgnoreCase);
            //}

            //if (!bError && Request.Params["HTTP_HOST"] != null)
            //{
            //    bError = !Request.Params["HTTP_HOST"].Equals(Request.Url.Authority, StringComparison.InvariantCultureIgnoreCase);
            //}

            //RQS = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            //RQS = Request.ServerVariables["SERVER_NAME"].ToString();


            bool validaAcceso = false;


            string coneccion = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString.ToString();

            validaAcceso = AuxiliarSeguridad.GrabaLog(coneccion,SesId, SesionBorrar, UrlReferrerHost, RUA, RURQ, Dir, aspx, RQS, valWizard);

            if (validaAcceso == true)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("~/ErrorPage.aspx", true);
                return;
            }

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
         
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}