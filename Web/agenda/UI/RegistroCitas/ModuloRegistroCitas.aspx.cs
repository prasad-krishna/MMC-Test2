/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2010 by Mercer
'===============================================================================
*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Logic.RegistroCitas;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.RegistroCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Busqueda de disponibilidad y registro de citas
    /// </summary>
    public partial class ModuloRegistroCitas : PaginaBaseAgenda
    {
        #region variables privadas
        private ParametrosBusquedaDisponibilidad _parametros = null;
        //True si esta en modo creación, false si esta en modo reprogramación
        private bool _creandoCita;
        private int _idEmpleado;
        private int _idBeneficiario;
        private InfoPaciente _paciente;
        private Cita _cita;
        private ServicioCitas _servicioCitas=new ServicioCitas();
        

        #endregion

        #region Eventos página
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender,e);
                LeerParametros();
                if (!IsPostBack)
                {
                    ctrInfoPaciente.Paciente = _paciente;
                    if (_creandoCita == false)
                    {
                        //Reprogramando Cita, cargar medios de comunicacion
                        CargarMediosComunicacionReprogramacion();
                        ctrFiltros.CargarFiltrosParaCita(_cita);
                        if(PuedeReprogramarCita() == false)
                        {
                            //No se puede reprogramar cita!
                            ctrFiltros.Visible = false;
                            grdResultados.Visible = false;
                            btnReprogramar.Visible = false;
                            btnBuscarDisponibilidad.Visible = false;
                            lblNoPuedeReprogramar.Visible = true;
                            
                        }


                    }
                }

                ActualizarUltimosTelefonosPaciente();


            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }

        }

        /// <summary>
        /// Actualiza los ultimos telefonos de contacto dados por el paciente (mira la ultima cita registrada)
        /// </summary>
        private void ActualizarUltimosTelefonosPaciente()
        {
            txtUltimoTelefonoPaciente.Text = Server.HtmlEncode(_servicioCitas.GetUltimoTelefonoPaciente(_idEmpleado,_idBeneficiario));
        }

        protected void BtnBuscarDisponibilidades_Click(object sender, EventArgs e)
        {
            try
            {
                grdResultados.Visible = true;
                grdResultados.DataBind();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al realizar la búsqueda");
            }

        }

        protected void objDataSourceIntervalos_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            /* No cargar datos al inicio*/
            if (!IsPostBack)
            {
                e.Cancel = true;
                return;
            }
            e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            e.InputParameters["parametros"] = ctrFiltros.ParametrosBusqueda;
        }

        protected void grdResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                /*Configurar  link de reprogramacion*/
                if (e.Row.RowIndex != -1 && _creandoCita == false)
                {
                    var linkReprogramar = (LinkButton)e.Row.FindControl("LinkReprogramar");
                    var intervalo = (IntervaloDisponibilidad)e.Row.DataItem;
                    linkReprogramar.Text = "Reprogramar";
                    linkReprogramar.PostBackUrl =
                        string.Format(
                            "ModuloRegistroCitas.aspx?idcita={0}&" +
                            "fechaReprog={1}" +
                            "&horaInicialReprog={2}" +
                            "&horaFinalReprog={3}" + "&idSedeReprog={4}&idMedicoReprog={5}",
                            _cita.Id, DateUtils.FormatSoloFecha(intervalo.Fecha), intervalo.HoraInicio, intervalo.HoraFin, intervalo.Sede.Id, intervalo.Prestador.Id);

                    linkReprogramar.Attributes.Add("fechaReprog", DateUtils.FormatSoloFecha(intervalo.Fecha));
                    linkReprogramar.Attributes.Add("horaInicialReprog", intervalo.HoraInicio.ToString());
                    linkReprogramar.Attributes.Add("horaInicialFormatoReprog", DateUtils.FormatSoloTiempo(intervalo.FechaInicio));
                    linkReprogramar.Attributes.Add("horaFinalReprog", intervalo.HoraFin.ToString());
                    linkReprogramar.Attributes.Add("horaFinalFormatoReprog", DateUtils.FormatSoloTiempo(intervalo.FechaFin));

                    linkReprogramar.Attributes.Add("fechaOriginalReprog", DateUtils.FormatFecha(_cita.StartDate));
                    linkReprogramar.Attributes.Add("idSedeReprog", intervalo.Sede.Id.ToString());
                    linkReprogramar.Attributes.Add("nombreSedeReprog", Server.HtmlEncode(intervalo.Sede.Nombre));
                    linkReprogramar.Attributes.Add("idMedicoReprog", _cita.Prestador.Id.ToString());
                    linkReprogramar.Attributes.Add("nombreMedicoReprog", Server.HtmlEncode(_cita.Prestador.Name));
                    linkReprogramar.Attributes.Add("telefonosContactoReprog", Server.HtmlEncode(_cita.TelefonosContacto ?? string.Empty));
                    linkReprogramar.Visible = true;
                    linkReprogramar.Attributes.Add("esFestivo",intervalo.EsDiaFestivo?"1":"0");
                    if (intervalo.EsDiaFestivo)
                        linkReprogramar.CssClass += " intervaloFestivo";
                 
                    var panelLinkRegistr = e.Row.FindControl("PanelLinkRegistroCita");
                    panelLinkRegistr.Visible = false;
                }

                else if (e.Row.RowIndex != -1 && _creandoCita == true)
                {
                    var panelLinkRegistr = e.Row.FindControl("PanelLinkRegistroCita");
                    panelLinkRegistr.Visible = true;
                    var linkReprogramar = (LinkButton)e.Row.FindControl("LinkReprogramar");
                    linkReprogramar.Visible = false;
                }
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex, "Se presentó un error al intentar");
            }

        }

        protected void BtnOkRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                var cita = RegistrarCita();
                grdResultados.Visible = false;
                MostrarConfirmacionCitaRegistrada(cita);

            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al intentar registrar la cita.");
            }

        }

        protected void BtnOkReprogramacion_Click(object sender, EventArgs e)
        {
            try
            {
                if(PuedeReprogramarCita())
                {
                    ReprogramarCita();
                    grdResultados.Visible = false;
                    ActualizarControlCita();
                    MostrarConfirmacionCitaReprogramada();
                }
                else
                {
                    ctrMensaje.MostrarMensaje("No se puede cambiar la fecha de la cita próxima a iniciarse",EnumUserMessage.Advertencia);
                }

            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al intentar reprogramar la cita");
            }
        }

        /// <summary>
        /// Determina si la cita esta muy próxima para poder ser cambiada
        /// </summary>
        /// <returns></returns>
        private bool PuedeReprogramarCita()
        {
            return _servicioCitas.PuedeModificarCita(SessionManager.IdEmpresa, _cita.Id);
        }

        protected void DropDownMediosComunicacionReprog_DataBound(object sender, EventArgs e)
        {
            DropDownMediosComunicacionReprog.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Carga los medios de comunicacion para mostrar en el dialogo
        /// </summary>
        private void CargarMediosComunicacionReprogramacion()
        {
            var mediosRep = new MediosComunicacionDataRepository();
            DropDownMediosComunicacionReprog.DataSource = mediosRep.GetAll(SessionManager.IdEmpresa);
            DropDownMediosComunicacionReprog.DataBind();
        }



        /// <summary>
        /// Obtiene el id de la cita a reprogramar (si existe)
        /// o el id de empleado y beneficiario si se desea registrar nueva cita
        /// </summary>
        private void LeerParametros()
        {
            var citaRep = new CitasDataRepository();
            var pacienteRep = new PacientesDataRepository();
            if (Request.Params["idcita"] != null)
            {
                //TODO:Refactor. No obtener cita desde control.

                _creandoCita = false;
                /*Obtener la cita*/
                _cita = citaRep.GetCitaById(Convert.ToInt32(Request.Params["idcita"]));
                _idEmpleado = _cita.IdEmpleado;
                _idBeneficiario = _cita.IdBeneficiario;

                ctrInfoCitas.Visible = true;
                ActualizarControlCita();
                /*Actualizar último telefono de paciente*/

            }


            /*Se desea crear una cita para el paciente especificado*/
            else if (Request.Params["idempleado"] != null && Request.Params["idbeneficiario"] != null)
            {
                ctrInfoPaciente.Visible = true;
                _idEmpleado = Convert.ToInt32(Request.Params["idempleado"]);
                _idBeneficiario = Convert.ToInt32(Request.Params["idbeneficiario"]);
                _creandoCita = true;
            }
            else
            {
                throw new ApplicationException("se debe especificar una cita , o un idempleado y idbeneficiario");
            }

            _paciente = pacienteRep.GetPacienteByIds(_idEmpleado, _idBeneficiario);
            if (_paciente == null)
                throw new ApplicationException("No se encontró paciente idEmpleado:" + _idEmpleado + " idBeneficiario:" + _idBeneficiario);
        }

        private void ActualizarControlCita()
        {
            ctrInfoCitas.IdCita = _cita.Id;
            ctrInfoCitas.LoadData();
        }


        /// <summary>
        /// Mostrar mensaje de confirmacion cuando la cita se haya registrado exitosamente
        /// </summary>
        /// <param name="cita"></param>
        private void MostrarConfirmacionCitaRegistrada(Cita cita)
        {
            var msg = string.Format("Cita registrada para paciente {0} en la fecha {1:ddd MMM dd yyyy} a las {2:hh:mm tt}",
                              cita.NombrePaciente, cita.StartDate, cita.StartDate);
            ctrMensaje.MostrarMensaje(msg, EnumUserMessage.Notificacion);
        }

        /// <summary>
        /// Registra una cita a partir de los parámetros ingresados
        /// </summary>
        /// <returns></returns>
        private Cita RegistrarCita()
        {

            var prestadorRep = new PrestadoresDataRepository();
            var sedesRep = new SedesDataRepository();
            var tipoCitaRep = new TiposCitaDataRepository();
            var pacientesRep = new PacientesDataRepository();
            var citasRep = new CitasDataRepository();
            var cita = new Cita();

            var parametrosRegistro = LeerParametrosRegistro();
            var prestador = prestadorRep.GetPrestadorById(parametrosRegistro.IdMedico);
            var sede = sedesRep.GetSedeById(parametrosRegistro.IdSede);
            var tipoCita = tipoCitaRep.GetTiposCitaById(parametrosRegistro.IdTipoCita);

            if (prestador == null)
                throw new ApplicationException("No se encontró el médico con Id:" + parametrosRegistro.IdMedico);
            if (sede == null)
                throw new ApplicationException("No se encontró la sede con Id:" + parametrosRegistro.IdSede);
            if (tipoCita == null)
                throw new ApplicationException("No se encontró el tipo de cita" + parametrosRegistro.IdTipoCita);


            cita.StartDate = parametrosRegistro.FechaInicio;
            cita.EndDate = parametrosRegistro.FechaFin;
            cita.Prestador = prestador;
            cita.Sede = sede;
            cita.Tipo = tipoCita;
            cita.NotasAdicionales = parametrosRegistro.NotasAdicionales;
            cita.EstadoCita = EnumEstadoCita.Pendiente;
            cita.Recordatorio = false;
            cita.NombrePaciente = _paciente.Nombre;

            //Obtener la identificación del empleado desde SICAU
            if (_idEmpleado > 0)
            {
                var empleado = pacientesRep.GetPacienteByIds(_idEmpleado, 0);
                if (empleado == null)
                    throw new ArgumentException("No se encontró empleado en SICAU con id:" + _idEmpleado);
                cita.EmpleadoIdentificacion = empleado.Identificacion;
            }
            else
            {
                cita.EmpleadoIdentificacion = string.Empty;
            }

            cita.PacienteIdentificacion = _paciente.Identificacion;
            cita.IdEmpleado = _idEmpleado;
            cita.IdBeneficiario = _idBeneficiario;
            cita.TelefonosContacto = parametrosRegistro.TelefonosContacto;
            /*Asignar fecha UTC de inicio que sirve como referencia en el tiempo*/
            cita.FechaInicioUtc = _servicioCitas.ConvertirFechaLocalAUtc(SessionManager.IdEmpresa, cita.StartDate);
            citasRep.RegistrarCita(cita, SessionManager.IdUser);
            return cita;
        }

        /// <summary>
        /// Lee los parametros del request que serán utilizados para registrar en la cita
        /// </summary>
        /// <returns></returns>
        private ParametrosRegistroCita LeerParametrosRegistro()
        {
            var parametros = new ParametrosRegistroCita();

            /*Validar que se tienen los parámetros*/
            WebUtils.RequerirParametros("horaInicial", "horaFinal", "fecha", "idSede", "idMedico", "idEmpleado", "idBeneficiario", "notasRegistro", "telefonosRegistro", "idTipoCita");
            /*Obtener la hora inicial y final para determinar la fecha de inicio y fin de la cita*/
            string horaInicial = Request.Params["horaInicial"];
            string horaFinal = Request.Params["horaFinal"];
            var partesHoraInicio = horaInicial.Split(':');
            var partesHoraFinal = horaFinal.Split(':');

            TimeSpan tiempoInicio = new TimeSpan(Convert.ToInt32(partesHoraInicio[0]), Convert.ToInt32(partesHoraInicio[1]), 0);
            TimeSpan tiempoFin = new TimeSpan(Convert.ToInt32(partesHoraFinal[0]), Convert.ToInt32(partesHoraFinal[1]), 0);
            DateTime diaCita = ConfiguracionAgendaManager.ParseDate(Request.Params["fecha"]).Date;

            parametros.FechaInicio = diaCita.AddHours(tiempoInicio.Hours).AddMinutes(tiempoInicio.Minutes);
            parametros.FechaFin = diaCita.AddHours(tiempoFin.Hours).AddMinutes(tiempoFin.Minutes);
            parametros.NotasAdicionales = Request.Params["notasRegistro"] ?? string.Empty;
            parametros.TelefonosContacto = Request.Params["telefonosRegistro"] ?? string.Empty;

            string idsede = (Request.Params["idSede"]);
            parametros.IdSede = Convert.ToInt32(idsede.Replace(",", ""));
            string idmedico = (Request.Params["idMedico"]);
            parametros.IdMedico = Convert.ToInt32(idmedico.Replace(",", ""));

            string strTipoCita = Request.Params["idTipoCita"];
            parametros.IdTipoCita = Convert.ToInt32(strTipoCita.Replace(",", ""));
            return parametros;
        }



        /// <summary>
        /// Muestra mensaje de confirmación cuando se reprograma una cita
        /// </summary>
        private void MostrarConfirmacionCitaReprogramada()
        {
            var msg = "La cita fue reprogramada con éxito";
            ctrMensaje.MostrarMensaje(msg, EnumUserMessage.Notificacion);
        }

        /// <summary>
        /// Método que reprograma la cita en el horario seleccionado
        /// </summary>
        private void ReprogramarCita()
        {
            var citasRep = new CitasDataRepository();
            var mediosRep = new MediosComunicacionDataRepository();
            var sedesRep = new SedesDataRepository();

            var parametros = LeerParametrosReprogramacion();
            var medio = mediosRep.GetById(parametros.IdMedio);
            var sede = sedesRep.GetSedeById(parametros.IdSede);
            if (sede == null)
                throw new ArgumentException("No se encontró sede con Id" + parametros.IdSede);
            if (medio == null)
                throw new ArgumentException("No se encontró medio de comunicación con Id:" + parametros.IdMedio);
            //Obtener la fecha de inicio UTC actualizada
            var fechaInicioUtc = _servicioCitas.ConvertirFechaLocalAUtc(SessionManager.IdEmpresa, parametros.FechaInicio);

            citasRep.ReprogramarCita(_cita.Id, parametros.IdSede, parametros.FechaInicio, parametros.FechaFin,fechaInicioUtc, SessionManager.IdUser,
                parametros.NombreSolicita, medio.Id, parametros.NotasAdicionales, parametros.Origen);


        }

        /// <summary>
        /// Lee los parametros del request usados para reprogramar la cita
        /// </summary>
        /// <returns></returns>
        private ParametrosReprogramacionCita LeerParametrosReprogramacion()
        {
            var parametros = new ParametrosReprogramacionCita();
            /*Validar que se tienen los parámetros*/
            WebUtils.RequerirParametros("horaInicialReprog",
                "horaFinalReprog", "fechaReprog", "idSedeReprog",
                "nombreSolicitaReprog", "notasRegistroReprog", "telefonosRegistroReprog", "origenReprogramacion");
            /*Obtener la hora inicial y final para determinar la fecha de inicio y fin de la cita*/
            string horaInicial = Request.Params["horaInicialReprog"];
            string horaFinal = Request.Params["horaFinalReprog"];
            var partesHoraInicio = horaInicial.Split(':');
            var partesHoraFinal = horaFinal.Split(':');

            TimeSpan tiempoInicio = new TimeSpan(Convert.ToInt32(partesHoraInicio[0]), Convert.ToInt32(partesHoraInicio[1]), 0);
            TimeSpan tiempoFin = new TimeSpan(Convert.ToInt32(partesHoraFinal[0]), Convert.ToInt32(partesHoraFinal[1]), 0);
            DateTime diaCita = ConfiguracionAgendaManager.ParseDate(Request.Params["fechaReprog"]).Date;

            parametros.FechaInicio = diaCita.AddHours(tiempoInicio.Hours).AddMinutes(tiempoInicio.Minutes);
            parametros.FechaFin = diaCita.AddHours(tiempoFin.Hours).AddMinutes(tiempoFin.Minutes);
            parametros.NotasAdicionales = Request.Params["notasRegistroReprog"] ?? string.Empty;
            parametros.TelefonosContacto = Request.Params["telefonosRegistroReprog"] ?? string.Empty;
            parametros.NombreSolicita = Request.Params["nombreSolicitaReprog"];
            parametros.IdSede = Convert.ToInt32(Request.Params["idSedeReprog"]);
            parametros.Origen = (EnumOrigenModificacionCita)Convert.ToInt32(Request.Params["origenReprogramacion"]);
            if (string.IsNullOrEmpty(DropDownMediosComunicacionReprog.SelectedValue))
            {
                throw new ArgumentException("No se seleccionó medio de comunicación");
            }
            parametros.IdMedio = Convert.ToInt32(DropDownMediosComunicacionReprog.SelectedValue);
            return parametros;
        }

        #endregion


    }
}
