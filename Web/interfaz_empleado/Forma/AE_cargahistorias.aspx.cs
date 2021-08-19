using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPA;
using System.Data.OleDb;
using System.Data;
using Mercer.Medicines.Logic;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace Web.interfaz_empleado.Forma
{
    public partial class AE_CargaHistorias : PB_PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet datosUltimaCarga = new DataSet();
                ConsultaBiometricos objConsultaBiometricos = new ConsultaBiometricos();
                datosUltimaCarga = objConsultaBiometricos.ConsultaUltimaCarga(Convert.ToInt32(Session["Company"]));

                if (datosUltimaCarga.Tables[0].Rows.Count > 0)
                {
                    lblFechaUltimaCargaDato.Text = datosUltimaCarga.Tables[0].Rows[0][0].ToString();
                    lblRealizadoPorDato.Text = datosUltimaCarga.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    lblFechaUltimaCargaDato.Text = "---";
                    lblRealizadoPorDato.Text = "---";
                }
            }
        }

        public void btnSubir_Click(object sender, EventArgs e)
        {
            String str_Ruta;

         
            str_Ruta = Server.MapPath("../");

            if ((uploadBtn.PostedFile != null) && (uploadBtn.PostedFile.ContentLength > 0))
            {

                string strNombre_archivo = string.Empty;
                strNombre_archivo += "HistoriasClinicas_" + DateTime.Now.Millisecond+ DateTime.Now.Minute + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year + ".xls";
                lblNombreArchivo.Text = strNombre_archivo;

                string str_fn = System.IO.Path.GetFileName(uploadBtn.PostedFile.FileName);
                string SaveLocation = str_Ruta + "\\" + strNombre_archivo;//"carga_titulares.xls"; //str_fn;

                try
                {
                    uploadBtn.PostedFile.SaveAs(SaveLocation);

                    btnSubir.Visible = false;
                    //btnContinuar.Visible = true;
                    uploadBtn.Visible = false;
                    uploadBtn.Visible = false;
                    //this.DisplayMessage("Archivo cargado, para continuar de clic en aceptar.");
                    btnContinuar_Click(null, null);
                }
                catch (Exception ex)
                {
                    //Response.Write("<script>alert('Error al cargar el archivo'); window.open('AE_cargahistorias.aspx','_self') </script>");
                    //Response.Write("<script>alert('"+ ex.InnerException.ToString().Replace("'", " ")+"'); window.open('AE_cargahistorias.aspx','_self') </script>");
                    //Response.Write(ex.InnerException.ToString());
                    this.DisplayMessage(ex.ToString());
                }
            }
            else
            {
                this.DisplayMessage("Por favor selecciona un archivo");
            }
        }

        /// <summary>
        /// Método, inserta una nueva consulta
        /// </summary>
        /// <returns></returns>
        public bool InsertConsulta(DataSet dsConsultas)
        {
            #region Declaración de variables

            long idConsulta;
            int i;
            Consulta objConsulta;
            DataTable dtErrores;
            DataTable dtConsultas;

            DataRow rowError;
            DataRow rowConsulta;

            string strError;
            bool bolErrores;

            #endregion

            #region Inicialización de variables

            dtErrores = new DataTable();
            dtConsultas = new DataTable();

            dtErrores.Columns.Add("NumeroRegistro", Type.GetType("System.String"));
            dtErrores.Columns.Add("Error", Type.GetType("System.String"));
            dtConsultas.Columns.Add("IdConsulta", Type.GetType("System.String"));

            strError = "";

            bolErrores = false;
            i = 0;

            #endregion                      

            #region Se valida si existe el asegurado

            foreach (DataRow row in dsConsultas.Tables[0].Rows)
            {
                if (i > 0)
                {
                    objConsulta = new Consulta();

                    if (!ValidarAsegurados(objConsulta, row, ref strError))
                    {

                        rowError = dtErrores.NewRow();
                        rowError["NumeroRegistro"] = i.ToString();
                        rowError["Error"] = strError;

                        dtErrores.Rows.Add(rowError);
                        bolErrores = true;
                    }
                }
                i++;
            }

            #endregion

            //Se muestran los asegurados que no existen
            if (bolErrores)
            {
                //RAM* ya no se  cargara el grid errores
                //gvErrores.DataSource = dtErrores;
                //gvErrores.DataBind();

                return bolErrores;
            }

            //Se procede a realizar la inserción de las consultas
            i = 0;

            foreach (DataRow row in dsConsultas.Tables[0].Rows)
            {
                if (i > 0)
                {
                    objConsulta = new Consulta();

                    if (this.LoadObjectConsulta(objConsulta, row, ref strError)) //Se llena el objeto consulta
                    {
                        idConsulta = objConsulta.InsertConsulta();     //Se inserta la consulta                   
                        rowConsulta = dtConsultas.NewRow();
                        rowConsulta["IdConsulta"] = idConsulta.ToString();

                        dtConsultas.Rows.Add(rowConsulta);

                    }
                    else
                    {
                        //Se registran los errores
                        rowError = dtErrores.NewRow();
                        rowError["NumeroRegistro"] = i.ToString();
                        rowError["Error"] = strError;

                        dtErrores.Rows.Add(rowError);
                        bolErrores = true;
                        //RAM* lblErrores.Visible = true;
                    }
                }
                i++;
            }

            //Se enlazan los errores existentes
            //RAM* se quita para tener solo un gridview
            //gvErrores.DataSource = dtErrores;
            //gvErrores.DataBind();
            
            //dgConsultasCargadas.DataSource = dtConsultas;
            //dgConsultasCargadas.DataBind();
            //dgConsultasCargadas.Visible = true;
            //lblConsultasCargadas.Visible = true;

            return bolErrores;
        }

        public bool LoadObjectConsulta(Consulta objConsulta, DataRow dtrConsulta, ref string strError)
        {
            try
            {
                #region Declaración de variables

                SIC_EMPLEADO objEmpleado;
                DataSet dsAsegurados;
                DataSet dsDiagnosticos;
                bool bolExisteAsegurado;
                string strSeparadorMiles;
                string strNumeroEmpleado;
                string strClaveParentesco;
                string strFechaNacimiento;
                string strNombre;
                string strApellidoPaterno;
                string strApellidoMaterno;
                string strNombreBusqueda;
                string strPeso;
                string strTalla;
                string strTemperatura;
                string strFrecuenciaCar;
                string strFrecuenciaRes;

                #endregion

                #region Inicialización de variables

                objEmpleado = new SIC_EMPLEADO();
                dsAsegurados = new DataSet();
                bolExisteAsegurado = false;
                objConsulta.Empresa_id = Convert.ToInt32(Session["Company"]);
                strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();

                #endregion

                #region Búsqueda de asegurado

                objEmpleado.Primer_nombre = "";
                objEmpleado.Empresa_id = Convert.ToInt32(Session["Company"]);
                objEmpleado.Identificacion = dtrConsulta[0].ToString().Trim();

                dsAsegurados = objEmpleado.ConsultSIC_USUARIOS("", null);

                foreach (DataRow row in dsAsegurados.Tables[0].Rows)
                {
                    strNumeroEmpleado = dtrConsulta[0].ToString().Trim();
                    strClaveParentesco = dtrConsulta[1].ToString();
                    strNombre = dtrConsulta[2].ToString().Trim().ToUpper();
                    strNombreBusqueda = row["primer_nombre"].ToString().Trim().ToUpper();
                    strApellidoPaterno = dtrConsulta[3].ToString().Trim().ToUpper();
                    strApellidoMaterno = dtrConsulta[4].ToString().Trim().ToUpper();
                    strFechaNacimiento = dtrConsulta[5].ToString();


                    if (int.Parse(strClaveParentesco) > 1)
                    {
                        if (strNumeroEmpleado == row["identificacion"].ToString() &&
                           strNombre == strNombreBusqueda &&
                            strClaveParentesco == row["ClaveParentesco"].ToString() &&
                            strFechaNacimiento == row["fecha_nacimiento"].ToString())
                        {
                            objConsulta.Id_empleado = int.Parse(row["id_empleado"].ToString());
                            objConsulta.Beneficiario_id = int.Parse(row["beneficiario_id"].ToString());

                            bolExisteAsegurado = true;

                            break;
                        }
                    }
                    else
                    {
                        if (strNumeroEmpleado == row["identificacion"].ToString() &&
                            strClaveParentesco == row["ClaveParentesco"].ToString())
                        {
                            objConsulta.Id_empleado = int.Parse(row["id_empleado"].ToString());
                            objConsulta.Beneficiario_id = int.Parse(row["beneficiario_id"].ToString());

                            bolExisteAsegurado = true;

                            break;
                        }
                    }
                }

                #endregion

                if (bolExisteAsegurado)
                {
                    #region Datos de la consulta

                    objConsulta.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
                    objConsulta.IdPrestador = int.Parse(IsNull(dtrConsulta[6].ToString(), 0).ToString());
                    objConsulta.FechaInicioCreacion = DateTime.Now;
                    objConsulta.IdTipoConsulta = short.Parse(IsNull(dtrConsulta[7].ToString(), 0).ToString());

                    //Inicio MAHG 04/06/2010
                    //Se agregó el campo transcripción 
                    //objConsulta.ComentariosTranscripcion = this.txtComentariosTranscripcion.Text;

                    //string strIdSolicitante  = dtrConsulta[8].ToString();

                    //if (strIdSolicitante != "")
                    //    objConsulta.IdProveedorTranscripcion = Convert.ToInt32(strIdSolicitante);

                    //DataColumn column = dtrConsulta.Table.Columns[8];
                    //dtrConsulta.Table.Columns.RemoveAt(8);

                    //Fin MAHG 04/06/2010


                    objConsulta.IdTipoEnfermedad = short.Parse(IsNull(dtrConsulta[8].ToString(), 0).ToString());
                    objConsulta.Motivo = dtrConsulta[9].ToString();
                    objConsulta.Contrarreferencia = dtrConsulta[11].ToString();
                    objConsulta.EnfermedadActual = dtrConsulta[10].ToString();
                    objConsulta.ObservacionesGenerales = dtrConsulta[12].ToString();
                    objConsulta.PlanTratamiento = dtrConsulta[13].ToString();

                    objConsulta.ExamenesLaboratorio = dtrConsulta[123].ToString();

                    if (dtrConsulta[14].ToString() != string.Empty)
                        objConsulta.CitaControl = Convert.ToDateTime(dtrConsulta[14].ToString());
                    else
                        objConsulta.CitaControl = new DateTime(1900, 1, 1);

                    #endregion

                    #region Antecedentes
                    objConsulta.Medicos = dtrConsulta[16].ToString();
                    objConsulta.Quirurgicos = dtrConsulta[18].ToString();
                    objConsulta.Ginecobstetricos = dtrConsulta[20].ToString();
                    objConsulta.Transfusionales = dtrConsulta[29].ToString();
                    objConsulta.ToxicoAlergicos = dtrConsulta[31].ToString();
                    objConsulta.Farmacologicos = dtrConsulta[33].ToString();
                    objConsulta.OtrosAntecedentes = dtrConsulta[35].ToString();
                    objConsulta.Familiares = dtrConsulta[37].ToString();
                    objConsulta.Menarquia = dtrConsulta[21].ToString();
                    objConsulta.FechaUltimaMestruacion = dtrConsulta[22].ToString();

                    if (dtrConsulta[23].ToString() != string.Empty)
                        objConsulta.Gestaciones = Convert.ToInt16(dtrConsulta[23].ToString());
                    if (dtrConsulta[24].ToString() != string.Empty)
                        objConsulta.Partos = Convert.ToInt16(dtrConsulta[24].ToString());
                    if (dtrConsulta[25].ToString() != string.Empty)
                        objConsulta.Cesareas = Convert.ToInt16(dtrConsulta[25].ToString());
                    if (dtrConsulta[26].ToString() != string.Empty)
                        objConsulta.Abortos = Convert.ToInt16(dtrConsulta[26].ToString());
                    if (dtrConsulta[27].ToString() != string.Empty)
                        objConsulta.Vivos = Convert.ToInt16(dtrConsulta[27].ToString());

                    objConsulta.NormalMedicos = !Convert.ToBoolean((Convert.ToInt16(IsNull(dtrConsulta[15].ToString(), 0))));
                    objConsulta.NormalQuirurgicos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[17].ToString(), 0)));
                    objConsulta.NormalGinecobstetricos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[19].ToString(), 0)));
                    objConsulta.NormalTransfusionales = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[28].ToString(), 0)));
                    objConsulta.NormalToxicoAlergicos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[30].ToString(), 0)));
                    objConsulta.NormalFarmacologicos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[32].ToString(), 0)));
                    objConsulta.NormalOtrosAntecedentes = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[34].ToString(), 0)));
                    objConsulta.NormalFamiliares = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[36].ToString(), 0)));
                    #endregion

                    #region Revisión por sistemas

                    objConsulta.AspectoGeneral = dtrConsulta[39].ToString();
                    objConsulta.Cabeza = dtrConsulta[41].ToString();
                    objConsulta.Cuello = dtrConsulta[43].ToString();
                    objConsulta.Torax = dtrConsulta[45].ToString();
                    objConsulta.Abdomen = dtrConsulta[47].ToString();
                    objConsulta.Otros = dtrConsulta[49].ToString();

                    objConsulta.NormalAspectoGeneral = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[38].ToString(), 0)));
                    objConsulta.NormalCabeza = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[40].ToString(), 0)));
                    objConsulta.NormalCuello = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[42].ToString(), 0)));
                    objConsulta.NormalTorax = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[44].ToString(), 0)));
                    objConsulta.NormalAbdomen = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[46].ToString(), 0)));
                    objConsulta.NormalOtros = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[48].ToString(), 0)));

                    #endregion

                    #region  Exámen físico

                    //if (this.txtTension.Visible)
                    //    objConsulta.TensionArterial = this.txtTension.Text;
                    //if (this.txtTensionMedia.Visible)
                    //    objConsulta.TensionArterial = Math.Round((decimal.Parse(txtDiastolica.Text.Replace(strSeparadorMiles, "")) + (decimal.Parse(txtSistolica.Text.Replace(strSeparadorMiles, "")) - decimal.Parse(txtDiastolica.Text.Replace(strSeparadorMiles, ""))) / 3), 0).ToString();

                    objConsulta.TensionArterial = dtrConsulta[53].ToString();
                    objConsulta.TensionArterialDiastolica = dtrConsulta[54].ToString();
                    objConsulta.TensionArterialSistolica = dtrConsulta[55].ToString();
                    objConsulta.ComentariosExamenFisico = dtrConsulta[113].ToString();

                    strPeso = dtrConsulta[50].ToString().Replace(strSeparadorMiles, "");
                    strTalla = dtrConsulta[51].ToString().Replace(strSeparadorMiles, "");
                    //this.txtPerimetroAbdominal.Text = this.txtPerimetroAbdominal.Text.Replace(strSeparadorMiles, "");
                    strTemperatura = dtrConsulta[58].ToString().Replace(strSeparadorMiles, "");
                    strFrecuenciaCar = dtrConsulta[56].ToString().Replace(strSeparadorMiles, "");
                    strFrecuenciaRes = dtrConsulta[57].ToString().Replace(strSeparadorMiles, "");


                    if (strPeso != string.Empty)
                        objConsulta.Peso = Convert.ToDecimal(strPeso);
                    if (strTalla != string.Empty)
                        objConsulta.Talla = Convert.ToDecimal(strTalla);
                    if (strTalla != string.Empty && strPeso != string.Empty)
                        objConsulta.IndiceMasaCorporal = Convert.ToDecimal(strPeso) / (Convert.ToDecimal(strTalla) * Convert.ToDecimal(strTalla));
                    if (strFrecuenciaCar != string.Empty)
                        objConsulta.FrecuenciaCardiaca = Convert.ToInt32(strFrecuenciaCar);
                    if (strFrecuenciaRes != string.Empty)
                        objConsulta.FrecuenciaRespiratoria = Convert.ToInt32(strFrecuenciaRes);
                    //if (this.txtPerimetroAbdominal.Text != string.Empty)
                    //    objConsulta.PerimetroAbdominal = Convert.ToDecimal(this.txtPerimetroAbdominal.Text);
                    if (strTemperatura != string.Empty)
                        objConsulta.Temperatura = Convert.ToDecimal(strTemperatura);

                    objConsulta.ExamenAspectoGeneral = dtrConsulta[60].ToString();
                    objConsulta.ExamenCabeza = dtrConsulta[64].ToString();
                    objConsulta.ExamenCuello = dtrConsulta[82].ToString();
                    objConsulta.ExamenTorax = dtrConsulta[88].ToString();
                    objConsulta.ExamenAbdomen = dtrConsulta[94].ToString();
                    objConsulta.ExamenOtros = dtrConsulta[112].ToString();
                    objConsulta.ExamenPielFanelas = dtrConsulta[62].ToString();
                    objConsulta.ExamenConjuntivaOcular = dtrConsulta[66].ToString();
                    objConsulta.ExamenReflejoCorneal = dtrConsulta[68].ToString();
                    objConsulta.ExamenPupilas = dtrConsulta[70].ToString();
                    objConsulta.ExamenOidos = dtrConsulta[72].ToString();
                    objConsulta.ExamenOtoscopia = dtrConsulta[74].ToString();
                    objConsulta.ExamenRinoscopia = dtrConsulta[76].ToString();
                    objConsulta.ExamenBocaFaringe = dtrConsulta[78].ToString();
                    objConsulta.ExamenAmigdalas = dtrConsulta[80].ToString();
                    objConsulta.ExamenTiroides = dtrConsulta[84].ToString();
                    objConsulta.ExamenAdenopatias = dtrConsulta[86].ToString();
                    objConsulta.ExamenRuidosCardiacos = dtrConsulta[90].ToString();
                    objConsulta.ExamenRuidosRespiratorios = dtrConsulta[92].ToString();
                    objConsulta.ExamenPalpacionAbdomen = dtrConsulta[96].ToString();
                    objConsulta.ExamenGenitalesExternos = dtrConsulta[98].ToString();
                    objConsulta.ExamenHernias = dtrConsulta[100].ToString();
                    objConsulta.ExamenColumnaVertebral = dtrConsulta[102].ToString();
                    objConsulta.ExamenExtremidadesSuperiores = dtrConsulta[104].ToString();
                    objConsulta.ExamenExtremidadesInferiores = dtrConsulta[106].ToString();
                    objConsulta.ExamenVarices = dtrConsulta[108].ToString();
                    objConsulta.ExamenNeurologico = dtrConsulta[110].ToString();


                    objConsulta.ExamenNormalAspectoGeneral = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[59].ToString(), 0)));
                    objConsulta.ExamenNormalCabeza = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[63].ToString(), 0)));
                    objConsulta.ExamenNormalCuello = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[81].ToString(), 0)));
                    objConsulta.ExamenNormalTorax = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[87].ToString(), 0)));
                    objConsulta.ExamenNormalAbdomen = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[93].ToString(), 0)));
                    objConsulta.ExamenNormalOtros = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[111].ToString(), 0)));
                    objConsulta.ExamenNormalPielFanelas = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[61].ToString(), 0)));
                    objConsulta.ExamenNormalConjuntivaOcular = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[65].ToString(), 0)));
                    objConsulta.ExamenNormalReflejoCorneal = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[67].ToString(), 0)));
                    objConsulta.ExamenNormalPupilas = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[69].ToString(), 0)));
                    objConsulta.ExamenNormalOidos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[71].ToString(), 0)));
                    objConsulta.ExamenNormalOtoscopia = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[73].ToString(), 0)));
                    objConsulta.ExamenNormalRinoscopia = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[75].ToString(), 0)));
                    objConsulta.ExamenNormalBocaFaringe = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[77].ToString(), 0)));
                    objConsulta.ExamenNormalAmigdalas = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[79].ToString(), 0)));
                    objConsulta.ExamenNormalTiroides = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[83].ToString(), 0)));
                    objConsulta.ExamenNormalAdenopatias = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[85].ToString(), 0)));
                    objConsulta.ExamenNormalRuidosCardiacos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[89].ToString(), 0)));
                    objConsulta.ExamenNormalRuidosRespiratorios = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[91].ToString(), 0)));
                    objConsulta.ExamenNormalPalpacionAbdomen = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[95].ToString(), 0)));
                    objConsulta.ExamenNormalGenitalesExternos = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[97].ToString(), 0)));
                    objConsulta.ExamenNormalHernias = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[99].ToString(), 0)));
                    objConsulta.ExamenNormalColumnaVertebral = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[101].ToString(), 0)));
                    objConsulta.ExamenNormalExtremidadesSuperiores = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[103].ToString(), 0)));
                    objConsulta.ExamenNormalExtremidadesInferiores = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[105].ToString(), 0)));
                    objConsulta.ExamenNormalVarices = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[107].ToString(), 0)));
                    objConsulta.ExamenNormalNeurologico = !Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[109].ToString(), 0)));

                    #endregion

                    #region Habitos
                    //if (this.rblTabaquismo.SelectedIndex > -1)
                    //    objConsulta.Tabaquismo = Convert.ToInt32(this.rblTabaquismo.SelectedValue);
                    //else
                    //    objConsulta.Tabaquismo = -1;
                    //if (this.rblAlcohol.SelectedIndex > -1)
                    //    objConsulta.ConsumoAlcohol = Convert.ToInt32(this.rblAlcohol.SelectedValue);
                    //else
                    //    objConsulta.ConsumoAlcohol = -1;
                    //if (this.rblDeportiva.SelectedIndex > -1)
                    //    objConsulta.ActividadDeportiva = Convert.ToInt32(this.rblDeportiva.SelectedValue);
                    //else
                    //    objConsulta.ActividadDeportiva = -1;
                    //objConsulta.FrecuenciaConsumo = this.txtFrecuenciaAlcohol.Text;
                    //objConsulta.FrecuenciaTabaquismo = this.txtFrecuenciaTabaquismo.Text;
                    //objConsulta.Vacunacion = this.txtVacunacion.Text;


                    ////Carga diagnósticos
                    //this.WC_AdicionarDiagnosticoConsulta1.LoadDiagnosticos(objConsulta);

                    #endregion

                    #region Pruebas biométricas

                    ////PRUEBAS BIOMÉTRICAS 09/03/2010
                    if (dtrConsulta[114].ToString() != string.Empty)
                        objConsulta.ColesterolTotal = int.Parse(dtrConsulta[114].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[115].ToString() != string.Empty)
                        objConsulta.ColesterolHDL = int.Parse(dtrConsulta[115].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[116].ToString() != string.Empty)
                        objConsulta.ColesterolHDLmmol = decimal.Parse(dtrConsulta[116].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[117].ToString() != string.Empty)
                        objConsulta.ColesterolLDL = int.Parse(dtrConsulta[117].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[118].ToString() != string.Empty)
                        objConsulta.Trigliceridos = int.Parse(dtrConsulta[118].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[119].ToString() != string.Empty)
                        objConsulta.IndiceAterogenico = decimal.Parse(dtrConsulta[119].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[120].ToString() != string.Empty)
                        objConsulta.AntigenoProstata = decimal.Parse(dtrConsulta[120].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[121].ToString() != string.Empty)
                        objConsulta.GlucemiaAyunas = int.Parse(dtrConsulta[121].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[122].ToString() != string.Empty)
                        objConsulta.HemoglobinaGlucosilada = decimal.Parse(dtrConsulta[122].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[124].ToString() != string.Empty)
                        objConsulta.Homocisteina = decimal.Parse(dtrConsulta[124].ToString().Replace(strSeparadorMiles, ""));
                    if (dtrConsulta[125].ToString() != string.Empty)
                        if (dtrConsulta[125].ToString() == "2")
                            objConsulta.PresenciaMicroorganismos = 0;
                        else
                            objConsulta.PresenciaMicroorganismos = Convert.ToInt32(dtrConsulta[125].ToString());
                    else
                        objConsulta.PresenciaMicroorganismos = -1;
                    if (dtrConsulta[126].ToString() != string.Empty)
                        objConsulta.FechaPapanicolauMicro = Convert.ToDateTime(dtrConsulta[126].ToString());
                    else
                        objConsulta.FechaPapanicolauMicro = new DateTime(1900, 1, 1);
                    if (dtrConsulta[127].ToString() != string.Empty)
                        objConsulta.ObservacionesPresenciaMicro = dtrConsulta[127].ToString().Trim();
                    if (dtrConsulta[128].ToString() != string.Empty)
                        objConsulta.ResultadoMorfologico = int.Parse(dtrConsulta[128].ToString());
                    if (dtrConsulta[129].ToString() != string.Empty && dtrConsulta[128].ToString() != string.Empty && dtrConsulta[128].ToString() == "3")
                        objConsulta.AnormalidadCelulasEpiteliales = int.Parse(dtrConsulta[129].ToString());
                    if (dtrConsulta[130].ToString() != string.Empty && dtrConsulta[129].ToString() == "4")
                        objConsulta.CelulasEscamosasAtipicas = int.Parse(dtrConsulta[130].ToString());
                    if (dtrConsulta[131].ToString() != string.Empty)
                        objConsulta.Mamografia = int.Parse(dtrConsulta[131].ToString());
                    if (dtrConsulta[132].ToString() != string.Empty)
                        objConsulta.MamografiaObservaciones = dtrConsulta[132].ToString().Trim();
                    if (dtrConsulta[133].ToString() != string.Empty)
                        objConsulta.Audiometria = Convert.ToInt16(IsNull(dtrConsulta[133].ToString(), 0));
                    else
                        objConsulta.Audiometria = -1;
                    if (dtrConsulta[134].ToString() != string.Empty)
                        objConsulta.AudiometriaObservaciones = dtrConsulta[134].ToString().Trim();
                    if (dtrConsulta[135].ToString() != string.Empty)
                        objConsulta.RayosX = Convert.ToInt16(IsNull(dtrConsulta[135].ToString(), 0));
                    else
                        objConsulta.RayosX = -1;
                    if (dtrConsulta[136].ToString() != string.Empty)
                        objConsulta.RayosXObservaciones = dtrConsulta[136].ToString().Trim();
                    objConsulta.Miopia = Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[137].ToString(), 0)));
                    if (dtrConsulta[138].ToString() != string.Empty)
                        objConsulta.MiopiaValor = decimal.Parse(dtrConsulta[138].ToString().Trim());
                    if (dtrConsulta[139].ToString() != string.Empty)
                        objConsulta.MiopiaValorOI = decimal.Parse(dtrConsulta[139].ToString().Trim());
                    if (dtrConsulta[140].ToString() != string.Empty)
                        objConsulta.MiopiaObservaciones = dtrConsulta[140].ToString().Trim();
                    objConsulta.Astigmatismo = Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[141].ToString(), 0)));
                    if (dtrConsulta[142].ToString() != string.Empty)
                        objConsulta.AstigmatismoValor = decimal.Parse(dtrConsulta[142].ToString());
                    if (dtrConsulta[143].ToString() != string.Empty)
                        objConsulta.AstigmatismoValorOI = decimal.Parse(dtrConsulta[143].ToString().Trim());
                    if (dtrConsulta[144].ToString() != string.Empty)
                        objConsulta.AstigmatismoObservaciones = dtrConsulta[144].ToString().Trim();
                    objConsulta.Hipermetropia = Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[145].ToString(), 0)));
                    if (dtrConsulta[146].ToString() != string.Empty)
                        objConsulta.HipermetropiaValor = decimal.Parse(dtrConsulta[146].ToString().Trim());
                    if (dtrConsulta[147].ToString() != string.Empty)
                        objConsulta.HipermetropiaValorOI = decimal.Parse(dtrConsulta[147].ToString().Trim());
                    if (dtrConsulta[148].ToString() != string.Empty)
                        objConsulta.HipermetropiaObservaciones = dtrConsulta[148].ToString().Trim();
                    objConsulta.Presbicia = Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[149].ToString(), 0)));
                    if (dtrConsulta[150].ToString() != string.Empty)
                        objConsulta.PresbiciaValor = decimal.Parse(dtrConsulta[150].ToString().Trim());
                    if (dtrConsulta[151].ToString() != string.Empty)
                        objConsulta.PresbiciaValorOI = decimal.Parse(dtrConsulta[151].ToString().Trim());
                    if (dtrConsulta[152].ToString() != string.Empty)
                        objConsulta.PresbiciaObservaciones = dtrConsulta[152].ToString().Trim();
                    objConsulta.OtrosExamenVisual = Convert.ToBoolean(Convert.ToInt16(IsNull(dtrConsulta[153].ToString(), 0)));

                    if (dtrConsulta[154].ToString() != string.Empty)
                    {
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[154].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";

                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objConsulta.IdDiagnosticoExamenVisual = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());

                    }

                    #endregion

                    #region Impresión diagnostica


                    ArrayList arrDiagnosticos = new ArrayList();


                    if (dtrConsulta[155].ToString() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos1 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[155].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos1.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());

                        if (dtrConsulta[156].ToString() != string.Empty)
                            objDiagnosticos1.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[156].ToString());

                        PeriodoEvolucion(objDiagnosticos1, dtrConsulta[157].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                        objDiagnosticos1.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[158].ToString());
                        arrDiagnosticos.Add(objDiagnosticos1);
                    }

                    if (dtrConsulta[159].ToString() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos2 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[159].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos2.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());
                        if (dtrConsulta[160].ToString() != string.Empty)
                            objDiagnosticos2.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[160].ToString());

                        PeriodoEvolucion(objDiagnosticos2, dtrConsulta[161].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                        objDiagnosticos2.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[162].ToString());
                        arrDiagnosticos.Add(objDiagnosticos2);
                    }

                    if (dtrConsulta[163].ToString().Trim() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos3 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[163].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos3.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());
                        if (dtrConsulta[164].ToString() != string.Empty)
                            objDiagnosticos3.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[164].ToString());

                        PeriodoEvolucion(objDiagnosticos3, dtrConsulta[165].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                        objDiagnosticos3.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[166].ToString());
                        arrDiagnosticos.Add(objDiagnosticos3);
                    }


                    if (dtrConsulta[167].ToString() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos4 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[167].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos4.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());
                        if (dtrConsulta[168].ToString() != string.Empty)
                            objDiagnosticos4.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[168].ToString());

                        PeriodoEvolucion(objDiagnosticos4, dtrConsulta[169].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                        objDiagnosticos4.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[170].ToString());
                        arrDiagnosticos.Add(objDiagnosticos4);
                    }


                    if (dtrConsulta[171].ToString() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos5 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[171].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos5.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());

                        if (dtrConsulta[172].ToString() != string.Empty)
                            objDiagnosticos5.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[172].ToString());

                        PeriodoEvolucion(objDiagnosticos5, dtrConsulta[173].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                        objDiagnosticos5.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[174].ToString());
                        arrDiagnosticos.Add(objDiagnosticos5);
                    }


                    if (dtrConsulta[175].ToString() != string.Empty)
                    {
                        ConsultaDiagnosticos objDiagnosticos6 = new ConsultaDiagnosticos();
                        Diagnosticos objDiagnostico = new Diagnosticos();

                        objDiagnostico.CodigoDiagnostico = dtrConsulta[175].ToString().Trim();
                        objDiagnostico.NombreDiagnostico = "";
                        dsDiagnosticos = objDiagnostico.ConsultDiagnosticos();

                        objDiagnosticos6.IdDiagnostico = int.Parse(dsDiagnosticos.Tables[0].Rows[0]["IdDiagnostico"].ToString());
                        if (dtrConsulta[176].ToString() != string.Empty)
                            objDiagnosticos6.TiempoEvolucion = Convert.ToDecimal(dtrConsulta[176].ToString());

                        PeriodoEvolucion(objDiagnosticos6, dtrConsulta[177].ToString());
                        //if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                        //    throw new Exception("Debe seleccionar el tipo de diagnóstico");

                        DataSet ds = objDiagnosticos6.ConsultConsultaDiagnosticos();
                        objDiagnosticos6.IdTipoDiagnostico = Convert.ToInt16(dtrConsulta[178].ToString());
                        arrDiagnosticos.Add(objDiagnosticos6);
                    }

                    if (dtrConsulta[155].ToString().Trim() == string.Empty && dtrConsulta[159].ToString().Trim() == string.Empty && dtrConsulta[163].ToString().Trim() == string.Empty && dtrConsulta[167].ToString().Trim() == string.Empty && dtrConsulta[171].ToString().Trim() == string.Empty && dtrConsulta[175].ToString().Trim() == string.Empty)
                    {
                        //strError = "Debe adicionar al menos un diagnóstico";
                        //return false;
                    }

                    if (!this.validarDiagnosticos(dtrConsulta))
                    {
                        //strError = ("Debe seleccionar diagnosticos diferentes");
                        //return false;
                    }

                    objConsulta.ConsultaDiagnosticos = arrDiagnosticos;

                    #endregion

                    return true;

                }
                else
                {
                    return false;
                }  
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }

        public bool ValidarAsegurados(Consulta objConsulta, DataRow dtrConsulta, ref string strError)
        {
            try
            {
                #region Declaración de variables

                SIC_EMPLEADO objEmpleado;
                DataSet dsAsegurados;
                DataSet dsDiagnosticos;
                bool bolExisteAsegurado;
                string strSeparadorMiles;
                string strNumeroEmpleado;
                string strClaveParentesco;
                string strFechaNacimiento;
                string strNombre;
                string strApellidoPaterno;
                string strApellidoMaterno;
                string strNombreBusqueda;
                string strPeso;
                string strTalla;
                string strTemperatura;
                string strFrecuenciaCar;
                string strFrecuenciaRes;

                #endregion

                #region Inicialización de variables

                objEmpleado = new SIC_EMPLEADO();
                dsAsegurados = new DataSet();
                bolExisteAsegurado = false;
                objConsulta.Empresa_id = Convert.ToInt32(Session["Company"]);
                strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();

                #endregion

                #region Búsqueda de asegurado

                objEmpleado.Primer_nombre = "";
                objEmpleado.Empresa_id = Convert.ToInt32(Session["Company"]);
                objEmpleado.Identificacion = dtrConsulta[0].ToString().Trim();

                dsAsegurados = objEmpleado.ConsultSIC_USUARIOS("", null);

                foreach (DataRow row in dsAsegurados.Tables[0].Rows)
                {
                    strNumeroEmpleado = dtrConsulta[0].ToString().Trim();
                    strClaveParentesco = dtrConsulta[1].ToString();
                    strNombre = dtrConsulta[2].ToString().Trim().ToUpper();
                    strNombreBusqueda = row["primer_nombre"].ToString().Trim().ToUpper();
                    strApellidoPaterno = dtrConsulta[3].ToString().Trim().ToUpper();
                    strApellidoMaterno = dtrConsulta[4].ToString().Trim().ToUpper();
                    strFechaNacimiento = dtrConsulta[5].ToString();


                    if (int.Parse(strClaveParentesco) > 1)
                    {
                        if (strNumeroEmpleado == row["identificacion"].ToString() &&
                           strNombre == strNombreBusqueda &&
                            strClaveParentesco == row["ClaveParentesco"].ToString() &&
                            strFechaNacimiento == row["fecha_nacimiento"].ToString())
                        {
                            objConsulta.Id_empleado = int.Parse(row["id_empleado"].ToString());
                            objConsulta.Beneficiario_id = int.Parse(row["beneficiario_id"].ToString());

                            bolExisteAsegurado = true;

                            break;
                        }
                    }
                    else
                    {
                        if (strNumeroEmpleado == row["identificacion"].ToString() &&
                            strClaveParentesco == row["ClaveParentesco"].ToString())
                        {
                            objConsulta.Id_empleado = int.Parse(row["id_empleado"].ToString());
                            objConsulta.Beneficiario_id = int.Parse(row["beneficiario_id"].ToString());

                            bolExisteAsegurado = true;

                            break;
                        }
                    }
                }

                #endregion

                if (bolExisteAsegurado)
                {                  
                    return true;
                }
                else
                {
                    strError = "El asegurado no existe";
                    return false;
                }
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        
        /// <summary>
        /// Método, valida que no haya diagnosticos iguales seleccionados
        /// </summary>
        /// <returns></returns>
        public bool validarDiagnosticos(DataRow dtrConsulta)
        {
            if (dtrConsulta[155].ToString().Trim() != string.Empty && (dtrConsulta[155].ToString().Trim() == dtrConsulta[159].ToString().Trim() || dtrConsulta[155].ToString().Trim() == dtrConsulta[163].ToString().Trim() || dtrConsulta[155].ToString().Trim() == dtrConsulta[167].ToString().Trim() || dtrConsulta[155].ToString().Trim() == dtrConsulta[171].ToString().Trim() || dtrConsulta[155].ToString().Trim() == dtrConsulta[175].ToString().Trim()))
                return false;
            if (dtrConsulta[159].ToString().Trim() != string.Empty && (dtrConsulta[159].ToString().Trim() == dtrConsulta[155].ToString().Trim() || dtrConsulta[159].ToString().Trim() == dtrConsulta[163].ToString().Trim() || dtrConsulta[159].ToString().Trim() == dtrConsulta[167].ToString().Trim() || dtrConsulta[159].ToString().Trim() == dtrConsulta[171].ToString().Trim() || dtrConsulta[159].ToString().Trim() == dtrConsulta[175].ToString().Trim()))
                return false;
            if (dtrConsulta[163].ToString().Trim() != string.Empty && (dtrConsulta[163].ToString().Trim() == dtrConsulta[159].ToString().Trim() || dtrConsulta[163].ToString().Trim() == dtrConsulta[155].ToString().Trim() || dtrConsulta[163].ToString().Trim() == dtrConsulta[167].ToString().Trim() || dtrConsulta[163].ToString().Trim() == dtrConsulta[171].ToString().Trim() || dtrConsulta[163].ToString().Trim() == dtrConsulta[175].ToString().Trim()))
                return false;
            if (dtrConsulta[167].ToString().Trim() != string.Empty && (dtrConsulta[167].ToString().Trim() == dtrConsulta[159].ToString().Trim() || dtrConsulta[167].ToString().Trim() == dtrConsulta[163].ToString().Trim() || dtrConsulta[167].ToString().Trim() == dtrConsulta[155].ToString().Trim() || dtrConsulta[167].ToString().Trim() == dtrConsulta[171].ToString().Trim() || dtrConsulta[167].ToString().Trim() == dtrConsulta[175].ToString().Trim()))
                return false;
            if (dtrConsulta[171].ToString().Trim() != string.Empty && (dtrConsulta[171].ToString().Trim() == dtrConsulta[159].ToString().Trim() || dtrConsulta[171].ToString().Trim() == dtrConsulta[163].ToString().Trim() || dtrConsulta[171].ToString().Trim() == dtrConsulta[167].ToString().Trim() || dtrConsulta[171].ToString().Trim() == dtrConsulta[155].ToString().Trim() || dtrConsulta[171].ToString().Trim() == dtrConsulta[175].ToString().Trim()))
                return false;
            if (dtrConsulta[175].ToString().Trim() != string.Empty && (dtrConsulta[175].ToString().Trim() == dtrConsulta[159].ToString().Trim() || dtrConsulta[175].ToString().Trim() == dtrConsulta[163].ToString().Trim() || dtrConsulta[175].ToString().Trim() == dtrConsulta[167].ToString().Trim() || dtrConsulta[175].ToString().Trim() == dtrConsulta[171].ToString().Trim() || dtrConsulta[175].ToString().Trim() == dtrConsulta[155].ToString().Trim()))
                return false;
            return true;
        }

        private void PeriodoEvolucion(ConsultaDiagnosticos objConsultaDiagnostico, string strTiempo)
        {
            switch (strTiempo)
            {
                case "1":
                    objConsultaDiagnostico.PeriodoEvolucion = "Días";
                    break;

                case "2":
                    objConsultaDiagnostico.PeriodoEvolucion = "Meses";
                    break;

                case "3":
                    objConsultaDiagnostico.PeriodoEvolucion = "Años";
                    break;

                default:
                    objConsultaDiagnostico.PeriodoEvolucion = "--";
                    break;
            }
        }

        private object IsNull(string strValor, object objNuevoValor)
        {
            if (strValor == "")
            {
                return objNuevoValor;
            }
            else
            {
                return strValor;
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            string errores = "El archivo no tiene el formato correcto";
            try
            {
                #region Declaración de variables

                string strRutaArchivo;
                OleDbDataAdapter OLEda;
                OleDbConnection OLEcn;
                OleDbCommand OLEcommand;
                DataSet dsDatos;
                string str_ConnectionString;
                int idUsuario = Convert.ToInt32(Session["IdUser"]);
                int idEmpresa = Convert.ToInt32(Session["Company"]);
                #endregion

                #region Inicialización de variables

                dsDatos = new DataSet();
                strRutaArchivo = Server.MapPath("../" + lblNombreArchivo.Text.ToString());

                
                str_ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=";
                str_ConnectionString += strRutaArchivo + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                OLEda = new OleDbDataAdapter();
                OLEcn = new OleDbConnection(str_ConnectionString);

                #endregion
                #region Apertura del archivo y obtención de datos

                OLEcn.Open();
                OLEcommand = new OleDbCommand("SELECT * FROM [Hoja1$]", OLEcn);
                OLEda.SelectCommand = OLEcommand;
                OLEda.Fill(dsDatos);
                OLEcn.Close();
                dsDatos.Tables[0].Rows[0].Delete();
                
                //dsDatos.Tables[0].Columns.RemoveAt(0);

                System.IO.File.Delete(strRutaArchivo);

                #endregion
                #region validaciones Excel
                DataSet dsErrores = new DataSet();
                DataTable dtErrores = new DataTable();
                dtErrores.Columns.Add(" Error ");
                dtErrores.Columns.Add(" Descripción ");
                DataRow rowError;
                int numError = 1;
                //valida el número de columnas del archivo de excel
                if (dsDatos.Tables[0].Columns.Count > 8)
                {
                    rowError = dtErrores.NewRow();
                    rowError[" Error "] = (numError++).ToString();
                    rowError[" Descripción "] = "El archivo no contiene el número de columnas correcto.";
                    dtErrores.Rows.Add(rowError);
                }
                if (dsDatos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 1; i < dsDatos.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < dsDatos.Tables[0].Columns.Count; j++)
                        {
                            string valorDato = dsDatos.Tables[0].Rows[i][j].ToString();
   
                            
                            if (j >= 2)
                            {
                                //valida solo números float
                                if (j == 6)
                                {
                                    if (!Regex.IsMatch(valorDato, "^[0-9]([.][0-9]{1,3})?$") || valorDato == string.Empty)
                                    {
                                        rowError = dtErrores.NewRow();
                                        rowError[" Error "] = (numError++).ToString();
                                        rowError[" Descripción "] = "El valor del campo en el registro " + (i + 1).ToString() + " en el nombre de columna '" + dsDatos.Tables[0].Columns[j].ColumnName + "' no tiene formato correcto.";
                                        dtErrores.Rows.Add(rowError);
                                    }
                                    break;
                                }
                                //valida solo números
                                if (!Regex.IsMatch(valorDato, "^[0-9]{0,3}$") && valorDato != string.Empty)
                                {
                                    rowError = dtErrores.NewRow();
                                    rowError[" Error "] = (numError++).ToString();
                                    rowError[" Descripción "] = "El valor del campo en el registro " + (i + 1).ToString() + " en el nombre de columna '" + dsDatos.Tables[0].Columns[j].ColumnName + "' no tiene formato correcto.";
                                    dtErrores.Rows.Add(rowError);
                                }
                            }

                        }
                    }
                    //valida que el empleado pertenezca a la empresa
                    string cadenaEmpleados = "";
                    for (int i = 1; i < dsDatos.Tables[0].Rows.Count; i++)
                    {
                        cadenaEmpleados = cadenaEmpleados + dsDatos.Tables[0].Rows[i][1].ToString() + ",";
                    }
                    ConsultaBiometricos objconsulta = new ConsultaBiometricos();
                    DataSet dsResultadoValidacion = new DataSet();
                    cadenaEmpleados = cadenaEmpleados.Substring(0, cadenaEmpleados.Length - 1);
                    dsResultadoValidacion = objconsulta.ValidaNumeroEmpleadoBiometricos(idEmpresa, cadenaEmpleados);
                    for (int j = 1; j < dsResultadoValidacion.Tables[0].Rows.Count; j++)
                    {
                        rowError = dtErrores.NewRow();
                        rowError[" Error "] = (numError++).ToString();
                        rowError[" Descripción "] = "El número de empleado '" + dsResultadoValidacion.Tables[0].Rows[j][0].ToString() + "' no pertenece a la empresa seleccionada.";
                        dtErrores.Rows.Add(rowError);
                    }
                }
                #endregion
                Session["dsDatos"] = dsDatos;
                if (dtErrores.Rows.Count == 0)
                {
                    EmpresaDatos objEmpresa = new EmpresaDatos();
                    objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
                    objEmpresa.GetEmpresaDatos();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>continuar_confirm("+ dsDatos.Tables[0].Rows.Count+",'"+objEmpresa.AbreviacionEmpresa+"');</script>", false);
                }
                else
                {
                    EstatusProceso.Text = "Error en el proceso de carga <br>";
                    EstatusProceso.Attributes.Add("style", "color:red;");
                    MensajeProceso.Text = "Se encontraron los siguientes errores durante el proceso. <br>";

                    gridView.DataSource = dtErrores;
                    gridView.DataBind();
                    resultados.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + errores + "'); window.open('AE_cargahistorias.aspx','_self') </script>");
            }
        }
       
        //RAM* cambio de proceso de carga
        //protected void btnContinuar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        #region Declaración de variables

        //        string strRutaArchivo;
        //        OleDbDataAdapter OLEda;
        //        OleDbConnection OLEcn;
        //        OleDbCommand OLEcommand;
        //        string str_ConnectionString;
        //        DataSet dsDatos;

        //        #endregion

        //        #region Inicialización de variables

        //        dsDatos = new DataSet();
        //        strRutaArchivo = Server.MapPath("../" + lblNombreArchivo.Text.ToString());


        //        str_ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=";
        //        str_ConnectionString += strRutaArchivo + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
        //        OLEda = new OleDbDataAdapter();
        //        OLEcn = new OleDbConnection(str_ConnectionString);

        //        #endregion

        //        #region Apertura del archivo y obtención de datos

        //        OLEcn.Open();
        //        OLEcommand = new OleDbCommand("SELECT * FROM [Hoja1$]", OLEcn);

        //        OLEda.SelectCommand = OLEcommand;
        //        OLEda.Fill(dsDatos);
        //        OLEcn.Close();

        //        dsDatos.Tables[0].Rows[0].Delete();
        //        dsDatos.Tables[0].Columns.RemoveAt(0);

        //        System.IO.File.Delete(strRutaArchivo);

        //        #endregion


        //        if (this.InsertConsulta(dsDatos))
        //            this.DisplayMessage("Se encontraron errores en el layout");
        //        else
        //            this.DisplayMessage("La carga ha finalizado correctamente");                

        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMessage(ex.ToString());
        //    }

        //}

        private void ExportarExcel(GridView dgDatos)
        {
            try
            {
                #region	Exportación a Excel

                //Se cambia el color del encabezado 
                //dgDatos.Visible = true;
                //dgDatos.HeaderStyle.BackColor = Color.Gray;
                dgDatos.HeaderStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                dgDatos.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                Response.ContentType = "application/vnd.xls";

                Response.Charset = "";
                Response.AddHeader("Content-disposition", "attachment;filename=Reporte" + dgDatos.ID + ".xls");
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                dgDatos.RenderControl(htmlWrite);
                dgDatos.Visible = false;

                #endregion

                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                this.DisplayMessage("Ocurrió un problema al intentar exportar a Excel");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            GridView gridViewE = new GridView();
            gridViewE.DataSource = HttpContext.Current.Session["gridview"];
            gridViewE.DataBind();

            ExportarExcel(gridViewE);
        }

        public bool InsertConsultaBiometricos(DataSet dsConsultas)
        {
            #region Declaración de variables

            long idConsulta;
            int i;
            Consulta objConsulta;
            DataTable dtErrores;
            DataTable dtConsultas;

            DataRow rowError;
            DataRow rowConsulta;

            string strError;
            bool bolErrores;

            #endregion

            #region Inicialización de variables

            dtErrores = new DataTable();
            dtConsultas = new DataTable();

            dtErrores.Columns.Add("NumeroRegistro", Type.GetType("System.String"));
            dtErrores.Columns.Add("Error", Type.GetType("System.String"));
            dtConsultas.Columns.Add("IdConsulta", Type.GetType("System.String"));

            strError = "";

            bolErrores = false;
            i = 0;

            #endregion

            //Se procede a realizar la inserción de las consultas
            i = 0;

            foreach (DataRow row in dsConsultas.Tables[0].Rows)
            {
                if (i > 0)
                {
                    objConsulta = new Consulta();

                    if (this.LoadObjectConsulta(objConsulta, row, ref strError)) //Se llena el objeto consulta
                    {
                        idConsulta = objConsulta.InsertConsulta();     //Se inserta la consulta                   
                        rowConsulta = dtConsultas.NewRow();
                        rowConsulta["IdConsulta"] = idConsulta.ToString();

                        dtConsultas.Rows.Add(rowConsulta);

                    }
                    else
                    {
                        //Se registran los errores
                        rowError = dtErrores.NewRow();
                        rowError["NumeroRegistro"] = i.ToString();
                        rowError["Error"] = strError;

                        dtErrores.Rows.Add(rowError);
                        bolErrores = true;
                        //RAM* lblErrores.Visible = true;
                    }
                }
                i++;
            }

            //Se enlazan los errores existentes
            //RAM* ya no cargara esta informacion
            //gvErrores.DataSource = dtErrores;
            //gvErrores.DataBind();

            //dgConsultasCargadas.DataSource = dtConsultas;
            //dgConsultasCargadas.DataBind();
            //dgConsultasCargadas.Visible = true;
            //lblConsultasCargadas.Visible = true;

            return bolErrores;
        }


        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //RAM* colores
            int par = e.Row.RowIndex;
            if ((par % 2)==0)
                e.Row.BackColor = System.Drawing.Color.FromName("#F7F7F7");
            else
                e.Row.BackColor = System.Drawing.Color.White;

        }
        protected void gridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //RAM* colores
            int par = e.Row.RowIndex;
            if ((par % 2) == 0)
                e.Row.BackColor = System.Drawing.Color.FromName("#F7F7F7");
            else
                e.Row.BackColor = System.Drawing.Color.White;

        }

        [WebMethod]
        public static Dictionary<string,string> continuar()
        {
            AE_CargaHistorias frm1 = new AE_CargaHistorias();
            frm1.gridView = new System.Web.UI.WebControls.GridView();
            frm1.resultados = new System.Web.UI.HtmlControls.HtmlTableRow();
            frm1.EstatusProceso = new System.Web.UI.WebControls.Label();
            frm1.btnExcel = new System.Web.UI.WebControls.Button();
            frm1.lblAccion = new System.Web.UI.WebControls.Label();

            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUser"]);
            int idEmpresa = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            //this.DisplayMessage("Se realizará la carga de " + dsDatos.Tables[0].Rows.Count.ToString() + " registros de información Biométrica en la empresa " + Session["Company"].ToString());
            //RAM* Llenado de tabla dbo.cargarBiometricosExcel
            ConsultaBiometricos objBiometricos = new ConsultaBiometricos();
            DataTable dataTable = new DataTable();
            DataSet dsResultado = new DataSet();
            Guid idGuid;
            idGuid = Guid.NewGuid();
            string identificadorBorrar = idGuid.ToString();
            DataSet dsDatos = (DataSet)HttpContext.Current.Session["dsDatos"];
            dataTable = dsDatos.Tables[0];

            System.Data.DataColumn nuevaColumna = new System.Data.DataColumn("identificadorBorrar", typeof(System.String));
            nuevaColumna.DefaultValue = identificadorBorrar;
            dataTable.Columns.Add(nuevaColumna);
            string strCadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString.ToString();
            //carga archivo de excel a tabla cargarBiometricosExcel
            SqlBulkCopy sqlBulk = new SqlBulkCopy(strCadenaConexion);
            sqlBulk.DestinationTableName = "cargarBiometricosExcel";
            sqlBulk.WriteToServer(dataTable);

            dsResultado = objBiometricos.InsertarBiometricos(idEmpresa, idUsuario, identificadorBorrar);
            HttpContext.Current.Session["gridview"] = dsResultado.Tables[0];
            frm1.gridView.DataSource = dsResultado.Tables[0];
            frm1.gridView.DataBind();
            frm1.resultados.Visible = true;

            frm1.EstatusProceso.Text = "Carga exitosa:";
            //frm1.EstatusProceso.Attributes.Add("style", "color:green;");
            //frm1.MensajeProceso.Text = "Se han generado las siguientes consultas";
            //frm1.btnExcel.Visible = true;

            //RAM* Registra carga bitacora
            objBiometricos.InsertarBitacoraCargaExcel(idEmpresa, idUsuario);
            frm1.lblAccion.Visible = false;
            Dictionary<string, string> datos = new Dictionary<string, string>();
            foreach (DataRow row in dsResultado.Tables[0].Rows)
            {
                datos[row[0].ToString()] = row[1].ToString();
            }
            return datos;
        }

        //public void carga()
        //{

        //    string errores = "El archivo no tiene el formato correcto";
        //    try
        //    {
        //        #region Declaración de variables

        //        string strRutaArchivo;
        //        OleDbDataAdapter OLEda;
        //        OleDbConnection OLEcn;
        //        OleDbCommand OLEcommand;
        //        DataSet dsDatos;
        //        string str_ConnectionString;
        //        int idUsuario = Convert.ToInt32(Session["IdUser"]);
        //        int idEmpresa = Convert.ToInt32(Session["Company"]);
        //        #endregion

        //        #region Inicialización de variables

        //        dsDatos = new DataSet();
        //        strRutaArchivo = Server.MapPath("../" + lblNombreArchivo.Text.ToString());

        //        str_ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=";
        //        str_ConnectionString += strRutaArchivo + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
        //        OLEda = new OleDbDataAdapter();
        //        OLEcn = new OleDbConnection(str_ConnectionString);
        //        #endregion
        //        #region Apertura del archivo y obtención de datos
        //        OLEcn.Open();
        //        OLEcommand = new OleDbCommand("SELECT * FROM [Hoja1$]", OLEcn);
        //        OLEda.SelectCommand = OLEcommand;
        //        OLEda.Fill(dsDatos);
        //        OLEcn.Close();
        //        //dsDatos.Tables[0].Rows[0].Delete();
        //        //dsDatos.Tables[0].Columns.RemoveAt(0);

        //        System.IO.File.Delete(strRutaArchivo);

        //        #endregion
        //        #region validaciones Excel
        //        DataSet dsErrores = new DataSet();
        //        DataTable dtErrores = new DataTable();
        //        dtErrores.Columns.Add(" Error ");
        //        dtErrores.Columns.Add(" Descripción ");
        //        DataRow rowError;
        //        int numError = 1;
        //        //valida el número de columnas del archivo de excel
        //        if (dsDatos.Tables[0].Columns.Count > 12)
        //        {
        //            rowError = dtErrores.NewRow();
        //            rowError[" Error "] = (numError++).ToString();
        //            rowError[" Descripción "] = "El archivo no contiene el número de columnas correcto.";
        //            dtErrores.Rows.Add(rowError);
        //        }
        //        if (dsDatos.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dsDatos.Tables[0].Rows.Count; i++)
        //            {
        //                for (int j = 0; j < dsDatos.Tables[0].Columns.Count; j++)
        //                {
        //                    string valorDato = dsDatos.Tables[0].Rows[i][j].ToString();


        //                    if (j >= 6)
        //                    {
        //                        //valida solo números float
        //                        if (j == 10)
        //                        {
        //                            if (!Regex.IsMatch(valorDato, "^[0-9]([.][0-9]{1,3})?$") || valorDato == string.Empty)
        //                            {
        //                                rowError = dtErrores.NewRow();
        //                                rowError[" Error "] = (numError++).ToString();
        //                                rowError[" Descripción "] = "El valor del campo en el registro " + (i + 1).ToString() + " en el nombre de columna '" + dsDatos.Tables[0].Columns[j].ColumnName + "' no tiene formato correcto.";
        //                                dtErrores.Rows.Add(rowError);
        //                            }
        //                            break;
        //                        }
        //                        //valida solo números
        //                        if (!Regex.IsMatch(valorDato, "^[0-9]{0,3}$") && valorDato != string.Empty)
        //                        {
        //                            rowError = dtErrores.NewRow();
        //                            rowError[" Error "] = (numError++).ToString();
        //                            rowError[" Descripción "] = "El valor del campo en el registro " + (i + 1).ToString() + " en el nombre de columna '" + dsDatos.Tables[0].Columns[j].ColumnName + "' no tiene formato correcto.";
        //                            dtErrores.Rows.Add(rowError);
        //                        }
        //                    }

        //                }
        //            }
        //            //valida que el empleado pertenezca a la empresa
        //            string cadenaEmpleados = "";
        //            for (int i = 0; i < dsDatos.Tables[0].Rows.Count; i++)
        //            {
        //                cadenaEmpleados = cadenaEmpleados + dsDatos.Tables[0].Rows[i][1].ToString() + ",";
        //            }
        //            ConsultaBiometricos objconsulta = new ConsultaBiometricos();
        //            DataSet dsResultadoValidacion = new DataSet();
        //            cadenaEmpleados = cadenaEmpleados.Substring(0, cadenaEmpleados.Length - 1);
        //            dsResultadoValidacion = objconsulta.ValidaNumeroEmpleadoBiometricos(idEmpresa, cadenaEmpleados);
        //            for (int j = 0; j < dsResultadoValidacion.Tables[0].Rows.Count; j++)
        //            {
        //                rowError = dtErrores.NewRow();
        //                rowError[" Error "] = (numError++).ToString();
        //                rowError[" Descripción "] = "El número de empleado '" + dsResultadoValidacion.Tables[0].Rows[j][0].ToString() + "' no pertenece a la empresa seleccionada.";
        //                dtErrores.Rows.Add(rowError);
        //            }
        //        }
        //        #endregion
        //        Session["dsDatos"] = dsDatos;
        //        if (dtErrores.Rows.Count == 0)
        //        {
        //            EmpresaDatos objEmpresa = new EmpresaDatos();
        //            objEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
        //            objEmpresa.GetEmpresaDatos();
        //            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>continuar_confirm(" + dsDatos.Tables[0].Rows.Count + ",'" + objEmpresa.AbreviacionEmpresa + "');</script>", false);
        //        }
        //        else
        //        {
        //            EstatusProceso.Text = "Error en el proceso de carga <br>";
        //            EstatusProceso.Attributes.Add("style", "color:red;");
        //            MensajeProceso.Text = "Se encontraron los siguientes errores durante el proceso. <br>";

        //            gridView.DataSource = dtErrores;
        //            gridView.DataBind();
        //            resultados.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('" + errores +  "'); window.open('AE_cargahistorias.aspx','_self') </script>");
        //    }
        //}
    }
}
