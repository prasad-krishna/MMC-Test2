﻿using System;
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

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Pagina intermedia que carga los datos necesarios para el ingreso de un usuario desde el sistema de SICAU
    /// </summary>
    public partial class IntermedioSICAU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Session["IdUser"] = Convert.ToInt32(Request.QueryString["usuario_id"]);
            Session["IPUser"] = Request.UserHostAddress;
            Session["Company"] = Convert.ToInt32(Request.QueryString["empresa_id"]);
            Session["NameUser"] = Request.QueryString["nombre_usuario"];
            Session["SICAU"] = true;

            PB_PaginaBase objPagina = new PB_PaginaBase();

            objPagina.RegisterLog(Log.EnumActionsLog.IngresarDesdeSICAU, Convert.ToInt64(Request.QueryString["usuario_id"]), "Id Usuario SICAU:" + Request.QueryString["usuario_id"] + " Usuario:" + Request.QueryString["nombre_usuario"]);

            if (Request.QueryString["tipo_solicitud"] == "AUT")
            {
                Session["URLRedirect"] = "AE_solicitudautorizacion.aspx?employee_id=" + Request.QueryString["id_empleado"] + "&SICAU_solicitud_id=" + Request.QueryString["solicitud_id"];
                Response.Redirect("Home.aspx");
            }
            if (Request.QueryString["tipo_solicitud"] == "REE")
            {
                Session["URLRedirect"] = "AE_solicitudreembolso.aspx?employee_id=" + Request.QueryString["id_empleado"] + "&SICAU_solicitud_id=" + Request.QueryString["solicitud_id"];
                Response.Redirect("Home.aspx");
            }
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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}
