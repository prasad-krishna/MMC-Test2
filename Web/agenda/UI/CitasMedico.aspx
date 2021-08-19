<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitasMedico.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.CitasMedico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../../css/admon.css" />
    <link href="../../Content/agenda.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.min.js"></script>

    <!-- Includes necesarios para grid jqGrid -->

    <script src="../../Scripts/jqGrid/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Fin dependencias jqGrid -->

    <script type="text/javascript" src="../../Scripts/json2.js"></script>

    <script type="text/javascript" src="../../Scripts/CitasMedico.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridViewCitasMedico" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataSourceID="ObjectDataSourceCitasMedico" 
            onrowdatabound="GridView1_RowDataBound" 
    PageSize="2">
                    <Columns>
                        <asp:TemplateField HeaderText="Menu">
                                <ItemTemplate>
                                    <a class="seleccionCita" href="#" idcita="<%#Eval("cita_id")%>" dia="<%#Eval("start_date_")%>"
                                        nombrepaciente="<%#Eval("nombrePaciente")%>">xxxx</a></ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="nombrePaciente" HeaderText="Nombre paciente" />
                        <asp:BoundField DataField="start_date_" HeaderText="Dia" />
                        <asp:BoundField DataField="end_date" HeaderText="Hora" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridViewCitasMedico" 
                    EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    
        <asp:ObjectDataSource ID="ObjectDataSourceCitasMedico" runat="server" 
            EnablePaging="True" onselecting="ObjectDataSourceCitasMedico_Selecting" 
            SelectCountMethod="GetTotalCitasBusqueda" SelectMethod="GetCitasBusqueda" 
            TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository">
            <SelectParameters>
                <asp:Parameter Name="idSede" Type="Int32" />
                <asp:Parameter Name="idMedico" Type="Int32" />
                <asp:Parameter Name="recordatorio" Type="Int32" />
                <asp:Parameter Name="estado" Type="Int32" />
                <asp:Parameter Name="sortExpression" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
