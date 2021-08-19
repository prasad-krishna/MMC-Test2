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
    /// Formato de Autorización para Consultas
    /// </summary>
    public partial class FO_Consultas : PB_PaginaBase
    {

        #region Atributos

      
        #endregion

        #region Inicialización

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10


                this.WC_EncabezadoFormato1.DesplegarServicios = true;
                this.tblPrincipal.Style.Add("BACKGROUND-POSITION", "center center");
                this.tblPrincipal.Style.Add("BACKGROUND-REPEAT", "no-repeat");
                this.tblPrincipal.Style.Add("BACKGROUND-IMAGE", "url(../../logos/logoFondo_" + Session["Company"] + ".gif)");

                if (!this.Page.IsPostBack)
                {
                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
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
            DataTable dtBeneficiarios;
            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            SolicitudTipoServicioProveedores objSolProveedor = new SolicitudTipoServicioProveedores();
            GeneralTable objGeneral = new GeneralTable();

            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();

            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicio.GetSolicitudTipoServicio();

            EmpresaTipoServicios objEmpresaTipoServicio = new EmpresaTipoServicios();
            objEmpresaTipoServicio.IdTipoServicio = objTipoServicio.IdTipoServicio;
            objEmpresaTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaTipoServicio.GetEmpresaTipoServicios();
            this.lblVigencia.Text = objEmpresaTipoServicio.DiasVigencia.ToString();
            this.lblTextoFormato.Text = objEmpresaTipoServicio.TextoFormato;

            this.lblUnidadAprobacion.Text = objTipoServicio.UnidadAprobacion;

            Proveedores objProveedor = new Proveedores();
            objProveedor.IdProveedor = p_idProveedor;
            objProveedor.GetProveedores();
            this.lblMedico.Text = objProveedor.NombreProveedor;


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
                objEmpleado.Id_empleado = objSolicitud.Id_empleado;
                objEmpleado.GetSIC_EMPLEADO();
                this.lblNombrePaciente.Text = objEmpleado.Nombre_completo;
                this.lblTipoIdentificacion.Text = objEmpleado.Tipo_documento;
                this.lblNumero.Text = objEmpleado.Identificacion;
            }

            objSolProveedor = new SolicitudTipoServicioProveedores();
            objSolProveedor.IdSolicitud = p_idSolicitud;
            objSolProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            int cantidad = objSolProveedor.GetCantidadProveedoresTipoServicioSolicitud();

            if (cantidad > 1 || objTipoServicio.ConsecutivoNombre.EndsWith("-"))
            {
                objSolProveedor = new SolicitudTipoServicioProveedores();
                objSolProveedor.IdProveedor = p_idProveedor;
                objSolProveedor.IdSolicitud = p_idSolicitud;
                objSolProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
                objSolProveedor.GetSolicitudTipoServicioProveedores();
                this.txtNoSolicitud.Text = objTipoServicio.ConsecutivoNombre + "-" + objSolProveedor.Consecutivo;
            }

            else
            {
                this.txtNoSolicitud.Text = objTipoServicio.ConsecutivoNombre;
            }

            //Cargar los servicios
            SolicitudServicio objServicios = new SolicitudServicio();
            objServicios.IdSolicitud = p_idSolicitud;
            objServicios.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            DataTable dtServicios = objServicios.ConsultSolicitudServicioFormatos().Tables[0];
            this.dtgDetalle.DataSource = dtServicios;
            this.dtgDetalle.DataBind();
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
                this.lblValorTotal.Text = string.Format("{0:0,0}", valorTotal);

        }

        #endregion

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

        }
        #endregion
    }
}
