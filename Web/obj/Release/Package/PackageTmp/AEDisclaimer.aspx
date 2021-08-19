<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AEDisclaimer.aspx.cs" Inherits="Web.AEDisclaimer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Disclaimer</title>
    <link href="css/admon.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .footer input
        {
            float: right;
            margin-left: 10px;
        }
        .footer
        {
            margin-right: auto;
            margin-left: auto;
            padding-top: 10px;
            width: 975px;
        }
        #pnContenidoDisclaimer
        {
            width: 945px;
            height: 550px;
            overflow-y: auto;
            overflow-x: hidden;
            margin-top: 10px;
            margin-left: auto;
            margin-right: auto;
            border: solid 1px silver;
            padding: 10px 20px 10px 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="table1" cellspacing="0" cellpadding="0" width="975px" border="0" style="margin-left: auto;
        margin-right: auto; margin-top: 15px">
        <tr>
            <td align="left" background="" colspan="2" height="60">
                <table height="80" cellspacing="1" cellpadding="12" width="100%">
                    <tr>
                        <td>
                            <img alt="" src="images/NuevoBanner.jpg" />
                        </td>
                        <td class="titleBigTPA" align="right">
                            HC
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnContenidoDisclaimer" runat="server">
    </asp:Panel>
    <div class="footer">
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="button" OnClick="btnCancelar_Click" />
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="button" OnClick="btnAceptar_Click" />
    </div>
    </form>
</body>
</html>
