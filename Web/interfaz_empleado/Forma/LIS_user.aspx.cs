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
using System.Web.Security;


namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza la búsqueda del usuarios del sistema
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 15 de Octubre de 2008</remarks>
    public partial class LIS_user : PB_PaginaBase
    {
        #region Atributtes


        #endregion

        #region Initializing

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //Inicio MAHG 22/01/10
            //Se carga el load de la página base

            base.Page_Load(sender, e);

            //Fin MAHG 22/01/10

            try
            {
                if (!this.Page.IsPostBack)
                {


                }
               
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

            /* Proyecto: PenTest
             * GAMM
             * Fecha: 24/04/2019
             * Funcionalidad: Valida la sesión  para determinar si tiene el modulo asignado
             */
            if (Session["MostrarOpcionUsuarios"].ToString() == "False")
            {
                this.Session.Clear();
                this.Session.Abandon();
                FormsAuthentication.SignOut();//Elimina el vale de autenticación                        

                //Fin 22/01/10 MAHG
                string message = "";
                //message = "alert('Su sesión a caducado, ingrese nuevamente');window.top.close();";
                message = "alert('Su sesión a caducado, ingrese nuevamente');window.top.close();";

                //Inicio MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(base.Page) != null && System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "error", message);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", message, true);
                }

                //Response.End();
            }
        }

        #endregion

        #region Events

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
            this.dgdUsuario.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgdUsuario_PageIndexChanged);
            this.dgdUsuario.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgdUsuario_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado a la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dgdUsuario.CurrentPageIndex = 0;
                this.FindListUsers();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, carga evento para selección en grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgdUsuario_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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

        private void dgdUsuario_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dgdUsuario.CurrentPageIndex = e.NewPageIndex;
            this.FindListUsers();

        }

        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AE_user.aspx");
        }

        private void imbAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("AE_user.aspx");
        }

        #endregion

        #region Methods


        /// <summary>
        /// Método, realiza la búsqueda del log
        /// </summary>
        public void FindListUsers()
        {
            Users objUser = new Users();


            objUser.NameUser = this.txtNombre.Text.Trim();
            objUser.Login = this.txtLogin.Text.Trim();
            /* Proyecto: PenTest
             * GAMM
             * Fecha: 24/04/2019
             * Funcionalidad: Se setea la propiedad de IdUser para el filtrado de la información
             */
            objUser.IdUser =  Convert.ToInt32(Session["IdUser"].ToString());


            /* Proyecto: AMEX
             * Marco A. Herrera Gabriel
             * Fecha: 12/07/10
             * Funcionalidad: Se oculta el campo empresa_id para Méxio
             */

            if (int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"]) != 1)
            {
                if (Session["Administrador"].ToString() == string.Empty)
                {
                    objUser.empresa_id = Convert.ToInt32(Session["Company"]);
                }
            }

            this.dgdUsuario.DataSource = objUser.ConsultUsers();
            this.dgdUsuario.DataBind();
        }


        #endregion

    }
}
