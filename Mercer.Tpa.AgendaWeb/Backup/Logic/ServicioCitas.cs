using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Servicios asociados con las operaciones de la cita
    /// </summary>
    public class ServicioCitas
    {

        #region Variables privadas
        readonly string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        HorarioRepository _horarioRep = new HorarioRepository();
        AgendaDataRepository _agendaRep = new AgendaDataRepository();
        CitasDataRepository _citasRep = new CitasDataRepository();
        #endregion

        /// <summary>
        /// Determina si se pueden realizar modificaciones a la cita
        /// teniendo en cuenta el limite  de horas configurado para modificar una cita
        /// Ej. 5 horas antes de la cita esta no puede ser modificada
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idCita"></param>
        /// <returns></returns>
        public bool PuedeModificarCita(int idEmpresa, int idCita)
        {
            var zonaHoraria = _horarioRep.GetZonaHorariaDeEmpresa(idEmpresa);
            var horaLocal = zonaHoraria == null ? DateTime.Now : zonaHoraria.HoraActualLocal;
            var cita = _citasRep.GetCitaById(idCita);
            var parametrosAgenda = _agendaRep.GetConfiguracionAgenda(idEmpresa);
            if (cita == null)
            {
                throw new ApplicationException("No se encontró la cita con id:" + idCita);

            }
            //Si no se ha configurado tiempo limite, permitir la modificación
            if(parametrosAgenda == null || parametrosAgenda.NumHorasLimiteModificacionCitas<=0)
            {
                return true;
            }
            //Determinar si se puede cancelar la cita
            if (cita.StartDate < horaLocal)
                return false;
            var horasParaLaCita = cita.StartDate.Subtract(horaLocal).TotalHours;
            return horasParaLaCita > parametrosAgenda.NumHorasLimiteModificacionCitas;
        }

        /// <summary>
        /// Retorna la hora en que la fecha inicio para la zona horaria UTC
        /// Requiere que este configurada la zona horaria de la empresa, de lo contrario 
        /// retorna NULL pues no es posible conocer la hora local
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="cita"></param>
        /// <returns></returns>
        public DateTime? ConvertirFechaLocalAUtc(int idEmpresa, DateTime fecha)
        {
            var zonaHoraria = _horarioRep.GetZonaHorariaDeEmpresa(idEmpresa);
            if (zonaHoraria == null)
                return null;
            /*Convertir la hora de inicio de la cita (local) a su equivalente UTC*/
            return DateUtils.ConvertirFechaLocalAUtc(zonaHoraria,fecha);
        }

        /// <summary>
        /// Cancela la lista de citas dado su ID
        /// Solo cancela citas que aun no se hayan reealizado (En estado pendiente o En espera)
        /// </summary>
        /// <param name="listaIds"></param>
        public void CancelarCitas(List<int> listaIds,int idUsuario,string nombreSolicita,int origen, int idMedio, string notasAdicionales)
        {
            using(var con = new SqlConnection(_strCon))
            {
                con.Open();
                SqlTransaction tran = null;
                try
                {
                    tran = con.BeginTransaction();
                    foreach (var id in listaIds)
                    {
                        SqlHelper.ExecuteNonQuery(tran,"[dbo].[UpdateCitaCancelar]", id, idUsuario,
                                                  idMedio, origen, nombreSolicita, notasAdicionales);
                    }
                    tran.Commit();
                }
                catch (Exception)
                {
                    if(tran!=null)
                    {
                        tran.Rollback();
                    }
                    throw;
                }

            }

        }

        public string GetUltimoTelefonoPaciente(int empleado, int beneficiario)
        {
            return _citasRep.GetUltimoTelefonoPaciente(empleado, beneficiario);
        }
    }
}
