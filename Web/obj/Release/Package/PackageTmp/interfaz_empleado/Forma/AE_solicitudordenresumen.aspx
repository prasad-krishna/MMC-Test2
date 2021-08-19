<%@ Register TagPrefix="uc1" TagName="WC_DatosConsulta" Src="../WebControls/WC_DatosConsulta.ascx" %>

<%@ Page Language="c#" CodeBehind="AE_solicitudordenresumen.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.interfaz_empleado.forma.AE_solicitudordenresumen" ValidateRequest="false" %>

<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
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
    
    <script type="text/javascript">

        function OcultarFechaPrivacidad() {

            var lblFechaPrivacidad = document.getElementById('lblFechaPrivacidad')
            var imgPapanicolau = document.getElementById('imgPapanicolau')
            var chkAvisoPrivacidad = document.getElementById('chkAvisoPrivacidad')
            var txtFechaAvisoPrivacidad = document.getElementById('txtFechaAvisoPrivacidad');

            if (chkAvisoPrivacidad.checked == true) {

                ValidatorEnable(document.getElementById("rfvFechaAvisoPrivacidad"), true);
                lblFechaPrivacidad.style.display = "";
                imgPapanicolau.style.display = "";
                txtFechaAvisoPrivacidad.style.display = "";                                
            }
            else {

                ValidatorEnable(document.getElementById("rfvFechaAvisoPrivacidad"), false);
                lblFechaPrivacidad.style.display = "none";
                imgPapanicolau.style.display = "none";
                txtFechaAvisoPrivacidad.style.display = "none";
            }

        }
	    
    </script>
    
    
    <style type="text/css">
        .style1
        {
            width: 25%;
        }
        .style2
        {
            color: #006600;
        }
        .style3
        {
            color: #003300;
        }
    </style>
</head>
<body onload="CargarConfiguracion();" leftmargin="5" topmargin="5" rightmargin="5">
    <form id="Form1" method="post" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table class="GG" cellspacing="0" cellpadding="10" width="100%" align="center" border="0">
       
        <tr>
            <td align="center">
                <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <uc1:WC_DatosConsulta ID="WC_DatosConsulta1" runat="server"></uc1:WC_DatosConsulta>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;&nbsp;
                <fieldset class="FieldSet" style="width: 99%">
                    <legend>
                        <img src="../../images/icoSolicitud.gif" border="0">
                        &nbsp;Órdenes</legend>
                    <br>
                    <table id="Table1" cellspacing="0" cellpadding="1" width="98%" align="center">
                        <tbody>
                            <tr>
                                <td>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DataList ID="dtlTipoServicio" runat="server" Width="100%" CellPadding="8">
                                        <ItemTemplate>
                                            <div id="divDataList" runat="server">
                                                <asp:TextBox ID="txtConsecutivoNombre" runat="server" CssClass="textBigGreen" Text='<%# DataBinder.Eval(Container, "DataItem.ConsecutivoTipoServicioNombre") %>'>
                                                </asp:TextBox>
                                                <table class="tableBorder" id="Table4" cellspacing="0" cellpadding="8" width="100%"
                                                    border="0">
                                                    <tr class="headerGrid">
                                                        <td>
                                                            Tipo de Servicio
                                                        </td>
                                                        <td>
                                                            Diagnosticos
                                                        </td>
                                                        <td>
                                                            Prestadores
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:Label ID="lblTipoServicio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio") %>'>
                                                            </asp:Label>
                                                            <asp:Label ID="lblIdTipoServicio" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>'>
                                                            </asp:Label>
                                                        <td>
                                                            <asp:Label ID="lblDiagnosticos" CssClass="textSmallBlack" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProveedor" runat="server" CssClass="textSmallBlack"></asp:Label>
                                                            <asp:Label ID="lblEspecialidad" runat="server" CssClass="textSmallBlack"></asp:Label>
                                                        </td>
                                                        <td style="display: none">
                                                            Tipo Atención:&nbsp;
                                                            <asp:Label ID="lblTipoAtencion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoAtencion") %>'>
																		<br />
                                                            </asp:Label><br />
                                                            Clase Atención:&nbsp;
                                                            <asp:Label ID="lblClaseAtencion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreClaseAtencion") %>'>
																		<br />
                                                            </asp:Label><br />
                                                            Contigencia:&nbsp;
                                                            <asp:Label ID="lblContingencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreContingencia") %>'>
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblPrestadorTipoServicio" runat="server" Text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.NombrePrestador")) ? "Solicitante: " + DataBinder.Eval(Container, "DataItem.NombrePrestador") : "" %>'>
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:DataGrid ID="dtgProductoServicio" runat="server" Width="100%" CssClass="grid"
                                                    CellPadding="0" AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal"
                                                    OnItemDataBound="dtgProductoServicio_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
                                                    <ItemStyle CssClass="norItems"></ItemStyle>
                                                    <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Servicio/Producto">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblProductoServicio" Text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="190px"></ItemStyle>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Cantidad/Dosis">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' runat="server"
                                                                    ID="lblCantidad" />
                                                                <br />
                                                                <asp:Label ID="lblDosis" Visible="false" Text='<%# "Dosis:" + DataBinder.Eval(Container, "DataItem.Dosis").ToString() %>'
                                                                    runat="server">
                                                                </asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblViaAdministracion" Visible="false" Text='<%# "Vía Administración:" + DataBinder.Eval(Container, "DataItem.ViaAdministracion").ToString() %>'
                                                                    runat="server">
                                                                </asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblDuracion" Visible="false" Text='<%# "Duración:" + DataBinder.Eval(Container, "DataItem.Duracion").ToString() %>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="250px"></ItemStyle>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Fecha Prestación" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaPrestacion")) %>'
                                                                    ID="lblFechaPrestacion">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Valor Solicitado" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) %>'
                                                                    ID="lblValorConvenioSolicitado">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Estado" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstadoServicio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudEstado") %>'>
                                                                </asp:Label><br />
                                                                <asp:Label runat="server" Text='<%# "Aprobado $ " + string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) %>'
                                                                    ID="lblValorAprobado" Visible="false">
                                                                </asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" Text='<%# "Descuento % " + string.Format("{0:#,##}",DataBinder.Eval(Container, "DataItem.Descuento")) %>'
                                                                    ID="lblDescuento" Visible="false">
                                                                </asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" Text='<%# "UVR " + string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVR")) %>'
                                                                    ID="lblUVR" Visible="false">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Motivo" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMotivo" Text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudMotivo") %>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Comentario">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComentarioServicioProducto" Text='<%# DataBinder.Eval(Container, "DataItem.Comentarios") %>'
                                                                    runat="server">
                                                                </asp:Label>
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
                                                            <asp:TextBox MaxLength="500" ID="txtComentariosTipoServicio" CssClass="LabelNoModify"
                                                                runat="server" Width="300px" Text='<%# DataBinder.Eval(Container, "DataItem.Comentarios") %>'>
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trComentariosAnulacion" runat="server" style="display: none">
                                                        <td width="30%">
                                                            Comentarios Anulación
                                                        </td>
                                                        <td width="70%">
                                                            <asp:TextBox ID="txtComentariosAnulacion" runat="server" CssClass="LabelNoModify"
                                                                Width="300px" MaxLength="500"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table class="tableBorder" runat="server" id="tblRecomendaciones" style="display: none"
                                        cellspacing="0" cellpadding="3" width="98%" border="0">
                                        <tr>
                                            <td class="headerTable">
                                                RECOMENDACIONES&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                <asp:Label ID="txtRecomendaciones" runat="server" CssClass="LabelNoModify"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br>
                                    <table class="tableBorder" runat="server" id="tblIncapacidad" style="display: none"
                                        cellspacing="0" cellpadding="3" width="98%" border="0">
                                        <tr>
                                            <td class="headerTable" colspan="6">
                                                INCAPACIDAD
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px" width="15%">
                                                Fecha Inicio
                                            </td>
                                            <td style="height: 26px" width="15%">
                                                &nbsp;
                                                <asp:TextBox ID="txtFechaInicioIncapacidad" runat="server" CssClass="LabelNoModify"
                                                    Width="100px"></asp:TextBox><a href="javascript:MostrarCalendario(Form1.txtInicioIncapacidad,Form1.txtInicioIncapacidad,'dd/mm/yyyy');"
                                                        name="btnFecha"></a>
                                            </td>
                                            <td align="right" width="15%">
                                                Fecha Fin
                                            </td>
                                            <td style="height: 26px" width="15%">
                                                &nbsp;
                                                <asp:TextBox ID="txtFechaFinIncapacidad" runat="server" CssClass="LabelNoModify"
                                                    Width="100px"></asp:TextBox><a href="javascript:MostrarCalendario(Form1.txtFinIncapacidad,Form1.txtFinIncapacidad,'dd/mm/yyyy');"
                                                        name="btnFecha"></a>
                                            </td>
                                            <td width="15%">
                                                <asp:CheckBox ID="chkContinuacion" runat="server" Enabled="False" Text="Continuación">
                                                </asp:CheckBox>
                                            </td>
                                            <td width="15%">
                                                <asp:CheckBox ID="chkTranscripcion" runat="server" Enabled="False" Text="Transcripción">
                                                </asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Diagnosticos
                                            </td>
                                            <td colspan="5">
                                                <asp:Label ID="lblDiagnosticosIncapacidad" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones Incapacidad
                                            </td>
                                            <td colspan="5">
                                                <asp:Label ID="lblObservacionesIncapacidad" runat="server" CssClass="LabelNoModify"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table id="Table6" cellspacing="0" cellpadding="0" width="300" border="0" style="display: none">
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
                        </tbody>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset class="FieldSet" style="width: 95%" id="fldFormatos" runat="server">
                    <legend>
                        <img src="../../iconos/icoPrint.gif" border="0">&nbsp;Impresión Órdenes</legend>
                    <br>
                    <table id="Table7" cellspacing="0" cellpadding="5" width="80%" align="center">
                        <tr>
                            <td>
                                <table class="tableAllBorder" id="Table8" cellspacing="0" width="95%" border="0">
                                    <tr>
                                        <td width="70%">
                                            Pantalla Resumen
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplFormato" runat="server">Imprimir</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Historia Clínica
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplHistoria" runat="server">Imprimir</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr id="trIncapacidad" runat="server" style="display: none">
                                        <td>
                                            Incapacidad
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplIncapacidad" runat="server">Imprimir</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr id="trRecomendaciones" runat="server" style="display: none">
                                        <td>
                                            Recomendaciones
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplRecomendaciones" runat="server">Imprimir</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                                <asp:DataList ID="dtlFormatos" runat="server" Width="95%" CellPadding="0">
                                    <ItemTemplate>
                                        <table class="tableAllBorder" id="Table3" cellspacing="0" cellpadding="8" width="100%"
                                            align="center">
                                            <tr>
                                                <td width="70%">
                                                    Orden de
                                                    <asp:Label ID="lblTipoServicioFormato" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio").ToString() + " - " + DataBinder.Eval(Container, "DataItem.NombreProveedor").ToString() %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblIdTipoServicioFormato" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblIdProveedor" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.IdProveedor") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblUrlFormato" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.URLFormato") %>'>
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkImprimirFormato" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdSolicitudTipoServicio") %>'
                                                        CommandName="Imprimir" ForeColor='<%#  !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Impresiones")) && Convert.ToInt16(DataBinder.Eval(Container, "DataItem.Impresiones")) > 0 ? System.Drawing.Color.Purple : System.Drawing.Color.FromName("#002E63")%>'>Imprimir</asp:LinkButton>&nbsp;
                                                    <asp:Label ID="lblImpresiones" runat="server" CssClass="textSmallBlack" Text='<%#!Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Impresiones")) ? "(" +  DataBinder.Eval(Container, "DataItem.Impresiones") + " impresiones)" : ""  %>'>
                                                    </asp:Label><br>
                                                    <asp:LinkButton ID="linkExportarFormato" runat="server" Visible="false">Exportar Excel</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:DataList ID="dtlFormatosControl" runat="server" CellPadding="0" Width="95%"
                                    OnItemCommand="dtlFormatosControl_ItemCommand">
                                    <ItemTemplate>
                                        <table class="tableAllBorder" id="Table6" cellspacing="0" cellpadding="8" width="100%"
                                            align="center">
                                            <tr>
                                                <td width="70%">
                                                    Orden de
                                                    <asp:Label ID="lblTipoServicioFormatoControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio").ToString() + " - " + DataBinder.Eval(Container, "DataItem.NombreComercial").ToString()  %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblIdTipoServicioFormatoControl" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblIdSolicitudServicio" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.IdSolicitudServicio") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblUrlFormatoControl" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.URLFormato") %>'>
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkImprimirFormatoControl" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdSolicitudTipoServicio") %>'
                                                        CommandName="Imprimir" ForeColor='<%#  !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Impresiones")) && Convert.ToInt16(DataBinder.Eval(Container, "DataItem.Impresiones")) > 0 ? System.Drawing.Color.Purple : System.Drawing.Color.FromName("#002E63")%>'>Imprimir</asp:LinkButton>&nbsp;
                                                    <br>
                                                    <asp:LinkButton ID="linkExportarFormatoControl" runat="server" Visible="false">Exportar Excel</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlCerrarHistoria" runat="server" Visible="false">
                    <fieldset class="FieldSet" style="width: 95%" id="FIELDSET1" runat="server">
                        <legend>
                            <img src="../../iconos/edit.PNG" border="0">&nbsp; Wellness Primera Vez</legend>
                        <br>
                        <table id="tblCerrarHistoria" style="width: 100%;">
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="lblCerrarHistoria" runat="server" Text="¿Desea cerrar la historia clínica?"></asp:Label>
                                </td>
                                <td width="12%">
                                    <asp:RadioButtonList ID="rdbCerrarHistoria" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="60%">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr id="trAvisoPrivacidad" runat="server" Visible=false>                
                <td align="center">
                <asp:Panel ID="pnlAvisoPrivacidad" runat="server">
                    <fieldset class="FieldSet" style="width: 95%" id="FIELDSET2" runat="server">
                        <legend>
                            <img src="../../iconos/label_ok.gif" border="0">&nbsp; Aviso de privacidad</legend>
                        <br>
                        <table id="Table2" style="width: 100%;">
                            
                            <tr>
                            <td class="style3" colspan=5>
                                <asp:Label ID="lblfechafirma" runat="server" Visible=false Text="El paciente ya firmo el aviso de privacidad el dia: "></asp:Label>
                                <asp:Label ID="lblNoFechaFirma" runat="server" Visible=false Text="El paciente no ha firmado el aviso de privacidad" ForeColor="#990000"></asp:Label>
                                <asp:Label ID="LblUltimaFirma" runat="server" Visible=false Text="Label"></asp:Label>
                            </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
            </td>    
           </tr>
        <tr>
            <td>
                <p align="center">
                    <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="« Anterior" Visible="False">
                    </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFinalizar" runat="server"
                        CssClass="button" Text="Finalizar"></asp:Button></p>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
