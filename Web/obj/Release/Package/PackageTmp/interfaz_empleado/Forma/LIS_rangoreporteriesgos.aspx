<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LIS_rangoreporteriesgos.aspx.cs"
    Inherits="TPA.interfaz_admon.forma.LIS_rangoreporteriesgos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HC-Historias Clínicas</title>
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>

</head>
<body onload="CargarConfiguracion();">
    <form id="form1" runat="server">
    <table class="GG" cellspacing="0" cellpadding="10" width="100%" align="center">
        <tr>
            <td>
                <table class="tableBorder" id="Table2" cellspacing="0" cellpadding="5" width="45%"
                    align="center">
                    <tr>
                        <td class="titleBackBlue" background="../../iconos/fondo_main.PNG" colspan="2">
                            Buscar Rangos
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Reporte Riesgo
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReporte" runat="server" CssClass="textBox" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p align="center">
                                <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="Buscar" OnClick="btnBuscar_Click">
                                </asp:Button></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" CssClass="textoDestacado"></asp:Label><br>
                <a>
                    <asp:ImageButton ID="imbAdicionar" runat="server" ImageUrl="../../iconos/ico_adicionar.gif"
                        OnClick="imbAdicionar_Click"></asp:ImageButton>&nbsp;</a>
                <asp:LinkButton ID="lnkAdicionar" runat="server" OnClick="lnkAdicionar_Click">Adicionar Rango</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grvRango" runat="server" CssClass="grid" Width="90%" AllowPaging="True"
                    CellPadding="2" AutoGenerateColumns="False" PageSize="20" 
                    onrowcommand="grvRango_RowCommand">
                    <AlternatingRowStyle CssClass="altItems" />
                    <RowStyle CssClass="norItems" />
                    <HeaderStyle CssClass="headerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton BackColor="Transparent" ID="imgEditarMed" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdRangoRiesgos") %>'
                                    runat="server" ImageUrl="../../iconos/ico_editar.gif"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RangoRiegosNombre" HeaderText="Nombre Rango">
                            <ItemStyle HorizontalAlign="Center" Width="22%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReporteRiesgo" HeaderText="Reporte">
                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LimiteInferior" HeaderText="Límite Inferior">
                            <ItemStyle HorizontalAlign="Center" Width="22%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LimiteSuperior" HeaderText="Límite Superior">
                            <ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
