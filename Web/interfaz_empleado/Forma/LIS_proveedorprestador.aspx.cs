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
    /// Descripción breve de LIS_proveedorprestador.
    /// </summary>
    public partial class LIS_proveedorprestador : PB_PaginaBase
    {

       

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

                if (!this.Page.IsPostBack)
                {
                    this.FillList("Especialidades", "Especialidad", this.ddlEspecialidad, "--Todas---");
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
        /// Método, realiza la consulta de Proveedores
        /// </summary>
        private void FindProveedorPrestador()
        {
            Proveedores objProveedor = new Proveedores();
            objProveedor.NombreProveedor = this.Fnombre.Text.Trim();
            objProveedor.IdEspecialidad = Convert.ToInt16(this.ddlEspecialidad.SelectedValue);
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);

            this.dtgBusqueda.DataSource = objProveedor.ConsultProveedores();
            this.dtgBusqueda.DataBind();
            this.Fcount.InnerHtml = (this.dtgBusqueda.PageCount * this.dtgBusqueda.PageSize).ToString() + " registados aproximados.";

            if (this.dtgBusqueda.Items.Count > 0)
            {
                this.dtgBusqueda.Visible = true;
            }
            else
            {
                this.dtgBusqueda.Visible = false;
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
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.imbAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionar_Click);
            this.lnkAdicionar.Click += new System.EventHandler(this.lnkAdicionar_Click);
            this.dtgBusqueda.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgBusqueda_ItemCommand);
            this.dtgBusqueda.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgBusqueda_PageIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            this.dtgBusqueda.CurrentPageIndex = 0;
            this.FindProveedorPrestador();
        }

        /// <summary>
        /// Evento, realiza la paginación de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgBusqueda_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgBusqueda.CurrentPageIndex = e.NewPageIndex;
            this.FindProveedorPrestador();

        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de prestadores o proveedores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("AE_proveedorprestador.aspx", 750, 600);
        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de prestadores o proveedores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            this.OpenWindow("AE_proveedorprestador.aspx", 750, 600);
        }

        /// <summary>
        /// Evento, abre la ventana para edición
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgBusqueda_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.OpenWindow("AE_proveedorprestador.aspx?IdProveedor=" + e.Item.Cells[1].Text + "&IdPrestador=" + e.Item.Cells[0].Text, 750, 600);
            }

        }

        #endregion




    }
}

