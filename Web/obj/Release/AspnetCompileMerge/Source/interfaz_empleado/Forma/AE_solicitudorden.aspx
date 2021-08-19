<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarServicioProducto" Src="../WebControls/WC_BuscarServicioProducto.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_RegistrarUVR" Src="../WebControls/WC_RegistrarUVR.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnostico" Src="../WebControls/WC_AdicionarDiagnostico.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarPrestador" Src="../WebControls/WC_AdicionarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarSolicitante" Src="../WebControls/WC_AdicionarSolicitante.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_SeleccionarDiagnostico" Src="../WebControls/WC_SeleccionarDiagnostico.ascx" %>

<%@ Page Language="c#" CodeBehind="AE_solicitudorden.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.interfaz_empleado.forma.AE_solicitudorden" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>

    <link href="../../css/Calendar.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Scripts para control de sesión -->
    <script src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.countdown.js" type="text/javascript"></script>
    <script src="../../scripts/ControlSesion.js" type="text/javascript"></script>
    <!-- Fin - Emilio Bueno 20/11/2012 -->
    <style type="text/css">
        .textBoxOculto {
            display: none !important;
        }
    </style>

</head>
<body leftmargin="5" topmargin="1" onload="CargarConfiguracion(); HabilitarValidacion(<%=Session["idTipoConsulta"].ToString() %>)" rightmargin="5">
    <form id="Form1" method="post" runat="server">
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Controles para mensaje de control de sesión -->
    <div style="display: none;">
        <asp:HiddenField ID="hdnTimeout" runat="server" />
        <asp:HiddenField ID="hdnSesion" runat="server" />
        <span id="shortly" style="display: none;"></span>
        <asp:HiddenField ID="hdnBotonClick" Value="lnkGuardar" runat="server" />
        <asp:HiddenField ID="hdnTiempoMostrarAlerta" runat="server" />
        <asp:HiddenField ID="hdnTiempoGuardarTemporal" runat="server" />
    </div>
    <div id="divCodigoCS">
    </div>
    <!-- Fin - Emilio Bueno 20/11/2012 -->

    <asp:ScriptManager ID="scrMng" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">

		    if (typeof window.event != 'undefined')
		        document.onkeydown = function() {
		            var test_var = event.srcElement.tagName.toUpperCase();
		            var test_id = event.srcElement.id;
		            if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
		                return (event.keyCode != 8);
		        }
		    else
		        document.onkeypress = function(e) {
		            var test_var = e.target.nodeName.toUpperCase();
		            var test_id = e.target.id;
		            if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
		                return (e.keyCode != 8);
		        }
   				
   			function BorrarItem(image)
			{
				var tr = image.parentNode.parentNode;				
				var inputs = tr.getElementsByTagName('input'); 
				for (var j = 0; j < inputs.length; j++)
				{
					if(inputs[j].getAttribute('type') == 'text' && inputs[j].getAttribute('id').indexOf('txtIdTipoServicio') == -1 && inputs[j].getAttribute('id').indexOf('txtIdSolicitudServicio') == -1) 
						inputs[j].value = "";	
				}				
				var inputs = tr.getElementsByTagName('textarea');				
				for (var j = 0; j < inputs.length; j++)
				{
					if(inputs[j].getAttribute('id').indexOf('txtIdTipoServicio') == -1 && inputs[j].getAttribute('id').indexOf('txtIdSolicitudServicio') == -1) 
						inputs[j].value = "";	
				}		
			}
			
			function CalcularDias()
			{					
				if(document.getElementById('txtInicioIncapacidad').value != "" && document.getElementById('txtFinIncapacidad').value != "")
				{
					var ONE_DAY = 1000 * 60 * 60 * 24;
					var dateBits = document.getElementById('txtInicioIncapacidad').value.split('/');
					var txtInicioIncapacidad = new Date(dateBits[1] + '/' + dateBits[0] + '/' + dateBits[2] + ' 00:00:00');
					dateBits = document.getElementById('txtFinIncapacidad').value.split('/');
					var txtFinIncapacidad = new Date(dateBits[1] + '/' + dateBits[0] + '/' + dateBits[2] + ' 00:00:00');
					var txtDias = document.getElementById('txtDias');
					var dias = Math.ceil(txtFinIncapacidad - txtInicioIncapacidad);
					txtDias.value = Math.round(dias/ONE_DAY) + 1;
				}
			
			}
			
			function BloquearValorAprobado(txtValorAprobado, checkBox)
			{	
				if(checkBox.checked)		
				{						
					txtValorAprobado.disabled=false;	
				}
				else
				{
					txtValorAprobado.value = "";
					txtValorAprobado.disabled=true;					
				}
			}
			
			function CambiarPaciente(idEmpleado, idBeneficiario)
			{	
				
				if(idEmpleado != "" && idEmpleado != "0")
				{
					var txtIdEmpleado = document.getElementById('txtOtroEmpleado');
					txtIdEmpleado.value = idEmpleado;
				}
				if(idBeneficiario != "" && idBeneficiario != "0")
				{
					var txtIdBeneficiario = document.getElementById('txtOtroBeneficiario');
					txtIdBeneficiario.value = idBeneficiario;
				}					
				__doPostBack('lnkCargarNuevoPaciente', '');
			
			}
		
		
			
		function dobleClickSubmint()
        {              
            document.getElementById('lblCargandoConsulta1').innerHTML = 'Cargando Órdenes';            
//            document.getElementById('btnGuardar').style.display = "none";
//            document.getElementById('btnCancelar').style.display = "none";
            document.getElementById('trBotones').style.display = "none";            
            scroll();       
        }
        
        var msg = "||||||||||||||||||||"
            var x = ""
            num = 1
            toggle = 1
            tt = 1
            OOK = 0
            timval = 60
            var tval = ""
            function fasl() { timval = (timval==60 ? 150 : 60 ) }

            function csd() {
               tt = ( tt==1 ? 0 : 1)
            }
            function scroll() {
              if (tt==1)
                if (num <= msg.length)
                  OOK = 1
              if (tt==0)
                if (num >= 0)
                  OOK = 1

              if ( OOK == 1)
             {
             OOK = 0
             x = msg.substring(0,num)
             document.getElementById('lblCargandoConsulta').innerHTML = x;
             num = ( tt==1 ? num+1 : num-1 )
             }
              else
             {
             x = ""
             document.getElementById('lblCargandoConsulta').innerHTML = "";
             num = (tt==1 ? 0 : msg.length )
             }
              if (toggle == 1)
             setTimeout("scroll()", timval)
            }
            function toggla() {
             toggle = (toggle == 1 ? 0 : 1 )
             if (toggle == 1) scroll()
            }
            
			
			function ShowPrestadorEspecifico(sender, idDiv)
			{
				var div = document.getElementById(idDiv);	
					
				if(sender.checked)
				{					
					div.style.display = "";
				}
				else
				{
					div.style.display = "none";				
				}					
				
			}
			
			function HabilitarValidacion(checkBox) {
			    var medico = ["1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17"];
			    var nutriologo = ["15", "16"];
			    var todo = ["3", "4"];
			    var tipoConsulta = String(checkBox);
			    var esMedico = $.inArray(tipoConsulta, medico);
			    var esNutriologo = $.inArray(tipoConsulta, nutriologo);
			    var esTodo = $.inArray(tipoConsulta, todo);

			    if (medico >= 0) {
			        $("#Table8").show();
			    }
			    if (esNutriologo >= 0) {
			        $("#Table8").hide();
			    }
			    if (todo >= 0) {
			        $("#Table8").show();
			    }
                }
		    
    </script>

    <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0">
        <tbody>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;
                    <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;
                    <fieldset class="FieldSet" style="width: 100%">
                        <legend>
                            <img src="../../images/icoSolicitud.gif" border="0">
                            &nbsp;Órdenes</legend>
                        <br>
                        <table id="Table1" cellspacing="0" cellpadding="1" width="99%" align="center">
                            <tbody>
                                <tr>
                                    <td>
                                        <table id="tblDatosSolicitud" style="display: none" cellspacing="0" cellpadding="3"
                                            width="90%" align="center" runat="server">
                                            <tr>
                                                <td class="textSmallBlack" width="20%" colspan="4">
                                                    Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                                                    <asp:Label ID="lblNoSolicitud" runat="server" CssClass="titleBig" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    Fecha y Usuario Creación
                                                </td>
                                                <td width="33%">
                                                    <asp:Label ID="lblFechaCreacion" runat="server"></asp:Label><br>
                                                    <asp:Label ID="lblUsuarioCreacion" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                </td>
                                                <td width="35%">
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="Table2" cellspacing="0" cellpadding="3" width="90%" align="center">
                                            <tbody>
                                                <tr>
                                                    <td width="20%" height="20">
                                                        <asp:Label ID="lblPlanesSolicitud" runat="server" Visible="False">Planes</asp:Label>
                                                    </td>
                                                    <td width="33%">
                                                        <asp:DropDownList ID="ddlPlanesSolicitud" runat="server" CssClass="textBox" Visible="False"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="20%">
                                                        <span class="textRed"></span>
                                                    </td>
                                                    <td width="35%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEstado" runat="server" Visible="False">Estado Solicitud</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEstadoSoli" runat="server" CssClass="textBox" Visible="False"
                                                            Width="200px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMotivo" runat="server" Visible="False">Motivo Solicitud</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMotivoSoli" runat="server" CssClass="textBox" Visible="False"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:ImageButton ID="imbAdicionarSolicitud" runat="server" ImageUrl="../../images/icoAdd.gif"
                                            CausesValidation="false"></asp:ImageButton><asp:LinkButton ID="lnkAdicionarSolicitud"
                                                runat="server" CausesValidation="false">Adicionar Nueva Solicitud</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlAdicionarTipoServicio" runat="server" CssClass="textBoxSmall"
                                            Width="40px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:ImageButton ID="imbAdicionarTipoServicio" runat="server" Visible="False" ImageUrl="../../images/icoAdd.gif"
                                            CausesValidation="false"></asp:ImageButton><asp:LinkButton ID="lnkAdicionarTipoServicio"
                                                runat="server" Visible="False" CausesValidation="false">Adicionar Prestador</asp:LinkButton>&nbsp;
                                        <asp:ImageButton ID="imbVerHistorial" runat="server" ImageUrl="../../images/icoHistorial.gif"
                                            CausesValidation="false"></asp:ImageButton><asp:LinkButton ID="lnkVerHistorico" runat="server"
                                                CausesValidation="false">Ver Histórico del Paciente</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imbGuardar" runat="server" ImageUrl="../../iconos/icoGuardar.gif"
                                            CausesValidation="false" Style="width: 21px"></asp:ImageButton>
                                        <asp:LinkButton ID="lnkGuardar" runat="server" CausesValidation="false">Guardar</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imbCopiar" runat="server" CausesValidation="false" ImageUrl="../../images/icoCopiar.gif" />
                                        &nbsp;<asp:LinkButton ID="lnkCopiar" runat="server" CausesValidation="false">Copiar Orden Anterior</asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DataList ID="dtlTipoServicio" runat="server" Width="100%" CellPadding="8">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="Ajaxpanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="divDataList" runat="server">
                                                            <asp:Label ID="lblNuevo" runat="server" CssClass="textBigGreen"></asp:Label>&nbsp;
                                                            <asp:TextBox ID="txtConsecutivoNombre" runat="server" CssClass="textBigGreen"></asp:TextBox>
                                                            <asp:TextBox ID="txtIdSolicitudEstado" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                            <table class="tableBorder" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                                <tr class="headerGrid">
                                                                    <td>
                                                                        Tipo de Servicio<span class="textRed">*</span>
                                                                    </td>
                                                                    <td>
                                                                        Diagnósticos
                                                                    </td>
                                                                    <td>
                                                                        Prestador
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="top">
                                                                    <td width="25%">
                                                                        <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="textBox" Width="160px"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtIdSolicitudTipoServicio" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtConsecutivo" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtImpresiones" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                        <asp:ImageButton ID="imbHistoricoTipoServicio" runat="server" CausesValidation="false"
                                                                            ImageUrl="../../iconos/icoHistorial.gif" CommandName="VerHistorico" AlternateText="Histórico">
                                                                        </asp:ImageButton>
                                                                    </td>
                                                                    <td width="35%">
                                                                        <uc1:WC_SeleccionarDiagnostico ID="WC_SeleccionarDiagnostico2" runat="server"></uc1:WC_SeleccionarDiagnostico>
                                                                    </td>
                                                                    <td width="40%" id="tdPrestadoresControl" runat="server">
                                                                        Enviar a la especialidad:
                                                                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="textBoxSmall" Width="200px">
                                                                        </asp:DropDownList>
                                                                        <br>
                                                                        <br>
                                                                        o enviar al médico/institución específica:
                                                                        <uc1:WC_AdicionarPrestador ID="WC_AdicionarPrestador1" runat="server"></uc1:WC_AdicionarPrestador>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imbBorrar" runat="server" Visible="false" CssClass="ImageTransparent"
                                                                            ImageUrl="../../iconos/ico_borrar.gif" CommandName="Eliminar"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="5">
                                                                        <table id="Table4" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                                            <tr style="display: none">
                                                                                <td class="textNegrita" align="left">
                                                                                    Atención
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTipoAtencion" runat="server" CssClass="textBoxSmall" Width="110px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlClaseAtencion" runat="server" CssClass="textBoxSmall" Width="110px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlContingencia" runat="server" CssClass="textBoxSmall" Width="110px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    Aprobada por:<br>
                                                                                    <asp:DropDownList ID="ddlUnidadAprueba" runat="server" CssClass="textBoxSmall" Width="105px">
                                                                                        <asp:ListItem Value="">--</asp:ListItem>
                                                                                        <asp:ListItem Value="Coordinación de usuarios">Coordinación de usuarios</asp:ListItem>
                                                                                        <asp:ListItem Value="División Médica">División Médica</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td width="40%">
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="trFechaPrestacion" style="display: none">
                                                                                <td colspan="6" class="textNegrita" align="left">
                                                                                    Fecha Prestación Servicios&nbsp;
                                                                                    <asp:TextBox ID="txtFechaPrestacionGeneral" runat="server" Width="70px" CssClass="textBoxSmall"></asp:TextBox>
                                                                                    <asp:Image ID="btnFechaPrestacionGeneral" runat="server" ImageUrl="../../images/icoCalendar.gif">
                                                                                    </asp:Image>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Descuento %&nbsp;
                                                                                    <asp:TextBox ID="txtDescuentoGeneral" onkeypress="return currencyFormat(this,event,true,true)"
                                                                                        runat="server" Width="40px" CssClass="textBoxSmall"></asp:TextBox>
                                                                                    <asp:Image ID="btnDescuento" runat="server" ImageUrl="../../iconos/ico_aceptar.gif">
                                                                                    </asp:Image>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:ImageButton ID="imbAdicionarServicio" runat="server" CausesValidation="false"
                                                                                ImageUrl="../../images/icoAdd.gif" CommandName="Adicionar"></asp:ImageButton>
                                                                            <asp:LinkButton ID="lnkAdicionarServicio" runat="server" CausesValidation="false"
                                                                                CommandName="Adicionar">Adicionar Servicio/Producto</asp:LinkButton>&nbsp;
                                                                            <asp:DropDownList ID="ddlAdicionarServicios" runat="server" CssClass="textBoxSmall"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAdicionarServicios_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                            </table>
                                                            <table class="tableBorderBack" id="tblInternet" runat="server" cellspacing="0" cellpadding="3"
                                                                width="100%" border="0" style="display: none">
                                                                <tr>
                                                                    <td width="20%">
                                                                        Diagnósticos
                                                                    </td>
                                                                    <td width="80%">
                                                                        <asp:Label ID="lblDiagnosticos" runat="server" CssClass="LabelNoModify" Width="450px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="20%">
                                                                        Servicios Solicitados
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:Label ID="lblServicios" runat="server" CssClass="LabelNoModify" Width="450px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:DataGrid ID="dtgProductoServicio" runat="server" CssClass="grid" Width="100%"
                                                                CellPadding="0" CellSpacing="0" OnItemDataBound="dtgProductoServicio_ItemDataBound"
                                                                OnItemCommand="dtgProductoServicio_ItemCommand" GridLines="Horizontal" AutoGenerateColumns="False"
                                                                AllowPaging="False">
                                                                <AlternatingItemStyle CssClass="altItemsSmall"></AlternatingItemStyle>
                                                                <ItemStyle CssClass="norItemsSmall"></ItemStyle>
                                                                <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn HeaderText="Servicio/Producto">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox Width="150px" runat="server" ID="txtProductoServicio" CssClass="textBoxSmall"></asp:TextBox>
                                                                            <asp:Button CausesValidation="false" ID="btnBuscarProductoServicio" ToolTip="Seleccione para buscar el producto o servicio"
                                                                                runat="server" Width="20px" CssClass="buttonSmall" Text="..."></asp:Button>
                                                                            <asp:TextBox runat="server" ID="txtIdServicioProducto" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                            <asp:TextBox runat="server" ID="txtIdTipoServicio"  CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                            <asp:TextBox runat="server" ID="txtIdSolicitudServicio" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="180px"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Cantidad">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox MaxLength="50" ID="txtCantidad" runat="server" Width="85px" CssClass="textBoxSmall"
                                                                                onkeypress='return (this.value.length < 50);'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Dosis/Posología" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDosis" CssClass="textSmallBlack" runat="server" Visible="false"></asp:Label>
                                                                            <asp:TextBox MaxLength="100" ID="txtDosis" Visible="false" runat="server" Width="105px"
                                                                                Rows="2" TextMode="MultiLine" CssClass="textBoxSmall" onkeypress='return (this.value.length < 100);'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                     <asp:TemplateColumn HeaderText="Vía de Administración" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox MaxLength="100" ID="txtViaAdministracion" Visible="false" runat="server" Width="100px"
                                                                                Rows="2" TextMode="MultiLine" CssClass="textBoxSmall" onkeypress='return (this.value.length < 100);'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Duración" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox MaxLength="100" ID="txtDuracion" Visible="false" runat="server" Width="105px"
                                                                                Rows="2" TextMode="MultiLine" CssClass="textBoxSmall" onkeypress='return (this.value.length < 100);'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Fecha Prestación" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkPrestado" runat="server" Visible="false" Text="Prestado" TextAlign="Left">
                                                                            </asp:CheckBox>
                                                                            <asp:TextBox ID="txtFechaPrestacion" name="txtFechaPrestacion" runat="server" Width="20px"
                                                                                CssClass="textBoxSmall"></asp:TextBox>
                                                                            <asp:Image ID="btnFechaPrestacion" runat="server" ImageUrl="../../images/icoCalendar.gif">
                                                                            </asp:Image>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Valor Solici." Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox Width="55px" onkeypress="return currencyFormat(this,event,true,false)"
                                                                                runat="server" ID="txtValorConvenioSolicitado" CssClass="textBoxSmall"></asp:TextBox>
                                                                            <asp:Button CausesValidation="false" ID="lnkUVRSolicitado" ToolTip="Seleccione para calcular el UVR"
                                                                                runat="server" Width="28px" CssClass="buttonSmall" Text="UVR"></asp:Button>
                                                                            <asp:TextBox Width="35px" runat="server" ID="txtUVRSolicitado" CssClass="textBoxSmall"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Estado" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList OnSelectedIndexChanged="ddlEstadoServicio_SelectedIndexChanged"
                                                                                AutoPostBack="true" ID="ddlEstadoServicio" runat="server" Width="95px" CssClass="textBoxSmall">
                                                                            </asp:DropDownList>
                                                                            <asp:Label ID="lblValorAprobado" CssClass="textSmallBlack" runat="server" Visible="false">Aprobado $</asp:Label>
                                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" Width="60px"
                                                                                runat="server" ID="txtValorAprobado" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
                                                                            <asp:Button CausesValidation="false" Visible="false" ID="lnkRegistrarUVR" ToolTip="Seleccione para calcular el UVR"
                                                                                runat="server" Width="28px" CssClass="buttonSmall" Text="UVR"></asp:Button>
                                                                            <asp:TextBox Width="35px" runat="server" ID="txtUVR" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
                                                                            <asp:Label ID="lblDescuento" CssClass="textSmallBlack" runat="server" Visible="false">Descuento %</asp:Label>
                                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" Width="40px"
                                                                                runat="server" ID="txtDescuento" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Motivo" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlMotivo" runat="server" Width="90px" CssClass="textBoxSmall">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Comentario">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox MaxLength="500" ID="txtComentarioServicioProducto" runat="server" Width="135px"
                                                                                onkeypress='return (this.value.length < 500);' TextMode="MultiLine" Rows="2"
                                                                                CssClass="textBoxSmall"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn Visible="false" HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton CommandName="Eliminar" CssClass="ImageTransparent" ID="imbEliminarServicio"
                                                                                runat="server" ImageUrl="../../iconos/ico_borrar.gif"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="">
                                                                        <ItemTemplate>
                                                                            <img class="ImageTransparentLink" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this);"
                                                                                border="0">
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                            <table class="tableBorder" id="Table4" cellspacing="0" cellpadding="3" width="100%"
                                                                border="0">
                                                                <tr>
                                                                    <td width="30%">
                                                                        Observaciones<br>
                                                                        <span class="textSmallBlack">(Estas observaciones se despliegan en el formato de este
                                                                            tipo de servicio)</span>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:TextBox ID="txtComentariosTipoServicio" runat="server" CssClass="textBoxSmall"
                                                                            Width="300px" onkeypress='return (this.value.length < 500);' MaxLength="500"
                                                                            TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoServicio" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table class="tableBorder" id="Table6" cellspacing="0" cellpadding="3" width="98%"
                                            border="0">
                                            <tr>
                                                <td class="headerTable" colspan="7">
                                                    RECOMENDACIONES&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%">
                                                    Recomendaciones
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRecomendaciones" runat="server" CssClass="textBox" Width="550px"
                                                        Height="70px" MaxLength="500" TextMode="MultiLine" onkeypress='return (this.value.length < 500);'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <br>
                                        <table class="tableBorder" id="Table8" cellspacing="0" cellpadding="3" width="98%"
                                            border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="headerTable" colspan="7">
                                                        INCAPACIDAD&nbsp;
                                                        <asp:ImageButton ID="imbHistorialIncapacidades" runat="server" ImageUrl="../../images/icoHistorial.gif"
                                                            CausesValidation="false"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Fecha Inicio
                                                    </td>
                                                    <td width="12%">
                                                        <asp:TextBox ID="txtInicioIncapacidad" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                                            href="javascript:MostrarCalendario(Form1.txtInicioIncapacidad,Form1.txtInicioIncapacidad,'dd/mm/yyyy');"
                                                            name="btnFecha"><img id="imgFechaInicioIncapacidad" src="../../images/icoCalendar.gif"
                                                                border="0" runat="server"></a>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        Fecha Fin
                                                    </td>
                                                    <td width="12%">
                                                        <asp:TextBox ID="txtFinIncapacidad" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                                            href="javascript:MostrarCalendario(Form1.txtFinIncapacidad,Form1.txtFinIncapacidad,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0"></a>
                                                    </td>
                                                    <td width="5%">
                                                        Días
                                                    </td>
                                                    <td width="5%">
                                                        <asp:TextBox ID="txtDias" runat="server" CssClass="LabelNoModify" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:CheckBox ID="chkTranscripcion" runat="server" Text="Transcripción"></asp:CheckBox>
                                                        <asp:CheckBox ID="chkContinuacion" runat="server" Text="Prórroga"></asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Diagnósticos
                                                    </td>
                                                    <td colspan="6">
                                                        <uc1:WC_SeleccionarDiagnostico ID="WC_SeleccionarDiagnostico1" runat="server"></uc1:WC_SeleccionarDiagnostico>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Observaciones Incapacidad
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txtObservacionesIncapacidad" runat="server" CssClass="textBox" Width="500px"
                                                            MaxLength="500" TextMode="MultiLine" onkeypress='return (this.value.length < 500);'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <br>
                                        <table id="tblTotal" style="display: none" cellspacing="0" cellpadding="0" width="300"
                                            border="0">
                                            <tr>
                                                <td width="275">
                                                    <table class="tableBorder" id="Table5" cellspacing="0" cellpadding="5" width="275">
                                                        <tr class="tableBorder">
                                                            <td>
                                                                Total Solicitado
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalProductosServicios" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Total Aprobado
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalAprobado" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Total Facturas
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalFacturas" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" cellspacing="0" cellpadding="5" width="85%" align="center">
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="lblAnulacion" runat="server" Visible="False">Observaciones Anulacion<BR>
									<SPAN class="textSmallBlack">(Ingrese si va a anular la orden original)</SPAN> </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAnulacion" runat="server" CssClass="textBox" Visible="False"
                                                        Width="368px" Height="40px" MaxLength="500" TextMode="MultiLine" onkeypress='return (this.value.length < 500);'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
        <td align="center" colspan="2">
            <asp:Label ID="lblCargandoConsulta1" runat="server"></asp:Label>            
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
           <asp:Label ID="lblCargandoConsulta" runat="server" Text="" style="text-align: left"></asp:Label>
        </td>
    </tr>
            <tr id= trBotones>
                <td align="center" colspan="2">
                    <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="« Anterior">
                    </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnGuardar" OnClientClick = "dobleClickSubmint()" runat="server"
                        CssClass="button" Text="Siguiente »"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="button" CausesValidation="false"
                        Text="Cancelar"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAnular" runat="server" CssClass="buttonBig" Visible="False" Text="Anular/Copiar">
                    </asp:Button>
                </td>
            </tr>
        </tbody>
    </table>
    <uc1:WC_BuscarServicioProducto ID="WC_BuscarServicioProducto1" runat="server"></uc1:WC_BuscarServicioProducto>
    <uc1:WC_BuscarPrestador ID="WC_BuscarPrestador1" runat="server"></uc1:WC_BuscarPrestador>
    <uc1:WC_BuscarPrestadorTipoServicio ID="WC_BuscarPrestadorTipoServicio1" runat="server">
    </uc1:WC_BuscarPrestadorTipoServicio>
    <uc1:WC_BuscarProveedor ID="WC_BuscarProveedor1" runat="server"></uc1:WC_BuscarProveedor>
    <uc1:WC_RegistrarUVR ID="WC_RegistrarUVR1" runat="server"></uc1:WC_RegistrarUVR>
    </form>
    </TR></TBODY></TABLE></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
</body>
</html>
