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
    ///	Descripción breve de WC_BuscarDiagnosticoTipoServicio.
    /// </summary>
    public partial class WC_BuscarDiagnosticoTipoServicio : WC_Base
    {


        #region Atributos

        
        #endregion

        #region Inicialización

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            this.lblResultado.Text = "";
            if (!this.IsPostBack)
            {
                this.txtCodigo.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkBuscar.UniqueID + "',''); return false }");
                this.txtNombre.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) {__doPostBack('" + this.lnkBuscar.UniqueID + "','') ; return false}");
            }

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la búsqueda de los diagnósticos
        /// </summary>
        public void FindDiagnosticos(bool page)
        {
            Diagnosticos objDiagnostico = new Diagnosticos();
            objDiagnostico.CodigoDiagnostico = this.txtCodigo.Text.Trim();
            objDiagnostico.NombreDiagnostico = this.txtNombre.Text.Trim();

            DataView dv = new DataView();
            /*DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("../XMLS/Diagnosticos.xml"));

            dv.Table = dataSet.Tables[0];
            if(this.txtCodigo.Text.Trim() != string.Empty)
                dv.RowFilter = "CodigoDiagnostico ='" + this.txtCodigo.Text.Trim() + "' AND NombreDiagnostico LIKE '*" + this.txtNombre.Text.Trim() + "*'";
            else
                dv.RowFilter = "NombreDiagnostico LIKE '*" + this.txtNombre.Text.Trim() + "*'";*/



            dv.Table = objDiagnostico.ConsultDiagnosticos().Tables[0];

            if (dv.Count < 1)
            {
                this.lblResultado.Text = "No se encuentran resultados";
                this.dtgDiagnosticos.CurrentPageIndex = 0;
                this.dtgDiagnosticos.DataSource = null;
                this.dtgDiagnosticos.DataBind();
            }
            else
            {
                if (dv.Count == 1)
                {
                    this.dtgDiagnosticos.DataSource = dv;
                    this.dtgDiagnosticos.DataBind();
                    string script = "<script language='javascript'>DevolverDiagnosticoTipoServicio('" + dv[0].Row["CodigoDiagnostico"].ToString() + "-" + dv[0].Row["NombreDiagnostico"].ToString() + "'," + dv[0].Row["IdDiagnostico"].ToString() + ")</script>";
                    Page.RegisterStartupScript("Diagnostico" + this.txtTemp.Text, script);


                }
                else
                {
                    if (!page)
                        this.dtgDiagnosticos.CurrentPageIndex = 0;
                    this.dtgDiagnosticos.DataSource = dv;
                    this.dtgDiagnosticos.DataBind();
                }
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
            this.lnkBuscar.Click += new System.EventHandler(this.lnkBuscar_Click);
            this.dtgDiagnosticos.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgDiagnosticos_PageIndexChanged);
            this.dtgDiagnosticos.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgDiagnosticos_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBuscar_Click(object sender, System.EventArgs e)
        {
            this.lblResultado.Text = "";
            this.FindDiagnosticos(false);
        }

        /// <summary>
        /// Evento, realiza la paginación
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgDiagnosticos_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgDiagnosticos.CurrentPageIndex = e.NewPageIndex;
            this.FindDiagnosticos(true);
        }

        /// <summary>
        /// Evento, carga el evento de selección
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgDiagnosticos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                LinkButton lnkDiagnostico = (LinkButton)e.Item.FindControl("lnkDiagnostico");
                lnkDiagnostico.Attributes.Add("onClick", "javascript:DevolverDiagnostico('" + rowItem["CodigoDiagnostico"].ToString() + "-" + rowItem["NombreDiagnostico"].ToString() + "'," + rowItem["IdDiagnostico"].ToString() + ");closeLayer('BuscarDiagnosticoTipoServicio');");
            }


        }

        #endregion




    }
}
