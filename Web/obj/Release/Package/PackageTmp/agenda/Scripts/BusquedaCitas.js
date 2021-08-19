/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console = { log: function(msg) { alert(msg); } };
}

var CitaInfo = {};
$(function() {

    try {
        console.profile("Inicializacion BusquedaCitas.js");
        console.profile("ConfigurarUI BusquedaCitas.js");
        ConfigurarUI();
        console.profileEnd();        
        configurarAcciones();
        console.profileEnd();
    } catch (e) {
        if (console && console.log) {
            console.log(e.toString());
        }
    }
});

/*Setup dialogos modales y otros elementos de interfaz*/
function ConfigurarUI() {

    /*Configurar panel seleccion rango horario*/
    $("#campoHorarioEspecificoCheck span.horarioEspecificoCheck input").change(function(ev) {
        mostrarEsconderRangoHorasSegunOpcion();
    });
    
    /*Al hacer click en link para seleccionar todos, marcar todos los checks*/
    $("#linkSeleccionarTodos").click(function(ev) {
        $(".checkSeleccion input").attr("checked", true);
        ev.preventDefault();
    });

    /*Configurar panel seleccion rango de fechas*/
    $("#campoComboFechas select.selectRangoFechas").change(function(ev) {
        var valor = $(this).val();

        if (valor == "Otro") {
            mostrarRangoFechas();
        }
        else {
            esconderRangoFechas();
        }
    });

    var now = new Date();
    $("#selectorRangoFechas input.rangoFechaInicio").datepicker({ dateFormat: ClientApp.formatoFecha, changeYear: true });
    $("#selectorRangoFechas input.rangoFechaFin").datepicker({ dateFormat: ClientApp.formatoFecha, changeYear: true });
    //Configurar inputs que reciben hora
    $("#contenedorRangoHoras input.txtHoraInicio").timeEntry({ show24Hours: false });
    $("#contenedorRangoHoras input.txtHoraFin").timeEntry({ show24Hours: false });


    /*Si es postback y la opcion "Otro" esta chequeada mostrar el rango de fachas*/
    if ($("#campoComboFechas select.selectRangoFechas").val() == "Otro") {
        mostrarRangoFechas();
    }
    /*Si es postback y la opción de rango de horas esta chequedda mostrar el rango de horas*/
    mostrarEsconderRangoHorasSegunOpcion();

}
/*Muestra el contenedor del rango de horas o lo esconde dependiendo del check*/
function mostrarEsconderRangoHorasSegunOpcion() {

    var checkeado = $("#campoHorarioEspecificoCheck span.horarioEspecificoCheck input:checked").length;
    if (checkeado > 0) {
        $("#contenedorRangoHoras").show();
    }
    else {
        $("#contenedorRangoHoras").hide();
    }
}

function mostrarRangoFechas() {
    var contenedorFechas = $("#selectorRangoFechas");
    $(contenedorFechas).show();
}

function esconderRangoFechas() {
    var contenedorFechas = $("#selectorRangoFechas");
    $(contenedorFechas).hide();
}


function configurarAcciones() {

    $("#btnCerrarDialogoRecordatorio").click(function(e) {
        $("#dialogoRegistrarRecordatorio").dialog('close');
        e.preventDefault();
    });
    
    $("#canCita").click(function(e) {
        $("#dialogoCancelarCita").dialog('open');
        $("#lblNombrePaciente").text(CitaInfo.nombrepaciente);
        $("#idCitaCancelacion").val(CitaInfo.id);

        e.preventDefault();
    });


    $("#regRecordatorio").click(function(e) {
        $("#dialogoRegistrarRecordatorio").dialog('open');
        $("#lblNombrePacienteRecordatorio").text(CitaInfo.nombrepaciente);
        $("#idCitaRecordatorio").val(CitaInfo.id);
        e.preventDefault();

    });

    $("#regLlegadaPaciente").click(function(e) {
        $("#dialogoRegistrarLlegadaPaciente").dialog('open');
        $("#lblNombrePacienteLlegada").text(CitaInfo.nombrepaciente);
        $("#idCitaLlegada").val(CitaInfo.id);

        e.preventDefault();

    });
    
    /*Configurar menu contextual grid*/
    Agenda.setupMenuGrid("#divAccionesCita", "#contenedorGridResultados table a.menuGrid", configurarCitaActual);
}

/*Funcion que sera enviada como callback al configurar el menu contextual 
se encarga de asignar los atributos para la cita seleccionada*/
function configurarCitaActual(linkSeleccionado) {

    /*Optimización, en este momento se inicializan los dialogos (operacion costosa)*/
    if (!ClientApp.dialogosConfigurados) {

        configurarDialogos();
        ClientApp.dialogosConfigurados = true;
    }
    
    var id = $(linkSeleccionado).attr("idcita");
    var dia = $(linkSeleccionado).attr("dia");
    var nombrepaciente = $(linkSeleccionado).attr("nombrepaciente");
    var idempleado = $(linkSeleccionado).attr("idempleado");
    var idbeneficiario = $(linkSeleccionado).attr("idbeneficiario");
    var estado = $(linkSeleccionado).attr("estado");
    CitaInfo.id = id;
    CitaInfo.dia = dia;
    CitaInfo.nombrepaciente = nombrepaciente;
    CitaInfo.idempleado = idempleado;
    CitaInfo.idbeneficiario = idbeneficiario;
    CitaInfo.estado = estado;
    
    var d = new Date();
    CitaInfo.linkReprogramar = "../RegistroCitas/ModuloRegistroCitas.aspx?idCita=" + CitaInfo.id + "&rand=" + d.getMilliseconds();

    /*Configurar link reprogramar*/
    $("#repCita").click(function() {
        window.location = CitaInfo.linkReprogramar;
    });


    /*Solo mostrar los links apropiados dependiendo del estado*/

    var linkRecordatorio = $("#liregRecordatorio");
    var linkLlegada = $("#liregLlegadaPaciente");
    var linkReprogramar = $("#lirepCita");
    var linkCancelar = $("#licanCita");
    var msgSinAcciones = $("#lisinAcciones");
    
    $(linkRecordatorio).hide();
    $(linkLlegada).hide();
    $(linkReprogramar).hide();
    $(linkCancelar).hide();
    $(msgSinAcciones).hide();

    if (estado == "Pendiente") {
        $(linkRecordatorio).show();
        $(linkLlegada).show();
        $(linkReprogramar).show();
        $(linkCancelar).show();
    }

    else if (estado == "Finalizada" || estado == "Inasistida" || estado == "Cancelada" || estado == "Espera") {
        $(msgSinAcciones).show();
    }
}


function configurarDialogos() {
    console.profile("ConfigurarDialogos");
    var dlg = $("#dialogoCancelarCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 300,
        width: 320,
        modal: false,
        position: 'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });
    $("#btnNoCancelar").click(function(e) {
        $(dlg).dialog("close");
        e.preventDefault();
    });
    var dlg2 = $("#dialogoRegistrarRecordatorio").dialog({
        bgiframe: true,
        resizable: false,
        height: 210,
        width: 370,
        modal: false, position: 'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });
    var dlg3 = $("#dialogoRegistrarLlegadaPaciente").dialog({
        bgiframe: true,
        resizable: false,
        height: 130,
        width: 270,
        modal: false, position: 'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });

    console.profileEnd();

}
//Necesaria para evitar que jquery saque los dialogs del form principal de webforms
function fixDialogos() {
    
    var dlg = $("#dialogoCancelarCita");
    var dlg2 = $("#dialogoRegistrarRecordatorio");
    var dlg3 = $("#dialogoRegistrarLlegadaPaciente");
    dlg.parent().appendTo(jQuery("form:first"));
    dlg2.parent().appendTo(jQuery("form:first"));
    dlg3.parent().appendTo(jQuery("form:first"));


}

function EndRequestHandler() {
    esconderLoading();
}
function mostrarLoading() {
    $("#loadingMessage").show();
}
function esconderLoading() {
    $("#loadingMessage").hide();
}

function cerrarDialogoLlegada() {
    $("#dialogoRegistrarLlegadaPaciente").dialog("close");
    return false;
}

function validarParametrosCancelacion() {
    var valido = true;
    var errorMsg = [];
    var nombreSolicita = $(".txtSolicitaCancelar").val();

    if (nombreSolicita.length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Nombre de quien solicita la cancelación requerido";
    }
    if (!valido) {
        alert(errorMsg.join("\n"));
    }
    return valido;
}

/*Valida que los parámetros seleccionados sean correctos*/
function validarParametrosBusqueda() {
   
    var errorMsg = [];
    var valido = true;

    /*Validar rango de fechas*/
    if ($(".selectRangoFechas").val() == "Otro") {

        var fechaInicio = $(".rangoFechaInicio").datepicker("getDate");
        var fechaFin = $(".rangoFechaFin").datepicker("getDate");
        if (fechaInicio == null) {
            valido = false;
            errorMsg[errorMsg.length] = "Fecha de inicio requerida si la opción es Otros";
        }
        if (fechaFin == null) {
            valido = false;
            errorMsg[errorMsg.length] = "Fecha Fin requerida si la opción es Otros";
        }
        if (fechaInicio != null && fechaFin != null && fechaInicio > fechaFin) {
            valido = false;
            errorMsg[errorMsg.length] = "Fecha de inicio no puede ser mayor que fecha final";
        }
    }

    /*Validar rango de horas*/
    var horarioEspecifico = $(".horarioEspecificoCheck input:checked").length > 0;
    if (horarioEspecifico) {

        var horaInicio = $(".txtHoraInicio").timeEntry("getTime");
        var horaFin = $(".txtHoraFin").timeEntry("getTime");


        if (horaInicio === null) {
            valido = false;
            errorMsg[errorMsg.length] = "Hora de inicio requerida";
        }
        if (horaFin === null) {
            valido = false;
            errorMsg[errorMsg.length] = "Hora de finalización requerida";
        }

        if (horaInicio > horaFin) {
            valido = false;
            errorMsg[errorMsg.length] = "Hora de finalización debe ser mayor ";
        }
    }
    if (valido) {
    
    }
    else {
        alert(errorMsg.join("\n"));
    }

    return valido;
}


/*Valida los parametros de cancelación masiva*/
function validarCancelacionMasiva() {
    var valido = true;
    var errorMsg = [];
    var nombreSolicita = $(".txtSolicitaCancelacionMasiva").val();
    var medio = $(".selectMedioCancelacionMasiva").val();
    var infoAdicional = $(".notasCancelacionMasiva").val();
    /*Verificar que existe al menos una cita chequeada*/
    if ($(".checkSeleccion input:checked").length == 0) {

        valido = false;
        errorMsg[errorMsg.length] = "No hay ninguna cita seleccionada";
    }

    if (jQuery.trim(nombreSolicita).length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Nombre de quien solicita requerido";
    }

    if (medio== null || medio == "-1") {
        valido = false;
        errorMsg[errorMsg.length] = "Medio de contacto requerido";
    }

    if (jQuery.trim(infoAdicional) == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Información adicional requerida";
    } 
    

    if (!valido) {
        alert(errorMsg.join("\n"));
    }
    return valido;
}

/*Validacion de los parametros para registro de  recordatorios*/
function validarParametrosRecordatorio() {
    var valido = true;
    var errorMsg = [];
    var medio = $(".selectMedioRecordatorio").val();

    if (medio == "-1") {
        valido = false;
        errorMsg[errorMsg.length] = "Medio de contacto requerido";
    }
    if (!valido) {
        alert(errorMsg.join("\n"));
    }
    return valido;
}
