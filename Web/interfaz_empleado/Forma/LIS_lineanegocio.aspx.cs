/*
HC, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer LLC.
HC is proprietary to Mercer LLC trade secret information. The
documentation and all related HC materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer LLC.
*/
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

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza la búsqueda de Línes de Negocio
    /// </summary>
    /// <remarks>Autor: Emilio Bueno
    /// Fecha de creación: 27/09/2012
    /// </remarks>

    public partial class LIS_lineanegocio : PB_PaginaBase
    {
        #region Inicializacion

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);

                if (!this.Page.IsPostBack)
                {
                    this.loadControls();

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
        /// Método, carga los controles iniciales
        /// </summary>
        public void loadControls()
        {
            return;
        }

        /// <summary>
        /// Método, realiza la búsqueda
        /// </summary>
        public void findLineasNegocio()
        {
            LineasNegocio objLineasNegocio = new LineasNegocio();
            objLineasNegocio.NombreLineaNegocio = this.txtNombreLineaNegocio.Text.Trim();
            objLineasNegocio.empresa_id = Convert.ToInt32(Session["Company"]);

            this.grvList.DataSource = objLineasNegocio.ConsultLineasNegocio();
            this.grvList.DataBind();

            if (this.grvList.Items.Count > 0)
            {
                this.grvList.Visible = true;
                this.lblMensaje.Visible = false;
                this.lblCount.Text = this.grvList.Items.Count.ToString();

            }
            else
            {
                this.grvList.Visible = false;
                this.lblMensaje.Visible = true;
                this.lblMensaje.Text = "No se encontraron registros";
                this.lblCount.Text = "0";
            }

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
            this.grvList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grvList_ItemCommand);
            this.grvList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grvList_PageIndexChanged);
            this.grvList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grvList_ItemDataBound);
        }
        #endregion

        /// <summary>
        /// Evento, realiza paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void grvList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.grvList.CurrentPageIndex = e.NewPageIndex;
            this.findLineasNegocio();
        }

        /// <summary>
        /// Evento, carga evento para selección en grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");
            }
        }

        /// <summary>
        /// Evento, abre la ventana para edición
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void grvList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.OpenWindow("AE_lineanegocio.aspx?IdLineaNegocio=" + e.CommandArgument.ToString(), 350, 250);
            }
        }

        /// <summary>
        /// Evento, realiza el llamado a la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            this.grvList.CurrentPageIndex = 0;
            this.findLineasNegocio();
        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbAdicionar_Click1(object sender, ImageClickEventArgs e)
        {
            this.OpenWindow("AE_lineanegocio.aspx", 350, 250);
        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionar_Click1(object sender, EventArgs e)
        {
            this.OpenWindow("AE_lineanegocio.aspx", 350, 250);
        }

        #endregion


   
    }
}
