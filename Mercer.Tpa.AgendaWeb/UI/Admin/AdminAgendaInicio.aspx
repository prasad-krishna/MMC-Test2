<%@ Page Title="" Language="C#" MasterPageFile="AdminAgenda.Master" AutoEventWireup="true" CodeBehind="AdminAgendaInicio.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminAgendaInicio" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<%@ Register src="../Utils/UserControlMessage.ascx" tagname="UserControlMessage" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Parámetros de Agenda
    </h1>
    
    <uc2:UserControlMessage ID="ctrMensaje" runat="server" />
   <uc1:ControlMensajeError ID="ctrError" runat="server" />
   
   <fieldset class="FieldSet" style="width:200px;padding:10px">
    <legend>Modificación de citas</legend>
        <div class="campoFormulario">
            <label>Tiempo límite (Horas)</label>
            <span class="textRed">*</span>

            <asp:TextBox ID="txtHorasLimite" MaxLength="4" CssClass="textBox" runat="server" ToolTip="No será posible cancelar o reprogramar una cita antes del valor establecido"></asp:TextBox>
        </div>
       <asp:Button ID="btnGuardarParametros" CssClass="button" runat="server" 
           Text="Guardar" onclick="btnGuardarParametros_Click" />
   </fieldset>
</asp:Content>
