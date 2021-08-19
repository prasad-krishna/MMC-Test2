using System.Collections.Generic;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesSedesUsuario;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    public interface ISedesDataRepository
    {
        /// <summary>
        /// Retorna todas las sedes asociadas a un codigo de empresa , Usado para cuando se necesite Paginacion
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        IEnumerable<InfoSede> GetSedesByEmpresa(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 29/07/2010
        /// Funcionalidad: Retorna todas las sedes asociadas a un codigo de empresa , Usado para cuando se necesite Paginacion        
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        IEnumerable<InfoSede> GetSedesByEmpresaUser(int idEmpresa, int idUser, string sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Retorna todas las sedes asociadas a un codigo de empresa
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        IEnumerable<InfoSede> GetSedesByEmpresa(int idEmpresa);

        /// <summary>
        /// Devuelve el total de sedes que tiene una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>total de sedes de una empresa</returns>
        int GetTotalSedes(int idEmpresa, int sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 29/07/2010
        /// Devuelve el total de sedes filtrado por empresa y usuario
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>total de sedes de una empresa</returns>
        int GetTotalSedesByEmpresaUser(int idEmpresa, int idUser, int sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Registra una nueva sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sede"></param>
        int RegistrarSede(int idEmpresa, InfoSede sede);

        /// <summary>
        /// Elimina una sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idSede"></param>
        void EliminarSede(int idEmpresa, int idSede);

        /// <summary>
        /// Actualiza una sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sede"></param>
        void ActualizarSede(int idEmpresa, InfoSede sede);

        List<InfoSede> GetSedesUsuario(int idUsuario);
        void RegistrarSedesUsuario(int idUsuario, List<InfoSedeVista> sedes, int idEmpresa);

        /// <summary>
        /// Obtener una sede dado su id
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        InfoSede GetSedeById(int idSede);

        /// <summary>
        /// Registra una relacion entre un prestador y una sede , no es necesario el idempresa porq la sede es unica
        /// </summary>
        /// <param name="idPrestador"></param>
        /// <param name="idSede"></param>
        void RegistrarUsuarioSede(int idUsuario, int idSede);

        /// <summary>
        /// Elimina la relacion entre un prestador y una sede , no es necesario el idempresa porq la sede es unica
        /// </summary>
        /// <param name="idPrestador"></param>
        /// <param name="idSede"></param>
        void EliminarUsuarioSede(int idUsuario, int idSede);
    }
}