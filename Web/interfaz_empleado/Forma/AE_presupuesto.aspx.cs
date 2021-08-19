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

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Despliega los datos del presupuesto
    /// </summary>
    public partial class AE_presupuesto : PB_PaginaBase
    {

        #region Atributos

        #endregion

        #region Inicialización

        /// <summary>
        /// Inicialización
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

                               
                WC_Report1.redirectPage = "AE_presupuestoPrint.aspx?id=0";

                if (!this.Page.IsPostBack)
                {
                    this.FindPrespuestos();
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
        /// Método, realiza la carga de la información del presupuesto
        /// </summary>
        private void FindPrespuestos()
        {
            PresupuestosEmpresa objPresupuesto = new PresupuestosEmpresa();
            objPresupuesto.Empresa_id = Convert.ToInt32(Session["Company"]);
            this.dtgPresupuesto.DataSource = objPresupuesto.ConsultPresupuestosEmpresa();
            this.dtgPresupuesto.DataBind();

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
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.dtgPresupuesto.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgPresupuesto_PageIndexChanged);

        }
        #endregion

        /// <summary>
        /// Evento, realiza la paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgPresupuesto_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgPresupuesto.CurrentPageIndex = e.NewPageIndex;
            this.FindPrespuestos();
        }

        #endregion


    }
}
