namespace TPA.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Collections;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Control para adición de diagnósticos
    /// </summary>
    public partial class WC_AdicionarDiagnosticoConsulta : WC_Base
    {

        #region Atributes

        protected System.Web.UI.WebControls.Label lblTiempoEvolución1;
        protected System.Web.UI.WebControls.Label lblDiagnostico;
        protected System.Web.UI.WebControls.Label lblTiempoEvolución;
        protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico1;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico1;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion1;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion1;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico1;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico1;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico4;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico4;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion4;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion4;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico4;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico4;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico2;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico2;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion2;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion2;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico2;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico2;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico5;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico5;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion5;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion5;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico5;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico5;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico3;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico3;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion3;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion3;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico3;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico3;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico6;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico6;
        protected System.Web.UI.WebControls.TextBox txtTiempoEvolucion6;
        protected System.Web.UI.WebControls.DropDownList ddlTiempoEvolucion6;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico6;
        protected System.Web.UI.WebControls.DropDownList ddlTipoDiagnostico6;

        // Inicio - Emilio Bueno 13/12/2012
        #region Atributos de Propiedades
        /// <summary>Atributo, Verdadero si el diagnóstico debe ser Requerido al guardar automáticamente</summary>
        private bool _DiagnosticoEsRequerido = true;
        #endregion
        // Fin - Emilio Bueno 13/12/2012

        #endregion

        // Inicio - Emilio Bueno 13/12/2012
        #region Propiedades
        /// <summary>Propiedad, Verdadero si el diagnóstico debe ser Requerido al guardar automáticamente</summary>
        public bool DiagnosticoEsRequerido
        {
            get { return _DiagnosticoEsRequerido; }
            set { _DiagnosticoEsRequerido = value; }
        }
        #endregion
        // Fin - Emilio Bueno 13/12/2012

        #region Inicialización

        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                this.LoadControls();
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, Realiza la carga inicial de los controles
        /// </summary>
        public void LoadControls()
        {
            this.btnBuscarDiagnostico1.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico1.ClientID + "','" + this.txtDiagnostico1.ClientID + "','" + this.ddlClasificacion1.ClientID + "','" + this.txtEnabledddlClasificacion1.ClientID + "'); return false;");
            this.btnBuscarDiagnostico2.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico2.ClientID + "','" + this.txtDiagnostico2.ClientID + "','" + this.ddlClasificacion2.ClientID + "','" + this.txtEnabledddlClasificacion2.ClientID + "'); return false;");
            this.btnBuscarDiagnostico3.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico3.ClientID + "','" + this.txtDiagnostico3.ClientID + "','" + this.ddlClasificacion3.ClientID + "','" + this.txtEnabledddlClasificacion3.ClientID + "'); return false;");
            this.btnBuscarDiagnostico4.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico4.ClientID + "','" + this.txtDiagnostico4.ClientID + "','" + this.ddlClasificacion4.ClientID + "','" + this.txtEnabledddlClasificacion4.ClientID + "'); return false;");
            this.btnBuscarDiagnostico5.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico5.ClientID + "','" + this.txtDiagnostico5.ClientID + "','" + this.ddlClasificacion5.ClientID + "','" + this.txtEnabledddlClasificacion5.ClientID + "'); return false;");
            this.btnBuscarDiagnostico6.Attributes.Add("OnClick", "javascript:ShowDiagnosticoClasificacion(this,'" + this.txtIdDiagnostico6.ClientID + "','" + this.txtDiagnostico6.ClientID + "','" + this.ddlClasificacion6.ClientID + "','" + this.txtEnabledddlClasificacion6.ClientID + "'); return false;");

             //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtDiagnostico1.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico4.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico2.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico5.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico3.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico6.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin MAHG 12/01/10

            GeneralTable objGeneral = new GeneralTable();
            objGeneral.TableName = "TipoDiagnosticos";
            objGeneral.ColumnName = "TipoDiagnostico";
            DataTable dtTipoDiagnosticos = objGeneral.ConsultGeneralTable().Tables[0];

            this.ddlTipoDiagnostico1.DataTextField = "Nombre";
            this.ddlTipoDiagnostico1.DataValueField = "Id";
            this.ddlTipoDiagnostico2.DataTextField = "Nombre";
            this.ddlTipoDiagnostico2.DataValueField = "Id";
            this.ddlTipoDiagnostico3.DataTextField = "Nombre";
            this.ddlTipoDiagnostico3.DataValueField = "Id";
            this.ddlTipoDiagnostico4.DataTextField = "Nombre";
            this.ddlTipoDiagnostico4.DataValueField = "Id";
            this.ddlTipoDiagnostico5.DataTextField = "Nombre";
            this.ddlTipoDiagnostico5.DataValueField = "Id";
            this.ddlTipoDiagnostico6.DataTextField = "Nombre";
            this.ddlTipoDiagnostico6.DataValueField = "Id";
            this.ddlTipoDiagnostico1.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico1.DataBind();
            this.ddlTipoDiagnostico2.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico2.DataBind();
            this.ddlTipoDiagnostico3.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico3.DataBind();
            this.ddlTipoDiagnostico4.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico4.DataBind();
            this.ddlTipoDiagnostico5.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico5.DataBind();
            this.ddlTipoDiagnostico6.DataSource = dtTipoDiagnosticos;
            this.ddlTipoDiagnostico6.DataBind();

            objGeneral.TableName = "DiagnosticosClasificacion";
            objGeneral.ColumnName = "DiagnosticoClasificacion";
            DataTable dtClasificacionDiagnostico = objGeneral.ConsultGeneralTable().Tables[0];

            this.ddlClasificacion1.DataTextField = "Nombre";
            this.ddlClasificacion1.DataValueField = "Id";
            this.ddlClasificacion2.DataTextField = "Nombre";
            this.ddlClasificacion2.DataValueField = "Id";
            this.ddlClasificacion3.DataTextField = "Nombre";
            this.ddlClasificacion3.DataValueField = "Id";
            this.ddlClasificacion4.DataTextField = "Nombre";
            this.ddlClasificacion4.DataValueField = "Id";
            this.ddlClasificacion5.DataTextField = "Nombre";
            this.ddlClasificacion5.DataValueField = "Id";
            this.ddlClasificacion6.DataTextField = "Nombre";
            this.ddlClasificacion6.DataValueField = "Id";
            this.ddlClasificacion1.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion1.DataBind();
            this.ddlClasificacion2.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion2.DataBind();
            this.ddlClasificacion3.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion3.DataBind();
            this.ddlClasificacion4.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion4.DataBind();
            this.ddlClasificacion5.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion5.DataBind();
            this.ddlClasificacion6.DataSource = dtClasificacionDiagnostico;
            this.ddlClasificacion6.DataBind();

            this.ddlTipoDiagnostico1.Items.Insert(0, new ListItem("--", "0"));
            this.ddlTipoDiagnostico2.Items.Insert(0, new ListItem("--", "0"));
            this.ddlTipoDiagnostico3.Items.Insert(0, new ListItem("--", "0"));
            this.ddlTipoDiagnostico4.Items.Insert(0, new ListItem("--", "0"));
            this.ddlTipoDiagnostico5.Items.Insert(0, new ListItem("--", "0"));
            this.ddlTipoDiagnostico6.Items.Insert(0, new ListItem("--", "0"));

            this.ddlClasificacion1.Items.Insert(0, new ListItem("-Clasificación-", "0"));
            this.ddlClasificacion2.Items.Insert(0, new ListItem("-Clasificación-", "0"));
            this.ddlClasificacion3.Items.Insert(0, new ListItem("-Clasificación-", "0"));
            this.ddlClasificacion4.Items.Insert(0, new ListItem("-Clasificación-", "0"));
            this.ddlClasificacion5.Items.Insert(0, new ListItem("-Clasificación-", "0"));
            this.ddlClasificacion6.Items.Insert(0, new ListItem("-Clasificación-", "0"));

        }

        /// <summary>
        /// Método, Carga los objetos de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objConsulta"></param>
        public void LoadDiagnosticos(Consulta p_objConsulta)
        {

            if (!this.validarDiagnosticos())
                throw new Exception("Debe seleccionar diagnosticos diferentes");

            if (p_objConsulta.IdTipoConsulta == 15 || p_objConsulta.IdTipoConsulta == 16)
                this._DiagnosticoEsRequerido = false;
            //Inicio - Emilio Bueno 13/12/2012
            //if (p_objConsulta.IdTipoConsulta != 3 && this.txtDiagnostico1.Text == string.Empty && this.txtDiagnostico2.Text == string.Empty && this.txtDiagnostico3.Text == string.Empty && this.txtDiagnostico4.Text == string.Empty && this.txtDiagnostico5.Text == string.Empty && this.txtDiagnostico6.Text == string.Empty)
            if (this._DiagnosticoEsRequerido && p_objConsulta.IdTipoConsulta != 3 && this.txtDiagnostico1.Text == string.Empty && this.txtDiagnostico2.Text == string.Empty && this.txtDiagnostico3.Text == string.Empty && this.txtDiagnostico4.Text == string.Empty && this.txtDiagnostico5.Text == string.Empty && this.txtDiagnostico6.Text == string.Empty)
            //Fin - Emilio Bueno 13/12/2012
                throw new Exception("Debe adicionar al menos un diagnóstico");

            ArrayList arrDiagnosticos = new ArrayList();

            if (this.txtDiagnostico1.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos1 = new ConsultaDiagnosticos();
                objDiagnosticos1.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (this.txtTiempoEvolucion1.Text != string.Empty)
                    objDiagnosticos1.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion1.Text);
                objDiagnosticos1.PeriodoEvolucion = this.ddlTiempoEvolucion1.SelectedValue;
                if (this.ddlTipoDiagnostico1.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos1.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico1.SelectedValue);
                if (this.ddlClasificacion1.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if(this.ddlClasificacion1.SelectedValue != "0")
                    objDiagnosticos1.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion1.SelectedValue);
                objDiagnosticos1.OrdenDiagnosticos = 1;
                arrDiagnosticos.Add(objDiagnosticos1);

                
            }

            if (this.txtDiagnostico2.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos2 = new ConsultaDiagnosticos();
                objDiagnosticos2.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico2.Text);
                if (this.txtTiempoEvolucion2.Text != string.Empty)
                    objDiagnosticos2.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion2.Text);
                objDiagnosticos2.PeriodoEvolucion = this.ddlTiempoEvolucion2.SelectedValue;
                if (this.ddlTipoDiagnostico2.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos2.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico2.SelectedValue);
                if (this.ddlClasificacion2.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if (this.ddlClasificacion2.SelectedValue != "0")
                    objDiagnosticos2.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion2.SelectedValue);
                objDiagnosticos2.OrdenDiagnosticos = 2;
                arrDiagnosticos.Add(objDiagnosticos2);
            }

            if (this.txtDiagnostico3.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos3 = new ConsultaDiagnosticos();
                objDiagnosticos3.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico3.Text);
                if (this.txtTiempoEvolucion3.Text != string.Empty)
                    objDiagnosticos3.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion3.Text);
                objDiagnosticos3.PeriodoEvolucion = this.ddlTiempoEvolucion3.SelectedValue;
                if (this.ddlTipoDiagnostico3.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos3.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico3.SelectedValue);
                if (this.ddlClasificacion3.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if (this.ddlClasificacion3.SelectedValue != "0")
                    objDiagnosticos3.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion3.SelectedValue);
                objDiagnosticos3.OrdenDiagnosticos = 3;
                arrDiagnosticos.Add(objDiagnosticos3);
            }


            if (this.txtDiagnostico4.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos4 = new ConsultaDiagnosticos();
                objDiagnosticos4.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico4.Text);
                if (this.txtTiempoEvolucion4.Text != string.Empty)
                    objDiagnosticos4.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion4.Text);
                objDiagnosticos4.PeriodoEvolucion = this.ddlTiempoEvolucion4.SelectedValue;
                if (this.ddlTipoDiagnostico4.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos4.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico4.SelectedValue);
                if (this.ddlClasificacion4.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if (this.ddlClasificacion4.SelectedValue != "0")
                    objDiagnosticos4.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion4.SelectedValue);
                objDiagnosticos4.OrdenDiagnosticos = 4;
                arrDiagnosticos.Add(objDiagnosticos4);
            }


            if (this.txtDiagnostico5.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos5 = new ConsultaDiagnosticos();
                objDiagnosticos5.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico5.Text);
                if (this.txtTiempoEvolucion5.Text != string.Empty)
                    objDiagnosticos5.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion5.Text);
                objDiagnosticos5.PeriodoEvolucion = this.ddlTiempoEvolucion5.SelectedValue;
                if (this.ddlTipoDiagnostico5.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos5.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico5.SelectedValue);
                if (this.ddlClasificacion5.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if (this.ddlClasificacion5.SelectedValue != "0")
                    objDiagnosticos5.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion5.SelectedValue);
                objDiagnosticos5.OrdenDiagnosticos = 5;
                arrDiagnosticos.Add(objDiagnosticos5);
            }


            if (this.txtDiagnostico6.Text.Trim() != string.Empty)
            {
                ConsultaDiagnosticos objDiagnosticos6 = new ConsultaDiagnosticos();
                objDiagnosticos6.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico6.Text);
                if (this.txtTiempoEvolucion6.Text != string.Empty)
                    objDiagnosticos6.TiempoEvolucion = Convert.ToDecimal(this.txtTiempoEvolucion6.Text);
                objDiagnosticos6.PeriodoEvolucion = this.ddlTiempoEvolucion6.SelectedValue;
                if (this.ddlTipoDiagnostico6.SelectedValue == "0")
                    throw new Exception("Debe seleccionar el tipo de diagnóstico");
                objDiagnosticos6.IdTipoDiagnostico = Convert.ToInt16(this.ddlTipoDiagnostico6.SelectedValue);
                if (this.ddlClasificacion6.SelectedValue == "0")
                    throw new Exception("Debe seleccionar la clasificación de cada diagnóstico");
                if (this.ddlClasificacion6.SelectedValue != "0")
                    objDiagnosticos6.IdDiagnosticoClasificacion = Convert.ToInt32(this.ddlClasificacion6.SelectedValue);
                objDiagnosticos6.OrdenDiagnosticos = 6;
                arrDiagnosticos.Add(objDiagnosticos6);
            }

            p_objConsulta.ConsultaDiagnosticos = arrDiagnosticos;
        }

        /// <summary>
        /// Método, Carga los controles con los diagnósticos del tipo de servicio
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void LoadControlDiagnosticos(long p_idConsulta)
        {
            DataRow dr;
            ConsultaDiagnosticos objDiagnostico = new ConsultaDiagnosticos();
            objDiagnostico.IdConsulta = p_idConsulta;

            if (Request.QueryString["IdConsulta"] != null)
            {
                objDiagnostico.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
                this.imgLimpiar1.Style["display"] = "none";
                this.imgLimpiar2.Style["display"] = "none";
                this.imgLimpiar3.Style["display"] = "none";
                this.imgLimpiar4.Style["display"] = "none";
                this.imgLimpiar5.Style["display"] = "none";
                this.imgLimpiar6.Style["display"] = "none";
            }
            if (Request.QueryString["IdConsultaCopia"] != null)
                objDiagnostico.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsultaCopia"]);

            DataTable dtDiagnosticos = objDiagnostico.ConsultConsultaDiagnosticos().Tables[0];

            if (dtDiagnosticos.Rows.Count > 0)
            {
                dr = dtDiagnosticos.Rows[0];
                this.txtDiagnostico1.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico1.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion1.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion1.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico1.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion1.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }

            if (dtDiagnosticos.Rows.Count > 1)
            {
                dr = dtDiagnosticos.Rows[1];
                this.txtDiagnostico2.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico2.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion2.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion2.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico2.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion2.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }

            if (dtDiagnosticos.Rows.Count > 2)
            {
                dr = dtDiagnosticos.Rows[2];
                this.txtDiagnostico3.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico3.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion3.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion3.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico3.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion3.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }
            if (dtDiagnosticos.Rows.Count > 3)
            {
                dr = dtDiagnosticos.Rows[3];
                this.txtDiagnostico4.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico4.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion4.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion4.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico4.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion4.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }
            if (dtDiagnosticos.Rows.Count > 4)
            {
                dr = dtDiagnosticos.Rows[4];
                this.txtDiagnostico5.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico5.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion5.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion5.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico5.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion5.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }
            if (dtDiagnosticos.Rows.Count > 5)
            {
                dr = dtDiagnosticos.Rows[5];
                this.txtDiagnostico6.Text = dr["NombreDiagnostico"].ToString();
                this.txtIdDiagnostico6.Text = dr["IdDiagnostico"].ToString();
                this.txtTiempoEvolucion6.Text = dr["TiempoEvolucion"].ToString();
                if (dr["PeriodoEvolucion"].ToString() != string.Empty)
                    this.ddlTiempoEvolucion6.SelectedValue = dr["PeriodoEvolucion"].ToString();
                if (!Convert.IsDBNull(dr["IdTipoDiagnostico"]))
                    this.ddlTipoDiagnostico6.SelectedValue = dr["IdTipoDiagnostico"].ToString();
                if (!Convert.IsDBNull(dr["IdDiagnosticoClasificacion"]))
                    this.ddlClasificacion6.SelectedValue = dr["IdDiagnosticoClasificacion"].ToString();
            }

            

        }

        /// <summary>
        /// Método, valida que no haya diagnosticos iguales seleccionados
        /// </summary>
        /// <returns></returns>
        public bool validarDiagnosticos()
        {
            if (this.txtDiagnostico1.Text != string.Empty && (this.txtDiagnostico1.Text == this.txtDiagnostico2.Text || this.txtDiagnostico1.Text == this.txtDiagnostico3.Text || this.txtDiagnostico1.Text == this.txtDiagnostico4.Text || this.txtDiagnostico1.Text == this.txtDiagnostico5.Text || this.txtDiagnostico1.Text == this.txtDiagnostico6.Text))
                return false;
            if (this.txtDiagnostico2.Text != string.Empty && (this.txtDiagnostico2.Text == this.txtDiagnostico1.Text || this.txtDiagnostico2.Text == this.txtDiagnostico3.Text || this.txtDiagnostico2.Text == this.txtDiagnostico4.Text || this.txtDiagnostico2.Text == this.txtDiagnostico5.Text || this.txtDiagnostico2.Text == this.txtDiagnostico6.Text))
                return false;
            if (this.txtDiagnostico3.Text != string.Empty && (this.txtDiagnostico3.Text == this.txtDiagnostico2.Text || this.txtDiagnostico3.Text == this.txtDiagnostico1.Text || this.txtDiagnostico3.Text == this.txtDiagnostico4.Text || this.txtDiagnostico3.Text == this.txtDiagnostico5.Text || this.txtDiagnostico3.Text == this.txtDiagnostico6.Text))
                return false;
            if (this.txtDiagnostico4.Text != string.Empty && (this.txtDiagnostico4.Text == this.txtDiagnostico2.Text || this.txtDiagnostico4.Text == this.txtDiagnostico3.Text || this.txtDiagnostico4.Text == this.txtDiagnostico1.Text || this.txtDiagnostico4.Text == this.txtDiagnostico5.Text || this.txtDiagnostico4.Text == this.txtDiagnostico6.Text))
                return false;
            if (this.txtDiagnostico5.Text != string.Empty && (this.txtDiagnostico5.Text == this.txtDiagnostico2.Text || this.txtDiagnostico5.Text == this.txtDiagnostico3.Text || this.txtDiagnostico5.Text == this.txtDiagnostico4.Text || this.txtDiagnostico5.Text == this.txtDiagnostico1.Text || this.txtDiagnostico5.Text == this.txtDiagnostico6.Text))
                return false;
            if (this.txtDiagnostico6.Text != string.Empty && (this.txtDiagnostico6.Text == this.txtDiagnostico2.Text || this.txtDiagnostico6.Text == this.txtDiagnostico3.Text || this.txtDiagnostico6.Text == this.txtDiagnostico4.Text || this.txtDiagnostico6.Text == this.txtDiagnostico5.Text || this.txtDiagnostico6.Text == this.txtDiagnostico1.Text))
                return false;
            return true;

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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);


        }
        #endregion

        #endregion
    }
}
