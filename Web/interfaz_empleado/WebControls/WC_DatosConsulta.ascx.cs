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
    ///	Control que despliega los datos principales de una consulta
    /// </summary>
    public partial class WC_DatosConsulta : WC_Base
    {

        #region Atributos

        

        #endregion

        #region Inicialización

        /// <summary>
        /// Evento, inicializa el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (Request.QueryString["idConsulta"] != null)
            {
                this.LoadControlConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, carga la información del empleado
        /// </summary>
        private void LoadControlConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();

            this.lblFecha.Text = objConsulta.FechaCreacion.ToShortDateString();
            this.lblNoConsulta.Text = objConsulta.ConsecutivoNombre;
            this.lblControl.Text = objConsulta.CitaControl.ToShortDateString();

            Prestadores objPrestador = new Prestadores();
            objPrestador.IdPrestador = objConsulta.IdPrestador;
            objPrestador.GetPrestadores();
            this.lblSolicitante.Text = objPrestador.NombrePrestador + "-" + objPrestador.NombreEspecialidad;

            //Cargar tipo de consulta
            GeneralTable objGeneral = new GeneralTable();
            objGeneral.TableName = "TipoConsultas";
            objGeneral.ColumnName = "TipoConsulta";
            objGeneral.Id = objConsulta.IdTipoConsulta;
            objGeneral.GetGeneralTable();
            this.lblTipoConsulta.Text = objGeneral.Nombre;
            //Cargar tipo de enfermedad
            objGeneral.TableName = "TipoEnfermedades";
            objGeneral.ColumnName = "TipoEnfermedad";
            objGeneral.Id = objConsulta.IdTipoEnfermedad;
            objGeneral.GetGeneralTable();
            this.lblTipoEnfermedad.Text = objGeneral.Nombre;
        }

        #endregion

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
    }
}
