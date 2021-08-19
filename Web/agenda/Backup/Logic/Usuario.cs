using Mercer.Tpa.Agenda.Web.Logic.UtilidadesCadenas;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Representa un usuario del sistema
    /// </summary>
    public class Usuario
    {


        #region Propiedades

        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public int IdCiudad { get; set; }

        public string NombreLogin
        {
            get
            {
                return StringUtils.UppercaseWords(Nombre) + " (" + Login + ")";
            }
        }

        #endregion
    }
}