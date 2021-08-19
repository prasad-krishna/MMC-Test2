namespace WebMedicamentos.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///		Descripción breve de WC_PieFormatoOrden.
    /// </summary>
    public partial class WC_PieFormatoOrden : WC_Base
    {       
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            LoadStrings();
            //Fin MAHG 26/01/10

            // Introducir aquí el código de usuario para inicializar la página
        }

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Oculta las etiquetas que no deben mostrarse para México
        /// </summary>
        private void LoadStrings()
        {
            this.lblFechaActual.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
          
        }


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
