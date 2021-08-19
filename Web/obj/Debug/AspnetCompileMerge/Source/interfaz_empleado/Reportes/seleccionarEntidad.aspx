<%@ Page Language="c#" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.reportes.seleccionarEntidad" CodeBehind="seleccionarEntidad.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Reportes</title>
<!--[if gte IE 5.5]>
<script language="JavaScript" src="dhtml.js" type="text/JavaScript"></script>
<![endif]-->
<link href="../../css/admon.css" rel="stylesheet" type="text/css" />
<script src="../../js/utilReportes.js"></script>
    <script>
        //RAM*
        function load()
        {
            if (typeof (document.escogerRadio.escogido) != 'undefined')            {
                document.getElementById("idRegresar").style.visibility = "hidden";
            }
            else {
                document.getElementById("idRegresar").style.visibility = "visible";
            }
        }
    </script>
</head>
<body onload="load()">
    <form id="form1" runat="server">
    </form>
<div id="wrapper" style="margin:0 0 0 0;">
    <div id="contenido">
        <span><a href="javascript:seleccionar()" class="textogris">Escoger valor marcando un valor y haciendo clic aqui</a></span>
        <label runat=server id=contenidoReporte></label>
    </div>
    <%-- RAM* --%>
    <div id="idRegresar">
        <br /><br /><br />
        <a href="javascript:atras()">regresar</a>
    </div>
    <br />
</div>
    
</body>
<script>
        function seleccionar(){
            var cadena;
            var campoTexto, campoValor;
            var escogido = false;
            var i = 0;
            
            if (typeof (window.opener.document.reporte) != 'undefined') {
                campoTexto = eval("window.opener.document.reporte._T<% Response.Write(Microsoft.Security.Application.Encoder.HtmlEncode(Request["nombreCampo"].ToString())); %>");
                campoValor = eval("window.opener.document.reporte._V<% Response.Write(Microsoft.Security.Application.Encoder.HtmlEncode(Request["nombreCampo"].ToString())); %>");
            }
            if (typeof (document.escogerRadio.escogido) != 'undefined') {
                if (document.escogerRadio.escogido.length) {
                    while (i < document.escogerRadio.escogido.length) {
                        if (document.escogerRadio.escogido[i].checked) {
                            cadena = new String(document.escogerRadio.escogido[i].value);
                            elementos = cadena.split("|");
                            if (campoTexto.value != "") {
                                campoTexto.value = campoTexto.value + ",";
                                campoValor.value = campoValor.value + ",";
                            }
                            campoTexto.value = campoTexto.value + elementos[0];
                            campoValor.value = campoValor.value + elementos[1];
                            campoTexto.className = "textBox";
                            escogido = true;
                            self.close();
                        }
                        i = i + 1;
                    }
                    if (!escogido) {
                        alert('Debe escoger un valor');
                    }
                }
                else {
                    if (!document.escogerRadio.escogido.checked) {
                        alert('Debe escoger un valor');
                    }
                    else {
                        cadena = new String(document.escogerRadio.escogido.value);
                        elementos = cadena.split("|");
                        if (campoTexto.value != "") {
                            campoTexto.value = campoTexto.value + ",";
                            campoValor.value = campoValor.value + ",";
                        }
                        campoTexto.value = campoTexto.value + elementos[0];
                        campoValor.value = campoValor.value + elementos[1];
                        campoTexto.className = "textBox";
                        self.close();
                    }
                }
            }
        }
    <%-- RAM* --%>
    function atras() {
        window.history.back();
    }
</script>
</html>
