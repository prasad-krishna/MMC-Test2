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

namespace TPA.interfaz_empleado.formatos
{
    /// <summary>
    /// Formato con texto legal para autorización de registro de Historia Clínica
    /// </summary>
    public partial class FO_AutorizacionHistoriaClinica : PB_PaginaBase
    {


        #region Inicialización

        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
           
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10

                if (!this.Page.IsPostBack)
                {
                    this.lblFecha.Text = DateTime.Now.ToShortDateString();
                    string script = "";
                    script = "<script language='javascript'>window.print();</script>";


                    //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                    if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "print", script, false);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "print", script);
                    }
                    //Fin      

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            //base.OnInit(e);
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
    }
}
