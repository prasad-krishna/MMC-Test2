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
    ///	Control para adición de prestadores
    /// </summary>
    public partial class WC_AdicionarPrestador : WC_Base
    {

        #region Atributes

        
        #endregion

        #region Propiedades

        /// <summary>Propiedad, Indica si alguna solicitud tiene UVR seleccionado </summary>
        public bool tieneUVR
        {
            get
            {
                if (this.txtUVR.Text == "1")
                    return true;
                else
                    return false;
            }
            set { this.txtUVR.Text = value.ToString(); }
        }

        #endregion

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

            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtProveedor1.Attributes.Add("ReadOnly", "ReadOnly");
            txtProveedor2.Attributes.Add("ReadOnly", "ReadOnly");
            txtProveedor3.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin MAHG 12/01/10
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la carga inicial de los controles
        /// </summary>
        /// <param name="p_idTipoServicio"></param>
        public void LoadControls(string p_idTipoServicio)
        {
            Proveedores objProveedor = new Proveedores();
            ddlProveedor1.Items.Clear();

            objProveedor.IdTipoServicio = Convert.ToInt16(p_idTipoServicio);
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);
            DataTable dtProveedores = objProveedor.ConsultEmpresaProveedoresTipoServicio().Tables[0];

            if (dtProveedores.Rows.Count == 1)
            {
                this.ddlProveedor1.DataTextField = "nombre";
                this.ddlProveedor1.DataValueField = "id";
                this.ddlProveedor1.DataSource = dtProveedores;
                this.ddlProveedor1.DataBind();
                this.ddlProveedor1.SelectedIndex = 0;
                this.ddlProveedor1.Visible = true;
                this.chkDesplegarUVR1.Visible = true;

            }
            else
            {
                if (dtProveedores.Rows.Count > 40)
                {
                    this.txtProveedor1.Visible = true;
                    this.txtProveedor2.Visible = true;
                    this.txtProveedor3.Visible = true;
                    this.btnBuscarProveedor1.Visible = true;
                    this.btnBuscarProveedor2.Visible = true;
                    this.btnBuscarProveedor3.Visible = true;
                    this.chkDesplegarUVR1.Visible = true;
                    this.chkDesplegarUVR2.Visible = true;
                    this.chkDesplegarUVR3.Visible = true;
                    this.btnBuscarProveedor1.Attributes.Add("OnClick", "javascript:ShowProveedor(this,'" + this.txtIdProveedor1.ClientID + "','" + p_idTipoServicio + "','" + this.txtProveedor1.ClientID + "');");
                    this.btnBuscarProveedor2.Attributes.Add("OnClick", "javascript:ShowProveedor(this,'" + this.txtIdProveedor2.ClientID + "','" + p_idTipoServicio + "','" + this.txtProveedor2.ClientID + "');");
                    this.btnBuscarProveedor3.Attributes.Add("OnClick", "javascript:ShowProveedor(this,'" + this.txtIdProveedor3.ClientID + "','" + p_idTipoServicio + "','" + this.txtProveedor3.ClientID + "');");

                }
                else
                {
                    this.chkDesplegarUVR1.Visible = true;
                    this.chkDesplegarUVR2.Visible = true;
                    this.chkDesplegarUVR3.Visible = true;
                    this.ddlProveedor1.Visible = true;
                    this.ddlProveedor2.Visible = true;
                    this.ddlProveedor3.Visible = true;

                    this.ddlProveedor1.DataTextField = "nombre";
                    this.ddlProveedor1.DataValueField = "id";
                    this.ddlProveedor1.DataSource = dtProveedores;
                    this.ddlProveedor1.DataBind();
                    this.ddlProveedor1.Items.Insert(0, new ListItem("--Prestador--", "0"));

                    this.ddlProveedor2.DataTextField = "nombre";
                    this.ddlProveedor2.DataValueField = "id";
                    this.ddlProveedor2.DataSource = dtProveedores;
                    this.ddlProveedor2.DataBind();
                    this.ddlProveedor2.Items.Insert(0, new ListItem("--Prestador--", "0"));

                    this.ddlProveedor3.DataTextField = "nombre";
                    this.ddlProveedor3.DataValueField = "id";
                    this.ddlProveedor3.DataSource = dtProveedores;
                    this.ddlProveedor3.DataBind();
                    this.ddlProveedor3.Items.Insert(0, new ListItem("--Prestador--", "0"));

                }
            }
        }


        /// <summary>
        /// Método, realiza la carga inicial de los controles para ordenes
        /// </summary>
        /// <param name="p_idTipoServicio"></param>
        public void LoadControlsOrden(string p_idTipoServicio)
        {
            Proveedores objProveedor = new Proveedores();
            ddlProveedor1.Items.Clear();

            objProveedor.IdTipoServicio = Convert.ToInt16(p_idTipoServicio);
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);
            DataTable dtProveedores = objProveedor.ConsultEmpresaUsuario(Convert.ToInt32(Session["IdUser"])).Tables[0];
            this.trProveedor2.Style["display"] = "none";
            this.trProveedor3.Style["display"] = "none";

            if (dtProveedores.Rows.Count == 1)
            {
                this.ddlProveedor1.DataTextField = "nombre";
                this.ddlProveedor1.DataValueField = "id";
                this.ddlProveedor1.DataSource = dtProveedores;
                this.ddlProveedor1.DataBind();
                this.ddlProveedor1.SelectedIndex = 0;
                this.ddlProveedor1.Visible = true;


            }
            else
            {
                if (dtProveedores.Rows.Count > 40)
                {
                    this.txtProveedor1.Visible = true;
                    this.btnBuscarProveedor1.Visible = true;
                    this.btnBuscarProveedor1.Attributes.Add("OnClick", "javascript:ShowProveedorUser(this,'" + this.txtIdProveedor1.ClientID + "','" + p_idTipoServicio + "','" + this.txtProveedor1.ClientID + "');");

                }
                else
                {
                    this.ddlProveedor1.Visible = true;
                    this.ddlProveedor1.DataTextField = "nombre";
                    this.ddlProveedor1.DataValueField = "id";
                    this.ddlProveedor1.DataSource = dtProveedores;
                    this.ddlProveedor1.DataBind();
                    this.ddlProveedor1.Items.Insert(0, new ListItem("--Prestador--", "0"));

                }
            }
        }

        /// <summary>
        /// Método, Carga los objetos de de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public ArrayList LoadProveedores(int p_tipoServicio, short p_idTipoSolicitud)
        {
            if (this.ddlProveedor1.Visible)
            {
                //Si es un reembolso
                if (p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso) && this.ddlProveedor1.SelectedValue == "0" && this.ddlProveedor2.SelectedValue == "0" && this.ddlProveedor3.SelectedValue == "0")
                    throw new Exception("Debe adicionar al menos un prestador");

                //Si el tipo de servicio es medicamentos
                if (p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion) && p_tipoServicio != 17 && this.ddlProveedor1.SelectedValue == "0" && this.ddlProveedor2.SelectedValue == "0" && this.ddlProveedor3.SelectedValue == "0")
                    throw new Exception("Debe adicionar al menos un prestador si el tipo de servicio no es medicamentos");
            }
            else
            {
                //Si es un reembolso
                if (p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso) && this.txtProveedor1.Text == string.Empty && this.txtProveedor2.Text == string.Empty && this.txtProveedor3.Text == string.Empty)
                    throw new Exception("Debe adicionar al menos un prestador");

                //Si el tipo de servicio es medicamentos y es autorizacion
                if (p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion) && p_tipoServicio != 17 && this.txtProveedor1.Text == string.Empty && this.txtProveedor2.Text == string.Empty && this.txtProveedor3.Text == string.Empty)
                    throw new Exception("Debe adicionar al menos un prestador si el tipo de servicio no es medicamentos");
            }

            SolicitudTipoServicioProveedores objProveedores = new SolicitudTipoServicioProveedores();
            ArrayList arrProveedores = new ArrayList();

            if (this.ddlProveedor1.Visible && this.ddlProveedor1.SelectedValue != "0")
            {
                SolicitudTipoServicioProveedores objProveedores1 = new SolicitudTipoServicioProveedores();
                objProveedores1.IdProveedor = Convert.ToInt32(this.ddlProveedor1.SelectedValue);
                objProveedores1.DespliegaUVR = this.chkDesplegarUVR1.Checked;
                if (this.txtConsecutivo1.Text != string.Empty)
                    objProveedores1.Consecutivo = Convert.ToInt32(this.txtConsecutivo1.Text);
                if (objProveedores1.DespliegaUVR)
                    this.txtUVR.Text = "1";
                arrProveedores.Add(objProveedores1);

            }
            else
            {
                if (this.txtProveedor1.Text != string.Empty)
                {
                    SolicitudTipoServicioProveedores objProveedores1 = new SolicitudTipoServicioProveedores();
                    objProveedores1.IdProveedor = Convert.ToInt32(this.txtIdProveedor1.Text);
                    objProveedores1.DespliegaUVR = this.chkDesplegarUVR1.Checked;
                    if (this.txtConsecutivo1.Text != string.Empty)
                        objProveedores1.Consecutivo = Convert.ToInt32(this.txtConsecutivo1.Text);
                    if (objProveedores1.DespliegaUVR)
                        this.txtUVR.Text = "1";
                    arrProveedores.Add(objProveedores1);
                }
            }
            if (this.ddlProveedor2.Visible && this.ddlProveedor2.SelectedValue != "0")
            {
                SolicitudTipoServicioProveedores objProveedores2 = new SolicitudTipoServicioProveedores();
                objProveedores2.IdProveedor = Convert.ToInt32(this.ddlProveedor2.SelectedValue);
                objProveedores2.DespliegaUVR = this.chkDesplegarUVR2.Checked;
                if (this.txtConsecutivo2.Text != string.Empty)
                    objProveedores2.Consecutivo = Convert.ToInt32(this.txtConsecutivo2.Text);
                if (objProveedores2.DespliegaUVR)
                    this.txtUVR.Text = "1";
                arrProveedores.Add(objProveedores2);

            }
            else
            {
                if (this.txtProveedor2.Text != string.Empty)
                {
                    SolicitudTipoServicioProveedores objProveedores2 = new SolicitudTipoServicioProveedores();
                    objProveedores2.IdProveedor = Convert.ToInt32(this.txtIdProveedor2.Text);
                    objProveedores2.DespliegaUVR = this.chkDesplegarUVR2.Checked;
                    if (this.txtConsecutivo2.Text != string.Empty)
                        objProveedores2.Consecutivo = Convert.ToInt32(this.txtConsecutivo2.Text);
                    if (objProveedores2.DespliegaUVR)
                        this.txtUVR.Text = "1";
                    arrProveedores.Add(objProveedores2);
                }
            }
            if (this.ddlProveedor3.Visible && this.ddlProveedor3.SelectedValue != "0")
            {
                SolicitudTipoServicioProveedores objProveedores3 = new SolicitudTipoServicioProveedores();
                objProveedores3.IdProveedor = Convert.ToInt32(this.ddlProveedor3.SelectedValue);
                objProveedores3.DespliegaUVR = this.chkDesplegarUVR3.Checked;
                if (this.txtConsecutivo3.Text != string.Empty)
                    objProveedores3.Consecutivo = Convert.ToInt32(this.txtConsecutivo3.Text);
                if (objProveedores3.DespliegaUVR)
                    this.txtUVR.Text = "1";
                arrProveedores.Add(objProveedores3);

            }
            else
            {
                if (this.txtProveedor3.Text != string.Empty)
                {
                    SolicitudTipoServicioProveedores objProveedores3 = new SolicitudTipoServicioProveedores();
                    objProveedores3.IdProveedor = Convert.ToInt32(this.txtIdProveedor3.Text);
                    objProveedores3.DespliegaUVR = this.chkDesplegarUVR3.Checked;
                    if (this.txtConsecutivo3.Text != string.Empty)
                        objProveedores3.Consecutivo = Convert.ToInt32(this.txtConsecutivo3.Text);
                    if (objProveedores3.DespliegaUVR)
                        this.txtUVR.Text = "1";
                    arrProveedores.Add(objProveedores3);
                }
            }

            if ((p_tipoServicio == 17 && arrProveedores.Count == 0) || (p_idTipoSolicitud == Convert.ToInt16(Solicitud.EnumTipoSolicitud.Orden) && arrProveedores.Count == 0))
            {
                objProveedores = new SolicitudTipoServicioProveedores();
                arrProveedores.Add(objProveedores);
            }
            return arrProveedores;
        }

        /// <summary>
        /// Metodo, carga los controles de los proveedores del tipo de servicio
        /// </summary>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlProveedores(long p_idSolicitudTipoServicio)
        {
            DataRow dr;
            SolicitudTipoServicioProveedores objProveedor = new SolicitudTipoServicioProveedores();
            objProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

            if (Request.QueryString["IdSolicitud"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            if (Request.QueryString["IdSolicitudCopia"] != null)
                objProveedor.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

            DataTable dtProveedores = objProveedor.ConsultSolicitudTipoServicioProveedores().Tables[0];

            if (dtProveedores.Rows.Count > 0)
            {
                dr = dtProveedores.Rows[0];
                if (this.ddlProveedor1.Visible)
                {
                    this.ddlProveedor1.SelectedValue = dr["IdProveedor"].ToString();
                    this.chkDesplegarUVR1.Checked = Convert.ToBoolean(dr["DespliegaUVR"].ToString());
                    this.txtConsecutivo1.Text = dr["Consecutivo"].ToString();
                }
                else
                {
                    this.txtProveedor1.Text = dr["NombreProveedor"].ToString();
                    this.txtIdProveedor1.Text = dr["IdProveedor"].ToString();
                    this.chkDesplegarUVR1.Checked = Convert.ToBoolean(dr["DespliegaUVR"].ToString());
                    this.txtConsecutivo1.Text = dr["Consecutivo"].ToString();
                    this.txtProveedor1.ToolTip = dr["NombreProveedor"].ToString() + " " + dr["Direcciones"].ToString() + " Tel " + dr["Telefonos"].ToString() + "-" + dr["Horario"].ToString();
                }
            }

            if (dtProveedores.Rows.Count > 1)
            {
                dr = dtProveedores.Rows[1];
                this.txtProveedor2.Text = dr["NombreProveedor"].ToString();
                this.txtIdProveedor2.Text = dr["IdProveedor"].ToString();
                this.chkDesplegarUVR2.Checked = Convert.ToBoolean(dr["DespliegaUVR"].ToString());
                this.txtConsecutivo2.Text = dr["Consecutivo"].ToString();
                this.txtProveedor2.ToolTip = dr["NombreProveedor"].ToString() + " " + dr["Direcciones"].ToString() + " Tel " + dr["Telefonos"].ToString() + "-" + dr["Horario"].ToString();
            }

            if (dtProveedores.Rows.Count > 2)
            {
                dr = dtProveedores.Rows[2];
                this.txtProveedor3.Text = dr["NombreProveedor"].ToString();
                this.txtIdProveedor3.Text = dr["IdProveedor"].ToString();
                this.chkDesplegarUVR3.Checked = Convert.ToBoolean(dr["DespliegaUVR"].ToString());
                this.txtConsecutivo3.Text = dr["Consecutivo"].ToString();
                this.txtProveedor3.ToolTip = dr["NombreProveedor"].ToString() + " " + dr["Direcciones"].ToString() + " Tel " + dr["Telefonos"].ToString() + "-" + dr["Horario"].ToString();
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
