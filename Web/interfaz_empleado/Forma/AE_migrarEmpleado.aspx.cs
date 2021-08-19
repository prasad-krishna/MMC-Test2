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
using System.Collections.Generic;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Clase que provee la funcionalidad para adicionar o editar empleados
    /// </summary>
    public partial class AE_migrarEmpleado : PB_PaginaBase
    {

        #region Atributos

        #endregion

        #region Inicialización

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

                }

                Datagrid1.Visible = true;
                Datagrid2.Visible = true;


                DataTable dt = new DataTable();

                DataColumn c0 = new DataColumn("id_empleado");
                DataColumn c1 = new DataColumn("identificacion");
                DataColumn c2 = new DataColumn("nombre_completo");

                dt.Columns.Add(c0);
                dt.Columns.Add(c1);
                dt.Columns.Add(c2);


                DataRow row, row1, row2;

                row = dt.NewRow();

                row["id_empleado"] = "123";

                row["identificacion"] = "555-1234";

                row["nombre_completo"] = "Nombre 1 Apellido 1";

                row1 = dt.NewRow();

                row1["id_empleado"] = "1234";

                row1["identificacion"] = "555-4567";

                row1["nombre_completo"] = "Nombre 2 Apellido 2";

                row2 = dt.NewRow();

                row2["id_empleado"] = "1234";

                row2["identificacion"] = "555-7890";

                row2["nombre_completo"] = "Nombre 3 Apellido 3";

                //Add 3 rows to table

                dt.Rows.Add(row);

                dt.Rows.Add(row1);

                dt.Rows.Add(row2);

                Datagrid1.DataSource = dt;
                Datagrid2.DataSource = dt;
                Datagrid1.DataBind();
                Datagrid2.DataBind();
            }
            catch (Exception ex)
            {
                string message = "";
                message = "<script language='javascript'>alert('Exception :" + ex.Message + "')</script>";


                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin 

            }
        }

        #endregion

        #region Métodos

        #endregion

        #region Eventos

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
            this.Load += new System.EventHandler(this.Page_Load);
            //this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado a la adición o edición del empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Aceptar_Click(object sender, System.EventArgs e)
        //{
            
        //}

        #endregion
    }
}
