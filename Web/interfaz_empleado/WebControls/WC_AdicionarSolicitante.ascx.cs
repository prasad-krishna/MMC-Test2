namespace TPA.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Control para adición de solicitante diferente en tipo de servicio
    /// </summary>
    public partial class WC_AdicionarSolicitante : WC_Base
    {

        #region Atributes

        
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
            txtPrestador.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin MAHG 12/01/10
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la carga inicial de los controles
        /// </summary>
        /// <param name="p_idTipoServicio"></param>
        public void LoadControls()
        {
            this.chkOtroSolicitante.Attributes.Add("OnClick", "javascript:ShowSolicitante(this, '" + this.dvSolicitante.ClientID + "');");
            Prestadores objPrestador = new Prestadores();
            objPrestador.Empresa_id = Convert.ToInt32(Session["Company"]);
            DataTable dtPrestadores = objPrestador.ConsultEmpresaPrestadores().Tables[0];

            if (dtPrestadores.Rows.Count == 1)
            {
                this.ddlPrestador.DataTextField = "nombre";
                this.ddlPrestador.DataValueField = "id";
                this.ddlPrestador.DataSource = dtPrestadores;
                this.ddlPrestador.DataBind();
                this.ddlPrestador.SelectedIndex = 0;
                this.ddlPrestador.Visible = true;

            }
            else
            {
                if (dtPrestadores.Rows.Count > 20)
                {
                    this.txtPrestador.Visible = true;
                    this.btnBuscarPrestador.Style["display"] = "";
                    this.btnBuscarPrestador.Attributes.Add("OnClick", "javascript:ShowPrestadorTipoServicio(this,'" + this.txtIdPrestador.ClientID + "','" + this.txtPrestador.ClientID + "');");
                }
                else
                {
                    this.ddlPrestador.Visible = true;
                    this.ddlPrestador.DataTextField = "nombre";
                    this.ddlPrestador.DataValueField = "id";
                    this.ddlPrestador.DataSource = dtPrestadores;
                    this.ddlPrestador.DataBind();
                    this.ddlPrestador.Items.Insert(0, new ListItem("--Solicitante--", "0"));

                }
            }
        }


        /// <summary>
        /// Método, retorna el id del prestador o solicitante seleccionado
        /// </summary>
        /// <returns></returns>
        public int GetSolicitante()
        {
            if (this.chkOtroSolicitante.Checked)
                return Convert.ToInt32(this.txtIdPrestador.Text);

            else
                return 0;
        }

        public void SetSolicitante(int p_idPrestador)
        {
            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = p_idPrestador;
            objPrestador.GetPrestadores();
            this.txtIdPrestador.Text = p_idPrestador.ToString();
            this.txtPrestador.Text = objPrestador.NombrePrestador;
            this.chkOtroSolicitante.Checked = true;
            this.dvSolicitante.Style["display"] = "";

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
