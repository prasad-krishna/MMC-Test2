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
    public partial class FO_RemisionesOrden : PB_PaginaBase
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

                    this.WC_DiagnosticosFormato1.DesplegarDescripcion = true;
                    this.WC_EncabezadoFormatoOrden1.DesplegarPrestador = true;

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]));
                        this.LoadGridDetail(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]));
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
                    //Inicio MAHG 15/04/2010
                    //Se cargan las etiquetas 

                    LoadStrings();

                    //Fin MAHG 15/04/2010
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Método

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Oculta las etiquetas que no deben mostrarse para México
        /// </summary>
        private void LoadStrings()
        {
            int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

            if (intPais == 1)
            {
                dtgDetalle.Columns[2].Visible = false;
                pnlTotal.Visible = false;
                lblTituloTotal.Visible = false;
                this.lblTextoFormato.Text = this.lblTextoFormato.Text + "SI EL EMPLEADO ES INCAPACITADO O REQUIERE DE PROCEDIMIENTO, FAVOR ADJUNTAR RESUMEN HISTORIA CLÍNICA";
            }
        }

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio)
        {
            //Cargar datos generales solicitud
            Solicitud objSolicitud = new Solicitud();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();

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

            this.lblObservaciones.Text = objTipoServicio.Comentarios;

            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = objSolicitud.IdPrestador;
            objPrestador.GetPrestadores();
            this.lblMedico.Text = objPrestador.NombrePrestador;
            this.lblIdentificacion.Text = objPrestador.Nit;

            /*MAHG 11/06/2010*/
            //Se agrega la cedula e institución del médico
            this.lblCedula.Text = "Céd. Prof.: " + objPrestador.Cedula;
            //Fin MAHG 11/06/2010

            //Adriana Diazgranados 10/10/2012
            //Se agrega la institución y la fecha de expedición de la cédula
            this.lblInstitucion.Text = "Institución: " + objPrestador.Institucion;            
            //Fin AD
        }

        /// <summary>
        /// Método, carga la grilla de detalle y totales
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        /// <param name="p_idProveedor"></param>
        public void LoadGridDetail(long p_idSolicitud, long p_idSolicitudTipoServicio)
        {
            //Consultar servicios
            SolicitudServicio objServicios = new SolicitudServicio();
            objServicios.IdSolicitud = p_idSolicitud;
            objServicios.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            DataTable dtServicios = objServicios.ConsultSolicitudServicioFormatos().Tables[0];


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

        }
        #endregion

        #endregion
    }
}
