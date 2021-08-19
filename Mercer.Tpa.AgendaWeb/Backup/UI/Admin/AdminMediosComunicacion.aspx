<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="AdminAgenda.Master"
    CodeBehind="AdminMediosComunicacion.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminMediosComunicacion" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../../Scripts/AdministracionMedios.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Medios de contacto de pacientes.
    </h1>
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <div class="agenda_menu">
        <ul class="agenda_menu_list">
            <li><a href="#" id="addMedioBtn" class="linkAdd" title="Registrar nuevo medio de comunicación">
                Nuevo medio de contacto</a> </li>
        </ul>
        <div style="clear: both">
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSourceMedios" runat="server" SelectMethod="GetAll"
        TypeName="Mercer.Tpa.Agenda.Web.DataAcess.MediosComunicacionDataRepository" EnablePaging="True"
        OnSelecting="ObjectDataSourceMedios_Selecting" SelectCountMethod="GetTotalMedios">
        <SelectParameters>
            <asp:Parameter Name="idEmpresa" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="grdMedios" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceMedios"
        CssClass="grid" AllowPaging="True" HeaderStyle-CssClass="headerGrid" RowStyle-CssClass="norItems"
        AlternatingRowStyle-CssClass="altItems" Width="380px" DataKeyNames="Id" OnRowCommand="grdMedios_RowCommand"
        OnRowCreated="grdMedios_RowCreated" OnRowDataBound="grdMedios_RowDataBound" PageSize="30">
        <RowStyle CssClass="norItems"></RowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Activo" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblMedioActiva" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button ID="btnDesactivar" CssClass="button" CommandName="Desactivar" runat="server"
                        Text="Desactivar" />
                    <asp:Button ID="btnActivar" CssClass="button" CommandName="Activar" runat="server"
                        Text="Activar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Opciones">
                <ItemTemplate>
                    <a class="actualizarMedio" href="#" idmedio="<%#Eval("Id")%>" nombremedio="<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Nombre"))%>">
                        Editar</a> <a class="eliminarMedio" href="#" idmedio="<%#Eval("Id")%>">Eliminar</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre Medio" SortExpression="Nombre"
                HtmlEncode="true" ItemStyle-CssClass="columnaGrid" ItemStyle-Wrap="true" />
        </Columns>
        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
        <AlternatingRowStyle CssClass="altItems"></AlternatingRowStyle>
    </asp:GridView>
    <div id="dialogoBorrarMedios" title="Eliminar Medio">
        <input type="hidden" name="idMedioABorrar" class="idMedioABorrar" id="idMedioABorrar" />
        <div class="campoFormulario">
            <label>
                Está seguro?</label>
        </div>
        <div class="campoFormulario">
            <asp:Button ID="btnEliminar" CssClass="BtnEliminarDialogoMedio button" runat="server"
                Text="Eliminar" OnClick="BtnEliminar_Click" />
            <asp:Button ID="btnCerrarDialogo" CssClass="BtnCerrarDialogoMedio button" runat="server"
                Text="No eliminar" /></div>
    </div>
    <div id="dialogoMedio" title="Medio">
        <fieldset class="FieldSet">
            <legend></legend>
            <input type="hidden" name="idMedio" class="id_dialogoMedio" id="idMedio" />
            <div class="campoFormulario">
                <label>
                    Nombre Medio<span class="textRed">*</span></label>
                <input type="text" maxlength="100" name="nombreMedio" class="name_dialogoMedio textBox"
                    id="nombreMedio" />
            </div>
            <div class="campoFormulario">
                <asp:Button ID="btnAddMedio" runat="server" Text="Aceptar" CssClass="button" OnClick="btnGuardarMedio_Click"
                    OnClientClick="return validarParametros();" />
            </div>
        </fieldset>
    </div>
</asp:Content>
