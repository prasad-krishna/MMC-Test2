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
    /// Despliega el listado de histórico de solicitudes y resumen de prespuesto familiar de reembolso
    /// </summary>
    public partial class LIS_solicitudgastos : PB_PaginaBase
    {
        #region Atributos

        #endregion

        #region Inicialización

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //Inicio MAHG 22/01/10
            //Se carga el load de la página base

            base.Page_Load(sender, e);

            //Fin MAHG 22/01/10

            try
            {
                if (!this.Page.IsPostBack)
                {

                    if (Request.QueryString["employee_id"] != null)
                    {
                        this.LoadControlUsr(Convert.ToInt32(Request.QueryString["employee_id"]));
                    }
                }


            }
            catch (Exception ex)
            {
                string message = "";
                message = "<script language='javascript'>alert('Exception :" + ex.Message + "')</script>";

                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin      

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, carga redirect a la solicitud
        /// </summary>
        private void LinkNewRequest()
        {

            if (Session["EMPLOYEE_ID"] != null)
            {
                Response.Redirect("AE_solicitud.aspx?acc=IX&employee_id=" + Session["EMPLOYEE_ID"].ToString() + "&id=0");
            }
        }


        /// <summary>
        /// Método, carga la información 
        /// </summary>
        private void LoadControlUsr(int p_idEmpleado)
        {

            if (p_idEmpleado != 0)
            {
                PresupuestosIndividuo objPresupuesto = new PresupuestosIndividuo();

                //Cargar solicitudes

                ViewState["BusquedaEspecial"] = true;
                object dateFrom = null;
                object dateUntil = null;
                object dateCreateFrom = null;
                object dateCreateUntil = null;
                int idProveedor = 0;
                int mesLiquidacion = 0;
                int anoLiquidacion = 0;
                int idSolicitudEstado1 = 0;
                int idSolicitudEstado2 = 0;
                int idSolicitudEstado3 = 0;
                long id_empleado = 0;
                long id_beneficiario = 0;
                ArrayList arrEstados = new ArrayList();

                SolicitudTipoServicio objSolicitud = new SolicitudTipoServicio();
                objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
                id_empleado = p_idEmpleado;

                this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudTipoServicioBusqueda(dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, mesLiquidacion, anoLiquidacion, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, id_empleado, id_beneficiario, 0, 0, 0, 1, null);
                this.dtgSolicitudes.DataBind();

                string Rid = p_idEmpleado.ToString();
                this.Rcount.Text = this.dtgSolicitudes.Items.Count.ToString();
                if (this.dtgSolicitudes.Items.Count > 0)
                {
                    this.dtgSolicitudes.Visible = true;
                    this.Rmsg.Visible = false;
                }
                else
                {
                    this.dtgSolicitudes.Visible = false;
                    this.Rmsg.Text = "No se han registrado solicitudes";
                    this.Rmsg.Visible = true;
                }

                //Cargar presupuesto
                objPresupuesto.Id_empleado = p_idEmpleado;
                objPresupuesto.FechaInicio = DateTime.Now;
                this.dtgPresupuesto.DataSource = objPresupuesto.ConsultPresupuestosIndividuo();
                this.dtgPresupuesto.DataBind();

                Permissions objPermission = new Permissions();
                objPermission.IdUser = Convert.ToInt32(Session["IdUser"]);
                objPermission.IdPermission = Convert.ToInt32(Permissions.EnumPermissions.ImprimirCertificadosConstancias);
                objPermission.GetPermission();

                if (objPermission.IdPermissionType != 0)
                {
                    this.lnkCertificado.Visible = true;
                    this.lnkConstancia.Visible = true;
                }
                else
                {
                    this.lnkCertificado.Visible = false;
                    this.lnkConstancia.Visible = false;
                }

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
            this.lnkBeneficiarios.Click += new System.EventHandler(this.lnkBeneficiarios_Click);
            this.lnkCertificado.Click += new System.EventHandler(this.lnkCertificado_Click);
            this.lnkConstancia.Click += new System.EventHandler(this.lnkConstancia_Click);
            this.dtgPresupuesto.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgPresupuesto_ItemDataBound);
            this.dtgSolicitudes.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgSolicitudes_ItemCommand);
            this.dtgSolicitudes.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitudes_PageIndexChanged);
            this.dtgSolicitudes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgSolicitudes_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        /// <summary>
        /// Evento, paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgSolicitudes.CurrentPageIndex = e.NewPageIndex;
            this.LoadControlUsr(Convert.ToInt32(Request.QueryString["employee_id"]));
        }


        /// <summary>
        /// Evento, carga controles de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");

            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");

            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblPaciente = (Label)e.Item.FindControl("lblPaciente");

                if (e.Item.Cells[4].Text == string.Empty || e.Item.Cells[4].Text == "&nbsp;")
                {
                    SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                    objEmpleado.Id_empleado = Convert.ToInt32(e.Item.Cells[2].Text);
                    objEmpleado.GetSIC_EMPLEADO();
                    lblPaciente.Text = objEmpleado.Nombre_completo;
                }
                else
                {
                    SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                    DataTable dtBeneficiarios;
                    objBeneficiario.Opcion = 2;
                    objBeneficiario.Beneficiario_id = Convert.ToInt32(e.Item.Cells[4].Text);
                    dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    lblPaciente.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                }

            }

        }

        /// <summary>
        /// Evnia a ingreso de nuevo reembolso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkReembolso_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AE_solicitudreembolso.aspx?employee_id=" + Request.QueryString["employee_id"]);
        }

        /// <summary>
        /// Evento, Envia a ingreso de nueva autorización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAutorizacion_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AE_solicitudautorizacion.aspx?employee_id=" + Request.QueryString["employee_id"]);

        }

        /// <summary>
        /// Evento, carga el listado de beneficiarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBeneficiarios_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("LIS_beneficiarios.aspx?EMPLOYEE_ID=" + Request.QueryString["employee_id"], 900, 400);
        }

        /// <summary>
        /// Evento, envia a impresión de certificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkCertificado_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("../Formatos/FO_CertificadoPlanMedico.aspx?employee_id=" + Request.QueryString["employee_id"], 800, 500);


        }

        /// <summary>
        /// Evento, envia a impresión de constancia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkConstancia_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("../Formatos/FO_ConstanciaPlanMedico.aspx?employee_id=" + Request.QueryString["employee_id"], 800, 500);


        }


        /// <summary>
        /// Evento, Realiza la carga de datos en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgPresupuesto_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblBeneficiario = (Label)e.Item.FindControl("lblBeneficiario");
                Label lblPresupuesto = (Label)e.Item.FindControl("lblPresupuesto");
                Label lblUtilizado = (Label)e.Item.FindControl("lblUtilizado");
                Label lblDisponible = (Label)e.Item.FindControl("lblDisponible");
                Label lblExceso = (Label)e.Item.FindControl("lblExceso");
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                DataTable dtBeneficiarios;

                if (e.Item.Cells[0].Text != "&nbsp;" && e.Item.Cells[0].Text != "0")
                {
                    objBeneficiario.Opcion = 2;
                    objBeneficiario.Beneficiario_id = Convert.ToInt32(e.Item.Cells[0].Text);
                    dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    lblBeneficiario.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                }
                else
                {
                    objEmpleado.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objEmpleado.GetSIC_EMPLEADO();
                    lblBeneficiario.Text = objEmpleado.Nombre_completo;
                }

                if (!Convert.IsDBNull(rowItem["Presupuesto"]))
                {
                    lblPresupuesto.Text = string.Format("{0:$ #,##0;($#,##0)}", rowItem["Presupuesto"].ToString());
                    if (!Convert.IsDBNull(rowItem["Utilizado"]))
                        lblDisponible.Text = string.Format("{0:$ #,##0;($#,##0)}", (Convert.ToDecimal(rowItem["Presupuesto"]) - Convert.ToDecimal(rowItem["Utilizado"])));
                    else
                        lblDisponible.Text = string.Format("{0:$ #,##0;($#,##0)}", rowItem["Presupuesto"].ToString());
                }
                else
                {
                    lblPresupuesto.Text = "Sin tope";
                    lblDisponible.Text = "";
                }

                if (!Convert.IsDBNull(rowItem["Utilizado"]))
                    lblUtilizado.Text = string.Format("{0:$ #,##0;($#,##0)}", rowItem["Utilizado"].ToString());
                else
                    lblUtilizado.Text = "0";

                if (!Convert.IsDBNull(rowItem["ExcesoPresupuesto"]))
                    lblExceso.Text = string.Format("{0:$ #,##0;($#,##0)}", rowItem["ExcesoPresupuesto"].ToString());
                else
                    lblExceso.Text = "0";




                if (e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                    e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");

                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                    e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");

                }
            }

        }

        /// <summary>
        /// Evento, realiza el llamado al ver el detalle de la solicitud
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ver")
                {
                    if (Convert.ToInt16(e.Item.Cells[5].Text) == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso))
                        Response.Redirect("AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&IdSolicitudTipoServicio=" + e.Item.Cells[3].Text);
                    if (Convert.ToInt16(e.Item.Cells[5].Text) == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion))
                        Response.Redirect("AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&IdSolicitudTipoServicio=" + e.Item.Cells[3].Text);
                    if (Convert.ToInt16(e.Item.Cells[5].Text) == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Orden))
                        Response.Redirect("AE_solicitudordenresumen.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&IdSolicitudTipoServicio=" + e.Item.Cells[3].Text + "&IdConsulta=" + e.Item.Cells[14].Text);

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
