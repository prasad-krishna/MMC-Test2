namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Representa un tipo de cita
    /// </summary>
    public class TipoCita
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        private bool _activa=true;
        public bool Activa
        {
            get { return _activa; }
            set { _activa = value; }
        }

       
    }
}