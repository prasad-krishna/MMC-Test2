namespace WebMedicamentos.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///		Descripción breve de WC_RegistrarUVR.
    /// </summary>
    public partial class WC_RegistrarUVR : WC_Base
    {

        #region Atributos

        

        #endregion

        #region Inicialización

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                this.txtUVRS.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkCalcular.UniqueID + "',''); return false }");
                this.txtValorUVR.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkCalcular.UniqueID + "',''); return false }");
                this.LoadControls();
            }

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
            this.lnkCalcular.Click += new System.EventHandler(this.lnkCalcular_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void lnkCalcular_Click(object sender, System.EventArgs e)
        {
            if (this.txtUVRS.Text == string.Empty || this.txtUVRS.Text == string.Empty)
            {
                string message = "";
                message = "<script language='javascript'>alert('Debe ingresar las UVRs y el valor de la UVR')</script>";


                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
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
            else
            {
                string script = "<script language='javascript'>DevolverUVR('" + string.Format("{0:0,0}", this.txtUVRS.Text) + "','" + string.Format("{0:0,0}", Convert.ToDecimal((Convert.ToDecimal(this.txtUVRS.Text) * Convert.ToDecimal(this.txtValorUVR.Text)))) + "')</script>";

                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje" + this.txtTemp.Text , script, false);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Mesaje" + this.txtTemp.Text, script);
                }
                //Fin   
            }
        }

        #endregion

        #region Métodos

        public void LoadControls()
        {
            EmpresaDatos objEmpresa = new EmpresaDatos();
            objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresa.GetEmpresaDatos();

            if (Convert.ToInt32(objEmpresa.ValorUVR) != 0)
                this.txtValorUVR.Text = string.Format("{0:0,0}", objEmpresa.ValorUVR);

        }

        #endregion

    }
}
