using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_admon.formatos
{
    /// <summary>
    /// Genera el reporte de riesgos detallado consulta a consulta
    /// Autor: Adriana Diazgranados
    /// Fecha: 21 de Junio 2012
    /// </summary>
    public partial class FO_ListadoReporteRiesgos : PB_PaginaBase
    {
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
                base.Page_Load(sender, e);

                if (!this.Page.IsPostBack)
                {
                    this.LoadControls();
                    this.exportReport();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }


        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, carga el listado de las consultas con los criterios de búsqueda enviados
        /// </summary>
        public void LoadControls()
        {
            object dateFrom = null;
            object dateUntil = null;
            string empresas = "";
            string sedes = "";
            string usuarios = "";
            string medicos = "";
            string tiposconsulta = "";
            int beneficiarios = -1;

            if (Request.QueryString["dateFrom"] != null && Request.QueryString["dateFrom"] != string.Empty)
                dateFrom = Convert.ToDateTime(Request.QueryString["dateFrom"]);
            if (Request.QueryString["dateUntil"] != null && Request.QueryString["dateUntil"] != string.Empty)
                dateUntil = Convert.ToDateTime(Request.QueryString["dateUntil"]);
            if (Request.QueryString["empresas"] != null && Request.QueryString["empresas"] != string.Empty)
                empresas = Request.QueryString["empresas"];
            if (Request.QueryString["sedes"] != null && Request.QueryString["sedes"] != string.Empty)
                sedes = Request.QueryString["sedes"];
            if (Request.QueryString["usuarios"] != null && Request.QueryString["usuarios"] != string.Empty)
                usuarios = Request.QueryString["usuarios"];
            if (Request.QueryString["medicos"] != null && Request.QueryString["medicos"] != string.Empty)
                medicos = Request.QueryString["medicos"];
            if (Request.QueryString["tiposconsulta"] != null && Request.QueryString["tiposconsulta"] != string.Empty)
                tiposconsulta = Request.QueryString["tiposconsulta"];
            if (Request.QueryString["beneficiarios"] != null && Request.QueryString["beneficiarios"] != string.Empty)
                beneficiarios = Convert.ToInt16(Request.QueryString["beneficiarios"]);

            Consulta objConsulta = new Consulta();
            this.gvDetalle.DataSource = objConsulta.ListConsultasRiesgosDetalle(dateFrom, dateUntil, empresas, sedes, usuarios, medicos, tiposconsulta, beneficiarios);
            this.gvDetalle.DataBind();
        }


        /// <summary>
        /// Método, exporta la grilla a Excel
        /// </summary>
        public void exportReport()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xls");
            Response.Charset = "";
            Response.ContentEncoding = System.Text.UTF7Encoding.Default;
            this.EnableViewState = false;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            this.gvDetalle.RenderControl(htmlWrite);
            
            Response.Write(stringWrite.ToString());
            Response.End();
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
