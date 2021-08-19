<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HorarioMedicoInicio.aspx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.HorarioMedicoInicio" %>

<%@ Register src="../Utils/ControlMensajeError.ascx" tagname="ControlMensajeError" tagprefix="uc1" %>

<%@ Register src="../Utils/UserControlMessage.ascx" tagname="UserControlMessage" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuración horario</title>
    <link href="../../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../css/admon.css" />
    <link href="../../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Consulta y modificación de horario de médico.
    </h1>
    <uc2:UserControlMessage ID="ctrMensaje" runat="server" />
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <div>
         <label>Por favor seleccione el médico</label>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelected="ObjectDataSource1_Selected"
            OnSelecting="ObjectDataSource1_Selecting" SelectMethod="GetCitasBusqueda" TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository"
            EnablePaging="True" SelectCountMethod="GetTotalCitasBusqueda">
            <SelectParameters>
                <asp:Parameter Name="idSede" Type="Int32" />
                <asp:Parameter Name="idMedico" Type="Int32" />
                <asp:Parameter Name="recordatorio" Type="Int32" />
                <asp:Parameter Name="estado" Type="Int32" />
                <asp:Parameter Name="identificacion_empleado" Type="Int32" />
                <asp:Parameter Name="identificacion_paciente" Type="Int32" />
                <asp:Parameter Name="idEmpleado" Type="Int32" />
                <asp:Parameter Name="idPaciente" Type="Int32" />
                <asp:Parameter Name="nombrePaciente" Type="String" />
                <asp:Parameter Name="sortExpression" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div class="busquedaPanels" style="height:10px">
            <table style="height: 9px">
                <tbody>
                    <tr>
                        <td>
                            <label class="labelFiltro">
                                Especialidad<span class="textRed">*</span></label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dbcEspecialidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBoxEspecialidad_SelectedIndexChanged"
                                Width="248px" OnDataBound="ListBoxEspecialidad_DataBound"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="labelFiltro">
                                Médico<span class="textRed">*</span></label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dbcMedicos" runat="server"  Width="248px" OnDataBound="ListBoxMedicos_DataBound">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnBuscar" CssClass="bigButton" runat="server" Text="Configurar horario" OnClick="btnBuscar_Click" /></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>            
        </div>    
    
    </div>

    </form>
</body>
</html>
