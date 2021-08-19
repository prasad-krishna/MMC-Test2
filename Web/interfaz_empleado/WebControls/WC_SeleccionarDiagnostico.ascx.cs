namespace TPA.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Collections;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Descripción breve de WC_BuscarDiagnosticoTipoServicio.
    /// </summary>
    public partial class WC_SeleccionarDiagnostico : WC_Base
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


            if (!this.IsPostBack)
            {
            }

        }

        #endregion

        #region Métodos


        /// <summary>
        /// Método, carga los diagnósticos de la consulta
        /// </summary>
        public void LoadControls()
        {
            if (Request.QueryString["IdConsulta"] != null && Request.QueryString["IdConsulta"] != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos = new ConsultaDiagnosticos();
                objDiagnosticos.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
                this.chkDiagnosticos.DataTextField = "NombreCompleto";
                this.chkDiagnosticos.DataValueField = "IdDiagnostico";
                this.chkDiagnosticos.DataSource = objDiagnosticos.ConsultConsultaDiagnosticos();
                this.chkDiagnosticos.DataBind();

                foreach (ListItem item in chkDiagnosticos.Items)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// Método, selecciona los diagnosticos de la incapacidad
        /// </summary>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlDiagnosticosIncapacidad(long p_idIncapacidad)
        {
            IncapacidadDiagnosticos objDiagnosticos = new IncapacidadDiagnosticos();
            objDiagnosticos.IdIncapacidad = p_idIncapacidad;
            DataTable dtDiagnosticos = objDiagnosticos.ConsultIncapacidadDiagnosticos().Tables[0];

            foreach (ListItem item in chkDiagnosticos.Items)
            {
                item.Selected = false;
            }

            foreach (DataRow row in dtDiagnosticos.Rows)
            {
                ListItem item = this.chkDiagnosticos.Items.FindByValue(row["IdDiagnostico"].ToString());
                if (item != null)
                    item.Selected = true;
                else
                    item.Selected = false;

            }
        }

        /// <summary>
        /// Método, selecciona los diagnosticos del tipo de servicio
        /// </summary>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlDiagnosticos(long p_idSolicitudTipoServicio)
        {
            SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
            objDiagnostico.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

            foreach (ListItem item in chkDiagnosticos.Items)
            {
                item.Selected = false;
            }


            foreach (DataRow row in dtDiagnosticos.Rows)
            {
                ListItem item = this.chkDiagnosticos.Items.FindByValue(row["IdDiagnostico"].ToString());
                if (item != null)
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }


        /// <summary>
        /// Método, carga los diagnósticos
        /// </summary>
        /// <param name="p_objSolicitudTipoServicio"></param>
        public void LoadDiagnosticos(SolicitudTipoServicio p_objSolicitudTipoServicio)
        {
            ArrayList arrDiagnosticos = new ArrayList();
            bool seleccionado = false;

            foreach (ListItem item in this.chkDiagnosticos.Items)
            {
                if (item.Selected)
                {
                    SolicitudTipoServicioDiagnosticos objDiagnosticos = new SolicitudTipoServicioDiagnosticos();
                    objDiagnosticos.IdDiagnostico = Convert.ToInt32(item.Value);
                    string strDiagnostico = item.Text;
                    string[] arrNombreDiagnosticos = strDiagnostico.Split('|');
                    if (arrNombreDiagnosticos[2] != string.Empty)
                        objDiagnosticos.TiempoEvolucion = Convert.ToDecimal(arrNombreDiagnosticos[2]);
                    objDiagnosticos.PeriodoEvolucion = arrNombreDiagnosticos[3];
                    arrDiagnosticos.Add(objDiagnosticos);
                    seleccionado = true;
                }
            }
            if (!seleccionado && this.chkDiagnosticos.Items.Count > 0)
                throw new Exception("Debe adicionar al menos un diagnóstico");

            p_objSolicitudTipoServicio.SolicitudTipoServicioDiagnosticos = arrDiagnosticos;
        }


        /// <summary>
        /// Método, carga los diagnósticos de la incapacidad
        /// </summary>
        /// <param name="p_objSolicitudTipoServicio"></param>
        public void LoadDiagnosticosIncapacidad(Incapacidad p_objIncapacidad)
        {
            ArrayList arrDiagnosticos = new ArrayList();
            bool seleccionado = false;

            foreach (ListItem item in this.chkDiagnosticos.Items)
            {
                if (item.Selected)
                {
                    IncapacidadDiagnosticos objDiagnosticos = new IncapacidadDiagnosticos();
                    objDiagnosticos.IdDiagnostico = Convert.ToInt32(item.Value);
                    string strDiagnostico = item.Text;
                    string[] arrNombreDiagnosticos = strDiagnostico.Split('|');
                    if (arrNombreDiagnosticos[2] != string.Empty)
                        objDiagnosticos.TiempoEvolucion = Convert.ToDecimal(arrNombreDiagnosticos[2]);
                    objDiagnosticos.PeriodoEvolucion = arrNombreDiagnosticos[3];
                    arrDiagnosticos.Add(objDiagnosticos);
                    seleccionado = true;
                }
            }
            if (!seleccionado && this.chkDiagnosticos.Items.Count > 0)
                throw new Exception("Debe adicionar al menos un diagnóstico");

            p_objIncapacidad.IncapacidadDiagnosticos = arrDiagnosticos;
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
