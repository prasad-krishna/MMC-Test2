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
    /// Descripción breve de AE_solicitudreembolsoresumen.
    /// </summary>
    public partial class AE_solicitudreembolsoresumen : PB_PaginaBase
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


                    //Inicio MAHG 12/01/10
                    //Se agrega el atributo readonly 
                    txtFecha.Attributes.Add("ReadOnly", "ReadOnly");
                    txtUsuario.Attributes.Add("ReadOnly", "ReadOnly");
                    txtPrestador.Attributes.Add("ReadOnly", "ReadOnly");
                    txtMes.Attributes.Add("ReadOnly", "ReadOnly");
                    txtAno.Attributes.Add("ReadOnly", "ReadOnly");
                    txtFormaPago.Attributes.Add("ReadOnly", "ReadOnly");
                    txtEstadoSoli.Attributes.Add("ReadOnly", "ReadOnly");
                    txtDiagnostico.Attributes.Add("ReadOnly", "ReadOnly");
                    txtComentarioSolicitud.Attributes.Add("ReadOnly", "ReadOnly");
                    //Fin MAHG 12/01/10

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    }
                    if (Request.QueryString["Resumen"] != null)
                    {
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
            DataTable dtBeneficiarios;
            Solicitud objSolicitud = new Solicitud();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            GeneralTable objGeneral = new GeneralTable();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();

            if (objSolicitud.Beneficiario_id != 0)
            {
                objBeneficiario.Opcion = 2;
                objBeneficiario.Beneficiario_id = objSolicitud.Beneficiario_id;
                dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                this.txtUsuario.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
            }
            else
            {
                objEmpleado.Id_empleado = objSolicitud.Id_empleado;
                objEmpleado.GetSIC_EMPLEADO();
                this.txtUsuario.Text = objEmpleado.Nombre_completo;
            }

            this.txtFecha.Text = objSolicitud.Fecha.ToShortDateString();
            this.txtComentarioSolicitud.Text = objSolicitud.Observaciones;
            this.txtDocumentos.Text = objSolicitud.Documentos;
            this.lblNoSolicitud.Text = objSolicitud.ConsecutivoNombre;
            this.lblFechaCreacion.Text = objSolicitud.FechaCreacion.ToShortDateString();
            if (objSolicitud.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objSolicitud.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                this.lblUsuarioCreacion.Text = dtUsuario.Rows[0]["nombre"].ToString();
            }
            else
            {
                this.lblUsuarioCreacion.Text = objSolicitud.NameUser;
            }

            this.lblTotalFacturas.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalFactura);
            this.lblTotalProductosServicios.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalConvenioSolicitado);
            this.lblTotalAprobado.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalAprobado);

            if (objSolicitud.IdDiagnostico != 0)
            {
                objGeneral.TableName = "Diagnosticos";
                objGeneral.ColumnName = "Diagnostico";
                objGeneral.Id = Convert.ToInt32(objSolicitud.IdDiagnostico);
                objGeneral.GetGeneralTable();
                this.txtIdDiagnostico.Text = objSolicitud.IdDiagnostico.ToString();
                this.txtDiagnostico.Text = objGeneral.Nombre;
            }
            if (objSolicitud.IdPrestador != 0)
            {

                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = objSolicitud.IdPrestador;
                objGeneral.GetGeneralTable();
                this.txtPrestador.Text = objGeneral.Nombre;

                //Cargar la especialidad
                Prestadores objPrestador = new Prestadores();
                objPrestador.IdPrestador = objSolicitud.IdPrestador;
                objPrestador.GetPrestadores();
                this.lblEspecialidad.Text = objPrestador.NombreEspecialidad;
            }
            if (objSolicitud.MesLiquidacion != 0)
                this.txtMes.Text = objSolicitud.MesLiquidacion.ToString();
            if (objSolicitud.AnoLiquidacion != 0)
            {

                this.txtAno.Text = objSolicitud.AnoLiquidacion.ToString();
            }
            if (objSolicitud.IdFormaPago != 0)
            {
                objGeneral.TableName = "FormasPago";
                objGeneral.ColumnName = "FormaPago";
                objGeneral.Id = objSolicitud.IdFormaPago;
                objGeneral.GetGeneralTable();
                this.txtFormaPago.Text = objGeneral.Nombre;
            }

            objGeneral.TableName = "SolicitudEstados";
            objGeneral.ColumnName = "SolicitudEstado";
            objGeneral.Id = objSolicitud.IdSolicitudEstado;
            objGeneral.GetGeneralTable();
            this.txtEstadoSoli.Text = objGeneral.Nombre;

            ViewState["IdPresupuestoIndividuo"] = objSolicitud.IdPresupuestoIndividuo;
            ViewState["IdPresupuestoEmpresa"] = objSolicitud.IdPresupuestoEmpresa;
            this.tblDatosSolicitud.Style["display"] = "";

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

            //Cargar las anotaciones fijas			
            this.FillList(this.chkAnotaciones, "SolicitudAnotacionesFijas", p_idSolicitud);
            foreach (ListItem itemAnotacion in this.chkAnotaciones.Items)
            {
                itemAnotacion.Selected = true;
            }

            this.hplFormato.NavigateUrl = "javascript:openPopUp('AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&Resumen=1',900,700)";
            this.hplResumen.NavigateUrl = "javascript:openPopUp('../Formatos/FO_ResumenImpresionReembolso.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&Imprimir=1',829,900)";
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
        public void LoadLabelProveedores(Label p_lblProveedores, long p_idSolicitudTipoServicio)
        {
            SolicitudTipoServicioProveedores objProveedor = new SolicitudTipoServicioProveedores();
            objProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtProveedores = objProveedor.ConsultSolicitudTipoServicioProveedores().Tables[0];

            foreach (DataRow dr in dtProveedores.Rows)
            {
                p_lblProveedores.Text += "-" + dr["NombreProveedor"].ToString() + "-" + dr["Direcciones"].ToString() + " Tel " + dr["Telefonos"].ToString() + "-" + dr["Horario"].ToString() + "</br>";

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
            ////base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtlTipoServicio.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlTipoServicio_ItemDataBound);
            this.dtlFormatos.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtlFormatos_ItemCommand);
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Carga la grilla de productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");
                    Label lblDescuento = (Label)e.Item.FindControl("lblDescuento");
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                    Label lblUVR = (Label)e.Item.FindControl("lblUVR");

                    if (!Convert.IsDBNull(rowItem["Dosis"]) && rowItem["Dosis"].ToString() != string.Empty)
                    {
                        lblDosis.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["ValorAprobado"]))
                    {
                        lblValorAprobado.Visible = true;
                        lblUVR.Visible = true;
                    }
                    if (!Convert.IsDBNull(rowItem["Descuento"]))
                    {
                        lblDescuento.Visible = true;
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
                    Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");

                    //Cargar diagnosticos y prestadores
                    LoadLabelDiagnosticos(lblDiagnosticos, ((long)rowItem["IdSolicitudTipoServicio"]));
                    LoadLabelProveedores(lblProveedor, ((long)rowItem["IdSolicitudTipoServicio"]));

                    //Cargar los servicio
                    DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                    SolicitudServicio objSolicitudServicio = new SolicitudServicio();

                    TextBox txtConsecutivoNombre = (TextBox)e.Item.FindControl("txtConsecutivoNombre");
                    txtConsecutivoNombre.Attributes.Add("ReadOnly", "ReadOnly");

                    //Inicio PETF 14/01/10 ReadOnly
                    TextBox txtComentariosTipoServicio = (TextBox)e.Item.FindControl("txtComentariosTipoServicio");
                    txtComentariosTipoServicio.Attributes.Add("ReadOnly", "ReadOnly");

                    TextBox txtComentariosAnulacion = (TextBox)e.Item.FindControl("txtComentariosAnulacion");
                    txtComentariosAnulacion.Attributes.Add("ReadOnly", "ReadOnly");
                    //Fin PETF 14/01/10

                    objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                    objSolicitudServicio.IdSolicitudTipoServicio = ((long)rowItem["IdSolicitudTipoServicio"]);
                    dtgProductoServicio.DataSource = objSolicitudServicio.ConsultSolicitudServicio();
                    dtgProductoServicio.DataBind();

                    if (!Convert.IsDBNull(rowItem["ObservacionAnulacion"]) && rowItem["ObservacionAnulacion"].ToString() != string.Empty)
                    {
                        HtmlTableRow trComentariosAnulacion = (HtmlTableRow)e.Item.FindControl("trComentariosAnulacion");
                        trComentariosAnulacion.Style["display"] = "";
                        txtComentariosAnulacion.Text = rowItem["ObservacionAnulacion"].ToString();
                    }
                }
            }
        }

        protected void btnFinalizar_Click(object sender, System.EventArgs e)
        {
            if (Request.QueryString["popup"] == null || Request.QueryString["popup"] == string.Empty)
                Response.Redirect("LIS_solicitudgastos.aspx?employee_id=" + Request.QueryString["employee_id"]);
            else
            {
                if (Request.QueryString["reload"] != null && Request.QueryString["reload"] != string.Empty)
                    Response.Write("<script>alert('La solicitud fue modificada exitosamente'); window.opener.ActualizarConfirmadas(); top.close();</script>");
                else
                    Response.Write("<script>alert('La solicitud fue modificada exitosamente'); top.close();</script>");
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
                this.OpenWindow("../Formatos/FO_Reembolsos.aspx?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdProveedor=" + lblIdProveedor.Text, 829, 900);
            }
            if (e.CommandName == "Exportar")
            {
                Label lblUrlFormato = (Label)e.Item.FindControl("lblUrlFormato");
                Label lblIdProveedor = (Label)e.Item.FindControl("lblIdProveedor");
                this.OpenWindow("../Formatos/FO_Reembolsos.aspx?IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&IdSolicitudTipoServicio=" + e.CommandArgument.ToString() + "&IdProveedor=" + lblIdProveedor.Text + "exportar=S", 829, 900);
            }

            //Cargar de nuevo la grilla actualizando impresiones
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);

            DataTable dtTipoServicioFormatos = objTipoServicio.ConsultSolicitudTipoServicioFormatos().Tables[0];
            this.dtlFormatos.DataSource = dtTipoServicioFormatos;
            this.dtlFormatos.DataBind();
        }

        #endregion


    }
}
