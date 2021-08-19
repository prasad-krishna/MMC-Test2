<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CitasMedicoGrid.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.CitasMedico.CitasMedicoGrid" %>
<%@ Import Namespace="Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<%@ Register Src="../BusquedaCitas/ConvencionesEstados.ascx" TagName="ConvencionesEstados"
    TagPrefix="uc2" %>
<%@ Register Src="../Notificaciones/Notificador.ascx" TagName="Notificador" TagPrefix="uc3" %>
<uc3:Notificador ID="Notificador1" NumResults="30" runat="server" />

    <table id="tablaConvenciones">
        <tbody>
            <tr>
                <td>
                    <div class="estadoCitaPendiente">
                    </div>
                </td>
                <td>
                    Cita pendiente
                </td>

                <td>
                    <div class="estadoCitaFinalizada">
                    </div>
                </td>
                <td>
                    Cita finalizada
                </td>

                <td>
                    <div class="estadoCitaEspera">
                    </div>
                </td>
                <td>
                    El paciente esta esperando
                </td>
            </tr>
        </tbody>
    </table>

<div class="contenedorCitasGrid">
    <asp:GridView ID="GridViewCitasMedico" runat="server" AutoGenerateColumns="False"
        DataSourceID="ObjectDataSourceGrid" OnRowDataBound="GridViewCitasMedico_RowDataBound"
        OnSelectedIndexChanged="GridViewCitasMedico_SelectedIndexChanged" Height="16px"
        Width="100%" CssClass="grid" RowStyle-CssClass="norItems" AlternatingRowStyle-CssClass="altItems"
        EmptyDataText="No hay citas pendientes por esta semana.">
        <RowStyle CssClass="norItems"></RowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Menu">
                <ItemTemplate>
                    <a class="menuGrid linkMenuCitas" dia='<%#Eval("StartDate",ConfiguracionAgendaManager.FormatoFechaGrid)%>'
                        href="#" idcita='<%#Eval("Id")%>' nombrepaciente='<%#HttpUtility.HtmlEncode((string)Eval("NombrePaciente"))%>'
                        idempleado="<%#Eval("IdEmpleado")%>" idbeneficiario="<%#Eval("IdBeneficiario")%>" estado="<%#Eval("EstadoCita") %>">
                        <img style="border:none" src="../Content/images/icoMenu.gif"/>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nombreEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="NombreSede" HeaderText="Sede" />
            <asp:BoundField DataField="FormatoDia" HeaderText="Fecha" />
            <asp:BoundField DataField="FormatoHoras" HeaderText="Hora" />
            <asp:BoundField DataField="NombrePaciente" HeaderText="Paciente" />
            <asp:BoundField DataField="NotasAdicionales" HeaderText="Notas Adicionales" ItemStyle-CssClass="columnaGrid" />
            <asp:TemplateField>
                <ItemTemplate>
                    <img style="border:none" src="../Content/images/<%#Eval("EstadoCita")%>.gif"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="headerGrid" />
        <AlternatingRowStyle CssClass="altItems"></AlternatingRowStyle>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="GetCitasMedico"
        TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository" OnSelecting="ObjectDataSourceGrid_Selecting"
        >
        <SelectParameters>
            <asp:Parameter Name="idMedico" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
</div>
