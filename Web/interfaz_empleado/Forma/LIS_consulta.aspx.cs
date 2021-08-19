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
    /// Realiza la búsqueda de consultas
    /// </summary>
    public partial class LIS_consulta : PB_PaginaBase
    {


        #region Atributos


        #endregion

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
                    this.LoadControls();

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
        /// Método, carga los controles iniciales
        /// </summary>
        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly 
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            this.FillList("Prestadores", "Prestador", Convert.ToInt32(Session["Company"]), this.ddlMedico, "--Todos--");

        }

        /// <summary>
        /// Método, Realiza la búsqueda de solicitudes
        /// </summary>
        public void FindConsultas()
        {
            object dateFrom = null;
            object dateUntil = null;
            int idUsuario = 0;

            Consulta objConsulta = new Consulta();
            
            objConsulta.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.txtNoConsulta.Text != string.Empty)
                objConsulta.ConsecutivoNombre = this.txtNoConsulta.Text;
            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
            idUsuario = Convert.ToInt32(this.ddlMedico.SelectedValue);

            if (this.txtIdEmpleado.Text != string.Empty)
            {
                SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                objEmpleado.Identificacion = this.txtIdEmpleado.Text;
                objEmpleado.Empresa_id = Convert.ToInt32(Session["Company"]);
                objEmpleado.GetSIC_EMPLEADOByIdentificacion();
                if (objEmpleado.Id_empleado == 0)
                    objConsulta.Id_empleado = -1;
                else
                    objConsulta.Id_empleado = objEmpleado.Id_empleado;
            }
            if (this.txtIdBeneficiario.Text != string.Empty)
            {
                SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                objBeneficiario.Identificacion = this.txtIdBeneficiario.Text;
                objBeneficiario.GetSIC_BENEFICIARIOByIdentificacion(Convert.ToInt32(Session["Company"]));
                if (objBeneficiario.Beneficiario_id == 0)
                    objConsulta.Beneficiario_id = -1;
                else
                    objConsulta.Beneficiario_id = objBeneficiario.Beneficiario_id;
            }


            this.dtgConsultas.DataSource = objConsulta.ConsultConsultaBusqueda(dateFrom, dateUntil, idUsuario);
            this.dtgConsultas.DataBind();
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
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtgConsultas.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgConsultas_PageIndexChanged);
            this.dtgConsultas.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgConsultas_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        /// <summary>
        /// Evento, realiza la búsqueda de la solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dtgConsultas.CurrentPageIndex = 0;
                this.FindConsultas();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, realiza la paginación en la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgConsultas_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgConsultas.CurrentPageIndex = e.NewPageIndex;
            this.FindConsultas();
        }

        /// <summary>
        /// Evento, realiza la carga de datos en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgConsultas_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                    Label lblEmpleado = (Label)e.Item.FindControl("lblEmpleado");
                    Label lblPaciente = (Label)e.Item.FindControl("lblPaciente");
                    HyperLink hplName = (HyperLink)e.Item.FindControl("hplName");

                    objEmpleado.Id_empleado = Convert.ToInt32(e.Item.Cells[1].Text);
                    objEmpleado.GetSIC_EMPLEADO();
                    lblEmpleado.Text = objEmpleado.Nombre_completo;

                    if (e.Item.Cells[2].Text == string.Empty || e.Item.Cells[2].Text == "&nbsp;")
                    {
                        lblPaciente.Text = objEmpleado.Nombre_completo;
                    }
                    else
                    {
                        SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                        DataTable dtBeneficiarios;
                        objBeneficiario.Opcion = 2;
                        objBeneficiario.Beneficiario_id = Convert.ToInt32(e.Item.Cells[2].Text);
                        dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                        lblPaciente.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        #endregion
    }
}
