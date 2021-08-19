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
    /// Formato de orden de incapacidad
    /// </summary>
    public partial class FO_IncapacidadOrden : PB_PaginaBase
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

                    //Inicio MAHG 12/04/10
                    //Se oculta algunas etiquetas para México

                    int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"]);

                    if (intPais == 1)
                    {
                        LoadStrings(false);
                        this.WC_EncabezadoFormatoIncapacidad1.TipoOrden = "RESUMEN DE INCAPACIDAD";

                        //Inicio MAHG 14/06/2010
                        //Se oculta el campo Registro Médico No. para México
                        this.lblRegistroMedico.Visible = false;
                        //Fin MAHG 14/06/2010
                    }

                    //Inicio MAHG 12/04/10
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
        private void LoadStrings(bool bolHabilitar)
        {
            lblFooterIncapacidad.Visible = false;                 
            //lblTituloObservaciones.Visible = bolHabilitar;
            //lblObservaciones.Visible = bolHabilitar;
        }

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio)
        {
            this.WC_EncabezadoFormatoIncapacidad1.TipoOrden = "RESUMEN DE INCAPACIDAD Y AUXILIO DE ENFERMEDAD";
            this.LoadIncapacidad(p_idSolicitud);
        }

        /// <summary>
        /// Método, carga la información de la incapacidad
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void LoadIncapacidad(long p_idSolicitud)
        {
            Incapacidad objIncapacidad = new Incapacidad();
            objIncapacidad.IdSolicitud = p_idSolicitud;
            objIncapacidad.GetIncapacidad();

            if (objIncapacidad.IdIncapacidad != 0)
            {
                this.lblFechaInicio.Text = objIncapacidad.FechaInicio.ToShortDateString();
                this.lblFechaFin.Text = objIncapacidad.FechaFin.ToShortDateString();
                TimeSpan tmDias = objIncapacidad.FechaFin.Subtract(objIncapacidad.FechaInicio);
                this.lblDias.Text = (tmDias.Days + 1).ToString(); ;
                if (objIncapacidad.Continuacion)
                    this.lblContinuacion.Text = "SI";
                else
                    this.lblContinuacion.Text = "NO";
                if (objIncapacidad.Transcripcion)
                    this.lblTranscripcion.Text = "SI";
                else
                    this.lblTranscripcion.Text = "NO";

                this.lblObservaciones.Text = objIncapacidad.Observaciones;
                this.LoadDiagnosticos(objIncapacidad.IdIncapacidad);
                this.LoadDatosConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
            }
        }


        /// <summary>
        /// Método, carga los diagnósticos de la incapacidad
        /// </summary>
        /// <param name="p_idIncapacidad"></param>
        public void LoadDiagnosticos(long p_idIncapacidad)
        {
            IncapacidadDiagnosticos objDiagnosticos = new IncapacidadDiagnosticos();
            objDiagnosticos.IdIncapacidad = p_idIncapacidad;

            this.dtgDiagnosticos.DataSource = objDiagnosticos.ConsultIncapacidadDiagnosticos().Tables[0];
            this.dtgDiagnosticos.DataBind();

            if (dtgDiagnosticos.Items.Count == 0)
                this.dtgDiagnosticos.Visible = false;

            this.dtgDiagnosticos.Columns[1].Visible = true;
        }
        /// <summary>
        /// Método, carga los diagnósticos de la incapacidad
        /// </summary>
        /// <param name="p_idIncapacidad"></param>
        public void LoadDatosConsulta(long p_idConsulta)
        {

            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();
            GeneralTable objGeneral = new GeneralTable();
            objGeneral.TableName = "TipoEnfermedades";
            objGeneral.ColumnName = "TipoEnfermedad";
            objGeneral.Id = objConsulta.IdTipoEnfermedad;
            objGeneral.GetGeneralTable();
            this.lblTipoEnfermedad.Text = objGeneral.Nombre;

            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = objConsulta.IdPrestador;
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
