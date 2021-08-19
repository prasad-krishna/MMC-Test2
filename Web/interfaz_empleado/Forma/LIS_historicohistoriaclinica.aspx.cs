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
    /// Lista la historia clínica completa
    /// </summary>
    public partial class LIS_historicohistoriaclinica : PB_PaginaBase
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
                    this.FindHistoria();
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

        public void LoadControls()
        {
            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 

            foreach(DataListItem item in dtlConsultas.Items)
            {
                ((Label)item.FindControl("txtPeso")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtTalla")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtIMC")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtTension")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtFrecuenciaCardiaca")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtFrecuenciaRespiratoria")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtTemperatura")).Attributes.Add("ReadOnly", "ReadOnly");
                ((Label)item.FindControl("txtPerimetroAbdominal")).Attributes.Add("ReadOnly", "ReadOnly");
            }

          

            //Fin MAHG 12/01/10
        }

        /// <summary>
        /// Métoco, realiza la búsqueda de la historia clínica
        /// </summary>
        public void FindHistoria()
        {
            Consulta objConsulta = new Consulta();
            if (Request.QueryString["empleado"] != null && Request.QueryString["empleado"] == "s")
            {
                this.dtlConsultas.DataSource = objConsulta.ConsultHistoriaClinica(0, Convert.ToInt32(Request.QueryString["beneficiario_id"])).Tables[0];
                this.dtlConsultas1.DataSource = objConsulta.ConsultHistoriaClinica(0, Convert.ToInt32(Request.QueryString["beneficiario_id"])).Tables[1];
                this.dtlConsultas2.DataSource = objConsulta.ConsultHistoriaClinica(0, Convert.ToInt32(Request.QueryString["beneficiario_id"])).Tables[2];
            }
            else
            {
                this.dtlConsultas.DataSource = objConsulta.ConsultHistoriaClinica(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0).Tables[0];
                this.dtlConsultas1.DataSource = objConsulta.ConsultHistoriaClinica(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0).Tables[1];
                this.dtlConsultas2.DataSource = objConsulta.ConsultHistoriaClinica(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0).Tables[2];
            }
            this.dtlConsultas.DataBind();
            this.dtlConsultas1.DataBind();
            this.dtlConsultas2.DataBind();
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
            this.dtlConsultas.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlConsultas_ItemDataBound);
            this.dtlConsultas1.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlConsultas1_ItemDataBound);
            this.dtlConsultas2.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlConsultas2_ItemDataBound);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCerrar_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script>top.close();</script>");
        }

        /// <summary>
        /// Evento, Realiza la carga de las tablas que deben desplegarse dependiendo del histórico que se quiera desplegar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtlConsultas_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (Request.QueryString["Modulo"] == "todos")
                {
                    HtmlTable tblDatosConsulta = (HtmlTable)e.Item.FindControl("tblDatosConsulta");
                    tblDatosConsulta.Style["display"] = "";
                }

                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "antecedentes")
                {
                    HtmlTable tblAntecendentes = (HtmlTable)e.Item.FindControl("tblAntecendentes");
                    tblAntecendentes.Style["display"] = "";
                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "revision")
                {
                    HtmlTable tblRevisionSistemas = (HtmlTable)e.Item.FindControl("tblRevisionSistemas");
                    tblRevisionSistemas.Style["display"] = "";
                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "examen")
                {
                    HtmlTable tblExamenFisico = (HtmlTable)e.Item.FindControl("tblExamenFisico");
                    tblExamenFisico.Style["display"] = "";
                }
                //John Portela 16/03/2010 
                if (Session["Company"] != null)
                {
                    EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                    int numIdEmpresa = int.Parse(Session["Company"].ToString());
                    if (numIdEmpresa != 0)
                    {
                        objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                        objEmpresaDivisiones.GetEmpresaDivisiones();

                        //PRUEBAS BIOMÉTRICAS
                        if (objEmpresaDivisiones.DivColesterolGlicemia && objEmpresaDivisiones.DivExamenesLaboratorio
                            && objEmpresaDivisiones.DivMujer && objEmpresaDivisiones.DivAudiometria)
                        {
                            HtmlTable tblPruebasBiometricas = (HtmlTable)e.Item.FindControl("tblPruebasBiometricas");
                            tblPruebasBiometricas.Style["display"] = "";
                        }
                        //ESTILO DE VIDA
                        if (objEmpresaDivisiones.DivWellness)
                        {
                            HtmlTable tblWellness = (HtmlTable)e.Item.FindControl("tblWellness");
                            tblWellness.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivHabitoFumar)
                        {
                            HtmlTable tblHabitoFumar = (HtmlTable)e.Item.FindControl("tblHabitoFumar");
                            tblHabitoFumar.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivConsumoAlcohol)
                        {
                            HtmlTable tblConsumoAlcohol = (HtmlTable)e.Item.FindControl("tblConsumoAlcohol");
                            tblConsumoAlcohol.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivVacunacion)
                        {
                            HtmlTable tblVacunacion = (HtmlTable)e.Item.FindControl("tblVacunacion");
                            tblVacunacion.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivPerimetroAbdominal)
                        {
                            HtmlTableCell tdlblPerimetroAbdominal = (HtmlTableCell)e.Item.FindControl("tdlblPerimetroAbdominal");
                            HtmlTableCell tdPerimetroAbdominal = (HtmlTableCell)e.Item.FindControl("tdPerimetroAbdominal");
                            tdlblPerimetroAbdominal.Style["display"] = "";
                            tdPerimetroAbdominal.Style["display"] = "";
                        }

                        Label txtTension = (Label)e.Item.FindControl("txtTension");
                        if (objEmpresaDivisiones.DivDiastolicaSisTolica)                        
                            txtTension.Text = txtTension.Text.Replace("--", "Media:");
                        else
                            txtTension.Text = txtTension.Text.Replace("--", "");


                    }
                }

            }
        }
            /// <summary>
        /// Evento, Realiza la carga de las tablas que deben desplegarse dependiendo del histórico que se quiera desplegar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtlConsultas1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                
                //John Portela 16/03/2010 
                if (Session["Company"] != null)
                {
                    EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                    int numIdEmpresa = int.Parse(Session["Company"].ToString());
                    if (numIdEmpresa != 0)
                    {
                        objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                        objEmpresaDivisiones.GetEmpresaDivisiones();

                        if (objEmpresaDivisiones.DivSedentarismo)
                        {
                            HtmlTable tblSedentarismo = (HtmlTable)e.Item.FindControl("tblSedentarismo");
                            tblSedentarismo.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivSaludOral)
                        {
                            HtmlTable tblSaludOral = (HtmlTable)e.Item.FindControl("tblSaludOral");
                            tblSaludOral.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivEstres)
                        {
                            HtmlTable tblEstres = (HtmlTable)e.Item.FindControl("tblEstres");
                            tblEstres.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivEmocional)
                        {
                            HtmlTable tblEmocional = (HtmlTable)e.Item.FindControl("tblEmocional");
                            tblEmocional.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivAccidentalidad)
                        {
                            HtmlTable tblAccidentalidad = (HtmlTable)e.Item.FindControl("tblAccidentalidad");
                            tblAccidentalidad.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivEstadoSalud)
                        {
                            HtmlTable tblEstadoSalud = (HtmlTable)e.Item.FindControl("tblEstadoSalud");
                            tblEstadoSalud.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivNutricion)
                        {
                            HtmlTable tblNutricion = (HtmlTable)e.Item.FindControl("tblNutricion");
                            tblNutricion.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivAntecedentesAusentismo)
                        {
                            HtmlTable tblAntecedentesAusentismo = (HtmlTable)e.Item.FindControl("tblAntecedentesAusentismo");
                            tblAntecedentesAusentismo.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivRecomendaciones)
                        {
                            HtmlTable tblRecomendaciones = (HtmlTable)e.Item.FindControl("tblRecomendaciones");
                            tblRecomendaciones.Style["display"] = "";
                       }

                    }
                }

            }


        }
        /// <summary>
        /// Evento, Realiza la carga de las tablas que deben desplegarse dependiendo del histórico que se quiera desplegar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtlConsultas2_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                //John Portela 11/05/2010 
                if (Session["Company"] != null)
                {
                    EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                    int numIdEmpresa = int.Parse(Session["Company"].ToString());
                    if (numIdEmpresa != 0)
                    {
                        objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                        objEmpresaDivisiones.GetEmpresaDivisiones();

                       
                        if (objEmpresaDivisiones.DivNutricion)
                        {
                            HtmlTable tblNutricion = (HtmlTable)e.Item.FindControl("tblRutinaDiaria");
                            tblNutricion.Style["display"] = "";
                        }
                       

                    }
                }

            }


        }


        #endregion
    }
}
