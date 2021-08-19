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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor:Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Pantalla de selección de médico
    /// </summary>
    public partial class HorarioMedicoInicio : PaginaBaseAgenda
    {
        private int selectedMedico = -1;
        private int selectedSede = -1;
        private int selectedRecordatorio = -1;
        private int selectedEstado = -1;
        private int selectedIdentificacionEmpleado = -1;
        private int selectedIdentificacionPaciente = -1;
        private int selectedIdEmpleado = -1; //usado solo para citas pacientes
        private int selectedIdPaciente = -1; //usadi solo para citas pacientes
        private string nombrePaciente = "";
        private int selectedCita = -1;
        private static bool redirected = false;

        protected override void Page_Load(object sender, EventArgs e)
        {

            try
            {
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {

                    CargarEspecialidades();
                }
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }

        }

        private void CargarEspecialidades()
        {
            EspecialidadDataRepository edr = new EspecialidadDataRepository();
            List<Especialidad> listaEspecialidades = edr.GetEspecialidades() as List<Especialidad>;
            dbcEspecialidad.DataMember = "Especialidad";
            dbcEspecialidad.DataTextField = "Nombre";
            dbcEspecialidad.DataValueField = "Id";
            dbcEspecialidad.DataSource = listaEspecialidades;
            dbcEspecialidad.DataBind();
        }

        private void AsignarFiltros()
        {
            if (dbcMedicos.SelectedIndex > 0)
            {
                selectedMedico = Convert.ToInt32(dbcMedicos.SelectedValue);
            }


        }


        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                AsignarFiltros();
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }


        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        protected void ListBoxEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarMedicosParaEspecialidadSeleccionada();
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }
        }

        private void CargarMedicosParaEspecialidadSeleccionada()
        {
            List<InfoPrestador> listaPrestadores = new List<InfoPrestador>();
            var pdr = new PrestadoresDataRepository();
            if (dbcEspecialidad.SelectedIndex != 0)
            {

                int selectedVal = Convert.ToInt32(dbcEspecialidad.SelectedValue);
                listaPrestadores =
                    pdr.GetPrestadoresPorEspecialidad(SessionManager.IdEmpresa, selectedVal) as List<InfoPrestador>;
            }
            else //Todas las especialidades , por lo tanto cargar todos los medicos mezclados
            {
                listaPrestadores =
                    pdr.GetPrestadoresPorEspecialidad(SessionManager.IdEmpresa, -1) as List<InfoPrestador>;
            }
            dbcMedicos.DataMember = "InfoPrestador";
            dbcMedicos.DataTextField = "Name";
            dbcMedicos.DataValueField = "Id";
            dbcMedicos.DataSource = listaPrestadores;
            dbcMedicos.DataBind();
        }


        protected void ListBoxEspecialidad_DataBound(object sender, EventArgs e)
        {
            dbcEspecialidad.Items.Insert(0, "-- Seleccione Especialidad -- ");
        }

        protected void ListBoxMedicos_DataBound(object sender, EventArgs e)
        {
            dbcMedicos.Items.Insert(0, "-- Seleccione médico -- ");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dbcMedicos.SelectedIndex > 0)
                {
                    Response.Redirect("ConfigurarHorarioMedico.aspx?idMedico=" + dbcMedicos.SelectedValue + "&rand=" + DateTime.Now.Ticks);
                }
                else
                {
                    ctrMensaje.MostrarMensaje("Debe seleccionar un médico para configurar su horario", EnumUserMessage.Advertencia);
                }
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }
        }
    }
}
