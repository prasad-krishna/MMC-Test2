<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnosticoConsulta" Src="../WebControls/WC_AdicionarDiagnosticoConsulta.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>

<%@ Page Language="c#" CodeBehind="AE_registroestilovida.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.interfaz_empleado.forma.AE_registroestilovida" %>

<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>
<%@ Register Src="../WebControls/WC_AdicionarDiagnosticoConsultaCIE10.ascx" TagName="WC_AdicionarDiagnosticoConsultaCIE10"
    TagPrefix="uc2" %>
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

    <style type="text/css">
        .style1 {
            padding-left: 3px;
            padding-top: 0px;
            padding-bottom: 0px;
            font-weight: bold;
            font-size: 10px;
            background-image: url( '../../Images/imgHeaderGrid.gif' );
            color: #102251;
            border-bottom: #55a0cd 1px solid;
            background-repeat: repeat-x;
            font-family: Verdana, tahoma, sans-serif;
            height: 20px;
        }
    </style>
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Scripts para control de sesión -->
    <script src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.countdown.js" type="text/javascript"></script>
    <script src="../../scripts/ControlSesion.js" type="text/javascript"></script>
    <!-- Fin - Emilio Bueno 20/11/2012 -->
</head>
<body leftmargin="5" topmargin="1" onload="CargarConfiguracion();"
    rightmargin="5">
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
            function ValidarCaracteres(textareaControl, maxlength) {
                if (textareaControl.value.length > maxlength) {
                    textareaControl.value = textareaControl.value.substring(0, maxlength);
                    alert("Debe ingresar hasta un maximo de " + maxlength + " caracteres");
                }
            }
            function CalcularCinturaCadera() {
                var strSeparadorMiles = '<%=ConfigurationManager.AppSettings["SeparadorMiles"].ToString() %>';
			    var strSeparadorDecimales = '<%=ConfigurationManager.AppSettings["SeparadorDecimales"].ToString() %>';
			    var txtCintura = document.getElementById('txtDiametroCintura').value.replace(strSeparadorMiles, "");
			    var txtCadera = document.getElementById('txtDiametroCadera').value.replace(strSeparadorMiles, "");
			    var txtRelacionCinturaCadera = document.getElementById('txtRelacionCinturaCadera');
			    var ResultadoRelacion = (parseFloat(txtCintura) / (parseFloat(txtCadera))).toFixed(2);
			    txtRelacionCinturaCadera.value = ResultadoRelacion.toString().replace(".", strSeparadorDecimales).replace(",", strSeparadorDecimales);
			}

			function HabilitarValidadores(tipoConsulta) {
			    var medico = ["1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17"];
			    var nutriologo = ["15", "16"];
			    var todo = ["3", "4"];

			    var esMedico = $.inArray(tipoConsulta, medico);
			    var esNutriologo = $.inArray(tipoConsulta, nutriologo);
			    var esTodo = $.inArray(tipoConsulta, todo);

			    //RAM cambio
			    //if (esMedico >= 0) {
			    $("#divAlimentacionInadecuada").hide();
			    if (tipoConsulta == 15 || tipoConsulta == 16) {
			        $("#divWellness").hide();
			        $("#divAntecedentesAusentismo").hide();
			        $("#divEstadoSalud").hide();
			        $("#divAccidentalidad").hide();
			        $("#divEmocional").hide();
			        $("#divEstres").hide();
			        $("#divSaludOral").hide();
			        $("#divSedentarismo").hide();
			        $("#divRutinaEjercicioGrande").hide();
			        $("#divVacunacion").hide();
			        $("#divConsumoAlcohol").hide();
			        $("#divHabitoFumar").hide();
			        $("#divRecomendaciones").hide();
			        $("#divAlimentacionInadecuada").show();
			    }
			}

			if (typeof window.event != 'undefined')
			    document.onkeydown = function () {
			        var test_var = event.srcElement.tagName.toUpperCase();
			        var test_id = event.srcElement.id;
			        if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
			            return (event.keyCode != 8);
			    }
			else
			    document.onkeypress = function (e) {
			        var test_var = e.target.nodeName.toUpperCase();
			        var test_id = e.target.id;
			        if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
			            return (e.keyCode != 8);
			    }


			function dobleClickSubmint() {
			    var isValid = false;
			    //if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) //Marsh - JFEE - 2014/11/26 - Correcciones generales, previene que se muestren dos mensajes de validación
			    if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == true) {
			        isValid = Page_ClientValidate();
			    }

			    if (isValid) {
			        document.getElementById('lblCargandoConsulta1').innerHTML = 'Cargando Consulta';
			        document.getElementById('btnGuardar').style.display = "none";
			        document.getElementById('btnCancelar').style.display = "none";
			        document.getElementById('trValidar').style.display = "none";
			        scroll();

			    }
			}

			var msg = "||||||||||||||||||||"
			var x = ""
			num = 1
			toggle = 1
			tt = 1
			OOK = 0
			timval = 60
			var tval = ""
			function fasl() { timval = (timval == 60 ? 150 : 60) }

			function csd() {
			    tt = (tt == 1 ? 0 : 1)
			}
			function scroll() {
			    if (tt == 1)
			        if (num <= msg.length)
			            OOK = 1
			    if (tt == 0)
			        if (num >= 0)
			            OOK = 1

			    if (OOK == 1) {
			        OOK = 0
			        x = msg.substring(0, num)
			        document.getElementById('lblCargandoConsulta').innerHTML = x;
			        num = (tt == 1 ? num + 1 : num - 1)
			    }
			    else {
			        x = ""
			        document.getElementById('lblCargandoConsulta').innerHTML = "";
			        num = (tt == 1 ? 0 : msg.length)
			    }
			    if (toggle == 1)
			        setTimeout("scroll()", timval)
			}
			function toggla() {
			    toggle = (toggle == 1 ? 0 : 1)
			    if (toggle == 1) scroll()
			}
        </script>

        <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0">
            <tbody>
                <tr>
                    <td align="center" colspan="2">&nbsp;
                    <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:ImageButton ID="imbHistorial" runat="server" CausesValidation="false" ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>&nbsp;<asp:LinkButton ID="lnkVerHistorico" runat="server" CausesValidation="false">Ver Histórico de Consultas</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imbVerHistorialOrdenes" runat="server" ImageUrl="../../images/icoHistorial.gif"
                        CausesValidation="false"></asp:ImageButton>&nbsp;
                    <asp:LinkButton ID="lnkVerHistorialOrdenes" runat="server" CausesValidation="false">Ver Histórico de Órdenes</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imblnkVerHistoria" runat="server" ImageUrl="../../images/icoHistorial.gif"
                        CausesValidation="false"></asp:ImageButton>&nbsp;
                    <asp:LinkButton ID="lnkVerHistoria" runat="server" CausesValidation="false">Ver Historia Clínica Consolidada</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imbGuardar" runat="server" ImageUrl="../../iconos/icoGuardar.gif"
                        CausesValidation="false" OnClick="imbGuardar_Click"></asp:ImageButton>
                        <asp:LinkButton ID="lnkGuardar" runat="server" CausesValidation="false" OnClick="lnkGuardar_Click">Guardar</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">&nbsp;
                    <fieldset class="FieldSet" style="width: 97%">
                        <legend>
                            <img src="../../images/icoHistoria.gif" border="0">
                            &nbsp;Estilo de Vida</legend>
                        <br>
                        <table id="Table3" cellspacing="0" cellpadding="3" width="95%" align="center">
                            <tr>
                                <td colspan="2" id="wellness">
                                    <div id="divWellness" runat="server" visible="False">
                                        <table class="tableBorder" id="Table1" cellspacing="0" cellpadding="3" width="100%"
                                            align="center">
                                            <tr>
                                                <td class="headerTable" colspan="4">WELLNESS
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">¿Está actualmente afiliado al programa de wellness?<br />
                                                    <asp:RadioButtonList ID="rblwellness" runat="server" Width="112px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">¿Tiene firmado el acuerdo para participar en el programa?<br />
                                                    <asp:RadioButtonList ID="rblFirmaWellness" runat="server" Width="112px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="habitoFumar">
                                    <div id="divHabitoFumar" runat="server" visible="False">
                                        <asp:UpdatePanel ID="UpdatePanel29" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="tableBorder" id="Table2" cellspacing="0" cellpadding="3" width="100%"
                                                    align="center">
                                                    <tr>
                                                        <td class="style1">HÁBITO DE FUMAR
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>¿Convive, trabaja o pasa algún tiempo con personas que regularmente fuman cerca
                                                            de usted?<br />
                                                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlConviveFumador" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <asp:RequiredFieldValidator ID="rfvConviveFumador" runat="server" ControlToValidate="ddlConviveFumador"
                                                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                ForeColor=" "></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>¿Cuál de las siguientes respuestas describe mejor su conducta frente a el cigarrillo?
                                                            <asp:RadioButtonList ID="rblConductaCigarrillo" runat="server" RepeatDirection="Vertical"
                                                                OnSelectedIndexChanged="rblConductaCigarrillo_SelectedIndexChanged" AutoPostBack="True">
                                                            </asp:RadioButtonList>
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
                                                                <ProgressTemplate>
                                                                    <div align="center" style="font-weight: bold">
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/iconos/LoadingBlueSmall.gif" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:RequiredFieldValidator ID="rfvConductaCigarrillo" runat="server" CssClass="textRed"
                                                                ForeColor=" " Display="Dynamic" ControlToValidate="rblConductaCigarrillo" ErrorMessage="Requerido"
                                                                Enabled="False"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <caption>
                                                        <br />
                                                        <div id="divFumaActualmente" runat="server" visible="false">
                                                            <tr>
                                                                <td>Seleccione cual de los siguientes productos utiliza actualmente
                                                                    <asp:CheckBoxList ID="cblQueFuma" runat="server" RepeatColumns="2">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Cuánto tiempo transcurre desde que se levanta hasta encender el primer cigarrillo?<br />
                                                                    <asp:RadioButtonList ID="rblTiempoPrimerCigarrillo" runat="server" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvTiempoPrimerCigarrillo" runat="server" ControlToValidate="rblTiempoPrimerCigarrillo"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Tiene dificultades para no fumar en lugares donde está prohibido (Ej. en la iglesia,
                                                                    en la biblioteca, en el cine)?<br />
                                                                    <asp:RadioButtonList ID="rblDificultadFumar" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvDificultadFumar" runat="server" ControlToValidate="rblDificultadFumar"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Qué cigarrillo le costaría más suprimir?<br />
                                                                    <asp:RadioButtonList ID="rblCigarrilloSuprimir" runat="server" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvCigarrilloSuprimir" runat="server" ControlToValidate="rblCigarrilloSuprimir"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Cuántos cigarrillos fuma al día?<br />
                                                                    <asp:TextBox ID="txtCigarrillosalDia" runat="server" CssClass="textBox" MaxLength="4"
                                                                        onkeydown="return keyDown(this,event)" onkeypress="return currencyFormat(this,event,true,false)"
                                                                        Width="70px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCigarrillosalDia" runat="server" ControlToValidate="txtCigarrillosalDia"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Fuma más frecuentemente durante las primeras horas del día que durante el resto
                                                                    del día?<br />
                                                                    <asp:RadioButtonList ID="rblFrecuenciaPrimerasHorasDia" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvFrecuenciaPrimerasHorasDia" runat="server" ControlToValidate="rblFrecuenciaPrimerasHorasDia"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Fuma cuándo debe guardar cama por una enfermedad la mayor parte del día?<br />
                                                                    <asp:RadioButtonList ID="rblFumaEnfermedad" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rvfFumaEnfermedad" runat="server" ControlToValidate="rblFumaEnfermedad"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Dentro de qué categoría entran la mayoría de cigarrillos que usted fuma?<br />
                                                                    <asp:RadioButtonList ID="rblCategoriaCigarrillos" runat="server" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvCategoriaCigarrillos" runat="server" ControlToValidate="rblCategoriaCigarrillos"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Aspira el humo cuando fuma?<br />
                                                                    <asp:RadioButtonList ID="rblAspiraHumo" runat="server" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="rfvAspiraHumo" runat="server" ControlToValidate="rblAspiraHumo"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </div>
                                                        <div id="divFumaba" runat="server" visible="false">
                                                            <tr>
                                                                <td>¿Cuántos años hace que dejó de fumar?<br />
                                                                    <asp:TextBox ID="txtAnosDejoFumar" runat="server" CssClass="textBox" onkeydown="return keyDown(this,event)"
                                                                        onkeypress="return currencyFormatOneDecimal(this,event,true,true,1)" Width="70px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAnosDejoFumar" runat="server" ControlToValidate="txtAnosDejoFumar"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>¿Cual era el promedio diario de cigarrillos que fumaba durante los dos años previos
                                                                    de dejar el hábito?<br />
                                                                    <asp:TextBox ID="txtPromedioDiarioX2Anos" runat="server" CssClass="textBox" onkeydown="return keyDown(this,event)"
                                                                        onkeypress="return currencyFormat(this,event,true,false)" Width="70px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvPromedioDiarioX2Anos" runat="server" ControlToValidate="txtPromedioDiarioX2Anos"
                                                                        CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="Requerido"
                                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </caption>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="consumoAlcohol">
                                    <div id="divConsumoAlcohol" runat="server" visible="False">
                                        <table class="tableBorder" id="Table9" cellspacing="0" cellpadding="3" width="100%"
                                            align="center">
                                            <tr>
                                                <td class="headerTable" colspan="6">CONSUMO DE ALCOHOL&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Alcohol, Copas a la semana (rangos)<br />
                                                    <asp:DropDownList ID="ddlCopasSemana" runat="server" CssClass="textBox" Visible="True">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="rfvCopasSemana" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlCopasSemana" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Alguna vez han criticado su consumo de alcohol?<br />
                                                    <asp:RadioButtonList ID="rblCriticaAlcohol" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvCriticaAlcohol" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblCriticaAlcohol" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Alguna vez se ha arrepentido de la cantidad de alcohol que consumió?<br />
                                                    <asp:RadioButtonList ID="rblArrepentidoAlcohol" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvArrepentidoAlcohol" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblArrepentidoAlcohol" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Ha tenido lagunas por el consumo de alcohol?<br />
                                                    <asp:RadioButtonList ID="rblLagunaAlcohol" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvLagunaAlcohol" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblLagunaAlcohol" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Alguna vez lo primero que ha consumido en la mañana ha sido una copa de alcohol?<br />
                                                    <asp:RadioButtonList ID="rblMananaAlcohol" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvMananaAlcohol" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblMananaAlcohol" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="vacunacion">
                                    <div id="divVacunacion" runat="server" visible="False">
                                        <table class="tableBorder" id="Table10" cellspacing="0" cellpadding="3" width="100%"
                                            align="center">
                                            <tr>
                                                <td class="headerTable" colspan="6">VACUNACIÓN&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Influenza Estacional en el último año?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblInfluenciaEstacional" runat="server"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaInfluenzaEstacional" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaInfluenzaEstacional,Form1.txtFechaInfluenzaEstacional,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="imgCalInfluenza"
                                                                runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvInfluenciaEstacional" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblInfluenciaEstacional" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteInfluenciaEstacional" runat="server" CssClass="textBox"
                                                        Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Influenza H1N1 en el último año?<br />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButtonList ID="rblInfluenciaH1N1" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaInfluenciaH1N1" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaInfluenciaH1N1,Form1.txtFechaInfluenciaH1N1,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="imgCalH1N1"
                                                                runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvInfluenciaH1N1" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblInfluenciaH1N1" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteInfluenciaH1N1" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Fiebre Amarilla?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblFiebreAmarilla" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaFiebreAmarilla" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaFiebreAmarilla,Form1.txtFechaFiebreAmarilla,'dd/mm/yyyy');"
                                                            name="btnFecha1"><img src="../../images/icoCalendar.gif" border="0" id="imgCalFiebre"
                                                                runat="server"></a><asp:RequiredFieldValidator ID="rfvFiebreAmarilla" runat="server"
                                                                    CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="rblFiebreAmarilla"
                                                                    ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteFiebreAmarilla" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Hepatitis Viral A?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButtonList ID="rblHepatitisViral" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaHepatitisViral" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaHepatitisViral,Form1.txtFechaHepatitisViral,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="imgCalHepatitis"
                                                                runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvHepatitisViral" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblHepatitisViral" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteHepatitisViral" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Difteria, Tos ferina, o Tétanos (DPT)?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblToxoideTetanico" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaToxoideTetanico" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaToxoideTetanico,Form1.txtFechaToxoideTetanico,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="imgCalTetanos"
                                                                runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvToxoideTetanico" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblToxoideTetanico" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteToxoideTetanico" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra la Hepatitis Viral B?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblHepatitisViralB" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaHepatitisViralB" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaHepatitisViralB,Form1.txtFechaHepatitisViralB,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="img1" runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvHepatitisViralB" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblHepatitisViralB" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteHepatitisViralB" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra el Meningococo?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblMeningococo" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaMeningococo" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaMeningococo,Form1.txtFechaMeningococo,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="img2" runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvMeningococo" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblMeningococo" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteMeningococo" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra Fiebre Tifoidea?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblFiebreTifoidea" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaFiebreTifoidea" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaFiebreTifoidea,Form1.txtFechaFiebreTifoidea,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="img3" runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvFiebreTifoidea" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblFiebreTifoidea" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteFiebreTifoidea" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">¿Se ha aplicado la vacuna contra VPH?
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rblVPH" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaVPH" runat="server" CssClass="textBox"
                                                        Width="80px"></asp:TextBox>&nbsp;<a href="javascript:MostrarCalendario(Form1.txtFechaVPH,Form1.txtFechaVPH,'dd/mm/yyyy');"
                                                            name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="img4" runat="server"></a>
                                                    <asp:RequiredFieldValidator ID="rfvVPH" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="rblVPH" ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Marca y lote&nbsp;
                                                    <asp:TextBox ID="txtMarcaLoteVPH" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                    </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="divRutinaEjercicioGrande" runat="server" visible="False">
                                    <table class="tableBorder" id="Table13" cellspacing="0" cellpadding="3" width="100%"
                                        align="center">
                                        <tr>
                                            <td class="headerTable">RUTINA EJERCICIO&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divRutinaEjercicio" runat="server" visible="false">
                                                    <tr>
                                                        <td>¿Ha llevado alguna rutina de ejercicio durante el ultimo mes?<br />
                                                            <asp:RadioButtonList ID="rblrutinaUltimoMes" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rblrutinaUltimoMes_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="textRed"
                                                                ForeColor=" " Display="Dynamic" ControlToValidate="rblrutinaUltimoMes" ErrorMessage="Requerido"
                                                                Enabled="False"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </div>
                                                <div id="DivNoRutinaEjercicio" runat="server" visible="false">
                                                    <tr>
                                                        <td>¿Por que no ha llevado alguna rutina de ejercicio durante el ultimo mes?<br />
                                                            <asp:DropDownList ID="ddlNoRutinaEjercicio" runat="server" CssClass="textBox" Visible="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Otro motivo&nbsp;
                                                        <asp:TextBox ID="txtOtroMotivo" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </td> </tr>
                            <tr>
                                <td colspan="2" id="sedentarismo">
                                    <div id="divSedentarismo" runat="server" visible="False">
                                        <table class="tableBorder" id="Table4" cellspacing="0" cellpadding="3" width="100%"
                                            align="center">
                                            <tr>
                                                <td class="headerTable">SEDENTARISMO&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>¿Practicas de manera constante deporte en los últimos 6 meses?<br />
                                                    <asp:RadioButtonList ID="rblPracticaDeporte" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvPracticaDeporte" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblPracticaDeporte" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>¿Cuántas veces practicas deporte o actividad física en la semana?<br />
                                                    <asp:DropDownList ID="ddlPracticaDeporteSemana" runat="server" CssClass="textBox"
                                                        Visible="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPracticaDeporteSemana" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlPracticaDeporteSemana"
                                                        ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Promedio de tiempo en minutos en cada sesión (Tiempo total al día o conteo de segmentos
                                                    de actividad realizada de 10 minutos o más. Ejemplo: 2 períodos de 10 minutos en
                                                    el día cuenta un total de 20 minutos):<br />
                                                    <asp:DropDownList ID="ddlPromedioTiempoMinutos" runat="server" CssClass="textBox"
                                                        Visible="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPromedioTiempoMinutos" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlPromedioTiempoMinutos"
                                                        ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>¿Qué tipo de actividad física realizas?<br />
                                                    <asp:RadioButtonList ID="rblTipoActividadFisica" runat="server" RepeatDirection="Vertical"
                                                        CssClass="tableNoBorderSmall" Visible="True">
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvTipoactividadFisica" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblTipoactividadFisica" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>¿Cuántas horas ves diarias en promedio de televisión?<br />
                                                    <asp:DropDownList ID="ddlHorasDiariasTV" runat="server" CssClass="textBox" Visible="True">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="rfvHorasDiariasTV" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlHorasDiariasTV" ErrorMessage="Requerido"
                                                        Enabled="False"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" id="saludOral">
                        <div id="divSaludOral" runat="server" visible="False">
                            <table class="tableBorder" id="Table5" cellspacing="0" cellpadding="3" width="100%"
                                align="center">
                                <tr>
                                    <td class="headerTable" colspan="6">SALUD ORAL
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">¿Ha asistido a consulta odontológica en el último año?<br />
                                        <asp:RadioButtonList ID="rblConsultaOdontologica" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvConsultaOdontologica" runat="server" CssClass="textRed"
                                            ForeColor=" " Display="Dynamic" ControlToValidate="rblConsultaOdontologica" ErrorMessage="Requerido"
                                            Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">¿Cuántas veces se lava los dientes al día?<br />
                                        <asp:DropDownList ID="ddlLavaDientes" runat="server" CssClass="textBox" Visible="True">
                                        </asp:DropDownList>
                                        &nbsp;
                                    <asp:RequiredFieldValidator ID="rfvLavaDientes" runat="server" CssClass="textRed"
                                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlLavaDientes" ErrorMessage="Requerido"
                                        Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">¿Además de lavarse los dientes, usa hilo dental todos los días?<br />
                                        <asp:RadioButtonList ID="rblSedaDental" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvSedaDental" runat="server" CssClass="textRed"
                                            ForeColor=" " Display="Dynamic" ControlToValidate="rblSedaDental" ErrorMessage="Requerido"
                                            Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" id="estres">
                        <div id="divEstres" runat="server" visible="False">
                            <table class="tableBorder" id="Table6" cellspacing="0" cellpadding="3" width="100%"
                                align="center">
                                <tr>
                                    <td class="headerTable" colspan="4">ESTRÉS&nbsp;
                                    </td>
                                </tr>
                                <tr id="trUltimosMesesDecaido" style="display: none">
                                    <td colspan="4">En los últimos meses, ¿te has sentido decaído (a), deprimido (a) o estresado (a)
                                    de manera persistente?<br />
                                        <asp:RadioButtonList CssClass="tableNoBorderSmall" ID="rblSentidoDecaido" runat="server"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvSentidoDecaido" runat="server" CssClass="textRed"
                                            ForeColor=" " Display="Dynamic" ControlToValidate="rblSentidoDecaido" ErrorMessage="Requerido"
                                            Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                </tr>
                <tr>
                    <td colspan="4">¿Sobre los últimos 3 meses, qué tanto se ha sentido estresado?<br />
                        <asp:RadioButtonList ID="rblSentidoEstresado" runat="server" RepeatDirection="Vertical">
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvSentidoEstresado" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="rblSentidoEstresado" ErrorMessage="Requerido"
                            Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trSentidoInteres" style="display: none">
                    <td colspan="4">En los dos últimos meses, ¿has tenido poco interés o placer al hacer las cosas?<br />
                        <asp:RadioButtonList ID="rblInteresPlacer" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvInteresPlacer" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="rblInteresPlacer" ErrorMessage="Requerido"
                            Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">¿Cómo clasificarías tu nivel de estrés?<br />
                        <asp:DropDownList ID="ddlNivelEstres" runat="server" CssClass="textBox" Visible="True">
                        </asp:DropDownList>
                        &nbsp;
                    <asp:RequiredFieldValidator ID="rfvNivelEstres" runat="server" CssClass="textRed"
                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlNivelEstres" ErrorMessage="Requerido"
                        Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">En las últimas dos semanas, qué tan frecuente ha estado preocupado por alguno de
                    los siguientes problemas<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">POCO INTERÉS O PLACER POR HACER COSAS
                    <br />
                        <asp:DropDownList ID="ddlPocoInteres" runat="server" CssClass="textBox" Visible="True">
                        </asp:DropDownList>
                        &nbsp;
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" CssClass="textRed"
                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlNivelEstres" ErrorMessage="Requerido"
                        Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">DECAÍDO, DEPRIMIDO O SIN ESPERANZA
                    <br />
                        <asp:DropDownList ID="ddlSinEsperanza" runat="server" CssClass="textBox" Visible="True">
                        </asp:DropDownList>
                        &nbsp;
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" CssClass="textRed"
                        ForeColor=" " Display="Dynamic" ControlToValidate="ddlNivelEstres" ErrorMessage="Requerido"
                        Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">A continuación se muestra una lista de las fuentes típicas de estrés. Marque todas
                    las que usted sienta sean una sobrecarga para usted:<br />
                        <asp:CheckBoxList ID="cblFuentesEstres" runat="server" RepeatDirection="Vertical"
                            CssClass="tableNoBorderSmall">
                        </asp:CheckBoxList>
                    </td>
                    <tr>
                        <td colspan="4">
                            <br />
                            Selecciona el enunciado que mejor describa tus planes para controlar tu estrés<br />
                            <asp:RadioButtonList ID="rblControlarEstres" runat="server" RepeatDirection="Vertical"
                                CssClass="tableNoBorderSmall">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvControlarEstres" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="rblControlarEstres" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
        </table>
        </div> </td> </tr>
    <tr>
        <td colspan="2" id="emocional">
            <div id="divEmocional" runat="server" visible="False">
                <table class="tableBorder" id="Table7" cellspacing="0" cellpadding="3" width="100%"
                    align="center">
                    <tr>
                        <td class="headerTable" colspan="6">EMOCIONAL&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">¿En el último mes cómo calificarías la calidad general de tu sueño?<br />
                            <asp:RadioButtonList ID="rblCalificacionSueno" runat="server" RepeatDirection="Vertical"
                                CssClass="tableNoBorderSmall">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvCalificacionSueno" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="rblCalificacionSueno" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trNocheHabitual" style="display: none" runat="server">
                        <td colspan="6">Después de una noche habitual de sueño ¿te sientes cansado(a) o fatigado(a) al levantarte?<br />
                            <asp:DropDownList ID="ddlEstadoLevantarse" runat="server" CssClass="textBox" Visible="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEstadoLevantarse" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlEstadoLevantarse" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">¿En el último mes, ha dormido lo suficiente la mayoría de las noches y usted se
                            ha levantado descansado y alerta?<br />
                            <asp:DropDownList ID="ddlDormidoSuficiente" runat="server" CssClass="textBox" Visible="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDormidoSuficiente" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlDormidoSuficiente" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">¿Cuantas horas duerme regularmente?<br />
                            <asp:DropDownList ID="ddlHorasDuermeRegular" runat="server" CssClass="textBox" Visible="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvHorasDuermeRegular" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlHorasDuermeRegular" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">¿Cómo califica su estado de ánimo emocional?<br />
                            <asp:DropDownList ID="ddlEstadoAnimoEmocional" runat="server" CssClass="textBox"
                                Visible="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEstadoAnimoEmocional" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlEstadoAnimoEmocional" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
        <tr>
            <td colspan="2" id="alimentacion">
                <div id="divAlimentacionInadecuada" runat="server" visible="False">
                    <table class="tableBorder" id="Table12" cellspacing="0" cellpadding="3" width="100%"
                        align="center">
                        <tr>
                            <td class="headerTable" colspan="6">ALIMENTACIÓN&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuantas porciones de fruta come en el día en promedio?<br />
                                <b>Ejemplo:</b> 120ml (1/2 tasa) de fruta fresca, congelada o enlatada. 60 ml (1/4
                            tasa) de fruta seca, 120 ml (1/2 tasa) de jugo 100% de fruta, 1 manzana pequeña,
                            1 plátano pequeño o 4 fresas grandes.<br />
                                <asp:DropDownList ID="ddlPorcionesFrutas" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPorcionesFrutas" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlPorcionesFrutas" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuantas porciones de vegetales come cada día, en promedio?<br />
                                <b>Ejemplo:</b> 120ml (1/2 tasa) de vegetales crudos o cocidos, 240 ml (1 tasa)
                            de lechuga frondosa, 120 ml (1/2 tasa) de jugo 100% de vegetales, 120 (1/2 tasa)
                            de frijoles, 1 zanahoria mediana, 1 elote pequeño.<br />
                                <asp:DropDownList ID="ddlPorcionesVegetales" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPorcionesVegetales" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlPorcionesVegetales" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Qué tan frecuente su elección de comida y/o opciones al cocinar incluyen uno o
                            más de los siguientes ejemplos?<br />
                                <b>Ejemplo:</b> Carne roja grasa (Tocino o carne frita), salami, Bolonia, carne
                            molida, hamburguesas, hot dogs y salsas, pollo frito, pescado frito y papas fritas,
                            donas, galletas, pastas, barras de dulce, leche entera, crema o queso (o comida
                            hecho con éstos), mantequilla, margarina, manteca, helado.<br />
                                <asp:DropDownList ID="ddlFrecuenciaCarne" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFrecuenciaCarne" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlFrecuenciaCarne" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Qué tan frecuente su elección de comida y/o opciones al cocinar incluyen uno o
                            más de los siguientes ejemplos?<br />
                                <b>Ejemplo:</b> Nueces, mantequilla de nuez, mantequilla de cacahuate, aguacate,
                            pescado rico en omega 3 (salmón, atún), cápsulas de aceite de pescado, aceites como:
                            oliva, canola, girasol, sésamo, maíz, cacahuate, soya, (NO aceite de palma o coco),
                            margarina líquida.<br />
                                <asp:DropDownList ID="ddlFrecuenciaSano" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFrecuenciaSano" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlFrecuenciaSano" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuántas porciones de alimentos de grano come al día, en promedio?<br />
                                <b>Ejemplo:</b> Avena, palomas de maíz, arroz. 1 rebanada de pan 100% de trigo,
                            28 gr o 240 ml (1 tasa) de cereal de grano, 120 ml (1/2 tasa) de cereal de avena
                            (cocido), 120 ml (1/2 tasa) de arroz cocido o pasta de trigo.<br />
                                <asp:DropDownList ID="ddlFrecuenciaGranos" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFrecuenciaGranos" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlFrecuenciaGranos" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuántas bebidas con azúcar (no endulzadas artificialmente) toma por día en promedio?<br />
                                <b>Ejemplo:</b> una bebida es equivalente a 360ml.<br />
                                <asp:DropDownList ID="ddlFrecuenciaAzucar" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFrecuenciaAzucar" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlFrecuenciaAzucar" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuántas veces es cuidadoso con el límite y cantidad de sal (sodio) en sus comidas
                            y bebidas?<br />
                                <asp:DropDownList ID="ddlFrecuenciaSodio" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFrecuenciaSodio" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlFrecuenciaSodio" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="comportamientosRiegoAccidentalidad">
                <div id="divAccidentalidad" runat="server" visible="False">
                    <table class="tableBorder" id="Table8" cellspacing="0" cellpadding="3" width="100%"
                        align="center">
                        <tr>
                            <td class="headerTable" colspan="6">COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Utiliza el Cinturón de seguridad?
                            <br />
                                <asp:DropDownList ID="ddlCinturonSeguridad" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCinturonSeguridad" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlCinturonSeguridad" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cuándo conduce el coche utiliza el celular con manos libres?
                            <br />
                                <asp:DropDownList ID="ddlCocheCelular" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCocheCelular" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlCocheCelular" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Qué tan cerca del límite de velocidad conduces generalmente?<br />
                                <asp:DropDownList ID="ddlLimiteVelocidad" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLimiteVelocidad" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlLimiteVelocidad" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Con qué frecuencia en el último mes has manejado o viajado en un vehículo en el
                            que posiblemente el conductor había bebido demasiado?<br />
                                <asp:DropDownList ID="ddlConductorBebido" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                &nbsp;
                            <asp:RequiredFieldValidator ID="rfvConductorBebido" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlConductorBebido" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Con qué frecuencia usas un casco cuando paseas en bicicleta o motocicleta?<br />
                                <asp:DropDownList ID="ddlCasco" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                &nbsp;
                            <asp:RequiredFieldValidator ID="rfvCasco" runat="server" CssClass="textRed" ForeColor=" "
                                Display="Dynamic" ControlToValidate="ddlCasco" ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Con qué frecuencia usas filtro solar con factor de protección 15 o mayor cuando
                            pasas tiempo al sol?<br />
                                <asp:DropDownList ID="ddlFiltroSolar" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                &nbsp;
                            <asp:RequiredFieldValidator ID="rfvFiltroSolar" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlFiltroSolar" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Has realizado alguna revisión de seguridad doméstica en los seis meses anteriores?<br />
                                <asp:RadioButtonList ID="rblSeguridadDomestica" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvSeguridadDomestica" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="rblSeguridadDomestica" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Toma las medidas de protección adecuadas frente al riesgo de contraer enfermedades
                            de transmisión sexual?<br />
                                <asp:DropDownList ID="ddlTrasmisionSexual" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                &nbsp;
                            <asp:RequiredFieldValidator ID="rfvTrasmisionSexual" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlTrasmisionSexual" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="percepcionEstadoSalud">
                <div id="divEstadoSalud" runat="server" visible="False">
                    <table class="tableBorder" id="Table11" cellspacing="0" cellpadding="3" width="100%"
                        align="center">
                        <tr>
                            <td class="headerTable" colspan="6">PERCEPCIÓN DEL ESTADO DE SALUD&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">¿Cómo califica su estado de salud en términos generales?<br />
                                <asp:DropDownList ID="ddlEstadoSalud" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEstadoSalud" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlEstadoSalud" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">En general que tan dispuesto está a modificar sus hábitos de vida como son actividad
                            física, dejar de fumar y un programa de educación en salud<br />
                                <asp:DropDownList ID="ddlHabitosVida" runat="server" CssClass="textBox" Visible="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHabitosVida" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="ddlHabitosVida" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="antecedentesAusentismo">
                <div id="divAntecedentesAusentismo" runat="server" visible="False">
                    <table class="tableBorder" id="Table15" cellspacing="0" cellpadding="3" width="100%"
                        align="center">
                        <tr>
                            <td class="headerTable" colspan="5">ANTECEDENTES AUSENTISMO
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">¿Ha estado incapacitado en el último año?<br />
                                <asp:RadioButtonList ID="rblIncapacitado" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvIncapacitado" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="rblIncapacitado" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">¿Cuál fue el diagnóstico que originó la incapacidad?<br />
                                <table id="Table16" cellspacing="0" cellpadding="3" width="100%" align="center">
                                    <tr>
                                        <td>
                                            <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE1"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">¿Por cuántos días fue incapacitado?<br />
                                <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                    MaxLength="5" ID="txtDiasIncapacidad" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDiasIncapacidad" runat="server" CssClass="textRed"
                                    ForeColor=" " Display="Dynamic" ControlToValidate="txtDiasIncapacidad" ErrorMessage="Requerido"
                                    Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">Antecedentes hospitalarios, médicos o quirúrgicos en el último año que hayan generado
                            incapacidad:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table17" cellspacing="0" cellpadding="3" width="100%" align="center">
                                    <tr>
                                        <td>
                                            <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE2"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaHospitalizacion1" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                    href="javascript:MostrarCalendario(Form1.txtFechaHospitalizacion1,Form1.txtFechaHospitalizacion1,'dd/mm/yyyy');"
                                    name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalAntecendentes1"
                                        runat="server"></a><asp:RequiredFieldValidator ID="rfvFechaHospitalizacion1" runat="server"
                                            CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaHospitalizacion1"
                                            ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3">
                                <asp:TextBox onkeydown="return keyDown(this,event)" MaxLength="5" onkeypress="return currencyFormat(this,event,true,false)"
                                    ID="txtDiasHospitalizacion1" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                Días
                            <asp:RequiredFieldValidator ID="rfvDiasHospitalizacion1" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtDiasHospitalizacion1" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table14" cellspacing="0" cellpadding="3" width="100%" align="center">
                                    <tr>
                                        <td>
                                            <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE3"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaHospitalizacion2" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                    href="javascript:MostrarCalendario(Form1.txtFechaHospitalizacion2,Form1.txtFechaHospitalizacion2,'dd/mm/yyyy');"
                                    name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalAntecendentes2"
                                        runat="server"></a><asp:RequiredFieldValidator ID="rfvFechaHospitalizacion2" runat="server"
                                            CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaHospitalizacion2"
                                            ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3">
                                <asp:TextBox onkeydown="return keyDown(this,event)" MaxLength="5" onkeypress="return currencyFormat(this,event,true,false)"
                                    ID="txtDiasHospitalizacion2" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                Días
                            <asp:RequiredFieldValidator ID="rfvDiasHospitalizacion2" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtDiasHospitalizacion2" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table18" cellspacing="0" cellpadding="3" width="100%" align="center">
                                    <tr>
                                        <td>
                                            <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE4"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaHospitalizacion3" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                    href="javascript:MostrarCalendario(Form1.txtFechaHospitalizacion3,Form1.txtFechaHospitalizacion3,'dd/mm/yyyy');"
                                    name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalAntecendentes3"
                                        runat="server"></a><asp:RequiredFieldValidator ID="rfvFechaHospitalizacion3" runat="server"
                                            CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaHospitalizacion3"
                                            ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3">
                                <asp:TextBox onkeydown="return keyDown(this,event)" MaxLength="5" onkeypress="return currencyFormat(this,event,true,false)"
                                    ID="txtDiasHospitalizacion3" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                Días
                            <asp:RequiredFieldValidator ID="rfvDiasHospitalizacion3" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtDiasHospitalizacion3" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="22%">
                                <table id="Table19" cellspacing="0" cellpadding="3" width="100%" align="center">
                                    <tr>
                                        <td>
                                            <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE5"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtFechaHospitalizacion4" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                    href="javascript:MostrarCalendario(Form1.txtFechaHospitalizacion4,Form1.txtFechaHospitalizacion4,'dd/mm/yyyy');"
                                    name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalAntecendentes4"
                                        runat="server"></a><asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server"
                                            CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaHospitalizacion4"
                                            ErrorMessage="Requerido" Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3">
                                <asp:TextBox onkeydown="return keyDown(this,event)" MaxLength="5" onkeypress="return currencyFormat(this,event,true,false)"
                                    ID="txtDiasHospitalizacion4" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                Días
                            <asp:RequiredFieldValidator ID="rfvDiasHospitalizacion4" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtDiasHospitalizacion4" ErrorMessage="Requerido"
                                Enabled="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="recomendaciones">
                <div id="divRecomendaciones" runat="server" visible="False">
                    <table class="tableBorder" id="Table20" cellspacing="0" cellpadding="3" width="100%"
                        align="center">
                        <tr>
                            <td class="headerTable" colspan="4">RECOMENDACIONES&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">Se recomienda ingresar a alguno de los siguientes programas<br />
                                <asp:CheckBoxList ID="cblRecomendacion" runat="server" RepeatDirection="Vertical"
                                    CssClass="tableNoBorderSmall">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr>
        <td align="center">
            <asp:Label ID="lblCargandoConsulta1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblCargandoConsulta" runat="server" Text="" Style="text-align: left"></asp:Label>
        </td>
    </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="« Anterior" CausesValidation="False"
                    OnClick="btnAnterior_Click1"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnGuardar" runat="server"  CssClass="button" Text="Siguiente »"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" CssClass="button"
                Text="Cancelar"></asp:Button>&nbsp;<asp:ValidationSummary ID="valSum" runat="server"
                    Font-Names="verdana" Font-Size="12" ShowSummary="false" HeaderText="Para poder continuar debe llenar los campos obligatorios"
                    ShowMessageBox="true" />
            </td>
        </tr>
        </TBODY></table>
    <p>
        <br>
        <uc1:WC_BuscarPrestador ID="WC_BuscarPrestador1" runat="server"></uc1:WC_BuscarPrestador>
        <br>
        <uc1:WC_BuscarDiagnostico ID="WC_BuscarDiagnostico1" runat="server"></uc1:WC_BuscarDiagnostico>
        <br>
        <uc1:WC_BuscarProveedor ID="WC_BuscarProveedor1" runat="server"></uc1:WC_BuscarProveedor>
    </p>
        <p>
            <br>
            &nbsp;
        </p>
    </form>
</body>
</html>
