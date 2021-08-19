<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlHorarioDia.ascx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.ControlHorarioDia" %>
<div>
    <asp:Label ID="LabelDiaSemana" runat="server" Text=""></asp:Label> 
</div>
<div>
        <asp:Label ID="LabelFechaDia" runat="server" Text=""></asp:Label>   
</div>

<asp:DataList ID="DataListHorarioDia" runat="server" 
    onitemdatabound="DataListHorarioDia_ItemDataBound" Width="110px" 
    CaptionAlign="Top">
    <ItemStyle BorderColor="#003399" BorderStyle="Solid" BorderWidth="1px" 
        CssClass="intervaloHorario" VerticalAlign="Top" />
    <SelectedItemStyle BorderStyle="Solid" />
    <ItemTemplate>
    <div class="rowHorarioMedico">
        <asp:Label ID="LabelSede" runat="server" CssClass="labelSedeHorarioMedico"></asp:Label>
        <br />
        <asp:Label ID="LabelHoraInicio" runat="server" CssClass="labelHoraHorarioMedico"></asp:Label>
        <br />
        <asp:Label ID="LabelHoraFin" runat="server" CssClass="labelHoraHorarioMedico"></asp:Label>
        <a runat="server" ID="_btnEliminarIntervalo" href="#" class="btnEliminarIntervalo">
            <table>
                <tr>
                    <td style="width:16px"><img style="border:none" src="../../Content/images/close_16.gif"/></td>
                    <td><span style="float:left">Eliminar</span></td>
                </tr>
            </table>           
        </a>
        <div style="clear:both"></div>
    </div>
    </ItemTemplate>
</asp:DataList>
<div class="dayOptions">

</div>