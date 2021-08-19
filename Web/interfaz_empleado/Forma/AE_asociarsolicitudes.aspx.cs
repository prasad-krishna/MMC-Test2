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
    /// Realiza la asociación de solicitudes en un solo consecutivo para agrupar en un reporte
    /// </summary>
    public partial class AE_asociarsolicitudes : PB_PaginaBase
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
        /// Método, Realiza la carga inicial de controles
        /// </summary>
        public void LoadControls()
        {
            //Se agrega el atributo readonly PETF 14/01/10
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            this.lblMensaje.Text = "";
            this.WC_Report1.Visible = false;
            this.FillList("Proveedores", "Proveedor", Convert.ToInt32(Session["Company"]), this.ddlProveedorBus, "--Prestadores--");

            this.ddlAno.Items.Add(new ListItem("--Año--", "0"));
            for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 2; i++)
            {
                this.ddlAno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
        /// <summary>
        /// Método, Realiza la búsqueda de solicitudes
        /// </summary>
        public void FindSolicitudes()
        {
            ViewState["BusquedaEspecial"] = true;
            object dateFrom = null;
            object dateUntil = null;
            int idProveedor = 0;

            Solicitud objSolicitud = new Solicitud();

            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.txtNoSolicitud.Text != string.Empty)
                objSolicitud.ConsecutivoNombre = this.txtNoSolicitud.Text;
            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
            idProveedor = Convert.ToInt32(this.ddlProveedorBus.SelectedValue);
            if (this.ddlMes.SelectedValue != "0")
                objSolicitud.MesLiquidacion = Convert.ToInt16(this.ddlMes.SelectedValue);
            if (this.ddlAno.SelectedValue != "0")
                objSolicitud.AnoLiquidacion = Convert.ToInt16(this.ddlAno.SelectedValue);

            this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudBusqueda(dateFrom, dateUntil, idProveedor, 0, 0);
            this.dtgSolicitudes.DataBind();
        }

        /// <summary>
        /// Método, Realiza la búsqueda de solicitudes
        /// </summary>
        public void FindSolicitudesReporte()
        {
            SolicitudReportes objReporte = new SolicitudReportes();
            objReporte.ConsecutivoNombre = this.txtNoReporte.Text;
            objReporte.Empresa_id = Convert.ToInt32(Session["Company"]);


            this.dtgSolicitudes.DataSource = objReporte.ConsultSolicitudesSolicitudReporte();
            this.dtgSolicitudes.DataBind();

            objReporte.GetSolicitudReportes();
            ViewState["IdSolicitudReporte"] = objReporte.IdSolicitudReporte;

            foreach (DataGridItem item in this.dtgSolicitudes.Items)
            {
                CheckBox chkAsociar = (CheckBox)item.FindControl("chkAsociar");
                chkAsociar.Checked = true;
            }

            this.lblMensaje.Text = "El reporte " + objReporte.ConsecutivoNombre + " asocia las siguientes solicitudes";
            this.WC_Report1.Visible = true;
            WC_Report1.redirectPage = "../Formatos/FO_CartaAsociacion.aspx?IdSolicitudReporte=" + objReporte.IdSolicitudReporte.ToString();


        }

        /// <summary>
        /// Método, asocia las solicitudes seleccionadas en un solo consecutivo
        /// </summary>
        public void AsociarSolicitudes()
        {
            long idSolicitudReporte;
            SolicitudReportes objSolicitudReporte = new SolicitudReportes();
            Solicitud objSolicitud = new Solicitud();
            objSolicitudReporte.Solicitudes = new ArrayList();

            if (this.txtNoReporte.Visible == true && this.btnAsociar.Text == "Asociar")
            {
                objSolicitudReporte.ConsecutivoNombre = this.txtNoReporte.Text;
                objSolicitudReporte.GetSolicitudReportes();
            }

            objSolicitudReporte.UsuarioCreacion = Convert.ToInt32(Session["IdUser"]);
            objSolicitudReporte.Empresa_id = Convert.ToInt32(Session["Company"]);
            foreach (DataGridItem item in this.dtgSolicitudes.Items)
            {
                objSolicitud = new Solicitud();
                CheckBox chkAsociar = (CheckBox)item.FindControl("chkAsociar");

                if (chkAsociar.Checked)
                {
                    objSolicitud.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                    objSolicitudReporte.Solicitudes.Add(objSolicitud);
                }
            }

            if (this.btnAsociar.Text == "Modificar")
            {
                objSolicitudReporte.IdSolicitudReporte = Convert.ToInt64(ViewState["IdSolicitudReporte"]);
                idSolicitudReporte = objSolicitudReporte.InsertSolicitudReportes(true, false);
            }
            else
            {
                if (this.txtNoReporte.Visible == true)
                {
                    objSolicitudReporte.IdSolicitudReporte = Convert.ToInt64(ViewState["IdSolicitudReporte"]);
                    idSolicitudReporte = objSolicitudReporte.InsertSolicitudReportes(false, false);
                }
                else
                {
                    idSolicitudReporte = objSolicitudReporte.InsertSolicitudReportes(false, true);
                }
            }
            this.lblMensaje.Text = "Se ha creado el reporte " + objSolicitudReporte.ConsecutivoNombre;
            this.WC_Report1.Visible = true;
            WC_Report1.redirectPage = "../Formatos/FO_CartaAsociacion.aspx?IdSolicitudReporte=" + idSolicitudReporte.ToString();

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
            this.rdlOpcionAsociacion.SelectedIndexChanged += new System.EventHandler(this.rdlOpcionAsociacion_SelectedIndexChanged);
            this.btnBuscarReporte.Click += new System.EventHandler(this.btnBuscarReporte_Click);
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtgSolicitudes.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitudes_PageIndexChanged);
            this.dtgSolicitudes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgSolicitudes_ItemDataBound);
            this.btnAsociar.Click += new System.EventHandler(this.btnAsociar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza la búsqueda de la solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.lblMensaje.Text = "";
                this.WC_Report1.Visible = false;
                this.btnAsociar.Text = "Asociar";
                this.FindSolicitudes();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, realiza la paginación en la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgSolicitudes.CurrentPageIndex = e.NewPageIndex;
            this.FindSolicitudes();
        }

        /// <summary>
        /// Evento, asocia varias solicitudes en un solo consecutivo para reportes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAsociar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.AsociarSolicitudes();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, despliega o no controles de no de solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdlOpcionAsociacion_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.tblBuscar.Style["display"] = "";
            this.btnAsociar.Text = "Asociar";
            this.dtgSolicitudes.DataSource = new DataTable();
            this.dtgSolicitudes.DataBind();

            if (this.rdlOpcionAsociacion.SelectedValue == "1")
            {
                this.txtNoReporte.Visible = false;
                this.lblNoReporte.Visible = false;
                this.rfvNoReporte.Enabled = false;
                this.btnBuscarReporte.Visible = false;

            }
            else
            {
                this.txtNoReporte.Visible = true;
                this.lblNoReporte.Visible = true;
                this.rfvNoReporte.Enabled = true;
                this.btnBuscarReporte.Visible = true;
            }
        }

        protected void btnBuscarReporte_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.btnAsociar.Text = "Modificar";
                this.lblMensaje.Text = "";
                this.WC_Report1.Visible = false;
                this.FindSolicitudesReporte();

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Evento, realiza la carga de datos en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                    Label lblEmpleado = (Label)e.Item.FindControl("lblEmpleado");
                    HyperLink hplName = (HyperLink)e.Item.FindControl("hplName");

                    SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
                    objTipoServicio.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                    DataTable dtTipoServicio = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];

                    objEmpleado.Id_empleado = Convert.ToInt32(e.Item.Cells[1].Text);
                    objEmpleado.GetSIC_EMPLEADO();
                    lblEmpleado.Text = objEmpleado.Nombre_completo;

                    foreach (DataRow row in dtTipoServicio.Rows)
                    {
                        if (row["ConsecutivoNombre"].ToString().EndsWith("-"))
                        {

                            hplName.Text += row["ConsecutivoNombre"].ToString().TrimEnd('-') + " ";
                        }

                        else
                            hplName.Text += row["ConsecutivoNombre"].ToString() + " ";
                    }

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
