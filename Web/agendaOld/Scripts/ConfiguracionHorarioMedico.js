/*Logica de cliente para el modulo de configuracion del
horario de un medico*/
var ClientApp = ClientApp || {};
$(function() {
    console.profile("Inicializacion ConfiguracionHorarioMedico.js");
    ConfigurarUI();
    ConfigurarAcciones();
    console.profileEnd();

});
/*Setup dialogos modales y otros elementos de interfaz*/
function ConfigurarUI() {
    /*Configurar datepickers */
    $("#fechaVigenciaDesde").datepicker({ dateFormat: ClientApp.formatoFecha, changeYear: true });
    $("#fechaVigenciaLimite").datepicker({ dateFormat: ClientApp.formatoFecha, changeYear: true });

    /*Esconder o mostrar panel de rango de horas dependiendo del radio button seleccionado*/
    $("#panelVigenciaHorario input[name='vigenciaLimite']").change(function(e) {
        var valor = $(this).val();
        if (valor == "1") {
            //Sin limite de vigencia
            $("#panelVigencia").hide();
        }
        else {
            $("#panelVigencia").show();
        }
    });
    
    var dlgForm = $("#dialogoAgregarIntervalo").dialog({
        bgiframe: true,
        resizable: false,
        height: 370,
        width: 430,
        modal: false, position: 'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        },
        buttons: {}
    });
    
    //Configurar inputs que reciben hora
    $("#txtHoraInicio").timeEntry({ show24Hours: false });
    $("#txtHoraFin").timeEntry({ show24Hours: false });

    /*Configuracion de dialogo de eliminacion de intervalo*/
    var dlgEliminar = $("#dialogoEliminarIntervalo").dialog({
        bgiframe: true,
        resizable: false,
        height: 200,
        width: 300,
        modal: false, position: 'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        },
        buttons: {}
    });



    /*Configurar datepicker*/
    $("#cambioFecha").datepicker(
        { 
            onSelect: function(dateText, inst) {
                var fecha = $(this).datepicker("getDate");
                $("#fechaActual").val($.datepicker.formatDate(ClientApp.formatoFecha, fecha));
                fixDialogos();
                hacerPostback();
            }
            ,changeYear:true,
            dateFormat: ClientApp.formatoFecha 
        }
    );    
}

function fixDialogos() {
    //Corregir ubicacion del dialogo para que
    //Controles de asp.net puedan hacer postback
    $("#dialogoAgregarIntervalo").parent().appendTo($("form:first"));
    $("#dialogoEliminarIntervalo").parent().appendTo($("form:first"));
}
/*Setup links que generan alguna accion*/
function ConfigurarAcciones() {

    $("#selectorFecha").click(function(e) {
        $("#contenedorSeleccionFecha").toggle();
        e.preventDefault();
    });
    
    $("#linkAgregarIntervalo").click(function(e) {
        //Limpiar los campos
        //Por defecto asignar rango 8:00 am a 6:00 pm
        var start = new Date();
        var end = new Date();
        start.setHours(8, 0, 0, 0);
        end.setHours(18, 0, 0, 0);
        $("#txtHoraInicio").timeEntry("setTime", start);
        $("#txtHoraFin").timeEntry("setTime", end);
        $("#dialogoAgregarIntervalo").dialog("open");
        $("#linkCancelarCreacionIntervalo").dialog("close");
        e.preventDefault();
    });
    /*Configurar links eliminar horario*/
    $("#tblHorarioMedico a.btnEliminarIntervalo").click(function(e) {
        /*Obtener los atributos con datos del link*/
        var idIntervalo = $(this).attr("data-idIntervalo");
        /*Obtener fecha en formato ISO 8601*/
        var strFecha = $(this).attr("data-fecha");
        var valorFecha = $.datepicker.parseDate("yy-mm-dd", strFecha);
        /*Establecer los parametros del dialogo que seran enviados en el request*/
        $("#labelFechaEliminacion").text($.datepicker.formatDate("dd/mm/yy", valorFecha));
        $("#fechaEliminacion").val(strFecha);
        $("#idIntervaloToDelete").val(idIntervalo);
        $("#dialogoEliminarIntervalo").dialog("open", null, null, 'top');
        e.preventDefault();
    });

    $("#btnCancelarEliminacion").click(function(e) {
        cancelarEliminacion();
        e.preventDefault();
    });

    $("#linkCancelarCreacionIntervalo").click(function(e) {
        $("#dialogoAgregarIntervalo").dialog("close");
        e.preventDefault();
    });
}

/*
Valida que el intervalo que se vaya a agregar sea correcto
*/
function ValidarIntervalo() {
    var horaInicio = $("#txtHoraInicio").timeEntry("getTime");
    var horaFin = $("#txtHoraFin").timeEntry("getTime");
    var fechaVigenciaDesde = $("#fechaVigenciaDesde").datepicker("getDate");
    var vigenciaLimite = $("#radioVigenciaConLimite:checked").length;
    var sede = $(".listaSedes").val();
    var diasSeleccionados = $(".listaChecksDias input:checked").length;
    
    var valido = true;
    var errorMsg = [];
    if (diasSeleccionados == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Debe seleccionar por lo menos un día de la semana.";

    }
    if (fechaVigenciaDesde == null) {
        valido = false;
        errorMsg[errorMsg.length] = "Fecha de vigencia inicial requerida";
    }
    if (sede == null) {
        valido = false;
        errorMsg[errorMsg.length] = "Sede requerida";
    }
    if (vigenciaLimite > 0) {
        //Vigencia con limite
        var fechaVigenciaLimite = $("#fechaVigenciaLimite").datepicker("getDate");
        if (fechaVigenciaLimite == null) {
            valido = false;
            errorMsg[errorMsg.length] = "Si marcó la opción de limitar la vigencia, debe seleccionar la fecha limite";
        }

        if (fechaVigenciaDesde >= fechaVigenciaLimite) {
            valido = false;
            errorMsg[errorMsg.length] = "Fecha de vigencia límite debe ser mayor al inicio de la vigencia";
        }
    }
    if (horaInicio === null) {
        valido = false;
        errorMsg[errorMsg.length] = "Hora de inicio requerida";
    }
    if (horaFin === null) {
        valido = false;
        errorMsg[errorMsg.length] = "Hora de finalización requerida";
    }

    if (horaInicio >= horaFin) {
        valido = false;
        errorMsg[errorMsg.length] = "Hora de finalización incorrecta, debe ser mayor a la hora de inicio";
    }
    if (!valido) {
        alert(errorMsg.join("\n"));
    }
    else {
        ActualizarVariableHoras();
    }
    return valido;
}

/* Coloca la hora en formato hora-minutos en los hidden inputs
'horaInicio' y 'horaFin'
*/
function ActualizarVariableHoras() {
    var horaInicio = $("#txtHoraInicio").timeEntry("getTime");
    var horaFin = $("#txtHoraFin").timeEntry("getTime");
    $("#horaInicio").val(horaInicio.getHours() + "-" + horaInicio.getMinutes());
    $("#horaFin").val(horaFin.getHours() + "-" + horaFin.getMinutes());
}

/*Esconde el dialogo de eliminacion de intervalo*/
function cancelarEliminacion() {
    $("#dialogoEliminarIntervalo").dialog("close");
}