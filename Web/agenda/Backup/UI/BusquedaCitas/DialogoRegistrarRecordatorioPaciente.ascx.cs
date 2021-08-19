using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.BusquedaCitas
{
    /// <summary>
    /// Registro de recordatorios previos a la cita
    /// </summary>
    public partial class DialogoRegistrarRecordatorioPaciente : System.Web.UI.UserControl
    {
        public event EventHandler IntentoAvisoPacienteExitoso;
        public event EventHandler IntentoAvisoPacienteNoExitoso;
        public int IdCita { get { return Convert.ToInt32(Request.Params["idCitaRecordatorio"]); } }
        public int Medio { get { return Convert.ToInt32(dbcMedio.SelectedValue); } }
        public string Descripcion { get { return Request.Params["descripcionRecordatorio"]; } }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarMedios();
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }

        }

        private void CargarMedios()
        {
            var mediosRep = new MediosComunicacionDataRepository();
            dbcMedio.DataSource = mediosRep.GetActivas(SessionManager.IdEmpresa);
            dbcMedio.DataBind();

       
        }

        protected void BtnContacto_Click(object sender, EventArgs e)
        {
            if(IntentoAvisoPacienteExitoso != null)
            {
                IntentoAvisoPacienteExitoso(this, null);
            }
        }

        protected void BtnNoContacto_Click(object sender, EventArgs e)
        {
                    
            if(IntentoAvisoPacienteNoExitoso != null)
            {
                IntentoAvisoPacienteNoExitoso(this, null);
            }
        }

        protected void dbcMedio_DataBound(object sender, EventArgs e)
        {
            dbcMedio.Items.Insert(0,new ListItem("-- Medio de contacto --","-1"));
        }
    }
}