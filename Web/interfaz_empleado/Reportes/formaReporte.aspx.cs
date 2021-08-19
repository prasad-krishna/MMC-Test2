using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace TPA.interfaz_empleado.reportes
{
    /// <summary>
    /// Generador de reportes
    /// </summary>
    public partial class formaReporte : PB_PaginaBase
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
            string archivoXML = Request["xml"].ToString();
            string pathXML = Server.MapPath("xmls/");
            string pathPlantillas = Server.MapPath("Plantillas/");
            int pagina;
            Reporte reporte;
            string resultado;


            try
            {
                try
                {
                    pagina = Convert.ToInt32(Request["pagina"].ToString());
                }
                catch
                {
                    pagina = 1;
                }
                /* Se genera el nuevo reporte */
                reporte = new Reporte();
                resultado = reporte.formularioXMLReporte(archivoXML, pathXML, pathPlantillas, this.Request, Session);
                //resultado.Replace("onChange='cambiarRestriccion0();var a = document.getElementById('_C0'); if(this.options.selectedIndex != 0 && a.options.selectedIndex == 0){a.options.selectedIndex = 2;}", "onChange='cambiarRestriccion0();var a = document.getElementById('_C0'); if(this.options.selectedIndex != 0 && a.options.selectedIndex == 0){a.options.selectedIndex = 2;}; alert ('cambiar valor')");
                this.contenidoReporte.InnerHtml = resultado;
                //}
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
