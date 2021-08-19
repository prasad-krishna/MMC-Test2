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
using Mercer.Medicines.Logic;

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Liquida un tipo de servicio de la solicitud
    /// </summary>
    public partial class AE_liquidardetalleautorizacion : PB_PaginaBase
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

        public void LiquidarTipoServicioSolicitud(long idSolicitud, long idSolicitudTipoServicio)
        {
            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objSolicitudTipoServicio = new SolicitudTipoServicio();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objSolicitud.IdSolicitud = idSolicitud;
            objSolicitud.GetSolicitud();
            objSolicitudTipoServicio.Glosa = this.chkGlosa.Checked;
            objSolicitudTipoServicio.IdSolicitudEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);
            //Cargar usuario de la liquidación						
            if (Session["SICAU"] != null)
                objSolicitudTipoServicio.Usuario_idLiquidacion = Convert.ToInt32(Session["IdUser"]);
            else
                objSolicitudTipoServicio.IdUserLiquidacion = Convert.ToInt32(Session["IdUser"]);

            //Cargar el tipo de servicio

            objTipoServicio.IdSolicitud = idSolicitud;

            //Si se va a liquidar se carga el id de la solicitud de SICAU para cierre
            if (ViewState["Id_solicitud_SICAU"] != null && ViewState["Id_solicitud_SICAU"].ToString() != null)
                objSolicitud.Id_solicitud_SICAU = Convert.ToInt32(ViewState["Id_solicitud_SICAU"]);
            objTipoServicio.IdSolicitudTipoServicio = idSolicitudTipoServicio;
            objTipoServicio.IdProveedor = Convert.ToInt32(this.ddlProveedor.SelectedValue);
            objTipoServicio.NumeroFactura = this.txtNumFactura.Text;
            objTipoServicio.FechaFactura = Convert.ToDateTime(this.txtFechaFactura.Text);
            objTipoServicio.FechaRadicacionFactura = Convert.ToDateTime(this.txtFechaRadicacion.Text);
            objTipoServicio.ValorFactura = Convert.ToDecimal(this.txtValorFactura.Text);
            objTipoServicio.NumeroCuentaCobro = this.txtCuentaCobro.Text;
            objTipoServicio.FechaConfirmacion = Convert.ToDateTime(this.txtFechaConfirmacion.Text);

            //Cargar el servicio
            SolicitudServicio objSolicitudServicio = new SolicitudServicio();
            objSolicitudServicio.IdSolicitudEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);
            objSolicitudServicio.IdSolicitudMotivo = Convert.ToInt16(Solicitud.EnumMotivosEstadoSolicitud.Liquidado);
            objSolicitudServicio.IdSolicitud = idSolicitud;
            objSolicitudServicio.IdSolicitudTipoServicio = idSolicitudTipoServicio;
            objTipoServicio.SolicitudServicios = new ArrayList();
            objTipoServicio.SolicitudServicios.Add(objSolicitudServicio);

            objTipoServicio.UpdateSolicitudTipoServicioEstado(objSolicitud);
            this.RegisterLog(Log.EnumActionsLog.ModificarEstadoTipoServicioSolicitud, Convert.ToInt64(idSolicitudTipoServicio), "Liquidación tipo servicio solicitud de solicitud " + idSolicitud);



        }

        /// <summary>
        /// Método, Realiza la carga inicial de controles
        /// </summary>
        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly                         
            txtFechaFactura.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaRadicacion.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaConfirmacion.Attributes.Add("ReadOnly", "ReadOnly");            
            //Fin PETF 14/01/10

            this.FillList(this.ddlProveedor, "ProveedoresEmpresaTipoServicio", "--Proveedor--", Convert.ToInt16(Request.QueryString["IdTipoServicio"]), Convert.ToInt32(Session["Company"]));

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
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLiquidar.Click += new System.EventHandler(this.btnLiquidar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void btnLiquidar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.LiquidarTipoServicioSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]));
                    Response.Write("<script>opener.location.reload(true); top.close();</script>");


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
