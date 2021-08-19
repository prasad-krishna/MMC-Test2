using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Medicines.Logic;
using System.Web.Security;
using System.Configuration;
namespace Web
{
    public partial class AEDisclaimer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Inicio
                //Autor:Diego Montejano Avila
                //Proyecto:Auditoria 2014
                //Fecha:23/09/2014
                //Observaciones:Leer la información que será mostrada en el disclaimer de la bd
                if (ConfigurationManager.AppSettings["MostrarDisclaimer"] == null)
                {
                    btnAceptar_Click(sender, e);
                    return;
                }
                else if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MostrarDisclaimer"].ToString()) || ConfigurationManager.AppSettings["MostrarDisclaimer"] != "1")
                {
                    btnAceptar_Click(sender, e);
                    return;
                }
                pnContenidoDisclaimer.Controls.Add(new LiteralControl() { Text = new Dislclaimer().ConsultaDisclaimer() });

                //FIN

            }
            catch
            {


            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["urlRedirectDisclaimer"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["urlRedirectDisclaimer"].ToString()))
                    {
                        Response.Redirect(Session["urlRedirectDisclaimer"].ToString());
                        return;
                    }
                }
                GotoLogin();
            }
            catch
            {
            }
        }
        private void GotoLogin()
        {
            try
            {
                this.Session.Clear();
                this.Session.Abandon();
                FormsAuthentication.SignOut();
            }
            catch
            {

            }
            finally
            {

                Response.Redirect("AE_login_admin.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            GotoLogin();
        }
    }
}
