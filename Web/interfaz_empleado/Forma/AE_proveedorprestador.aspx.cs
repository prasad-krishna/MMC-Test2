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

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza el ingreso o actualización de Proveedores
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 16 de Octubre de 2008</remarks>
    public partial class AE_proveedorprestador : PB_PaginaBase
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

        #region Events

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
            this.chkRetirar.CheckedChanged += new System.EventHandler(this.chkRetirar_CheckedChanged);            
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        

        /// <summary>
        /// Despliega u oculta los campos para retiro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void chkRetirar_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chkRetirar.Checked)
            {
                this.trRetiro.Style["display"] = "";
                this.lblMotivo.Visible = true;
                this.txtMotivoRetiro.Visible = true;
                this.rfvFechaRetiro.Enabled = true;
                this.rfvMotivoRetiro.Enabled = true;
                this.rfvPersonaRetiro.Enabled = true;
            }
            else
            {
                this.trRetiro.Style["display"] = "none";
                this.lblMotivo.Visible = false;
                this.txtMotivoRetiro.Visible = false;
                this.rfvFechaRetiro.Enabled = false;
                this.rfvMotivoRetiro.Enabled = false;
                this.rfvPersonaRetiro.Enabled = false;
            }
        }

        protected void btnAceptar_Click1(object sender, EventArgs e)
        {
            try
            {
                if (this.checkprestador.Checked == false && this.checkproveedor.Checked == false)
                    throw new Exception("Debe seleccionar si es prestador y/o proveedor");

                if ((Request.QueryString["IdProveedor"] != null && Request.QueryString["IdProveedor"] != string.Empty && Request.QueryString["IdProveedor"] != "0") || (Request.QueryString["IdPrestador"] != null && Request.QueryString["IdPrestador"] != string.Empty && Request.QueryString["IdPrestador"] != "0"))
                {
                    int idProveedor = (Request.QueryString["IdProveedor"] != null && Request.QueryString["IdProveedor"] != string.Empty && Request.QueryString["IdProveedor"] != "0") ? Convert.ToInt32(Request.QueryString["IdProveedor"]) : 0;
                    int idPrestador = (Request.QueryString["IdPrestador"] != null && Request.QueryString["IdPrestador"] != string.Empty && Request.QueryString["IdPrestador"] != "0") ? Convert.ToInt32(Request.QueryString["IdPrestador"]) : 0;
                    this.updateProveedorPrestador(idProveedor, idPrestador);
                    if (idProveedor != 0)
                        this.RegisterLog(Log.EnumActionsLog.ModificarProveedor, Convert.ToInt32(Request.QueryString["IdProveedor"]), "Id Proveedor:" + Convert.ToInt32(Request.QueryString["IdProveedor"]) + " Nombre:" + this.txtNombre.Text.ToUpper());
                    if (idPrestador != 0)
                        this.RegisterLog(Log.EnumActionsLog.ModificarPrestador, Convert.ToInt32(Request.QueryString["idPrestador"]), "Id Prestador:" + Convert.ToInt32(Request.QueryString["IdPrestador"]) + " Nombre:" + this.txtNombre.Text.ToUpper());

                    Response.Write("<script>alert('El solicitante y/o prestador modificado exitosamente'); top.close();</script>");

                }
                else
                {
                    int IdPrestador = this.insertProveedorPrestador();

                    if (Request.QueryString["Validar"] != null)
                    {
                        Response.Write("<script>alert('El solicitante y/o prestador adicionado exitosamente');</script>");
                        Response.Write("<script> window.opener.AsociarPrestador('" + IdPrestador + "', '"+ this.txtNombre.Text +"'); top.close();</script>");
                    }

                    Response.Write("<script>alert('El solicitante y/o prestador adicionado exitosamente'); top.close();</script>");
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Método, Realiza la carga inicial de controles
        /// </summary>
        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly                         
            txtFechaIngreso.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaRetiro.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            //Inicio EBC 17/10/2012
            //Se agrega el atributo readonly                         
            txtFechaExpedicion.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin EBC 17/10/2012

            this.FillList("Especialidades", "Especialidad", this.ddlTipoProveedor, "--Especialidad--");
            this.FillList("Ciudades", "Ciudad", this.ddlCiudad, "--Ciudad---");
            this.FillList("EstadoGenerales", "EstadoGeneral", this.ddlEstado, "--Estado--");
            this.ddlEstado.SelectedValue = "1";

            if (Request.QueryString["Validar"] != null)
            {

                this.txtNombre.Text = Request.QueryString["NombrePrestador"];
                this.txtEmail.Text = Request.QueryString["Email"];
                this.ddlCiudad.SelectedValue = Request.QueryString["Ciudad"];
                //this.txtTelefonos.Text = Request.QueryString["Empresa"];

                this.checkprestador.Disabled = true;
                this.checkprestador.Checked = true;

            }

            if (Request.QueryString["IdProveedor"] != null && Request.QueryString["IdProveedor"] != string.Empty && Request.QueryString["IdProveedor"] != "0")
            {
                this.loadFormProveedor(Convert.ToInt32(Request.QueryString["IdProveedor"]));
            }
            else
            {
                if (Request.QueryString["IdPrestador"] != null && Request.QueryString["IdPrestador"] != string.Empty && Request.QueryString["IdPrestador"] != "0")
                {
                    this.loadFormPrestador(Convert.ToInt32(Request.QueryString["IdPrestador"]));
                }
            }

        }

        /// <summary>
        /// Método, realiza la inserción del Proveedor
        /// </summary>
        /// <returns></returns>
        /// 
        //************************************************************************************
        public int insertProveedorPrestador()
        {
            int idPrestador = 0;
            int idProveedor = 0;

            if (this.checkprestador.Checked)
            {
                Prestadores objPrestador = new Prestadores();
                this.loadObjectPrestador(objPrestador);
                idPrestador = objPrestador.InsertPrestadores();
                this.RegisterLog(Log.EnumActionsLog.InsertarPrestador, idPrestador, "Id Prestador:" + idPrestador + " Nombre:" + this.txtNombre.Text.ToUpper());

            }
            if (this.checkproveedor.Checked)
            {
                Proveedores objProveedor = new Proveedores();
                this.loadObjectProveedor(objProveedor);
                if (idPrestador != 0)
                    objProveedor.IdPrestador = idPrestador;
                idProveedor = objProveedor.InsertProveedores();
                this.RegisterLog(Log.EnumActionsLog.InsertarProveedor, idProveedor, "Id Proveedor:" + idProveedor + " Nombre:" + this.txtNombre.Text.ToUpper());
            }


            return idPrestador;
        }
        //**********************************************************************************

        /// <summary>
        /// Método, realiza la modificación del Proveedor
        /// </summary>
        /// <param name="p_IdProveedor"></param>
        public void updateProveedorPrestador(int p_IdProveedor, int p_idPrestador)
        {
            if (this.checkprestador.Checked)
            {
                if (p_idPrestador != 0)
                {
                    Prestadores objPrestador = new Prestadores();
                    this.loadObjectPrestador(objPrestador);
                    objPrestador.IdPrestador = p_idPrestador;
                    objPrestador.UpdatePrestadores();
                }
                else
                {
                    Prestadores objPrestador = new Prestadores();
                    this.loadObjectPrestador(objPrestador);
                    p_idPrestador = objPrestador.InsertPrestadores();
                }
            }
            else
            {
                if (p_idPrestador != 0)
                {
                    Prestadores objPrestador = new Prestadores();
                    this.loadObjectPrestador(objPrestador);
                    objPrestador.IdPrestador = p_idPrestador;
                    objPrestador.IdEstadoGeneral = 0; //Inactivar prestador
                    objPrestador.UpdatePrestadores();
                }
            }
            if (this.checkproveedor.Checked)
            {
                if (p_IdProveedor != 0)
                {
                    Proveedores objProveedor = new Proveedores();
                    this.loadObjectProveedor(objProveedor);
                    if (p_idPrestador != 0)
                        objProveedor.IdPrestador = p_idPrestador;
                    objProveedor.IdProveedor = p_IdProveedor;
                    objProveedor.UpdateProveedores();
                }
                else
                {
                    Proveedores objProveedor = new Proveedores();
                    this.loadObjectProveedor(objProveedor);
                    if (p_idPrestador != 0)
                        objProveedor.IdPrestador = p_idPrestador;
                    objProveedor.InsertProveedores();
                }
            }
            else
            {
                if (p_IdProveedor != 0)
                {
                    Proveedores objProveedor = new Proveedores();
                    this.loadObjectProveedor(objProveedor);
                    if (p_idPrestador != 0)
                        objProveedor.IdPrestador = p_idPrestador;
                    objProveedor.IdProveedor = p_IdProveedor;
                    objProveedor.IdEstadoGeneral = 0; //Inactivar prestador
                    objProveedor.UpdateProveedores();
                }
            }
        }

        /// <summary>
        /// Método, carga un objeto Proveedor con los datos del formulario
        /// </summary>
        /// <param name="objProveedor"></param>
        public void loadObjectProveedor(Proveedores objProveedor)
        {
            objProveedor.NombreProveedor = this.txtNombre.Text;
            if (Convert.ToInt32(this.ddlTipoProveedor.SelectedValue) > 0)
            {
                objProveedor.IdEspecialidad = Convert.ToInt16(this.ddlTipoProveedor.SelectedValue);
            }
            objProveedor.Telefonos = this.txtTelefonos.Text;
            objProveedor.Direcciones = this.txtDirecciones.Text;
            objProveedor.Horario = this.txtHorario.Text;
            objProveedor.NIT = this.txtNit.Text;
            objProveedor.Fax = this.txtFax.Text;
            objProveedor.Email = this.txtEmail.Text;
            objProveedor.IdEstadoGeneral = Convert.ToInt32(this.ddlEstado.SelectedValue);
            objProveedor.IdCiudad = Convert.ToInt32(this.ddlCiudad.SelectedValue);
            objProveedor.Supraespecialidad = this.txtSupraespecialidad.Text;
            objProveedor.FechaIngreso = Convert.ToDateTime(this.txtFechaIngreso.Text);
            objProveedor.PersonaAprobacionIngreso = this.txtPersonaIngreso.Text;
            objProveedor.IdTpaAnterior = this.txtTpaAnterior.Text;
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);
            objProveedor.Activo = !this.chkRetirar.Checked;
            objProveedor.FechaRetiro = new DateTime(1900, 1, 1);
            objProveedor.Cedula = txtCedula.Text.Trim();
            //Inicio EBC 17/10/2012
            if(this.txtFechaExpedicion.Text != string.Empty)
                objProveedor.FechaExpedicion = Convert.ToDateTime(this.txtFechaExpedicion.Text);
            else
                objProveedor.FechaExpedicion = new DateTime(1900, 1, 1);
            objProveedor.Institucion = this.txtInstitucion.Text.Trim();
            //Fin EBC 17/10/2012

            if (this.chkRetirar.Checked)
            {
                objProveedor.FechaRetiro = Convert.ToDateTime(this.txtFechaRetiro.Text);
                objProveedor.MotivoRetiro = this.txtMotivoRetiro.Text;
                objProveedor.PersonaAprobacionRetiro = this.txtPersonaRetiro.Text;
            }

        }


        /// <summary>
        /// Método, carga un objeto Prestador con los datos del formulario
        /// </summary>
        /// <param name="objPrestador"></param>
        public void loadObjectPrestador(Prestadores objPrestador)
        {
            objPrestador.NombrePrestador = this.txtNombre.Text;
            if (Convert.ToInt32(this.ddlTipoProveedor.SelectedValue) > 0)
            {
                objPrestador.IdEspecialidad = Convert.ToInt16(this.ddlTipoProveedor.SelectedValue);
            }
            objPrestador.Telefonos = this.txtTelefonos.Text;
            objPrestador.Direcciones = this.txtDirecciones.Text;
            objPrestador.Horario = this.txtHorario.Text;
            objPrestador.Nit = this.txtNit.Text;
            objPrestador.Fax = this.txtFax.Text;
            objPrestador.Email = this.txtEmail.Text;
            objPrestador.IdEstadoGeneral = Convert.ToInt16(this.ddlEstado.SelectedValue);
            objPrestador.IdCiudad = Convert.ToInt32(this.ddlCiudad.SelectedValue);
            objPrestador.Supraespecialidad = this.txtSupraespecialidad.Text;
            objPrestador.FechaIngreso = Convert.ToDateTime(this.txtFechaIngreso.Text);
            objPrestador.PersonaAprobacionIngreso = this.txtPersonaIngreso.Text;
            objPrestador.IdTpaAnterior = this.txtTpaAnterior.Text;
            objPrestador.Empresa_id = Convert.ToInt32(Session["Company"]);
            objPrestador.Activo = !this.chkRetirar.Checked;
            objPrestador.FechaRetiro = new DateTime(1900, 1, 1);
            objPrestador.Cedula = txtCedula.Text.Trim();
            //Inicio EBC 17/10/2012
            if(this.txtFechaExpedicion.Text != string.Empty)
                objPrestador.FechaExpedicion = Convert.ToDateTime(this.txtFechaExpedicion.Text);
            else
                objPrestador.FechaExpedicion = new DateTime(1900, 1, 1);
            objPrestador.Institucion = this.txtInstitucion.Text.Trim();
            //Fin EBC 17/10/2012

            if (this.chkRetirar.Checked)
            {
                objPrestador.FechaRetiro = Convert.ToDateTime(this.txtFechaRetiro.Text);
                objPrestador.MotivoRetiro = this.txtMotivoRetiro.Text;
                objPrestador.PersonaAprobacionRetiro = this.txtPersonaRetiro.Text;
            }

        }

        /// <summary>
        /// Método, consulta el Proveedor y carga el formulario a partir del objeto
        /// </summary>
        /// <param name="p_IdProveedor"></param>
        public void loadFormProveedor(int p_IdProveedor)
        {
            Proveedores objProveedor = new Proveedores();
            objProveedor.IdProveedor = p_IdProveedor;
            objProveedor.Empresa_id = Convert.ToInt32(Session["Company"]);
            objProveedor.GetProveedores();
            this.txtNombre.Text = objProveedor.NombreProveedor;
            this.ddlTipoProveedor.SelectedValue = objProveedor.IdEspecialidad.ToString();
            this.txtTelefonos.Text = objProveedor.Telefonos;
            this.txtHorario.Text = objProveedor.Horario;
            this.txtNit.Text = objProveedor.NIT;
            this.txtFax.Text = objProveedor.Fax;
            this.txtDirecciones.Text = objProveedor.Direcciones.ToString();
            this.txtEmail.Text = objProveedor.Email;
            this.ddlEstado.SelectedValue = objProveedor.IdEstadoGeneral.ToString();
            this.ddlCiudad.SelectedValue = objProveedor.IdCiudad.ToString();
            this.txtSupraespecialidad.Text = objProveedor.Supraespecialidad;
            this.txtFechaIngreso.Text = objProveedor.FechaIngreso.ToShortDateString();
            this.txtPersonaIngreso.Text = objProveedor.PersonaAprobacionIngreso;
            this.chkRetirar.Checked = !objProveedor.Activo;
            this.txtTpaAnterior.Text = objProveedor.IdTpaAnterior;
            this.txtCedula.Text = objProveedor.Cedula;
            //Inicio EBC 17/10/2012
            if (objProveedor.FechaExpedicion.ToShortDateString() != "01/01/0001")
            {
                this.txtFechaExpedicion.Text = objProveedor.FechaExpedicion.ToShortDateString();
            }
            this.txtInstitucion.Text = objProveedor.Institucion;
            //Fin EBC 17/10/2012
            
            if (!objProveedor.Activo)
            {
                this.trRetiro.Style["display"] = "";
                this.lblMotivo.Visible = true;
                this.txtMotivoRetiro.Visible = true;
                this.rfvFechaRetiro.Enabled = true;
                this.rfvMotivoRetiro.Enabled = true;
                this.rfvPersonaRetiro.Enabled = true;
                this.txtMotivoRetiro.Text = objProveedor.MotivoRetiro;
                this.txtPersonaRetiro.Text = objProveedor.PersonaAprobacionRetiro;
                this.txtFechaRetiro.Text = objProveedor.FechaRetiro.ToShortDateString();
            }

            if (objProveedor.IdPrestador != 0)
            {
                this.checkproveedor.Checked = true;
                this.checkprestador.Checked = true;
            }
            else
            {
                this.checkproveedor.Checked = true;
                this.checkprestador.Checked = false;
            }

            this.checkproveedor.Disabled = true;
            this.checkprestador.Disabled = true;
        }


        /// <summary>
        /// Método, consulta el Prestador y carga el formulario a partir del objeto
        /// </summary>
        /// <param name="p_IdPrestador"></param>
        public void loadFormPrestador(int p_IdPrestador)
        {
            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = p_IdPrestador;
            objPrestador.Empresa_id = Convert.ToInt32(Session["Company"]);
            objPrestador.GetPrestadores();
            this.txtNombre.Text = objPrestador.NombrePrestador;
            this.ddlTipoProveedor.SelectedValue = objPrestador.IdEspecialidad.ToString();
            this.txtTelefonos.Text = objPrestador.Telefonos;
            this.txtDirecciones.Text = objPrestador.Direcciones;
            this.txtHorario.Text = objPrestador.Horario;
            this.txtNit.Text = objPrestador.Nit;
            this.txtFax.Text = objPrestador.Fax;
            this.txtEmail.Text = objPrestador.Email;
            this.ddlEstado.SelectedValue = objPrestador.IdEstadoGeneral.ToString();
            this.ddlCiudad.SelectedValue = objPrestador.IdCiudad.ToString();
            this.txtSupraespecialidad.Text = objPrestador.Supraespecialidad;
            this.txtFechaIngreso.Text = objPrestador.FechaIngreso.ToShortDateString();
            this.txtPersonaIngreso.Text = objPrestador.PersonaAprobacionIngreso;
            this.txtTpaAnterior.Text = objPrestador.IdTpaAnterior;
            this.chkRetirar.Checked = !objPrestador.Activo;
            this.txtCedula.Text = objPrestador.Cedula;
            //Inicio EBC 17/10/2012
            if (objPrestador.FechaExpedicion.ToShortDateString() != "01/01/0001")
            {
                this.txtFechaExpedicion.Text = objPrestador.FechaExpedicion.ToShortDateString();
            }
            this.txtInstitucion.Text = objPrestador.Institucion;
            //Fin EBC 17/10/2012

            if (!objPrestador.Activo)
            {
                this.trRetiro.Style["display"] = "";
                this.lblMotivo.Visible = true;
                this.txtMotivoRetiro.Visible = true;
                this.rfvFechaRetiro.Enabled = true;
                this.rfvMotivoRetiro.Enabled = true;
                this.rfvPersonaRetiro.Enabled = true;
                this.txtMotivoRetiro.Text = objPrestador.MotivoRetiro;
                this.txtPersonaRetiro.Text = objPrestador.PersonaAprobacionRetiro;
                this.txtFechaRetiro.Text = objPrestador.FechaRetiro.ToShortDateString();
            }


            this.checkproveedor.Checked = false;
            this.checkprestador.Checked = true;

            this.checkproveedor.Disabled = true;
            this.checkprestador.Disabled = true;
        }

        #endregion

      
       
    }
}
