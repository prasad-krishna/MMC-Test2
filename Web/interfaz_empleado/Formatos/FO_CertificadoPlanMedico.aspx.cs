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
    /// Genera certificado del plan de BP
    /// </summary>
    public partial class FO_CertificadoPlanMedico : PB_PaginaBase
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
                    if (Request.QueryString["employee_id"] != null)
                    {
                        this.LoadForm(Convert.ToInt32(Request.QueryString["employee_id"]));
                    }

                    if (Request.QueryString["exportar"] != null && Request.QueryString["exportar"] == "S")
                    {

                        string script = "";
                        script = "<script language='javascript'>window.print();</script>";

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
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentEncoding = System.Text.UTF7Encoding.Default;
                        Response.ContentType = "Content-type: application/vnd.ms-word";
                        Response.AddHeader("Content-type", "application/vnd.ms-word");
                        Response.AddHeader("Content-Disposition", "attachment; filename=certificado_" + this.lblNombre.Text + ".doc");

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Métodos

        public void LoadForm(int p_idEmpleado)
        {
            SIC_EMPLEADO objSicEmpleado = new SIC_EMPLEADO();
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

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
                else
                {
                    parentescos += "200,";
                }
            }
            if (parentescos != "")
                parentescos = parentescos.Remove(parentescos.Length - 1, 1);


            objSicEmpleado.Id_empleado = p_idEmpleado;
            objSicEmpleado.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objSicEmpleado.GetSIC_EMPLEADO();

            this.lblDocumento.Text = objSicEmpleado.Identificacion;
            string nombreCompleto = (objSicEmpleado.Primer_nombre != null && objSicEmpleado.Primer_nombre != string.Empty ? objSicEmpleado.Primer_nombre : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Segundo_nombre != null && objSicEmpleado.Segundo_nombre != string.Empty ? " " + objSicEmpleado.Segundo_nombre : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Apellido_paterno != null && objSicEmpleado.Apellido_paterno != string.Empty ? " " + objSicEmpleado.Apellido_paterno : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Apellido_materno != null && objSicEmpleado.Apellido_materno != string.Empty ? " " + objSicEmpleado.Apellido_materno : "");
            this.lblNombre.Text = nombreCompleto;
            this.lblTipoDocumento.Text = objSicEmpleado.Tipo_documento;
            this.lblFecha.Text = DateTime.Now.ToLongDateString();

            if (objSicEmpleado.fecha_ingreso_salud.ToShortDateString() != "01/01/0001")
                this.lblFechaIngreso.Text = objSicEmpleado.fecha_ingreso_salud.ToShortDateString();

            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            objBeneficiario.Id_empleado = p_idEmpleado;
            objBeneficiario.Opcion = 3;
            objBeneficiario.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objBeneficiario.Parentescos = parentescos;
            this.dtgDetalle.DataSource = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
            this.dtgDetalle.DataBind();


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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
