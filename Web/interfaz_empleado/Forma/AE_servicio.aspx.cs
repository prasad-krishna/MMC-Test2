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
    /// Administra la creación y edición de servicios
    /// </summary>
    public partial class AE_servicio : PB_PaginaBase
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
                    this.FillList(this.chkTipoServicio, "TipoServicioEmpresaEspeciales", Convert.ToInt32(Session["Company"]), true);
                    this.chkTipoServicio.ClearSelection();
                    if (this.chkTipoServicio.Items.Count < 1)
                        this.lblTipoServicio.Visible = false;

                    if (Request.QueryString["IdServicio"] != null && Request.QueryString["IdServicio"] != string.Empty)
                    {                      
                        this.loadFormService(Convert.ToInt32(Request.QueryString["IdServicio"]));
                        
                    }
                    
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
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, Realiza el llamado para la modificación o inserción del Servicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["IdServicio"] != null && Request.QueryString["IdServicio"] != string.Empty)
                {
                    this.updateService(Convert.ToInt32(Request.QueryString["IdServicio"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarServicio, Convert.ToInt32(Request.QueryString["IdServicio"]), "Id Servicio:" + Convert.ToInt32(Request.QueryString["IdServicio"]) + " Servicio:" + this.txtNombre.Text.ToUpper());
                    Response.Write("<script>alert('El servicio fue modificado exitosamente'); top.close();</script>");
                }
                else
                {
                    int IdServicio = this.insertService();
                    this.RegisterLog(Log.EnumActionsLog.InsertarServicio, IdServicio, "Id Servicio:" + IdServicio + " Nombre:" + this.txtNombre.Text.ToUpper());
                    Response.Write("<script>alert('El servicio fue adicionado exitosamente'); top.close();</script>");
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
        /// Método, realiza la inserción del Servicio
        /// </summary>
        /// <returns></returns>
        public int insertService()
        {
            Servicios objService = new Servicios();
            this.loadObjectService(objService);
            int IdServicio = objService.InsertServicios();
            return IdServicio;
        }


        /// <summary>
        /// Método, realiza la modificación del Servicio
        /// </summary>
        /// <param name="p_IdServicio"></param>
        public void updateService(int p_IdServicio)
        {
            Servicios objService = new Servicios();
            this.loadObjectService(objService);
            objService.IdServicio = p_IdServicio;
            objService.UpdateServicios();
        }

        /// <summary>
        /// Método, carga un objeto Servicio con los datos del formulario
        /// </summary>
        /// <param name="objService"></param>
        public void loadObjectService(Servicios objService)
        {
            int activo_referencia = 0;
            objService.NombreServicio = this.txtNombre.Text.ToUpper();
            objService.CodigoServicio = this.txtCodigoServicio.Text;
            if (this.txtValorConvenio.Text != "")
            {
                objService.ValorConvenio = Convert.ToDecimal(this.txtValorConvenio.Text.Trim());
            }
            objService.Incluye = this.txtIncluye.Text;
            objService.Excluye = this.txtExcluye.Text;
            objService.Simultaneo = this.txtSimultaneo.Text;
            objService.empresa_id = Convert.ToInt32(Session["Company"]);

            ///Validación que permite dejar el check en true o false.
            if (this.chkActivo.Checked == true)
            {
                activo_referencia = 1;
            }
            else
            {
                activo_referencia = 0;
            }
            objService.Activo = activo_referencia;

            ArrayList arrTipoServicios = new ArrayList();

            foreach (ListItem item in this.chkTipoServicio.Items)
            {
                if (item.Selected)
                    arrTipoServicios.Add(Convert.ToInt32(item.Value));
            }
            objService.TipoServicios = arrTipoServicios;
        }

        /// <summary>
        /// Método, consulta el Servicio y carga el formulario a partir del objeto
        /// </summary>
        /// <param name="p_IdServicio"></param>
        public void loadFormService(int p_IdServicio)
        {
            int activo_referencia = 0;
            Servicios objService = new Servicios();
            objService.IdServicio = p_IdServicio;
            objService.GetServicios();
            this.txtNombre.Text = objService.NombreServicio;
            this.txtValorConvenio.Text = objService.ValorConvenio.ToString();
            this.txtCodigoServicio.Text = objService.CodigoServicio;
            this.txtIncluye.Text = objService.Incluye;
            this.txtExcluye.Text = objService.Excluye;
            this.txtSimultaneo.Text = objService.Simultaneo;

            activo_referencia = objService.Activo;
            ///Validación que permite dejar el check en true o false.
            if (activo_referencia == 1)
            {
                this.chkActivo.Checked = true;
            }
            else
            {
                this.chkActivo.Checked = false;
            }

            //Cargar los tipos de servicio asociados
            objService.IdServicio = p_IdServicio;
            DataTable tblTipoServicios = objService.ConsultServicioTipoServicios().Tables[0];

            foreach (DataRow row in tblTipoServicios.Rows)
            {
                ListItem item = this.chkTipoServicio.Items.FindByValue(row["IdTipoServicio"].ToString());
                if (item != null)
                    item.Selected = true;
            }
        }
        #endregion

    }
}
