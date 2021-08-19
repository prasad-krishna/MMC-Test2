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
    /// Realiza la búsqueda del log de auditoría
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 7 de Octubre de 2008</remarks>
    public partial class LIS_log : PB_PaginaBase
    {


        #region Atributtes

        protected System.Web.UI.WebControls.TextBox txtFechaInicio;
        protected System.Web.UI.WebControls.TextBox txtFechaFin;
        protected System.Web.UI.WebControls.DropDownList ddlUsuario;
        protected System.Web.UI.WebControls.DropDownList ddlAccion;
        protected System.Web.UI.WebControls.Button btnBuscar;
        protected System.Web.UI.WebControls.DataGrid dgdLog;


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
                    LoadControls();
                    this.FillList("ActionLog", "ActionLog", this.ddlAccion, "--Acción--");


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
            //base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dgdLog.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgdLog_PageIndexChanged);
            this.dgdLog.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgdLog_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado a la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dgdLog.CurrentPageIndex = 0;
                this.FindListLog();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, realiza la paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgdLog_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dgdLog.CurrentPageIndex = e.NewPageIndex;
            this.FindListLog();

        }

        /// <summary>
        /// Evento, carga evento para selección en grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgdLog_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                Label lblUsuario = (Label)e.Item.FindControl("lblUsuario");

                if (e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                    e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");

                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                    e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");

                }

                if (e.Item.Cells[0].Text != "&nbsp;" && e.Item.Cells[0].Text != "0")
                {
                    SIC_USUARIO objUsuario = new SIC_USUARIO();
                    objUsuario.Usuario_id = Convert.ToInt32(e.Item.Cells[0].Text);
                    DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                    lblUsuario.Text = dtUsuario.Rows[0]["nombre"].ToString();
                }
                else
                {
                    lblUsuario.Text = rowItem["NameUser"].ToString();
                }

            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// Método, realiza la búsqueda del log
        /// </summary>
        public void FindListLog()
        {
            Log objLog = new Log();
            object dateFrom = null;
            object dateUntil = null;

            if (this.ddlUsuario.SelectedValue.StartsWith("SICAU"))
            {
                objLog.usuario_id = Convert.ToInt32(this.ddlUsuario.SelectedValue.Split('-')[1]);
            }
            else
                objLog.IdUser = Convert.ToInt32(this.ddlUsuario.SelectedValue);

            objLog.IdActionLog = (Log.EnumActionsLog)Convert.ToInt32(this.ddlAccion.SelectedValue);

            if (this.txtFechaInicio.Text.Trim() != string.Empty)
            {
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
            }
            if (this.txtFechaFin.Text.Trim() != string.Empty)
            {
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
            }
            this.dgdLog.DataSource = objLog.ConsultLog(dateFrom, dateUntil);
            this.dgdLog.DataBind();
        }

        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly 
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            Users objUser = new Users();
            objUser.Active = true;
            DataTable dtUsers = objUser.ConsultUsers().Tables[0];
            this.ddlUsuario.Items.Add(new ListItem("--Todos--", "0"));
            foreach (DataRow row in dtUsers.Rows)
            {
                this.ddlUsuario.Items.Add(new ListItem(row["NameUser"].ToString(), row["IdUser"].ToString()));
            }

            SIC_USUARIO objUsuario = new SIC_USUARIO();
            DataTable dtUsersSICAU = objUsuario.ConsultSIC_USUARIO(1).Tables[0];
            foreach (DataRow rowSICAU in dtUsersSICAU.Rows)
            {
                this.ddlUsuario.Items.Add(new ListItem(rowSICAU["nombre"].ToString(), "SICAU-" + rowSICAU["usuario_id"].ToString()));
            }
        }


        #endregion




    }
}
