namespace TPA.interfaz_empleado.WebControls
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
    ///	Carga encabezado de cartas
    /// </summary>
    public partial class WC_EncabezadoMercer : WC_Base
    {

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                if (Request.QueryString["IdSolicitudReporte"] != null)
                {
                    this.LoadFormDatos(Convert.ToInt64(Request.QueryString["IdSolicitudReporte"]));
                }

            }
        }

        #region Métodos

        public void LoadFormDatos(long p_idSolicitudReporte)
        {
            EmpresaDatos objEmpresa = new EmpresaDatos();
            objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresa.GetEmpresaDatos();

            this.lblEncabezado.Text = objEmpresa.EncabezadoCarta;

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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #endregion
    }
}
