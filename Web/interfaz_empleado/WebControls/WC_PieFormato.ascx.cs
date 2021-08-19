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
    /// Contiene los datos de pie de págiona de los formatos
    /// </summary>
    public partial class WC_PieFormato : WC_Base
    {

        #region Atributos


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
            LoadStrings();
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                }
            }
        }

        #endregion

        #region Métodos

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            this.lblFechaActual.Text = DateTime.Now.ToShortDateString();

        }

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Oculta las etiquetas que no deben mostrarse para México
        /// </summary>
        private void LoadStrings()
        {
            int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

            if (intPais == 1)
            {
                lblCentroSeguros.Visible = false;
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #endregion
    }
}
