<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaMedico.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.AgendaMedico" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1" Namespace="DevExpress.Web.ASPxScheduler"
    TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v10.1.Core" Namespace="DevExpress.XtraScheduler"
    TagPrefix="dx" %>
<%@ Register Src="Notificaciones/Notificador.ascx" TagName="Notificador" TagPrefix="uc1" %>
<%@ Register Src="CitasMedico/CitasMedicoGrid.ascx" TagName="CitaGrid" TagPrefix="citaGrid" %>
<%@ Register Src="CitasMedico/CitasMedicoScheduler.ascx" TagName="CitaScheduler"
    TagPrefix="citaScheduler" %>
<%@ Register src="Utils/ControlMensajeError.ascx" tagname="ControlMensajeError" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link href="../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />--%>
    <link href="../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/ui.achtung.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../css/admon.css" />
    <link href="../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />
    <link href="../Content/notificaciones.css" type="text/css" rel="Stylesheet" />
    <link href="../Content/agenda.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.custom.min.js"></script>

    <script type="text/javascript" src="../Scripts/jquery.ui.datepicker-es.js"></script>

    <script type="text/javascript" src="../Scripts/ui.achtung-min.js"></script>

    <script type="text/javascript" src="../Scripts/common.js"></script>

    <script type="text/javascript" src="../Scripts/AgendaMedico.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1 class="tituloAgenda">
            Agenda
        </h1>
        <a href="#" id="linkCerrarAlertas" style="display:none;">Cerrar alertas</a>
        <div id="panelTabsAgenda">
            
            <asp:LinkButton ID="btnProximasCitas" runat="server" 
                onclick="btnProximasCitas_Click">Próximas citas</asp:LinkButton>
            <asp:LinkButton ID="btnVerAgenda" runat="server" onclick="btnVerAgenda_Click">Ver agenda</asp:LinkButton>
            <a href="BusquedaCitas/BusquedaCitas.aspx">Buscar citas</a>                       
        </div>
        <uc2:ControlMensajeError ID="ctrError" runat="server" />
        <citaScheduler:CitaScheduler ID="CtrlCitasScheduler" runat="server" 
            Visible="False" />
           
        <citaGrid:CitaGrid ID="CtrlCitasGrid" runat="server" />
        <div id="divAccionesCita" class="menuContextualGrid">
            <a href="#" class="linkCerrarMenu"><img style="border:none" src="../Content/images/imgClose.gif" /></a>
            <ul>
                <li id="liRegistrar"><a href="#" id="regConsulta">Registrar consulta</a></li>
                <li id="sinAcciones"><span>No hay acciones disponibles.</span></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
