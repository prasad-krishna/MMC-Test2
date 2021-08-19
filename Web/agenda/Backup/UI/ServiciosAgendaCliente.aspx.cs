using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.Alertas;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI
{
    /// <summary>
    /// Servicio que retorna información en formato JSON requerida por el cliente.
    /// Se decidió utilizar esta aproximación en vez de WebMethods pues hay un
    /// cambio de la versión 2 a 3.5 de ASP.NET que modifica el formato
    /// JSON al serializar.
    /// http://encosia.com/2009/02/10/a-breaking-change-between-versions-of-aspnet-ajax/
    /// </summary>
    public partial class ServiciosAgendaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Request.Params["action"]))
                throw new ApplicationException("Parámetro requerido: action en ServiciosAgendaCliente");
            var strAction = Request.Params["action"];
            Response.AddHeader("Content-type", "text/json");
            Response.AddHeader("Content-type", "application/json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object obj = ProcesarAccion(strAction);
            Response.Write(serializer.Serialize(obj));
        }

        /// <summary>
        /// Procesa la accion y retorna el objeto que posteriormente será serializado a JSON
        /// para que el cliente lo consuma.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private object ProcesarAccion(string action)
        {
            switch (action)
            {
                case "getAlertas":
                    return ObtenerAlertas();
                    break;
                default:
                    throw new ApplicationException("Accion no soportada:"+action);
            }
        }

        /// <summary>
        /// Retorna las alertas que se le deben mostrar al médico
        /// </summary>
        /// <returns></returns>
        private object ObtenerAlertas()
        {
            var alertaMgr = new AlertasManager();
            return alertaMgr.GetAlertasMedico(SessionManager.IdPrestador, SessionManager.IdEmpresa);
        }
    }

}
