using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
    /// <summary>
    /// Permite seleccionar una semana a partir de una fecha de referencia.
    /// </summary>
    public partial class SelectorSemana : System.Web.UI.UserControl
    {
        #region propiedades

        public event EventHandler CambioFecha;
        public DateTime FechaReferencia
        {
            get
            {
                return CalendarioSemana.SelectedDate;
            }
            set
            {
                CalendarioSemana.SelectedDate = value;
                UpdateLabelSemana();
            }
        }

        /// <summary>
        /// Actualizar la semana
        /// </summary>
        private void UpdateLabelSemana()
        {
            //Obtener el inicio y fin de la semana actual.
            var fechaRef = CalendarioSemana.SelectedDate;
            var primerDiaSemana = DateUtils.GetPrimerDiaDeSemana(fechaRef, DayOfWeek.Monday);
            var ultimoDiaSemana = DateUtils.GetUltimoDiaSemana(fechaRef, DayOfWeek.Monday);
            LabelSemana.Text = string.Format("{0} - {1}", primerDiaSemana.ToLongDateString(),ultimoDiaSemana.ToLongDateString());

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CalendarioSemana_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLabelSemana();
            if(CambioFecha!=null)
            {
                CambioFecha(this, null);
            }
        }
    }
}