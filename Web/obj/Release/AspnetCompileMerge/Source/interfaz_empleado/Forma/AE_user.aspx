<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>

<%@ Page Language="c#" CodeBehind="AE_user.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_user" %>

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

    <script src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script src="../../scripts/ValidaEntradaDatos.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 15%;
        }
        .style2
        {
            width: 13%;
        }
        .style3
        {
            height: 20px;
        }
        .style4
        {
            border: 0px solid #ffffff;
            padding: 0px;
            font-size: 11px;
            color: black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            letter-spacing: normal;
            background-color: #ffffff;
            height: 20px;
        }
        .style5
        {
            width: 308px;
        }
    </style>
</head>
<body onload="CargarConfiguracion()">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="scrMng" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">

        function AsociarPrestador(IdPrestador, nombre) {
            //var txtIdPrestador = document.getElementById('txtIdPrestador');
            var txtIdPrestador = "idprestador";
            var txtPrestador = document.getElementById('txtPrestador');
            txtIdPrestador.value = IdPrestador;
            txtPrestador.value = nombre;
        }
			
    </script>

    <table id="tblPrincipal" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                <table class="tableBorder" id="table6" width="75%" align="center">
                    <tr>
                        <td>
                            <table id="Table1" cellspacing="0" cellpadding="3" border="0">
                                <tr>
                                    <td class="titleBackBlue" colspan="3">
                                        USUARIO
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <%--                     <tr>
                                    <td class="style1">
                                    </td>
                                    <td class="style5">
                                    </td>
                                    <td align="left" valign="top" rowspan="4">
                                        <table id="tblDatos" cellspacing="0" cellpadding="3" border="0">
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style3">
                                                </td>
                                                <td class="style4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td class="list">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td class="list">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                    </td>
                                    <td class="style5">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <td class="style5">
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="style5">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="3">
                                        <table cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td class="style1">
                                                    Nombre <span class="textRed">*</span>
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtNombre" runat="server" Width="200px" CssClass="textBox cadena"
                                                        TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" CssClass="textRed" ErrorMessage="Requerido"
                                                        ControlToValidate="txtNombre" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlEmpresa" Visible="true" runat="server">
                                                        <table id="tblEmpresa" cellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td class="style2">
                                                                    Empresa <span class="textRed">*</span>
                                                                </td>
                                                                <td width="35%" align="left">
                                                                    <asp:DropDownList ID="ddlCompania" runat="server" Width="160px" CssClass="textBox"
                                                                        TabIndex="2">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    Email<span class="textRed">*</span>
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="textBox cadena"
                                                        TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="textRed" ErrorMessage="Requerido"
                                                        ControlToValidate="txtEmail" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revContrasena" runat="server" CssClass="textRed"
                                                        ErrorMessage="Ingrese un email válido" ControlToValidate="txtEmail" Display="Dynamic"
                                                        ForeColor=" " ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                                <td class="style1">
                                                    Usuario Acceso<span class="textRed">*</span>
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="textBox cadena"
                                                        MaxLength="25" TabIndex="2" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" CssClass="textRed" ErrorMessage="Requerido"
                                                        ControlToValidate="txtUsuario" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                           <%-- <tr>
                                                <td class="style1">
                                                    Contraseña <span class="textRed">*</span>
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtContrasena" runat="server" Width="100px" CssClass="textBox" TextMode="Password"
                                                        MaxLength="10" TabIndex="5" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" CssClass="textRed"
                                                        ErrorMessage="Requerido" ControlToValidate="txtContrasena" Display="Dynamic"
                                                        ForeColor=" "></asp:RequiredFieldValidator><br />
                                                    <asp:RegularExpressionValidator ID="revCaracteresEspeciales" runat="server" CssClass="textRed"
                                                        ControlToValidate="txtContrasena" Enabled="false" Display="Dynamic" ErrorMessage="La contraseña debe estar entre 15 y 32 caracteres, contener al menos un número y una letra y no debe contener caracteres especiales."
                                                        ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" ForeColor=" "></asp:RegularExpressionValidator>
                                                </td>
                                                <td class="style1">
                                                    Confirmación Contraseña <span class="textRed">*</span>
                                                </td>
                                                <td class="List">
                                                    <asp:TextBox ID="txtConfContrasena" runat="server" Width="100px" CssClass="textBox"
                                                        TextMode="Password" TabIndex="6" MaxLength="17" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvConfContrasena" runat="server" CssClass="textRed"
                                                        ErrorMessage="Requerido" ControlToValidate="txtContrasena" Display="Dynamic"
                                                        ForeColor=" "></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cmvConfirmacion" runat="server" CssClass="textRed" ErrorMessage="La confirmación es diferente a la contraseña"
                                                        ControlToValidate="txtConfContrasena" Display="Dynamic" ForeColor=" " ControlToCompare="txtContrasena"></asp:CompareValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="style1">
                                                    Estado <span class="textRed">*</span>
                                                </td>
                                                <td class="style5">
                                                    <asp:RadioButtonList ID="rblEstado" runat="server" CssClass="list" RepeatDirection="Horizontal"
                                                        TabIndex="7">
                                                        <asp:ListItem Value="True" Selected="True">Activo</asp:ListItem>
                                                        <asp:ListItem Value="False">Inactivo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td class="style1">
                                                    Ciudad <span class="textRed">*</span>
                                                </td>
                                                <td class="List">
                                                    <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textBox" Width="160px"
                                                        TabIndex="8">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="ddlCiudad" ErrorMessage="Requerido" Operator="NotEqual"
                                                        ValueToCompare="0"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="chkBypassMfa" runat="server" Text="ByPass MFA" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="20" colspan="4" class="textSmallBlack" style="color: #2f4f64">
                                                    Al recuperar al usuario y requiere resetear la contraseña, solo debe presionar el boton de Aceptar.
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="gainsboro" colspan="3" height="25">
                                        <p>
                                            <strong>Usuario prestador</strong></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Si el usuario a ingresar es un nuevo medico
                                        <asp:LinkButton ID="LnkNuevoMedico" runat="server" Font-Bold="True" CausesValidation="False"
                                            OnClick="LnkNuevoMedico_Click">Clic Aqui</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Asociar con medico existente&nbsp;
                                        <asp:TextBox ID="txtPrestador" runat="server" CssClass="textBox" Width="170px" Visible="True">
<%--                                            ReadOnly="True">--%>

                                        </asp:TextBox>&nbsp;<input class="buttonSmall" id="btnBuscarPrestador"
                                                onclick="javascript:ShowPrestador(this);" type="button" value="..." name="btnBuscarPrestador"
                                                runat="server">
                                        <asp:TextBox ID="txtIdPrestador" runat="server" Width="0px"></asp:TextBox>
                                        <asp:TextBox ID="txtIdProveedor" runat="server" Width="0px"></asp:TextBox>
                                        <asp:LinkButton ID="LnkModificar" runat="server" Font-Bold="True" CausesValidation="False"
                                            OnClick="LnkModificar_Click">Modificar datos del medico</asp:LinkButton>
                                        <uc1:WC_BuscarPrestador ID="WC_BuscarPrestador1" runat="server"></uc1:WC_BuscarPrestador>
                                    </td>
                                    <tr>
                                        <td bgcolor="gainsboro" colspan="3" height="25">
                                            <strong>Permisos</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textSmallBlack" colspan="3" height="20">
                                            <asp:CheckBoxList ID="chlPermisos" runat="server" CssClass="list" TabIndex="9">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="gainsboro" colspan="3" height="25">
                                            <strong>Reportes</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textSmallBlack" colspan="3" height="20">
                                            <asp:CheckBoxList ID="ChlReportes" runat="server" CssClass="list" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="gainsboro" colspan="3" height="25">
                                            <strong>Tipo Servicios</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textSmallBlack" colspan="3" height="20">
                                            <asp:CheckBoxList ID="ChlTipoServicios" runat="server" CssClass="list" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textSmallBlack" colspan="3" height="20">
                                            Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Button ID="btnAceptar" runat="server" CssClass="button" Text="Aceptar" TabIndex="10">
                                            </asp:Button>
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

<script type="text/javascript">
    $(document).ready(function() {
        ValidaInput();
    });
</script>

</html>
