namespace TPA.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Control que permite la búsqueda del proveedor
    /// </summary>
    public partial class WC_BuscarProveedor : WC_Base
    {
        #region Atributos

        #endregion

        #region Inicialización

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            this.lblResultado.Text = "";
            if (!this.IsPostBack)
            {
                this.txtNombre.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkBuscar.UniqueID + "',''); return false }");
                PB_PaginaBase objPaginaBase = new PB_PaginaBase();
                objPaginaBase.FillList("Especialidades", "Especialidad", this.ddlEspecialidad, "--Especialidad--");
            }

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la búsqueda de los diagnósticos
        /// </summary>
        public void FindProveedores(bool page)
        {

            Proveedores objProveedor = new Proveedores();
            DataTable dtProveedores;
            objProveedor.IdTipoServicio = Convert.ToInt16(this.txtIdTipoServicio.Text);
            objProveedor.NombreProveedor = this.txtNombre.Text;
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);

            if (this.ddlEspecialidad.SelectedValue != "0")
                objProveedor.IdEspecialidad = Convert.ToInt16(this.ddlEspecialidad.SelectedValue);

            if (this.txtCiudad.Text != string.Empty)
                dtProveedores = objProveedor.ConsultEmpresaProveedoresUser(Convert.ToInt32(Session["IdUser"])).Tables[0];
            else
                dtProveedores = objProveedor.ConsultEmpresaProveedores().Tables[0];

            if (dtProveedores.Rows.Count < 1)
            {
                this.lblResultado.Text = "No se encuentran resultados";
            }
            else
            {
                if (dtProveedores.Rows.Count == 1)
                {
                    string script = "<script language='javascript'>DevolverProveedor('" + dtProveedores.Rows[0]["NombreProveedor"].ToString() + "'," + dtProveedores.Rows[0]["IdProveedor"].ToString() + ",'" + dtProveedores.Rows[0]["Telefonos"].ToString() + "','" + dtProveedores.Rows[0]["Direcciones"].ToString() + "','" + dtProveedores.Rows[0]["Horario"].ToString() + "')</script>";

                     //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
                    if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", script, false);
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Mensaje" + this.txtTemp.Text, script);
                    }
                    
                    this.dtgProveedor.DataSource = new DataTable();
                    this.dtgProveedor.DataBind();
                    this.txtNombre.Text = "";
                    this.ddlEspecialidad.SelectedValue = "0";
                }
                else
                {
                    this.dtgProveedor.DataSource = dtProveedores;
                    this.dtgProveedor.DataBind();
                }
            }

        }

        /// <summary>
        /// Método, limpia los controles del web user control
        /// </summary>
        public void CleanControl()
        {
            this.dtgProveedor.DataBind();
            this.txtNombre.Text = "";
            this.ddlEspecialidad.SelectedIndex = 0;
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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlEspecialidad.SelectedIndexChanged += new System.EventHandler(this.ddlEspecialidad_SelectedIndexChanged);
            this.lnkBuscar.Click += new System.EventHandler(this.lnkBuscar_Click);
            this.dtgProveedor.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgProveedor_PageIndexChanged);
            this.dtgProveedor.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgProveedor_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void lnkBuscar_Click(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindProveedores(false);

        }

        private void dtgProveedor_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "seleccionar")
            {
                LinkButton lnkProveedor = (LinkButton)e.Item.FindControl("lnkProveedor");

                string script = "<script language='javascript'>DevolverProveedor('" + HttpUtility.ParseQueryString(lnkProveedor.Text) + "'," + Convert.ToInt32(e.Item.Cells[1].Text).ToString() + ",'" + HttpUtility.ParseQueryString(e.Item.Cells[2].Text.ToString()) + "','" + HttpUtility.ParseQueryString(e.Item.Cells[3].Text.ToString()) + "','" + HttpUtility.ParseQueryString(e.Item.Cells[4].Text.ToString()) + "')</script>";

                 //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje" + this.txtTemp.Text, script, false);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Mensaje" + this.txtTemp.Text, script);
                }
                
                this.dtgProveedor.DataSource = new DataTable();
                this.dtgProveedor.DataBind();
                this.txtNombre.Text = "";
                this.ddlEspecialidad.SelectedValue = "0";
            }
        }

        /// <summary>
        /// Evento, realiza la paginación
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgProveedor_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgProveedor.CurrentPageIndex = e.NewPageIndex;
            this.FindProveedores(true);
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindProveedores(false);
        }

        /// <summary>
        /// Evento, carga el evento de selección
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgProveedor_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                LinkButton lnkProveedor = (LinkButton)e.Item.FindControl("lnkProveedor");
                lnkProveedor.Attributes.Add("onClick", "javascript:DevolverProveedor('" + HttpUtility.ParseQueryString(rowItem["NombreProveedor"].ToString()) + "'," + Convert.ToInt32(rowItem["IdProveedor"]).ToString() + ",'" + HttpUtility.ParseQueryString(rowItem["Telefonos"].ToString()) + "','" + HttpUtility.ParseQueryString(rowItem["Direcciones"].ToString()) + "','" + HttpUtility.ParseQueryString(rowItem["Horario"].ToString())+ "');closeLayer('BuscarDiagnostico');");
            }
        }


        #endregion





    }
}
