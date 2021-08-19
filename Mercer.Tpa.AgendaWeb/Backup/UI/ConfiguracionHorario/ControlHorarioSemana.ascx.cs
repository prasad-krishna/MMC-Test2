using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
   /// <summary>
   /// Control que muestra el horario semanal de un medico
   /// </summary>
    public partial class ControlHorarioSemana : System.Web.UI.UserControl
    {
        private List<ControlHorarioDia> controlesDias = new List<ControlHorarioDia>();
        private TipoControlHorario _tipoControl = TipoControlHorario.Calendario;
        private List<IntervaloHorarioSede> _intervalos = new List<IntervaloHorarioSede>();
        public List<IntervaloHorarioSede> Intervalos
        {
            get
            {
                return _intervalos;
            }
            set
            {
                _intervalos = value;
                ShowData();
            }
        }

       /// <summary>
       /// Esta fecha le indica al control que pinte correctamente las semanas
       /// </summary>
       public DateTime FechaReferencia
       {
        set
        {
            var inicioSemana = DateUtils.GetPrimerDiaDeSemana(value,DayOfWeek.Monday);
            controlesDias[0].Fecha = inicioSemana;
            controlesDias[1].Fecha = inicioSemana.AddDays(1);
            controlesDias[2].Fecha = inicioSemana.AddDays(2);
            controlesDias[3].Fecha = inicioSemana.AddDays(3);
            controlesDias[4].Fecha = inicioSemana.AddDays(4);
            controlesDias[5].Fecha = inicioSemana.AddDays(5);
            controlesDias[6].Fecha = inicioSemana.AddDays(6);
        }
       }


        /// <summary>
        /// Si es de tipo calendario, el usuari puede navegar usando el calendario
        /// Si es de tipo "General", no se muestran fechas especificas.
        /// La opcion "Calendario" se usara para configurar calendario de medicos
        /// La opcion "General" se usara para configurar la agenda por defecto
        /// </summary>
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Page_Init()
        {
            controlesDias.Clear();
            //Guardar en un array, facilitara operaciones con los intervalos para cada dia
            controlesDias.Add(HorarioLunes);
            controlesDias.Add(HorarioMartes);
            controlesDias.Add(HorarioMiercoles);
            controlesDias.Add(HorarioJueves);
            controlesDias.Add(HorarioViernes);
            controlesDias.Add(HorarioSabado);
            controlesDias.Add(HorarioDomingo);

        }



        /// <summary>
        /// Recarga los controles con la informacion de los intervalos.
        /// Debe ser llamado para que se actualize el control despues de haber
        /// asignado los intervalos
        /// </summary>
        public void ShowData()
        {
            /*Limpiar primero*/
            foreach (var controlDia in controlesDias)
            {
                controlDia.Intervalos.Clear();
            }
            foreach (var intervalo in _intervalos)
            {
               
                switch (intervalo.Fecha.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        controlesDias[0].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Tuesday:
                        controlesDias[1].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Wednesday:
                        controlesDias[2].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Thursday:
                        controlesDias[3].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Friday:
                        controlesDias[4].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Saturday:
                        controlesDias[5].Intervalos.Add(intervalo);
                        break;
                    case DayOfWeek.Sunday:
                        controlesDias[6].Intervalos.Add(intervalo);
                        break;
                }
            }
            foreach (var controlDia in controlesDias)
            {
                controlDia.ActualizarIntervalos();
            }
        }
}
    public enum TipoControlHorario { Calendario, General }
}