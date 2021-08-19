using System;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Representa una sede configurada
    /// </summary>
    [Serializable]
    public class InfoSede
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        private bool _activa=true;
        public bool Activa
        {
            get { return _activa; }
            set { _activa = value; }
        }

        public bool Desactiva
        {
            get
            {
                return !Activa;
            }
        }
    }
}