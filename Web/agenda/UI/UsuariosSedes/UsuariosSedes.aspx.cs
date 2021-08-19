using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesSedesUsuario;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.PrestadoresSedes
{
    public partial class PrestadoresSedes : System.Web.UI.Page
    {
        private string selectedUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            selectedUsuario = DropDownListMedicos.SelectedValue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<InfoSedeVista> sedes = new List<InfoSedeVista>();
            ListItemCollection items = CheckBoxListSedes.Items;
            foreach (ListItem i in items)
            {
                InfoSedeVista d = new InfoSedeVista();
                d.Sede = new InfoSede() { Id = Convert.ToInt32(i.Value) };
                d.Selected = false;
                if (i.Selected)
                {
                    d.Selected = true;
                }
                sedes.Add(d);
            }
            SedesDataRepository sedesRep = new SedesDataRepository();
            sedesRep.RegistrarSedesUsuario(Convert.ToInt32(selectedUsuario), sedes);

        }

        protected void ObjectDataSourceMedicos_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount == false)
            {
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            }
        }

        protected void ObjectDataSourceSedes_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount == false)
            {
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            }
        }

        protected void DropDownListMedicos_DataBound(object sender, EventArgs e)
        {
            selectedUsuario = DropDownListMedicos.SelectedValue;
        }

        protected void CheckBoxListSedes_DataBound(object sender, EventArgs e)
        {
            ListItemCollection items = CheckBoxListSedes.Items;
            SedesDataRepository sdr = new SedesDataRepository();
            List<InfoSede> sedesPrestador = sdr.GetSedesUsuario(Convert.ToInt32(selectedUsuario));
            foreach (var sede in sedesPrestador)
            {
                ListItem li = CheckBoxListSedes.Items.FindByValue(Convert.ToString(sede.Id));
                li.Selected = true;
            }
        }

        protected void ObjectDataSourceSedes_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            selectedUsuario = DropDownListMedicos.SelectedValue;
        }

        protected void DropDownListMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxListSedes.DataBind();
        }
    }
}
