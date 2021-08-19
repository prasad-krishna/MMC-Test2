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

namespace TPA.interfaz_empleado.reportes
{
    /// <summary>
    /// Lista los reportes disponibles
    /// </summary>
    public partial class principalreportes : PB_PaginaBase
    {
        #region Atributos


        #endregion

        #region Inicialización

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    if (Session["SICAU"] != null)
                        this.FillList(this.ddlReportes, "UserReportes", "--Seleccione el Reporte--", Convert.ToInt32(Request.QueryString["SICAU_solicitud_id"]));
                    else
                        this.FillList(this.ddlReportes, "UserReportes", "--Seleccione el Reporte--", Convert.ToInt32(Session["IdUser"]));
                    if (this.ddlReportes.Items.Count <= 1)
                    {
                        this.ddlReportes.Items.Insert(0, (new ListItem("--Seleccione el Reporte--", "0")));
                        this.ddlReportes.SelectedValue = "0";
                    }



                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlReportes.SelectedIndexChanged += new System.EventHandler(this.ddlReportes_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza la carga del reporte seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlReportes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Response.Redirect(this.ddlReportes.SelectedValue);

        }

        #endregion
    }
}
