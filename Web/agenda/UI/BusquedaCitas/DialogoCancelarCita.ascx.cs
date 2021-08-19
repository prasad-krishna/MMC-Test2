using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.BusquedaCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Formlario de cancelación de cita
    /// </summary>
    public partial class DialogoCancelarCita : System.Web.UI.UserControl
    {

        #region Eventos

        public event EventHandler CancelarCita;

        #endregion

        #region Propiedades

        public int IdCita
        {
            get
            {
                return Convert.ToInt32(Request.Params["idCita"]);
            }
        }

        public string NombreSolicita
        {
            get
            {
                return solicitante.Text;
            }
        }

        /// <summary>
        /// Determina quien solicita la cancelación de la cita
        /// </summary>
        public EnumOrigenModificacionCita Origen
        {
            get
            {
                return (EnumOrigenModificacionCita)Convert.ToInt32( DropDownListOrigen.SelectedValue);
            }
        }

        /// <summary>
        /// Retorna el medio de comunicación por el cual se solicitó la cita
        /// </summary>
        public MedioComunicacion Medio
        {
            get
            {
                return GetMedioComunicacionSeleccionado();
            }
        }

        /// <summary>
        /// Retorna las notas adicionales asociadas a la cancelación
        /// </summary>
        public string NotasAdicionales
        {
            get
            {
                return Request.Params["notasCancelacion"] ?? string.Empty;
            }
        }
        #endregion

        #region Métodos privados

        /// <summary>
        /// Retorna el medio de comunicación seleccionado
        /// </summary>
        /// <returns></returns>
        private MedioComunicacion GetMedioComunicacionSeleccionado()
        {
            var medioRep = new MediosComunicacionDataRepository();
            var selValue = DropDownListMedio.SelectedValue;
            if (string.IsNullOrEmpty(selValue))
                return null;
            var idMedio = Convert.ToInt32(selValue);
            return medioRep.GetById(idMedio);
        }


        private void CargarMediosComunicacion()
        {
            var mediosRep = new MediosComunicacionDataRepository();
            DropDownListMedio.DataSource = mediosRep.GetActivas(SessionManager.IdEmpresa);
            DropDownListMedio.DataBind();
        }

        private void CargarOrigenesCancelacion()
        {
            DropDownListOrigen.Items.Add(new ListItem("Paciente solicita","1"));
            DropDownListOrigen.Items.Add(new ListItem("Médico solicita","2"));
            DropDownListOrigen.Items.Add(new ListItem("Otro","3"));

        }


        #endregion

        #region Eventos página


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarOrigenesCancelacion();
                CargarMediosComunicacion();
            }
        }

        protected void BtnOkCancelacion_Click(object sender, EventArgs e)
        {
            if (CancelarCita != null)
            {
                CancelarCita(this, null);
            }
        }

        #endregion

    }
}