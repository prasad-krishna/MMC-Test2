using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Drawing;

public partial class HorizontalAppointmentTemplateCustom : System.Web.UI.UserControl {
	HorizontalAppointmentTemplateContainer Container { get { return (HorizontalAppointmentTemplateContainer)Parent; } }
	HorizontalAppointmentTemplateItems Items { get { return Container.Items; } }

	protected void Page_Load(object sender, EventArgs e) {
		appointmentDiv.Style.Value = Items.AppointmentStyle.GetStyleAttributes(Page).Value;

        lblTitle.ControlStyle.MergeWith(Items.Title.Style);
        lblStartContinueText.ControlStyle.MergeWith(Items.StartContinueText.Style);
        lblEndContinueText.ControlStyle.MergeWith(Items.EndContinueText.Style);
 

		LayoutAppointmentImages();

		statusContainer.Controls.Add(Items.StatusControl);
		startTimeClockContainer.Controls.Add(Items.StartTimeClock);
		endTimeClockContainer.Controls.Add(Items.EndTimeClock);
        /*Asignar toolip*/
        var strTooltip = new StringBuilder();
        strTooltip.AppendLine(Container.Items.StartTimeText.Text + Container.Items.EndTimeText.Text);
        strTooltip.AppendLine("Duración (Minutos):" + Container.AppointmentViewInfo.AppointmentInterval.Duration.TotalMinutes);
        strTooltip.AppendLine("Paciente: " + Container.AppointmentViewInfo.Appointment.Subject);
        strTooltip.AppendLine("Sede: " + Container.AppointmentViewInfo.Appointment.Location);
        strTooltip.AppendLine("Notas: " + Container.AppointmentViewInfo.Appointment.Description);
        appointmentDiv.Attributes["title"] = HttpUtility.HtmlAttributeEncode(strTooltip.ToString());
	}
	void LayoutAppointmentImages() {		
		int count = Items.Images.Count;
		HtmlTableRow row = new HtmlTableRow();
		row.Cells.Add(new HtmlTableCell());
		for (int i = 0; i < count; i++) {
			HtmlTableCell cell = new HtmlTableCell();
			AddImage(cell, Items.Images[i]);
			row.Cells.Add(cell);
		}
		imageContainer.Rows.Add(row);

		Items.StartContinueArrow.ImageProperties.AssignToControl(imgStartContinueArrow, false);
		Items.EndContinueArrow.ImageProperties.AssignToControl(imgEndContinueArrow, false);
	}
	void AddImage(HtmlTableCell targetCell, AppointmentImageItem imageItem) {
		Image image = new Image();
		imageItem.ImageProperties.AssignToControl(image, false);
		targetCell.Controls.Add(image);
	}

}
