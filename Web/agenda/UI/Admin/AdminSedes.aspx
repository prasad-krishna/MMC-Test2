<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="AdminAgenda.Master"
    CodeBehind="AdminSedes.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminSedes" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../../Scripts/AdministracionSedes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Administración de sedes</h1>
    <div class="agenda_menu">
        <ul class="agenda_menu_list">
            <li><a href="#" id="addSedeBtn" class="linkAdd" title="Registrar una nueva sede">Registrar
                Nueva Sede</a> </li>
            <li><a href="UsuariosSedes.aspx">Configurar Sedes asociadas a usuarios</a></li>
        </ul>
        <div style="clear: both">
        </div>
    </div>
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <div>
        <asp:GridView ID="grdSedes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataSourceID="ObjDataSourceSedes" Width="599px" 
            OnRowCommand="GridSedes_RowCommand" PageSize="30" CssClass="grid" AlternatingRowStyle-CssClass="altItems"
            RowStyle-CssClass="norItems" HeaderStyle-CssClass="headerGrid" DataKeyNames="Id"
            OnRowCreated="grdSedes_RowCreated" onrowdatabound="grdSedes_RowDataBound">
            <RowStyle CssClass="norItems"></RowStyle>
            <Columns>
                <asp:TemplateField HeaderText="Activa" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSedeActiva" runat="server"></asp:Label>
                    </ItemTemplate><ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Button ID="btnDesactivar" CssClass="button" CommandName="Desactivar" runat="server"
                            Text="Desactivar" />
                        <asp:Button ID="btnActivar" CssClass="button" CommandName="Activar" runat="server"
                            Text="Activar"  />
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opciones">
                    <ItemTemplate>
                        <a class="actualizarSede" href="#" idsede="<%#Eval("Id")%>" nombresede="<%# HttpUtility.HtmlEncode((string)Eval("Nombre"))%>"
                            descripcionsede="<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Descripcion"))%>">Editar</a>
                        <a class="eliminarSede" href="#" idsede="<%#Eval("Id")%>">Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" 
                    HtmlEncode="true" >
                <ItemStyle CssClass="columnaGrid" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" 
                    SortExpression="Descripcion" HtmlEncode="true" ItemStyle-Wrap="true" >
                    <ItemStyle Wrap="True" CssClass="columnaGrid"></ItemStyle>
                </asp:BoundField>

            </Columns>
            <HeaderStyle CssClass="headerGrid"></HeaderStyle>
            <AlternatingRowStyle CssClass="altItems"></AlternatingRowStyle>
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjDataSourceSedes" runat="server" SelectMethod="GetSedesByEmpresaUser"
        TypeName="Mercer.Tpa.Agenda.Web.DataAcess.SedesDataRepository" EnablePaging="True"
        SelectCountMethod="GetTotalSedesByEmpresaUser" OnSelecting="ObjDataSourceSedes_Selecting"
        SortParameterName="sortExpression" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="idEmpresa" Type="Int32" />
            <asp:Parameter Name="idUser" Type="Int32" DefaultValue="1" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="dialogoBorrarSedes" title="Eliminar Sede">
        <div>
            <p>
                Esta seguro?</p>
            <input type="hidden" name="idSedeABorrar" class="idSedeABorrar" id="idSedeABorrar" />
            <asp:Button ID="btnEliminar" runat="server" Text="Si" OnClick="BtnEliminar_Click"
                CssClass="button" />
            <asp:Button ID="btnCancelarEliminar" CssClass="BtnCerrarDialogoSede button" runat="server"
                Text="Cancelar" />
        </div>
    </div>
    <div id="dialogoSedes" title="Sede">
        <div>
            <fieldset class="FieldSet">
                <legend>Detalles de la Sede</legend>
                <div class="campoFormulario">
                    <label>
                        Nombre Sede<span class="textRed">*</span></label>
                    
                    <input type="text" maxlength="200" name="nombreSede" class="name_dialogoSedes textBox" id="nombreSede" />
                </div>
                <div class="campoFormulario">
                    <label>
                        Descripción<span class="textRed">*</span></label>
                    <textarea name="descripcionSede" maxlength="1000" class="descripcion_dialogoSedes textBox" id="descripcionSede"
                        rows="6"></textarea>
                </div>
            </fieldset>
            <input type="hidden" name="idSede" class="id_dialogoSedes" id="idSede" />
            <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" OnClick="btnGuardar_Click"
                OnClientClick="return validarParametrosSede();" CssClass="button" />
        </div>
    </div>
</asp:Content>
