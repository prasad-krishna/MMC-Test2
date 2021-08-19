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
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Administrador de tipos de cita.
    /// </summary>
    public partial class AdminTipoCitas : PaginaBaseAgenda
    {
        #region Variables privadas

        TiposCitaDataRepository _tipoCitaRep = new TiposCitaDataRepository();

        #endregion
        #region Eventos controles
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
  
        protected void BtnGuardarCita_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarTipocita();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Error al guardar el registro .");
            }
            
        }

        protected void grdTipoCitas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row's command button was
            // clicked. To identify which row's button was clicked, use 
            // the button's CommandArgument property by setting it to the 
            // row's index.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the LinkButton control from the first column.
                var btnActivar = (Button)e.Row.FindControl("btnActivar");
                btnActivar.CommandArgument = e.Row.RowIndex.ToString();
                var btnDesactivar = (Button)e.Row.FindControl("btnDesactivar");
                btnDesactivar.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void grdTipoCitas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var idRegistro = (int)grdTipoCitas.DataKeys[e.Row.RowIndex].Value;
                    var sede = _tipoCitaRep.GetTiposCitaById(idRegistro);
                    var label = e.Row.FindControl("lblTipoCitaActiva") as Label;
                    var btnDesactivar = e.Row.FindControl("btnDesactivar") as Button;
                    var btnActivar = e.Row.FindControl("btnActivar") as Button;
                    if (label == null)
                        throw new ApplicationException("No encontró label lblTipoCitaActiva en evento rowDatabound");
                    btnActivar.Visible = !sede.Activa;
                    btnDesactivar.Visible = sede.Activa;

                    if (sede.Activa)
                    {
                        label.Text = "Si";
                    }
                    else
                    {
                        label.Text = "No";
                    }
                }

            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }
        }

        protected void GridTipoCitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var idRegistro = (int)grdTipoCitas.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                if (e.CommandName == "Activar")
                {
                    _tipoCitaRep.ActivarTipoCita(idRegistro);
                    grdTipoCitas.DataBind();
                }
                else if (e.CommandName == "Desactivar")
                {
                    _tipoCitaRep.DesactivarTipoCita(idRegistro);
                    grdTipoCitas.DataBind();
                }
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }
        }

        protected void ObjDataSourceTipoCitas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarRegistro();
            }
            catch (SqlException sqlEx)
            {
                ctrError.MostrarError(sqlEx, "No se pudo eliminar el tipo de cita porque está siendo usado por otros registros de la base de datos. ");
            }
            catch(Exception ex)
            {
                ctrError.MostrarError(ex, "No fue posible eliminar el tipo de cita");
            }

        }


        #endregion

        #region Métodos privados

        /// <summary>
        /// Elimina un tipo de cita
        /// </summary>
        private void EliminarRegistro()
        {
            if (Request.Params["idTipoCitaABorrar"] != "")
            {
                int idTipoCita = Convert.ToInt32(Request.Params["idTipoCitaABorrar"]);
                TiposCitaDataRepository tcdr = new TiposCitaDataRepository();
                tcdr.EliminarTipoCita(idTipoCita);
                grdTipoCitas.DataBind();
            }
        }
        /// <summary>
        /// Crea o actualiza una cita
        /// </summary>
        private void GuardarTipocita()
        {
            TipoCita tipoCita = new TipoCita();
            if (Request.Params["idTipoCita"] != "")//update
            {
                tipoCita.Id = Convert.ToInt32(Request.Params["idTipoCita"]);
                tipoCita.Name = Request.Params["nombreTipoCita"];
                tipoCita.Duration =Convert.ToInt32(Request.Params["duracionTipoCita"]);
                TiposCitaDataRepository tcdr = new TiposCitaDataRepository();
                tcdr.ActualizarTipoCita(SessionManager.IdEmpresa,tipoCita);
                grdTipoCitas.DataBind();
            }
            else //Se está creando nuevo
            {
                tipoCita.Name = Request.Params["nombreTipoCita"];
                tipoCita.Duration = Convert.ToInt32(Request.Params["duracionTipoCita"]);
                TiposCitaDataRepository tcdr = new TiposCitaDataRepository();
                //tipoCita.Id = tcdr.GetTotalTiposCita(0) + 1;
                tcdr.RegistrarTipoCita(SessionManager.IdEmpresa,tipoCita);
                grdTipoCitas.DataBind();
            }
        }

        #endregion


    }
}
