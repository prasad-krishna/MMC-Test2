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
    /// Formato de autorización para medicamentos
    /// </summary>
    public partial class FO_MedicamentosMargen : PB_PaginaBase
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
                    this.WC_DiagnosticosFormato1.DesplegarDescripcion = false;
                    this.WC_EncabezadoFormato1.DesplegarPrestador = false;

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]));
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

        #region Método

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio)
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

            //Cargar los servicios
            SolicitudServicio objServicios = new SolicitudServicio();
            objServicios.IdSolicitud = p_idSolicitud;
            objServicios.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            this.dtgDetalle.DataSource = objServicios.ConsultSolicitudServicioFormatos().Tables[0];
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

        }
        #endregion

        #endregion
    }
}