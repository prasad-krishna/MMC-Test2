/*
HC, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer LLC.
HC is proprietary to Mercer LLC trade secret information. The
documentation and all related HC materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer LLC.
*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPA;
using Mercer.Medicines.Logic;
using System.Data;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Permite guardar el registro del aviso de privacidad    
    /// </summary>
    /// <remarks>Autor: Ricardo Silva
    /// Fecha de creación: 05/10/2011
    /// </remarks>
    public partial class AE_cambiarempresa : PB_PaginaBase
    {
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
                        this.ddlEmpresa.DataSource = objEmpresaUsers.GetEmpresasUser();

                    }// Fin 120710
                    this.ddlEmpresa.DataBind();
                    this.ddlEmpresa.Items.Insert(0, new ListItem("--Empresa--", "0"));

                    this.ddlEmpresa.SelectedValue =  Session["Company"].ToString();
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
            this.btnAceptar.Click += new System.EventHandler(this.btnIngresar_Click);

        }
        #endregion

        protected void btnIngresar_Click(object sender, System.EventArgs e)
        {
            try
            {
                /*
                 I. GAMM. 20200206 Cargamos variable de session con los permisos asignados al usuario.
                 */
                if (Session["dtPadres"] == null)
                {
                    DataTable dtPadres;
                    Permissions objMenu = new Permissions();

                    objMenu.IdPermissionType = Permissions.EnumPermissionsTypes.Menu;
                    objMenu.Parent = true;

                    dtPadres = objMenu.ConsultPermissionsUserGeneral((int)Session["IdUser"]).Tables[0];

                    Session.Add("dtPadres", dtPadres);
                }
                /*
                 F. GAMM. 20200206 Cargamos variable de session con los permisos asignados al usuario.
                 */

                Session["Company"] = Convert.ToInt32(this.ddlEmpresa.SelectedValue);
                Session["Administrador"] = "S";

                this.DisplayMessage("El cambio de empresa se realizó correctamente");
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
    }
}
