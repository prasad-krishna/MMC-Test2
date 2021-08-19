using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.CitasMedico
{
    public partial class CitasMedicoGrid : System.Web.UI.UserControl
    {
        #region Atributos privados
        private int _selectedMedico = -1;
        #endregion

        #region Propiedades
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion

        #region Eventos Página

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _selectedMedico = SessionManager.IdPrestador;
        }

        protected void ObjectDataSourceGrid_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idMedico"] = _selectedMedico;
            e.InputParameters["startDate"] = StartDate.Date;
            //Asignar la fecha final como el ultimo momento (un segundo antes del dia siguiente) para que se incluyan todas las del día.
            e.InputParameters["endDate"] = EndDate.Date.AddDays(1).AddSeconds(-1);
            e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
        }

        protected void GridViewCitasMedico_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewCitasMedico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos públicos
        public void RefrescarDatos()
        {
            try
            {
                GridViewCitasMedico.DataBind();
            }
            catch (Exception ex)
            {
              ctrError.MostrarError(ex);  
        
            }
           
        }
        #endregion
    }
}