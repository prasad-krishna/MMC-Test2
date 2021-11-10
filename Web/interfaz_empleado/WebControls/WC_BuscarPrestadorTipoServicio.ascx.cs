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
    ///	Control que permite la búsqueda del prestador
    /// </summary>
    public partial class WC_BuscarPrestadorTipoServicio : WC_Base
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
        public void FindPrestadores()
        {
            Prestadores objPrestador = new Prestadores();
            objPrestador.NombrePrestador = this.txtNombre.Text.Trim();
            objPrestador.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.ddlEspecialidad.SelectedValue != "0")
                objPrestador.IdEspecialidad = Convert.ToInt32(this.ddlEspecialidad.SelectedValue);


            DataTable dtPrestadores = objPrestador.ConsultEmpresaPrestadores().Tables[0];

            if (dtPrestadores.Rows.Count < 1)
            {
                this.lblResultado.Text = "No se encuentran resultados";
            }
            else
            {
                if (dtPrestadores.Rows.Count == 1)
                {
                    string script = "<script language='javascript'>DevolverPrestadorTipoServicio('" + HttpUtility.ParseQueryString(dtPrestadores.Rows[0]["NombrePrestador"].ToString()) + "'," + Convert.ToInt32(dtPrestadores.Rows[0]["IdPrestador"]).ToString() + ")</script>";


                     //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
                    if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje" , script, false);
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Mensaje", script);
                    }


                }
                else
                {
                    this.dtgPrestadores.DataSource = dtPrestadores;
                    this.dtgPrestadores.DataBind();
                }
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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlEspecialidad.SelectedIndexChanged += new System.EventHandler(this.ddlEspecialidad_SelectedIndexChanged);
            this.lnkBuscar.Click += new System.EventHandler(this.lnkBuscar_Click);
            this.dtgPrestadores.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgPrestadores_PageIndexChanged);
            this.dtgPrestadores.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgPrestadores_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void lnkBuscar_Click(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindPrestadores();
        }

        private void dtgPrestadores_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "seleccionar")
            {
                LinkButton lnkNombre = (LinkButton)e.Item.FindControl("lnkNombre");

                
                string script = "<script language='javascript'>DevolverPrestadorTipoServicio('" + HttpUtility.ParseQueryString(lnkNombre.Text) + "'," + Convert.ToInt32(e.Item.Cells[0].Text).ToString() + ")</script>";


                 //Inicio 12/01/10 MAHG Se verifica si la solicitud es Asíncrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", script, false);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Mensaje", script);
                }

            }

        }

        private void dtgPrestadores_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgPrestadores.CurrentPageIndex = e.NewPageIndex;
            this.FindPrestadores();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindPrestadores();
        }

        /// <summary>
        /// Evento, carga evento para seleccion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgPrestadores_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                LinkButton lnkNombre = (LinkButton)e.Item.FindControl("lnkNombre");
                lnkNombre.Attributes.Add("onClick", "javascript:DevolverPrestadorTipoServicio('" + HttpUtility.ParseQueryString(rowItem["NombrePrestador"].ToString()) + "'," + Convert.ToInt32(rowItem["IdPrestador"]).ToString() + ");closeLayer('BuscarPrestadorTipoServicio');");
            }

        }




        #endregion







    }
}
    