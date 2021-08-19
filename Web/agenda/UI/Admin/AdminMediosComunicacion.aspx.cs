using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Administración de medios de contacto usados al momento de cancelar cita
    /// </summary>
    public partial class AdminMediosComunicacion : PaginaBaseAgenda
    {

        #region Variables privadas

        MediosComunicacionDataRepository _mediosRep = new MediosComunicacionDataRepository();

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

        protected void btnGuardarMedio_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarMedioContacto();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al intentar guardar el medio");
            }
        }


        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarMedio();
            }
            catch (SqlException sqlEx)
            {
      
                ctrError.MostrarError(sqlEx, "No fue posible eliminar el registro, otros registros de la base de datos dependen de este.");
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex, "Se presentó un error al intentar eliminar el medio");
            }
        }


        protected void ObjectDataSourceMedios_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount == false)
            {
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            }
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Inserta o actualiza un medio de contacto
        /// </summary>
        private void GuardarMedioContacto()
        {
            var medio = new MedioComunicacion();
            if (Request.Params["idMedio"] != "") //update
            {
                medio.Id = Convert.ToInt32(Request.Params["idMedio"]);
                medio.Nombre = Request.Params["nombreMedio"];
                var mcdr = new MediosComunicacionDataRepository();
                mcdr.ActualizarMedio(SessionManager.IdEmpresa, medio);
                grdMedios.DataBind();
            }
            else //new
            {
                medio.Nombre = Request.Params["nombreMedio"];
                var mcdr = new MediosComunicacionDataRepository();
                medio.Id = mcdr.GetTotalMedios(SessionManager.IdEmpresa, 0, 0, 0) + 1;
                mcdr.RegistrarMedio(SessionManager.IdEmpresa, medio);
                grdMedios.DataBind();
            }
        }

        /// <summary>
        /// Elimina un medio de contacto
        /// </summary>
        private void EliminarMedio()
        {
            if (Request.Params["idMedioABorrar"] != "")
            {
                int idMedio = Convert.ToInt32(Request.Params["idMedioABorrar"]);
                var mcdr = new MediosComunicacionDataRepository();
                mcdr.EliminarMedio(SessionManager.IdEmpresa, idMedio);
                grdMedios.DataBind();
            }
        }

        #endregion

        protected void grdMedios_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void grdMedios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var idRegistro = (int)grdMedios.DataKeys[e.Row.RowIndex].Value;
                    var medio = _mediosRep.GetById(idRegistro);
                    var label = e.Row.FindControl("lblMedioActiva") as Label;
                    var btnDesactivar = e.Row.FindControl("btnDesactivar") as Button;
                    var btnActivar = e.Row.FindControl("btnActivar") as Button;
                    if (label == null)
                        throw new ApplicationException("No encontró label lblMedioActiva en evento rowDatabound");
                    btnActivar.Visible = !medio.Activa;
                    btnDesactivar.Visible = medio.Activa;

                    if (medio.Activa)
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

        protected void grdMedios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var idRegistro = (int)grdMedios.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                if (e.CommandName == "Activar")
                {
                    _mediosRep.ActivarMedio(idRegistro);
                    grdMedios.DataBind();
                }
                else if (e.CommandName == "Desactivar")
                {
                    _mediosRep.DesactivarMedio(idRegistro);
                    grdMedios.DataBind();
                }
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }
    }
}