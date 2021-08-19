using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.Notificaciones
{
    public partial class Notificador : System.Web.UI.UserControl
    {
        #region "Variables privadas"

        private int _numResults = 30;

        #endregion

        #region Propiedades

        public int NumResults
        {
            get { return _numResults; }
            set { _numResults = value; }
        }


        #endregion

        #region Eventos página
  
        protected void Page_Load(object sender, EventArgs e)
        {
            var mgr = new NotificacionesManager();
            var listaNotificaciones = mgr.GetNotificacionesMedico(SessionManager.IdPrestador, NumResults,SessionManager.IdEmpresa);
            dlNotificaciones.DataSource = listaNotificaciones;
            dlNotificaciones.DataBind();
        }
        public DataList GetDataList { get { return dlNotificaciones; } }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Notificacion notificacion = (Notificacion) e.Item.DataItem;
                if(notificacion.Tipo==EnumTipoNotificacion.CitaRegistrada)
                {
                    e.Item.CssClass = "citaRegistrada";
                }
                else if(notificacion.Tipo==EnumTipoNotificacion.CitaReprogramada)
                {
                    e.Item.CssClass = "citaReprogramada";
                }
                else if(notificacion.Tipo==EnumTipoNotificacion.CitaCancelada)
                {
                    e.Item.CssClass = "citaCancelada";
                }
                else if(notificacion.Tipo==EnumTipoNotificacion.CitaEnEspera)
                {
                    e.Item.CssClass = "citaEspera";
                }
            }
        }
        #endregion
    }
}