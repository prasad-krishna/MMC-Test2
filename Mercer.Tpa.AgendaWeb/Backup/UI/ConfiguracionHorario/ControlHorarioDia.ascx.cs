using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Control que muestra los intervalos de horario de un día para un médico
    /// </summary>
    public partial class ControlHorarioDia : System.Web.UI.UserControl
    {
        #region Variables privadas

     

        #endregion

        #region propiedades

        public bool MostrarLabelFecha
        {
            set
            {
                LabelFechaDia.Visible = value;
            }

        }

        public DateTime Fecha
        {
            set
            {
                LabelFechaDia.Text = DateUtils.FormatSoloFecha(value);
                if(EsFestivo(value))
                {
                    LabelFechaDia.CssClass = "intervaloFestivo";
                }
                else
                {
                   LabelFechaDia.CssClass=  LabelFechaDia.CssClass.Replace("intervaloFestivo", " ");
                }
                LabelDiaSemana.Text = DateUtils.GetNombreDia(value.DayOfWeek);
            }
        }

        private bool EsFestivo(DateTime value)
        {
            var festivosRep = new DiasFestivosDataRepository();
            return festivosRep.EsFestivo(SessionManager.IdEmpresa,value);
        }

        private List<IntervaloHorarioSede> _intervalos = new List<IntervaloHorarioSede>();
        public List<IntervaloHorarioSede> Intervalos
        {
            set
            {
                _intervalos = value;
                ActualizarIntervalos();
            }
            get
            {
                return _intervalos;
            }
        }
        #endregion

        #region Eventos página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void  Page_Init(object sender, EventArgs e)
        {

        }

        public void ActualizarIntervalos()
        {
            DataListHorarioDia.DataSource = _intervalos;
            DataListHorarioDia.DataBind();
        }

        protected void DataListHorarioDia_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var item = e.Item.DataItem as IntervaloHorarioSede;
            if(item== null)
                return;
            var lblSede = e.Item.FindControl("LabelSede") as Label;
            var lblHoraInicio = e.Item.FindControl("LabelHoraInicio") as Label;
            var lblHoraFin = e.Item.FindControl("LabelHoraFin") as Label;
            lblSede.Text = item.Sede==null?string.Empty:Server.HtmlEncode(item.Sede.Nombre);
            lblHoraInicio.Text = string.Format("{0:hh:mm tt}", item.FechaInicio);
            lblHoraFin.Text = string.Format("{0:hh:mm tt}", item.FechaFin);
           

            //Guardar datos requeridos por codigo de cliente en el link de eliminar intervalo
            var btnEliminar = e.Item.FindControl("_btnEliminarIntervalo") as HtmlAnchor;
            if(btnEliminar == null)
                throw new ApplicationException("No se encontró elemento con id:_btnEliminarIntervalo");
            btnEliminar.Attributes.Add("data-idIntervalo",item.Id.ToString());
            btnEliminar.Attributes.Add("data-fecha",item.Fecha.ToString("s"));

            /*Verificar si el intervalo actual esta en conflicto con otro intervalo del mismo día */
            foreach (var intervalo in Intervalos)
            {
                if(intervalo!= item && DateUtils.Intersects(intervalo.FechaInicio,intervalo.FechaFin,item.FechaInicio,item.FechaFin))
                {
                    e.Item.CssClass+=" intervaloConflicto";
                    e.Item.ToolTip = "Este intervalo se encuentra en conflicto.";
                }
            }
      
   
        
        }

        protected void LinkEliminarIntervaloHorario_Click(object sender, EventArgs e)
        {
            var dataListItem = (sender as LinkButton).Parent as DataListItem;
            _intervalos.RemoveAt(dataListItem.ItemIndex);
            ActualizarIntervalos();
        }

        #endregion
    }
}