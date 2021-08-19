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

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Lista los beneficiarios del empleado
    /// </summary>
    public partial class LIS_beneficiarios : PB_PaginaBase
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
                    this.loadControlUsr();
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
        /// Método, carga la información de los beneficiarios
        /// </summary>
        private void loadControlUsr()
        {
            string parentescos = "";
            DataTable dtParentescos;
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() != "0")
                {
                    parentescos += row["IdParentesco"].ToString() + ",";
                }
            }
            if (parentescos != "")
                parentescos = parentescos.Remove(parentescos.Length - 1, 1);

            EmpresaDatos objEmpresaDatos = new EmpresaDatos();

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            objBeneficiario.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objBeneficiario.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objBeneficiario.Opcion = 3;
            objBeneficiario.Parentescos = parentescos;
            this.dtgBeneficiarios.DataSource = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
            this.dtgBeneficiarios.DataBind();

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
            this.Load += new System.EventHandler(this.Page_Load);
            this.dtgBeneficiarios.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgBeneficiarios_ItemCommand);
            this.dtgBeneficiarios.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.Flist_ItemDataBound);

        }
        #endregion

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

        #endregion

        private void dtgBeneficiarios_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Historico")
                {
                    this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + e.Item.Cells[0].Text, 800, 700);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }




    }
}
