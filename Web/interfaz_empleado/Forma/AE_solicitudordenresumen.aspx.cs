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
    /// Descripción breve de AE_solicitudautorizacionresumen.
    /// </summary>
    public partial class AE_solicitudordenresumen : PB_PaginaBase
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
                    Response.Write("<script>window.parent.scrollTo(0,0);</script>");

                    txtFechaInicioIncapacidad.Attributes.Add("ReadOnly", "ReadOnly");
                    txtFechaFinIncapacidad.Attributes.Add("ReadOnly", "ReadOnly");

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    }

                    //txtFechaAvisoPrivacidad.Attributes.Add("ReadOnly", "ReadOnly");  

                    if (Request.QueryString["Resumen"] != null)
                    {
                        FIELDSET1.Visible = false;
                        this.btnFinalizar.Visible = false;
                        this.fldFormatos.Style["display"] = "none";
                        string script = "";
                        script = "<script language='javascript'>window.print();</script>";


                        //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                        if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "print", script);
                        }
                        //Fin

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

        public void LoadFormSolicitud(long p_idSolicitud)
        {
            Solicitud objSolicitud = new Solicitud();
            GeneralTable objGeneral = new GeneralTable();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();
            this.btnAnterior.Visible = false; //Marsh - JFEE - 2014/10/10 - Se pone en false la visibilidad del botón anterior para que no se muestre en el resumen

            ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
            objConsultaOpcion.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsultaOpcion.IdPreguntaRespuestaPadre = 168;
            DataTable dtProgramas = objConsultaOpcion.ConsultConsultaOpcionPadre().Tables[0];


            if (objSolicitud.Observaciones != string.Empty || dtProgramas.Rows.Count > 0)
            {
                this.txtRecomendaciones.Text = objSolicitud.Observaciones;
                this.trRecomendaciones.Style["display"] = "";
                this.tblRecomendaciones.Style["display"] = "";
            }

            //Inicio Ricardo Silva 05/10/2011
            //Se insertan los campos del aviso de privacidad 

            //descomentariar para agregar una alerta al final de la consulta del aviso de privacidad

            //Consulta objConsultaPrivacidad = new Consulta();
            //objConsultaPrivacidad.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            //objConsultaPrivacidad.GetConsulta();
            //int beneficiarioID;
            //int EmpleadoID;
            //beneficiarioID = Convert.ToInt32(objConsultaPrivacidad.Beneficiario_id);
            //EmpleadoID = Convert.ToInt32(objConsultaPrivacidad.Id_empleado);

            //SIC_PRIVACIDAD Privacidad = new SIC_PRIVACIDAD();
            //Privacidad.GetSIC_PRIVACIDAD(beneficiarioID, EmpleadoID);
            //string fechaUltimaFirma;

            //if (Privacidad.fechaUltimaFirma != null)
            //{
            //    trAvisoPrivacidad.Style["display"] = "none";
            //    //lblfechafirma.Visible = true;                
            //    //LblUltimaFirma.Visible = true;
            //    //fechaUltimaFirma = Convert.ToString(Privacidad.fechaUltimaFirma);
            //    //fechaUltimaFirma = fechaUltimaFirma.Substring(0,10);
            //    //LblUltimaFirma.Text = fechaUltimaFirma;                
            //}
            //else
            //{
            //    trAvisoPrivacidad.Style["display"] = "";
            //    lblNoFechaFirma.Visible = true;
            //}
            
            
            //Fin Ricardo Silva

            this.lblTotalFacturas.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalFactura);
            this.lblTotalProductosServicios.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalConvenioSolicitado);
            this.lblTotalAprobado.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalAprobado);

            objGeneral.TableName = "SolicitudEstados";
            objGeneral.ColumnName = "SolicitudEstado";
            objGeneral.Id = objSolicitud.IdSolicitudEstado;
            objGeneral.GetGeneralTable();

            ViewState["IdPresupuestoIndividuo"] = objSolicitud.IdPresupuestoIndividuo;
            ViewState["IdPresupuestoEmpresa"] = objSolicitud.IdPresupuestoEmpresa;

            //Cargar los tipos de servicio
            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (Request.QueryString["IdSolicitudTipoServicio"] != null && Request.QueryString["IdSolicitudTipoServicio"] != string.Empty)
                objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]);
            DataTable dtTipoServicio = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];
            this.dtlTipoServicio.DataSource = dtTipoServicio;
            DataTable dtTipoServicioFormatos = objTipoServicio.ConsultSolicitudTipoServicioFormatos().Tables[0];
            this.dtlFormatos.DataSource = dtTipoServicioFormatos;
            this.dtlTipoServicio.DataBind();
            this.dtlFormatos.DataBind();

            this.hplFormato.NavigateUrl = "javascript:openPopUp('AE_solicitudordenresumen.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&Resumen=1',900,700)";
            //this.hplFormato.NavigateUrl = "javascript:window.print();";
            this.hplIncapacidad.NavigateUrl = "javascript:openPopUp('../Formatos/FO_IncapacidadOrden.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&Imprimir=1&IdConsulta=" + Request.QueryString["IdConsulta"] + "',829,900)";
            this.hplRecomendaciones.NavigateUrl = "javascript:openPopUp('../Formatos/FO_RecomendacionesOrden.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&Imprimir=1&IdConsulta=" + Request.QueryString["IdConsulta"] + "',829,900)";
            this.hplHistoria.NavigateUrl = "javascript:openPopUp('../Formatos/FO_HistoriaClinica.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&Imprimir=1&IdConsulta=" + Request.QueryString["IdConsulta"] + "',829,900)";

            this.LoadIncapacidad(p_idSolicitud);

            /*Inicio MAHG 19/07/2010
             Se agrega el botón cerrar para los tipos de consultas "Wellness Primera Vez"
             <--*/

            Consulta objconsulta = new Consulta();

            objconsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objconsulta.GetConsulta();

            if (objconsulta.IdTipoConsulta == 3 && !objconsulta.Finalizada)
            {
                pnlCerrarHistoria.Visible = true;
                //btnFinalizar.Text = "Continuar";
            }
            else
            {
                pnlCerrarHistoria.Visible = false;
            }

            //-->Fin MAHG 19/07/2010
        }

        /// <summary>
        /// Carga el listado de diagnósticos en el label
        /// </summary>
        /// <param name="p_lblIdDiagnosticos"></param>
        /// <param name="p_lblDiagnostico"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelDiagnosticos(Label p_lblDiagnostico, long p_idSolicitudTipoServicio)
        {
            SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
            objDiagnostico.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objDiagnostico.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

            foreach (DataRow dr in dtDiagnosticos.Rows)
            {
                int lenght = dr["NombreDiagnostico"].ToString().Length;

                if (lenght > 25)
                    lenght = 25;

                p_lblDiagnostico.Text += dr["NombreDiagnostico"].ToString().Substring(0, lenght) + "... -" + dr["TiempoEvolucion"].ToString() + " " + dr["PeriodoEvolucion"].ToString() + "</br>";

            }
        }


        /// <summary>
        /// Carga el listado de proveedores
        /// </summary>
        /// <param name="p_lblIdProveedores"></param>
        /// <param name="p_lblProveedores"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelProveedores(Label p_lblProveedores, long p_idSolicitudTipoServicio, Label p_lblEspecialidad, int p_idEspecialidad)
        {
            SolicitudTipoServicioProveedores objProveedor = new SolicitudTipoServicioProveedores();
            objProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtProveedores = objProveedor.ConsultSolicitudTipoServicioProveedores().Tables[0];

            if (dtProveedores.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProveedores.Rows)
                {
                    p_lblProveedores.Text += "-" + dr["NombreProveedor"].ToString() + "-" + dr["Direcciones"].ToString() + " Tel " + dr["Telefonos"].ToString() + "-" + dr["Horario"].ToString() + "</br>";

                }
            }
            else
            {
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.Id = p_idEspecialidad;
                objGeneral.TableName = "Especialidades";
                objGeneral.ColumnName = "Especialidad";
                objGeneral.GetGeneralTable();
                p_lblEspecialidad.Text = objGeneral.Nombre;
            }


        }

        /// <summary>
        /// Carga el listado de diagnósticos de la incapacidad en el label
        /// </summary>
        /// <param name="p_lblIdDiagnosticos"></param>
        /// <param name="p_lblDiagnostico"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelDiagnosticosIncapacidad(long p_idIncapacidad)
        {
            IncapacidadDiagnosticos objDiagnosticos = new IncapacidadDiagnosticos();
            objDiagnosticos.IdIncapacidad = p_idIncapacidad;
            DataTable dtDiagnosticos = objDiagnosticos.ConsultIncapacidadDiagnosticos().Tables[0];

            foreach (DataRow dr in dtDiagnosticos.Rows)
            {
                int lenght = dr["NombreDiagnostico"].ToString().Length;

                if (lenght > 50)
                    lenght = 50;

                this.lblDiagnosticosIncapacidad.Text += dr["NombreDiagnostico"].ToString().Substring(0, lenght) + "... -" + dr["TiempoEvolucion"].ToString() + " " + dr["PeriodoEvolucion"].ToString() + "</br>";

            }
        }


        /// <summary>
        /// Método, carga la información de la incapacidad
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void LoadIncapacidad(long p_idSolicitud)
        {
            Incapacidad objIncapacidad = new Incapacidad();
            objIncapacidad.IdSolicitud = p_idSolicitud;
            objIncapacidad.GetIncapacidad();

            if (objIncapacidad.IdIncapacidad != 0)
            {
                this.txtFechaInicioIncapacidad.Text = objIncapacidad.FechaInicio.ToShortDateString();
                this.txtFechaFinIncapacidad.Text = objIncapacidad.FechaFin.ToShortDateString();
                this.chkContinuacion.Checked = objIncapacidad.Continuacion;
                this.chkTranscripcion.Checked = objIncapacidad.Transcripcion;
                this.lblObservacionesIncapacidad.Text = objIncapacidad.Observaciones;
                this.LoadLabelDiagnosticosIncapacidad(objIncapacidad.IdIncapacidad);
                this.trIncapacidad.Style["display"] = "";
                this.tblIncapacidad.Style["display"] = "";
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
            this.dtlTipoServicio.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlTipoServicio_ItemDataBound);
            this.dtlFormatos.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtlFormatos_ItemCommand);
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        public void dtgProductoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DataGrid dtgProductoServicio = (DataGrid)sender;
                Label lblIdTipoServicio = (Label)dtgProductoServicio.Parent.FindControl("lblIdTipoServicio");

                if (lblIdTipoServicio.Text != "0")
                {
                    TipoServicios objTipoServicio = new TipoServicios();
                    objTipoServicio.IdTipoServicio = Convert.ToInt32(lblIdTipoServicio.Text);
                    objTipoServicio.GetTipoServicios();

                    e.Item.Cells[0].Text = objTipoServicio.EtiquetaProductoServicio;
                    e.Item.Cells[1].Text = objTipoServicio.EtiquetaCantidad;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (Request.QueryString["IdSolicitud"] != null)
                {
                    Label lblDosis = (Label)e.Item.FindControl("lblDosis");
                    Label lblViaAdministracion = (Label)e.Item.FindControl("lblViaAdministracion");
                    Label lblDuracion = (Label)e.Item.FindControl("lblDuracion");
                    Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");
                    Label lblDescuento = (Label)e.Item.FindControl("lblDescuento");
                    Label lblUVR = (Label)e.Item.FindControl("lblUVR");
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    if (!Convert.IsDBNull(rowItem["Dosis"]) && rowItem["Dosis"].ToString() != string.Empty)
                    {
                        lblDosis.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["ViaAdministracion"]) && rowItem["ViaAdministracion"].ToString() != string.Empty)
                    {
                        lblViaAdministracion.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["Duracion"]) && rowItem["Duracion"].ToString() != string.Empty)
                    {
                        lblDuracion.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["ValorAprobado"]))
                    {
                        lblValorAprobado.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["Descuento"]))
                    {
                        lblDescuento.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["UVR"]))
                    {
                        lblUVR.Visible = true;
                    }

                }
            }
        }

        private void dtlTipoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Cargar los datos si viene el id de la solicitud
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                    Label lblProveedor = (Label)e.Item.FindControl("lblProveedor");
                    Label lblEspecialidad = (Label)e.Item.FindControl("lblEspecialidad");
                    Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");

                    //Cargar diagnosticos y prestadores
                    LoadLabelDiagnosticos(lblDiagnosticos, ((long)rowItem["IdSolicitudTipoServicio"]));
                    LoadLabelProveedores(lblProveedor, ((long)rowItem["IdSolicitudTipoServicio"]), lblEspecialidad, ((int)rowItem["IdEspecialidad"]));

                    TextBox txtConsecutivoNombre = (TextBox)e.Item.FindControl("txtConsecutivoNombre");
                    txtConsecutivoNombre.Attributes.Add("ReadOnly", "ReadOnly");

                    //Inicio PETF 14/01/10 ReadOnly
                    TextBox txtComentariosTipoServicio = (TextBox)e.Item.FindControl("txtComentariosTipoServicio");
                    txtComentariosTipoServicio.Attributes.Add("ReadOnly", "ReadOnly");

                    TextBox txtComentariosAnulacion = (TextBox)e.Item.FindControl("txtComentariosAnulacion");
                    txtComentariosAnulacion.Attributes.Add("ReadOnly", "ReadOnly");
                    //Fin PETF 14/01/10

                    //Cargar los servicio
                    DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                    SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                    objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                    objSolicitudServicio.IdSolicitudTipoServicio = ((long)rowItem["IdSolicitudTipoServicio"]);
                    if (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty)
                    {
                        HtmlGenericControl divDataList = (HtmlGenericControl)e.Item.FindControl("divDataList");

                        if (((long)rowItem["IdSolicitudTipoServicio"]) != Convert.ToInt64(Request.QueryString["liquidacionConfirmacion"]))
                        {
                            divDataList.Style["display"] = "none";
                        }

                        SolicitudLiquidacion objLiquidacion = new SolicitudLiquidacion();
                        objLiquidacion.IdSolicitud = objSolicitudServicio.IdSolicitud;
                        objLiquidacion.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["liquidacionConfirmacion"]);
                        if (Session["SICAU"] != null)
                            objLiquidacion.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
                        else
                            objLiquidacion.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
                        objLiquidacion.InsertSolicitudLiquidacion();
                        Session["Confirmados"] = 1;
                    }

                    if (!Convert.IsDBNull(rowItem["ObservacionAnulacion"]) && rowItem["ObservacionAnulacion"].ToString() != string.Empty)
                    {
                        txtComentariosAnulacion = (TextBox)e.Item.FindControl("txtComentariosAnulacion");
                        HtmlTableRow trComentariosAnulacion = (HtmlTableRow)e.Item.FindControl("trComentariosAnulacion");
                        trComentariosAnulacion.Style["display"] = "";
                        txtComentariosAnulacion.Text = rowItem["ObservacionAnulacion"].ToString();
                    }
                    dtgProductoServicio.DataSource = objSolicitudServicio.ConsultSolicitudServicio();
                    dtgProductoServicio.DataBind();

                }
            }
        }

        protected void btnFinalizar_Click(object sender, System.EventArgs e)
        {

            ////Inicio Ricardo Silva 05/10/2011
            ////Se insertan los campos del aviso de privacidad 

            //Consulta objConsultaPrivacidad = new Consulta();
            //objConsultaPrivacidad.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            //objConsultaPrivacidad.GetConsulta();
            //int beneficiarioID;
            //beneficiarioID = Convert.ToInt32(objConsultaPrivacidad.Beneficiario_id);            

            //SIC_PRIVACIDAD objPrivacidad = new SIC_PRIVACIDAD();
            //objPrivacidad.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);

            //if (beneficiarioID != null && beneficiarioID > 0)
            //    objPrivacidad.Beneficiario_id = beneficiarioID;
            //else
            //    objPrivacidad.Beneficiario_id = -1;

            //if (chkAvisoPrivacidad.Checked == true)
            //{
            //    objPrivacidad.Firma = true;
            //}
            //else
            //{
            //    objPrivacidad.Firma = false;
            //}

            
            //if (chkAvisoPrivacidad.Checked == true) 
            //{
            //    objPrivacidad.Fecha_firma = Convert.ToDateTime(txtFechaAvisoPrivacidad.Text);
            //    objPrivacidad.InsertSIC_PRIVACIDAD(); 
            //}
            

            ////Fin Ricardo Silva 05/10/2011


            if (pnlCerrarHistoria.Visible && rdbCerrarHistoria.SelectedValue == "1")
            {
                Consulta objConsulta = new Consulta();

                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
                objConsulta.Finalizada = true;

                objConsulta.FinalizarConsulta();
            }
            if (pnlCerrarHistoria.Visible && rdbCerrarHistoria.SelectedValue == "0")
            {
                Consulta objConsulta = new Consulta();

                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
                objConsulta.Finalizada = false;

                objConsulta.FinalizarConsulta();
            }
            if (!pnlCerrarHistoria.Visible)
            {
                Consulta objConsulta = new Consulta();

                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
                objConsulta.Finalizada = true;

                objConsulta.FinalizarConsulta();
            }


            if ((Request.QueryString["popup"] == null || Request.QueryString["popup"] == string.Empty) && (Request.QueryString["liquidacionConfirmacion"] == null || Request.QueryString["liquidacionConfirmacion"] == string.Empty))
                //Response.Redirect("LIS_empleado.aspx");
                Response.Redirect("LIS_consulta.aspx"); //Marsh - JFEE - 2014/11/26 - Correcciones generales, al finalizar una consulta siempre debe direccionar a la página de consultas
            else
            {
                if (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty)
                {
                    Response.Write("<script>alert('La solicitud fue modificada exitosamente'); window.opener.ActualizarConfirmadas(); top.close();</script>");
                }
                else
                {
                    if (Request.QueryString["reload"] != null && Request.QueryString["reload"] != string.Empty)
                        Response.Write("<script>alert('La solicitud fue modificada exitosamente'); window.opener.ActualizarConfirmadas(); top.close();</script>");
                    else
                        Response.Write("<script>alert('La solicitud fue modificada exitosamente'); top.close();</script>");
                }
            }
        }



        /// <summary>
        /// Evento, realiza el llamado a la impresión o exportación del formato
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtlFormatos_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {

            if (e.CommandName == "Imprimir")
            {
                SolicitudTipoServicio objSolicitudTipoServicio = new SolicitudTipoServicio();
                objSolicitudTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                objSolicitudTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(e.CommandArgument.ToString());
                objSolicitudTipoServicio.UpdateSolicitudTipoServicioImpresiones();

                Label lblUrlFormato = (Label)e.Item.FindControl("lblUrlFormato");
                Label lblIdProveedor = (Label)e.Item.FindControl("lblIdProveedor");
                if (lblIdProveedor.Text == "")
                    lblIdProveedor.Text = "0";
                this.OpenWindow(lblUrlFormato.Text + "?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdProveedor=" + lblIdProveedor.Text, 829, 900);
            }
            if (e.CommandName == "Exportar")
            {
                Label lblUrlFormato = (Label)e.Item.FindControl("lblUrlFormato");
                Label lblIdProveedor = (Label)e.Item.FindControl("lblIdProveedor");
                if (lblIdProveedor.Text == "")
                    lblIdProveedor.Text = "0";
                this.OpenWindow(lblUrlFormato.Text + "?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdProveedor=" + lblIdProveedor.Text + "exportar=S", 829, 900);
            }

            //Cargar de nuevo la grilla actualizando impresiones
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);

            DataTable dtTipoServicioFormatos = objTipoServicio.ConsultSolicitudTipoServicioFormatos().Tables[0];
            this.dtlFormatos.DataSource = dtTipoServicioFormatos;
            this.dtlFormatos.DataBind();
        }

        /// <summary>
        /// Evento, realiza el llamado a la impresión o exportación del formato
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dtlFormatosControl_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Imprimir")
            {
                SolicitudTipoServicio objSolicitudTipoServicio = new SolicitudTipoServicio();
                objSolicitudTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                objSolicitudTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(e.CommandArgument.ToString());
                objSolicitudTipoServicio.UpdateSolicitudTipoServicioImpresiones();

                Label lblUrlFormatoControl = (Label)e.Item.FindControl("lblUrlFormatoControl");
                Label lblIdSolicitudServicio = (Label)e.Item.FindControl("lblIdSolicitudServicio");
                this.OpenWindow(lblUrlFormatoControl.Text + "?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdSolicitudServicio=" + lblIdSolicitudServicio.Text, 829, 900);
            }
            if (e.CommandName == "Exportar")
            {
                Label lblUrlFormato = (Label)e.Item.FindControl("lblUrlFormato");
                Label lblIdSolicitudServicio = (Label)e.Item.FindControl("lblIdSolicitudServicio");
                this.OpenWindow(lblUrlFormato.Text + "?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdSolicitudServicio=" + lblIdSolicitudServicio.Text + "exportar=S", 829, 900);
            }

            //Cargar de nuevo la grilla actualizando impresiones
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);

            DataTable dtTipoServicioFormatosControl = objTipoServicio.ConsultSolicitudTipoServicioFormatosControl().Tables[0];
            this.dtlFormatosControl.DataSource = dtTipoServicioFormatosControl;
            this.dtlFormatosControl.DataBind();

        }


        /// <summary>
        /// Retornar al paso anterio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AE_solicitudorden.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=" + Request.QueryString["editar"] + "&adicion=" + Request.QueryString["adicion"]);
        }



        #endregion

        
        
    }
}
