using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Genera el reporte de morbilidad
    /// </summary>
    public partial class REP_morbilidad : PB_PaginaBase
    {
        #region Inicialización

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }


        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);               

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
            EmpresaUsers objEmpresaUsers = new EmpresaUsers();
            objEmpresaUsers.IdUser = (int)Session["IdUser"];

            //Carga los corporativos
            this.chkCorporativo.DataTextField = "nombre";
            this.chkCorporativo.DataValueField = "corporativo_id";
            DataTable dtCorporativos = objEmpresaUsers.ListCorporativosUser().Tables[0];
            this.chkCorporativo.DataSource = dtCorporativos;
            this.chkCorporativo.DataBind();

            //Carga las empresas del usuario
            this.chkEmpresas.DataTextField = "nombre";
            this.chkEmpresas.DataValueField = "empresa_id";
            DataTable dtEmpresas = objEmpresaUsers.GetEmpresasUser().Tables[0];
            this.chkEmpresas.DataSource = dtEmpresas;
            this.chkEmpresas.DataBind();

            //Carga todas las sedes 
            this.chkSedes.DataTextField = "nombreSede";
            this.chkSedes.DataValueField = "sede_id";
            DataTable dtSedes = objEmpresaUsers.GetSedeEmpresa().Tables[0];
            this.chkSedes.DataSource = dtSedes;
            this.chkSedes.DataBind();

            //Carga todos los usuarios
            this.chkUsuarios.DataTextField = "NameUser";
            this.chkUsuarios.DataValueField = "idusuario";
            DataTable dtUsuarios = objEmpresaUsers.GetSedeUsuarios().Tables[0];
            this.chkUsuarios.DataSource = dtUsuarios;
            this.chkUsuarios.DataBind();


            //Carga todos medicos
            this.chkMedicos.DataTextField = "NombrePrestador";
            this.chkMedicos.DataValueField = "IdPrestador";
            DataTable dtMedicos = objEmpresaUsers.GetMedicos().Tables[0];
            this.chkMedicos.DataSource = dtMedicos;
            this.chkMedicos.DataBind();


            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");     
            
        }

        /// <summary>
        /// Método, exporta la grilla a Excel
        /// </summary>
        public void exportReport()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xls");
            Response.Charset = "";
            Response.ContentEncoding = System.Text.UTF7Encoding.Default;
            this.EnableViewState = false;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            this.gvReporte.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        /// <summary>
        /// Método, recarga las empresas de los corporativos
        /// </summary>
        public void RecargarEmpresas()
        {
            string corporativos = "";
            EmpresaUsers objEmpresaUsers = new EmpresaUsers();
            objEmpresaUsers.IdUser = (int)Session["IdUser"];

            foreach (ListItem item in this.chkCorporativo.Items)
            {
                if (item.Selected)
                    corporativos += item.Value + ",";
            }

            //Carga las empresas con filtro
            this.chkEmpresas.DataTextField = "nombre";
            this.chkEmpresas.DataValueField = "empresa_id";
            DataTable dtEmpresas = objEmpresaUsers.GetEmpresasCorporativo(corporativos).Tables[0];
            this.chkEmpresas.DataSource = dtEmpresas;
            this.chkEmpresas.DataBind();

            if (corporativos != "")
            {
                foreach (ListItem item in this.chkEmpresas.Items)
                {
                    item.Selected = true;
                }
            }

            this.RecargarFiltros();
        }

        /// <summary>
        /// Método, recarga los filtros para el reporte
        /// </summary>
        public void RecargarFiltros()
        {
            string empresas = "";
            EmpresaUsers objEmpresaUsers = new EmpresaUsers();
            objEmpresaUsers.IdUser = (int)Session["IdUser"];

            foreach (ListItem item in this.chkEmpresas.Items)
            {
                if (item.Selected)
                    empresas += item.Value + ",";
            }

            //Carga las sedes con filtro
            this.chkSedes.DataTextField = "nombreSede";
            this.chkSedes.DataValueField = "sede_id";
            DataTable dtSedes = objEmpresaUsers.GetSedeEmpresa(empresas).Tables[0];
            this.chkSedes.DataSource = dtSedes;
            this.chkSedes.DataBind();

            //Carga los usuarios con filtro
            this.chkUsuarios.DataTextField = "NameUser";
            this.chkUsuarios.DataValueField = "idusuario";
            DataTable dtUsuarios = objEmpresaUsers.GetSedeUsuarios(empresas).Tables[0];
            this.chkUsuarios.DataSource = dtUsuarios;
            this.chkUsuarios.DataBind();

            //Carga los medicos con filtro
            this.chkMedicos.DataTextField = "NombrePrestador";
            this.chkMedicos.DataValueField = "IdPrestador";
            DataTable dtMedicos = objEmpresaUsers.GetMedicos(empresas).Tables[0];
            this.chkMedicos.DataSource = dtMedicos;
            this.chkMedicos.DataBind();

        }

        #endregion

        #region Eventos

        /// <summary>
        /// Método, realiza el llamado a la consulta para generación del reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            object dateFrom = null;
            object dateUntil = null;
            string empresas = "";
            string sedes = "";
            string usuarios = "";
            string medicos = "";
            Consulta objConsulta = new Consulta();
   
            if (this.txtFechaInicio.Text.Trim() != string.Empty)
            {
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
            }
            if (this.txtFechaFin.Text.Trim() != string.Empty)
            {
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
            }

            foreach (ListItem item in this.chkEmpresas.Items)
            {
                if (item.Selected)
                    empresas += item.Value + ",";
            }

            foreach (ListItem itemSede in this.chkSedes.Items)
            {
                if (itemSede.Selected)
                    sedes += itemSede.Value + ",";
            }

            foreach (ListItem itemUsuarios in this.chkUsuarios.Items)
            {
                if (itemUsuarios.Selected)
                    usuarios += itemUsuarios.Value + ",";
            }

            foreach (ListItem itemMedicos in this.chkMedicos.Items)
            {
                if (itemMedicos.Selected)
                    medicos += itemMedicos.Value + ",";
            }

            gvReporte.DataSource = objConsulta.ListConsultasMorbilidad(dateFrom, dateUntil, empresas, sedes, usuarios, medicos);
            gvReporte.DataBind();


        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            this.exportReport();
        }


        protected void imbExportar_Click(object sender, ImageClickEventArgs e)
        {
            this.exportReport();
        }

        protected void chkEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarFiltros();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            RecargarFiltros();
        }

        protected void chkCorporativo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarEmpresas();
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

        }
        #endregion       

     

    }
}
