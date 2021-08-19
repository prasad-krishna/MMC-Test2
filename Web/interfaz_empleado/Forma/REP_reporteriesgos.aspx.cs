using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mercer.Medicines.Logic;


namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Genera el reporte de riesgos (accidentabilidad, alcohol, tabaquismo, cardiovascular, metabolico...)
    /// Autor: Adriana Diazgranados
    /// Fecha: 17 de mayo 2012
    /// </summary>
    public partial class REP_reporteriesgos : PB_PaginaBase
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

            //Carga todos medicos
            this.chkTiposConsulta.DataTextField = "NombrePrestador";
            this.chkTiposConsulta.DataValueField = "IdPrestador";

            this.FillList("TipoConsultas", "TipoConsulta", this.chkTiposConsulta);
            
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
            this.tblReporte.RenderControl(htmlWrite);
            string style = @"<style> .gridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; cellpadding: 1px }
	                    .norItemsFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8pt; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; COLOR: #000000; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse }
	                    .headerGridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 8pt; PADDING-BOTTOM: 3px; BORDER-LEFT: #000000 1px solid; COLOR: #ffffff; PADDING-TOP: 3px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse; BACKGROUND-COLOR: Gray; TEXT-ALIGN: center }
	                    .tdTituloSeccionReporte { FONT-WEIGHT: bold; FONT-SIZE: 14px; VERTICAL-ALIGN: bottom; COLOR: #003399; FONT-FAMILY: Verdana, Helvetica, sans-serif; HEIGHT: 30px } </style> ";
            Response.Write(style);
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
            string tiposconsulta = "";
            int beneficiarios = -1;
            int contGrilla = 1;
            string parametros = "reporte=1";
            Consulta objConsulta = new Consulta();

            if (this.txtFechaInicio.Text.Trim() != string.Empty)
            {
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
                parametros += "&dateFrom=" + this.txtFechaInicio.Text;
            }
            if (this.txtFechaFin.Text.Trim() != string.Empty)
            {
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
                parametros += "&dateUntil=" + this.txtFechaFin.Text;
            }

            foreach (ListItem item in this.chkEmpresas.Items)
            {
                if (item.Selected)
                    empresas += item.Value + ",";
            }
            if(empresas != "")
                parametros += "&empresas=" + empresas;

            foreach (ListItem item in this.chkSedes.Items)
            {
                if (item.Selected)
                    sedes += item.Value + ",";
            }
            if (sedes != "")
                parametros += "&sedes=" + sedes;

            foreach (ListItem itemUsuarios in this.chkUsuarios.Items)
            {
                if (itemUsuarios.Selected)
                    usuarios += itemUsuarios.Value + ",";
            }
            if (usuarios != "")
                parametros += "&usuarios=" + usuarios;

            foreach (ListItem itemMedicos in this.chkMedicos.Items)
            {
                if (itemMedicos.Selected)
                    medicos += itemMedicos.Value + ",";
            }
            if (medicos != "")
                parametros += "&medicos=" + medicos;

            foreach (ListItem item in this.chkTiposConsulta.Items)
            {
                if (item.Selected)
                    tiposconsulta += item.Value + ",";
            }
            if (tiposconsulta != "")
                parametros += "&tiposconsulta=" + tiposconsulta;

            beneficiarios = Convert.ToInt32(this.rblReporte.SelectedValue);

            parametros += "&beneficiarios=" + beneficiarios.ToString();

            DataSet dsReporte = objConsulta.ListConsultasRiesgos(dateFrom, dateUntil, empresas, sedes, usuarios, medicos, tiposconsulta, beneficiarios);

            //Exporta en cada GridView el resultado de cada tabla (select) del reporte
            foreach (DataTable dt in dsReporte.Tables)
            {
                ((GridView)this.Page.FindControl("gvReporte" + contGrilla.ToString())).DataSource = dt;
                ((GridView)this.Page.FindControl("gvReporte" + contGrilla.ToString())).DataBind();
                contGrilla++;
            }

            //Mostrar control de reporte detalles y guardar cadena de parámetros  
            ViewState["parametros"] = parametros;
            this.lnkExportarDetalle.Visible = true;
            this.imbExportarDetalle.Visible = true;

        }

        /// <summary>
        /// Evento, realiza el llamado para exportar a Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            this.exportReport();
        }

        /// <summary>
        /// Evento, realiza el llamado para exportar a Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbExportar_Click(object sender, ImageClickEventArgs e)
        {
            this.exportReport();
        }

        /// <summary>
        /// Evento, redirecciona para realizar la impresión del detalle del reporte (consulta a consulta)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbExportarDetalle_Click(object sender, ImageClickEventArgs e)
        {
            this.OpenWindow("../formatos/FO_ListadoReporteRiesgos.aspx?" + ViewState["parametros"].ToString(), 850, 750, 1);

        }

        /// <summary>
        /// Evento, redirecciona para realizar la impresión del detalle del reporte (consulta a consulta)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkExportarDetalle_Click(object sender, EventArgs e)
        {
            this.OpenWindow("../formatos/FO_ListadoReporteRiesgos.aspx?" + ViewState["parametros"].ToString(), 850, 750, 1);

        }

        /// <summary>
        /// Evento, realiza la carga de sedes a partir de las empresas seleccionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarFiltros();

        }

        /// <summary>
        /// Evento, realiza la carga de sedes a partir de las empresas seleccionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
