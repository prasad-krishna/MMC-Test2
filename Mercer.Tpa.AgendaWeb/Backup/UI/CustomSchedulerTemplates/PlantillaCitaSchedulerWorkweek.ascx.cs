using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Drawing;

namespace Mercer.Tpa.Agenda.Web.UI.CustomSchedulerTemplates
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Plantailla personalizada para mostrar las citas en la vista diaria
    /// </summary>
    public partial class PlantillaCitaSchedulerWorkweek : System.Web.UI.UserControl
    {
        #region Variables privadas
        VerticalAppointmentTemplateContainer Container { get { return (VerticalAppointmentTemplateContainer)Parent; } }
        VerticalAppointmentTemplateItems Items { get { return Container.Items; } }


        #endregion

        #region Eventos






        protected void Page_Load(object sender, EventArgs e)
        {
            appointmentDiv.Style.Value = Items.AppointmentStyle.GetStyleAttributes(Page).Value;
            lblTitle.ControlStyle.MergeWith(Items.Title.Style);
            statusContainer.Controls.Add(Items.StatusControl);
            LayoutAppointmentImages();
            /*Asignar toolip*/
            var strTooltip = new StringBuilder();
            strTooltip.AppendLine(Container.Items.StartTimeText.Text + Container.Items.EndTimeText.Text);
            strTooltip.AppendLine("Duración (Minutos):" + Container.AppointmentViewInfo.AppointmentInterval.Duration.TotalMinutes);
            strTooltip.AppendLine("Paciente: " + Container.AppointmentViewInfo.Appointment.Subject);
            strTooltip.AppendLine("Sede: " + Container.AppointmentViewInfo.Appointment.Location);
            strTooltip.AppendLine("Notas: " + Container.AppointmentViewInfo.Appointment.Description);
            appointmentDiv.Attributes["title"] = HttpUtility.HtmlAttributeEncode(strTooltip.ToString());


        }

        #endregion

        #region Métodos privados

        void LayoutAppointmentImages()
        {
            int count = Items.Images.Count;
            for (int i = 0; i < count; i++)
            {
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                AddImage(cell, Items.Images[i]);
                row.Cells.Add(cell);
                imageContainer.Rows.Add(row);
            }
        }
        void AddImage(HtmlTableCell targetCell, AppointmentImageItem imageItem)
        {
            Image image = new Image();
            imageItem.ImageProperties.AssignToControl(image, false);
            targetCell.Controls.Add(image);
        }
        #endregion
    }
}