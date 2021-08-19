using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI
{
    public partial class CitasMedico : System.Web.UI.Page
    {
        private int selectedMedico = -1;
        private int selectedEstado = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            selectedMedico = SessionManager.IdUser;
            selectedEstado = (int)Estado.Pendiente;
        }

        protected void ObjectDataSourceCitasMedico_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idMedico"] = selectedMedico;
            e.InputParameters["estado"] = selectedEstado;
            e.InputParameters["recordatorio"] = -1;
            e.InputParameters["idSede"] = -1;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex!=-1 && e.Row.Cells.Count >1)
            {
                DateTime inicio = Convert.ToDateTime(e.Row.Cells[2].Text);
                DateTime fin = Convert.ToDateTime(e.Row.Cells[3].Text);
                e.Row.Cells[2].Text = inicio.GetDateTimeFormats()[7];
                e.Row.Cells[3].Text = inicio.GetDateTimeFormats()[89] + " - " + fin.GetDateTimeFormats()[89];

            }
        }
    }
}
