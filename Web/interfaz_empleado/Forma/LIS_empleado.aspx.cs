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
    /// Esta forma realiza la búsqueda de empleados por diferentes criterios
    /// </summary>
    public partial class LIS_empleado : PB_PaginaBase
    {

        #region Atributos

       

        #endregion

        #region Inicialización

        /// <summary>
        /// Inicialización de la página
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
                    this.loadControls();
                    if (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty)
                    {
                        this.hdnLiquidacionValue.Text = Request.QueryString["liquidacionConfirmacion"];
                    }
                    if (Request.QueryString["cambioPaciente"] != null && Request.QueryString["cambioPaciente"] != string.Empty)
                    {
                        this.dtgUsuarios.Columns[2].Visible = false;
                        this.dtgUsuarios.Columns[3].Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                string message = "";
                message = "<script language='javascript'>alert('Exception :" + ex.Message + "')</script>";


                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
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
            this.dtgUsuarios.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgUsuarios_ItemCommand);
            this.dtgUsuarios.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgUsuarios_PageIndexChanged);
            this.dtgUsuarios.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgUsuarios_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el cambio de página de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgUsuarios_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgUsuarios.CurrentPageIndex = e.NewPageIndex;
            this.getResultFinder();

        }

        /// <summary>
        /// Evento, Realiza el llamado a la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Bfinder_Click(object sender, System.EventArgs e)
        {
            this.dtgUsuarios.CurrentPageIndex = 0;
            this.getResultFinder();
        }


        /// <summary>
        /// Evento, carga evento para selección en grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgUsuarios_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.Cells[7].Text == "Retirado")
                {
                    e.Item.CssClass = "norItemsColor";
                    e.Item.Attributes.Remove("onmouseenter");
                    e.Item.Attributes.Remove("onmouseover");
                    e.Item.Attributes.Remove("onmouseout");
                }
            }
        }

        /// <summary>
        /// Evento, realiza cambio de paciente cuando se llama la forma desde la solicitud
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgUsuarios_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarPaciente")
                {
                    Response.Write("<script> window.opener.CambiarPaciente('" + e.Item.Cells[0].Text + "','" + e.Item.Cells[1].Text + "'); top.close();</script>");

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }


        }


        /// <summary>
        /// Evento, realiza la carga de la grilla cuando la ventana de privacidad se cierra
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void btnReload_Click(object sender, EventArgs e)
        {
            this.getResultFinder();
        }


        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la búsqueda y carga la grilla
        /// </summary>
        private void getResultFinder()
        {
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();

            string parentescos = "";
            DataTable dtParentescos;
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            EmpresaDatos objEmpresa = new EmpresaDatos();
            objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresa.GetEmpresaDatos();

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() != "0")
                {
                    parentescos += row["IdParentesco"].ToString() + ",";
                }
            }
            if (parentescos != "")
                parentescos = parentescos.Remove(parentescos.Length - 1, 1);

            objEmpleado.Identificacion = this.txtIdentificacion.Text.Trim();
            objEmpleado.Primer_nombre = this.txtNombre.Text.Trim();
            objEmpleado.Empresa_id = Convert.ToInt32(Session["Company"]);
            //RAM* se mostrara solo los activos
            objEmpleado.Estado = 1;
            DataTable dtEmpleados = objEmpleado.ConsultSIC_USUARIOS(parentescos, objEmpresa.IdPlanMedicamentos).Tables[0];
            this.dtgUsuarios.DataSource = dtEmpleados;
            this.dtgUsuarios.DataBind();

            if (this.dtgUsuarios.Items.Count > 0)
            {
                this.dtgUsuarios.Visible = true;
                this.Rcount.Text = dtEmpleados.Rows.Count.ToString();

            }
            else
            {
                this.dtgUsuarios.Visible = false;
                this.Rcount.Text = "No se encontraron registros";
            }
        }

        public void loadControls()
        {
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();
            Permissions objPermission;


            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

            if (!objEmpresaDatos.RealizaAutorizaciones)
            {
                this.trAutorizacion.Style["display"] = "none";
                this.hdnRealizaAutorizaciones.Text = "0";
            }
            if (!objEmpresaDatos.RealizaReembolsos)
            {
                this.trReembolso.Style["display"] = "none";
                this.hdnRealizaReembolsos.Text = "0";
            }
            if (!objEmpresaDatos.RealizaConsultas)
            {
                this.trConsulta.Style["display"] = "none";
                this.hdnRealizaConsultas.Text = "0";

            }
            else
            {
                objPermission = new Permissions();
                objPermission.IdUser = Convert.ToInt32(Session["IdUser"]);
                objPermission.IdPermission = Convert.ToInt32(Permissions.EnumPermissions.RealizaConsultas);
                objPermission.GetPermission();

                if (objPermission.IdPermissionType != 0)
                {
                    this.trConsulta.Style["display"] = "";
                    this.hdnRealizaConsultas.Text = "1";
                }
                else
                {   
                    this.trConsulta.Style["display"] = "none";
                    this.hdnRealizaConsultas.Text = "0";

                }
            }
            if (!objEmpresaDatos.RealizaCitasAgenda)
            {
                this.trNuevaCita.Style["display"] = "none";
                this.hdnRealizaCitas.Text = "0";
            }

            objPermission = new Permissions();
            objPermission.IdUser = Convert.ToInt32(Session["IdUser"]);
            objPermission.IdPermission = Convert.ToInt32(Permissions.EnumPermissions.ConsultarRetirados);
            objPermission.GetPermission();

            if (objPermission.IdPermissionType != 0)
                this.hdnPermisos.Text = "1";
            else
                this.hdnPermisos.Text = "0";
        }

        #endregion

        

        



    }
}
