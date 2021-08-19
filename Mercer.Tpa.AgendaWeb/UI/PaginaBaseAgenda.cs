using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Mercer.Tpa.Agenda.Web.UI
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Página base, requerida para poder redireccionar si se vence la sesión.
    /// El código es similar a PB_PaginaBase con la diferencia de que el Path al login es virtual
    /// </summary>
    public class PaginaBaseAgenda:Page
    {
        /// <summary>
        /// Path de login form, virtual para que pueda ser modificado dependiendo de la ubicación de las formas
        /// </summary>
        protected virtual string PathLoginForm
        {
            get
            {
                return "../../../AE_login_admin.aspx";
            }
        }

        protected virtual void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                    //RAM se cerrará sesion desde HC
                    //if (Session["IdUser"] == null || FormsAuthentication.FormsCookieName == "")
                    //{
                    //    FormsAuthentication.SignOut();//Elimina el vale de autenticación

                    //    //La siguiente redirección funciona gracias al http handler de DevExpress que permite
                    //    //redirecciones desde callbacks
                    //    if(IsCallback)
                    //    {
                    //        this.Response.RedirectLocation = PathLoginForm;
                    //    }
                    //    //Fin 22/01/10 MAHG
                    //    string message = "";
                    //    message = "<script language='javascript'>alert('Su sesión a caducado, ingrese nuevamente');window.parent.location ='"+PathLoginForm+"'</script>";

                    //    //Inicio MAHG Se verifica si la solicitud es Asincrona
                    //    if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                    //    {
                    //        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, false);
                    //    }
                    //    else
                    //    {
                    //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", message);
                    //    }

                    //    Response.End();
                    //    return;
                    //}
                if (!this.Page.IsPostBack)
                {
             
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

                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "error", message);
                }
            }

        }

    }
}
