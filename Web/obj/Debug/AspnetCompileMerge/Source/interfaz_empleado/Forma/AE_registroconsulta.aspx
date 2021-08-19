<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnosticoConsulta" Src="../WebControls/WC_AdicionarDiagnosticoConsulta.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>

<%@ Page Language="c#" CodeBehind="AE_registroconsulta.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.interfaz_empleado.forma.AE_registroconsulta" %>

<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>
<%@ Register Src="../WebControls/WC_AdicionarDiagnosticoConsultaCIE10.ascx" TagName="WC_AdicionarDiagnosticoConsultaCIE10"
    TagPrefix="uc2" %>
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

    <style type="text/css">
        .style1 {
            height: 23px;
        }

        .TableInfHis tr td:first-child {
            font-weight: bold;
            font-size: 10px;
        }

        .TableInfHis tr td {
            font-size: 10px;
        }

        .prueba {
        }
    </style>
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Scripts para control de sesión -->
    <script src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.countdown.js" type="text/javascript"></script>
    <script src="../../scripts/ControlSesion.js" type="text/javascript"></script>
    <!-- Fin - Emilio Bueno 20/11/2012 -->
</head>
<body leftmargin="5" topmargin="1" onload="CargarConfiguracion();HabilitarValidadores(null)"
    rightmargin="5">
    <form id="Form1" method="post" runat="server">
        <!-- Inicio - Emilio Bueno 20/11/2012 -->
        <!-- Se agregan Controles para mensaje de control de sesión -->
        <div style="display: none;">
            <asp:HiddenField ID="hdnTimeout" runat="server" />
            <asp:HiddenField ID="hdnSesion" runat="server" />
            <span id="shortly" style="display: none;"></span>
            <asp:HiddenField ID="hdnBotonClick" Value="lnkGuardar" runat="server" />
            <asp:HiddenField ID="hdnTiempoMostrarAlerta" runat="server" />
            <asp:HiddenField ID="hdnTiempoGuardarTemporal" runat="server" />
        </div>
        <div id="divCodigoCS">
        </div>
        <!-- Fin - Emilio Bueno 20/11/2012 -->

        <asp:ScriptManager ID="scrMng" runat="server">
        </asp:ScriptManager>

        <script type="text/javascript">
            var tipoConsultaGeneral = "";
            function ValidarCaracteres(textareaControl, maxlength) {
                if (textareaControl.value.length > maxlength) {
                    textareaControl.value = textareaControl.value.substring(0, maxlength);
                    alert("Debe ingresar hasta un maximo de " + maxlength + " caracteres");
                }
            }
            function CalcularIMC() {
                var strSeparadorMiles = '<%=ConfigurationManager.AppSettings["SeparadorMiles"].ToString() %>';
                var strSeparadorDecimales = '<%=ConfigurationManager.AppSettings["SeparadorDecimales"].ToString() %>';
                var txtPeso = document.getElementById('txtPeso').value.replace(strSeparadorMiles, "");
                var txtTalla = document.getElementById('txtTalla').value.replace(strSeparadorMiles, "");
                var txtIMC = document.getElementById('txtIMC');
                var IMC = (parseFloat(txtPeso) / ((parseFloat(txtTalla) * parseFloat(txtTalla)))).toFixed(3);
                txtIMC.value = IMC.toString().replace(".", strSeparadorDecimales).replace(",", strSeparadorDecimales);
            }

            function CalcularTensionMedia() {
                if (document.getElementById('txtTensionMedia') != null) {
                    var strSeparadorMiles = '<%=ConfigurationManager.AppSettings["SeparadorMiles"].ToString() %>';
                    var strSeparadorDecimales = '<%=ConfigurationManager.AppSettings["SeparadorDecimales"].ToString() %>';
                    var txtDiastolica = document.getElementById('txtDiastolica').value.replace(strSeparadorMiles, "");
                    var txtSistolica = document.getElementById('txtSistolica').value.replace(strSeparadorMiles, "");
                    var txtTensionMedia = document.getElementById('txtTensionMedia');
                    var tensionMedia = (parseInt(txtDiastolica) + (parseInt(txtSistolica) - parseInt(txtDiastolica)) / 3).toFixed(0);;
                    txtTensionMedia.value = tensionMedia.toString().replace(".", strSeparadorDecimales).replace(",", strSeparadorDecimales);
                }
            }

            function BorrarCampo(image, nameControl) {
                var control = document.getElementById(nameControl);
                control.value = '';

            }

            function dobleClickSubmint() {
                var isValid = false;
                // alert("ValidatorOnSubmit() " + ValidatorOnSubmit() + "   typeof (ValidatorOnSubmit)  " + typeof (ValidatorOnSubmit) + "  Page_ClientValidate()   " + Page_ClientValidate());
                //if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) //Marsh - JFEE - 2014/11/26 - Correcciones generales, previene que se muestren dos mensajes de validación
                ////if (typeof (ValidatorOnSubmit) == "function" && Page_ClientValidate() == true)
                //{
                //    isValid = Page_ClientValidate();
                //    //isValid = true;
                //}

                if (isValid) {
                    document.getElementById('lblCargandoConsulta1').innerHTML = 'Cargando Consulta';
                    document.getElementById('btnGuardar').style.display = "none";
                    document.getElementById('btnCancelar').style.display = "none";
                    document.getElementById('trValidar').style.display = "none";
                    scroll();
                }
            }
            function HabilitarValidadores(checkBox) {
                var tipoConsulta = "";
                var radio;

                var medico = ["1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17"];
                //Prototipo0-DMA-10/09/2018
                var laboral = ["18","19"];
                var nutriologo = ["15", "16"];
                var todo = ["3", "4"];

                if (checkBox == null)
                    radio = document.getElementsByName("rblTipoConsulta");
                else
                    radio = document.getElementsByName(checkBox.id);


                for (var j = 0; j < radio.length; j++) {
                    if (radio[j].checked) {
                        tipoConsulta = radio[j].value;
                        tipoConsultaGeneral = tipoConsulta;
                    }
                }

                //Prototipo0-DMA-14/09/2018
                //Condiciones laboral - Se oculta
                $("#HistoriaLaboral").hide();
                var idTipoConsulta = "<%=Session["idTipoConsulta"]??""%>";
                var IdConsulta = "<%=Request.QueryString["IdConsulta"]??""%>";

                if (idTipoConsulta == "")
                    idTipoConsulta = tipoConsulta;

                if (!(idTipoConsulta == "18" || idTipoConsulta == "19")) {
                    $("#HistoriaLaboral input[type='text'],#HistoriaLaboral textarea").val("");
                    $("#HistoriaLaboral select").val("0");
                    $("#HistoriaLaboral input[type='checkbox']").removeAttr("checked");
                }

                fnIniHistLab();

                if (tipoConsulta != "") {

                    if (tipoConsulta == 3) {
                        //Inicio MAHG 21/07/2010

                        document.getElementById("lblObligatorioEnfermedadActual").style.display = "none";
                        //document.getElementById("lblObligatorioMotivo").style.display = "none";
                        var lblIdObligatorioDiagnostico = '<%=(WC_AdicionarDiagnosticoConsulta1.FindControl("lblObligatorioDiagnostico")).ClientID %>';
                        document.getElementById(lblIdObligatorioDiagnostico).style.display = "none";
                        document.getElementById("lblObligatorioPlanTratamiento").style.display = "none";

                        //Fin MAHG 21/07/2010
                    }
                    else {

                        //Inicio MAHG 21/07/2010

                        document.getElementById("lblObligatorioEnfermedadActual").style.display = "";
                        //document.getElementById("lblObligatorioMotivo").style.display = "";
                        var lblIdObligatorioDiagnostico = '<%=(WC_AdicionarDiagnosticoConsulta1.FindControl("lblObligatorioDiagnostico")).ClientID %>';
                        document.getElementById(lblIdObligatorioDiagnostico).style.display = "";
                        document.getElementById("lblObligatorioPlanTratamiento").style.display = "";

                        //Fin MAHG 21/07/2010
                    }

                    if (tipoConsulta == "9" || tipoConsulta == "3") {

                        if (tipoConsulta == "9") {
                            document.getElementById("trSolicitanteTranscripcion").style.display = "";
                            document.getElementById("trComentariosTranscripcion").style.display = "";
                        }
                        ValidatorEnable(document.getElementById("rfvPeso"), false);
                        ValidatorEnable(document.getElementById("rfvTalla1"), false);
                        ValidatorEnable(document.getElementById("rfvTalla"), false);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaCar"), false);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaRes"), false);
                        ValidatorEnable(document.getElementById("rfvPeso1"), false);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaCar1"), false);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaRes1"), false);
                        ValidatorEnable(document.getElementById("rfvTemperatura"), false);
                        ValidatorEnable(document.getElementById("rfvTemperatura1"), false);


                        if (document.getElementById("divDiastolicaSisTolica") != null) {
                            ValidatorEnable(document.getElementById("rfvDiastolica1"), false);
                            ValidatorEnable(document.getElementById("rfvDiastolica"), false);
                            ValidatorEnable(document.getElementById("rfvSistolica1"), false);
                            ValidatorEnable(document.getElementById("rfvSistolica"), false);

                            ValidatorEnable(document.getElementById("rfvTension1"), false);
                            ValidatorEnable(document.getElementById("rfvTensionNormal1"), false);
                            ValidatorEnable(document.getElementById("rfvTension"), false);
                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvTension1"), false);
                            ValidatorEnable(document.getElementById("rfvTensionNormal1"), false);
                            ValidatorEnable(document.getElementById("rfvTension"), false);

                        }
                        if (document.getElementById("btnBuscarSolicitanteTranscripcion").style.display == "") {
                            if (tipoConsulta == "9") {
                                ValidatorEnable(document.getElementById("rfvSolicitanteTranscripcion"), true);
                                ValidatorEnable(document.getElementById("cmvSolicitanteTranscripcion"), false);
                            }
                            if (tipoConsulta == "3") {
                                ValidatorEnable(document.getElementById("rfvSolicitanteTranscripcion"), false);
                                ValidatorEnable(document.getElementById("cmvSolicitanteTranscripcion"), false);
                            }

                        }
                        else {
                            if (tipoConsulta == "9") {
                                ValidatorEnable(document.getElementById("cmvSolicitanteTranscripcion"), true);
                                ValidatorEnable(document.getElementById("rfvSolicitanteTranscripcion"), false);
                            }
                            if (tipoConsulta == "3") {
                                ValidatorEnable(document.getElementById("cmvSolicitanteTranscripcion"), false);
                                ValidatorEnable(document.getElementById("rfvSolicitanteTranscripcion"), false);
                            }
                        }

                        if (tipoConsulta == "3") {
                            ValidatorEnable(document.getElementById("rfvMotivo"), false);
                            ValidatorEnable(document.getElementById("rfvEnfermedadActual"), false);
                            ValidatorEnable(document.getElementById("rfvPlanTratamiento"), false);
                        }

                    }
                    else {

                        ValidatorEnable(document.getElementById("rfvMotivo"), false);
                        ValidatorEnable(document.getElementById("rfvEnfermedadActual"), true);
                        ValidatorEnable(document.getElementById("rfvPlanTratamiento"), true);
                        ValidatorEnable(document.getElementById("rfvPeso"), true);
                        if (tipoConsulta == "11" || tipoConsulta == "12") {
                            ValidatorEnable(document.getElementById("rfvTalla1"), true);
                            ValidatorEnable(document.getElementById("rfvTalla"), true);
                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvTalla1"), false);
                            ValidatorEnable(document.getElementById("rfvTalla"), false);
                        }
                        ValidatorEnable(document.getElementById("rfvSolicitanteTranscripcion"), false);
                        ValidatorEnable(document.getElementById("cmvSolicitanteTranscripcion"), false);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaCar"), true);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaRes"), true);
                        ValidatorEnable(document.getElementById("rfvPeso1"), true);
                        ValidatorEnable(document.getElementById("rfvTemperatura"), true);
                        ValidatorEnable(document.getElementById("rfvTemperatura1"), true);

                        ValidatorEnable(document.getElementById("rfvFrecuenciaCar1"), true);
                        ValidatorEnable(document.getElementById("rfvFrecuenciaRes1"), true);
                        document.getElementById("trSolicitanteTranscripcion").style.display = "none";
                        document.getElementById("trComentariosTranscripcion").style.display = "none";

                        if (document.getElementById("divDiastolicaSisTolica") != null) {
                            ValidatorEnable(document.getElementById("rfvDiastolica1"), true);
                            ValidatorEnable(document.getElementById("rfvDiastolica"), true);
                            ValidatorEnable(document.getElementById("rfvSistolica1"), true);
                            ValidatorEnable(document.getElementById("rfvSistolica"), true);

                            ValidatorEnable(document.getElementById("rfvTension"), false);
                            ValidatorEnable(document.getElementById("rfvTension1"), false);


                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvTension"), true);
                            ValidatorEnable(document.getElementById("rfvTension1"), true);
                        }
                    }
                    if (tipoConsulta == "1") {
                        if (document.getElementById("divHabitos") != null) {
                            ValidatorEnable(document.getElementById("rfvAlcohol"), true);
                            ValidatorEnable(document.getElementById("rfvFrecuenciaAlcohol"), true);
                            ValidatorEnable(document.getElementById("rfvFrecuenciaTabaquismo"), true);
                            ValidatorEnable(document.getElementById("rfvActividad"), true);
                            ValidatorEnable(document.getElementById("rfvTabaquismo"), true);
                            ValidatorEnable(document.getElementById("rfvVacunacion"), true);
                        }
                        if (document.getElementById("divPerimetroAbdominal") != null) {
                            ValidatorEnable(document.getElementById("rfvPerimetroAbdominal"), false);
                        }

                        ValidatorEnable(document.getElementById("rfvAnteFamiliares"), true);

                        if (document.getElementById("txtGenero").value == "1") {
                            ValidatorEnable(document.getElementById("rfvAnteGineco"), true);
                            ValidatorEnable(document.getElementById("rfvMenarquia"), true);
                            ValidatorEnable(document.getElementById("rfvFUM"), true);
                            ValidatorEnable(document.getElementById("rfvGestaciones"), true);
                            ValidatorEnable(document.getElementById("rfvPartos"), true);
                            ValidatorEnable(document.getElementById("rfvCesareas"), true);
                            ValidatorEnable(document.getElementById("rfvAbortos"), true);
                            ValidatorEnable(document.getElementById("rfvVivos"), true);

                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvAnteGineco"), false);
                            ValidatorEnable(document.getElementById("rfvMenarquia"), false);
                            ValidatorEnable(document.getElementById("rfvFUM"), false);
                            ValidatorEnable(document.getElementById("rfvGestaciones"), false);
                            ValidatorEnable(document.getElementById("rfvPartos"), false);
                            ValidatorEnable(document.getElementById("rfvCesareas"), false);
                            ValidatorEnable(document.getElementById("rfvAbortos"), false);
                            ValidatorEnable(document.getElementById("rfvVivos"), false);
                        }
                        if (esMedico >= 0) {
                            ValidatorEnable(document.getElementById("rfvAnteMedicos"), true);
                        }
                        ValidatorEnable(document.getElementById("rfvAnteOtros"), true);
                        ValidatorEnable(document.getElementById("rfvAnteQuirurgicos"), true);
                        ValidatorEnable(document.getElementById("rfvAnteToxico"), true);
                        ValidatorEnable(document.getElementById("rfvAnteFarmacologicos"), true);
                        ValidatorEnable(document.getElementById("rfvAnteTransfusionales"), true);
                        ValidatorEnable(document.getElementById("rfvFisAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvFisCabeza"), true);
                        ValidatorEnable(document.getElementById("rfvFisCuello"), true);
                        ValidatorEnable(document.getElementById("rfvFisGeneral"), true);
                        ValidatorEnable(document.getElementById("rfvFisOtros"), true);
                        ValidatorEnable(document.getElementById("rfvFisTorax"), true);
                        ValidatorEnable(document.getElementById("rfvFisPielFanelas"), true);
                        ValidatorEnable(document.getElementById("rfvFisConjuntiva"), true);
                        ValidatorEnable(document.getElementById("rfvFisReflejo"), true);
                        ValidatorEnable(document.getElementById("rfvFisPupilas"), true);
                        ValidatorEnable(document.getElementById("rfvFisOidos"), true);
                        ValidatorEnable(document.getElementById("rfvFisOtoscopia"), true);
                        ValidatorEnable(document.getElementById("rfvFisRinoscopia"), true);
                        ValidatorEnable(document.getElementById("rfvFisBocaFaringe"), true);
                        ValidatorEnable(document.getElementById("rfvFisAmigdalas"), true);
                        ValidatorEnable(document.getElementById("rfvFisTiroides"), true);
                        ValidatorEnable(document.getElementById("rfvFisAdenopatias"), true);
                        ValidatorEnable(document.getElementById("rfvFisRuidosCardiacos"), true);
                        ValidatorEnable(document.getElementById("rfvFisRuidosRespiratorios"), true);
                        ValidatorEnable(document.getElementById("rfvFisPalpacionAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvFisGenitales"), true);
                        ValidatorEnable(document.getElementById("rfvFisHernias"), true);
                        ValidatorEnable(document.getElementById("rfvFisColumna"), true);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesSuperiores"), true);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesInferiores"), true);
                        ValidatorEnable(document.getElementById("rfvFisVarices"), true);
                        ValidatorEnable(document.getElementById("rfvFisNeurologico"), true);
                        ValidatorEnable(document.getElementById("rfvSisAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvSisCabeza"), true);
                        ValidatorEnable(document.getElementById("rfvSisCuello"), true);
                        ValidatorEnable(document.getElementById("rfvSisGeneral"), true);
                        ValidatorEnable(document.getElementById("rfvSisOtros"), true);
                        ValidatorEnable(document.getElementById("rfvSisTorax"), true);
                        ValidatorEnable(document.getElementById("rfvTalla"), true);
                        ValidatorEnable(document.getElementById("rfvTalla1"), true);
                        ValidatorEnable(document.getElementById("rfvTemperatura"), true);
                        ValidatorEnable(document.getElementById("rfvTemperatura1"), true);
                        document.getElementById("esRequerida").value = "1";
                        document.getElementById("esRequerida2").value = "1";
                    }
                    else {
                        if (document.getElementById("divHabitos") != null) {
                            ValidatorEnable(document.getElementById("rfvAlcohol"), false);
                            ValidatorEnable(document.getElementById("rfvFrecuenciaAlcohol"), false);
                            ValidatorEnable(document.getElementById("rfvFrecuenciaTabaquismo"), false);
                            ValidatorEnable(document.getElementById("rfvActividad"), false);
                            ValidatorEnable(document.getElementById("rfvTabaquismo"), false);
                            ValidatorEnable(document.getElementById("rfvVacunacion"), false);
                        }
                        if (document.getElementById("divPerimetroAbdominal") != null) {
                            ValidatorEnable(document.getElementById("rfvPerimetroAbdominal"), false);
                        }

                        ValidatorEnable(document.getElementById("rfvAnteFamiliares"), false);
                        ValidatorEnable(document.getElementById("rfvAnteGineco"), false);
                        ValidatorEnable(document.getElementById("rfvMenarquia"), false);
                        ValidatorEnable(document.getElementById("rfvFUM"), false);
                        ValidatorEnable(document.getElementById("rfvGestaciones"), false);
                        ValidatorEnable(document.getElementById("rfvPartos"), false);
                        ValidatorEnable(document.getElementById("rfvCesareas"), false);
                        ValidatorEnable(document.getElementById("rfvAbortos"), false);
                        ValidatorEnable(document.getElementById("rfvVivos"), false);
                        if (esNutriologo >= 0) {
                            ValidatorEnable(document.getElementById("rfvAnteMedicos"), false);
                        }
                        ValidatorEnable(document.getElementById("rfvAnteOtros"), false);
                        ValidatorEnable(document.getElementById("rfvAnteQuirurgicos"), false);
                        ValidatorEnable(document.getElementById("rfvAnteToxico"), false);
                        ValidatorEnable(document.getElementById("rfvAnteFarmacologicos"), false);
                        ValidatorEnable(document.getElementById("rfvAnteTransfusionales"), false);
                        ValidatorEnable(document.getElementById("rfvFisAbdomen"), false);
                        ValidatorEnable(document.getElementById("rfvFisCabeza"), false);
                        ValidatorEnable(document.getElementById("rfvFisCuello"), false);
                        ValidatorEnable(document.getElementById("rfvFisGeneral"), false);
                        ValidatorEnable(document.getElementById("rfvFisOtros"), false);
                        ValidatorEnable(document.getElementById("rfvFisTorax"), false);
                        ValidatorEnable(document.getElementById("rfvFisPielFanelas"), false);
                        ValidatorEnable(document.getElementById("rfvFisConjuntiva"), false);
                        ValidatorEnable(document.getElementById("rfvFisReflejo"), false);
                        ValidatorEnable(document.getElementById("rfvFisPupilas"), false);
                        ValidatorEnable(document.getElementById("rfvFisOidos"), false);
                        ValidatorEnable(document.getElementById("rfvFisOtoscopia"), false);
                        ValidatorEnable(document.getElementById("rfvFisRinoscopia"), false);
                        ValidatorEnable(document.getElementById("rfvFisBocaFaringe"), false);
                        ValidatorEnable(document.getElementById("rfvFisAmigdalas"), false);
                        ValidatorEnable(document.getElementById("rfvFisTiroides"), false);
                        ValidatorEnable(document.getElementById("rfvFisAdenopatias"), false);
                        ValidatorEnable(document.getElementById("rfvFisRuidosCardiacos"), false);
                        ValidatorEnable(document.getElementById("rfvFisRuidosRespiratorios"), false);
                        ValidatorEnable(document.getElementById("rfvFisPalpacionAbdomen"), false);
                        ValidatorEnable(document.getElementById("rfvFisGenitales"), false);
                        ValidatorEnable(document.getElementById("rfvFisHernias"), false);
                        ValidatorEnable(document.getElementById("rfvFisColumna"), false);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesSuperiores"), false);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesInferiores"), false);
                        ValidatorEnable(document.getElementById("rfvFisVarices"), false);
                        ValidatorEnable(document.getElementById("rfvFisNeurologico"), false);
                        ValidatorEnable(document.getElementById("rfvSisAbdomen"), false);
                        ValidatorEnable(document.getElementById("rfvSisCabeza"), false);
                        ValidatorEnable(document.getElementById("rfvSisCuello"), false);
                        ValidatorEnable(document.getElementById("rfvSisGeneral"), false);
                        ValidatorEnable(document.getElementById("rfvSisOtros"), false);
                        ValidatorEnable(document.getElementById("rfvSisTorax"), false);
                        document.getElementById("esRequerida").value = "0";
                        document.getElementById("esRequerida2").value = "0";
                        if (tipoConsulta == "11" || tipoConsulta == "12") {
                            ValidatorEnable(document.getElementById("rfvTalla1"), true);
                            ValidatorEnable(document.getElementById("rfvTalla"), true);
                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvTalla"), false);
                            ValidatorEnable(document.getElementById("rfvTalla1"), false);
                        }
                        //Juan Sebastián Suárez
                        //Consulta Carga Biometricos
                        //enero 2014
                        //
                        if (tipoConsulta == "13") {
                            ValidatorEnable(document.getElementById("rfvColesterolTotal"), true);
                            ValidatorEnable(document.getElementById("rfvColesterolHDL"), true);
                            ValidatorEnable(document.getElementById("rfvColesterolLDL"), true);
                            ValidatorEnable(document.getElementById("rfvTrigliceridos"), true);
                            ValidatorEnable(document.getElementById("rfvIndiceAterogenico"), true);
                            ValidatorEnable(document.getElementById("rfvGlucemiaAyunas"), true);
                        }
                        else {
                            ValidatorEnable(document.getElementById("rfvColesterolTotal"), false);
                            ValidatorEnable(document.getElementById("rfvColesterolHDL"), false);
                            ValidatorEnable(document.getElementById("rfvColesterolLDL"), false);
                            ValidatorEnable(document.getElementById("rfvTrigliceridos"), false);
                            ValidatorEnable(document.getElementById("rfvIndiceAterogenico"), false);
                            ValidatorEnable(document.getElementById("rfvGlucemiaAyunas"), false);
                        }
                    }
                }

                var esMedico = $.inArray(tipoConsulta, medico);
                var esNutriologo = $.inArray(tipoConsulta, nutriologo);
                var esTodo = $.inArray(tipoConsulta, todo);
                //Prototipo0-DMA-10/09/2018
                var esLaboral = $.inArray(tipoConsulta, laboral);
                //RAM cambio
                //Prototipo0-DMA-10/09/2018-Laboral
                if (esMedico >= 0 || esLaboral >= 0) {
                    $("#Interrogatorio").show();
                    $("#ExploracionFisica").show();
                    $("#pruebasBiometricas").show();
                    $("#impresionDiagnostica").show();
                    $("#PlanTratamiento").show();

                    //solo si es laboral se mostrará la sección
                    if (esLaboral >= 0)
                        $("#HistoriaLaboral").show();

                }
                
                if (esNutriologo >= 0) {
                    ValidatorEnable(document.getElementById("rfvFrecuenciaRes"), false);
                    ValidatorEnable(document.getElementById("rfvFrecuenciaRes1"), false);
                    ValidatorEnable(document.getElementById("rfvFrecuenciaCar"), false);
                    ValidatorEnable(document.getElementById("rfvFrecuenciaCar1"), false);
                    ValidatorEnable(document.getElementById("rfvTemperatura"), false);
                    ValidatorEnable(document.getElementById("rfvTemperatura1"), false);
                    ValidatorEnable(document.getElementById("rfvPlanTratamiento"), false);


                    $("#Interrogatorio").hide();
                    $("#ExploracionFisica").show();
                    $("#pruebasBiometricas").hide();
                    $("#impresionDiagnostica").hide();
                    $("#PlanTratamiento").hide();
                }
                if (esTodo >= 0) {
                    $("#Interrogatorio").show();
                    $("#ExploracionFisica").show();
                    $("#pruebasBiometricas").show();
                    $("#impresionDiagnostica").show();
                    $("#PlanTratamiento").show();
                }
            }


            if (typeof window.event != 'undefined')
                document.onkeydown = function () {
                    var test_var = event.srcElement.tagName.toUpperCase();
                    var test_id = event.srcElement.id;
                    if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
                        return (event.keyCode != 8);
                }
            else
                document.onkeypress = function (e) {
                    var test_var = e.target.nodeName.toUpperCase();
                    var test_id = e.target.id;
                    if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
                        return (e.keyCode != 8);
                }

            var msg = "||||||||||||||||||||"
            var x = ""
            num = 1
            toggle = 1
            tt = 1
            OOK = 0
            timval = 60
            var tval = ""
            function fasl() { timval = (timval == 60 ? 150 : 60) }

            function csd() {
                tt = (tt == 1 ? 0 : 1)
            }
            function scroll() {
                if (tt == 1)
                    if (num <= msg.length)
                        OOK = 1
                if (tt == 0)
                    if (num >= 0)
                        OOK = 1

                if (OOK == 1) {
                    OOK = 0
                    x = msg.substring(0, num)
                    document.getElementById('lblCargandoConsulta').innerHTML = x;
                    num = (tt == 1 ? num + 1 : num - 1)
                }
                else {
                    x = ""
                    document.getElementById('lblCargandoConsulta').innerHTML = "";
                    num = (tt == 1 ? 0 : msg.length)
                }
                if (toggle == 1)
                    setTimeout("scroll()", timval)
            }
            function toggla() {
                toggle = (toggle == 1 ? 0 : 1)
                if (toggle == 1) scroll()
            }
            function seleccionaDeseleccionaTable5(element) {
                $("#Table5").find("input[type=checkbox]").attr('checked', element.checked);
                if (element.checked) {
                    ValidatorEnable(document.getElementById("rfvFisGeneral"), false);
                    ValidatorEnable(document.getElementById("rfvFisPielFanelas"), false);
                    ValidatorEnable(document.getElementById("rfvFisCabeza"), false);
                    ValidatorEnable(document.getElementById("rfvFisConjuntiva"), false);
                    ValidatorEnable(document.getElementById("rfvFisReflejo"), false);

                    ValidatorEnable(document.getElementById("rfvFisPupilas"), false);
                    ValidatorEnable(document.getElementById("rfvFisOidos"), false);
                    ValidatorEnable(document.getElementById("rfvFisOtoscopia"), false);
                    ValidatorEnable(document.getElementById("rfvFisRinoscopia"), false);
                    ValidatorEnable(document.getElementById("rfvFisBocaFaringe"), false);

                    ValidatorEnable(document.getElementById("rfvFisAmigdalas"), false);
                    ValidatorEnable(document.getElementById("rfvFisCuello"), false);
                    ValidatorEnable(document.getElementById("rfvFisTiroides"), false);
                    ValidatorEnable(document.getElementById("rfvFisAdenopatias"), false);
                    ValidatorEnable(document.getElementById("rfvFisTorax"), false);

                    ValidatorEnable(document.getElementById("rfvFisRuidosCardiacos"), false);
                    ValidatorEnable(document.getElementById("rfvFisRuidosRespiratorios"), false);
                    ValidatorEnable(document.getElementById("rfvFisAbdomen"), false);
                    ValidatorEnable(document.getElementById("rfvFisPalpacionAbdomen"), false);
                    ValidatorEnable(document.getElementById("rfvFisGenitales"), false);

                    ValidatorEnable(document.getElementById("rfvFisHernias"), false);
                    ValidatorEnable(document.getElementById("rfvFisColumna"), false);
                    ValidatorEnable(document.getElementById("rfvFisExtremidadesSuperiores"), false);
                    ValidatorEnable(document.getElementById("rfvFisExtremidadesInferiores"), false);
                    ValidatorEnable(document.getElementById("rfvFisVarices"), false);

                    ValidatorEnable(document.getElementById("rfvFisNeurologico"), false);
                    ValidatorEnable(document.getElementById("rfvFisOtros"), false);
                }
                if (!element.checked) {
                    if (document.getElementById("esRequerida2").value == "1") {
                        ValidatorEnable(document.getElementById("rfvFisGeneral"), true);
                        ValidatorEnable(document.getElementById("rfvFisPielFanelas"), true);
                        ValidatorEnable(document.getElementById("rfvFisCabeza"), true);
                        ValidatorEnable(document.getElementById("rfvFisConjuntiva"), true);
                        ValidatorEnable(document.getElementById("rfvFisReflejo"), true);

                        ValidatorEnable(document.getElementById("rfvFisPupilas"), true);
                        ValidatorEnable(document.getElementById("rfvFisOidos"), true);
                        ValidatorEnable(document.getElementById("rfvFisOtoscopia"), true);
                        ValidatorEnable(document.getElementById("rfvFisRinoscopia"), true);
                        ValidatorEnable(document.getElementById("rfvFisBocaFaringe"), true);

                        ValidatorEnable(document.getElementById("rfvFisAmigdalas"), true);
                        ValidatorEnable(document.getElementById("rfvFisCuello"), true);
                        ValidatorEnable(document.getElementById("rfvFisTiroides"), true);
                        ValidatorEnable(document.getElementById("rfvFisAdenopatias"), true);
                        ValidatorEnable(document.getElementById("rfvFisTorax"), true);

                        ValidatorEnable(document.getElementById("rfvFisRuidosCardiacos"), true);
                        ValidatorEnable(document.getElementById("rfvFisRuidosRespiratorios"), true);
                        ValidatorEnable(document.getElementById("rfvFisAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvFisPalpacionAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvFisGenitales"), true);

                        ValidatorEnable(document.getElementById("rfvFisHernias"), true);
                        ValidatorEnable(document.getElementById("rfvFisColumna"), true);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesSuperiores"), true);
                        ValidatorEnable(document.getElementById("rfvFisExtremidadesInferiores"), true);
                        ValidatorEnable(document.getElementById("rfvFisVarices"), true);

                        ValidatorEnable(document.getElementById("rfvFisNeurologico"), true);
                        ValidatorEnable(document.getElementById("rfvFisOtros"), true);
                    }
                }
            }
            function seleccionaDeseleccionaTable2(element) {
                $('.Table2').find("input[type=checkbox]").attr('checked', element.checked);
                if (element.checked) {
                    ValidatorEnable(document.getElementById("rfvSisGeneral"), false);
                    ValidatorEnable(document.getElementById("rfvSisCabeza"), false);
                    ValidatorEnable(document.getElementById("rfvSisCuello"), false);
                    ValidatorEnable(document.getElementById("rfvSisTorax"), false);
                    ValidatorEnable(document.getElementById("rfvSisAbdomen"), false);
                    ValidatorEnable(document.getElementById("rfvSisOtros"), false);
                }
                if (!element.checked) {
                    if (document.getElementById("esRequerida").value == "1") {
                        ValidatorEnable(document.getElementById("rfvSisGeneral"), true);
                        ValidatorEnable(document.getElementById("rfvSisCabeza"), true);
                        ValidatorEnable(document.getElementById("rfvSisCuello"), true);
                        ValidatorEnable(document.getElementById("rfvSisTorax"), true);
                        ValidatorEnable(document.getElementById("rfvSisAbdomen"), true);
                        ValidatorEnable(document.getElementById("rfvSisOtros"), true);
                    }
                }

            }
            function ocultar() {
                alert("ocultar");
            }
        </script>

        <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0">
            <tbody>
                <tr>
                    <td align="center">&nbsp;
                    <uc1:WC_DatosEmpleado ID="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:ImageButton ID="imbHistorial" runat="server" CausesValidation="false" ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>&nbsp;<asp:LinkButton ID="lnkVerHistorico" runat="server" CausesValidation="false">Ver Histórico de Consultas</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imbVerHistorialOrdenes" runat="server" ImageUrl="../../images/icoHistorial.gif"
                        CausesValidation="false"></asp:ImageButton>&nbsp;
                    <asp:LinkButton ID="lnkVerHistorialOrdenes" runat="server" CausesValidation="false">Ver Histórico de Órdenes</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imblnkVerHistoria" runat="server" ImageUrl="../../images/icoHistorial.gif"
                        CausesValidation="false"></asp:ImageButton>&nbsp;
                    <asp:LinkButton ID="lnkVerHistoria" runat="server" CausesValidation="false">Ver Historia Clínica Consolidada</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imbGuardar" runat="server" ImageUrl="../../iconos/icoGuardar.gif"
                        CausesValidation="false" OnClick="imbGuardar_Click" />
                        <asp:LinkButton ID="lnkGuardar" runat="server" CausesValidation="false" OnClick="lnkGuardar_Click">Guardar</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblUltimaConsulta" runat="server" CssClass="textSmallRed"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                    </td>
                </tr>
                <tr>
                    <td align="center">&nbsp;
                    <fieldset class="FieldSet" style="width: 97%">
                        <legend>
                            <img src="../../images/icoHistoria.gif" border="0">
                            &nbsp;Datos de la Consulta</legend>
                        <br>
                        <table id="Table3" cellspacing="0" cellpadding="3" width="95%" align="center">
                            <tr>
                                <td width="20%" colspan="2">
                                    <table id="tblDatosConsulta" style="display: none" cellspacing="0" cellpadding="2"
                                        width="100%" align="center" runat="server">
                                        <tr>
                                            <td width="20%">No Consulta
                                            </td>
                                            <td width="30%">
                                                <asp:Label ID="lblNoConsulta" runat="server" CssClass="titleBig"></asp:Label>
                                            </td>
                                            <td align="right" width="20%">Fecha y Usuario Creación
                                            </td>
                                            <td width="30%">
                                                <asp:Label ID="lblFechaCreacion" runat="server"></asp:Label><br>
                                                <asp:Label ID="lblUsuarioCreacion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="Table8" cellspacing="0" cellpadding="2" width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="20%">Sede Consulta
                                                </td>
                                                <td width="30%">
                                                    <asp:DropDownList ID="ddlSede" runat="server" CssClass="textBoxSmall" Width="218px">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="cmvUsuario0" runat="server" CssClass="textRed" Operator="NotEqual"
                                                        ValueToCompare="0" ForeColor=" " Display="Dynamic" ControlToValidate="ddlSede"
                                                        ErrorMessage="">Requerido</asp:CompareValidator>
                                                </td>
                                                <td align="right" width="20%">
                                                    <asp:Label ID="lblLineaNegocio" runat="server" Text="Línea Negocio"
                                                        Visible="False"></asp:Label>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:DropDownList ID="ddlLineaNegocio" runat="server" CssClass="textBoxSmall"
                                                        Width="218px" Visible="False">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="cmvLineaNegocio" runat="server" CssClass="textRed" Operator="NotEqual"
                                                        ValueToCompare="0" ForeColor=" " Display="Dynamic" ControlToValidate="ddlLineaNegocio"
                                                        ErrorMessage="" Enabled="False">Requerido</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">Paciente<span class="textRed">*</span>
                                                </td>
                                                <td width="30%">
                                                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textBoxSmall" Width="218px">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="cmvUsuario" runat="server" CssClass="textRed" Operator="NotEqual"
                                                        ValueToCompare="-1" ForeColor=" " Display="Dynamic" ControlToValidate="ddlUsuario"
                                                        ErrorMessage="">Requerido</asp:CompareValidator><asp:TextBox ID="txtOtroBeneficiario"
                                                            runat="server" Width="0px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="20%">Médico<asp:Label ID="lblValidadorMedico" runat="server" CssClass="textRed" Text="*"
                                                    Visible="False"></asp:Label>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:Label ID="lblSolicitante" runat="server"></asp:Label><asp:UpdatePanel ID="Ajaxpanel1"
                                                        runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlPrestador" runat="server" CssClass="textBox" Width="200px"
                                                                Visible="False">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblEspecialidad" runat="server" CssClass="textSmallBlue"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="Ajaxpanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtPrestador" runat="server" CssClass="textBox" Width="170px" Visible="False"></asp:TextBox>&nbsp;<input
                                                                class="buttonSmall" id="btnBuscarPrestador" style="display: none" onclick="javascript: ShowPrestador(this);"
                                                                type="button" value="..." name="btnBuscarPrestador" runat="server">
                                                            <asp:TextBox ID="txtIdPrestador" runat="server" Width="0px"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:CompareValidator ID="cmvSolicitante" runat="server" CssClass="textRed" Operator="NotEqual"
                                                        ValueToCompare="0" ForeColor=" " Display="Dynamic" ControlToValidate="ddlPrestador"
                                                        ErrorMessage="" Enabled="False">Requerido</asp:CompareValidator><asp:RequiredFieldValidator
                                                            ID="rfvSolicitante" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                            ControlToValidate="txtPrestador" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%"></td>
                                <td width="80%"></td>
                            </tr>
                            <tr>
                                <td width="20%">Tipo de Consultas<span class="textRed">*</span>
                                </td>
                                <td width="80%">
                                    <asp:RadioButtonList ID="rblTipoConsulta" runat="server" RepeatColumns="2">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvTipoConsulta" runat="server" CssClass="textRed"
                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblTipoConsulta" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trSolicitanteTranscripcion" style="display: none" runat="server">
                                <td>Solicitante
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="Ajaxpanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlSolicitanteTranscripcion" runat="server" CssClass="textBox"
                                                Width="200px" Visible="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="Ajaxpanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtSolicitanteTranscripcion" runat="server" CssClass="textBox" Width="170px"
                                                Visible="False"></asp:TextBox>&nbsp;
                                            <input class="buttonSmall" id="btnBuscarSolicitanteTranscripcion" style="display: none"
                                                type="button" value="..." name="btnBuscarSolicitanteTranscripcion" runat="server">
                                            <asp:TextBox ID="txtIdSolicitanteTranscripcion" runat="server" Width="0px"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:CompareValidator ID="cmvSolicitanteTranscripcion" runat="server" CssClass="textRed"
                                        Operator="NotEqual" ValueToCompare="0" ForeColor=" " Display="Dynamic" ControlToValidate="ddlSolicitanteTranscripcion"
                                        ErrorMessage="" Enabled="False">Requerido</asp:CompareValidator><asp:RequiredFieldValidator
                                            ID="rfvSolicitanteTranscripcion" runat="server" CssClass="textRed" ForeColor=" "
                                            Display="Dynamic" ControlToValidate="txtSolicitanteTranscripcion" ErrorMessage=""
                                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo de Enfermedad<span class="textRed">*</span>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblTipoEnfermedad" runat="server" Width="600px" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvTipoEnfermedad" runat="server" CssClass="textRed"
                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblTipoEnfermedad" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">Motivo de Consulta<%--<span class="textRed"><label id="lblObligatorioMotivo">*</label></span>--%>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="lblUltimaConsulta1" runat="server" CssClass="textSmallRed"></asp:Label>&nbsp;
                                    <asp:Image ID="imgBorrar1" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                        ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtMotivo');" /><br />
                                    <asp:TextBox ID="txtMotivo" runat="server" CssClass="textBox" Width="550px" MaxLength="2000"
                                        onkeypress='return (this.value.length < 2000);' TextMode="MultiLine" Height="40px"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rfvMotivo" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                        ControlToValidate="txtMotivo" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Descripción Padecimiento Actual (signos/síntomas)<span class="textRed"><label id="lblObligatorioEnfermedadActual">*</label></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEnfermedad" runat="server" CssClass="textBox" Width="550px" MaxLength="500"
                                        onkeypress='return (this.value.length < 500);' TextMode="MultiLine" Height="60px"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="rfvEnfermedadActual" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                            ControlToValidate="txtEnfermedad" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Contrarreferencia
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContrareferencia" runat="server" CssClass="textBox" Width="550px"
                                        Height="40px" TextMode="MultiLine" MaxLength="2000" onkeypress='return (this.value.length < 2000);'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="tableBorder" id="Table1" cellspacing="0" cellpadding="3" width="100%"
                                        align="center">
                                        <tr>
                                            <td class="headerTable" colspan="3">ANTECEDENTES
                                                <asp:ImageButton ID="imbHistorialAntecedentes" runat="server" CausesValidation="false"
                                                    ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">Médicos
                                            </td>
                                            <td width="15%">
                                                <asp:CheckBox ID="chkAnteMedicos" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td width="70%">
                                                <asp:Label ID="lblUltimaConsulta12" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar8" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteMedicos');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteMedicos" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteMedicos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteMedicos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Quirúrgicos
                                                </p>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteQuirurgicos" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta13" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar9" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteQuirurgicos');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteQuirurgicos" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteQuirurgicos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteQuirurgicos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ginecoobstétricos
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteGineco" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta3" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar2" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteGineco');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteGineco" runat="server" CssClass="textBox" Width="440px" MaxLength="300"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteGineco" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteGineco" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px" colspan="3">
                                                <table id="Table4" cellspacing="0" cellpadding="4" width="100%" align="center">
                                                    <tr style="height: 5px">
                                                        <td colspan="14">
                                                            <asp:Label ID="lblUltimaConsulta15" runat="server" CssClass="textSmallRed"></asp:Label>
                                                            <asp:TextBox ID="txtGenero" runat="server" CssClass="textBox" Width="0px" MaxLength="1"
                                                                onkeypress='return (this.value.length < 30);'></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Menarquia<br />
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtMenarquia" runat="server" CssClass="textBox" Width="50px" MaxLength="30"
                                                                onkeypress='return (this.value.length < 30);'></asp:TextBox>
                                                            <asp:Image ID="imgBorrar11" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtMenarquia');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvMenarquia" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtMenarquia" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>FUM
                                                            <br />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaUltimaMestruacion" runat="server" CssClass="textBox" Width="50px"
                                                                MaxLength="30" onkeypress='return (this.value.length < 30);'></asp:TextBox>
                                                            <asp:Image ID="imgBorrar12" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtFechaUltimaMestruacion');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvFUM" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtFechaUltimaMestruacion" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>G<br />
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                                ID="txtGestaciones" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                                                            <asp:Image ID="imgBorrar13" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtGestaciones');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvGestaciones" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtGestaciones" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>P
                                                            <br />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                                ID="txtPartos" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                                                            <asp:Image ID="imgBorrar14" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtPartos');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvPartos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtPartos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>C<br />
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                                ID="txtCesareas" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                                                            <asp:Image ID="imgBorrar15" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtCesareas');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvCesareas" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtCesareas" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>A
                                                            <br />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                                ID="txtAbortos" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                                                            <asp:Image ID="imgBorrar16" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAbortos');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvAbortos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtAbortos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>NV
                                                            <br />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                                ID="txtVivos" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                                                            <asp:Image ID="imgBorrar17" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                                ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtVivos');" />

                                                            <asp:RequiredFieldValidator
                                                                ID="rfvVivos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtVivos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Transfusionales
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteTransfusionales" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta14" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar10" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteTransfusionales');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteTransfusionales" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteTransfusionales" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteTransfusionales" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 37px">Tóxico-Alérgicos
                                            </td>
                                            <td style="height: 37px">
                                                <asp:CheckBox ID="chkAnteToxico" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td style="height: 37px">
                                                <asp:Label ID="lblUltimaConsulta2" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar3" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteToxico');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteToxico" runat="server" CssClass="textBox" Width="440px" MaxLength="300"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteToxico" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteToxico" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Farmacológicos
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteFarmacologicos" runat="server" Text="Normal"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkRiesgoCardiovascular" runat="server" Text="Riesgo Cardiovascular"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta4" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar4" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteFarmacologicos');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteFarmacologicos" runat="server" CssClass="textBox" Width="440px"
                                                    TextMode="MultiLine" MaxLength="300" onkeypress='return (this.value.length < 300);'
                                                    Height="50px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAnteFarmacologicos"
                                                        runat="server" CssClass="textRed" ErrorMessage="" ControlToValidate="txtAnteFarmacologicos"
                                                        Display="Dynamic" ForeColor=" " Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Otros Antecedentes
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteOtros" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta5" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar5" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteOtros');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteOtros" runat="server" CssClass="textBox" Width="440px" MaxLength="300"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteOtros" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteOtros" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Familiares
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAnteFamiliares" runat="server" Text="Normal"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUltimaConsulta6" runat="server" CssClass="textSmallRed"></asp:Label>
                                                &nbsp;<asp:Image ID="imgBorrar6" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtAnteFamiliares');" />
                                                <br />
                                                <asp:TextBox ID="txtAnteFamiliares" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvAnteFamiliares" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtAnteFamiliares" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="Interrogatorio">
                                    <table class="tableBorder Table2" id="Table2" cellspacing="0" cellpadding="3" width="100%" align="center">
                                        <tr>
                                            <td class="headerTable" colspan="3">INTERROGATORIO POR APARATOS Y SISTEMAS
                                                <asp:ImageButton ID="imbHistorialRevision" runat="server" CausesValidation="false"
                                                    ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%"></td>
                                            <td width="25%">El paciente no refiere datos patológicos o enfermedades relacionadas al momento del interrogatorio.
                                            </td>
                                            <td width="65%" style="padding-left: 10px;">
                                                <input type="checkbox" name="seleccionarAllInterrogatorio" onclick="seleccionaDeseleccionaTable2(this)">
                                                Seleccionar o deseleccionar todo<br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%">Aspecto General
                                            </td>
                                            <td width="25%" align="center">
                                                <asp:CheckBox ID="chkSisGeneral" runat="server" Text="" CssClass="prueba"></asp:CheckBox>
                                            </td>
                                            <td width="65%">
                                                <asp:TextBox ID="txtSisGeneral" runat="server" CssClass="textBox" Width="440px" MaxLength="300"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisGeneral" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisGeneral" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                <input type="hidden" id="esRequerida" value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Cabeza
                                                </p>
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkSisCabeza" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSisCabeza" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisCabeza" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisCabeza" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cuello
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkSisCuello" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSisCuello" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisCuello" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisCuello" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tórax
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkSisTorax" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSisTorax" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisTorax" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisTorax" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Abdomen
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkSisAbdomen" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSisAbdomen" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisAbdomen" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisAbdomen" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Otros
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkSisOtros" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSisOtros" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSisOtros" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtSisOtros" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="divHabitos" runat="server" visible="false">
                                        <table class="tableBorder" id="Table9" cellspacing="0" cellpadding="3" width="100%"
                                            align="center">
                                            <tr>
                                                <td class="headerTable" colspan="4">HÁBITOS&nbsp;
                                                    <asp:ImageButton ID="imbHistorialHabitos" runat="server" CausesValidation="false"
                                                        ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">Tabaquismo
                                                </td>
                                                <td width="30%">
                                                    <asp:RadioButtonList ID="rblTabaquismo" runat="server" Width="112px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvTabaquismo" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="rblTabaquismo" ErrorMessage=""
                                                        Enabled="False">Requerido</asp:RequiredFieldValidator><br>
                                                    Frecuencia&nbsp;
                                                    <asp:TextBox ID="txtFrecuenciaTabaquismo" runat="server" CssClass="textBox" Width="150px"
                                                        MaxLength="100" onkeypress='return (this.value.length < 100);'></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rfvFrecuenciaTabaquismo" runat="server" CssClass="textRed" ForeColor=" "
                                                            Display="Dynamic" ControlToValidate="txtFrecuenciaTabaquismo" ErrorMessage=""
                                                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="20%">Actividad Deportiva
                                                </td>
                                                <td width="30%">
                                                    <asp:RadioButtonList ID="rblDeportiva" runat="server" Width="112px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvActividad" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="rblDeportiva" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">Consumo de Alcohol
                                                </td>
                                                <td width="30%">
                                                    <asp:RadioButtonList ID="rblAlcohol" runat="server" Width="112px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvAlcohol" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="rblAlcohol" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>Frecuencia&nbsp;
                                                    <asp:TextBox ID="txtFrecuenciaAlcohol" runat="server" CssClass="textBox" Width="150px"
                                                        MaxLength="100" onkeypress='return (this.value.length < 100);'></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rfvFrecuenciaAlcohol" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                            ControlToValidate="txtFrecuenciaAlcohol" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="20%">Vacunación
                                                </td>
                                                <td width="30%">
                                                    <asp:TextBox ID="txtVacunacion" runat="server" CssClass="textBox" Width="220px" MaxLength="200"
                                                        onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rfvVacunacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                            ControlToValidate="txtVacunacion" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="ExploracionFisica">
                                    <table class="tableBorder" id="Table5" cellspacing="0" cellpadding="3" width="100%"
                                        align="center">
                                        <tr>
                                            <td class="headerTable" colspan="6">EXPLORACIÓN FÍSICA
                                                <asp:ImageButton ID="imbHistorialExamen" runat="server" CausesValidation="false"
                                                    ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">Peso<span class="textRed">
                                                <asp:RequiredFieldValidator ID="rfvPeso1" runat="server" CssClass="textRed" ErrorMessage=""
                                                    ControlToValidate="txtPeso" ForeColor=" ">*</asp:RequiredFieldValidator></span>
                                            </td>
                                            <td width="19%">
                                                <asp:Label ID="lblUltimaConsulta7" runat="server" CssClass="textSmallRed"></asp:Label>
                                                <br />
                                                <asp:TextBox onkeypress="return SoloNumero(event, this)" onkeydown="return keyDown(this,event)"
                                                    ID="txtPeso" onmouseout="javascript:CalcularIMC()" runat="server" CssClass="textBox"
                                                    Width="70px"></asp:TextBox>&nbsp;Kgs
                                                <br>
                                                <asp:RequiredFieldValidator ID="rfvPeso" runat="server" CssClass="textRed" ForeColor=" "
                                                    Display="Dynamic" ControlToValidate="txtPeso" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                            <td width="11%">Talla
                                                <asp:RequiredFieldValidator ID="rfvTalla1" runat="server" CssClass="textRed" ErrorMessage=""
                                                    ControlToValidate="txtTalla" ForeColor=" " Enabled="False">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td width="19%">
                                                <asp:Label ID="lblUltimaConsulta8" runat="server" CssClass="textSmallRed"></asp:Label>
                                                <br />
                                                <asp:TextBox onkeypress="return SoloNumero(event, this)" onkeydown="return keyDown(this,event)"
                                                    ID="txtTalla" onmouseout="javascript:CalcularIMC()" runat="server" CssClass="textBox"
                                                    Width="70px"></asp:TextBox>&nbsp;Mts
                                                <br>
                                                <asp:RequiredFieldValidator ID="rfvTalla" runat="server" CssClass="textRed" ErrorMessage=""
                                                    ControlToValidate="txtTalla" Display="Dynamic" ForeColor=" " Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                            <td width="12%">IMC
                                                <br>
                                                <span class="textSmallBlack">(Índice de masa corporal)</span>
                                            </td>
                                            <td width="19%">
                                                <asp:TextBox ID="txtIMC" runat="server" CssClass="LabelNoModify" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Tensión Arterial
                                                    <asp:RequiredFieldValidator ID="rfvTension1" runat="server" CssClass="textRed" ErrorMessage=""
                                                        ControlToValidate="txtTension" ForeColor=" ">*</asp:RequiredFieldValidator>
                                                </p>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMedia" runat="server">Media</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvTensionNormal1" runat="server" CssClass="textRed"
                                                    ErrorMessage="" ControlToValidate="txtTension" ForeColor=" ">*</asp:RequiredFieldValidator>&nbsp;<asp:TextBox
                                                        ID="txtTension" runat="server" CssClass="textBox" Width="70px" MaxLength="10"></asp:TextBox>
                                                <asp:TextBox ID="txtTensionMedia" runat="server" CssClass="LabelNoModify" Width="70px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvTension" runat="server" CssClass="textRed" ForeColor=" "
                                                    Display="Dynamic" ControlToValidate="txtTension" ErrorMessage="">Requerido</asp:RequiredFieldValidator><br />
                                                <div id="divDiastolicaSisTolica" runat="server" visible="false">
                                                    <asp:Label ID="lblUltimaConsulta10" runat="server" CssClass="textSmallRed"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblSistolica" runat="server" Visible="true">Sistólica</asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvSistolica1" runat="server" CssClass="textRed"
                                                        ErrorMessage="" ControlToValidate="txtSistolica" ForeColor=" ">*</asp:RequiredFieldValidator>&nbsp;
                                                    <asp:TextBox ID="txtSistolica" onkeydown="return keyDown(this,event)" onmouseout="javascript:CalcularTensionMedia()"
                                                        onkeypress="return currencyFormat(this,event,true,false)" runat="server" CssClass="textBox"
                                                        Width="65px" Visible="true" MaxLength="10"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvSistolica" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="txtSistolica" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:Label ID="lblDiastolica" runat="server" Visible="true">Diastólica</asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvDiastolica1" runat="server" CssClass="textRed"
                                                        ErrorMessage="" ControlToValidate="txtDiastolica" ForeColor=" ">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDiastolica" onkeydown="return keyDown(this,event)" onmouseout="javascript:CalcularTensionMedia()"
                                                        onkeypress="return currencyFormat(this,event,true,false)" runat="server" CssClass="textBox"
                                                        Width="65px" Visible="true" MaxLength="10"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvDiastolica" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="txtDiastolica" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>Frecuencia Cardiaca
                                                <asp:RequiredFieldValidator ID="rfvFrecuenciaCar1" runat="server" CssClass="textRed"
                                                    ErrorMessage="" ControlToValidate="txtFrecuenciaCar" ForeColor=" ">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                    ID="txtFrecuenciaCar" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;x
                                                minuto<br>
                                                <asp:RequiredFieldValidator ID="rfvFrecuenciaCar" runat="server" CssClass="textRed"
                                                    ForeColor=" " Display="Dynamic" ControlToValidate="txtFrecuenciaCar" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Frecuencia Respiratoria
                                                <asp:RequiredFieldValidator ID="rfvFrecuenciaRes1" runat="server" CssClass="textRed"
                                                    ErrorMessage="" ControlToValidate="txtFrecuenciaRes" ForeColor=" ">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                                    ID="txtFrecuenciaRes" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;x
                                                minuto<br>
                                                <asp:RequiredFieldValidator ID="rfvFrecuenciaRes" runat="server" CssClass="textRed"
                                                    ForeColor=" " Display="Dynamic" ControlToValidate="txtFrecuenciaRes" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Temperatura
                                                <asp:RequiredFieldValidator ID="rfvTemperatura1" runat="server" CssClass="textRed"
                                                    ErrorMessage="" ControlToValidate="txtTemperatura" ForeColor=" ">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return SoloNumero(event, this)" onkeydown="return keyDown(this,event)"
                                                    ID="txtTemperatura" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;°C
                                                <asp:RequiredFieldValidator ID="rfvTemperatura" runat="server" CssClass="textRed"
                                                    ForeColor=" " Display="Dynamic" ControlToValidate="txtTemperatura" ErrorMessage=""
                                                    Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                            <div id="divPerimetroAbdominal" runat="server" visible="false">
                                                <td>Perímetro Abdominal
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
                                                        ID="txtPerimetroAbdominal" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;cms
                                                    <asp:RequiredFieldValidator ID="rfvPerimetroAbdominal" runat="server" CssClass="textRed"
                                                        ForeColor=" " Display="Dynamic" ControlToValidate="txtPerimetroAbdominal" ErrorMessage=""
                                                        Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                            </div>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="margin-left: 40px; text-align: justify;">No se encuentran datos patológicos o signos de enfermedades.
                                            </td>
                                            <td style="padding-left: 10px;">
                                                <input type="checkbox" name="seleccionarAllExploracion" onclick="seleccionaDeseleccionaTable5(this)">
                                                Seleccionar o deseleccionar todo<br>
                                            </td>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td>Aspecto General
                                            </td>
                                            <td style="margin-left: 40px" align="center">
                                                <asp:CheckBox ID="chkFisGeneral" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisGeneral" runat="server" CssClass="textBox" Width="440px" MaxLength="300"
                                                    onkeypress='return (this.value.length < 300);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisGeneral" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisGeneral" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                <input type="hidden" id="esRequerida2" value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Piel y Faneras
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisPielFanelas" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisPielFanelas" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisPielFanelas" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisPielFanelas" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Cabeza
                                                </p>
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisCabeza" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisCabeza" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisCabeza" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisCabeza" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Conjuntiva Ocular
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisConjuntiva" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisConjuntiva" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisConjuntiva" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisConjuntiva" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Reflejo Corneal
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisReflejo" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisReflejo" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisReflejo" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisReflejo" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Pupilas
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisPupilas" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisPupilas" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisPupilas" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisPupilas" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Oídos
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisOidos" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisOidos" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisOidos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisOidos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Otoscopia
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisOtoscopia" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisOtoscopia" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisOtoscopia" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisOtoscopia" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Rinoscopia
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisRinoscopia" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisRinoscopia" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisRinoscopia" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisRinoscopia" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Boca y Faringe
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisBocaFaringe" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisBocaFaringe" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisBocaFaringe" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisBocaFaringe" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Amígdalas
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisAmigdalas" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisAmigdalas" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisAmigdalas" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisAmigdalas" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cuello
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisCuello" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisCuello" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisCuello" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisCuello" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tiroides
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisTiroides" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisTiroides" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisTiroides" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisTiroides" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Adenopatías
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisAdenopatias" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisAdenopatias" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisAdenopatias" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisAdenopatias" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tórax
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisTorax" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisTorax" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisTorax" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisTorax" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ruidos Cardiacos
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisRuidosCardiacos" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisRuidosCardiacos" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisRuidosCardiacos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisRuidosCardiacos" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ruidos Respiratorios
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisRuidosRespiratorios" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisRuidosRespiratorios" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisRuidosRespiratorios" runat="server" CssClass="textRed" ForeColor=" "
                                                        Display="Dynamic" ControlToValidate="txtFisRuidosRespiratorios" ErrorMessage=""
                                                        Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Abdomen
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisAbdomen" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisAbdomen" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisAbdomen" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisAbdomen" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Palpación Abdomen
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisPalpacionAbdomen" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisPalpacionAbdomen" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisPalpacionAbdomen" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisPalpacionAbdomen" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Genitales Externos
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisGenitales" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisGenitales" runat="server" CssClass="textBox" Width="440px"
                                                    MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisGenitales" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisGenitales" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Hernias
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisHernias" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisHernias" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisHernias" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisHernias" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Columna Vertebral
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisColumna" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisColumna" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisColumna" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisColumna" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Extremidades Superiores
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisExtremidadesSuperiores" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisExtremidadesSuperiores" runat="server" CssClass="textBox"
                                                    Width="440px" MaxLength="200" onkeypress='return (this.value.length < 200);'
                                                    TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFisExtremidadesSuperiores"
                                                        runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFisExtremidadesSuperiores"
                                                        ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                            <tr>
                                                <td>Extremidades Inferiores
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkFisExtremidadesInferiores" runat="server" Text=""></asp:CheckBox>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtFisExtremidadesInferiores" runat="server" CssClass="textBox"
                                                        Width="440px" MaxLength="200" onkeypress='return (this.value.length < 200);'
                                                        TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFisExtremidadesInferiores"
                                                            runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic" ControlToValidate="txtFisExtremidadesInferiores"
                                                            ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Várices
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkFisVarices" runat="server" Text=""></asp:CheckBox>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtFisVarices" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                        onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rfvFisVarices" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                            ControlToValidate="txtFisVarices" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <tr>
                                                    <td>Neurológico
                                                    </td>
                                                    <td align="center">
                                                        <asp:CheckBox ID="chkFisNeurologico" runat="server" Text=""></asp:CheckBox>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtFisNeurologico" runat="server" CssClass="textBox" Width="440px"
                                                            MaxLength="200" onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="rfvFisNeurologico" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                                ControlToValidate="txtFisNeurologico" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </tr>
                                        <tr>
                                            <td>Otros
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="chkFisOtros" runat="server" Text=""></asp:CheckBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtFisOtros" runat="server" CssClass="textBox" Width="440px" MaxLength="200"
                                                    onkeypress='return (this.value.length < 200);' TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvFisOtros" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                                        ControlToValidate="txtFisOtros" ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Comentarios Examen Físico
                                            </td>
                                            <td colspan="5">
                                                <asp:TextBox ID="txtComentariosFisico" runat="server" CssClass="textBox" Width="550px"
                                                    MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"
                                                    Height="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        <td colspan="2" id="pruebasBiometricas">
                            <table class="tableBorder" id="Table10" cellspacing="0" cellpadding="3" width="100%"
                                align="center">
                                <div id="divPruebasBiometricas" runat="server" visible="false">
                                    <tr>
                                        <td class="headerTable" colspan="6">PRUEBAS BIOMÉTRICAS
                                        <asp:ImageButton ID="Imagebutton1" runat="server" CausesValidation="false" ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </div>
                                <div id="divColesterolGlicemia" runat="server" visible="false">
                                <tr>
                                    <td width="20%">Colesterol total<span class="textRed">
                                        <asp:RequiredFieldValidator ID="rfvColesterolTotal1" runat="server" CssClass="textRed"
                                            ErrorMessage="" ControlToValidate="txtColesterolTotal" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator></span>
                                    </td>
                                    <td width="19%">
                                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                            ID="txtColesterolTotal" runat="server" CssClass="textBox" Width="70px" MaxLength="3"></asp:TextBox>&nbsp;mg/dl
                                    <br>
                                        <asp:RequiredFieldValidator ID="rfvColesterolTotal" runat="server" CssClass="textRed"
                                            ForeColor=" " Display="Dynamic" ControlToValidate="txtColesterolTotal" ErrorMessage=""
                                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="17%">Colesterol HDL
                                    <asp:RequiredFieldValidator ID="rfvColesterolHDL1" runat="server" CssClass="textRed"
                                        ErrorMessage="" ControlToValidate="txtColesterolHDL" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="17%">
                                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                                            ID="txtColesterolHDL" runat="server" CssClass="textBox" Width="70px" MaxLength="2"></asp:TextBox>&nbsp;mg/dl
                                    <br>
                                        <asp:RequiredFieldValidator ID="rfvColesterolHDL" runat="server" CssClass="textRed"
                                            ErrorMessage="" ControlToValidate="txtColesterolHDL" Display="Dynamic" ForeColor=" "
                                            Enabled="false">Requerido</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
                                            ID="txtColesterolHDLmmol" runat="server" CssClass="textBox" Width="70px" MaxLength="2"></asp:TextBox>&nbsp;mmol/L
                                    <br>
                                        <asp:RequiredFieldValidator ID="rfvColesterolHDLmmol" runat="server" CssClass="textRed"
                                            ErrorMessage="" ControlToValidate="txtColesterolHDLmmol" Display="Dynamic" ForeColor=" "
                                            Enabled="false">Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="11%">Colesterol LDL
                                    <asp:RequiredFieldValidator ID="rfvColesterolLDL1" runat="server" CssClass="textRed"
                                        ErrorMessage="" ControlToValidate="txtColesterolLDL" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                                    </td>
                        </td>
                    <td width="19%">
                        <asp:TextBox ID="txtColesterolLDL" runat="server" CssClass="textBox" Width="70px"
                            onkeypress="return currencyFormat(this,event,true,false)" MaxLength="3"></asp:TextBox>&nbsp;mg/dl
                    <br>
                        <asp:RequiredFieldValidator ID="rfvColesterolLDL" runat="server" CssClass="textRed"
                            ErrorMessage="" ControlToValidate="txtColesterolLDL" Display="Dynamic" ForeColor=" "
                            Enabled="false">Requerido</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            Triglicéridos
                        <asp:RequiredFieldValidator ID="rfvTrigliceridos1" runat="server" CssClass="textRed"
                            ErrorMessage="" ControlToValidate="txtTrigliceridos" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                        </p>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTrigliceridos" runat="server" CssClass="textBox" Width="70px"
                            MaxLength="3" onkeypress="return currencyFormat(this,event,true,false)"></asp:TextBox>&nbsp;mg/dl
                    <br>
                        <asp:RequiredFieldValidator ID="rfvTrigliceridos" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="txtTrigliceridos" ErrorMessage=""
                            Enabled="false">Requerido</asp:RequiredFieldValidator>
                    </td>
                    <td>Índice Aterogénico
                    <asp:RequiredFieldValidator ID="rfvIndiceAterogenico1" runat="server" CssClass="textRed"
                        ErrorMessage="" ControlToValidate="txtIndiceAterogenico" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox onkeypress="return currencyFormatOneDecimal(this,event,true,true,1);"
                            onkeydown="return keyDown(this,event);" runat="server" ID="txtIndiceAterogenico"
                            runat="server" CssClass="textBox" Width="70px" MaxLength="4"></asp:TextBox><br>
                        <asp:RequiredFieldValidator ID="rfvIndiceAterogenico" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="txtIndiceAterogenico" ErrorMessage=""
                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                    </td>
                    <td>Antígeno Específico de Próstata
                    <asp:RequiredFieldValidator ID="rfvAntigenoProstata1" runat="server" CssClass="textRed"
                        ErrorMessage="" ControlToValidate="txtAntigenoProstata" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
                            ID="txtAntigenoProstata" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;mg/dl<br>
                        <asp:RequiredFieldValidator ID="rfvAntigenoProstata" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="txtAntigenoProstata" ErrorMessage=""
                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Glicemia en Ayunas
                    <asp:RequiredFieldValidator ID="rfvGlucemiaAyunas1" runat="server" CssClass="textRed"
                        ErrorMessage="" ControlToValidate="txtGlucemiaAyunas" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                            ID="txtGlucemiaAyunas" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;mg/dl<br>
                        <asp:RequiredFieldValidator ID="rfvGlucemiaAyunas" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="txtGlucemiaAyunas" ErrorMessage=""
                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                    </td>
                    <td>Hemoglobina Glucosilada<asp:RequiredFieldValidator ID="rfvHemoglobinaGlucosilada1"
                        runat="server" CssClass="textRed" ErrorMessage="" ControlToValidate="txtHemoglobinaGlucosilada"
                        ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td colspan="1">
                        <asp:TextBox onkeypress="return currencyFormatOneDecimal(this,event,true,true,1)"
                            onkeydown="return keyDown(this,event)" ID="txtHemoglobinaGlucosilada" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;%
                    <br />
                        <asp:RequiredFieldValidator ID="rfvHemoglobinaGlucosilada" runat="server" ControlToValidate="txtHemoglobinaGlucosilada"
                            CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                    </td>
                    <td>Homocisteína<asp:RequiredFieldValidator ID="rfvHomocisteina1" runat="server" CssClass="textRed"
                        ErrorMessage="" ControlToValidate="txtHomocisteina" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
                            ID="txtHomocisteina" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;micromol/L
                    <br />
                        <asp:RequiredFieldValidator ID="rfvHomocisteina" runat="server" ControlToValidate="txtHomocisteina"
                            CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Glicemia sin Ayunas
                    <asp:RequiredFieldValidator ID="rfvGlucemiaSinAyunas1" runat="server" CssClass="textRed"
                        ErrorMessage="" ControlToValidate="txtGlicemiaSinAyunas" ForeColor=" " Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" onkeydown="return keyDown(this,event)"
                            ID="txtGlicemiaSinAyunas" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>&nbsp;mg/dl<br>
                        <asp:RequiredFieldValidator ID="rfvGlucemiaSinAyunas" runat="server" CssClass="textRed"
                            ForeColor=" " Display="Dynamic" ControlToValidate="txtGlicemiaSinAyunas" ErrorMessage=""
                            Enabled="False">Requerido</asp:RequiredFieldValidator>
                    </td>

                </tr>
                </div>
            <div id="divExamenesLaboratorio" runat="server" visible="false">
                <tr>
                    <td width="20%">Exámenes de Laboratorio
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtExamenesLaboratorio" runat="server" CssClass="textBox" Width="550px"
                            MaxLength="500" onkeypress='return (this.value.length < 500);' TextMode="MultiLine"
                            Height="40px"></asp:TextBox>
                    </td>
                </tr>
            </div>
                <div id="divMujer" runat="server" visible="false">
                    <tr>
                        <td width="20%" valign="top">Papanicolau Microbiológico
                        </td>
                        <td colspan="3" style="width: 38%">
                            <asp:RadioButtonList ID="rblPresenciaMicroorganismos" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Ausencia de Infección</asp:ListItem>
                                <asp:ListItem Value="0">Presencia Microorganismo</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvPresenciaMicroorganismos" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="rblPresenciaMicroorganismos"
                                ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator><br />
                        </td>
                        <td width="12%">Fecha
                        </td>
                        <td width="19%">
                            <asp:TextBox ID="txtFechaPapanicolauMicro" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                href="javascript:MostrarCalendario(Form1.txtFechaPapanicolauMicro,Form1.txtFechaPapanicolauMicro,'dd/mm/yyyy');"
                                name="btnFechaPapanicolau"><img src="../../images/icoCalendar.gif" border="0" id="imgPapanicolau"
                                    runat="server"></a>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvFechaPapanicolau" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtFechaPapanicolauMicro"
                                ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" valign="top">&nbsp;
                        </td>
                        <td>Observaciones
                        </td>
                        <td width="19%" colspan="4">
                            <asp:TextBox ID="txtObservacionesPresenciaMicro" runat="server" CssClass="textBox"
                                onkeypress='return (this.value.length < 300);' Width="470px" MaxLength="300"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvObservacionesPresenciaMicro" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="txtObservacionesPresenciaMicro"
                                ErrorMessage="" Enabled="False">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" valign="top">Papanicolau Morfológico
                        </td>
                        <td width="19%" colspan="5" style="width: 30%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlResultadoMorfologico" runat="server" CssClass="textBox"
                                        Visible="True" AutoPostBack="True" OnSelectedIndexChanged="ddlResultadoMorfologico_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvResultadoMorfologico" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlResultadoMorfologico" ErrorMessage=""
                                Enabled="False">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td width="80%" colspan="5">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblAnormalCelulasEpi" runat="server" Text="Anormalidades en células epiteliales"
                                        Visible="False"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlAnormalCelulasEpi" runat="server" CssClass="textBox" Visible="False"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlAnormalCelulasEpi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvAnormalCelulasEpi" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlAnormalCelulasEpi" ErrorMessage=""
                                Enabled="False">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td width="80%" colspan="5">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblCelulasEscamosas" runat="server" Text="ASC (Células escamosas atípicas)"
                                        Visible="False"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlCelulasEscamosas" runat="server" CssClass="textBox" Visible="False">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvCelulasEscamosas" runat="server" ControlToValidate="ddlCelulasEscamosas"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">RequeridoRequerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">Mamografía<asp:RequiredFieldValidator ID="rfvMamografia1" runat="server" CssClass="textRed"
                            ErrorMessage="" ControlToValidate="ddlMamografia" ForeColor=" " Enabled="False">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="19%" colspan="5">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlMamografia" runat="server" CssClass="textBox" Visible="True">
                                    </asp:DropDownList>
                                    &nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvMamografia" runat="server" CssClass="textRed"
                                ForeColor=" " Display="Dynamic" ControlToValidate="ddlMamografia" ErrorMessage=""
                                Enabled="False">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Observaciones
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtMamografiaObservaciones" runat="server" CssClass="textBox" MaxLength="300"
                                onkeypress='return (this.value.length < 300);' Width="470px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMamografiaObservaciones" runat="server" ControlToValidate="txtMamografiaObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </div>
                <div id="divAudiometria" runat="server" visible="false">
                    <tr>
                        <td width="20%" valign="top">Audiometría<asp:RequiredFieldValidator ID="rfvAudiometria1" runat="server" CssClass="textRed"
                            ErrorMessage="" ControlToValidate="rblAudiometria" ForeColor=" " Enabled="False">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="19%">
                            <asp:RadioButtonList ID="rblAudiometria" runat="server" RepeatDirection="Horizontal"
                                Width="112px">
                                <asp:ListItem Value="1">Normal</asp:ListItem>
                                <asp:ListItem Value="0">Anormal</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvAudiometria" runat="server" ControlToValidate="rblAudiometria"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="11%" colspan="4">
                            <asp:TextBox ID="txtAudiometriaObservaciones" runat="server" CssClass="textBox" MaxLength="300"
                                onkeypress='return (this.value.length < 300);' Width="470px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAudiometriaObservaciones" runat="server" ControlToValidate="txtAudiometriaObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" valign="top">Rayos X (de tórax)<asp:RequiredFieldValidator ID="rfvRayosX1" runat="server" ControlToValidate="rblRayosX"
                            CssClass="textRed" Enabled="False" ErrorMessage="" ForeColor=" ">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="19%">
                            <asp:RadioButtonList ID="rblRayosX" runat="server" RepeatDirection="Horizontal" Width="112px">
                                <asp:ListItem Value="1">Normal</asp:ListItem>
                                <asp:ListItem Value="0">Anormal</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvRayosX" runat="server" ControlToValidate="rblRayosX"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="11%" colspan="4">
                            <asp:TextBox ID="txtRayosXObservaciones" runat="server" CssClass="textBox" MaxLength="300"
                                onkeypress='return (this.value.length < 300);' Width="470px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRayosXObservaciones" runat="server" ControlToValidate="txtRayosXObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">Examen Visual
                        </td>
                        <td width="19%">
                            <asp:CheckBox ID="chkMiopia" runat="server" Text="Miopía" />
                        </td>
                        <td>Valor O.D.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtMiopiaValor" runat="server" CssClass="textBox"
                            Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvMiopiaValor" runat="server" ControlToValidate="txtMiopiaValor"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td>Valor O.I.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtMiopiaValorOI" runat="server" CssClass="textBox"
                            Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvMiopiaValorOI" runat="server" ControlToValidate="txtMiopiaValorOI"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="12%">Observaciones
                        </td>
                        <td width="19%">
                            <asp:TextBox ID="txtMiopiaObservaciones" runat="server" CssClass="textBox" MaxLength="300"
                                onkeypress='return (this.value.length < 300);' TextMode="MultiLine" Width="140px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvMiopiaObservaciones" runat="server" ControlToValidate="txtMiopiaObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%"></td>
                        <td width="19%">
                            <asp:CheckBox ID="chkAstigmatismo" runat="server" Text="Astigmatismo" />
                        </td>
                        <td>Valor O.D.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtAstigmatismoValor" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvAstigmatismoValor" runat="server" ControlToValidate="txtAstigmatismoValor"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td>Valor O.I.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtAstigmatismoValorOI" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvAstigmatismoValorOI" runat="server" ControlToValidate="txtAstigmatismoValorOI"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="12%">Observaciones
                        </td>
                        <td width="19%">
                            <asp:TextBox ID="txtAstigmatismoObservaciones" runat="server" CssClass="textBox"
                                MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"
                                Width="140px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvAstigmatismoObservaciones" runat="server" ControlToValidate="txtAstigmatismoObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td width="19%">
                            <asp:CheckBox ID="chkHipermetropia" runat="server" Text="Hipermetropía" />
                        </td>
                        <td>Valor O.D.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtHipermetropiaValor" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvHipermetropiaValor" runat="server" ControlToValidate="txtHipermetropiaValor"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td>Valor O.I.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtHipermetropiaValorOI" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvHipermetropiaValorOI" runat="server" ControlToValidate="txtHipermetropiaValorOI"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="12%">Observaciones
                        </td>
                        <td width="19%">
                            <asp:TextBox ID="txtHipermetropiaObservaciones" runat="server" CssClass="textBox"
                                MaxLength="300" onkeypress='return (this.value.length < 300);' TextMode="MultiLine"
                                Width="140px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvHipermetropiaObservaciones" runat="server" ControlToValidate="txtHipermetropiaObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td width="19%">
                            <asp:CheckBox ID="chkPresbicia" runat="server" Text="Presbicia" />
                        </td>
                        <td>Valor O.D.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtPresbiciaValor" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvPresbiciaValor" runat="server" ControlToValidate="txtPresbiciaValor"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td>Valor O.I.
                        <asp:TextBox onkeypress="return currencyFormatWithNegative(this,event,true,true)"
                            onkeydown="return keyDown(this,event)" ID="txtPresbiciaValorOI" runat="server"
                            CssClass="textBox" Width="70px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvPresbiciaValorOI" runat="server" ControlToValidate="txtPresbiciaValorOI"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                        <td width="12%">Observaciones
                        </td>
                        <td width="19%">
                            <asp:TextBox ID="txtPresbiciaObservaciones" runat="server" CssClass="textBox" MaxLength="300"
                                onkeypress='return (this.value.length < 300);' TextMode="MultiLine" Width="140px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvPresbiciaObservaciones" runat="server" ControlToValidate="txtPresbiciaObservaciones"
                                CssClass="textRed" Display="Dynamic" Enabled="False" ErrorMessage="" ForeColor=" ">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td width="19%">
                            <asp:CheckBox ID="chkOtros" runat="server" Text="Otros" />
                        </td>
                        <td colspan="4">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <table id="Table12" align="center" cellpadding="3" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE102"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </div>
        </table>
        <tr>
            <td height="15">&nbsp;
            </td>
            <td height="15"></td>
        </tr>
        <tr>
            <td width="20%">Observaciones Generales
            </td>
            <td>
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textBox" Width="550px"
                    MaxLength="500" onkeypress='return (this.value.length < 500);' TextMode="MultiLine"
                    Height="40px"></asp:TextBox>
            </td>
        </tr>
        </table></FIELDSET> </td></tr>
        <!--Prototipo0-DMA-10092018-Historial laboral-->
        <tr>
            <td colspan="2" id="HistoriaLaboral">
                <table class="tableBorder" id="tbHistorialLaboral" cellspacing="0" cellpadding="3" width="100%"
                    align="center">
                    <!---Empresas-->
                    <tr>
                        <td class="headerTable" colspan="6">Historia Laboral
                                                <asp:ImageButton ID="imbHistorialLaboral" runat="server" CausesValidation="false"
                                                    ImageUrl="../../images/icoHistorial.gif"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmp1">
                                <legend>Empresa 1</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td>Giros de la empresa</td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Puesto</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGirosEmpresa1" runat="server" CssClass="textBox" Style="max-width: 200px"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAniosEmpresa1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMesesEmpresa1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPuestoEmpresa1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmp2">
                                <legend>Empresa 2</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td>Giros de la empresa</td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Puesto</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGirosEmpresa2" runat="server" CssClass="textBox" Style="max-width: 200px"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAniosEmpresa2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMesesEmpresa2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPuestoEmpresa2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmp3">
                                <legend>Empresa 3</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td>Giros de la empresa</td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Puesto</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGirosEmpresa3" runat="server" CssClass="textBox" Style="max-width: 200px"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAniosEmpresa3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMesesEmpresa3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPuestoEmpresa3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmp4">
                                <legend>Empresa 4</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td>Giros de la empresa</td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Puesto</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGirosEmpresa4" runat="server" CssClass="textBox" Style="max-width: 200px"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAniosEmpresa4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMesesEmpresa4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPuestoEmpresa4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmp5">
                                <legend>Empresa 5</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td>Giros de la empresa</td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Puesto</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGirosEmpresa5" runat="server" CssClass="textBox" Style="max-width: 200px"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAniosEmpresa5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMesesEmpresa5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPuestoEmpresa5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <!---Empresas-->
                    <!---Cuestionario-->
                    <!--fisicos-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldCue1" style="text-align: left">
                                <legend>Fisicos</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Comentario</td>
                    </tr>
                    <tr>
                        <td>Ruido</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoRuido" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoRuido" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoRuido" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoRuido" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoRuido" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoRuido" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Iluminación</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoIluminacion" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoIluminacion" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoIluminacion" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoIluminacion" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoIluminacion" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoIluminacion" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Vibraciones</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoVibraciones" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoVibraciones" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoVibraciones" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoVibraciones" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoVibraciones" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoVibraciones" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Radiaciones ionizantes y no ionizantes</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoRadiacion" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoRadiacion" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoRadiacion" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoRadiacion" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoRadiacion" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoRadiacion" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Temperaturas extremas</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoTempExtremas" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoTempExtremas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoTempExtremas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoTempExtremas" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoTempExtremas" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoTempExtremas" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoOtro1" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoOtro1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoOtro1" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoOtro1" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoOtro2" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoOtro2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoOtro2" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoOtro2" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoOtro3" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoOtro3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoOtro3" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoOtro3" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoOtro4" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoOtro4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoOtro4" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoOtro4" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabFisicoOtro5" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosFisicoOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesFisicoOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosFisicoOtro5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosFisicoOtro5" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosFisicoOtro5" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>


                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <!--fisicos-->
                    <!--Quimicos-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldCue2" style="text-align: left">
                                <legend>Químicos</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Comentario</td>
                    </tr>

                    <!--Controles-->
                    <tr>
                        <td rowspan="2">Sólidos</td>
                        <td>Polvos</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoPolvos" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoPolvos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoPolvos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoPolvos" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoPolvos" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoPolvos" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Humos</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoHumos" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoHumos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoHumos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoHumos" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoHumos" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoHumos" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Líquidos</td>
                        <td>Rocíos/Neblinas</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoRociosNeblina" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoRociosNeblina" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoRociosNeblina" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoRociosNeblina" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoRociosNeblina" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoRociosNeblina" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2">Gaseoso</td>
                        <td>Vapores</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoVapores" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoVapores" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoVapores" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoVapores" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoVapores" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoVapores" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Gases</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoGases" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoGases" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoGases" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoGases" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoGases" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoGases" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoOtro1" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoOtro1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoOtro1" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoOtro1" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoOtro2" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoOtro2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoOtro2" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoOtro2" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoOtro3" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoOtro3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoOtro3" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoOtro3" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoOtro4" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoOtro4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoOtro4" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoOtro4" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabQuimicoOtro5" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosQuimicoOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesQuimicoOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosQuimicoOtro5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosQuimicoOtro5" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosQuimicoOtro5" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>

                    <!--Controles-->

                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <!--Quimicos-->
                    <!--Biológicos-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldCue3" style="text-align: left">
                                <legend>Biológicos</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Comentario</td>
                    </tr>

                    <!--Controles-->
                    <tr>
                        <td>Bacteria</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosBacteria" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosBacteria" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosBacteria" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosBacteria" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosBacteria" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosBacteria" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Virus</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosVirus" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosVirus" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosVirus" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosVirus" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosVirus" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosVirus" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Parasites and Fungi</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosParasitos" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosParasitos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosParasitos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosParasitos" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosParasitos" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosParasitos" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosOtro1" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosOtro1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosOtro1" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosOtro1" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosOtro2" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosOtro2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosOtro2" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosOtro2" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosOtro3" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosOtro3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosOtro3" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosOtro3" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosOtro4" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosOtro4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosOtro4" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosOtro4" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabBiologicosOtro5" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosBiologicosOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesBiologicosOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosBiologicosOtro5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosBiologicosOtro5" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosBiologicosOtro5" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>

                    <!--Controles-->

                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>

                    <!--Biológicos-->
                    <!--Ergonomicos-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldCue4" style="text-align: left">
                                <legend>Ergonómicos</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Comentario</td>
                    </tr>

                    <!--Controles-->
                    <tr>
                        <td>Movimientos repetitivos</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosMovsRepetitivos" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosMovsRepetitivos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosMovsRepetitivos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosMovsRepetitivos" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosMovsRepetitivos" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosMovsRepetitivos" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Posturas forzadas</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosPosturasForzadas" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosPosturasForzadas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosPosturasForzadas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosPosturasForzadas" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosPosturasForzadas" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosPosturasForzadas" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Manejo manual de cargas</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosManejoManCajas" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosManejoManCajas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosManejoManCajas" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosManejoManCajas" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosManejoManCajas" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosManejoManCajas" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Bipedestación prologada</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosBidepestacionProlongada" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosBidepestacionProlongada" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosBidepestacionProlongada" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosBidepestacionProlongada" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosBidepestacionProlongada" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosBidepestacionProlongada" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Sedestación prolongada</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosSedestacionProlongada" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosSedestacionProlongada" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosSedestacionProlongada" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosSedestacionProlongada" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosSedestacionProlongada" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosSedestacionProlongada" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosOtro1" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosOtro1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosOtro1" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosOtro1" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosOtro2" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosOtro2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosOtro2" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosOtro2" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosOtro3" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosOtro3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosOtro3" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosOtro3" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosOtro4" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosOtro4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosOtro4" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosOtro4" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabErgonomicosOtro5" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosErgonomicosOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesErgonomicosOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosErgonomicosOtro5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosErgonomicosOtro5" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosErgonomicosOtro5" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>

                    <!--Controles-->

                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>

                    <!--Ergonomicos-->

                    <!--psicosociales-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldCue5" style="text-align: left">
                                <legend>Riesgos psicosociales</legend>&nbsp; 

            <div style="width: 90%" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Años</td>
                        <td>Meses</td>
                        <td>Comentario</td>
                    </tr>

                    <!--Controles-->
                    <tr>
                        <td>Estrés laboral</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialEstres" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialEstres" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialEstres" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialEstres" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialEstres" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialEstres" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>Burnout</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialBurnot" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialBurnot" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialBurnot" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialBurnot" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialBurnot" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialBurnot" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Mobbing laboral</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialMobbing" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialMobbing" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialMobbing" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialMobbing" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialMobbing" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialMobbing" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Trabajo por turnos</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialTrabajoxTurnos" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialTrabajoxTurnos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialTrabajoxTurnos" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialTrabajoxTurnos" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialTrabajoxTurnos" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialTrabajoxTurnos" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialOtro1" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialOtro1" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialOtro1" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialOtro1" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialOtro1" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialOtro2" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialOtro2" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialOtro2" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialOtro2" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialOtro2" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialOtro3" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialOtro3" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialOtro3" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialOtro3" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialOtro3" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialOtro4" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialOtro4" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialOtro4" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialOtro4" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialOtro4" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Otro</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkHistLabPsicosocialOtro5" Text="Si" /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabAniosPsicosocialOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabMesesPsicosocialOtro5" MaxLength="2" CssClass="textBox" onkeydown="return keyDown(this,event)" onkeypress="return SoloNumero(event, this)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHistLabComentariosPsicosocialOtro5" MaxLength="250" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv_txtHistLabComentariosPsicosocialOtro5" runat="server" CssClass="textRed rfvHistLab" ForeColor=" " Display="Dynamic"
                                ControlToValidate="txtHistLabComentariosPsicosocialOtro5" Enabled="false" ErrorMessage="">Requerido</asp:RequiredFieldValidator>

                        </td>
                    </tr>

                    <!--Controles-->

                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>

                    <!--psicosociales-->

                    <!--Checklist labortatorio-->
                    <tr>
                        <td class="textRed" colspan="6">
                            <fieldset id="fldEmpLab" style="text-align: left">
                                <legend>Laboratorios</legend>&nbsp; 

            <div style="width: 100%" align="left">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkBiometriaHematica" Text="Biometría hemática" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkAudiometria" Text="Audiometría" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkPruebasEgonometricas" Text="Pruebas ergonométricas" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkGrupoSanguineo" Text="Grupo sanguíneo " /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkEspirometria" Text="Espirometría" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkEvaluacionPsicologica" Text="Evaluación psicológica" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkQuimicaSanguinea" Text="Química sanguínea" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkElectrocardiograma" Text="Electrocardiograma" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkOtro1" Text="Otro" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCoproparasitoscopio" Text="Coproparasitoscopio" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkPruebaEsfuerzo" Text="Prueba de esfuerzo" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkOtro2" Text="Otro" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkEgo" Text="Examen General de Orina" /></td>

                        <td>
                            <asp:CheckBox runat="server" ID="chkAgudezaVisual" Text="Agudeza visual y/o campimetría" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkOtro3" Text="Otro (otros más)" /></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkExudadoFaringeo" Text="Exudado faríngeo y/o bacilos ácido-alcohol resistentes (BAAR)." /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkToxicologia" Text="Monitoreo biológico (toxicología)" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkReaccionesFebriles" Text="Reacciones febriles" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkPerfilDrogas" Text="Perfil de abuso de drogas" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkTeleTorax" Text="Tele de torax" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDesintometriaOsea" Text="Densitometría osea" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkRxColumnaLumbar" Text="Rx. Columna lumbar" /></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkEcografia" Text="Ecografía" /></td>
                    </tr>

                </table>
            </div>
                            </fieldset>
                        </td>
                    </tr>
                    <!--Checklist labortatorio-->
                    <!---Cuestionario-->
                </table>
            </td>
        </tr>
        <!--Prototipo0-DMA-10092018-Historial laboral-->
        <tr>
            <td align="center" id="impresionDiagnostica">
                <fieldset class="FieldSet" style="width: 97%">
                    <legend>
                        <img src="../../images/icoDiagnostico.gif" border="0">&nbsp;&nbsp;Impresión Diagnóstica</legend>
                    <br>
                    <asp:UpdatePanel ID="Ajaxpanel13" runat="server">
                        <ContentTemplate>
                            <table id="Table6" cellspacing="0" cellpadding="3" width="99%" align="center">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imbHistorialDiagnosticos" runat="server" ImageUrl="../../images/icoHistorial.gif"
                                            CausesValidation="false"></asp:ImageButton>
                                        &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblUltimaConsulta11" runat="server" CssClass="textSmallRed"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:WC_AdicionarDiagnosticoConsulta ID="WC_AdicionarDiagnosticoConsulta1" runat="server"></uc1:WC_AdicionarDiagnosticoConsulta>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="height: 176px" align="center" id="PlanTratamiento">
                <fieldset class="FieldSet" style="width: 97%">
                    <legend>
                        <img src="../../images/icoPlan.gif" border="0">&nbsp;&nbsp;Plan de Tratamiento</legend>
                    <br>
                    <table id="Table7" cellspacing="0" cellpadding="3" width="95%" align="center">
                        <tr>
                            <td width="148">Plan de Tratamiento<span class="textRed"><asp:Label ID="lblObligatorioPlanTratamiento"
                                runat="server">*</asp:Label></span>
                            </td>
                            <td width="80%">
                                <asp:Label ID="lblUltimaConsulta9" runat="server" CssClass="textSmallRed"></asp:Label>
                                &nbsp;<asp:Image ID="imgBorrar7" runat="server" Height="12px" ImageUrl="~/iconos/ico_borrar.gif"
                                    ToolTip="Borrar Contenido" onclick="BorrarCampo(this, 'txtPlan');" />
                                <br />
                                <asp:TextBox ID="txtPlan" runat="server" CssClass="textBox" Width="550px" MaxLength="500"
                                    onkeypress='return (this.value.length < 500);' TextMode="MultiLine" Height="40px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="rfvPlanTratamiento" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
                                        ControlToValidate="txtPlan" ErrorMessage="">Requerido</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trComentariosTranscripcion" style="display: none" runat="server">
                            <td width="148">Comentarios Transcripción
                            </td>
                            <td width="80%">
                                <asp:TextBox ID="txtComentariosTranscripcion" runat="server" CssClass="textBox" Width="550px"
                                    MaxLength="500" onkeypress='return (this.value.length < 500);' TextMode="MultiLine"
                                    Height="40px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="148">Cita de Control
                            </td>
                            <td width="80%">
                                <asp:TextBox ID="txtFechaControl" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                    href="javascript:MostrarCalendario(Form1.txtFechaControl,Form1.txtFechaControl,'dd/mm/yyyy');"
                                    name="btnFecha"><img src="../../images/icoCalendar.gif" border="0" id="imgCalendario"
                                        runat="server"></a>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <%--    <tr>
        <td align="center">
            <asp:Label ID="lblCargandoConsulta1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblCargandoConsulta" runat="server" Text="" Style="text-align: left"></asp:Label>
        </td>
    </tr>--%>
        <tr>
            <td id="trValidar" align="center">
                <asp:Button ID="btnGuardar" runat="server" OnClientClick="dobleClickSubmint();" CssClass="button"
                    Text="Siguiente »"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" CssClass="button"
                Text="Cancelar"></asp:Button>&nbsp;
            <asp:ValidationSummary ID="valSum" runat="server" Font-Names="verdana" Font-Size="12"
                ShowSummary="false" HeaderText="Para poder continuar debe llenar los campos obligatorios"
                ShowMessageBox="true" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="textRed"><i>Por favor, haz clic una sola vez en el botón "Siguiente >>"</i></span>
            </td>
        </tr>
        </TBODY></table>
    <p>
        <br>
        <uc1:WC_BuscarPrestador ID="WC_BuscarPrestador1" runat="server"></uc1:WC_BuscarPrestador>
        <br>
        <uc1:WC_BuscarDiagnostico ID="WC_BuscarDiagnostico1" runat="server"></uc1:WC_BuscarDiagnostico>
        <br>
        <uc1:WC_BuscarProveedor ID="WC_BuscarProveedor1" runat="server"></uc1:WC_BuscarProveedor>
    </p>
        <p>
            <br>
            &nbsp;
        </p>
    </form>
    <script type="text/javascript">

        function fnIniHistLab() {
 

            $.each($("#HistoriaLaboral input[type='checkbox']"), function () {
                var tr = $(this).parent().parent();
                if (!$(tr).find("input[type='checkbox']").is(":checked")) {
                    $(tr).find("input[type!='checkbox'], textarea").val("");
                    $(tr).find("input[type!='checkbox'], textarea").attr("disabled", "disabled");
                }
                
            });
        }
        $(document).ready(function () {
      
            $("#HistoriaLaboral input[type='checkbox']").click(function () {
                var tr = $(this).parent().parent();
                var validator = $(tr).find(".rfvHistLab");
                if ($(this).is(":checked")) {

                    ValidatorEnable(document.getElementById($(validator).attr("id")), true);
                    $(tr).find("input[type!='checkbox'], textarea").val("");
                    $(tr).find("input[type!='checkbox'], textarea").removeAttr("disabled");
                    $($(tr).find("input[type!='checkbox'], textarea").get(0)).focus();
                }
                else {
                    ValidatorEnable(document.getElementById($(validator).attr("id")), false);
                    $(tr).find("input[type!='checkbox'], textarea").val("");
                    $(tr).find("input[type!='checkbox'], textarea").attr("disabled", "disabled");
                }
            });

            fnIniHistLab();

        });
          //Prototipo0-DMA-14/09/2018-Historia laboral
            //function () {
            //    var radio = document.getElementsByName("rblTipoConsulta");
            //    var tipoConsulta = "";

            //    for (var j = 0; j < radio.length; j++) {
            //        if (radio[j].checked) {
            //            tipoConsulta = radio[j].value;
            //        }
            //    }

            //    //Salud ocupacional
            //    if (tipoConsulta == "18") {
            //        //Revisamos los checks que estan prendidos para hacer forzosos los comentarios
            //        $.each($("#HistoriaLaboral input[type='checkbox']"), function () {
            //            var tr = $(this).parent().parent();
            //            var validator=$(tr).find(".rfvHistLab");
            //            if ($(this).is(":checked")) {

            //                ValidatorEnable($(validator).attr("id"), true);
            //            }
            //            else {
            //                ValidatorEnable($(validator).attr("id"), false);

            //            }
            //            //if ($(tr).find("textarea[id*='Comentarios']").val() == "") {
            //            //    alert("Debe ingresar los comentarios");
            //            //    $(tr).find("textarea[id*='Comentarios']").focus();
            //            //    r = false;
            //            //    return r;
            //            //}

            //        });

            //    }

            //}

    </script>
</body>
</html>
