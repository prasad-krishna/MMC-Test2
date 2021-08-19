using System.Collections.Generic;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    public interface ITiposCitaDataRepository
    {
        /// <summary>
        /// Devuelve todos los tipos de cita asociados a la empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista de tipoCita con todos los tipos de cita asociados a la empresa</returns>
        IEnumerable<TipoCita> GetTiposCitaByEmpresa(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows);
        IEnumerable<TipoCita> GetTiposCitaActivosByEmpresa(int idEmpresa);

        IEnumerable<TipoCita> GetTiposCitaByEmpresa(int idEmpresa);
        TipoCita GetTiposCitaById(int idTipoCita);

        /// <summary>
        /// Devuelve el total de tipos de citas que hay por empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns>numero total de tipos de cita</returns>
        int GetTotalTiposCita(int idEmpresa);

        int GetTotalTiposCita(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Registra un nuevo tipo de cita asociado a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="tipoCita"></param>
        void RegistrarTipoCita(int idEmpresa,TipoCita tipoCita);

        void EliminarTipoCita(int idTipoCita);

        /// <summary>
        /// Actualiza un tipo de cita especifico asociado a una empresa especifica
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="tipoCita"></param>
        void ActualizarTipoCita(int idEmpresa,TipoCita tipoCita);
    }
}