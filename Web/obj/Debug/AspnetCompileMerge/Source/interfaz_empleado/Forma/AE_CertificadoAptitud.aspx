<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_CertificadoAptitud.aspx.cs" Inherits="Web.interfaz_empleado.Forma.AE_CertificadoApitud" %>

<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>

    <link href="../../css/Calendar.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 25%;
        }

        .style2 {
            color: #006600;
        }

        .style3 {
            color: #003300;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="GG" cellspacing="0" cellpadding="10" width="100%" align="center" border="0">

            <tr>
                <td align="center">
                    <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlCerrarHistoria" runat="server">
                        <fieldset class="FieldSet" style="width: 95%" id="FIELDSET1" runat="server">
                            <legend>
                                <img src="../../iconos/ico_Documento.gif" border="0">&nbsp; Certificado de aptitud</legend>
                            <br>
                            <table id="tblCerrarHistoria" style="width: 100%;">
                                <tr>
                                    <td class="style1">
                                        <asp:LinkButton ID="lnkDescarga" runat="server" OnClick="lnkDescarga_Click">Descargar</asp:LinkButton>

                                    </td>

                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <p align="center">
                        <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="« Anterior" ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFinalizar" runat="server"
                            CssClass="button" Text="Finalizar"></asp:Button>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    document.getElementById("btnFinalizar").focus();

</script>