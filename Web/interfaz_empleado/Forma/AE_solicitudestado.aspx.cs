using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_empleado.forma
{

    /// <summary>
    /// Realiza el cambio de estado de una solicitud
    /// </summary>
    public partial class AE_solicitudestado : PB_PaginaBase
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
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10

                if (!this.Page.IsPostBack)
                {
                    this.LoadControls();

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la carga inicial de controles
        /// </summary>
        public void LoadControls()
        {
            if (Request.QueryString["IdEstado"] == "5")
            {
                this.lblEstado.Text = "Anulado";
                this.ddlMotivosAnulacion.Visible = true;
                this.FillList("SolicitudMotivosAnulacion", "SolicitudMotivoAnulacion", this.ddlMotivosAnulacion, "--Motivo Anulación--");
                this.lblAnulacion.Visible = true;
                this.btnEstado.Text = "Anular";
            }
            if (Request.QueryString["IdEstado"] == "2")
            {
                this.lblEstado.Text = "Negado";
                this.btnEstado.Text = "Negar";
            }

            this.FillList(this.ddlMotivos, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(Request.QueryString["IdEstado"]));

            if (this.ddlMotivos.Items.Count > 1)
            {
                this.lblMotivo.Visible = true;
                this.ddlMotivos.Visible = true;

            }
            else
            {
                this.lblMotivo.Visible = false;
                this.ddlMotivos.Visible = false;
                this.ddlMotivos.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// Método, Modifica la solicitud
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void UpdateEstadoSolicitud(short p_idEstado, short p_idMotivo)
        {
            Solicitud objSolicitud = new Solicitud();
            this.LoadObjectSolicitudEstado(objSolicitud, p_idEstado, p_idMotivo);
            objSolicitud.UpdateSolicitudEstado();
        }


        /// <summary>
        /// Método, Carga el objeto solicitud para modificación estado
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadObjectSolicitudEstado(Solicitud objSolicitud, short p_idEstado, short p_idMotivo)
        {
            objSolicitud.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objSolicitud.GetSolicitud();
            objSolicitud.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text.Trim();
            objSolicitud.IdSolicitudEstado = p_idEstado;

            this.LoadSolicitudTipoServiciosEstado(objSolicitud, p_idEstado, p_idMotivo);
        }

        /// <summary>
        /// Método, Carga los objetos de tipos de servicios para modificación estado
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadSolicitudTipoServiciosEstado(Solicitud objSolicitud, short p_idEstado, short p_idMotivo)
        {
            if (Request.QueryString["IdSolicitudTipoServicio"] == null || Request.QueryString["IdSolicitudTipoServicio"] == string.Empty)
            {
                SolicitudTipoServicio objTipoServicio;
                SolicitudTipoServicio objTipoServicioCarga;
                ArrayList arrTiposServicio = new ArrayList();

                objTipoServicio = new SolicitudTipoServicio();
                objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                DataTable dtTiposServicios = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];


                foreach (DataRow item in dtTiposServicios.Rows)
                {
                    if (Convert.ToInt16(item["IdSolicitudEstado"]) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) && Convert.ToInt16(item["IdSolicitudEstado"]) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                    {
                        objTipoServicioCarga = new SolicitudTipoServicio();
                        objTipoServicioCarga.IdSolicitudTipoServicio = Convert.ToInt64(item["IdSolicitudTipoServicio"]);
                        objTipoServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                        objTipoServicioCarga.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text;


                        this.LoadSolicitudServiciosEstado(objTipoServicioCarga, objTipoServicioCarga.IdSolicitudTipoServicio, p_idEstado, p_idMotivo);

                        arrTiposServicio.Add(objTipoServicioCarga);
                    }
                }
                objSolicitud.SolicitudTipoServicios = arrTiposServicio;
            }
            else
            {
                SolicitudTipoServicio objTipoServicioCarga;
                SolicitudTipoServicio objTipoServicio;
                ArrayList arrTiposServicio = new ArrayList();
                objTipoServicio = new SolicitudTipoServicio();

                objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]);
                objTipoServicio.GetSolicitudTipoServicio();

                if (objTipoServicio.IdSolicitudEstado != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) && objTipoServicio.IdSolicitudEstado != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                {
                    objTipoServicioCarga = new SolicitudTipoServicio();
                    objTipoServicioCarga.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]);
                    objTipoServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                    objTipoServicioCarga.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text;

                    this.LoadSolicitudServiciosEstado(objTipoServicioCarga, objTipoServicioCarga.IdSolicitudTipoServicio, p_idEstado, p_idMotivo);
                    arrTiposServicio.Add(objTipoServicioCarga);
                }
                objSolicitud.SolicitudTipoServicios = arrTiposServicio;
            }
        }

        /// <summary>
        /// Método, Carga los objetos de servicios para modificación estado
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public void LoadSolicitudServiciosEstado(SolicitudTipoServicio p_objTipoServicio, long p_idSolicitudTipoServicio, short p_idEstado, short p_idMotivo)
        {
            SolicitudServicio objServicio;
            SolicitudServicio objServicioCarga;
            ArrayList arrServicios = new ArrayList();

            objServicio = new SolicitudServicio();
            objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            DataTable dtServicios = objServicio.ConsultSolicitudServicio().Tables[0];

            foreach (DataRow item in dtServicios.Rows)
            {
                objServicioCarga = new SolicitudServicio();
                objServicioCarga.IdSolicitudServicio = Convert.ToInt64(item["IdSolicitudServicio"]);
                objServicioCarga.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
                objServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                objServicioCarga.IdSolicitudEstado = p_idEstado;
                objServicioCarga.IdSolicitudMotivo = p_idMotivo;

                arrServicios.Add(objServicioCarga);
            }
            p_objTipoServicio.SolicitudServicios = arrServicios;
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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void btnEstado_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (Request.QueryString["IdSolicitud"] != null)
                {
                    if (Request.QueryString["IdEstado"] == "5" && this.ddlMotivosAnulacion.SelectedValue == "0")
                        throw new Exception("Debe ingresar las Observaciones de Anulación");

                    short idEstado = Convert.ToInt16(Request.QueryString["IdEstado"]);
                    short idMotivo = Convert.ToInt16(this.ddlMotivos.SelectedValue);
                    this.UpdateEstadoSolicitud(idEstado, idMotivo);
                    this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación estado solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " A Estado idEstado");
                    Response.Write("<script>alert('La solicitud fue anulada exitosamente'); top.close();</script>");

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

    }
}


