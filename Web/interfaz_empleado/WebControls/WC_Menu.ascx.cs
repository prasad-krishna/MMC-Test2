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
    ///	Web Control que contiene el menú
    /// </summary>
    public partial class WC_Menu : WC_Base
    {

        #region Attributes

        

        #endregion

        #region Initializing

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.loadMenu();

                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.dtlMenu.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlMenu_ItemDataBound);

        }
        #endregion

        #region Methods

        /// <summary>
        /// Método, consulta el menú en la base de datos
        /// </summary>
        public void loadMenu()
        {
            DataTable dtHijos;
            DataTable dtPadres;
            Permissions objMenu = new Permissions();

            objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
            objMenu.Parent = true;

            dtPadres = objMenu.ConsultPermissionsUser((int)Session["IdUser"]).Tables[0];

            for (int i = 0; i < dtPadres.Rows.Count; i++)
            {
                if (Convert.IsDBNull(dtPadres.Rows[i]["IdPermissionParent"]))
                {
                    dtHijos = this.loadChilds(Convert.ToInt32(dtPadres.Rows[i]["IdPermission"]));
                    int j = 1;
                    foreach (DataRow row in dtHijos.Rows)
                    {
                        DataRow dr = dtPadres.NewRow();
                        dr.ItemArray = row.ItemArray;
                        dtPadres.Rows.InsertAt(dr, i + j);
                        j++;

                    }

                }
            }
            this.dtlMenu.DataSource = dtPadres;
            this.dtlMenu.DataBind();
        }

        /// <summary>
        /// Método, realiza la búsqueda de los hijos del menú
        /// </summary>
        /// <param name="p_idParent">Id del menú padre</param>
        /// <returns></returns>
        public DataTable loadChilds(int p_idParent)
        {
            DataTable dtHijos;
            Permissions objMenu = new Permissions();
            objMenu.IdPermissionParent = p_idParent;
            objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
            objMenu.Parent = false;
            dtHijos = objMenu.ConsultPermissionsUser((int)Session["IdUser"]).Tables[0];
            return dtHijos;

        }

        #endregion

        #region Eventos


        /// <summary>
        /// Evento, realiza la carga del menú dependiendo de sus características
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtlMenu_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                HyperLink hplMenu = (HyperLink)e.Item.FindControl("hplMenu");
                Label lblMenu = (Label)e.Item.FindControl("lblMenu");

                //Si el menú tiene URL se carga el link, si no tiene se carga un Label
                //RAM*
                if (!Convert.IsDBNull(rowItem["URL"]))
                {
                    hplMenu.Text = rowItem["NamePermission"].ToString();
                    hplMenu.Target = "ifrPageContent";
                    //if (hplMenu.Text == "Agenda")
                    //    hplMenu.NavigateUrl = "http://localhost:49800/UI/AgendaInicio.aspx";
                    //else if (hplMenu.Text == "Administración Agenda")
                    //    hplMenu.NavigateUrl = "http://localhost:49800/UI/Admin/AdminAgendaInicio.aspx";
                    //else if (hplMenu.Text == "Configuración Horario")
                    //    hplMenu.NavigateUrl = "http://localhost:49800/UI/Admin/AdminAgendaInicio.aspx";
                        
                   // else
                        hplMenu.NavigateUrl = "~/" + rowItem["URL"].ToString();
                    lblMenu.Visible = false;
                }
                else
                {
                    lblMenu.Text = rowItem["NamePermission"].ToString();
                    hplMenu.Visible = false;
                }

                //Si el menú es padre se carga el estilo de menú principal, si no se carga estilo de submenú
                if (Convert.IsDBNull(rowItem["IdPermissionParent"]))
                {
                    e.Item.CssClass = "tdSubmenu";
                    hplMenu.CssClass = "textSubmenu";
                    hplMenu.Attributes.Add("onMouseOut", "javascript:cambiarEstiloNormal(this);");
                    hplMenu.Attributes.Add("onMouseOver", "javascript:cambiarEstiloOver(this);");
                    hplMenu.Attributes.Add("onClick", "javascript:cambiarEstiloClick(this);");
                }
                else
                {
                    e.Item.CssClass = "tdSubmenu";
                    hplMenu.CssClass = "textSubmenu";
                    hplMenu.Attributes.Add("onMouseOut", "javascript:cambiarEstiloNormal(this);");
                    hplMenu.Attributes.Add("onMouseOver", "javascript:cambiarEstiloOver(this);");
                    hplMenu.Attributes.Add("onClick", "javascript:cambiarEstiloClick(this);");

                }

            }

        }


        #endregion
    }
}
