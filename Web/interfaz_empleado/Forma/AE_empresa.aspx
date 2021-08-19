<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2"  %>

<%@ Page Language="c#" CodeBehind="AE_empresa.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_empresa" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>

    <script language="javascript" src="../../scripts/Validaciones.js" type="text/javascript"></script>

    <%--GAMM - 21/08/2020 - Se actualiza versión de CKEditor 4--%>
    <script src="../../ckeditor/ckeditor.js" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">

        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }

            return false;
        }

        function FCKeditor_OnComplete(obj) {
        }

    
    </script>

    <style type="text/css">
        .styleDatosGenerales
        {
            width: 68px;
        }
    </style>
</head>
<body onload="CargarConfiguracion()">
    <form id="Form2" method="post" runat="server">
    <table id="tblPrincipal" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table class="tableBorder" id="table6" width="100%" align="center">
                    <tr>
                        <td>
                            <table id="Table1" cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="titleBackBlue" colspan="4">
                                        <asp:Label ID="lblTitulo" runat="server" Text="AGREGAR EMPRESA"></asp:Label>
                                    </td>
                                </tr>
                                <tr id = trSeleccion runat =server>
                                    <td align="center">Seleccione la acción que desea realizar
                                    </td>
                                </tr>
                                <tr id = trDdlSeleccion runat=server>
                                    <td align="center">
                                    <asp:DropDownList ID="ddlAccion" runat="server" AutoPostBack = true
                                            onselectedindexchanged="ddlAccion_SelectedIndexChanged" CssClass="textBox">
                                            <asp:ListItem Selected="True" Value="0">--Seleccione--</asp:ListItem>
                                            <asp:ListItem Value="1">Crear nueva empresa</asp:ListItem>
                                            <asp:ListItem Value="2">Actualizar empresa existente</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id=trLblActualizar runat=server style="DISPLAY: none">
                                    <td colspan="4" align="left">
                                        Seleccione la empresa que desea actualizar
                                        </td>
                                </tr>
                                <tr id=trActualizar runat=server style="DISPLAY: none">
                                    <td colspan="4" align="left">
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/iconos/edit.PNG" />
                                        &nbsp;
                                        <asp:DropDownList ID="ddlEmpresas" runat="server"  AutoPostBack=true
                                            onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" CssClass="textBox">                                            
                                        </asp:DropDownList>
                                        &nbsp; 
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr id=trWizzard runat=server style="DISPLAY: none">
                                    <td colspan="4">
                                        <asp:Wizard ID="WizardEmpresa" runat="server" BackColor="White" BorderColor="#B5C7DE"
                                            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" Width="779px" Height="16px"
                                            ActiveStepIndex="2" OnFinishButtonClick="WizardEmpresa_FinishButtonClick" OnNextButtonClick="WizardEmpresa_NextButtonClick">
                                            <StepStyle Font-Size="0.8em" ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <WizardSteps>
                                                <asp:WizardStep ID="wstDatosGenerales" runat="server" Title="Datos generales">
                                                    <table id="Table3" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label ID="lblDatosGenerales" runat="server" Text="Datos generales" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%">
                                                                <asp:Label ID="lblEmpresaHC" runat="server" Text="Empresa"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                            <td width="35%">
                                                                <asp:TextBox ID="txtNombreEmpresa" runat="server" Width="350px" 
                                                                    CssClass="textBox" MaxLength="255"></asp:TextBox>
                                                            </td>
                                                            <td width="5%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="35%" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%">
                                                                <asp:Label ID="lblEmpresaAbreviacion" runat="server" Text="Abreviatura empresa"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                            <td width="35%">
                                                                <asp:TextBox ID="txtEmpresaAbreviacion" runat="server" Width="250px" 
                                                                    CssClass="textBox" MaxLength="30"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="35%" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td>
                                                                <asp:Label ID="lblTipoCliente" runat="server" Text="Tipo de cliente"></asp:Label><span
                                                                    class="textRed">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoCliente" runat="server" Width="250px" CssClass="textBox">                                                                    
                                                                    <asp:ListItem Selected="True" Value="1">Jumbo</asp:ListItem>
                                                                    <asp:ListItem Value="2">Large</asp:ListItem>
                                                                    <asp:ListItem Value="3">Middlet Market</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="list">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textSmallBlack" colspan="4" height="20">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3" colspan="4">
                                                                <asp:Label ID="lblCamposObligatoriosGenerales" runat="server" Text="Los campos marcados con (&lt;SPAN class=&quot;textRed&quot;&gt;*&lt;/span&gt;)son obligatorios"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="wstDatosEmpresa" runat="server" Title="Datos Empresa">
                                                    <table id="Table2" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="lblDatosEmpresa" runat="server" Text="Datos de la empresa" Font-Bold="True"></asp:Label>
                                                                <hr/>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="styleDatosGenerales">
                                                                <asp:Label ID="lblDireccion" runat="server" Text="Dirección"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccion" CssClass="textBox" runat="server" 
                                                                    MaxLength="255" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="styleDatosGenerales">
                                                                <asp:Label ID="lblContacto" runat="server" Text="Contacto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtContacto" CssClass="textBox" runat="server" MaxLength="500" 
                                                                    Width="300px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="styleDatosGenerales">
                                                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelefono" CssClass="textBox" runat="server" MaxLength="100" 
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="styleDatosGenerales">
                                                                <asp:Label ID="lblCorreo" runat="server" Text="Correo"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCorreo" CssClass="textBox" runat="server" MaxLength="100" 
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="styleDatosGenerales">
                                                                <asp:Label ID="lblFax" runat="server" Text="Fax" MaxLength="100"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFax" CssClass="textBox" runat="server" Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="wstParentescos" runat="server" Title="Parentescos">
                                                <table id="Table4" cellspacing="0" style="display:none" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSeguridad" runat="server" Text="Seguridad" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDiasPassword" runat="server" Text="Días en los que caducará la contraseña:"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDiasPassword" Width="30" CssClass="textBox" runat="server">30</asp:TextBox>
                                                                <asp:Label ID="lblDias" runat="server" Text="Días"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblIntentosPassword" runat="server" Text="Cantidad de intentos fallidos en los que se bloqueará el usuario:"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtIntentosPassword" Width="30" CssClass="textBox" runat="server">3</asp:TextBox>
                                                                <asp:Label ID="lblIntentos" runat="server" Text="Intentos"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                <table id="Table11" style="display:none" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Seguridad" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Días en los que caducará la contraseña:"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" Width="30" CssClass="textBox" runat="server"></asp:TextBox>
                                                                <asp:Label ID="Label7" runat="server" Text="Días"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Cantidad de intentos fallidos en los que se bloqueará el usuario:"></asp:Label><span class="textRed">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBox2" Width="30" CssClass="textBox" runat="server"></asp:TextBox>
                                                                <asp:Label ID="Label9" runat="server" Text="Intentos"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    <table id="Table5" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblParentescos" runat="server" Text="Parentescos" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBoxList ID="chlParentescos" runat="server" RepeatColumns="2">
                                                                </asp:CheckBoxList>
                                                            </td>                                                            
                                                        </tr>                                                        
                                                    </table>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="wstPermisos" runat="server" Title="Permisos">
                                                    <table id="Table7" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPermisos" runat="server" Text="Permisos" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBoxList ID="chlPermisos" runat="server" RepeatColumns="1">                                                                    
                                                                    <asp:ListItem Value="0">Consultas: Permite a los usuarios registrar consultas en el sistema
                                                                    </asp:ListItem>                                                                    
                                                                    <asp:ListItem Value="1">Agenda citas: Permite a los usuarios agendar citas por medio del sistema
                                                                    </asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="wstSecciones" runat="server" Title="Secciones">
                                                    <table id="Table8" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Secciones" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Seleccionar: <a id="LnkTodas" href="#" onclick="javascript: CheckBoxListSelect ('<%= chlSecciones.ClientID %>',true)">
                                                                    Todas</a> <a id="LnkNinguna" href="#" onclick="javascript: CheckBoxListSelect ('<%= chlSecciones.ClientID %>',false)">
                                                                        | Ninguna</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBoxList ID="chlSecciones" runat="server" RepeatColumns="3">
                                                                    <asp:ListItem Value="0">Colesterol glicemia</asp:ListItem>
                                                                    <asp:ListItem Value="1">Exámenes laboratorio</asp:ListItem>
                                                                    <asp:ListItem Value="2">Mujer</asp:ListItem>
                                                                    <asp:ListItem Value="3">Audiometría</asp:ListItem>
                                                                    <asp:ListItem Value="4">Wellness</asp:ListItem>
                                                                    <asp:ListItem Value="5">Hábito fumar</asp:ListItem>
                                                                    <asp:ListItem Value="6">Consumo alcohol</asp:ListItem>
                                                                    <asp:ListItem Value="7">Vacunación</asp:ListItem>
                                                                    <asp:ListItem Value="8">Sedentarismo</asp:ListItem>
                                                                    <asp:ListItem Value="9">Salud oral</asp:ListItem>
                                                                    <asp:ListItem Value="10">Estrés</asp:ListItem>
                                                                    <asp:ListItem Value="11">Emocional</asp:ListItem>
                                                                    <asp:ListItem Value="12">Accidentalidad</asp:ListItem>
                                                                    <asp:ListItem Value="13">Estado salud</asp:ListItem>
                                                                    <asp:ListItem Value="14">Nutrición</asp:ListItem>
                                                                    <asp:ListItem Value="15">Antecedentes ausentismo</asp:ListItem>
                                                                    <asp:ListItem Value="16">Recomendaciones</asp:ListItem>
                                                                    <asp:ListItem Value="17">Diastolica sistólica</asp:ListItem>
                                                                    <asp:ListItem Value="18">Perímetro abdominal</asp:ListItem>                                                                                                                                   
                                                                    
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr id= "trNotaSecciones" style = "display:none" runat=server>
                                                            <td>
                                                           <span class=textRed>*</span> Nota: En caso de eliminar una seccion, esta no volvera a 
                                                                aparecer en la consulta médica pero seguiran apareciendo los registros en los 
                                                                reportes.                                                               
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table9" cellspacing="0" style="display:none" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTipoServicio" runat="server" Text="Tipos de servicios" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textSmallBlack" height="20">
                                                                <asp:CheckBoxList ID="ChlTipoServicios" runat="server" CssClass="list" RepeatColumns="2">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:WizardStep>                                                
                                                <asp:WizardStep ID="wstFormatos" runat="server" Title="Formatos">
                                                    <table id="Table10" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Formatos" Font-Bold="True"></asp:Label>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Ingrese el titulo de los formatos"></asp:Label>
                                                                &nbsp;(Es el titulo que saldra en los formatos de las órdenes)</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtTituloFormato" Width="300px" CssClass="textBox" 
                                                                    runat="server" MaxLength="250"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Ingrese el texto de los formatos"></asp:Label>
                                                                &nbsp;(Es el texto que saldra en la parte de abajo en los formatos de las órdenes)</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%--GAMM - 21/08/2020 - Se actualiza versión de CKEditor 4--%>

                                                               <%--<fckeditorv2:FCKeditor ID="fckEditorDetalle" runat="server" BasePath="../../fckeditor/" 
                                                                    ToolbarSet="Personal" Height="300px" Width="600px">
                                                                </fckeditorv2:FCKeditor>--%>
                                                                <textarea id="fckEditorDetalle" name="fckEditorDetalle" runat="server" rows="4" cols="2"></textarea>
                                                                    <script type="text/javascript">
                                                                        // Replace the <textarea id="editor1"> with a CKEditor 4 instance, using default configuration.
                                                                        CKEDITOR.replace('WizardEmpresa_fckEditorDetalle', {
                                                                            //uiColor: '#4268A0',
                                                                            //toolbar: [
                                                                            //    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink'],
                                                                            //    ['FontSize', 'TextColor', 'BGColor']
                                                                            //],
                                                                            width: ['700px'],
                                                                            height: ['150px']
                                                                        });
                                                                    </script> 
                                                            </td>
                                                        </tr>                                                        
                                                    </table>
                                                </asp:WizardStep>
                                                <asp:TemplatedWizardStep ID="TemplatedWizardStep1" runat="server" Title="Finalizar">
                                                    <ContentTemplate>
                                                    <table id="Table10" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFinalizar" runat="server" 
                                                            Text="Clic en finalizar para continuar con la asociación de la empresa"></asp:Label>
                                                            </td>
                                                        </tr>                                                                                                            
                                                    </table>                                                        
                                                    </ContentTemplate>
                                                </asp:TemplatedWizardStep>
                                            </WizardSteps>
                                            <SideBarButtonStyle BackColor="#4268A0" Font-Names="Verdana" ForeColor="White" />
                                            <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                                            <SideBarStyle Width="17%" BackColor="#4268A0" Font-Size="0.9em" VerticalAlign="Top" />
                                            <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
                                                Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
                                        </asp:Wizard>
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;
    </form>
</body>
</html>
