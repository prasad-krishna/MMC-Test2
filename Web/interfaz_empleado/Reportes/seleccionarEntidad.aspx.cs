using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Mercer.Medicines.Logic;
using Microsoft.Security.Application;

namespace TPA.interfaz_empleado.reportes
{
    /// <summary>
    /// Generador de reportes
    /// </summary>
    public partial class seleccionarEntidad : PB_PaginaBase
    {
        #region Atributos


        #endregion

        #region Inicialización

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string pathPlantillas = Server.MapPath("Plantillas/Selects/");
                int pagina;
                Reporte reporte;
                string resultado;
                string plantillaTabla;
                string plantillaFilaTitulo;
                string plantillaTitulo;
                string plantillaFilaCelda;
                string plantillaCelda;
                string plantillaNavegacion;
                StreamReader archivoLectura;

                try
                {
                    pagina = Convert.ToInt32(Request["paginaActual"].ToString());
                }
                catch
                {
                    pagina = 1;
                }
                /* Se genera el nuevo reporte */
                reporte = new Reporte();

                /* Lee la plantillas de las tablas */
                archivoLectura = new StreamReader(pathPlantillas + "Tabla.html");
                plantillaTabla = archivoLectura.ReadToEnd();
                archivoLectura.Close();

                /* Lee la plantillas de fila de titulo */
                archivoLectura = new StreamReader(pathPlantillas + "FilaTitulo.html");
                plantillaFilaTitulo = archivoLectura.ReadToEnd();
                archivoLectura.Close();

                /* Lee la plantillas de titulo */
                archivoLectura = new StreamReader(pathPlantillas + "Titulo.html");
                plantillaTitulo = archivoLectura.ReadToEnd();
                archivoLectura.Close();

                /* Lee la plantillas de fila de celda */
                archivoLectura = new StreamReader(pathPlantillas + "FilaCelda.html");
                plantillaFilaCelda = archivoLectura.ReadToEnd();
                archivoLectura.Close();

                /* Lee la plantillas de celda */
                archivoLectura = new StreamReader(pathPlantillas + "Celda.html");
                plantillaCelda = archivoLectura.ReadToEnd();
                archivoLectura.Close();

                /* Lee la plantillas de navegacion */
                archivoLectura = new StreamReader(pathPlantillas + "Navegacion.html");
                plantillaNavegacion = archivoLectura.ReadToEnd();
                archivoLectura.Close();


                //RAM* Agrego corporativo
                resultado = reporte.darReporteSelector(Encoder.HtmlEncode(Request["campo_nombre"].ToString()), 
                    Encoder.HtmlEncode(Request["tabla"].ToString()), 
                    Encoder.HtmlEncode(Request["campo_valor"].ToString()), 
                    Encoder.HtmlEncode(Request["campo_nombre"].ToString()), 
                    Encoder.HtmlEncode(Request["campo_nombre"].ToString()), 
                    Encoder.HtmlEncode(Request["campo_nombre"].ToString()), Request, 1, 10, plantillaTabla, plantillaTitulo, plantillaFilaTitulo, plantillaCelda, plantillaFilaCelda, plantillaNavegacion, 
                    Encoder.HtmlEncode(Request["conexion"].ToString()),
                    Encoder.HtmlEncode(Request["corporativo"].ToString()),
                    Encoder.HtmlEncode(Request["condicion"].ToString()));
                this.contenidoReporte.InnerHtml = resultado;

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
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
