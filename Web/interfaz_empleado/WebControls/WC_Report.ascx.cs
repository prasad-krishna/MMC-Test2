namespace WebMedicamentos.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Control que permite imprimir y exportar
    /// </summary>
    public partial class WC_Report : WC_Base
    {

        #region Atributos


        #endregion

        #region Propiedades

        /// <summary>Propiedad, Nombre de la página para redirección</summary>
        public string redirectPage
        {
            get { return ViewState["_redirectPage"].ToString(); }
            set { ViewState["_redirectPage"] = value; }
        }

        #endregion

        #region Inicialización

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

        }

        #endregion

        #region Eventos

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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.imbImprimir.Click += new System.Web.UI.ImageClickEventHandler(this.imbImprimir_Click);
            this.lnkImprimir.Click += new System.EventHandler(this.lnkImprimir_Click);
            this.imbExportar.Click += new System.Web.UI.ImageClickEventHandler(this.imbExportar_Click);
            this.lnkExportar.Click += new System.EventHandler(this.lnkExportar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void imbImprimir_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string propiedades = "dependent=yes,directories=no,hotkeys=no,menubar=no,personalbar=no,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,width=800,height=500,left=200,top=250";
            string script = "";
            script = "<script language='javascript'>open('" + this.redirectPage + "','Reporte','" + propiedades + "')</script>";


            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "abrir", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "abrir", script);
            }
            //Fin     


        }

        protected void lnkImprimir_Click(object sender, System.EventArgs e)
        {
            string propiedades = "dependent=yes,directories=no,hotkeys=no,menubar=no,personalbar=no,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,width=800,height=500,left=200,top=250";
            string script = "";
            script = "<script language='javascript'>open('" + this.redirectPage + "','Reporte','" + propiedades + "')</script>";

            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "abrir", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "abrir", script);
            }
            //Fin     

        }



        protected void lnkExportar_Click(object sender, System.EventArgs e)
        {
            string propiedades = "";
            string script = "";
            script = "<script language='javascript'>open('" + this.redirectPage + "&exportar=S','Reporte','" + propiedades + "')</script>";

            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "abrir", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "abrir", script);
            }
            //Fin     

        }

        private void imbExportar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string propiedades = "";
            string script = "";
            script = "<script language='javascript'>open('" + this.redirectPage + "&exportar=S','Reporte','" + propiedades + "')</script>";

            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "abrir", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "abrir", script);
            }
            //Fin     


        }

        #endregion


    }
}
