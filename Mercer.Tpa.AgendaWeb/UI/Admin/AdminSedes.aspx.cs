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
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesSedesUsuario;
using System.Collections.Generic;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Módulo de administración de sedes
    /// </summary>
    public partial class AdminSedes : PaginaBaseAgenda
    {

        #region Variables privadas

        SedesDataRepository _sedesRep = new SedesDataRepository();

        #endregion
        #region Eventos página

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                    grdSedes.DataBind();
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }

        }

        protected void grdSedes_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void grdSedes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var idSede = (int)grdSedes.DataKeys[e.Row.RowIndex].Value;
                    var sede = _sedesRep.GetSedeById(idSede);
                    var label = e.Row.FindControl("lblSedeActiva") as Label;
                    var btnDesactivar = e.Row.FindControl("btnDesactivar") as Button;
                    var btnActivar = e.Row.FindControl("btnActivar") as Button;
                    if (label == null)
                        throw new ApplicationException("No encontró label lblSedeActiva en evento rowDatabound");
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

        protected void GridSedes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                var idSede = (int)grdSedes.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                if (e.CommandName == "Activar")
                {
                    _sedesRep.ActivarSede(idSede);
                    grdSedes.DataBind();
                }
                else if (e.CommandName == "Desactivar")
                {
                    _sedesRep.DesactivarSede(idSede);
                    grdSedes.DataBind();
                }
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
                GuardarSede();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al intentar guardar la sede");
            }
        }


        protected void ObjDataSourceSedes_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            e.InputParameters["idUser"] = SessionManager.IdUser;

        }


        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarSede();
            }
            catch (SqlException sqlEx)
            {
                ctrError.MostrarError(sqlEx, "No es posible eliminar una sede cuando existen citas asociadas,usuarios asociados, o se han configurado horarios en dicha sede.");
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "No fue posible eliminar la sede.");
            }
        }

        #endregion

        #region Metodos privados

        /// <summary>
        /// Crea o actualiza una sede
        /// </summary>
        private void GuardarSede()
        {
            var infoSede = new InfoSede();
            if (Request.Params["idSede"] != "") //update
            {
                infoSede.Id = Convert.ToInt32(Request.Params["idSede"]);
                infoSede.Nombre = Request.Params["nombreSede"];
                infoSede.Descripcion = Request.Params["descripcionSede"];
                var sdr = new SedesDataRepository();
                sdr.ActualizarSede(SessionManager.IdEmpresa, infoSede);
                grdSedes.DataBind();
            }
            else //new
            {
                infoSede.Nombre = Request.Params["nombreSede"];
                infoSede.Descripcion = Request.Params["descripcionSede"];
                var sdr = new SedesDataRepository();
                infoSede.Id = sdr.GetTotalSedes(SessionManager.IdEmpresa, 0, 0, 0) + 1;
                var sedeId = sdr.RegistrarSede(SessionManager.IdEmpresa, infoSede);                
                
                /*Inicio Marco A. Herrera G. 30/07/2010
                 Se registran los permisos en la nueva sede al usuario 
                */

                var lstSedes = new List<InfoSedeVista>();
             
                var sede = new InfoSedeVista();
                sede.Sede = infoSede; //Se pasa el objeto sede creado
                sede.Sede.Id = sedeId;
                sede.Selected = true;

                lstSedes.Add(sede);                                

                var sedesRep = new SedesDataRepository();
                sedesRep.RegistrarSedesUsuario(SessionManager.IdUser, lstSedes, SessionManager.IdEmpresa);                

                //Fin MAHG 30/07/2010

                grdSedes.DataBind();

            }
        }

        /// <summary>
        /// Elimina una sede
        /// </summary>
        private void EliminarSede()
        {
            if (Request.Params["idSedeABorrar"] != "")
            {
                int idSede = Convert.ToInt32(Request.Params["idSedeABorrar"]);
                var sdr = new SedesDataRepository();
                sdr.EliminarSede(SessionManager.IdEmpresa, idSede);
                grdSedes.DataBind();
            }
        }

        #endregion



    }
}