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

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza el ingreso o actualización de usuarios
    /// </summary>
    /// <remarks>Autor: Ricardo Silva</remarks>
    /// <remarks>Fecha de creación: 16 de Octubre de 2011</remarks>
    public partial class AE_empresa : PB_PaginaBase
    {
        #region Atributtes



        #endregion

        #region Initializing

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
                    this.LoadControls();
                }               
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
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

        private bool Validaciones()
        {
            if (txtNombreEmpresa.Text == "")
            {
                DisplayMessage("Por favor introduce el nombre de la empresa");
                return false;
            }

            if (ddlTipoCliente.SelectedItem.Value == "-1")
            {
                DisplayMessage("Por favor selecciona el tipo de cliente");
                return false;
            }

            if (txtDiasPassword.Text == "")
            {
                DisplayMessage("Por favor selecciona los dias en que caducara el password");
                return false;
            }

            if (txtIntentosPassword.Text == "")
            {
                DisplayMessage("Por favor selecciona el numero de intentos en los que se bloqueará el password");
                return false;
            }


            return true;

        }


        private void UpdateEmpresa(int idEmpresa)
        {
            #region LoadEmpresa

            SIC_EMPRESA objEmpresa = new SIC_EMPRESA();       

            objEmpresa.Empresa_id = idEmpresa;
            objEmpresa.Tipo_cliente = int.Parse(ddlTipoCliente.SelectedItem.Value.ToString());
            objEmpresa.Nombre = txtNombreEmpresa.Text;
            objEmpresa.AbreviacionEmpresa = txtEmpresaAbreviacion.Text;
            objEmpresa.Direccion = txtDireccion.Text;
            objEmpresa.Telefono = txtTelefono.Text;
            objEmpresa.Correo = txtCorreo.Text;
            objEmpresa.Fax = txtFax.Text;
            objEmpresa.Contacto = txtContacto.Text;
            objEmpresa.DiasCaducaPassword = Convert.ToInt32(txtDiasPassword.Text);
            objEmpresa.IntentosBloqueaPassword = Convert.ToInt32(txtIntentosPassword.Text);

            ArrayList arrParentescos = this.loadObjectsParentescos();
            ArrayList arrTipoServicios = this.loadObjectsTipoServicios();

            //formatos

            string Texto;

            //GAMM
            //Texto = this.fckEditorDetalle.Value.Replace("\n", "");
            Texto = this.fckEditorDetalle.Value;

            //I. GAMM 20200706 Valida texto contra BlackList
            bool existeBlackList = false;


            string coneccion = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString.ToString();

            existeBlackList = AuxiliarSeguridad.ValidarTexto(Texto, coneccion, "EXECUTE spRecuperaBlackList");

            if (existeBlackList)
            {
                Response.Redirect("~/ErrorPage.aspx");
            }
            //F. GAMM 20200706 Valida texto contra BlackList
            

            Texto = Texto.Replace("\r", "");
            Texto = Texto.Replace("  ", "");
            Texto = Texto.Replace("<p>", "</br>");
            Texto = Texto.Replace("</p>", "");
            Texto = Texto + "</br>";
                      

            //GAMM codificamos
            //Texto = Server.HtmlEncode(Texto);

            string titulo;

            titulo = txtTituloFormato.Text;

            #region Permisos


            if (chlPermisos.Items[0].Selected)
            {
                objEmpresa.RealizaConsultas = 1;
            }
            else
            {
                objEmpresa.RealizaConsultas = 0;
            }

            if (chlPermisos.Items[1].Selected)
            {
                objEmpresa.RealizaCitasAgenda = 1;
            }
            else
            {
                objEmpresa.RealizaCitasAgenda = 0;
            }

            #endregion

            #region Divisiones
            ArrayList lstSecciones = new ArrayList();

            foreach (ListItem li in chlSecciones.Items)
            {

                if (li.Selected)
                {
                    lstSecciones.Add(1);
                }
                else
                {
                    lstSecciones.Add(0);
                }

            }

            objEmpresa.DivColesterolGlicemia = Convert.ToInt32(lstSecciones[0]);
            objEmpresa.DivExamenesLaboratorio = Convert.ToInt32(lstSecciones[1]);
            objEmpresa.DivMujer = Convert.ToInt32(lstSecciones[2]);
            objEmpresa.DivAudiometria = Convert.ToInt32(lstSecciones[3]);
            objEmpresa.DivWellness = Convert.ToInt32(lstSecciones[4]);
            objEmpresa.DivHabitoFumar = Convert.ToInt32(lstSecciones[5]);
            objEmpresa.DivConsumoAlcohol = Convert.ToInt32(lstSecciones[6]);
            objEmpresa.DivVacunacion = Convert.ToInt32(lstSecciones[7]);
            objEmpresa.DivSedentarismo = Convert.ToInt32(lstSecciones[8]);
            objEmpresa.DivSaludOral = Convert.ToInt32(lstSecciones[9]);
            objEmpresa.DivEstres = Convert.ToInt32(lstSecciones[10]);
            objEmpresa.DivEmocional = Convert.ToInt32(lstSecciones[11]);
            objEmpresa.DivAccidentalidad = Convert.ToInt32(lstSecciones[12]);
            objEmpresa.DivEstadoSalud = Convert.ToInt32(lstSecciones[13]);
            objEmpresa.DivNutricion = Convert.ToInt32(lstSecciones[14]);
            objEmpresa.DivAntecedentesAusentismo = Convert.ToInt32(lstSecciones[15]);
            objEmpresa.DivRecomendaciones = Convert.ToInt32(lstSecciones[16]);
            objEmpresa.DivDiastolicaSisTolica = Convert.ToInt32(lstSecciones[17]);
            objEmpresa.DivPerimetroAbdominal = Convert.ToInt32(lstSecciones[18]);


            #endregion

            int idUser;
            idUser = Convert.ToInt32(Session["IdUser"]);

            #endregion


            objEmpresa.UpdateEmpresa(arrParentescos, arrTipoServicios, titulo, Texto);

        }

        private bool InsertEmpresa()
        {
            #region LoadEmpresa

            string strIdEmpresa = null;
            strIdEmpresa = Request.QueryString["IDEmpresa"];
            SIC_EMPRESA objEmpresa = new SIC_EMPRESA();

            if (strIdEmpresa != null)
            {
                objEmpresa.Empresa_id = int.Parse(strIdEmpresa);
            }

            objEmpresa.Tipo_cliente = int.Parse(ddlTipoCliente.SelectedItem.Value.ToString());
            objEmpresa.Nombre = txtNombreEmpresa.Text;
            objEmpresa.AbreviacionEmpresa = txtEmpresaAbreviacion.Text;
            objEmpresa.Direccion = txtDireccion.Text;
            objEmpresa.Telefono = txtTelefono.Text;
            objEmpresa.Correo = txtCorreo.Text;
            objEmpresa.Fax = txtFax.Text;
            objEmpresa.Contacto = txtContacto.Text;
            objEmpresa.DiasCaducaPassword = Convert.ToInt32(txtDiasPassword.Text);
            objEmpresa.IntentosBloqueaPassword = Convert.ToInt32(txtIntentosPassword.Text);

            ArrayList arrParentescos = this.loadObjectsParentescos();
            ArrayList arrTipoServicios = this.loadObjectsTipoServicios();

            //formatos

            string Texto;

            //GAMM Actulizamos control
            Texto = this.fckEditorDetalle.Value.Replace("\n", "");

            Texto = Texto.Replace("\r", "");
            Texto = Texto.Replace("  ", "");
            Texto = Texto.Replace("<p>", "</br>");
            Texto = Texto.Replace("</p>", "");
            Texto = Texto + "</br>";

            //I. GAMM 20200706 Valida texto contra BlackList
            bool existeBlackList = false;

            string coneccion = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString.ToString();

            existeBlackList = AuxiliarSeguridad.ValidarTexto(Texto, coneccion, "EXECUTE spRecuperaBlackList");

            if (existeBlackList)
            {
                Response.Redirect("~/ErrorPage.aspx");
            }
            //F. GAMM 20200706 Valida texto contra BlackList

            //GAMM Codificamos
            //Texto = Server.HtmlEncode(Texto);

            string titulo;

            titulo = txtTituloFormato.Text;

            #region Permisos


            if (chlPermisos.Items[0].Selected)
            {
                objEmpresa.RealizaConsultas = 1;
            }
            else
            {
                objEmpresa.RealizaConsultas = 0;
            }

            if (chlPermisos.Items[1].Selected)
            {
                objEmpresa.RealizaCitasAgenda = 1;
            }
            else
            {
                objEmpresa.RealizaCitasAgenda = 0;
            }





            #endregion

            #region Divisiones
            ArrayList lstSecciones = new ArrayList();

            foreach (ListItem li in chlSecciones.Items)
            {

                if (li.Selected)
                {
                    lstSecciones.Add(1);
                }
                else
                {
                    lstSecciones.Add(0);
                }

            }

            objEmpresa.DivColesterolGlicemia = Convert.ToInt32(lstSecciones[0]);
            objEmpresa.DivExamenesLaboratorio = Convert.ToInt32(lstSecciones[1]);
            objEmpresa.DivMujer = Convert.ToInt32(lstSecciones[2]);
            objEmpresa.DivAudiometria = Convert.ToInt32(lstSecciones[3]);
            objEmpresa.DivWellness = Convert.ToInt32(lstSecciones[4]);
            objEmpresa.DivHabitoFumar = Convert.ToInt32(lstSecciones[5]);
            objEmpresa.DivConsumoAlcohol = Convert.ToInt32(lstSecciones[6]);
            objEmpresa.DivVacunacion = Convert.ToInt32(lstSecciones[7]);
            objEmpresa.DivSedentarismo = Convert.ToInt32(lstSecciones[8]);
            objEmpresa.DivSaludOral = Convert.ToInt32(lstSecciones[9]);
            objEmpresa.DivEstres = Convert.ToInt32(lstSecciones[10]);
            objEmpresa.DivEmocional = Convert.ToInt32(lstSecciones[11]);
            objEmpresa.DivAccidentalidad = Convert.ToInt32(lstSecciones[12]);
            objEmpresa.DivEstadoSalud = Convert.ToInt32(lstSecciones[13]);
            objEmpresa.DivNutricion = Convert.ToInt32(lstSecciones[14]);
            objEmpresa.DivAntecedentesAusentismo = Convert.ToInt32(lstSecciones[15]);
            objEmpresa.DivRecomendaciones = Convert.ToInt32(lstSecciones[16]);
            objEmpresa.DivDiastolicaSisTolica = Convert.ToInt32(lstSecciones[17]);
            objEmpresa.DivPerimetroAbdominal = Convert.ToInt32(lstSecciones[18]);


            #endregion

            int idUser;
            idUser = Convert.ToInt32(Session["IdUser"]);

            #endregion

            //Inserta la empresa
            return objEmpresa.InsertSIC_EMPRESA(arrParentescos, arrTipoServicios, titulo, Texto, idUser);
        }

        private void LoadControls()
        {
            string strEmpresa = Request.QueryString["IDEmpresa"];
            loadParentescos(0);
            loadTipoServicios(0);
            loadEmpresas();

            if (ViewState["editar"] != null && Convert.ToInt16(ViewState["editar"]) == 1)
            {
                lblTitulo.Text = "ACTUALIZAR EMPRESA";
                this.CargarDatosEmpresa(int.Parse(strEmpresa));
            }
            else
            {
                lblTitulo.Text = "AGREGAR EMPRESA";
            }

        }

        /// <summary>
        /// Método, carga el listado de los parentescos
        /// </summary>
        public void loadParentescos(int idEmpresa)
        {
            DataTable dtParentescos;
            Parentescos objParentescos = new Parentescos();

            dtParentescos = dtParentescos = objParentescos.ConsultParentescos().Tables[0];

            this.chlParentescos.DataSource = dtParentescos;
            this.chlParentescos.DataTextField = "NombreParentesco";
            this.chlParentescos.DataValueField = "IdParentesco";
            this.chlParentescos.DataBind();

            if (idEmpresa != 0)
            {
                DataTable dtParentescosEmpresaAsignados;
                dtParentescosEmpresaAsignados = objParentescos.ConsultEmpresaParentescosAsignados(idEmpresa).Tables[0];

                for (int i = 0; i < dtParentescosEmpresaAsignados.Rows.Count; i++)
                {
                    this.chlParentescos.Items.FindByValue(Convert.ToString(dtParentescosEmpresaAsignados.Rows[i]["Idparentesco"])).Selected = true;
                }
            }

        }

        /// <summary>
        /// Método, carga una arreglo con los ids de los tipos de servicios selecionados
        /// </summary>
        /// <returns></returns>
        public ArrayList loadObjectsTipoServicios()
        {
            ArrayList lstTipoServicios = new ArrayList();

            foreach (ListItem item in this.ChlTipoServicios.Items)
            {
                lstTipoServicios.Add(Convert.ToInt32(item.Value));
            }

            return lstTipoServicios;

        }

        /// <summary>
        /// Método, carga una arreglo con los ids de los parentescos seleccionados
        /// </summary>
        /// <returns></returns>
        public ArrayList loadObjectsParentescos()
        {
            ArrayList lstParentescos = new ArrayList();

            foreach (ListItem item in this.chlParentescos.Items)
            {
                if (item.Selected)
                {
                    lstParentescos.Add(Convert.ToInt32(item.Value));
                }
            }

            return lstParentescos;
        }

        /// <summary>
        /// Método, carga el listado de los tipos de servicio
        /// </summary>
        public void loadTipoServicios(int idEmpresa)
        {
            DataTable dtTipoServicios;
            TipoServicios objTipoServicios = new TipoServicios();

            dtTipoServicios = objTipoServicios.ConsultTipoServicioEmpresa(0).Tables[0];

            this.ChlTipoServicios.DataSource = dtTipoServicios;
            this.ChlTipoServicios.DataTextField = "NombreTipoServicio";
            this.ChlTipoServicios.DataValueField = "IdTipoServicio";
            this.ChlTipoServicios.DataBind();

            if (idEmpresa != 0)
            {
                DataTable dtTipoServiciosEmpresaAsignados;
                dtTipoServiciosEmpresaAsignados = objTipoServicios.ConsultEmpresaTipoServiciosAsignados(idEmpresa).Tables[0];

                for (int i = 0; i < dtTipoServiciosEmpresaAsignados.Rows.Count; i++)
                {
                    this.ChlTipoServicios.Items.FindByValue(Convert.ToString(dtTipoServiciosEmpresaAsignados.Rows[i]["IdTipoServicio"])).Selected = true;
                }
            }
        }

        /// <summary>
        /// Método, carga el listado de las empresas para el ddl que selecciona la empresa a modificar
        /// </summary>
        public void loadEmpresas()
        {
            DataTable dtEmpresas;
            SIC_EMPRESA objEmpresa = new SIC_EMPRESA();

            dtEmpresas = objEmpresa.ConsultSIC_EMPRESA().Tables[0];

            this.ddlEmpresas.DataSource = dtEmpresas;
            this.ddlEmpresas.DataTextField = "nombre";
            this.ddlEmpresas.DataValueField = "empresa_id";
            this.ddlEmpresas.DataBind();

            this.ddlEmpresas.Items.Insert(0, new ListItem("Selecione la empresa", "0"));
            this.ddlEmpresas.SelectedValue = "0";
        }

        //Carga los datos de la empresa a modificar

        private void CargarDatosEmpresa(int intIdEmpresa)
        {
            SIC_EMPRESA objEmpresa = new SIC_EMPRESA();
            objEmpresa.Empresa_id = intIdEmpresa;
            objEmpresa.GetSicEmpresa();

            //Datos generales
            txtNombreEmpresa.Text = objEmpresa.Nombre;
            txtEmpresaAbreviacion.Text = objEmpresa.AbreviacionEmpresa;
            ddlTipoCliente.SelectedValue = objEmpresa.Tipo_cliente.ToString();

            //Datos Empresa
            txtDireccion.Text = objEmpresa.Direccion;
            txtTelefono.Text = objEmpresa.Telefono;
            txtCorreo.Text = objEmpresa.Correo;
            txtFax.Text = objEmpresa.Fax;
            txtContacto.Text = objEmpresa.Contacto;

            //Seguridad
            txtDiasPassword.Text = Convert.ToString(objEmpresa.DiasCaducaPassword);
            txtIntentosPassword.Text = Convert.ToString(objEmpresa.IntentosBloqueaPassword);

            //Permisos


            if (objEmpresa.RealizaConsultas == 1)
            {
                chlPermisos.Items[0].Selected = true;
            }

            if (objEmpresa.RealizaCitasAgenda == 1)
            {
                chlPermisos.Items[1].Selected = true;
            }

            //Divisiones

            SIC_EMPRESA objEmpresaDivisiones = new SIC_EMPRESA();
            objEmpresaDivisiones.Empresa_id = intIdEmpresa;
            objEmpresaDivisiones.GetSicEmpresaDivisiones();

            ArrayList lstSecciones = new ArrayList();

            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivColesterolGlicemia));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivExamenesLaboratorio));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivMujer));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivAudiometria));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivWellness));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivHabitoFumar));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivConsumoAlcohol));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivVacunacion));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivSedentarismo));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivSaludOral));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivEstres));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivEmocional));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivAccidentalidad));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivEstadoSalud));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivNutricion));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivAntecedentesAusentismo));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivRecomendaciones));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivDiastolicaSisTolica));
            lstSecciones.Add(Convert.ToInt32(objEmpresaDivisiones.DivPerimetroAbdominal));

            for (int i = 0; i < lstSecciones.Count; i++)
            {
                if (Convert.ToInt32(lstSecciones[i]) == 1)
                {
                    chlSecciones.Items[i].Selected = true;
                }

            }

            //Formatos
            TipoServicios objTipoServicio = new TipoServicios();
            objTipoServicio.GetFormatosTipoServicios(intIdEmpresa);
            txtTituloFormato.Text = Convert.ToString(objTipoServicio.TituloFormato);

            //GAMM Codificamos
            //string decVal = string.Empty;
            //decVal = Server.HtmlDecode(objTipoServicio.TextoFormato);
            //fckEditorDetalle.Value = decVal;

            //GAMM
            fckEditorDetalle.Value = objTipoServicio.TextoFormato;
            

        }

        #region eventos

        //Selecciona la empresa a modificar/actualizar
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblTitulo.Text = "ACTUALIZAR EMPRESA";
            ViewState["editar"] = 1;
            int empresa_id;
            empresa_id = Convert.ToInt32(ddlEmpresas.SelectedItem.Value);
            loadParentescos(empresa_id);
            loadTipoServicios(empresa_id);
            CargarDatosEmpresa(empresa_id);
            trWizzard.Style["display"] = "";
            trNotaSecciones.Style["display"] = "";
            ddlEmpresas.Enabled = false;
        }

        //Selecciona la accion a realizar sobre la empresa
        protected void ddlAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccion.SelectedIndex == 1)
            {
                trSeleccion.Style["display"] = "none";
                trDdlSeleccion.Style["display"] = "none";
                trWizzard.Style["display"] = "";
            }

            if (ddlAccion.SelectedIndex == 2)
            {
                lblTitulo.Text = "ACTUALIZAR EMPRESA";
                trSeleccion.Style["display"] = "none";
                trDdlSeleccion.Style["display"] = "none";
                trLblActualizar.Style["display"] = "";
                trActualizar.Style["display"] = "";

            }

        }

        //Accion del boton finalizar del Wizard

        protected void WizardEmpresa_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Validaciones())
            {
                if (ViewState["editar"] != null && Convert.ToInt16(ViewState["editar"]) == 1)
                {
                    int empresa_id = Convert.ToInt32(ddlEmpresas.SelectedItem.Value);
                    UpdateEmpresa(empresa_id);
                    this.RegisterLog(Log.EnumActionsLog.ModificarEmpresa, empresa_id, "Id Usuario:" + Convert.ToInt32(Session["IdUser"]) + " Empresa:" + empresa_id);
                    DisplayMessageExito("La empresa ha sido actualizada con éxito");
                    Response.Redirect("AE_asociarempresas.aspx");
                }
                else
                {
                    if (!this.InsertEmpresa())
                    {
                        DisplayMessage("La empresa no pudo insertarse, por favor avise al administrador");
                    }
                    else
                    {
                        string strEmpresa = Request.QueryString["IDEmpresa"];

                        if (strEmpresa != null)
                        {
                            DisplayMessageExito("La empresa ha sido actualizada con éxito");
                        }
                        else
                        {
                            DisplayMessageExito("La empresa ha sido ingresado con éxito");
                            this.RegisterLog(Log.EnumActionsLog.AgregarEmpresa, Convert.ToInt32(Session["IdUser"]), "Id Usuario:" + Convert.ToInt32(Session["IdUser"]) + "Nombre empresa:" + txtNombreEmpresa.Text);
                            Response.Redirect("AE_asociarempresas.aspx");
                        }
                    }
                }
            }
        }

        #endregion

        protected void WizardEmpresa_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

            //GAMM
            string Texto = "";
            Texto = this.fckEditorDetalle.Value;


            //I. GAMM 20200706 Valida texto contra BlackList
            bool existeBlackList = false;

            
            string coneccion = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString.ToString();

            existeBlackList = AuxiliarSeguridad.ValidarTexto(Texto, coneccion, "EXECUTE spRecuperaBlackList");

            if (existeBlackList)
            {
                Response.Redirect("~/ErrorPage.aspx");
            }
            //F. GAMM 20200706 Valida texto contra BlackList


            //GAMM codificamos
           //Texto = Server.HtmlEncode(Texto);

            this.fckEditorDetalle.Value = Texto;

        }
    }

}

