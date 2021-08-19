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
    ///	Listado de diagnósticos del tipo de servicio para de los formatos
    /// </summary>
    public partial class WC_DiagnosticosFormato : WC_Base
    {

        #region Atributes



        #endregion

        #region Properties

        /// <summary>Propiedad, Id de la solicitud</summary>
        public bool DesplegarDescripcion
        {
            get { return (bool)ViewState["DesplegarDescripcion"]; }
            set { ViewState["DesplegarDescripcion"] = value; }
        }

        #endregion

        #region Inicialización

        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.LoadDiagnositcos(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la carga de los diagnósticos
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        /// <param name="p_idProveedor"></param>
        public void LoadDiagnositcos(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            SolicitudTipoServicioDiagnosticos objDiagnosticos = new SolicitudTipoServicioDiagnosticos();
            objDiagnosticos.IdSolicitud = p_idSolicitud;
            objDiagnosticos.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            this.dtgDiagnosticos.DataSource = objDiagnosticos.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];
            this.dtgDiagnosticos.DataBind();

            if (dtgDiagnosticos.Items.Count == 0)
                this.dtgDiagnosticos.Visible = false;

            if (ViewState["DesplegarDescripcion"] != null && !(bool)ViewState["DesplegarDescripcion"])
                this.dtgDiagnosticos.Columns[1].Visible = false;

            else
                this.dtgDiagnosticos.Columns[1].Visible = true;

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
