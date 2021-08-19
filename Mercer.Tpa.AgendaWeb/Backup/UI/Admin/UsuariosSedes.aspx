<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="AdminAgenda.Master"
    CodeBehind="UsuariosSedes.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.UsuariosSedes" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
    <%@ Register Src="../Utils/UserControlMessage.ascx" TagName="UserControlMessage"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../../Scripts/AdministracionSedes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Asociación de usuarios y sedes</h1>

    <div>
        <uc1:ControlMensajeError ID="ctrError" runat="server" />
        <uc2:UserControlMessage ID="ctrMensaje" runat="server" />
        <asp:ObjectDataSource ID="ObjectDataSourceUsuarios" runat="server" OnSelecting="ObjectDataSourceUsuarios_Selecting"
            SelectMethod="GetUsuariosEmpresa" TypeName="Mercer.Tpa.Agenda.Web.DataAcess.UsuariosDataRepository">
            <SelectParameters>
                <asp:Parameter DefaultValue="" Name="idEmpresa" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceSedes" runat="server" OnSelected="ObjectDataSourceSedes_Selected"
            OnSelecting="ObjectDataSourceSedes_Selecting" SelectMethod="GetSedesActivasByEmpresaUser"
            TypeName="Mercer.Tpa.Agenda.Web.DataAcess.SedesDataRepository">
            <SelectParameters>
                <asp:Parameter Name="idEmpresa" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:DropDownList ID="dbcUsuarios" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceUsuarios"
            DataTextField="NombreLogin" DataValueField="Id" OnDataBound="DropDownListUsuarios_DataBound"
            OnSelectedIndexChanged="dbcUsuarios_SelectedIndexChanged">
        </asp:DropDownList>
        <div id="contenedorListaSedes" style="width:400px;height:200px;overflow:auto;">
            <asp:CheckBoxList ID="chkListaSedes" runat="server" DataSourceID="ObjectDataSourceSedes"
                DataTextField="Nombre" DataValueField="Id" OnDataBound="CheckBoxListSedes_DataBound"                 >
            </asp:CheckBoxList>
        </div>
        <asp:Button ID="btnGuardar" CssClass="button" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
    </div>

</asp:Content>
