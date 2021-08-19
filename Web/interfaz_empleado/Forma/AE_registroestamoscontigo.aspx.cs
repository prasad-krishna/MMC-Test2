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
using System.Configuration;

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Inserta o modifica una consulta de tipo "estamos contigo"
    /// Autor: Ricardo Silva
    /// Fecha: 05/07/2012
    /// </summary>
    public partial class registroestamoscontigo : PB_PaginaBase
    {
        #region Inicialización
        bool _esNutriologo, _esMedico, _esTodo = false;
        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Inicio Ricardo silva 05/07/12
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                if (!this.Page.IsPostBack)
                {
                    Response.Write("<script>window.parent.scrollTo(0,0);</script>");

                    this.LoadControls();

                    if (Request.QueryString["IdConsulta"] != null)
                    {
                        Consulta objConsulta = new Consulta();
                        objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        if (objConsulta.ExisteConsultaEstamosContigo())
                            this.LoadFormConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                    }
                }

                string idConsulta = Session["idTipoConsulta"].ToString();

                string[] medico = new string[] { "1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17" };
                ArrayList ArrayMedico = new ArrayList();
                ArrayMedico.AddRange(medico);

                string[] nutriologo = new string[] { "15", "16" };
                ArrayList ArrayNutriologo = new ArrayList();
                ArrayNutriologo.AddRange(nutriologo);

                string[] todo = new string[] { "3", "4" };
                ArrayList ArrayTodo = new ArrayList();
                ArrayTodo.AddRange(todo);

                if (ArrayMedico.Contains(idConsulta))
                    _esMedico = true;
                if (ArrayNutriologo.Contains(idConsulta))
                    _esNutriologo = true;
                if (ArrayTodo.Contains(idConsulta))
                    _esTodo = true;

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método, Carga los controles iniciales
        /// </summary>
        public void LoadControls()
        {
            //Carga de controles 25/02/2010
            this.FillList(this.ddlValorPresionArterial30dias, "PreguntaRespuesta", "--Seleccione--", 515);
            this.FillList(this.ddlValorGlucosa30dias, "PreguntaRespuesta", "--Seleccione--", 520);
            this.FillList(this.ddlValorColesterolTotal30Dias, "PreguntaRespuesta", "--Seleccione--", 525);
            this.FillList(this.ddlValorColesterolHDL30Dias, "PreguntaRespuesta", "--Seleccione--", 528);
            this.FillList(this.ddlValorColesterolLDL30Dias, "PreguntaRespuesta", "--Seleccione--", 532);
            this.FillList(this.ddlValorTrigliceridos30DiasHombres, "PreguntaRespuesta", "--Seleccione--", 535);
            this.FillList(this.ddlValorTrigliceridos30DiasMujeres, "PreguntaRespuesta", "--Seleccione--", 539);
            this.FillList(this.ddlIndicacionesMedico, "PreguntaRespuesta", "--Seleccione--", 543);
            this.FillList(this.cblComplicacionHAS, "PreguntaRespuesta", 547);
            this.FillList(this.cblComplicacionDM, "PreguntaRespuesta", 553);
            this.FillList(this.cblComplicacionDislipidemias, "PreguntaRespuesta", 559);
            this.FillList(this.cblComplicacionTrigliceridos, "PreguntaRespuesta", 565);
        }

        
        /// <summary>
        /// Método, borra y oculta los controles
        /// </summary>
        public void OcultarBorrarControles()
        {
            this.cblComplicacionHAS.Visible = false;
            this.cblComplicacionDM.Visible = false;
            this.cblComplicacionDislipidemias.Visible = false;
            this.cblComplicacionTrigliceridos.Visible = false;
            this.rblVisitaNutriologo.SelectedIndex = -1;
            this.rblApegadoDieta.SelectedIndex = -1;
            this.txtNoVisitaNutriologo.Text = string.Empty;
            this.rblMedicoTratamiento.SelectedIndex = -1;
            this.ddlIndicacionesMedico.SelectedValue = "0";
            this.txtNoCumpleTratamientos.Text = string.Empty;
            this.txtMedicamentos.Text = string.Empty;
            this.txtOtraComplicacion.Text = string.Empty;
            this.txtRecomendaciones.Text = string.Empty;
            this.txtFechaSiguienteCita.Text = string.Empty;
            this.rfvFechaSiguienteCita.Enabled = false;

            this.cblComplicacionHAS.ClearSelection();
            this.cblComplicacionDM.ClearSelection();
            this.cblComplicacionDislipidemias.ClearSelection();
            this.cblComplicacionTrigliceridos.ClearSelection();
        }

        /// <summary>
        /// Método, modifica una consulta
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void UpdateEstamosContigo(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            this.LoadObjectConsulta(objConsulta);
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.UpdateConsultaEstamosContigo();
        }

        /// <summary>
        /// Método, inserta una nueva consulta estamos contigo 
        /// </summary>
        /// <returns></returns>
        public long InsertConsultaEstamosContigo(long p_idConsulta)
        {
            long idConsulta;
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            this.LoadObjectConsulta(objConsulta);
            idConsulta = objConsulta.InsertConsultaEstamosContigo();
            return idConsulta;
        }

        /// <summary>
        /// Método, inserta una nueva consulta estamos contigo general
        /// </summary>
        /// <returns></returns>
        public long InsertConsultaEstamosContigoGeneral(long p_idConsulta)
        {
            long idConsulta = 0;
            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            objConsultaEstamosContigoGeneral.IdConsulta = p_idConsulta;
            bool crearObjeto = this.LoadObjectConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral);
            if (crearObjeto)
            {
                idConsulta = objConsultaEstamosContigoGeneral.InsertConsultaEstamosContigoGenerales();
            }
            return idConsulta;
        }

        /// <summary>
        /// Método, modifica una consulta
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void UpdateConsultaEstamosContigoGeneral(long p_idConsulta)
        {
            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            this.LoadObjectConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral);
            objConsultaEstamosContigoGeneral.IdConsulta = p_idConsulta;
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            objConsultaEstamosContigoGeneral.UpdateConsultaEstamosContigoGenerales();
        }

        /// <summary>
        /// Método, Carga una consulta en el forma
        /// </summary>
        public void LoadFormConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();
            objConsulta.GetConsultaEstamosContigo();
            ViewState["IdSolicitud"] = objConsulta.IdSolicitud;
            if (objConsulta.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objConsulta.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
            }

            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
            {
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = objConsulta.IdPrestador;
                objGeneral.GetGeneralTable();
            }

            if (objConsulta.ExisteConsultaEstamosContigo() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            {
                DisableTextBoxesRecursive(Page);
                this.btnCancelar.Visible = true;
                this.btnGuardar.Visible = true;
                this.cblComplicacionDislipidemias.Enabled = false;
                this.cblComplicacionDM.Enabled = false;
                this.cblComplicacionHAS.Enabled = false;
                this.cblComplicacionTrigliceridos.Enabled = false;
                this.rfvFechaSiguienteCita.Enabled = false;
                this.imgCalCita.Style["display"] = "none";
                this.imgCalColTotal.Style["display"] = "none";
                this.imgCalHAS.Style["display"] = "none";
                this.imgCalHDL.Style["display"] = "none";
                this.imgCalLDL.Style["display"] = "none";
                this.imgCalMD.Style["display"] = "none";
                this.imgCalTrigliceridos.Style["display"] = "none";
            }

            //HIPERTENSIÓN ARTERIAL SISTEMICA (HAS)

            if (objConsulta.PresionArterial30dias != -1)
            {
                this.rblPresionArterial30Dias.SelectedValue = objConsulta.PresionArterial30dias.ToString();
                if (rblPresionArterial30Dias.SelectedIndex != 1)
                {
                    divPresionArterial.Visible = true;
                }
            }

            txtFechaPresionArterial30dias.Text = Convert.ToString(objConsulta.FechaPresionArterial30dias);

            if (objConsulta.ValorPresionArterial30dias != 0)
                this.ddlValorPresionArterial30dias.SelectedValue = objConsulta.ValorPresionArterial30dias.ToString();

            //DIEBETES MELLITUS (DM)            

            if (objConsulta.Glucosa30dias != -1)
            {
                this.rblGlucosa30dias.SelectedValue = objConsulta.Glucosa30dias.ToString();
                if (rblGlucosa30dias.SelectedIndex != 1)
                {
                    divSiDiabetesMellitus.Visible = true;
                }
            }
            txtFechaGlucosa30dias.Text = Convert.ToString(objConsulta.FechaGlucosa30dias);

            if (objConsulta.ValorGlucosa30dias != 0)
                this.ddlValorGlucosa30dias.SelectedValue = objConsulta.ValorGlucosa30dias.ToString();


            //DISLIPIDEMIAS          

            //Colesterol total

            if (objConsulta.ColesterolTotal30Dias != -1)
            {
                this.rblColesterolTotal30Dias.SelectedValue = objConsulta.ColesterolTotal30Dias.ToString();
                if (rblColesterolTotal30Dias.SelectedIndex != 1)
                {
                    divSiColesterolTotal.Visible = true;
                }
            }

            txtFechaColesterolTotal30Dias.Text = Convert.ToString(objConsulta.FechaColesterolTotal30Dias);

            if (objConsulta.ValorColesterolTotal30Dias != 0)
                this.ddlValorColesterolTotal30Dias.SelectedValue = objConsulta.ValorColesterolTotal30Dias.ToString();

            //colesterol HDL

            if (objConsulta.ColesterolHDL30Dias != -1)
            {
                this.rblColesterolHDL30Dias.SelectedValue = objConsulta.ColesterolHDL30Dias.ToString();
                if (rblColesterolHDL30Dias.SelectedIndex != 1)
                {
                    divSiHDL.Visible = true;
                }
            }

            txtFechaColesterolHDL30Dias.Text = Convert.ToString(objConsulta.FechaColesterolHDL30Dias);

            if (objConsulta.ValorColesterolHDL30Dias != 0)
                this.ddlValorColesterolHDL30Dias.SelectedValue = objConsulta.ValorColesterolHDL30Dias.ToString();

            //Colesterol LDL

            if (objConsulta.ColesterolLDL30Dias != -1)
            {
                this.rblColesterolLDL30Dias.SelectedValue = objConsulta.ColesterolLDL30Dias.ToString();
                if (rblColesterolLDL30Dias.SelectedIndex != 1)
                {
                    divSiLDL.Visible = true;
                }
            }
            txtFechaColesterolLDL30Dias.Text = Convert.ToString(objConsulta.FechaColesterolLDL30Dias);

            if (objConsulta.ValorColesterolLDL30Dias != 0)
                this.ddlValorColesterolLDL30Dias.SelectedValue = objConsulta.ValorColesterolLDL30Dias.ToString();


            //TRIGLICERIDOS

            if (objConsulta.Trigliceridos30Dias != -1)
            {
                this.rdlTrigliceridos30Dias.SelectedValue = objConsulta.Trigliceridos30Dias.ToString();
                if (rdlTrigliceridos30Dias.SelectedIndex != 1)
                {
                    divSiTrigliceridos.Visible = true;
                }
            }
            txtFechaTrigliceridos30Dias.Text = Convert.ToString(objConsulta.FechaTrigliceridos30Dias);

            if (objConsulta.ValorTrigliceridos30DiasHombres != 0)
                this.ddlValorTrigliceridos30DiasHombres.SelectedValue = objConsulta.ValorTrigliceridos30DiasHombres.ToString();

            if (objConsulta.ValorTrigliceridos30DiasMujeres != 0)
                this.ddlValorTrigliceridos30DiasMujeres.SelectedValue = objConsulta.ValorTrigliceridos30DiasMujeres.ToString();

        }


        /// <summary>
        /// Método, coloca en enable los controles de manera recursiva
        /// </summary>
        /// <param name="root"></param>
        private void DisableTextBoxesRecursive(Control root)
        {

            if (root is TextBox)
            {
                ((TextBox)root).CssClass = "LabelNoModifyBottom";
                ((TextBox)root).ReadOnly = true;
                ((TextBox)root).TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
                ((TextBox)root).Attributes["onkeypress"] = "return;";
            }
            if (root is CheckBox)
                ((CheckBox)root).Enabled = false;

            if (root is RadioButton)
                ((RadioButton)root).Enabled = false;

            if (root is RadioButtonList)
                ((RadioButtonList)root).Enabled = false;

            if (root is DropDownList)
                ((DropDownList)root).Enabled = false;

            if (root is Button)
                ((Button)root).Visible = false;

            foreach (Control child in root.Controls)
            {
                DisableTextBoxesRecursive(child);
            }
        }

        /// <summary>
        /// Método, carga un objeto consulta con los datos de la forma
        /// </summary>
        public void LoadObjectConsulta(Consulta objConsulta)
        {
            string strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();

            //HIPERTENSIÓN ARTERIAL SISTEMICA (HAS)
            if (this.rblPresionArterial30Dias.SelectedIndex > -1)
            {
                objConsulta.PresionArterial30dias = Convert.ToInt32(this.rblPresionArterial30Dias.SelectedValue);
            }
            else
            {
                objConsulta.PresionArterial30dias = -1;
            }

            if (this.txtFechaPresionArterial30dias.Text != string.Empty)
            {
                objConsulta.FechaPresionArterial30dias = Convert.ToString(this.txtFechaPresionArterial30dias.Text);
            }

            if (this.ddlValorPresionArterial30dias.SelectedValue != "0" && this.ddlValorPresionArterial30dias.SelectedValue != string.Empty)
            {
                objConsulta.ValorPresionArterial30dias = int.Parse(ddlValorPresionArterial30dias.SelectedValue);
            }

            //DIEBETES MELLITUS (DM)

            if (this.rblGlucosa30dias.SelectedIndex > -1)
            {
                objConsulta.Glucosa30dias = Convert.ToInt32(this.rblGlucosa30dias.SelectedValue);
            }
            else
            {
                objConsulta.Glucosa30dias = -1;
            }

            if (this.txtFechaGlucosa30dias.Text != string.Empty)
            {
                objConsulta.FechaGlucosa30dias = Convert.ToString(this.txtFechaGlucosa30dias.Text);
            }

            if (this.ddlValorGlucosa30dias.SelectedValue != "0" && this.ddlValorGlucosa30dias.SelectedValue != string.Empty)
            {
                objConsulta.ValorGlucosa30dias = int.Parse(ddlValorGlucosa30dias.SelectedValue);
            }

            //DISLIPIDEMIAS
            //Colesterol total
            if (this.rblColesterolTotal30Dias.SelectedIndex > -1)
            {
                objConsulta.ColesterolTotal30Dias = Convert.ToInt32(this.rblColesterolTotal30Dias.SelectedValue);
            }
            else
            {
                objConsulta.ColesterolTotal30Dias = -1;
            }

            if (this.txtFechaColesterolTotal30Dias.Text != string.Empty)
            {
                objConsulta.FechaColesterolTotal30Dias = Convert.ToString(this.txtFechaColesterolTotal30Dias.Text);
            }

            if (this.ddlValorColesterolTotal30Dias.SelectedValue != "0" && this.ddlValorColesterolTotal30Dias.SelectedValue != string.Empty)
            {
                objConsulta.ValorColesterolTotal30Dias = int.Parse(ddlValorColesterolTotal30Dias.SelectedValue);
            }
            //colesterol HDL
            if (this.rblColesterolHDL30Dias.SelectedIndex > -1)
            {
                objConsulta.ColesterolHDL30Dias = Convert.ToInt32(this.rblColesterolHDL30Dias.SelectedValue);
            }
            else
            {
                objConsulta.ColesterolHDL30Dias = -1;
            }

            if (this.txtFechaColesterolHDL30Dias.Text != string.Empty)
            {
                objConsulta.FechaColesterolHDL30Dias = Convert.ToString(this.txtFechaColesterolHDL30Dias.Text);
            }

            if (this.ddlValorColesterolHDL30Dias.SelectedValue != "0" && this.ddlValorColesterolHDL30Dias.SelectedValue != string.Empty)
            {
                objConsulta.ValorColesterolHDL30Dias = int.Parse(ddlValorColesterolHDL30Dias.SelectedValue);
            }
            //Colesterol LDL
            if (this.rblColesterolLDL30Dias.SelectedIndex > -1)
            {
                objConsulta.ColesterolLDL30Dias = Convert.ToInt32(this.rblColesterolLDL30Dias.SelectedValue);
            }
            else
            {
                objConsulta.ColesterolLDL30Dias = -1;
            }

            if (this.txtFechaColesterolLDL30Dias.Text != string.Empty)
            {
                objConsulta.FechaColesterolLDL30Dias = Convert.ToString(this.txtFechaColesterolLDL30Dias.Text);
            }

            if (this.ddlValorColesterolLDL30Dias.SelectedValue != "0" && this.ddlValorColesterolLDL30Dias.SelectedValue != string.Empty)
            {
                objConsulta.ValorColesterolLDL30Dias = int.Parse(ddlValorColesterolLDL30Dias.SelectedValue);
            }
            //TRIGLICERIDOS

            if (this.rdlTrigliceridos30Dias.SelectedIndex > -1)
            {
                objConsulta.Trigliceridos30Dias = Convert.ToInt32(this.rdlTrigliceridos30Dias.SelectedValue);
            }
            else
            {
                objConsulta.Trigliceridos30Dias = -1;
            }

            if (this.txtFechaTrigliceridos30Dias.Text != string.Empty)
            {
                objConsulta.FechaTrigliceridos30Dias = Convert.ToString(this.txtFechaTrigliceridos30Dias.Text);
            }

            if (this.ddlValorTrigliceridos30DiasHombres.SelectedValue != "0" && this.ddlValorTrigliceridos30DiasHombres.SelectedValue != string.Empty)
            {
                objConsulta.ValorTrigliceridos30DiasHombres = int.Parse(ddlValorTrigliceridos30DiasHombres.SelectedValue);
            }

            if (this.ddlValorTrigliceridos30DiasMujeres.SelectedValue != "0" && this.ddlValorTrigliceridos30DiasMujeres.SelectedValue != string.Empty)
            {
                objConsulta.ValorTrigliceridos30DiasMujeres = int.Parse(ddlValorTrigliceridos30DiasMujeres.SelectedValue);
            }

        }

        /// <summary>
        /// Método, carga un objeto consulta con los datos de la forma de la consulta general
        /// </summary>
        public bool LoadObjectConsultaEstamosContigoGeneral(ConsultaEstamosContigoGeneral ObjConsultaEstamosContigoGeneral)
        {
            bool cargarObjeto = false;
            //Tipo de padecimiento
            if (this.lblTipoPadecimientoAnterior.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.IdTipoPadecimiento = Convert.ToInt32(this.lblTipoPadecimientoAnterior.Text);
            }
            //GENERALES
            if (this.rblVisitaNutriologo.SelectedIndex > -1)
            {
                ObjConsultaEstamosContigoGeneral.VisitaNutriologo = Convert.ToInt32(this.rblVisitaNutriologo.SelectedValue);
                cargarObjeto = true;
            }
            else
                ObjConsultaEstamosContigoGeneral.VisitaNutriologo = -1;


            if (this.rblApegadoDieta.SelectedIndex > -1)
            {
                ObjConsultaEstamosContigoGeneral.ApegadoDieta = Convert.ToInt32(this.rblApegadoDieta.SelectedValue);
                cargarObjeto = true;
            }
            else
                ObjConsultaEstamosContigoGeneral.ApegadoDieta = -1;

            if (this.txtNoVisitaNutriologo.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.NoVisitaNutriologo = Convert.ToString(this.txtNoVisitaNutriologo.Text);
                cargarObjeto = true;
            }

            if (this.rblMedicoTratamiento.SelectedIndex > -1)
            {
                ObjConsultaEstamosContigoGeneral.MedicoTratamiento = Convert.ToInt32(this.rblMedicoTratamiento.SelectedValue);
                cargarObjeto = true;
            }
            else
                ObjConsultaEstamosContigoGeneral.MedicoTratamiento = -1;

            if (this.ddlIndicacionesMedico.SelectedValue != "0" && this.ddlIndicacionesMedico.SelectedValue != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.IndicacionesMedico = int.Parse(ddlIndicacionesMedico.SelectedValue);
                cargarObjeto = true;
            }

            if (this.txtNoCumpleTratamientos.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.NoCumpleTratamientos = Convert.ToString(this.txtNoCumpleTratamientos.Text);
                cargarObjeto = true;
            }

            if (this.txtOtraComplicacion.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.OtraComplicacion = Convert.ToString(this.txtOtraComplicacion.Text);
                cargarObjeto = true;
            }

            if (this.txtMedicamentos.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.Medicamentos = Convert.ToString(this.txtMedicamentos.Text);
                cargarObjeto = true;
            }

            if (this.txtRecomendaciones.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.Recomendaciones = Convert.ToString(this.txtRecomendaciones.Text);
                cargarObjeto = true;
            }

            if (this.txtFechaSiguienteCita.Text != string.Empty)
            {
                ObjConsultaEstamosContigoGeneral.FechaSiguienteCita = Convert.ToDateTime(this.txtFechaSiguienteCita.Text);
                cargarObjeto = true;
            }
            else
                ObjConsultaEstamosContigoGeneral.FechaSiguienteCita = new DateTime(1900, 1, 1);

            //OPCIONES LLENA ARREGLO DE OPCINES POR CADA CONTROL
            ArrayList arrOpciones = new ArrayList();

            for (int i = 0; i < this.cblComplicacionHAS.Items.Count; i++)
            {
                if (cblComplicacionHAS.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 547;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblComplicacionHAS.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                    cargarObjeto = true;
                }
            }

            for (int i = 0; i < this.cblComplicacionDM.Items.Count; i++)
            {
                if (cblComplicacionDM.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 553;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblComplicacionDM.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                    cargarObjeto = true;
                }
            }

            for (int i = 0; i < this.cblComplicacionDislipidemias.Items.Count; i++)
            {
                if (cblComplicacionDislipidemias.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 559;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblComplicacionDislipidemias.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                    cargarObjeto = true;
                }
            }

            for (int i = 0; i < this.cblComplicacionTrigliceridos.Items.Count; i++)
            {
                if (cblComplicacionTrigliceridos.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 565;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblComplicacionTrigliceridos.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                    cargarObjeto = true;
                }
            }
            ObjConsultaEstamosContigoGeneral.ConsultaOpcion = arrOpciones;
            return cargarObjeto;

        }

        /// <summary>
        /// Método, Carga una consulta en el forma
        /// </summary>
        public void LoadFormConsultaEstamosContigo(long p_idConsulta)
        {
            ConsultaEstamosContigoGeneral ObjConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            ObjConsultaEstamosContigoGeneral.IdConsulta = p_idConsulta;
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            ObjConsultaEstamosContigoGeneral.GetConsultaEstamosContigoGenerales(p_idConsulta, TipoPadecimiento);

            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;

            if (objConsulta.ExisteConsultaEstamosContigo() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            {
                DisableTextBoxesRecursive(Page);
                this.btnCancelar.Visible = true;
                this.btnGuardar.Visible = true;
                this.cblComplicacionDislipidemias.Enabled = false;
                this.cblComplicacionDM.Enabled = false;
                this.cblComplicacionHAS.Enabled = false;
                this.cblComplicacionTrigliceridos.Enabled = false;
                this.rfvFechaSiguienteCita.Enabled = false;
                this.imgCalCita.Style["display"] = "none";
                this.imgCalColTotal.Style["display"] = "none";
                this.imgCalHAS.Style["display"] = "none";
                this.imgCalHDL.Style["display"] = "none";
                this.imgCalLDL.Style["display"] = "none";
                this.imgCalMD.Style["display"] = "none";
                this.imgCalTrigliceridos.Style["display"] = "none";
            }

            //HIPERTENSIÓN ARTERIAL SISTEMICA (HAS)

            if (ObjConsultaEstamosContigoGeneral.VisitaNutriologo != -1)
            {
                this.rblVisitaNutriologo.SelectedValue = ObjConsultaEstamosContigoGeneral.VisitaNutriologo.ToString();
                if (this.rblMedicoTratamiento.SelectedIndex == 0)
                {
                    this.ddlIndicacionesMedico.Visible = true;
                    this.lblIndicacionesMedico.Visible = true;
                }
                if (this.rblMedicoTratamiento.SelectedIndex == 1)
                {
                    this.ddlIndicacionesMedico.Visible = false;
                    this.lblIndicacionesMedico.Visible = false;
                }
            }

            if (ObjConsultaEstamosContigoGeneral.ApegadoDieta != -1)
            {
                this.rblApegadoDieta.SelectedValue = ObjConsultaEstamosContigoGeneral.ApegadoDieta.ToString();
            }

            txtNoVisitaNutriologo.Text = Convert.ToString(ObjConsultaEstamosContigoGeneral.NoVisitaNutriologo);

            if (ObjConsultaEstamosContigoGeneral.MedicoTratamiento != -1)
            {
                this.rblMedicoTratamiento.SelectedValue = ObjConsultaEstamosContigoGeneral.MedicoTratamiento.ToString();
                if (this.rblMedicoTratamiento.SelectedIndex == 0)
                {
                    this.ddlIndicacionesMedico.Visible = true;
                    this.lblIndicacionesMedico.Visible = true;
                }
                if (this.rblMedicoTratamiento.SelectedIndex == 1)
                {
                    this.ddlIndicacionesMedico.Visible = false;
                    this.lblIndicacionesMedico.Visible = false;
                }
            }

            if (ObjConsultaEstamosContigoGeneral.IndicacionesMedico != 0)
            {
                this.ddlIndicacionesMedico.SelectedValue = ObjConsultaEstamosContigoGeneral.IndicacionesMedico.ToString();
                if (this.ddlIndicacionesMedico.SelectedValue == "546")
                {
                    this.lblNoCumpleTratamientos.Visible = true;
                    this.txtNoCumpleTratamientos.Visible = true;
                }
                else
                {
                    this.lblNoCumpleTratamientos.Visible = false;
                    this.txtNoCumpleTratamientos.Visible = false;
                }
            }

            txtMedicamentos.Text = Convert.ToString(ObjConsultaEstamosContigoGeneral.Medicamentos);
            txtNoCumpleTratamientos.Text = Convert.ToString(ObjConsultaEstamosContigoGeneral.NoCumpleTratamientos);
            txtOtraComplicacion.Text = Convert.ToString(ObjConsultaEstamosContigoGeneral.OtraComplicacion);
            txtRecomendaciones.Text = Convert.ToString(ObjConsultaEstamosContigoGeneral.Recomendaciones);

            if (ObjConsultaEstamosContigoGeneral.FechaSiguienteCita.ToShortDateString() != "01/01/0001")
                this.txtFechaSiguienteCita.Text = ObjConsultaEstamosContigoGeneral.FechaSiguienteCita.ToShortDateString();

            foreach (ConsultaOpcion objConsultaOpcion in ObjConsultaEstamosContigoGeneral.ConsultaOpcion)
            {
                if (objConsultaOpcion.IdPreguntaRespuestaPadre == 547)
                {
                    for (int i = 0; i < cblComplicacionHAS.Items.Count; i++)
                    {
                        if (cblComplicacionHAS.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                        {
                            cblComplicacionHAS.Items[i].Selected = true;
                            break;
                        }
                    }

                }

                if (objConsultaOpcion.IdPreguntaRespuestaPadre == 553)
                {
                    for (int i = 0; i < cblComplicacionDM.Items.Count; i++)
                    {
                        if (cblComplicacionDM.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                        {
                            cblComplicacionDM.Items[i].Selected = true;
                            break;
                        }
                    }

                }

                if (objConsultaOpcion.IdPreguntaRespuestaPadre == 559)
                {
                    for (int i = 0; i < cblComplicacionDislipidemias.Items.Count; i++)
                    {
                        if (cblComplicacionDislipidemias.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                        {
                            cblComplicacionDislipidemias.Items[i].Selected = true;
                            break;
                        }
                    }

                }

                if (objConsultaOpcion.IdPreguntaRespuestaPadre == 565)
                {
                    for (int i = 0; i < cblComplicacionTrigliceridos.Items.Count; i++)
                    {
                        if (cblComplicacionTrigliceridos.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                        {
                            cblComplicacionTrigliceridos.Items[i].Selected = true;
                            break;
                        }
                    }

                }
            }

        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento, guarda la consulta estamos contigo con los datos diligenciados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMostrarHAS_Click(object sender, EventArgs e)
        {
            this.lblTipoPadecimiento.Text = Convert.ToString(1);

            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            this.rfvFechaSiguienteCita.Enabled = false;

            if (lblPadeciminetoAnterior.Text != string.Empty)
            {
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimientoAnterior.Text)))
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);

                    }
                    else
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                }
            }

            OcultarBorrarControles();
            if (objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, TipoPadecimiento))
            {
                this.rfvFechaSiguienteCita.Enabled = true;
                LoadFormConsultaEstamosContigo(idconsulta);
            }

            lblTipoPadecimientoAnterior.Text = Convert.ToString(1);
            lblPadeciminetoAnterior.Text = "HAS";
            this.cblComplicacionHAS.Visible = true;
            this.cblComplicacionDM.Visible = false;
            this.cblComplicacionDislipidemias.Visible = false;
            this.cblComplicacionTrigliceridos.Visible = false;
            this.lblEstadoHAS.Text = Convert.ToString(1);
            this.divHAS.Visible = true;
            this.divDM.Visible = false;
            this.divDislipidemias.Visible = false;
            this.divTrigliceridos.Visible = false;
            this.divGenerales.Visible = true;

        }

        /// <summary>
        /// Evento, guarda la consulta estamos contigo con los datos diligenciados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMostrarDM_Click(object sender, EventArgs e)
        {
            this.lblTipoPadecimiento.Text = Convert.ToString(2);

            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            this.rfvFechaSiguienteCita.Enabled = false;

            if (lblPadeciminetoAnterior.Text != string.Empty)
            {
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimientoAnterior.Text)))
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                    else
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                }
            }
            OcultarBorrarControles();
            if (objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, TipoPadecimiento))
            {
                this.rfvFechaSiguienteCita.Enabled = true;
                LoadFormConsultaEstamosContigo(idconsulta);
            }


            lblTipoPadecimientoAnterior.Text = Convert.ToString(2);
            lblPadeciminetoAnterior.Text = "DM";
            this.cblComplicacionDM.Visible = true;
            this.cblComplicacionHAS.Visible = false;
            this.cblComplicacionDislipidemias.Visible = false;
            this.cblComplicacionTrigliceridos.Visible = false;
            this.lblEstadoDM.Text = Convert.ToString(1);
            this.divHAS.Visible = false;
            this.divDM.Visible = true;
            this.divDislipidemias.Visible = false;
            this.divTrigliceridos.Visible = false;
            this.divGenerales.Visible = true;
        }

        /// <summary>
        /// Evento, guarda la consulta estamos contigo con los datos diligenciados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMostrarColesterol_Click(object sender, EventArgs e)
        {
            this.lblTipoPadecimiento.Text = Convert.ToString(3);

            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            this.rfvFechaSiguienteCita.Enabled = false;

            if (lblPadeciminetoAnterior.Text != string.Empty)
            {
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimientoAnterior.Text)))
                    {

                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                    else
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                }
            }
            OcultarBorrarControles();
            if (objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, TipoPadecimiento))
            {
                this.rfvFechaSiguienteCita.Enabled = true;
                LoadFormConsultaEstamosContigo(idconsulta);
            }

            lblTipoPadecimientoAnterior.Text = Convert.ToString(3);
            lblPadeciminetoAnterior.Text = "Dislipidemias";
            this.lblEstadoDislipidemias.Text = Convert.ToString(1);
            this.cblComplicacionDM.Visible = false;
            this.cblComplicacionHAS.Visible = false;
            this.cblComplicacionDislipidemias.Visible = true;
            this.cblComplicacionTrigliceridos.Visible = false;
            this.divHAS.Visible = false;
            this.divDM.Visible = false;
            this.divDislipidemias.Visible = true;
            this.divTrigliceridos.Visible = false;
            this.divGenerales.Visible = true;
        }

        /// <summary>
        /// Evento, guarda la consulta estamos contigo con los datos diligenciados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMostrarTrigliceridos_Click(object sender, EventArgs e)
        {
            this.lblTipoPadecimiento.Text = Convert.ToString(4);

            ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
            long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
            int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);
            this.rfvFechaSiguienteCita.Enabled = false;

            if (lblPadeciminetoAnterior.Text != string.Empty)
            {
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimientoAnterior.Text)))
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                    else
                    {
                        objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                        UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                    }
                }
            }

            OcultarBorrarControles();
            if (objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, TipoPadecimiento))
            {
                this.rfvFechaSiguienteCita.Enabled = true;
                LoadFormConsultaEstamosContigo(idconsulta);
            }


            lblTipoPadecimientoAnterior.Text = Convert.ToString(4);
            lblPadeciminetoAnterior.Text = "Trigliceridos";
            this.cblComplicacionTrigliceridos.Visible = true;
            this.cblComplicacionDM.Visible = false;
            this.cblComplicacionHAS.Visible = false;
            this.cblComplicacionDislipidemias.Visible = false;
            this.cblComplicacionTrigliceridos.Visible = true;
            this.lblEstadoTrigliceridos.Text = Convert.ToString(1);
            this.divHAS.Visible = false;
            this.divDM.Visible = false;
            this.divDislipidemias.Visible = false;
            this.divTrigliceridos.Visible = true;
            this.divGenerales.Visible = true;
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de presión arterial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblPresionArterial30Dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rblPresionArterial30Dias.SelectedValue) == 1)
            {
                this.divPresionArterial.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divPresionArterial.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de glucosa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblGlucosa30dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rblGlucosa30dias.SelectedValue) == 1)
            {
                this.divSiDiabetesMellitus.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divSiDiabetesMellitus.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }

        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de colesterol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        protected void rblColesterolLDL30Dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rblColesterolLDL30Dias.SelectedValue) == 1)
            {
                this.divSiLDL.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divSiLDL.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de colesterol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void rblColesterolHDL30Dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rblColesterolHDL30Dias.SelectedValue) == 1)
            {
                this.divSiHDL.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divSiHDL.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de colesterol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void rblColesterolTotal30Dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rblColesterolTotal30Dias.SelectedValue) == 1)
            {
                this.divSiColesterolTotal.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divSiColesterolTotal.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de trigliceridos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void rdlTrigliceridos30Dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rdlTrigliceridos30Dias.SelectedValue) == 1)
            {
                this.divSiTrigliceridos.Visible = true;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
            else
            {
                this.divSiTrigliceridos.Visible = false;
                this.rfvFechaSiguienteCita.Enabled = true;
            }
        }

        /// <summary>
        /// Evento, realiza el llamado para guarda la consulta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, System.EventArgs e)
        {
            EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));

            if (lblTipoPadecimiento.Text == string.Empty)
            {
                if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                {
                    if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                        Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                    else
                        Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                }
                else
                {
                    if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                    {
                        if (_esMedico)
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                        else
                            Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                    }
                    else
                    {
                        if (_esMedico)
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                        else
                            Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                    }
                }
            }

            try
            {                

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaEstamosContigo())
                    {
                        ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
                        long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);

                        if (lblPadeciminetoAnterior.Text != string.Empty)
                        {
                            if (Request.QueryString["IdConsulta"] != null)
                            {
                                if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimiento.Text)))
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                                else
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                            }
                        }

                        long idConsulta = this.InsertConsultaEstamosContigo(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstamosContigo, idConsulta, "Ingreso consulta estamos contigo" + idConsulta);

                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                        {
                            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                        }
                        else
                        {
                            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                            {
                                if (_esMedico)
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            }
                            else
                            {
                                if (_esMedico)
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            }
                        }   

                        

                    }
                    else
                    {
                        this.UpdateEstamosContigo(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstamosContigo, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"]));

                        ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
                        long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);

                        if (lblPadeciminetoAnterior.Text != string.Empty)
                        {
                            if (Request.QueryString["IdConsulta"] != null)
                            {
                                if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimiento.Text)))
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                                else
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                            }
                        }

                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                        {
                            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                        }
                        else
                        {
                            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                            {
                                if (_esMedico)
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            }
                            else
                            {
                                if (_esMedico)
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            }
                        }                      

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, para volver a la página anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (lblTipoPadecimiento.Text != string.Empty)
            {
                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaEstamosContigo())
                    {
                        ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
                        long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);

                        if (lblPadeciminetoAnterior.Text != string.Empty)
                        {
                            if (Request.QueryString["IdConsulta"] != null)
                            {
                                if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimiento.Text)))
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                                else
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                            }
                        }

                        long idConsulta = this.InsertConsultaEstamosContigo(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstamosContigo, idConsulta, "Ingreso consulta estamos contigo" + idConsulta);
                    }

                    else
                    {
                        this.UpdateEstamosContigo(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstamosContigo, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"]));

                        ConsultaEstamosContigoGeneral objConsultaEstamosContigoGeneral = new ConsultaEstamosContigoGeneral();
                        long idconsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        int TipoPadecimiento = Convert.ToInt16(lblTipoPadecimiento.Text);

                        if (lblPadeciminetoAnterior.Text != string.Empty)
                        {
                            if (Request.QueryString["IdConsulta"] != null)
                            {
                                if (!objConsultaEstamosContigoGeneral.ExisteConsultaEstamosContigoGenerales(idconsulta, Convert.ToInt32(lblTipoPadecimiento.Text)))
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    InsertConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                                else
                                {
                                    objConsultaEstamosContigoGeneral.IdConsulta = idconsulta;
                                    UpdateConsultaEstamosContigoGeneral(objConsultaEstamosContigoGeneral.IdConsulta);
                                }
                            }
                        }
                    }
                }
                if (_esMedico)
                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                else
                    Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));

            }
            if (_esMedico)
                Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
            else
                Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));


        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de visita nutriologo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void rblVisitaNutriologo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblVisitaNutriologo.SelectedIndex == 0)
            {
                this.lblNoVisitaNutriologo.Visible = false;
                this.txtNoVisitaNutriologo.Visible = false;
                this.rblApegadoDieta.Visible = true;
                this.lblApegadoDieta.Visible = true;
            }
            if (this.rblVisitaNutriologo.SelectedIndex == 1)
            {
                this.lblApegadoDieta.Visible = false;
                this.rblApegadoDieta.Visible = false;
                this.txtNoVisitaNutriologo.Visible = true;
                this.lblNoVisitaNutriologo.Visible = true;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de medico tratamiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void rblMedicoTratamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblMedicoTratamiento.SelectedIndex == 0)
            {
                this.ddlIndicacionesMedico.Visible = true;
                this.lblIndicacionesMedico.Visible = true;
            }
            if (this.rblMedicoTratamiento.SelectedIndex == 1)
            {
                this.ddlIndicacionesMedico.Visible = false;
                this.lblIndicacionesMedico.Visible = false;
            }
        }

        /// <summary>
        /// Evento, Muestra nuevos campos al modificar la selección del campo de indicaciones médico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void ddlIndicacionesMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlIndicacionesMedico.SelectedValue == "546")
            {
                this.lblNoCumpleTratamientos.Visible = true;
                this.txtNoCumpleTratamientos.Visible = true;
            }
            else
            {
                this.lblNoCumpleTratamientos.Visible = false;
                this.txtNoCumpleTratamientos.Visible = false;
            }
        }

        /// <summary>
        /// Evento, realiza la cancelación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Consulta objConsulta = new Consulta();
            //objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            //objConsulta.GetConsulta();
            //objConsulta.GetConsultaEstiloVida();
            //if (objConsulta.ExisteConsultaEstiloVida() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            //    Response.Redirect("LIS_consulta.aspx");
            //else
            //    Response.Redirect("LIS_empleado.aspx");
            Response.Redirect("LIS_consulta.aspx");//Marsh - JFEE - 2014/11/26 - Correcciones generales, al cancelar una consulta siempre debe direccionar a la página de consultas

        }

        #endregion

        


    }

}
