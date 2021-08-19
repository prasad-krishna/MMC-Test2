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
    /// Permite la generación del reportes
    /// </summary>
    /// <remarks>Autor: KRAM Group
    /// </remarks>   

    public partial class formaSeleccionarEntidadReporte : PB_PaginaBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            /* Se lee la informacion de parametros pasados via GET, el xml que define el reporte */
            string pathPlantillas = Server.MapPath("Plantillas/Selects/");
            int pagina;
            Reporte reporte;
            string resultado;
            string plantillaTablaConsulta;
            string plantillaFilaCondicion;
            StreamReader archivoLectura;


            /* Se revisa si existe una pagina de reporte definida sino se asume 1 */
            try
            {
                pagina = Convert.ToInt32(Request["pagina"].ToString());
            }
            catch
            {
                pagina = 1;
            }

            /* Lee la plantillas de las tablas */
            archivoLectura = new StreamReader(pathPlantillas + "TablaConsulta.html");
            plantillaTablaConsulta = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de fila de titulo */
            archivoLectura = new StreamReader(pathPlantillas + "FilaCondicion.html");
            plantillaFilaCondicion = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Se genera el nuevo reporte */
            reporte = new Reporte();
            resultado = reporte.darFormularioReporteSelector("Seleccionar " + Encoder.HtmlEncode(Request["nombreEntidad"].ToString()),
                //Inicio
                //Auto:Diego Montejano Avila
                //Proyecto: Auditoria 2014
                //Fecha: 2014/09/17
                //Observaciones: Se utiliza la dll de Microsoft para eliminar el XSS
                Encoder.HtmlEncode(Request["tabla"].ToString()),
                Encoder.HtmlEncode(Request["campo_valor"].ToString()),
                Encoder.HtmlEncode(Request["campo_nombre"].ToString()),
                Encoder.HtmlEncode(Request["nombre_campo"].ToString()),
                Encoder.HtmlEncode(Request["titulo_campo"].ToString()), plantillaTablaConsulta, plantillaFilaCondicion,
                Encoder.HtmlEncode(Request["conexion"].ToString()),
                Encoder.HtmlEncode(Request["corporativo"].ToString()),
                Encoder.HtmlEncode(Request["condicion"].ToString()));
            //Fin
            this.contenidoReporte.InnerHtml = resultado;

        }

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
