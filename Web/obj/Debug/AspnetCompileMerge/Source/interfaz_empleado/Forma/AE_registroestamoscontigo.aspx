<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_registroestamoscontigo.aspx.cs"
    Inherits="TPA.interfaz_empleado.forma.registroestamoscontigo" %>

<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnosticoConsulta" Src="../WebControls/WC_AdicionarDiagnosticoConsulta.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>
<%@ Register Src="../WebControls/WC_AdicionarDiagnosticoConsultaCIE10.ascx" TagName="WC_AdicionarDiagnosticoConsultaCIE10"
    TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
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

    <script language="javascript" type="text/javascript">

function MostrarApartados(Apartado)
		{
		    if (Apartado == 1)
		    {
		        div = document.getElementById("divAlimentacionInadecuada");
                div.style.display = "";
                document.getElementById("lnkMostrar").style.display = "none";;
                document.getElementById("lnkNoMostrar").style.display = "";
		    }
		    
		}
    </script>

</head>
<body leftmargin="5" topmargin="1" onload="CargarConfiguracion()"
    rightmargin="5">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="scrMng" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0">
        <tbody>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;
                    <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                </td>
            </tr>            
            <tr>
                <td align="left" colspan="2">
                    Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;
                    <fieldset class="FieldSet" style="width: 97%">
                        <legend>
                            <img src="../../images/icoHistoria.gif" border="0">
                            &nbsp;¡Estamos Contigo!</legend>
                        <div id="divLinksApartados" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                <ContentTemplate>
                                    <table id="Table3" cellspacing="0" cellpadding="3" width="95%" align="center">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkMostrarHAS" class="accHAS" runat="server" OnClick="lnkMostrarHAS_Click"><b>Presione
                                                aquí</b> para visualizar el formulario de hipertensión arterial sistemica <b>HAS</b></a></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkMostrarDM" class="accHAS" runat="server" OnClick="lnkMostrarDM_Click"><b>Presione
                                                aquí</b> para visualizar el formulario de diabetes mellitus <b>DM</b></a></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkMostrarColesterol" class="accHAS" runat="server" OnClick="lnkMostrarColesterol_Click"><b>Presione
                                                aquí</b> para visualizar el formulario de dislipidemias <b>Colesterol Total, HDL y LDL</b></a></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkMostrarTrigliceridos" class="accHAS" runat="server" OnClick="lnkMostrarTrigliceridos_Click"><b>Presione
                                                    aquí</b> para visualizar el formulario de <b>Trigliceridos</b></a></asp:LinkButton>
                                            </td>
                                            <tr>
                                                <asp:Label ID="lblTipoPadecimiento" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblTipoPadecimientoAnterior" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblEstadoHAS" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblEstadoDM" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblEstadoDislipidemias" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblEstadoTrigliceridos" runat="server" Text="" style="display: none"></asp:Label>
                                                <asp:Label ID="lblPadeciminetoAnterior" runat="server" Text="" style="display: none"></asp:Label>
                                                <td align="center" colspan="4">
                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel29"
                                                        runat="server" DisplayAfter="1">
                                                        <ProgressTemplate>
                                                            <div align="center" style="font-weight: bold">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/iconos/LoadingBlueSmall.gif" />
                                                            </div>
                                                            <div align="center" style="font-weight: bold">
                                                                Cargando componentes
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div id="divHAS" runat="server" visible="false">
                                                    <table class="tableBorder" id="Table9" cellspacing="0" cellpadding="3" width="100%"
                                                        align="center">
                                                        <tr>
                                                            <td class="headerTable" colspan="6">
                                                                HAS&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado la presión arterial en los últimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rblPresionArterial30Dias" AutoPostBack="true" runat="server"
                                                                    Width="112px" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblPresionArterial30Dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="rfvPresionArterial30Dias" runat="server" CssClass="textRed"
                                                                    ForeColor=" " Display="Dynamic" ControlToValidate="rblPresionArterial30Dias"
                                                                    ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <div id="divPresionArterial" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo la presión arterial<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaPresionArterial30dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaPresionArterial30dias,Form1.txtFechaPresionArterial30dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalHAS"
                                                                            runat="server"></a>
                                                                    <asp:RequiredFieldValidator ID="rfvFechaPresionArterial30dias" runat="server" CssClass="textRed"
                                                                        ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaPresionArterial30dias" ErrorMessage=""
                                                                        Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="margin-left: 40px">
                                                                    Si se ha tomado la presión arterial, ¿Qué valor obtuvo?<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorPresionArterial30dias" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div id="divDM" runat="server" visible="false">
                                                    <table class="tableBorder" id="Table2" cellspacing="0" cellpadding="3" width="100%"
                                                        align="center">
                                                        <tr>
                                                            <td class="headerTable" colspan="6">
                                                                DIABETES MELLITUS(DM)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado el valor de la glucosa en la sangre en los ultimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rblGlucosa30dias" AutoPostBack="true" runat="server" Width="112px" 
                                                                    RepeatDirection="Horizontal" 
                                                                    onselectedindexchanged="rblGlucosa30dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>                                                                
                                                            </td>
                                                        </tr>
                                                        <div id="divSiDiabetesMellitus" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo el examen de glucosa<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaGlucosa30dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaGlucosa30dias,Form1.txtFechaGlucosa30dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalMD"
                                                                            runat="server"></a>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de glucosa, ¿Qué valor obtuvo?<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorGlucosa30dias" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div id="divDislipidemias" runat="server" visible="false">
                                                    <table class="tableBorder" id="Table4" cellspacing="0" cellpadding="3" width="100%"
                                                        align="center">
                                                        <tr>
                                                            <td class="headerTable" colspan="6">
                                                                DISLIPIDEMIAS
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado el valor de colesterol en sangre en los ultimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rblColesterolTotal30Dias" AutoPostBack="true" runat="server" Width="112px" 
                                                                    RepeatDirection="Horizontal" 
                                                                    onselectedindexchanged="rblColesterolTotal30Dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>                                                                
                                                            </td>
                                                        </tr>
                                                        <div id="divSiColesterolTotal" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo el examen de colesterol total<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaColesterolTotal30Dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaColesterolTotal30Dias,Form1.txtFechaColesterolTotal30Dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalColTotal"
                                                                            runat="server"></a>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de colesterol total, ¿Qué valor obtuvo?<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorColesterolTotal30Dias" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado el valor de colesterol HDL en los ultimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rblColesterolHDL30Dias" AutoPostBack="true" runat="server" Width="112px" 
                                                                    RepeatDirection="Horizontal" 
                                                                    onselectedindexchanged="rblColesterolHDL30Dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>                                                                
                                                            </td>
                                                        </tr>
                                                        <div id="divSiHDL" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo el examen de colesterol HDL<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaColesterolHDL30Dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaColesterolHDL30Dias,Form1.txtFechaColesterolHDL30Dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalHDL"
                                                                            runat="server"></a>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de colesterol HDL, ¿Qué valor obtuvo?<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorColesterolHDL30Dias" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado el valor de colesterol LDL en los ultimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rblColesterolLDL30Dias" AutoPostBack="true" runat="server" Width="112px" 
                                                                    RepeatDirection="Horizontal" 
                                                                    onselectedindexchanged="rblColesterolLDL30Dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>                                                                
                                                            </td>
                                                        </tr>
                                                        <div id="divSiLDL" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo el examen de colesterol LDL<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaColesterolLDL30Dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaColesterolLDL30Dias,Form1.txtFechaColesterolLDL30Dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalLDL"
                                                                            runat="server"></a>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de colesterol LDL, ¿Qué valor obtuvo?<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorColesterolLDL30Dias" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div id="divTrigliceridos" runat="server" visible="false">
                                                    <table class="tableBorder" id="Table5" cellspacing="0" cellpadding="3" width="100%"
                                                        align="center">
                                                        <tr>
                                                            <td class="headerTable" colspan="6">
                                                                TRIGLICÉRIDOS
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ¿Se ha tomado el valor de triglicéridos en los ultimos 30 días?<span class="textRed">*</span><br />
                                                                <asp:RadioButtonList ID="rdlTrigliceridos30Dias" AutoPostBack= true runat="server" Width="112px" 
                                                                    RepeatDirection="Horizontal" 
                                                                    onselectedindexchanged="rdlTrigliceridos30Dias_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>                                                                
                                                            </td>
                                                        </tr>
                                                        <div id="divSiTrigliceridos" runat="server" visible="false">
                                                            <tr>
                                                                <td>
                                                                    Indique la fecha que se tomo el examen de triglicéridos<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaTrigliceridos30Dias" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaTrigliceridos30Dias,Form1.txtFechaTrigliceridos30Dias,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalTrigliceridos"
                                                                            runat="server"></a>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de triglicéridos, ¿Qué valor obtuvo? (Hombres)<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorTrigliceridos30DiasHombres" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Si se ha tomado el examen de triglicéridos, ¿Qué valor obtuvo? (Mujeres)<span class="textRed">*</span><br />
                                                                    <asp:DropDownList ID="ddlValorTrigliceridos30DiasMujeres" runat="server" CssClass="textBox" Visible="True">
                                                                    </asp:DropDownList>                                                                    
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <div id="divGenerales" runat="server" visible =false>
                                                    <table class="tableBorder" id="Table1" cellspacing="0" cellpadding="3" width="100%"
                                                        align="center">
                                                        <tr>
                                                            <td class="headerTable" colspan="6">
                                                                GENERALES&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVisitaNutriologo" runat="server" Text="¿Ha visitado al nutriólogo debido a su padecimiento?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                   <asp:RadioButtonList ID="rblVisitaNutriologo" AutoPostBack = true Width="112px" 
                                                                        RepeatDirection="Horizontal" runat="server" 
                                                                        onselectedindexchanged="rblVisitaNutriologo_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblApegadoDieta" runat="server" 
                                                                    Text="¿Se ha apegado a la dieta que se le estableció para su padecimiento?" 
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblApegadoDieta" Width="112px" RepeatDirection="Vertical" Visible =false runat="server">
                                                                    <asp:ListItem Value="1">Si, totalmente</asp:ListItem>
                                                                    <asp:ListItem Value="0">Parcialmente</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNoVisitaNutriologo" runat="server" 
                                                                    Text="¿Por que no ha visitado al nutriólogo por su padecimiento?" 
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtNoVisitaNutriologo" runat="server" CssClass="textBox" Visible =false
                                                        Width="368px" Height="40px" MaxLength="500" TextMode="MultiLine" onkeypress='return (this.value.length < 500);'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMedicoTratamiento" runat="server" Text="¿Cuenta con un médico tratante responsable del tratamiento de su enfermedad?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                <ContentTemplate>
                                                                   <asp:RadioButtonList ID="rblMedicoTratamiento" AutoPostBack = true runat="server"
                                                                    Width="112px" RepeatDirection="Horizontal" 
                                                                        onselectedindexchanged="rblMedicoTratamiento_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                                                                                           
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblIndicacionesMedico" runat="server" 
                                                                    Text="¿Ha cumplido con el tratamiento según las indicaciones de su médico tratante?" 
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                   <asp:DropDownList  ID="ddlIndicacionesMedico" AutoPostBack = true runat="server" CssClass="textBox" 
                                                                        Visible=false
                                                                        onselectedindexchanged="ddlIndicacionesMedico_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>                                                                                                                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNoCumpleTratamientos" Visible =false runat="server" Text="¿Por que no ha cumplido con el tratamiento según las indicaciones de su médico tratante?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtNoCumpleTratamientos" runat="server" CssClass="textBox" 
                                                        Width="368px" Height="40px" MaxLength="500" TextMode="MultiLine" 
                                                                    onkeypress='return (this.value.length < 500);' Visible="False"></asp:TextBox>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblComplicacion" runat="server" Text="¿Ha presentado alguna complicación de su enfermedad desde la última visita a su médico tratante?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               <asp:CheckBoxList ID="cblComplicacionHAS" runat="server" RepeatColumns="2" Visible =false>
                                                                    </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               <asp:CheckBoxList ID="cblComplicacionDM" runat="server" RepeatColumns="2" Visible =false>
                                                                    </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               <asp:CheckBoxList ID="cblComplicacionDislipidemias" runat="server" RepeatColumns="2" Visible =false>
                                                                    </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               <asp:CheckBoxList ID="cblComplicacionTrigliceridos" runat="server" RepeatColumns="2" Visible =false>
                                                                    </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Otra complicación&nbsp; <asp:TextBox ID="txtOtraComplicacion" runat="server" CssClass="textBox" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMedicamentos" runat="server" Text="¿Qué medicamentos utiliza actualmente para el control de su enfermedad?"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtMedicamentos" runat="server" CssClass="textBox" Width="550px"
                                                                    MaxLength="500" onkeypress='return (this.value.length < 500);' TextMode="MultiLine"
                                                                    Height="40px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblRecomendaciones" runat="server" Text="Recomendaciones"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtRecomendaciones" runat="server" CssClass="textBox" Width="550px"
                                                                    MaxLength="500" onkeypress='return (this.value.length < 500);' TextMode="MultiLine"
                                                                    Height="40px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                                <td>
                                                                    Fecha siguiente cita<span class="textRed">*</span><br />
                                                                    <asp:TextBox ID="txtFechaSiguienteCita" runat="server" CssClass="textBox" Width="130px"></asp:TextBox>&nbsp;<a
                                                                        href="javascript:MostrarCalendario(Form1.txtFechaSiguienteCita,Form1.txtFechaSiguienteCita,'dd/mm/yyyy');"
                                                                        name="btnFecha0"><img src="../../images/icoCalendar.gif" border="0" id="imgCalCita"
                                                                            runat="server"></a> 
                                                                       <asp:RequiredFieldValidator ID="rfvFechaSiguienteCita" runat="server" CssClass="textRed"
                                                                    ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaSiguienteCita"
                                                                    ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>                                                                   
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        </div>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </td>
            </tr>
            <tr>
        <td align="center" colspan="2">
            <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="« Anterior" 
                CausesValidation="False" onclick="btnAnterior_Click"
               ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                    ID="btnGuardar" runat="server" CssClass="button" Text="Siguiente »" 
                onclick="btnGuardar_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" CssClass="button"
                Text="Cancelar" onclick="btnCancelar_Click"></asp:Button>&nbsp;<asp:ValidationSummary ID="valSum" runat="server"
                    Font-Names="verdana" Font-Size="12" ShowSummary="false" HeaderText="Para poder continuar debe llenar los campos obligatorios"
                    ShowMessageBox="true" />
        </td>
    </tr>            
    </table>
    </form>
</body>
</html>
