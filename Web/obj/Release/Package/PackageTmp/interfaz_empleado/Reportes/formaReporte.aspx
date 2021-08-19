<%@ Page Language="c#" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.reportes.formaReporte" CodeBehind="formaReporte.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Reportes</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link href="../../css/admon.css" rel="stylesheet" type="text/css">
		<script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
		<script src="../../scripts/utilReportes.js"></script>
        <script src="../../scripts/jquery-1.4.1.js"></script>
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
        <script>
            //RAM* agregar fucnionalidad a link de empresas
            $(document).ready(function () {
                var globalCodigo = "";
                $("#_C0").change(function () {
                    var condicion = $("#_C0").val();
                    if (condicion == "=")
                        condicion = 1;
                    else if (condicion == "<>")
                        condicion = 2;
                    else
                        condicion = $("#_C0").val();


                    if (globalCodigo == "")
                        globalCodigo = $("#controlEmpresa").attr('onclick').toString();
                    //var codigo = "javascript: seleccionarEntidad('EMPRESA ID', 'EmpresaDatos', 'empresa_id', 'AbreviacionEmpresa', 2, 'ConnectionStringReembolsos','" + $("#_V0").val() + "','" + condicion + "' );";
                    var codigo = globalCodigo;
                    codigo = codigo.replace('function onclick()', ' ');
                    codigo = codigo.replace('function onclick(event) ', ' ');
                    codigo = codigo.replace('{CORPORATIVO}', $("#_V0").val());
                    codigo = codigo.replace('{CONDICION}', condicion);
                    codigo = codigo.replace('{', ' ');
                    codigo = codigo.replace('}', ' ');
                    var newclick = new Function(codigo);
                    //$("#controlEmpresa").unbind('click');
                    //$("#controlEmpresa").bind('click', newclick);
                    //$("#controlEmpresa").click(function(){return newclick});
                    //solo Firefox
                    $("#controlEmpresa").attr('onclick', '').click(newclick);
                });


                $("#_V0").change(function () {
                    if ($("#_V0").val() != '') {
                        $("#_C0 option[value='']").remove();
                        //$('#_C0 option[value="="]').attr('selected', 'selected');
                        document.getElementById("_C0").selectedIndex = "0";
                        $("#_C0 option[value='=']").attr("selected", true);
                    }
                    else {
                        $("#_C0 option").remove();
                        $("#_C0").append("<option value=''>Escoja ...</option> <option value='='>Igual a</option> <option value='<>'>Diferente a</option>");
                    }
                    var condicion = $("#_C0").val();
                    if (condicion == "=")
                        condicion = 1;
                    else if (condicion == "<>")
                        condicion = 2;
                    else
                        condicion = $("#_C0").val();
                    if (globalCodigo == "")
                        globalCodigo = $("#controlEmpresa").attr('onclick').toString();
                    //var codigo = "javascript: seleccionarEntidad('EMPRESA ID', 'EmpresaDatos', 'empresa_id', 'AbreviacionEmpresa', 2, 'ConnectionStringReembolsos','" + $("#_V0").val() + "','" + condicion + "' );";
                    var codigo = globalCodigo;
                    codigo = codigo.replace('function onclick()', ' ');
                    codigo = codigo.replace('function onclick(event) ', ' ');
                    codigo = codigo.replace('{CORPORATIVO}', $("#_V0").val());
                    codigo = codigo.replace('{CONDICION}', condicion);
                    codigo = codigo.replace('{', ' ');
                    codigo = codigo.replace('}', ' ');
                    var newclick = new Function(codigo);
                    //$("#controlEmpresa").unbind('click');
                    //$("#controlEmpresa").bind('click', newclick);
                    //$("#controlEmpresa").click(function () { return newclick });
                    //solo Firefox
                    $("#controlEmpresa").attr('onclick', '').click(newclick);
                });
            });
        </script>
	</HEAD>
	<body  onload="CargarConfiguracion()" >
		<div id="wrapper" style="margin:0 0 0 0;">
			<div id="contenido">
				<label runat="server" id="contenidoReporte"></label>
			</div>
		</div>
	</body>
</HTML>
