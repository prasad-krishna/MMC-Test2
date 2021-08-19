using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Logic.RegistroCitas;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.RegistroCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Filtros para busqueda de disponibilidad
    /// </summary>
    public partial class ControlFiltroDisponibilidad : UserControl
    {
        #region Propiedades

        public ParametrosBusquedaDisponibilidad ParametrosBusqueda
        {
            get { return GetParametrosBusqueda(); }
        }

        #endregion

        #region Eventos página


        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarSedes();
                    CargarEspecialidades();
                    CargarTiposDeCita();
                }
            }
            catch (Exception exception)
            {
                ctrError.MostrarError(exception, "Ocurrió un error.");
            }

        }


        protected void dbcEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_eventosActivos)
                {
                    CargarMedicosParaEspecialidadSeleccionada();
                }

            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Ocurrió un error al cargar la lista de médicos para la especialidad");
            }
        }



        protected void dbcSedes_DataBound(object sender, EventArgs e)
        {
            dbcSedes.Items.Insert(0, "Todas Las Sedes");
        }

        protected void dbcEspecialidades_DataBound(object sender, EventArgs e)
        {
            dbcEspecialidades.Items.Insert(0, "Todas Las Especialidades");
        }

        protected void dbcMedicos_DataBound(object sender, EventArgs e)
        {
            dbcMedicos.Items.Insert(0, "Todos Los Medicos");
        }

        protected void dbcTipoCita_DataBound(object sender, EventArgs e)
        {
            dbcTipoCita.Items.Insert(0, new ListItem("-- Seleccione -- ", "-1"));
        }


        protected void dbcTipoCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_eventosActivos)
                {
                    CargarDuracionParaTipoDeCita();
                }

            }
            catch (Exception exception)
            {
                ctrError.MostrarError(exception, "Ocurrió un error al cargar la duración de la cita");
            }

        }



        #endregion

        #region Metodos privados

        /// <summary>
        /// Lee los parámetros seleccionados por el usuario para la búsqueda de disponibilidad
        /// de médicos para registrar o reprogramar cita.
        /// </summary>
        /// <returns></returns>
        private ParametrosBusquedaDisponibilidad GetParametrosBusqueda()
        {
            var parametros = new ParametrosBusquedaDisponibilidad();
            parametros.IdEspecialidad = dbcEspecialidades.SelectedIndex <= 0
                                            ? -1
                                            : Convert.ToInt32(dbcEspecialidades.SelectedValue);
            parametros.IdSedes = dbcSedes.SelectedIndex <= 0 ? -1 : Convert.ToInt32(dbcSedes.SelectedValue);
            parametros.IdMedico = dbcMedicos.SelectedIndex <= 0 ? -1 : Convert.ToInt32(dbcMedicos.SelectedValue);
            parametros.IdTipoCita = dbcTipoCita.SelectedIndex <= 0 ? -1 : Convert.ToInt32(dbcTipoCita.SelectedValue);
            parametros.Duracion = txtDuracion.Text == "" ? -1 : Convert.ToInt32(txtDuracion.Text);
            parametros.HorarioEspecifico = chkHorarioEspecifico.Checked;
            if (parametros.HorarioEspecifico)
            {
                DateTime hInicio;
                DateTime hFin;
                if (!DateTime.TryParse(txtHoraInicio.Text, out hInicio))
                {
                    throw new ArgumentException("Formato de hora incorrecto (Hora inicio)");
                }

                if (!DateTime.TryParse(txtHoraFin.Text, out hFin))
                {
                    throw new ArgumentException("Formato de hora incorrecto (Hora inicio)");
                }
                parametros.HoraInicio = hInicio.TimeOfDay;
                parametros.HoraFin = hFin.TimeOfDay;
            }
            //obtener las fechas segun el rango
            var horaLocalActual = GetHoraLocal();
            switch (dbcRangoFechas.SelectedValue)
            {
                case "Hoy":
                    /*Obtener la fecha local*/
                    parametros.FechaInicio = GetHoraLocal();
                    parametros.FechaFin = parametros.FechaInicio.AddDays(1).Date.AddSeconds(-1);
                    break;
                case "EstaSemana":
                    var inicioSemana = DateUtils.GetPrimerDiaDeSemana(horaLocalActual, DayOfWeek.Monday);
                    parametros.FechaInicio = horaLocalActual;
                    parametros.FechaFin = inicioSemana.AddDays(7).AddSeconds(-1);
                    break;
                case "SiguienteSemana":

                    parametros.FechaInicio = DateUtils.GetPrimerDiaDeSemana(horaLocalActual.AddDays(8), DayOfWeek.Monday);
                    parametros.FechaFin = parametros.FechaInicio.AddDays(7).AddSeconds(-1);
                    break;
                case "EsteMes":
                    var inicioMes = new DateTime(horaLocalActual.Year, horaLocalActual.Month, 1);
                    parametros.FechaInicio = horaLocalActual;
                    parametros.FechaFin = inicioMes.AddMonths(1).AddSeconds(-1);
                    break;
                case "Otro":
                    /*Leer los valores de los parametros de fecha inicio y fin*/
                    parametros.FechaInicio = ConfiguracionAgendaManager.ParseDate(txtRangoFechaInicio.Text).Date;
                    parametros.FechaFin = ConfiguracionAgendaManager.ParseDate(txtRangoFechaFin.Text).Date.AddDays(1).AddSeconds(-1);
                    break;
            }
            return parametros;
        }

        /// <summary>
        /// Retorna la hora local para la zona horaria
        /// </summary>
        /// <returns></returns>
        private DateTime GetHoraLocal()
        {
            var servicioZona = new ServicioZonaHoraria();
            return servicioZona.GetHoraLocal();
        }


        /// <summary>
        /// Pre selecciona los valores de la cita
        /// </summary>
        /// <param name="cita"></param>
        public void CargarFiltrosParaCita(Cita cita)
        {
            CargarFiltrosCitaExistente(cita);
        }

        private bool _eventosActivos = true;

        /// <summary>
        /// Preselecciona los valores de los filtros cuando se va a reprogramar una cita
        /// </summary>
        private void CargarFiltrosCitaExistente(Cita cita)
        {
            string strIdEspecialidad = cita.Prestador.Especialidad.Id.ToString();
            string strIdSede = cita.Sede.Id.ToString();
            var strIdTipoCita = cita.Tipo.Id.ToString();
            var pdr = new PrestadoresDataRepository();

            _eventosActivos = false;
            //selecciona la sede de la cita
            dbcSedes.SelectedIndex = -1;
            foreach (ListItem itemSede in dbcSedes.Items)
            {
                if (itemSede.Value == strIdSede)
                {
                    itemSede.Selected = true;
                    break;
                }
            }
            //selecciona la especialidad
            dbcEspecialidades.SelectedIndex = -1;
            foreach (ListItem itemEspecialidad in dbcEspecialidades.Items)
            {
                if (itemEspecialidad.Value == strIdEspecialidad)
                {
                    itemEspecialidad.Selected = true;
                    break;
                }
            }

            //Selecciona el tipo de cita
            dbcTipoCita.SelectedIndex = -1;
            foreach (ListItem itemTipoCita in dbcTipoCita.Items)
            {
                if (itemTipoCita.Value == strIdTipoCita)
                {
                    itemTipoCita.Selected = true;
                    break;

                }
            }

            txtDuracion.Text = cita.Duracion.ToString();

            //seleccionar el medico , pero primero carguelos de acuerdo a la especialidad

            var listaPrestadores =
                pdr.GetPrestadoresPorEspecialidad(SessionManager.IdEmpresa, cita.Prestador.Especialidad.Id) as
                List<InfoPrestador>;

            dbcMedicos.DataMember = "InfoPrestador";
            dbcMedicos.DataTextField = "Name";
            dbcMedicos.DataValueField = "Id";
            dbcMedicos.DataSource = listaPrestadores;
            dbcMedicos.DataBind();

            //seleccionar el medico
            string strIdMedico = cita.Prestador.Id.ToString();
            foreach (ListItem itemMedico in dbcMedicos.Items)
            {
                if (itemMedico.Value == strIdMedico)
                {
                    itemMedico.Selected = true;
                    break;
                }
            }
            //Desactivar combos de medico y especialidad
            dbcEspecialidades.Enabled = false;
            dbcMedicos.Enabled = false;
            _eventosActivos = true;
        }

        /// <summary>
        /// Carga el combo de tipos de cita
        /// </summary>
        private void CargarTiposDeCita()
        {
            var tcdr = new TiposCitaDataRepository();
            var listaTiposCita = tcdr.GetTiposCitaActivosByEmpresa(SessionManager.IdEmpresa) as List<TipoCita>;
            dbcTipoCita.DataMember = "TipoCita";
            dbcTipoCita.DataTextField = "Name";
            dbcTipoCita.DataValueField = "Id";
            dbcTipoCita.DataSource = listaTiposCita;
            dbcTipoCita.DataBind();
        }

        /// <summary>
        /// Carga el combo de especialidades
        /// </summary>
        private void CargarEspecialidades()
        {
            var edr = new EspecialidadDataRepository();
            var listaEspecialidades = edr.GetEspecialidades() as List<Especialidad>;
            dbcEspecialidades.DataMember = "Especialidad";
            dbcEspecialidades.DataTextField = "Nombre";
            dbcEspecialidades.DataValueField = "Id";
            dbcEspecialidades.DataSource = listaEspecialidades;
            dbcEspecialidades.DataBind();
        }

        private void CargarMedicosParaEspecialidadSeleccionada()
        {
            List<InfoPrestador> listaPrestadores;
            if (dbcEspecialidades.SelectedIndex != 0)
            {
                var pdr = new PrestadoresDataRepository();
                int selectedVal = Convert.ToInt32(dbcEspecialidades.SelectedValue);
                listaPrestadores =
                    pdr.GetPrestadoresPorEspecialidad(SessionManager.IdEmpresa, selectedVal) as List<InfoPrestador>;
            }
            else //Todas las especialidades , por lo tanto cargar todos los medicos mezclados
            {
                var pdr = new PrestadoresDataRepository();
                listaPrestadores =
                    pdr.GetPrestadoresPorEspecialidad(SessionManager.IdEmpresa, -1) as List<InfoPrestador>;
            }
            dbcMedicos.DataMember = "InfoPrestador";
            dbcMedicos.DataTextField = "Name";
            dbcMedicos.DataValueField = "Id";
            dbcMedicos.DataSource = listaPrestadores;
            dbcMedicos.DataBind();
        }

        /// <summary>
        /// Carga el combo de sedes
        /// </summary>
        private void CargarSedes()
        {
            var sdr = new SedesDataRepository();
            var listaSedes = sdr.GetSedesByUsuarioEmpresa(SessionManager.IdUser, SessionManager.IdEmpresa) as List<InfoSede>;
            dbcSedes.DataMember = "InfoSede";
            dbcSedes.DataTextField = "Nombre";
            dbcSedes.DataValueField = "Id";
            dbcSedes.DataSource = listaSedes;
            dbcSedes.DataBind();
        }

        /// <summary>
        /// Carga la duracion para el tipo de cita y lo asigna al textbox de duración
        /// </summary>
        private void CargarDuracionParaTipoDeCita()
        {
            if (dbcTipoCita.SelectedIndex != 0)
            {
                var tcdr = new TiposCitaDataRepository();

                TipoCita tc = tcdr.GetTiposCitaById(Convert.ToInt32(dbcTipoCita.SelectedValue));
                if (tc == null)
                {
                    throw new ArgumentException("No se encontró el Tipo de cita con id:" + dbcTipoCita.SelectedValue);
                }
                txtDuracion.Text = tc.Duration.ToString();
            }
            else
            {
                txtDuracion.Text = "";
            }
        }

        #endregion


    }
}