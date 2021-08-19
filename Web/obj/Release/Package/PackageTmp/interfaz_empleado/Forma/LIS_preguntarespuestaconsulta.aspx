<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LIS_preguntarespuestaconsulta.aspx.cs" Inherits="TPA.interfaz_admon.forma.LIS_preguntarespuestaconsulta" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HC-Historias Clínicas</title>
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>

</head>
<body>
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
                            Pregunta</td>
                        <td>
                            <asp:TextBox ID="txtPregunta" runat="server" CssClass="textBox" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Respuesta</td>
                        <td>
                            <asp:TextBox ID="txtRespuesta" runat="server" CssClass="textBox" Width="250px"></asp:TextBox>
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
            <td align="center">
                <asp:GridView ID="grvPregunta" runat="server" CssClass="grid" Width="90%" AllowPaging="True"
                    CellPadding="2" AutoGenerateColumns="False" PageSize="20" 
                    onrowcommand="grvPregunta_RowCommand">
                    <AlternatingRowStyle CssClass="altItems" />
                    <RowStyle CssClass="norItems" />
                    <HeaderStyle CssClass="headerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton BackColor="Transparent" ID="imgEditarMed" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdPreguntaRespuesta") %>'
                                    runat="server" ImageUrl="../../iconos/ico_editar.gif"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Pregunta" HeaderText="Pregunta">
                            <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Respuesta" HeaderText="Respuesta">
                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Seccion" HeaderText="Seccion">
                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Activa" HeaderText="Activa">
                            <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="Puntuacion" HeaderText="Puntuacion">
                            <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
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
