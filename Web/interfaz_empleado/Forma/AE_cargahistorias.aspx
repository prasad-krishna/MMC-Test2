<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_cargahistorias.aspx.cs" Inherits="Web.interfaz_empleado.Forma.AE_CargaHistorias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">


.Input { background: #FFFFFF; 
border: 1px solid #111111; 
font-family: Verdana, Arial, Helvetica, sans-serif; 
font-size: 11px;
}

        .auto-style1 {
            width: 345px;
        }
        .headerGrid
            {
                text-align:center;
                padding:5px;
            }

        #txtFileName {
            width: 333px;
        }
    /*RAM* Estilo button de carga*/
	.fileUpload {
	    position: relative;
	    overflow: hidden;
	    margin: 10px;
	}
	.fileUpload input.upload {
	    position: absolute;
	    top: 0;
	    right: 0;
	    margin: 0;
	    padding: 0;
	    font-size: 20px;
	    cursor: pointer;
	    opacity: 0;
	    filter: alpha(opacity=0);
	}
    #gridView th{
        background-color:#286090;
        text-align:center; 
        padding:10px;
        color:#FFF;
        border: solid 1px #286090;
    }
    #gridView td:nth-child(1) {  
        border-left: solid 1px #286090;
        border-right:0;
        text-align:center;
    }
    #gridView td:nth-child(2) {  
        border-right: solid 1px #286090;
        border-left:0;
    }
        #gridView tr:last-child {
            background-color:yellow;
        }
    #gridView tr{
        padding:10px;
        color:#000;
        border-left: solid 1px #286090;
        border-right: solid 1px #286090;
    }

    </style>
    <script type="text/javascript" src="../../scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../../scripts/jquery-2.1.4.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript">
        $("document").ready(function () {
            $("#ifrPageContent").attr("scrolling", "yes");
             //No funciona en internet explorer 8
                      //  $("#uploadBtn").change(function () {
                        //    alert("cambio texto");
                          //  $("#uploadFile").val(this.value);
                        //});
            $("#uploadBtn").change(function () {
                $("#uploadFile").val(this.value);
                //PageMethods.validar();
                //__doPostBack('btnSubir');
            });
        });

        function continuar_confirm(registros, nombreEmpresa) {
            if (confirm("Se realizará la carga de " + registros + " registros de información biométrica en la empresa " + nombreEmpresa + ", ¿Desea continuar? ") == true)
            {
                var x = PageMethods.continuar(
                
                    function(result)
                    {
                        var table;
                        table +="<tr id='resultados2' visible='true'>";
                        table +="<td class='auto-style1'>"
                        table +="<fieldset id='Fieldset2' style='padding:10px;'>";
                        table +="<legend>Resultado del proceso</legend>";
                        table +="<span id='EstatusProceso' style='color:green;'>";
                        table +="Carga exitosa <br> </span>";
                        table +="<span id='MensajeProceso'>";
                        table +="Se han generado las siguientes consultas. <br> </span>";
                        table +="<div>";
                        table +="<table id='gridView' class='TableInfHis' cellspacing='2' cellpadding='4' border='1' rules='all'>";
                        table +="<tbody>";
                        table +="<tr class='headerGrid' style='background-color:White;'>";
                        table += "<th scope='col' style='padding:5px; text-align:center;'> # Consulta </th>";
                        table += "<th scope='col' style='padding:5px; text-align:center;'> # Empleado </th>";
                        table += "</tr>";
                        var indice = 0;
                        for (var r in result) {
                            indice++;
                            var color = "#fff;";
                            if (indice % 2)
                                color = "#F7F7F7;";
                            table += "<tr style='background-color:" + color + "'>";
                            table += "<td style='padding:5px;'>" + r + "</td>";
                            table += "<td style='padding:5px;'>" + result[r] + "</td>";
                            table += "</tr>";
                        }
                        table +="</tbody>";
                        table +="</table>";
                        table +="</div>";
                        table +="<br>";
                        table +="</fieldset>";
                        table += "</td>";
                        table += "</tr>";
                        table += "<tr><td> <input type='submit' id='btnExcel' value='Excel' name='btnExcel'> </td></tr>";
                        $("#tblResultados").html(table);
                        $("#gridView").attr('runat', 'server');
                    });
            }

            else {
                return true;
            }
        }
    </script>
    <link rel="Stylesheet" href="../../css/bootstrap.min.css" type="text/css" />
</head>
<body>

    <form id="form1" runat="server" >
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
</asp:ScriptManager>
    <div>
        <table >
            <tr><td>
                <asp:Label ID="lblTitulo" runat="server" Width="339px" Font-Size="Large" Text="Carga de información biométrica" /><br />
                <br />
                </td>
                <td></td>
            </tr>
            <tr><td style="padding-left: 10px;">
                <asp:Label ID="lblAccion" runat="server" Font-Size="11px" Width="232px" Text="Seleccione el archivo a cargar"></asp:Label>
                </td>
            </tr>
            <tr><td style="padding-left: 10px;">
                <asp:Label ID="lblNombreArchivo" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table width="99%">
            <tr><td style="width:40%; padding-left: 10px;">
                    <input id="uploadFile" placeholder="Ningún archivo seleccionado" disabled="disabled" style="width:205px; font-size:11px;" />
                    <div class="fileUpload btn btn-primary btn-xs" style="margin: 1px;">
                        <span>Examinar...</span>
                        <input id="uploadBtn" type="file" class="upload" runat="server" onchange="this.blur();"/>
                    </div>
                </td>
                <td style="width:45%"><asp:Label ID="lblUltimaCarga" runat="server" Font-Size="11px" Text="Última carga realizada: "/><asp:Label ID="lblFechaUltimaCargaDato" Font-Size="11px" runat="server" Text="" /><br />
                    <asp:Label ID="lblRealizadoPor" runat="server" Font-Size="11px" Text="Realizado por: "/><asp:Label ID="lblRealizadoPorDato" Font-Size="11px" runat="server" Text="" />

                </td>
                <td style="width:14%">
                    <img src="../../images/excel.png" alt="" /><a href="../Layout_CargaBiometricas.xls" style="font-size:11px;">Layout de carga</a><br />
                    <img src="../../images/pdf.png" alt="" /><a href="#" style="font-size:11px;">Guia rápida</a>
                </td>
            </tr>
            <tr>
                <td style="padding-left:10px;">
                    <asp:Button ID="btnSubir" runat="server" OnClick="btnSubir_Click" Text="Procesar" CssClass="btn btn-primary btn-xs"/>
                    &nbsp;&nbsp;
        <asp:Button ID="btnContinuar" runat="server" OnClick="btnContinuar_Click" Text="Aceptar" CssClass="btn btn-primary btn-xs" Visible="false"/>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td></td>
            </tr>
        </table>
    </div>
    <table style="width:100%;" id="tblResultados">
<%--        <tr>
            <td>
    
        <asp:Label ID="lblErrores" runat="server" Text="Errores" Visible="False"></asp:Label>
    
    &nbsp;sasd</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>--%>
        <tr id="resultados" runat="server" visible="false">
            <td class="auto-style1">
                                            <fieldset id="Fieldset1" runat="server" style="padding:10px;">
                                    <LEGEND >Resultado del proceso</LEGEND>
                                        <asp:Label ID="EstatusProceso" runat="server" />
                                        <asp:Label ID="MensajeProceso" runat="server" />
                                        <asp:GridView ID="gridView" runat="server" OnRowDataBound="gridView_RowDataBound" CellPadding="4" CellSpacing="2" EmptyDataText="Sin resultados" CssClass="TableInfHis">
                                            <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                        </asp:GridView>
                                        <asp:Label ID="noDatos" runat="server" Visible="false"></asp:Label>
                                        <br />
                                        </div>
                                                <%--<asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" Text="Excel" Visible="true"/>--%>
                                </fieldset>
                </td>
            <td>
            <asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" Text="Descargar Excel" Visible="false"/></td></tr>
<%--        <tr>
            <td>
    <asp:GridView ID="dgConsultasCargadas" runat="server">
    </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>--%>
    </table>
    <p>
        &nbsp;</p>
        <%--no funciona correcto en IE8--%>
<%--            <div class="credential-container register-link" data-toggle="modal" data-target="#myModal" data-reveal-id="modalWindow">
                    <div class="credentials">
                        <b>Boton</b>
                    </div>
            </div>--%>
    <!-- Modal -->
<%--    <div class="modal fade" id="myModal" role="dialog" data-remote="false">
        <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                       encabezado
                        <asp:GridView ID="gridView1" runat="server" OnRowDataBound="gridView1_RowDataBound" CellPadding="4" CellSpacing="2" EmptyDataText="Sin resultados" CssClass="TableInfHis">
                                            <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                        </asp:GridView>
                    </div>
                    <div class="modal-body">
                        cuerpo
                    </div>
                </div>
        </div>
    </div>--%>
    </form>
</body>
</html>
