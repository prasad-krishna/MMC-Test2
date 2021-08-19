//ControlSesion.js
//Objetivo: Librería que utiliza jQuery para:
//          - Controlar el tiempo restante de la sesión
//          - Visualizar un mensaje de alerta HTML previo a la expiración de la sesión
//          - Gatillar el evento Click de un control de la página previo a la expiración de la sesión
//Autor: Emilio Bueno C.
//Fecha: 20-Nov-2012

$(document).ready(function() {
    //Sección que define la caja del mensaje y su contenido en HTML
    //que será insertado dentro del DIV divCodigoCS

    //Capa de fondo para bloqueo de controles de la forma
    var varHTML = "" +
    "<div id='divCapaBloqueo' style='display: none; background-color: #C0C0C0; position: absolute; width: 100%; height: 100%; top: 0px; left: 0px; z-index: 9998;'>" +
    "</div>";

    //Capa contenedora del mensaje
    var varHTML2 = "" +
    "<div id='divMsgSesion' style='display: none; background-color: White; width: 340px; height: 160px; font-family: Verdana; font-size: 12px;" +
    "    position: absolute; top: 50px;" +
    "    left: 50%; margin-left: -170px; margin-top: 0px; z-index: 9999;" +
    "     -moz-box-shadow: 3px 3px 4px #111;" +
    "    -webkit-box-shadow: 3px 3px 4px #111; box-shadow: 3px 3px 4px #111; -ms-filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=#111111);" +
    "    filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=#111111);'>" +
    "    <table class='tableBorder' border='0' cellspacing='0' cellpadding='0' style='width: 340px; height: 160px;'>" +
    "        <tr style='background-color: #6495ED;'>" +
    "            <td class='titleBackBlue' colspan='3' align='center'>Alerta</td>" +
    "        </tr>" +
    "        <tr>" +
    "            <td width='30px'>" +
    "            </td>" +
    "            <td width='340px'>" +
    "                <br />" +
    "                Estimado usuario:<br />" +
    "                <br />" +
    "                Su sesión expirará en menos de tres minutos.<br />" +
    "                <br />" +
    "                Por seguridad su consulta se guardará automáticamente con la información que ha ingresado hasta el momento. Por favor no olvide completar el registro de la consulta.<br />" +
    "                <br />" +
    "                <center>" +
    "                <Button id='btnMsgClose' text='Aceptar' class='button' OnClientClick='return false;'>Aceptar</Button>" +
    "                    </center>" +
    "                <br />" +
    "            </td>" +
    "            <td width='30px'>" +
    "            </td>" +
    "        </tr>" +
    "        <tr>" +
    "            <td colspan='3' align='center'>" +
    "                <span id='monitor' style='display: none;'></span>" +
    "                <br />" +
    "            </td>" +
    "        </tr>" +
    "    </table>" +
    "</div>"

    //Se cargan los HTML en la capa definida en la forma DIV divCodigoCS 
    $('#divCodigoCS').append(varHTML);
    $('#divCodigoCS').append(varHTML2);

    //Se recuperan valores de sesión precargados en el load de la página
    var campooculto = $('#hdnTimeout').val();
    var sesion = $('#hdnSesion').val();
    //Definición de los eventos de la API countdown de jQuery
    var valor = parseInt(sesion) * 60;
    if (campooculto != "") {
        var shortly = new Date();
        shortly.setSeconds(shortly.getSeconds() + valor);
        $('#shortly').countdown('change', { until: shortly });

        $('#shortly').countdown({ until: shortly,
            onExpiry: liftOff, onTick: watchCountdown
        });

        //Función auxiliar para el evento Click del botón btnMsgClose del mensaje de alerta
        //Se ejecuta en el evento Click del botón
        $('#btnMsgClose').click(function() {
            $('#shortly').countdown('destroy');
            $('#divMsgSesion').css("display", "none");
            $('#divCapaBloqueo').css("display", "none");
            var botonclick = $('#hdnBotonClick').val();
            document.getElementById(botonclick).click();
            return false;
        });
    }
});

//Función que se ejecuta en el evento onExpiry de la API
function liftOff() {
    alert("¡Su sesión ha expirado!");
    //top.location.href = "Login.aspx"; //redirection
}

//Función que se ejecuta en el evento onTick de la API
function watchCountdown(periods) {
    //Se recuperan valores desde el Web.Config
    var tiempomostraralerta = $('#hdnTiempoMostrarAlerta').val()
    var tiempoguardartemporal = $('#hdnTiempoGuardarTemporal').val()

    //Actualiza el span monitor en cada Tick (cada 1 segundo)
    $('#monitor').text('Tiempo restante de sesión: ' + periods[5] + ' minuto(s) y ' +
               periods[6] + ' segundos');
    //Se evalúa el período para hacer visible el mensaje
    if (periods[5] < tiempomostraralerta) {
        $('#divCapaBloqueo').css("display", "block");
        $('#divCapaBloqueo').animate({ opacity: 0.3 }, 500);
        $('#divMsgSesion').css("display", "block");
        $('#divMsgSesion').css("top", ($(top).scrollTop() + 50));
    }
    //Se evalúa el período final para la activación automática del evento Click del botón o link que se activará
    if (periods[5] < tiempoguardartemporal) {
        $('#shortly').countdown('destroy');
        $('#divMsgSesion').css("display", "none");
        $('#divCapaBloqueo').css("display", "none");
        var botonclick = $('#hdnBotonClick').val();
        document.getElementById(botonclick).click();
        return false;
    }
    
}
