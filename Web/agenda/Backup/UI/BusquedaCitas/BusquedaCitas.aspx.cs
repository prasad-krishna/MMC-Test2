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
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.BusquedaCitas;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.BusquedaCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Módulo de búsqueda de citas.
    /// </summary>
    public partial class BusquedaCitas : PaginaBaseAgenda
    {
        #region Variables privadas
        private int _selectedIdPaciente = -1; //usadi solo para citas pacientes
        private static bool _redirected = false;
        private ServicioCitas _servicioCitas = new ServicioCitas();
      

        #endregion

        #region Eventos página

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                    CargarMediosComunicacionCancelacionMasiva();
                    CargarOrigenesCancelacionCancelacionMasiva();
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }

        }

        private void CargarMediosComunicacionCancelacionMasiva()
        {
            var mediosRep = new MediosComunicacionDataRepository();
            dbcMedioCancelacionMasiva.DataSource = mediosRep.GetActivas(SessionManager.IdEmpresa);
            dbcMedioCancelacionMasiva.DataBind();
        }

        private void CargarOrigenesCancelacionCancelacionMasiva()
        {
            dbcOrigenCancelacionMasiva.Items.Add(new ListItem("Paciente solicita", "1"));
            dbcOrigenCancelacionMasiva.Items.Add(new ListItem("Médico solicita", "2"));
            dbcOrigenCancelacionMasiva.Items.Add(new ListItem("Otro", "3"));

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            /*Registrar eventos de controles y dialogos*/
            ctrFiltros.RealizarBusqueda += new EventHandler(ControlFiltroBusquedas1_RealizarBusqueda);
            dlgCancelarCita.CancelarCita += new EventHandler(DialogoCancelarCita1_CancelarCita);
            dlgRegistrarRecordatorio.IntentoAvisoPacienteExitoso += new EventHandler(DialogoRegistrarRecordatorioPaciente1_IntentoAvisoPacienteExitoso);
            dlgRegistrarRecordatorio.IntentoAvisoPacienteNoExitoso += new EventHandler(DialogoRegistrarRecordatorioPaciente1_IntentoAvisoPacienteNoExitoso);
            dlgRegistrarLlegada.RegistrarLlegadaPaciente += new EventHandler(DialogoRegistroLlegadaPaciente1_RegistrarLlegadaPaciente);
        }

        #endregion

        #region Registrar llegada de paciente


        /// <summary>
        /// Handler del evento registrar llegada paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DialogoRegistroLlegadaPaciente1_RegistrarLlegadaPaciente(object sender, EventArgs e)
        {
            try
            {
                RegistrarLlegadaPaciente();
                ctrMensaje.MostrarMensaje("Se actualizó el estado de la cita a : En espera", EnumUserMessage.Notificacion);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }

        }

        /// <summary>
        /// Actualiza el estado de la cita a En espera
        /// </summary>
        protected void RegistrarLlegadaPaciente()
        {
            int idCita = dlgRegistrarLlegada.IdCita;
            var cdr = new CitasDataRepository();
            cdr.RegistrarLlegadaPaciente(idCita, SessionManager.IdUser);
            grdResultados.DataBind();
        }


        #endregion

        #region Registro de avisos recordandole a paciente de sus cita

        /// <summary>
        /// Handler de evento IntentoAvisoPacienteNoExitoso. Que se genera cuando
        /// el usuario intentó pero no pudo contactar al paciente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DialogoRegistrarRecordatorioPaciente1_IntentoAvisoPacienteNoExitoso(object sender, EventArgs e)
        {
            try
            {
                RegistrarIntentoNoExitosoAviso();
                ctrMensaje.MostrarMensaje("Registro guardado", EnumUserMessage.Notificacion);
            }
            catch (Exception ex)
            {
               ctrError.MostrarError(ex);
            }

        }


        /// <summary>
        /// Handler de evento IntentoAvisoPacienteNoExitoso. Que se genera cuando
        /// el usuario intentó y pudo contactar al paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DialogoRegistrarRecordatorioPaciente1_IntentoAvisoPacienteExitoso(object sender, EventArgs e)
        {
            try
            {
                RegistrarIntentoExitosoAviso();
                ctrMensaje.MostrarMensaje("Registro guardado",EnumUserMessage.Notificacion);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }

        }

        /// <summary>
        /// Guarda un registro de intento exitoso de aviso al paciente sobre su cita
        /// </summary>
        protected void RegistrarIntentoExitosoAviso()
        {
            int idCita = dlgRegistrarRecordatorio.IdCita; ;
            string notas = dlgRegistrarRecordatorio.Descripcion;
            int medio = dlgRegistrarRecordatorio.Medio;
            var cdr = new CitasDataRepository();
            cdr.RegistrarRecordatorio(idCita, SessionManager.IdUser, medio, notas, DateTime.Now, true);
            grdResultados.DataBind();
        }

        /// <summary>
        /// Guarda un intento de aviso no exitos.
        /// </summary>
        protected void RegistrarIntentoNoExitosoAviso()
        {
            int idCita = dlgRegistrarRecordatorio.IdCita; ;
            string notas = dlgRegistrarRecordatorio.Descripcion;
            int medio = dlgRegistrarRecordatorio.Medio;
            var cdr = new CitasDataRepository();
            cdr.RegistrarRecordatorio(idCita, SessionManager.IdUser, medio, notas, DateTime.Now, false);
            grdResultados.DataBind();
        }


        #endregion

        #region Cancelar Cita

        /// <summary>
        /// Handler del evento CancelarCita lanzado por el control dialogo de cancelación
        /// cuando el usuario hace click en el botón de cancelacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DialogoCancelarCita1_CancelarCita(object sender, EventArgs e)
        {
            try
            {
                if(PuedeCancelarCitaSeleccionada())
                {
                    CancelarCita();
                    ctrMensaje.MostrarMensaje("La cita fue cancelada", EnumUserMessage.Notificacion);
                    RecargarResultados();
                }
                else
                {
                    ctrMensaje.MostrarMensaje("No es posible cancelar una cita próxima a su fecha de inicio o vencida",EnumUserMessage.Advertencia);
                }

            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }

        }

        /// <summary>
        /// Determina si la cita puede ser cancelada de acuerdo al parámetro de tiempo limite de modificacion
        /// configurado para la empresa.
        /// </summary>
        /// <returns></returns>
        private bool PuedeCancelarCitaSeleccionada()
        {
            var idCita = dlgCancelarCita.IdCita;
            return _servicioCitas.PuedeModificarCita(SessionManager.IdEmpresa, idCita);
        }

        /// <summary>
        /// Marca una cita como cancelada y registra la notificación correspondiente
        /// </summary>
        private void CancelarCita()
        {
            var idCita = dlgCancelarCita.IdCita;
            var nombreSolicita = dlgCancelarCita.NombreSolicita;
            var notas = dlgCancelarCita.NotasAdicionales;
            var origen = dlgCancelarCita.Origen;
            var idMedio = dlgCancelarCita.Medio.Id;
            var cdr = new CitasDataRepository();
            cdr.CancelarCita(idCita, SessionManager.IdUser, nombreSolicita, idMedio, notas, origen);
        }

        protected void btnCancelarSeleccionadas_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarCitasSeleccionadas();
                RecargarResultados();
                ctrMensaje.MostrarMensaje("Se cancelaron las citas seleccionadas (Pendientes o En espera)", EnumUserMessage.Notificacion);
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }
        }


        /// <summary>
        /// Cancela las citas seleccionadas
        /// </summary>
        private void CancelarCitasSeleccionadas()
        {
            var nombreSolicita = txtNombreSolicitaCancelacionMasiva.Text;
            var medio = Convert.ToInt32(dbcMedioCancelacionMasiva.SelectedValue);
            var origen = Convert.ToInt32(dbcOrigenCancelacionMasiva.SelectedValue);
            if (medio == -1)
            {
                throw new ArgumentException("Medio de contacto requerido");
            }
            if (origen == -1)
            {
                throw new ArgumentException("Origen de cancelación requerido");
            }
            var infoAdicional = txtNotasCancelacionMasiva.Text;

            var citasPorEliminar = new List<int>();
            foreach (GridViewRow row in grdResultados.Rows)
            {
                var check = row.FindControl("chkSeleccionFila") as CheckBox;
                if (check == null)
                    throw new ApplicationException("No se encontró control chkSeleccionFila");
                if (check.Checked)
                {
                    var idCita = (int)grdResultados.DataKeys[row.RowIndex].Value;
                    citasPorEliminar.Add(idCita);
                }
            }
            _servicioCitas.CancelarCitas(citasPorEliminar, SessionManager.IdUser, nombreSolicita, origen, medio, infoAdicional);

        }

        #endregion

        #region Configuracion Grid resultados

        void ControlFiltroBusquedas1_RealizarBusqueda(object sender, EventArgs e)
        {
            try
            {
                RecargarResultados();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }

        }

        private void RecargarResultados()
        {
            grdResultados.Visible = true;
            grdResultados.DataBind();
        }

        protected void objDataSourceCitas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            
            try
            {
              
                ctrFiltros.AsignarFiltros();

                if (!IsPostBack)
                {
                    e.Cancel = true;
                    return;
                }
                e.InputParameters["parametros"] = ctrFiltros.Parametros;
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }
        #endregion



    }
}
