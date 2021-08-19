<%@ Page Title="" Language="C#" MasterPageFile="AdminAgenda.Master" AutoEventWireup="true"
    CodeBehind="AdminTiposCita.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminTipoCitas" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../../Scripts/AdministracionTiposCita.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Tipos de Cita</h1>
    <div class="agenda_menu">
        <ul class="agenda_menu_list">
            <li><a href="#" id="addTipoCitaBtn" class="linkAdd" title="Registrar nuevo tipo de cita">
                Registrar Nuevo Tipo De Cita</a> </li>
        </ul>
        <div style="clear: both">
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjDataSourceTipoCitas" runat="server" SelectMethod="GetTiposCitaByEmpresa"
        TypeName="Mercer.Tpa.Agenda.Web.DataAcess.TiposCitaDataRepository" EnablePaging="True"
        OnSelecting="ObjDataSourceTipoCitas_Selecting" SelectCountMethod="GetTotalTiposCita"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="idEmpresa" Type="Int32" DefaultValue="1" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <div style="width: 500px">
        <asp:GridView ID="grdTipoCitas" runat="server" Height="16px" Width="620px" OnRowCommand="GridTipoCitas_RowCommand"
            AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjDataSourceTipoCitas"
            PageSize="30" CssClass="grid" EmptyDataText="No se han agregado tipos de cita"
            DataKeyNames="Id" OnRowCreated="grdTipoCitas_RowCreated" OnRowDataBound="grdTipoCitas_RowDataBound">
            <RowStyle CssClass="norItems" />
            <Columns>
                <asp:TemplateField HeaderText="Activo" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTipoCitaActiva" runat="server"></asp:Label>
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
                        <a class="actualizarTipoCita" href="#" idtipocita="<%#Eval("Id")%>" nombretipocita="<%#HttpUtility.HtmlEncode((string)Eval("Name"))%>"
                            duraciontipocita="<%#Eval("Duration")%>">Editar</a> <a class="eliminarTipoCita" href="#"
                                idtipocita="<%#Eval("Id")%>">Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Tipo De Cita" HtmlEncode="true" ItemStyle-CssClass="columnaGrid"
                    ItemStyle-Wrap="true" SortExpression="Name" />
                <asp:BoundField DataField="Duration" HeaderText="Duración" ItemStyle-CssClass="columnaGrid"
                    ItemStyle-Wrap="true" SortExpression="Duration">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="headerGrid" />
            <AlternatingRowStyle CssClass="altItems" />
        </asp:GridView>
    </div>
    <div id="dialogoBorrarTiposCita" title="Eliminar Tipo Cita">
        <div>
            <label>
                ¿Está seguro?</label>
            <input type="hidden" name="idTipoCitaABorrar" class="idTipoCitaABorrar" id="idTipoCitaABorrar" />
            <div>
                <asp:Button ID="btnEliminar" runat="server" Text="Si" OnClick="BtnEliminar_Click"
                    CssClass="button" />
                <asp:Button ID="btnCancelarEliminar" CssClass="BtnCerrarDialogoTipoCita button" runat="server"
                    Text="Cancelar" />
            </div>
        </div>
    </div>
    <div id="dialogoTiposCita" title="Tipo de cita">
        <div>
            <fieldset class="FieldSet">
                <div class="campoFormulario">
                    <label>
                        Nombre<span class="textRed">*</span>
                    </label>
                    <input type="text" maxlength="50" name="nombreTipoCita" class="name_dialogoTiposCita textBox"
                        id="nombreTipoCita" />
                </div>
                <input type="hidden" name="idTipoCita" class="id_dialogoTiposCita" id="idTipoCita" />
                <div class="campoFormulario">
                    <label>
                        Duración (Minutos)<span class="textRed">*</span></label>
                    <input type="text" maxlength="3" name="duracionTipoCita" class="duracion_dialogoTipoCita textBox"
                        id="duracionTipoCita" />
                </div>
                <asp:Button ID="btnGuardarTipoCita" runat="server" Text="Aceptar" OnClick="BtnGuardarCita_Click"
                    OnClientClick="return validarCamposCita();" CssClass="button" />
            </fieldset>
        </div>
    </div>
</asp:Content>
