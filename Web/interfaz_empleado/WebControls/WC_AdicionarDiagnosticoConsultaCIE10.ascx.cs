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
    using Mercer.Medicines.Logic;
    /// <summary>
    ///	Control para adición de diagnósticos
    /// </summary>
    public partial class WC_AdicionarDiagnosticoConsultaCIE10 : WC_Base
    {

        #region Atributes

        protected System.Web.UI.WebControls.Label lblTiempoEvolución1;
        protected System.Web.UI.WebControls.Label lblDiagnostico;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtDiagnostico1;
        protected System.Web.UI.WebControls.Button btnBuscarDiagnostico1;
        protected System.Web.UI.WebControls.TextBox txtIdDiagnostico1;
        
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
            this.btnBuscarDiagnostico1.Attributes.Add("OnClick", "javascript:ShowDiagnostico(this,'" + this.txtIdDiagnostico1.ClientID + "','" + this.txtDiagnostico1.ClientID + "');");
            
             //Se agrega el atributo readonly 
            txtDiagnostico1.Attributes.Add("ReadOnly", "ReadOnly");
            GeneralTable objGeneral = new GeneralTable();
            objGeneral.TableName = "TipoDiagnosticos";
            objGeneral.ColumnName = "TipoDiagnostico";
            DataTable dtTipoDiagnosticos = objGeneral.ConsultGeneralTable().Tables[0];

           
        }

        /// <summary>
        /// Método, Carga los objetos de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objConsulta"></param>
        public void LoadDiagnosticos(Consulta p_objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos ETiposDiagnostico)
        {
            
            if (this.txtDiagnostico1.Text.Trim() != string.Empty)
            {
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoExamenVisual == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoExamenVisual = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoNutricional == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoNutricional= Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoIncapacidad == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoIncapacidad = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion1 == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoHospitalizacion1 = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion2 == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoHospitalizacion2 = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion3 == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoHospitalizacion3 = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion4 == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoHospitalizacion4 = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                if (Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoTrastorno == ETiposDiagnostico)
                    p_objConsulta.IdDiagnosticoTrastorno = Convert.ToInt32(this.txtIdDiagnostico1.Text);
                
            }
    
        }

        /// <summary>
        /// Método, Carga los controles con los diagnósticos del tipo de servicio
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void LoadControlDiagnosticos(int p_idDiagnostico)
        {
            DataRow dr;
            Diagnosticos objDiagnostico = new Diagnosticos();
            objDiagnostico.IdDiagnostico = p_idDiagnostico;
            objDiagnostico.GetDiagnosticos();
            this.txtDiagnostico1.Text = objDiagnostico.NombreDiagnostico;
            this.txtIdDiagnostico1.Text = objDiagnostico.IdDiagnostico.ToString();
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
