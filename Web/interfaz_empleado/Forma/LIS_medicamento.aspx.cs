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
    /// Realiza la búsqueda de medicamentos
    /// </summary>
    public partial class LIS_medicamento : PB_PaginaBase
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

                if (!this.Page.IsPostBack)
                {
                    this.LoadControlUsr();
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
        private void LoadControlUsr()
        {
            this.FillList("Laboratorios", "Laboratorio", this.Flaboratory, "--Todos--");

        }

        /// <summary>
        /// Método, realiza la consulta de medicamentos
        /// </summary>
        private void FindMedicine()
        {
            Medicamentos objMedicine = new Medicamentos();
            objMedicine.NombreComercial = this.Fnombre.Text.Trim();
            objMedicine.IdLaboratorio = Convert.ToInt32(this.Flaboratory.SelectedValue);
            objMedicine.PrincipioActivo = this.Fprincipio_activo.Text.Trim();
            objMedicine.empresa_id = Convert.ToInt32(Session["Company"]);
            objMedicine.IdTipoServicio = Convert.ToInt16(Servicios.EnumTiposServicio.Medicamentos);
            objMedicine.Activo = -1;

            this.Flist.DataSource = objMedicine.ConsultMedicamentos();
            this.Flist.DataBind();
            this.Fcount.Text = this.Flist.Items.Count.ToString();

            if (this.Flist.Items.Count > 0)
            {
                this.lblMensaje.Text = "";
                this.Flist.Visible = true;
            }
            else
            {
                this.lblMensaje.Text = "No se encuentra Medicamentos con los criterios de búsqueda";
                this.Flist.Visible = false;
            }

            this.Flist.CurrentPageIndex = 0;
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
            this.Bfinder.Click += new System.EventHandler(this.Bfinder_Click);
            this.imbAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionar_Click);
            this.lnkAdicionar.Click += new System.EventHandler(this.lnkAdicionar_Click);
            this.Flist.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Flist_ItemCommand);
            this.Flist.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Flist_PageIndexChanged);
            this.Flist.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.Flist_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        /// <summary>
        /// Evento, realiza paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Flist_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.Flist.CurrentPageIndex = e.NewPageIndex;
            this.FindMedicine();
        }

        /// <summary>
        /// Evento, realiza el llamado a la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Bfinder_Click(object sender, System.EventArgs e)
        {
            this.Flist.CurrentPageIndex = 0;
            this.FindMedicine();
        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.OpenWindow("AE_medicamento.aspx", 600, 700);
        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("AE_medicamento.aspx", 600, 700);

        }

        /// <summary>
        /// Evento, carga evento para selección en grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Flist_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
        /// Evento, realiza el evento seleccionado en la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Flist_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.OpenWindow("AE_medicamento.aspx?medicine_id=" + e.CommandArgument.ToString(), 600, 700);

            }
        }

        #endregion 





    }
}
