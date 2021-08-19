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
using System.Data.SqlClient;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.BusquedaCitas;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Operaciones de acceso a datos asociadas a las citas
    /// </summary>
    public class CitasDataRepository : ICitasDataRepository
    {
        #region Variables privadas
        readonly string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Propiedades
        /// <summary>
        /// Retorna el repositorio para obtener las sedes de la base de datos.
        /// Se hace virtual para facilitar las pruebas unitarias
        /// </summary>
        public virtual SedesDataRepository RepositorioSedes
        {
            get
            {
                return new SedesDataRepository();
            }
        }

        /// <summary>
        /// Retorna el repositorio para obtener los tipos de cita de la base de datos
        /// </summary>
        public virtual TiposCitaDataRepository RepositorioTipoCita
        {
            get
            {
                return new TiposCitaDataRepository();
            }
        }
        #endregion

        #region Métodos publicos

        /// <summary>
        /// Registra una nueva cita en la base de datos
        /// </summary>
        /// <param name="cita">Instancia de Cita que se registra</param>
        /// <param name="IdUsuario">Id de usuario que realiza la operación</param>
        public void RegistrarCita(Cita cita,int IdUsuario)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertCita]",IdUsuario, cita.StartDate, cita.EndDate, cita.Prestador.Id, cita.Sede.Id,
                                  cita.Tipo.Id, cita.NotasAdicionales, cita.EstadoCita, cita.Recordatorio
                                  , cita.NombrePaciente, cita.EmpleadoIdentificacion, cita.PacienteIdentificacion, cita.IdEmpleado,
                                  cita.IdBeneficiario, cita.TelefonosContacto,cita.FechaInicioUtc);
        }

        /// <summary>
        /// Obtiene una cita dado su Id
        /// </summary>
        /// <param name="idCita">Identificador de la cita en la base de datos</param>
        /// <returns>Cita</returns>
        public Cita GetCitaById(int idCita)
        {
            Cita cita = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "[dbo].[GetCitaById]", idCita))
            {
                while (reader.Read())
                {
                    cita = LeerCitaDeDataReader(reader);
                }
            }
            return cita;
        }


        /// <summary>
        /// Retorna los registros de cita que cumplen los criterios especificados.
        /// </summary>
        /// <param name="parametros">Objeto contenedor de los parámetros</param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
       public List<CitaResult> GetCitasBusqueda(ParametrosBusquedaCita parametros, string sortExpression, int startRowIndex, int maximumRows)
       {
           int total;
           return GetCitasBusqueda(parametros,out total, sortExpression, startRowIndex, maximumRows);
       }

        /// <summary>
        /// Retorna el total de registros que cumplen con los criterios de búsqueda (este valor es requerido para paginación)
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
       public int GetTotalCitasBusqueda(ParametrosBusquedaCita parametros, string sortExpression, int startRowIndex, int maximumRows)
       {
           int total;
           GetCitasBusqueda(parametros, out total, sortExpression, startRowIndex, maximumRows);
           return total;
       }

        /// <summary>
        /// Retorna los resultados de la búsqueda de citas para los parámetros especificados y retorna el total de registros para paginación
        /// </summary>
        /// <param name="count">Out, retorna el total de registros</param>
        /// <param name="sortExpression">Expresión utilizada para ordenar los resultados</param>
        /// <param name="startRowIndex">(Paginación) registro inicial</param>
        /// <param name="maximumRows">(Paginación) total de registros</param>
        /// <param name="parametrosBusquedaCita"></param>
        /// <returns>Lista de objetos de tipo CitaResult para mostrar en la búsqueda</returns>
        private List<CitaResult> GetCitasBusqueda( ParametrosBusquedaCita parametrosBusquedaCita,out int count,string sortExpression, int startRowIndex, int maximumRows)
        {
            var listaCitas = new List<CitaResult>();
            var con = new SqlConnection(_strCon);
            var cmd = SqlHelper.CreateCommand(con, "[dbo].[GetCitasBusqueda1]", new string[]{});
            cmd.Parameters.Add(new SqlParameter("@sede_id", IsNullZero(parametrosBusquedaCita.IdSede)));
            cmd.Parameters.Add(new SqlParameter("@prestador_id",IsNullZero(parametrosBusquedaCita.IdMedico)));
            cmd.Parameters.Add(new SqlParameter("@especialidad_id",IsNullZero(parametrosBusquedaCita.IdEspecialidad)));
            cmd.Parameters.Add(new SqlParameter("@recordatorio",IsNullMinus(parametrosBusquedaCita.Recordatorio)));
            cmd.Parameters.Add(new SqlParameter("@estado",IsNullZero(parametrosBusquedaCita.Estado)));
            cmd.Parameters.Add(new SqlParameter("@identificacionempleado", parametrosBusquedaCita.IdentificacionEmpleado ?? string.Empty));
            cmd.Parameters.Add(new SqlParameter("@identificacionpaciente",parametrosBusquedaCita.IdentificacionPaciente ?? string.Empty));
            cmd.Parameters.Add(new SqlParameter("@nombrePaciente",parametrosBusquedaCita.NombrePaciente ?? string.Empty));
            
            cmd.Parameters.Add(new SqlParameter("@fechaInicio",parametrosBusquedaCita.FechaInicio));
            cmd.Parameters.Add(new SqlParameter("@fechaFin",parametrosBusquedaCita.FechaFin));

            cmd.Parameters.Add(new SqlParameter("@sortExpression",sortExpression ?? string.Empty));
            cmd.Parameters.Add(new SqlParameter("@RowIndex",startRowIndex));
            cmd.Parameters.Add(new SqlParameter("@PageSize",maximumRows));
            cmd.Parameters.Add(new SqlParameter("@idUsuario",parametrosBusquedaCita.IdUsuario));
            cmd.Parameters.Add(new SqlParameter("@idEmpresa",parametrosBusquedaCita.IdEmpresa));
            var parametroTotalRegistros = new SqlParameter("@totalRegistros", SqlDbType.Int);
            parametroTotalRegistros.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parametroTotalRegistros);
            count = 0;
            using (con)
            {
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new CitaResult();
                        c.Id = Convert.ToInt32(reader["cita_id"]);
                        c.StartDate = (DateTime)reader["fechaInicio"];
                        c.EndDate = (DateTime)reader["fechaFin"];
                        c.IdPrestador = Convert.ToInt32(reader["prestador_id"]);
                        c.NombrePrestador = (string)reader["NombrePrestador"];
                        c.IdSede = Convert.ToInt32(reader["sede_id"]);
                        c.NombreSede = (string)reader["nombreSede"];
                        c.IdTipoCita = Convert.ToInt32(reader["tipoCita_id"]);
                        c.NombreTipoCita = (string)reader["nombreTipoCita"];
                        c.DuracionTipoCita = Convert.ToInt32(reader["duracionTipoCita"]);
                        c.NotasAdicionales = (string)reader["notasAdicionales"];
                        c.EstadoCita = (EnumEstadoCita)reader["estado"];
                        c.Recordatorio = Convert.ToBoolean(reader["recordatorio"]);
                        c.NombrePaciente = (string)reader["nombrePaciente"];
                        c.EmpleadoIdentificacion = (string)reader["identificacion_empleado"];
                        c.PacienteIdentificacion = (string)reader["identificacion_paciente"];
                        c.IdEmpleado = Convert.ToInt32(reader["idempleado"]);
                        c.IdBeneficiario = Convert.ToInt32(reader["idbeneficiario"]);
                        c.TelefonosContacto = (string)reader["telefonosContacto"];
                        /*Solo agregar las citas dentro del intervalo horario especificado*/
                        if(parametrosBusquedaCita.HorarioEspecifico)
                        {
                            if(DateUtils.Intersects(c.StartDate.TimeOfDay,c.EndDate.TimeOfDay,parametrosBusquedaCita.HoraInicio,parametrosBusquedaCita.HoraFin))
                            {
                                listaCitas.Add(c);
                            }
                        }
                        else
                        {
                            listaCitas.Add(c); 
                        }
                        
                    }
                }
            }
            count = (int)cmd.Parameters["@totalRegistros"].Value;
            
            return listaCitas;
        }





        /// <summary>
        /// Retorna las citas del médico entre dos fechas. En estado pendiente,en espera o finalizadas
        /// que se mostrarán en el calendario
        /// </summary>
        /// <param name="idMedico">Identificador del medico</param>
        /// <param name="startDate">Fecha de Inicio</param>
        /// <param name="endDate">Fecha de Finalizacion</param>
        /// <param name="idEmpresa">Id de la empresa actual del usuario (para filtrar citas)</param>
        /// <returns></returns>
        public List<Cita> GetCitasMedico(int idMedico, DateTime startDate, DateTime endDate, int? idEmpresa)
        {
            var listaCitas = new List<Cita>();

            using (var reader = SqlHelper.ExecuteReader(_strCon, "[dbo].[GetCitasMedico]", idMedico, startDate, endDate,idEmpresa))
            {
                while (reader.Read())
                {
                    var c = LeerCitaDeDataReader(reader);
                    listaCitas.Add(c);
                }
            }
            return listaCitas;
        }


        /// <summary>
        /// Cancela una cita y registra los datos de cancelación.
        /// </summary>
        /// <param name="idCita">Id de la cita a cancelar</param>
        /// <param name="idUsuario">Id del usuario que cancela</param>
        public void CancelarCita(int idCita, int idUsuario, string nombreSolicita, int idMedio,  string notasAdicionales, EnumOrigenModificacionCita origen)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateCitaCancelar]", idCita, idUsuario,idMedio,origen, nombreSolicita,  notasAdicionales);
   
        }   
        
        /// <summary>
        /// Reprograma una cita
        /// </summary>
        /// <param name="idCita">Id de la cita a cancelar</param>
        /// <param name="idUsuario">Id del usuario que cancela</param>
        public void ReprogramarCita(int idCita,int IdSede, DateTime fechaInicio,DateTime fechaFin,DateTime? fechaInicioUtc, int idUsuario, string nombreSolicita, int idMedio,  string notasAdicionales, EnumOrigenModificacionCita origen)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateCitaReprogramar]", idCita,idUsuario,IdSede ,fechaInicio,fechaFin, idMedio,origen, nombreSolicita,  notasAdicionales,fechaInicioUtc);
   
        }

        /// <summary>
        /// Retorna el ultimo telefono usado para registrar una cita dado el id de emmpleado y beneficiario
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idBeneficiario"></param>
        /// <returns></returns>
        public string GetUltimoTelefonoPaciente(int idEmpleado, int idBeneficiario)
        {
            string ultimo =  (SqlHelper.ExecuteScalar(_strCon, "GetUltimoTelefonoPaciente",idEmpleado,idBeneficiario) ?? string.Empty).ToString();
            return ultimo;
        }

        /// <summary>
        /// Registra un nuevo intento de ubicar al paciente para recordarle su cita
        /// </summary>
        /// <param name="idCita"></param>
        /// <param name="idUsuario"></param>
        /// <param name="idMedio">Id del medio de contacto</param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="exitoso"></param>
        public void RegistrarRecordatorio(int idCita, int idUsuario, int idMedio, string notas, DateTime fecha, bool exitoso)
        {
           
             SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertRecordatorioCita]", idCita, idUsuario, idMedio, notas, fecha, exitoso);
       

        }

        /// <summary>
        /// Actualiza la cita indicando la llegada del paciente
        /// </summary>
        /// <param name="idCita">Id de la cita</param>
        public void RegistrarLlegadaPaciente(int idCita,int idUsuario)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateCitaLlegadaPaciente]", idCita,idUsuario);
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Llena un objeto de tipo cita a partir de un DataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Cita LeerCitaDeDataReader(SqlDataReader reader)
        {
            var pdr = new PrestadoresDataRepository();
            var sdr = new SedesDataRepository();
            var tcdr = new TiposCitaDataRepository();
            var cita = new Cita();
            cita.Id = Convert.ToInt32(reader["cita_id"]);
            cita.StartDate = (DateTime)reader["fechaInicio"];
            cita.EndDate = (DateTime)reader["fechaFin"];
            cita.Prestador = pdr.GetPrestadorById(Convert.ToInt32(reader["prestador_id"]));
            cita.Sede = sdr.GetSedeById(Convert.ToInt32(reader["sede_id"]));
            cita.Tipo = tcdr.GetTiposCitaById(Convert.ToInt32(reader["tipoCita_id"]));
            cita.NotasAdicionales = (string)reader["notasAdicionales"];
            cita.EstadoCita = (EnumEstadoCita)reader["estado"];
            cita.Recordatorio = Convert.ToBoolean(reader["recordatorio"]);
            cita.NombrePaciente = (string)reader["nombrePaciente"];
            cita.EmpleadoIdentificacion = reader["identificacion_empleado"].ToString();
            cita.PacienteIdentificacion = reader["identificacion_paciente"].ToString();
            cita.IdEmpleado = Convert.ToInt32(reader["idempleado"]);
            cita.IdBeneficiario = Convert.ToInt32(reader["idbeneficiario"]);
            cita.TelefonosContacto = (string)reader["telefonosContacto"];
            return cita;
        }

        /// <summary>
        /// Retorna null si el número es menor que certo
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int? IsNullMinus(int number)
        {
            if (number == -1)
            {
                return null;
            }
            else
            {
                return number;
            }
        }

        /// <summary>
        /// Retorna null sin el numero es menor o igual a 0
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int? IsNullZero(int number)
        {
            if (number <= 0)
            {
                return null;
            }
            else
            {
                return number;
            }
        }



        #endregion


    }
}
