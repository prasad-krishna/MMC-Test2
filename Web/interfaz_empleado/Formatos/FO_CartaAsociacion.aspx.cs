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
    /// Genera la carta que asocia solicitudes para las aseguradoras
    /// </summary>
    public partial class FO_CartaAsociacion : PB_PaginaBase
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
                    if (Request.QueryString["IdSolicitudReporte"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitudReporte"]));
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

        public void LoadFormSolicitud(long p_idSolicitudReporte)
        {
            EmpresaDatos objEmpresa = new EmpresaDatos();
            objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresa.GetEmpresaDatos();

            this.lblTexto.Text = objEmpresa.TextoCarta;
            this.lblFirma.Text = objEmpresa.FirmaCarta;
            this.lblAbreviacionEmpresa.Text = objEmpresa.AbreviacionEmpresa;

            //Cargar los servicios
            SolicitudReportes objReporte = new SolicitudReportes();
            objReporte.IdSolicitudReporte = p_idSolicitudReporte;
            objReporte.GetSolicitudReportes();

            this.lblConsecutivoNombre.Text = objReporte.ConsecutivoNombre;
            this.lblFecha.Text = DateTime.Now.ToLongDateString();

            objReporte.IdSolicitudReporte = p_idSolicitudReporte;
            objReporte.Empresa_id = Convert.ToInt32(Session["Company"]);
            this.dtgDetalle.DataSource = objReporte.ConsultSolicitudesSolicitudReporte().Tables[0];
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

        /// <summary>
        /// Evento, carga los datos de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgDetalle_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblUsuario = (Label)e.Item.FindControl("lblUsuario");
                Label lblCedulaEmpleado = (Label)e.Item.FindControl("lblCedulaEmpleado");
                Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");
                Label lblEmpleado = (Label)e.Item.FindControl("lblEmpleado");
                SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                DataTable dtBeneficiarios;
                objEmpleado.Id_empleado = Convert.ToInt32(e.Item.Cells[1].Text);
                objEmpleado.GetSIC_EMPLEADO();
                lblCedulaEmpleado.Text = objEmpleado.Identificacion;
                lblEmpleado.Text = objEmpleado.Nombre_completo;

                if (e.Item.Cells[0].Text != "&nbsp;")
                {
                    objBeneficiario.Opcion = 2;
                    objBeneficiario.Beneficiario_id = Convert.ToInt32(e.Item.Cells[0].Text);
                    dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    lblUsuario.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                }
                else
                {
                    lblUsuario.Text = objEmpleado.Nombre_completo;
                }

                /*SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
                objDiagnostico.IdSolicitud = Convert.ToInt64(e.Item.Cells[2].Text);

                DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

                foreach(DataRow dr in dtDiagnosticos.Rows)
                {	
                    lblDiagnosticos.Text += dr["NombreDiagnostico"].ToString();					
                }*/

            }


        }

        #endregion
    }
}
