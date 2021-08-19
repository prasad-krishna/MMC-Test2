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

namespace TPA.interfaz_empleado.formatos
{
    /// <summary>
    /// Formato de autorización para Hopitalización, Cirugía, Procedimientos y otros
    /// </summary>
    public partial class FO_HospitalarioOtros : PB_PaginaBase
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

                this.tblPrincipal.Style.Add("BACKGROUND-POSITION", "center center");
                this.tblPrincipal.Style.Add("BACKGROUND-REPEAT", "no-repeat");
                this.tblPrincipal.Style.Add("BACKGROUND-IMAGE", "url(../../logos/logoFondo_" + Session["Company"] + ".gif)");

                if (!this.Page.IsPostBack)
                {
                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                        this.LoadGridDetail(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                    }

                    if (Request.QueryString["exportar"] != null && Request.QueryString["exportar"] == "S")
                    {
                        Response.ClearContent();
                        Response.ContentType = "application/vnd.ms-excel";
                    }
                    else
                    {
                        string script = "";
                        script = "<script language='javascript'>window.print();</script>";


                        //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "print", script, false);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "print", script);
                        }
                        //Fin      


                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Métodos



        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            //Cargar datos generales solicitud
            Solicitud objSolicitud = new Solicitud();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();
            this.lblObservaciones.Text = objSolicitud.Observaciones;


            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicio.GetSolicitudTipoServicio();

            EmpresaTipoServicios objEmpresaTipoServicio = new EmpresaTipoServicios();
            objEmpresaTipoServicio.IdTipoServicio = objTipoServicio.IdTipoServicio;
            objEmpresaTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaTipoServicio.GetEmpresaTipoServicios();
            this.lblVigencia.Text = objEmpresaTipoServicio.DiasVigencia.ToString();
            this.lblTextoFormato.Text = objEmpresaTipoServicio.TextoFormato;

            this.lblObservaciones.Text = objTipoServicio.Comentarios + " " + this.lblObservaciones.Text;
            this.lblUnidadAprobacion.Text = objTipoServicio.UnidadAprobacion;

            SolicitudTipoServicioProveedores objTipoServicioProveedor = new SolicitudTipoServicioProveedores();
            objTipoServicioProveedor.IdSolicitud = p_idSolicitud;
            objTipoServicioProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicioProveedor.IdProveedor = p_idProveedor;
            objTipoServicioProveedor.GetSolicitudTipoServicioProveedores();
            ViewState["DespliegaUVR"] = objTipoServicioProveedor.DespliegaUVR;

            if (!objTipoServicioProveedor.DespliegaUVR)
            {
                this.dtgDetalle.Columns[2].Visible = false;

            }
        }

        /// <summary>
        /// Método, carga la grilla de detalle y totales
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        /// <param name="p_idProveedor"></param>
        public void LoadGridDetail(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            //Consultar servicios
            SolicitudServicio objServicios = new SolicitudServicio();
            objServicios.IdSolicitud = p_idSolicitud;
            objServicios.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            DataTable dtServicios = objServicios.ConsultSolicitudServicioFormatos().Tables[0];

            //Cargar totales
            decimal valorTotal = 0;

            foreach (DataRow row in dtServicios.Rows)
            {
                if (!Convert.IsDBNull(row["ValorAprobado"]) && row["ValorAprobado"].ToString() != string.Empty && row["ValorAprobado"].ToString() != "0")
                    valorTotal += Convert.ToDecimal(row["ValorAprobado"]);

                else
                    if (!Convert.IsDBNull(row["ValorConvenioSolicitado"]) && row["ValorConvenioSolicitado"].ToString() != string.Empty && row["ValorConvenioSolicitado"].ToString() != "0")
                        valorTotal += Convert.ToDecimal(row["ValorConvenioSolicitado"]);
            }

            if (valorTotal != 0)
            {
                this.lblValorTotal.Text = string.Format("{0:0,0}", valorTotal);
                this.lblTotal.Text = string.Format("{0:0,0}", valorTotal);
            }

            //Completar grila con líneas vacias		
            for (int i = dtServicios.Rows.Count; i < 12; i++)
            {
                DataRow newRow = dtServicios.NewRow();
                dtServicios.Rows.Add(newRow);
            }

            this.dtgDetalle.DataSource = dtServicios;
            this.dtgDetalle.DataBind();
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
            this.Load += new System.EventHandler(this.Page_Load);
            this.dtgDetalle.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgDetalle_ItemDataBound);

        }
        #endregion

        private void dtgDetalle_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");
                Label lblUVRProducto = (Label)e.Item.FindControl("lblUVRProducto");
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                if (Convert.IsDBNull(rowItem["UVR"]) || Convert.ToDecimal(rowItem["UVR"]) == 0)
                {
                    lblUVRProducto.Visible = false;
                }
                else
                {
                    if (ViewState["DespliegaUVR"] != null && Convert.ToBoolean(ViewState["DespliegaUVR"]) == false)
                    {
                        lblUVRProducto.Visible = false;
                        lblValorAprobado.Visible = false;
                        this.lblTotal.Visible = false;
                        this.lblValorTotal.Visible = false;
                    }
                }
            }
        }

        #endregion
    }
}
