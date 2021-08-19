using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPA;

namespace Web.interfaz_empleado.Forma
{
    public partial class AE_CertificadoApitud : PB_PaginaBase
    {
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            //base.OnInit(e);
        }
        protected override void Page_Load(object sender, EventArgs e)
        {

            try
            {
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }


        }

        private void InitializeComponent()
        {
 
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        protected void btnFinalizar_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("LIS_consulta.aspx");

        }
       

        protected void btnAnterior_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AE_registroconsulta.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1");
        }

        protected void lnkDescarga_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Certificado_Aptitud.docx");
            Response.TransmitFile(Server.MapPath("~/interfaz_empleado/Recursos/Certificado/Certificado_aptitud.docx"));
            Response.End();
        }
    }
}