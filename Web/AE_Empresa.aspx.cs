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

namespace WebMedicamentos
{
    /// <summary>
    /// Pagina para selección de empresa para perfil administrador
    /// </summary>
    public partial class AE_Empresa : TPA.PB_PaginaBase
    {
        #region Attributes

      
        #endregion

        #region Initializing

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
                    SIC_EMPRESA objEmpresa = new SIC_EMPRESA();
                    

                    this.ddlEmpresa.DataTextField = "nombre";
                    this.ddlEmpresa.DataValueField = "empresa_id";
                    

                     /* Proyecto: AMEX
                     * Marco A. Herrera Gabriel
                     * Fecha: 12/07/10
                     * Funcionalidad: Se oculta el campo empresa_id para Méxio
                     * Inicio 120710
                     */
                    EmpresaUsers objEmpresaUsers = new EmpresaUsers();
                    objEmpresaUsers.IdUser = (int)Session["IdUser"];

                    if (int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"]) != 1)
                    {

                        this.ddlEmpresa.DataSource = objEmpresa.ConsultSIC_EMPRESA();
                    }
                    else
                    {
                        lblSistema.Text = "Bienvenido al Sistema HC";
                        this.ddlEmpresa.DataSource = objEmpresaUsers.GetEmpresasUser();
                    }// Fin 120710
                    this.ddlEmpresa.DataBind();
                    this.ddlEmpresa.Items.Insert(0, new ListItem("--Empresa--", "0"));
                }
            }
            catch (Exception ex)
            {
                string message = "";
                message = ex.Message.Replace("'", "");
                message = message.Replace("\r", "");
                message = message.Replace("\n", "");
                message = "<script language='javascript'>alert('Exception :" + message + "')</script>";


                //Inicio MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                

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
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);

        }
        #endregion

        protected void btnIngresar_Click(object sender, System.EventArgs e)
        {
            try
            {
                Session["Company"] = Convert.ToInt32(this.ddlEmpresa.SelectedValue);
                Session["Administrador"] = "S";
                Response.Redirect("interfaz_empleado/forma/Home.aspx");
            }
            catch (Exception ex)
            {
                string message = "";
                message = ex.Message.Replace("'", "");
                message = message.Replace("\r", "");
                message = message.Replace("\n", "");
                message = "<script language='javascript'>alert('Exception :" + message + "')</script>";

                //Inicio MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin 
            }


        }

        #endregion
    }
}
