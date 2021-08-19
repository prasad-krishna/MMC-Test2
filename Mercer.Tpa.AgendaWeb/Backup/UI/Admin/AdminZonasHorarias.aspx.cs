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
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Módulo de administración de zonas horarias
    /// </summary>
    public partial class AdminZonasHorarias : PaginaBaseAgenda
    {
        #region Eventos página

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }


        protected void ObjectDataSourceZonasHorarias_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
        }

        protected void dbcZonasHorarias_DataBound(object sender, EventArgs e)
        {
            try
            {
                //Agregar primer item del control.
                dbcZonasHorarias.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
                //Seleccionar la zona horaria de la empresa en el dropdown
                CargarZonaHorariaEmpresa();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex,"Ocurrió un error.");
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarZonaHoraria();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex,"Ocurrió un error.");
            }
            
        }

        /// <summary>
        /// Actualiza la zona horaria de la empresaa
        /// </summary>
        private void GuardarZonaHoraria()
        {
            var horarioRep = new HorarioRepository();
            //Guardar la zona horaria
            if (dbcZonasHorarias.SelectedItem == null || dbcZonasHorarias.SelectedItem.Value == null)
            {
                return;
            }
            string strVal = dbcZonasHorarias.SelectedItem.Value;
            if (string.IsNullOrEmpty(strVal))
            {
                ctrMensaje.MostrarMensaje("Debe seleccionar la zona horaria", EnumUserMessage.Advertencia);
                return;
            }
            //Actualizar la zona horaria de la empresa.
            horarioRep.SetZonaHorariaEmpresa(SessionManager.IdEmpresa, Convert.ToInt32(strVal));
            ctrMensaje.MostrarMensaje("Zona horaria actualizada",EnumUserMessage.Notificacion);
        }

        #endregion

        #region Métodos privados

        private void CargarZonaHorariaEmpresa()
        {
            int idEmpresa = SessionManager.IdEmpresa;
            var horarioRep = new HorarioRepository();
            ZonaHoraria zonaHoraria = horarioRep.GetZonaHorariaDeEmpresa(idEmpresa);
            if (zonaHoraria == null)
            {
                dbcZonasHorarias.SelectedIndex = 0;
            }
            else
            {
                foreach (ListItem item in dbcZonasHorarias.Items)
                {
                    if (item.Value != null && item.Value == zonaHoraria.Id.ToString())
                    {
                        dbcZonasHorarias.SelectedIndex = dbcZonasHorarias.Items.IndexOf(item);
                    }
                }
            }
        }

        #endregion
    }
}