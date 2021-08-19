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
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesSedesUsuario;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Asociación de usuarios con sedes
    /// </summary>
    public partial class UsuariosSedes : PaginaBaseAgenda
    {
        #region Variables privadas



        #endregion

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarSedesAsociadas();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
            
        }

        /// <summary>
        /// Guardar sedes asociadas (agrega las seleccionadas, quita las que fueron de-seleccionadas)
        /// </summary>
        private void GuardarSedesAsociadas()
        {
            var selectedUsuario = dbcUsuarios.SelectedValue;
            if(string.IsNullOrEmpty(selectedUsuario))
            {
                ctrMensaje.MostrarMensaje("Debe seleccionar el usuario.",EnumUserMessage.Advertencia);
                return;
            }
            var sedes = new List<InfoSedeVista>();
            ListItemCollection items = chkListaSedes.Items;
            foreach (ListItem i in items)
            {
                var d = new InfoSedeVista();
                d.Sede = new InfoSede {Id = Convert.ToInt32(i.Value)};
                d.Selected = false;
                if (i.Selected)
                {
                    d.Selected = true;
                }
                sedes.Add(d);
            }
            var sedesRep = new SedesDataRepository();
            sedesRep.RegistrarSedesUsuario(Convert.ToInt32(selectedUsuario), sedes,SessionManager.IdEmpresa);
            ctrMensaje.MostrarMensaje("Usuario actualizado",EnumUserMessage.Notificacion);
        }

        protected void ObjectDataSourceUsuarios_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount == false)
            {
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            }
        }

        protected void ObjectDataSourceSedes_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount == false)
            {
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
                e.InputParameters["idUser"] = SessionManager.IdUser;

            }
        }

        protected void DropDownListUsuarios_DataBound(object sender, EventArgs e)
        {
           
        }

        protected void CheckBoxListSedes_DataBound(object sender, EventArgs e)
        {
            try
            {
                var selectedUsuario = dbcUsuarios.SelectedValue;
                if (string.IsNullOrEmpty(selectedUsuario))
                {
                    return;
                }

                var sedesRep = new SedesDataRepository();
                //Obtener todas las sedes activas asociadas al usuario para la empresa actual
                var sedesUsuario = sedesRep.GetSedesByUsuarioEmpresa(Convert.ToInt32(selectedUsuario),SessionManager.IdEmpresa);
                //Iterar por las sedes del usuario y marcar como seleccionadas en la lista de sedes por empresa
                foreach (InfoSede sede in sedesUsuario)
                {
                    var listItem = chkListaSedes.Items.FindByValue(Convert.ToString(sede.Id));
                    if(listItem!=null)
                    {
                        listItem.Selected = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }

        }

        protected void ObjectDataSourceSedes_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            
        }

        protected void dbcUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chkListaSedes.DataBind();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        #endregion
    }
}