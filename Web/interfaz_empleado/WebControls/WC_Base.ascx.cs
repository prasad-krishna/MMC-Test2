using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Web.interfaz_empleado.WebControls
{
    /// <summary>
    /// WebControl base 
    /// </summary>   
    /// <remarks>Autor: Adriana Diazgranados
    /// </remarks>

    public partial class WC_Base : System.Web.UI.UserControl
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {            
            if (!this.Page.IsPostBack)
                {             
                    //Inicio 22/01/10 MAHG Marco A. Herrera G.
                    /* Se verifica que el ticket (Cookie) y la sesión no hayan expirado.*/
                
                    if (Session["IdUser"] == null || FormsAuthentication.FormsCookieName == "")
                    {
                        FormsAuthentication.SignOut();//Elimina el vale de autenticación

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

                        Page.Response.End();
                    }
            }
        }
    }
}