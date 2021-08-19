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
    ///	Control que permite la búsqueda del diagnóstico
    /// </summary>
    public partial class WC_BuscarServicioProducto : WC_Base
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
            this.dtgServicioProducto.DataSource = new DataTable();
            this.dtgServicioProducto.DataBind();
            this.lblResultado.Text = "";
            if (!this.IsPostBack)
            {
                this.txtCodigo.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkBuscar.UniqueID + "',''); return false }");
                this.txtNombre.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkBuscar.UniqueID + "','') ; return false}");

                //Inicio 15/01/10 Marco A. Herrera G. MAHG 
                /*Se colocó el código en el codeBehind ya que se presentaron problemas con el 
                llenado de las etiquetas desde JavaScript.*/

                if (txtIdTipoServicio.Text == "17" || txtIdTipoServicio.Text == "18")
                {
                    lblNombre.Text = "Nombre";
                    lblPrincipioActivo.Text = "Principio Activo";

                }              
                else
                {
                    lblNombre.Text = "Código";
                    lblPrincipioActivo.Text = "Nombre";
                }
                //Fin 15/01/10 
            }

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la búsqueda de los diagnósticos
        /// </summary>
        public void FindServiciosProductos(bool page)
        {
            if (Convert.ToInt16(this.txtIdTipoServicio.Text) == Convert.ToInt16(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt16(this.txtIdTipoServicio.Text) == Convert.ToInt16(Servicios.EnumTiposServicio.Vacunas))
            {
                Permissions objPermission = new Permissions();
                objPermission.IdUser = Convert.ToInt32(Session["IdUser"]);
                objPermission.IdPermission = Convert.ToInt32(Permissions.EnumPermissions.AdicionarMedicamentos);
                objPermission.GetPermission();

                if (objPermission.IdPermissionType != 0)
                    this.lnkAdicionar.Visible = true;

                Medicamentos objMedicamentos = new Medicamentos();
                objMedicamentos.NombreComercial = this.txtCodigo.Text.Trim();
                objMedicamentos.PrincipioActivo = this.txtNombre.Text.Trim();
                objMedicamentos.empresa_id = Convert.ToInt32(Session["Company"]);
                objMedicamentos.IdTipoServicio = Convert.ToInt16(this.txtIdTipoServicio.Text);
                objMedicamentos.Activo = 1;
                DataTable dtServiciosProductos = objMedicamentos.ConsultMedicamentos().Tables[0];

                if (dtServiciosProductos.Rows.Count < 1)
                {
                    this.lblResultado.Text = "No se encuentran resultados";
                    this.dtgServicioProducto.DataSource = null;
                    this.dtgServicioProducto.DataBind();
                }
                else
                {
                    if (dtServiciosProductos.Rows.Count == 1)
                    {
                        string valor = "0";
                        if (!Convert.IsDBNull(dtServiciosProductos.Rows[0]["ValorConvenio"]) && dtServiciosProductos.Rows[0]["ValorConvenio"].ToString() != "0")
                            valor = string.Format("{0:0,0}", Convert.ToDecimal(dtServiciosProductos.Rows[0]["ValorConvenio"]).ToString());
                        string script = "<script language='javascript'>DevolverServicioProducto('" + dtServiciosProductos.Rows[0]["NombreCompleto"].ToString() + "'," + dtServiciosProductos.Rows[0]["IdServicioProducto"].ToString() + ",'" + valor + "')</script>";

                        //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje" , script, false);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Mensaje" + this.txtTemp.Text, script);
                        }
                        //Fin     

                       

                        this.dtgServicioProducto.DataSource = new DataTable();
                        this.dtgServicioProducto.DataBind();
                        this.txtCodigo.Text = "";
                        this.txtNombre.Text = "";
                    }
                    else
                    {
                        this.dtgServicioProducto.DataSource = dtServiciosProductos;
                        this.dtgServicioProducto.DataBind();
                    }
                }
            }
            else
            {
                Permissions objPermission = new Permissions();
                objPermission.IdUser = Convert.ToInt32(Session["IdUser"]);
                objPermission.IdPermission = Convert.ToInt32(Permissions.EnumPermissions.AdicionarServicios);
                objPermission.GetPermission();

                if (objPermission.IdPermissionType != 0)
                    this.lnkAdicionar.Visible = true;


                Servicios objServicio = new Servicios();
                objServicio.NombreServicio = this.txtNombre.Text.Trim();
                objServicio.CodigoServicio = this.txtCodigo.Text.Trim();
                objServicio.empresa_id = Convert.ToInt32(Session["Company"]);
                objServicio.IdTipoServicio = Convert.ToInt16(this.txtIdTipoServicio.Text);

                DataTable dtServiciosProductos = objServicio.ConsultServicios().Tables[0];

                if (dtServiciosProductos.Rows.Count < 1)
                {
                    this.lblResultado.Text = "No se encuentran resultados";
                    this.dtgServicioProducto.DataSource = null;
                    this.dtgServicioProducto.DataBind();
                }
                else
                {
                    if (dtServiciosProductos.Rows.Count == 1)
                    {
                        string valor = "0";
                        if (!Convert.IsDBNull(dtServiciosProductos.Rows[0]["ValorConvenio"]) && dtServiciosProductos.Rows[0]["ValorConvenio"].ToString() != "0")
                            valor = string.Format("{0:0,0}", Convert.ToDecimal(dtServiciosProductos.Rows[0]["ValorConvenio"]).ToString());
                        string script = "<script language='javascript'>DevolverServicioProducto('" + dtServiciosProductos.Rows[0]["NombreCompleto"].ToString() + "'," + dtServiciosProductos.Rows[0]["IdServicioProducto"].ToString() + ",'" + valor + "')</script>";

                        //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje" + this.txtTemp.Text, script, false);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Mensaje" + this.txtTemp.Text, script);
                        }
                        //Fin    



                        this.dtgServicioProducto.DataSource = new DataTable();
                        this.dtgServicioProducto.DataBind();
                        this.txtCodigo.Text = "";
                        this.txtNombre.Text = "";
                    }
                    else
                    {
                        this.dtgServicioProducto.DataSource = dtServiciosProductos;
                        this.dtgServicioProducto.DataBind();
                    }
                }
            }

        }

        /// <summary>
        /// Método, limpia los controles del web user control
        /// </summary>
        public void CleanControl()
        {
            this.dtgServicioProducto.DataBind();
            this.txtNombre.Text = "";
            this.txtCodigo.Text = "";
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
            this.lnkBuscar.Click += new System.EventHandler(this.lnkBuscar_Click);
            this.dtgServicioProducto.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgServicioProducto_PageIndexChanged);
            this.dtgServicioProducto.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgServicioProducto_ItemDataBound);
            this.lnkAdicionar.Click += new System.EventHandler(this.lnkAdicionar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void lnkBuscar_Click(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindServiciosProductos(false);
        }

        /// <summary>
        /// Evento, realiza paginación
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgServicioProducto_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgServicioProducto.CurrentPageIndex = e.NewPageIndex;
            this.FindServiciosProductos(true);
        }

        /// <summary>
        /// Evento, carga evento de selección
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgServicioProducto_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                LinkButton lnkServicioProducto = (LinkButton)e.Item.FindControl("lnkServicioProducto");
                string valor = "0";
                if (rowItem["ValorConvenio"].ToString() != "0")
                    valor = string.Format("{0:0,0}", string.Format("{0:0,0}", Convert.ToDecimal(rowItem["ValorConvenio"].ToString())));
                lnkServicioProducto.Attributes.Add("onClick", "javascript:DevolverServicioProducto('" + rowItem["NombreCompleto"].ToString().Replace("'", "-") + "'," + rowItem["IdServicioProducto"].ToString().Replace("'", "-") + ",'" + rowItem["ValorConvenio"].ToString().Replace("'", "-") + "');closeLayer('BuscarServicioProducto');");
            }

        }

        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt16(this.txtIdTipoServicio.Text) == Convert.ToInt16(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt16(this.txtIdTipoServicio.Text) == Convert.ToInt16(Servicios.EnumTiposServicio.Vacunas))
            {
                string script = "";
                script = "<script language='javascript'>openPopUp('AE_medicamento.aspx',600,700);</script>";

                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "print", script, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "print", script);
                }
                //Fin     

                

            }
            else
            {
                string script = "";
                script = "<script language='javascript'>openPopUp('AE_servicio.aspx',690,600);</script>";

                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "print", script, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "print", script);
                }
                //Fin     

            }


        }

        #endregion




    }
}
