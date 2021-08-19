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
    ///	Control para adición de diagnósticos
    /// </summary>
    public partial class WC_AdicionarDiagnostico : WC_Base
    {

        #region Atributes
       

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

            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtDiagnosticoTipoServicio1.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnosticoTipoServicio2.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnosticoTipoServicio3.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin MAHG 12/01/10
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, Realiza la carga inicial de los controles
        /// </summary>
        public void LoadControls()
        {
            this.btnBuscarDiagnosticoTipoServicio1.Attributes.Add("OnClick", "javascript:ShowDiagnosticoTipoServicio(this,'" + this.txtIdDiagnosticoTipoServicio1.ClientID + "','" + this.txtDiagnosticoTipoServicio1.ClientID + "');");
            this.btnBuscarDiagnosticoTipoServicio2.Attributes.Add("OnClick", "javascript:ShowDiagnosticoTipoServicio(this,'" + this.txtIdDiagnosticoTipoServicio2.ClientID + "','" + this.txtDiagnosticoTipoServicio2.ClientID + "');");
            this.btnBuscarDiagnosticoTipoServicio3.Attributes.Add("OnClick", "javascript:ShowDiagnosticoTipoServicio(this,'" + this.txtIdDiagnosticoTipoServicio3.ClientID + "','" + this.txtDiagnosticoTipoServicio3.ClientID + "');");
        }

        /// <summary>
        /// Método, Carga los objetos de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        public void LoadDiagnosticos(SolicitudTipoServicio p_objTipoServicio, short p_idTipoSolicitud)
        {

            if ((this.txtIdDiagnosticoTipoServicio1.Text != string.Empty && this.txtIdDiagnosticoTipoServicio1.Text == this.txtIdDiagnosticoTipoServicio2.Text) || (this.txtIdDiagnosticoTipoServicio2.Text != string.Empty && this.txtIdDiagnosticoTipoServicio2.Text == this.txtIdDiagnosticoTipoServicio3.Text) || (this.txtIdDiagnosticoTipoServicio1.Text != string.Empty && this.txtIdDiagnosticoTipoServicio1.Text == this.txtIdDiagnosticoTipoServicio3.Text))
                throw new Exception("Debe seleccionar diagnosticos diferentes");

            if (Request.QueryString["liquidacion"] != null && Request.QueryString["liquidacion"] != string.Empty && Convert.ToInt64(Request.QueryString["liquidacion"]) == p_objTipoServicio.IdSolicitudTipoServicio && this.txtDiagnosticoTipoServicio1.Text == string.Empty && this.txtDiagnosticoTipoServicio2.Text == string.Empty && this.txtDiagnosticoTipoServicio3.Text == string.Empty)
                throw new Exception("Debe adicionar al menos un diagnóstico");


            if ((Request.QueryString["liquidacion"] == null || Request.QueryString["liquidacion"] == string.Empty) && Convert.ToInt32(Session["Company"]) == 85 && p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion) && this.txtDiagnosticoTipoServicio1.Text == string.Empty && this.txtDiagnosticoTipoServicio2.Text == string.Empty && this.txtDiagnosticoTipoServicio3.Text == string.Empty)
                throw new Exception("Debe adicionar al menos un diagnóstico");


            ArrayList arrDiagnosticos = new ArrayList();

            if (this.txtDiagnosticoTipoServicio1.Text.Trim() != string.Empty)
            {
                SolicitudTipoServicioDiagnosticos objDiagnosticos1 = new SolicitudTipoServicioDiagnosticos();
                objDiagnosticos1.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnosticoTipoServicio1.Text);
                if (this.txtTiempoEvolucion1.Text != string.Empty)
                    objDiagnosticos1.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion1.Text);
                objDiagnosticos1.PeriodoEvolucion = this.ddlTiempoEvolucion1.SelectedValue;
                arrDiagnosticos.Add(objDiagnosticos1);
            }

            if (this.txtDiagnosticoTipoServicio2.Text.Trim() != string.Empty)
            {
                SolicitudTipoServicioDiagnosticos objDiagnosticos2 = new SolicitudTipoServicioDiagnosticos();
                objDiagnosticos2.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnosticoTipoServicio2.Text);
                if (this.txtTiempoEvolucion2.Text != string.Empty)
                    objDiagnosticos2.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion2.Text);
                objDiagnosticos2.PeriodoEvolucion = this.ddlTiempoEvolucion2.SelectedValue;
                arrDiagnosticos.Add(objDiagnosticos2);
            }

            if (this.txtDiagnosticoTipoServicio3.Text.Trim() != string.Empty)
            {
                SolicitudTipoServicioDiagnosticos objDiagnosticos3 = new SolicitudTipoServicioDiagnosticos();
                objDiagnosticos3.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnosticoTipoServicio3.Text);
                if (this.txtTiempoEvolucion3.Text != string.Empty)
                    objDiagnosticos3.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion3.Text);
                objDiagnosticos3.PeriodoEvolucion = this.ddlTiempoEvolucion3.SelectedValue;
                arrDiagnosticos.Add(objDiagnosticos3);
            }

            p_objTipoServicio.SolicitudTipoServicioDiagnosticos = arrDiagnosticos;
        }

        /// <summary>
        /// Método, Carga los controles con los diagnósticos del tipo de servicio
        /// </summary>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlDiagnosticos(long p_idSolicitudTipoServicio)
        {
            DataRow dr;
            SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
            objDiagnostico.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

            if (dtDiagnosticos.Rows.Count > 0)
            {
                dr = dtDiagnosticos.Rows[0];
                this.txtDiagnosticoTipoServicio1.Text = dr["NombreCompleto"].ToString();
                this.txtDiagnosticoTipoServicio1.ToolTip = dr["NombreCompleto"].ToString();
                this.txtIdDiagnosticoTipoServicio1.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion1.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion1.SelectedValue = dr["PeriodoEvolucion"].ToString();
            }

            if (dtDiagnosticos.Rows.Count > 1)
            {
                dr = dtDiagnosticos.Rows[1];
                this.txtDiagnosticoTipoServicio2.Text = dr["NombreCompleto"].ToString();
                this.txtDiagnosticoTipoServicio2.ToolTip = dr["NombreCompleto"].ToString();
                this.txtIdDiagnosticoTipoServicio2.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion2.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion2.SelectedValue = dr["PeriodoEvolucion"].ToString();
            }

            if (dtDiagnosticos.Rows.Count > 2)
            {
                dr = dtDiagnosticos.Rows[2];
                this.txtDiagnosticoTipoServicio3.Text = dr["NombreCompleto"].ToString();
                this.txtDiagnosticoTipoServicio3.ToolTip = dr["NombreCompleto"].ToString();
                this.txtIdDiagnosticoTipoServicio3.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion3.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion3.SelectedValue = dr["PeriodoEvolucion"].ToString();
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
