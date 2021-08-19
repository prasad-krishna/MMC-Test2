using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercer.Tpa.Agenda.Web.UI.BusquedaCitas
{
    public partial class DialogoRegistroLlegadaPaciente : System.Web.UI.UserControl
    {
        public event EventHandler RegistrarLlegadaPaciente;
        public int IdCita { get { return Convert.ToInt32(Request.Params["idCitaLlegada"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnOkLlegadaPaciente_Click(object sender, EventArgs e)
        {
            if(RegistrarLlegadaPaciente!=null)
            {
                RegistrarLlegadaPaciente(this, null);
            }
        }
    }
}