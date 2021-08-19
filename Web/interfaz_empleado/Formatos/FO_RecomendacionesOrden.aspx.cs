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
    /// Formato de recomendaciones para orden
    /// </summary>
    public partial class FO_RecomendacionesOrden : PB_PaginaBase
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


                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"print", script);

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
            this.WC_EncabezadoFormatoIncapacidad1.TipoOrden = "RECOMENDACIONES";
            Solicitud objSolicitud = new Solicitud();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();
            this.lblRecomendaciones.Text = objSolicitud.Observaciones;

            ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
            objConsultaOpcion.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);


            //Inicio MAHG 
            //Proyecto: Wellness
            //Fecha: 02-05-2010
            //Se cargan las recomendaciones que se seleccionaron en HC para el asegurado

            int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());
            DataTable dtProgramas;
            if (intPais == 1)
            {
                dtProgramas = objConsultaOpcion.ConsultConsultaRecomendacionesWellness().Tables[0];                
            }
            else
            {
                objConsultaOpcion.IdPreguntaRespuestaPadre = 168;
                dtProgramas = objConsultaOpcion.ConsultConsultaOpcionPadre().Tables[0];
            }

            /*Fin MAHG 05/05/2010           
            */

            if (dtProgramas.Rows.Count > 0)
            {
                this.lblRecomendaciones.Text = "<br/><br/>Se recomienda ingresar a alguno de los siguientes programas";
                btlRecomendaciones.DataSource = dtProgramas;
                btlRecomendaciones.DataBind();
               
            }

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
