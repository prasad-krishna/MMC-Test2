using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using System.Web.UI.HtmlControls;


namespace TPA.interfaz_empleado.reportes
{
    /// <summary>
    /// Permite la generación del reportes
    /// </summary>
    /// <remarks>Autor: KRAM Group
    /// </remarks>   
    public partial class reporte : PB_PaginaBase
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
            /* Se lee la informacion de parametros pasados via GET, el xml que define el reporte */
            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/17
            //Observaciones: Se utiliza la dll de Microsoft para eliminar el XSS
            string archivoXML = Encoder.HtmlEncode(Request["xml"].ToString());
            //FIN
            string pathXML = Server.MapPath("xmls/");
            string pathPlantillas = Server.MapPath("Plantillas/");
            int pagina;
            Reporte reporte;
            string resultado;
            int cantidad_grupos = 0;

            try
            {
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
                resultado = reporte.XMLReporte(archivoXML, pathXML, pathPlantillas, "", "", this.Request, pagina, 25, Session);
                if (resultado.Contains("encontraron registros para su consulta"))
                    lnkExcel.Visible = false;
                this.contenidoReporte.InnerHtml = resultado;
                //}
            }
            catch (Exception ex)
            {
                //GAMM. Information Leakage. No se debe mostrar el mensje de error.
                this.DisplayMessage(ex.Message);
                Response.Redirect(@"~\ErrorPage.aspx", true);
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
