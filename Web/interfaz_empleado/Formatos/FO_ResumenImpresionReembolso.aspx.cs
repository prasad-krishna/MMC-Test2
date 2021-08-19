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
    /// Formato resumen para impresión
    /// </summary>
    public partial class FO_ResumenImpresionReembolso : PB_PaginaBase
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
            //Inicio MAHG 22/01/10
            //Se carga el load de la página base

            base.Page_Load(sender, e);

            //Fin MAHG 22/01/10

            if (!this.Page.IsPostBack)
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                    this.LoadLogo();
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

        #endregion

        #region Métodos

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            DataTable dtBeneficiarios;
            Solicitud objSolicitud = new Solicitud();
            EmpresaTipoServicios objEmpresaTipoServicio = new EmpresaTipoServicios();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            GeneralTable objGeneral = new GeneralTable();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();

            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();

            if (objSolicitud.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objSolicitud.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                this.lblExpedidaPor.Text = dtUsuario.Rows[0]["nombre"].ToString();
                this.lblExpedidaPor.Text = objSolicitud.NameUser;
                objGeneral.Id = objSolicitud.Usuario_idCreacion;

            }
            else
            {
                this.lblExpedidaPor.Text = objSolicitud.NameUser;
                objGeneral.Id = objSolicitud.IdUserCreacion;

            }

            objGeneral.TableName = "Ciudades";
            objGeneral.ColumnName = "Ciudad";
            objGeneral.GetGeneralTable();
            this.lblExpedidaPor.Text += "<br/>" + objGeneral.Nombre;

            objEmpleado.Id_empleado = objSolicitud.Id_empleado;
            objEmpleado.GetSIC_EMPLEADO();
            this.lblNombreEmpleado.Text = objEmpleado.Nombre_completo;
            this.lblTipoIdentificacionEmpleado.Text = objEmpleado.Tipo_documento;
            this.lblTipoIdentificacionEmpleado.Text = objEmpleado.Identificacion;

            if (objSolicitud.Beneficiario_id != 0)
            {
                objBeneficiario.Opcion = 2;
                objBeneficiario.Beneficiario_id = objSolicitud.Beneficiario_id;
                dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                this.lblNombrePaciente.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                this.lblTipoIdentificacion.Text = dtBeneficiarios.Rows[0]["Tipo_doc"].ToString();
                this.lblNumero.Text = dtBeneficiarios.Rows[0]["identificacion"].ToString();
            }
            else
            {
                this.lblNombrePaciente.Text = objEmpleado.Nombre_completo;
                this.lblTipoIdentificacion.Text = objEmpleado.Tipo_documento;
                this.lblNumero.Text = objEmpleado.Identificacion;
            }

            this.lblFecha.Text = objSolicitud.FechaCreacion.ToShortDateString();
            this.txtNoSolicitud.Text = objSolicitud.ConsecutivoNombre;
            this.lblObservaciones.Text = objSolicitud.Observaciones;
            this.lblMesLiquidacion.Text = objSolicitud.MesLiquidacion.ToString() + " " + objSolicitud.AnoLiquidacion.ToString();

            objGeneral.TableName = "FormasPago";
            objGeneral.ColumnName = "FormaPago";
            objGeneral.GetGeneralTable();
            this.lblFormaPago.Text = objGeneral.Nombre;

            objGeneral.TableName = "SolicitudEstados";
            objGeneral.ColumnName = "SolicitudEstado";
            objGeneral.GetGeneralTable();
            this.lblEstado.Text = objGeneral.Nombre;

            //Cargar los tipos de servicio
            objTipoServicio.IdSolicitud = p_idSolicitud;

            DataTable dtTipoServicio = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];
            this.dtlTipoServicio.DataSource = dtTipoServicio;
            this.dtlTipoServicio.DataBind();

            //Cargar los cupos
            PresupuestosIndividuo objPresupuesto = new PresupuestosIndividuo();
            objPresupuesto.IdPresupuestoIndividuo = objSolicitud.IdPresupuestoIndividuo;
            objPresupuesto.GetPresupuestosIndividuo();
            this.lblCupo.Text = string.Format("{0:0,0}", objPresupuesto.Presupuesto);
            this.lblDisponible.Text = string.Format("{0:0,0}", objPresupuesto.Presupuesto - objPresupuesto.Utilizado);




        }

        /// <summary>
        /// Evento, realiza la carga del logotipo
        /// </summary>
        public void LoadLogo()
        {
            this.imgLogo.ImageUrl = "../../logos/logo_" + Session["Company"] + ".gif";
        }

        /// <summary>
        /// Carga el listado de diagnósticos en el label
        /// </summary>
        /// <param name="p_lblIdDiagnosticos"></param>
        /// <param name="p_lblDiagnostico"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelDiagnosticos(Label p_lblDiagnostico, long p_idSolicitudTipoServicio)
        {
            SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
            objDiagnostico.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

            foreach (DataRow dr in dtDiagnosticos.Rows)
            {
                int lenght = dr["NombreDiagnostico"].ToString().Length;

                if (lenght > 25)
                    lenght = 25;

                p_lblDiagnostico.Text += dr["NombreDiagnostico"].ToString().Substring(0, lenght) + "... -" + dr["TiempoEvolucion"].ToString() + " " + dr["PeriodoEvolucion"].ToString() + "</br>";

            }
        }


        /// <summary>
        /// Carga el listado de proveedores
        /// </summary>
        /// <param name="p_lblIdProveedores"></param>
        /// <param name="p_lblProveedores"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelProveedores(Label p_lblProveedores, long p_idSolicitudTipoServicio)
        {
            SolicitudTipoServicioProveedores objProveedor = new SolicitudTipoServicioProveedores();
            objProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtProveedores = objProveedor.ConsultSolicitudTipoServicioProveedores().Tables[0];

            foreach (DataRow dr in dtProveedores.Rows)
            {
                p_lblProveedores.Text += "-" + dr["NombreProveedor"].ToString() + "</br>";

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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.dtlTipoServicio.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlTipoServicio_ItemDataBound);

        }
        #endregion

        private void dtlTipoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Cargar los datos si viene el id de la solicitud
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                    Label lblProveedor = (Label)e.Item.FindControl("lblProveedor");
                    Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");

                    //Cargar diagnosticos y prestadores
                    LoadLabelDiagnosticos(lblDiagnosticos, ((long)rowItem["IdSolicitudTipoServicio"]));
                    LoadLabelProveedores(lblProveedor, ((long)rowItem["IdSolicitudTipoServicio"]));

                    //Cargar los servicio
                    DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                    SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                    objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                    objSolicitudServicio.IdSolicitudTipoServicio = ((long)rowItem["IdSolicitudTipoServicio"]);
                    dtgProductoServicio.DataSource = objSolicitudServicio.ConsultSolicitudServicioFormatos();
                    dtgProductoServicio.DataBind();

                }
            }
        }


        public void dtgProductoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DataGrid dtgProductoServicio = (DataGrid)sender;
                Label lblIdTipoServicio = (Label)dtgProductoServicio.Parent.FindControl("lblIdTipoServicio");

                if (lblIdTipoServicio.Text != "0")
                {
                    TipoServicios objTipoServicio = new TipoServicios();
                    objTipoServicio.IdTipoServicio = Convert.ToInt32(lblIdTipoServicio.Text);
                    objTipoServicio.GetTipoServicios();

                    e.Item.Cells[0].Text = objTipoServicio.EtiquetaProductoServicio;
                    e.Item.Cells[1].Text = objTipoServicio.EtiquetaCantidad;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (Request.QueryString["IdSolicitud"] != null)
                {
                    DataGrid dtgProductoServicio = (DataGrid)sender;
                    Label lblTotalSolicitado = (Label)dtgProductoServicio.Parent.FindControl("lblTotalSolicitado");
                    Label lblTotalAprobado = (Label)dtgProductoServicio.Parent.FindControl("lblTotalAprobado");


                    Label lblDosis = (Label)e.Item.FindControl("lblDosis");
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                    Label lblValorConvenioSolicitado = (Label)e.Item.FindControl("lblValorConvenioSolicitado");
                    Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");

                    if (!Convert.IsDBNull(rowItem["Dosis"]) && rowItem["Dosis"].ToString() != string.Empty)
                    {
                        lblDosis.Visible = true;
                    }

                    if (lblValorConvenioSolicitado.Text != string.Empty)
                        lblTotalSolicitado.Text = string.Format("{0:0,0}", Convert.ToDecimal(lblTotalSolicitado.Text) + Convert.ToDecimal(lblValorConvenioSolicitado.Text));
                    if (lblValorAprobado.Text != string.Empty)
                        lblTotalAprobado.Text = string.Format("{0:0,0}", Convert.ToDecimal(lblTotalAprobado.Text) + Convert.ToDecimal(lblValorAprobado.Text));
                }
            }
        }



        #endregion
    }
}
