using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.BusquedaCitas;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.BusquedaCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor:Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Filtros de búsqueda de citas
    /// </summary>
    public partial class ControlFiltroBusquedas : UserControl
    {
        #region Variables privadas

        private  string _selectedIdentificacionEmpleado = string.Empty;
        private  string _selectedIdentificacionPaciente = string.Empty;
        private string _nombrePaciente = "";
        private int _selectedCita = -1;
        private int _selectedEstado = -1;
        private int _selectedMedico = -1;
        private int _selectedEspecialidad = -1;
        private int _selectedRecordatorio = -1;
        private int _selectedSede = -1;

        #endregion

        #region Propiedades

        public ParametrosBusquedaCita Parametros
        {
            get
            {
                AsignarFiltros();
                return GetParametrosBusqueda();
            }
        }

        /// <summary>
        /// Retorna los parámetros de búsqueda en un objeto contenedor.
        /// </summary>
        /// <returns></returns>
        private ParametrosBusquedaCita GetParametrosBusqueda()
        {
            var parametros = new ParametrosBusquedaCita();
            parametros.IdSede = _selectedSede;
            parametros.IdMedico = _selectedMedico;
            parametros.IdEspecialidad = _selectedEspecialidad;
            parametros.Recordatorio = _selectedRecordatorio;
            parametros.Estado = _selectedEstado;
            parametros.IdentificacionEmpleado = _selectedIdentificacionEmpleado;
            parametros.IdentificacionPaciente = _selectedIdentificacionPaciente;
            parametros.NombrePaciente = _nombrePaciente;
            parametros.IdUsuario = SessionManager.IdUser;
            parametros.IdEmpresa = SessionManager.IdEmpresa;
            //obtener las horas segun el rango
            parametros.HorarioEspecifico = chkHorarioEspecifico.Checked;
            if(parametros.HorarioEspecifico)
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
            switch (dbcRangoFechas.SelectedValue)
            {
                case "Hoy":
                    parametros.FechaInicio = DateTime.Now.Date;
                    parametros.FechaFin = parametros.FechaInicio.AddDays(1).AddSeconds(-1);
                    break;
                case "EstaSemana":
                    parametros.FechaInicio = DateUtils.GetPrimerDiaDeSemana(DateTime.Now, DayOfWeek.Monday);
                    parametros.FechaFin = parametros.FechaInicio.AddDays(7).AddSeconds(-1);
                    break;
                case "SiguienteSemana":
                    parametros.FechaInicio = DateUtils.GetPrimerDiaDeSemana(DateTime.Now.AddDays(8), DayOfWeek.Monday);
                    parametros.FechaFin = parametros.FechaInicio.AddDays(7).AddSeconds(-1);
                    break;
                case "EsteMes":
                    parametros.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    parametros.FechaFin = parametros.FechaInicio.AddMonths(1).AddSeconds(-1);
                    break;
                case "EsteAño":
                    parametros.FechaInicio = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    parametros.FechaFin = parametros.FechaInicio.AddYears(1).AddSeconds(-1);
                    break;
                case "Otro":
                    /*Leer los valores de los parametros de fecha inicio y fin*/
                    parametros.FechaInicio = ConfiguracionAgendaManager.ParseDate(txtRangoFechaInicio.Text).Date;
                    parametros.FechaFin = ConfiguracionAgendaManager.ParseDate(txtRangoFechaFin.Text).Date.AddDays(1).AddSeconds(-1);
                    break;
            }
            return parametros;

        }


        public event EventHandler RealizarBusqueda;

        #endregion

        #region Eventos página

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarSedes();
                    CargarEspecialidades();
                    CargarEstadosCita();
                    CargarEmpresas();

                    if(SessionManager.IdPrestador>0)
                    {
                        SeleccionarMedicoActual();
                    }
                }
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        /// <summary>
        /// Seleccionar la especialidad del médico actual
        /// y al médico actual
        /// </summary>
        private void SeleccionarMedicoActual()
        {
            var prestadoresRep = new PrestadoresDataRepository();
            var prestador = prestadoresRep.GetPrestadorById(SessionManager.IdPrestador);
            if(prestador == null)
            {
                throw new ApplicationException("No se encontró el prestador con id:"+SessionManager.IdPrestador);
            }

            if(prestador.Especialidad != null)
            {
                dbcEspecialidad.SelectedValue = prestador.Especialidad.Id.ToString();
            }
            dbcMedicos.Items.Clear();
            //Agregar solo el médico seleccionado
            dbcMedicos.Items.Insert(0,new ListItem(prestador.Name,prestador.Id.ToString()));

            dbcEspecialidad.Enabled = false;
            dbcMedicos.Enabled = false;
        }

        protected void dbcEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarMedicosParaEspecialidadSeleccionada();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }


        protected void dbcEspecialidad_DataBound(object sender, EventArgs e)
        {
            dbcEspecialidad.Items.Insert(0, "Todas Las Especialidades");
        }

        protected void dbcMedicos_DataBound(object sender, EventArgs e)
        {
            dbcMedicos.Items.Insert(0, "Todos Los Medicos");
        }

        protected void dbcSedes_DataBound(object sender, EventArgs e)
        {
            dbcSedes.Items.Insert(0, "Todas Las Sedes");
        }


        protected void btnBuscarCitas_Click(object sender, EventArgs e)
        {
            try
            {
                AsignarFiltros();
                //Lanzar evento
                if (RealizarBusqueda != null)
                    RealizarBusqueda(this, null);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Carga el combo con los estados de la cita
        /// </summary>
        private void CargarEstadosCita()
        {
            string[] estadoEnumElementsNames = Enum.GetNames(typeof (EnumEstadoCita));
            for (int i = 0; i < estadoEnumElementsNames.Length; i++)
            {
                int indexValue = i + 1;
                var li = new ListItem(estadoEnumElementsNames.GetValue(i).ToString(),
                                      Convert.ToString(indexValue));
                dbcEstadoCita.Items.Add(li);
            }
        }

        /// <summary>
        /// Carga el combo de especialidades
        /// </summary>
        private void CargarEspecialidades()
        {
            var edr = new EspecialidadDataRepository();
            var listaEspecialidades = edr.GetEspecialidades() as List<Especialidad>;
            dbcEspecialidad.DataMember = "Especialidad";
            dbcEspecialidad.DataTextField = "Nombre";
            dbcEspecialidad.DataValueField = "Id";
            dbcEspecialidad.DataSource = listaEspecialidades;
            dbcEspecialidad.DataBind();
        }

        /// <summary>
        /// Obtiene la lista de medicos de la especialidad seleccionada
        /// </summary>
        private void CargarMedicosParaEspecialidadSeleccionada()
        {
            var listaPrestadores = new List<InfoPrestador>();
            if (dbcEspecialidad.SelectedIndex != 0)
            {
                var pdr = new PrestadoresDataRepository();
                int selectedVal = Convert.ToInt32(dbcEspecialidad.SelectedValue);
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
            List<InfoSede> listaSedes = sdr.GetSedesByUsuarioEmpresa(SessionManager.IdUser, SessionManager.IdEmpresa);
            dbcSedes.DataMember = "InfoSede";
            dbcSedes.DataTextField = "Nombre";
            dbcSedes.DataValueField = "Id";
            dbcSedes.DataSource = listaSedes;
            dbcSedes.DataBind();
        }

        /// <summary>
        /// Actualiza las variables donde se guardan los valores seleccionados
        /// en los filtros de búsqueda.
        /// </summary>
        public void AsignarFiltros()
        {
            if (dbcMedicos.SelectedIndex > 0)
            {
                _selectedMedico = Convert.ToInt32(dbcMedicos.SelectedValue);
            }
            if(dbcEspecialidad.SelectedIndex > 0)
            {
                _selectedEspecialidad = Convert.ToInt32(dbcEspecialidad.SelectedValue);
            }
            if (dbcSedes.SelectedIndex != 0 && dbcSedes.SelectedIndex != -1)
            {
                _selectedSede = Convert.ToInt32(dbcSedes.SelectedValue);
            }
            if (dbcEstadoCita.SelectedIndex != 0)
            {
                _selectedEstado = Convert.ToInt32(dbcEstadoCita.SelectedValue);
            }
            if (dbcRecordatorio.SelectedValue != "-1")
            {
                _selectedRecordatorio = Convert.ToInt32(dbcRecordatorio.SelectedValue);
            }
            if (txtEmpleadoId.Text != "")
            {
                _selectedIdentificacionEmpleado = txtEmpleadoId.Text;
            }
            if (txtPacienteId.Text != "")
            {
                _selectedIdentificacionPaciente = txtPacienteId.Text;
            }
            if (txtPacienteNombre.Text != "")
            {
                _nombrePaciente = txtPacienteNombre.Text;
            }
        }

        /// <summary>
        /// RAM carga empresas
        /// Carga el combo de empresas
        /// </summary>
        private void CargarEmpresas()
        {
            UsuariosDataRepository user = new UsuariosDataRepository();
            int id = 52;
            DropEmpresas.DataTextField = "nombre";
            DropEmpresas.DataValueField = "empresa_id";
            DropEmpresas.DataSource = user.GetEmpresasUser(id);
            DropEmpresas.DataBind();
        }

        protected void DropEmpresas_DataBound(object sender, EventArgs e)
        {
            DropEmpresas.Items.Insert(0, "Todas Las Empresas");
        }
        #endregion

        protected void txtEmpleadoId_KeyPress(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtEmpleadoId.Text, "[0-1]"))
            {
                var a = "";
            }
            }
    }
}