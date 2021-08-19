<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaInicio.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.AgendaInicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Módulo Agenda</title>
    <link type="text/css" rel="Stylesheet" href="../../css/admon.css" />
    <link type="text/css" rel="Stylesheet" href="../Content/agenda.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <!-- Estos controles los puede ver el asistente -->
            <h1>Agenda</h1>
            <div class="agenda_menu">
            <ul class="agenda_menu_list">
               <li>
                   <a href="BusquedaCitas/BusquedaCitas.aspx" class="menu-buscar">Busqueda de citas</a>
               </li>
            </ul>
            <div style="clear:both" />
        </div>
    </div>
    </form>
</body>
</html>
