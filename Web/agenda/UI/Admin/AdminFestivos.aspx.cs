using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor:Juan Camilo Zapata Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Administración de días Festivos
    /// </summary>
    public partial class AdminFestivos : PaginaBaseAgenda
    {
        #region Variables privadas

        private int _selectedMonth;
        private int _selectedYear;

        #endregion

        #region Eventos página

        protected void Page_Init(object sender,EventArgs e)
        {

        }

        private void LeerPeriodoSeleccionado()
        {
            _selectedMonth = Convert.ToInt32(dbcMes.SelectedValue);
            _selectedYear = Convert.ToInt32(dbcAño.SelectedValue);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                    CargarAños();
                }

                LeerPeriodoSeleccionado();
            }
            catch (Exception ex)
            {
                
               ctrError.MostrarError(ex);
            }
 
        }



        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarFestivos();
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex,"Ocurrió un error al intentar guardar los días festivos");
               
            }
            
        }



        /// <summary>
        /// Despues de hacer databind mirar si el valor del item es True , si es asi entonces marcarlo como chequeado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dblstDiasMes_DataBound(object sender, EventArgs e)
        {
            ListItemCollection items = dblstDias.Items;
            foreach (ListItem i in items)
            {
                if (i.Value == "True")
                {
                    i.Selected = true;
                }
            }
        }

        protected void ObjDataSourceFestivos_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            e.InputParameters["year"] = _selectedYear;
            e.InputParameters["month"] = _selectedMonth;
        }

        protected void ObjDataSourceFestivos_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //combinar festivos al resto de dias para mostrarlo en la lista

        }

        protected void dbcMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            dblstDias.DataBind();
        }

        protected void dbcAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            dblstDias.DataBind();
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Carga los siguientes 4 años y el año anterior a la fecha actual
        /// </summary>
        private void CargarAños()
        {
            dbcAño.Items.Clear();
            int añoAnterior = DateTime.Now.Year - 1;
            List<int> años = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                int año = añoAnterior + i;
                años.Add(año);
            }
            dbcAño.DataSource = años;
            dbcAño.DataBind();
            //Seleccionar el segundo item que corresponde con el año actual
            dbcAño.SelectedIndex = 1;
        }

        /// <summary>
        /// Inicia el proceso para guardar los dias festivos para el año seleccionado.
        /// </summary>
        private void GuardarFestivos()
        {
            List<Dia> dias = new List<Dia>();
            ListItemCollection items = dblstDias.Items;
            foreach (ListItem i in items)
            {
                Dia d = new Dia();
                d.fecha = new DateTime(Convert.ToInt32(_selectedYear), Convert.ToInt32(_selectedMonth), Convert.ToInt32(i.Text));
                d.IsFestivo = false;
                if (i.Selected)
                {
                    d.IsFestivo = true;
                }
                dias.Add(d);
            }
            DiasFestivosDataRepository festRep = new DiasFestivosDataRepository();
            festRep.RegistrarFestivosEnMes(SessionManager.IdEmpresa, Convert.ToInt32(_selectedYear), Convert.ToInt32(_selectedMonth), dias);
        }
        

        #endregion
    }
}
