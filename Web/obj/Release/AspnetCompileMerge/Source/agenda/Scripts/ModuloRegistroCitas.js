/*Logica de cliente para el modulo de configuracion del
horario de un medico*/
var CitaInfo = {};
var CitaReprog = {};
$(function() {
    console.profile("Inicializacion ModuloRegistroCitas.js");
    ConfigurarUI();
    ConfigurarAcciones();
    console.profileEnd();

});

function configurarDialogos() {
    var dlg = $("#dialogoConfirmarRegistroCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 300,
        width: 300,
        modal: false,
        position:'top',
        autoOpen: false,
        position:'top',
        open: function(event, ui) {
        fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });
    var dlg = $("#dialogoConfirmarReprogramacionCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 350,
        width: 325,
        modal: false,
        position:'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });

}
//Necesaria para evitar que jquery saque los dialogs del form principal de webforms
function fixDialogos() {
    var dlg = $("#dialogoConfirmarRegistroCita");
    var dlg2 = $("#dialogoConfirmarReprogramacionCita");
    dlg.parent().appendTo(jQuery("form:first"));
    dlg2.parent().appendTo(jQuery("form:first"));

}

/*Setup dialogos modales y otros elementos de interfaz*/
function ConfigurarUI() {

    /*Configurar panel seleccion rango horario*/
    $("#campoCheckHorarioEspecifico .horarioEspecificoCheck input").change(function(ev) {
        mostrarEsconderRangoHorasSegunOpcion();
    });

    /*Configurar panel seleccion rango de fechas*/
    $("#campoSelectRangoFechas select.selectRangoFechas").change(function(ev) {
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

    /*Poner los teléfonos de la última cita*/
    var ultimosTelefonos = $("#contenedorUltimoTelefono .ultimoTelefono").val();
    $("#telefonosRegistro").val(ultimosTelefonos);
    $("#telefonosRegistroReprog").val(ultimosTelefonos);


    /*Si es postback y la opcion "Otro" esta chequeada mostrar el rango de fachas*/
    if ($("#campoSelectRangoFechas select.selectRangoFechas").val() == "Otro") {
        mostrarRangoFechas();
    }
    /*Si es postback y la opción de rango de horas esta chequedda mostrar el rango de horas*/
    mostrarEsconderRangoHorasSegunOpcion();

    /*Intentar poner el scroll en el top*/
    $(window).scrollTop(0);
    $(document).scrollTop(0);

}
/*Muestra el contenedor del rango de horas o lo esconde dependiendo del check*/
function mostrarEsconderRangoHorasSegunOpcion() {

    var checkeado = $("#campoCheckHorarioEspecifico .horarioEspecificoCheck input:checked").length;
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

/*Setup links que generan alguna accion*/
function ConfigurarAcciones() {

    /*Actualizar informacion de cita y mostrar el dialogo de registro*/
    $("table.grid a.seleccionDisponibilidad").click(function(e) {
        e.preventDefault();

        /*Inicializar dialogos en caso de que no esten ya inicializados*/
        if (!ClientApp.dialogosRegistroInicializados) {
            ClientApp.dialogosRegistroInicializados = true;
            configurarDialogos();
        }
        CitaInfo.idmedico = $(this).attr("idmedico");
        CitaInfo.nombremedico = $(this).attr("nombremedico");
        CitaInfo.idsede = $(this).attr("idsede");
        CitaInfo.idtipocita = $(this).attr("idtipocita");
        CitaInfo.tipocita = $(this).attr("tipocita");
        CitaInfo.nombresede = $(this).attr("nombresede");
        CitaInfo.fecha = $(this).attr("fecha");
        CitaInfo.horainicio = $(this).attr("horainicio");
        CitaInfo.horafin = $(this).attr("horafin");
        CitaInfo.horainicioformato = $(this).attr("horaInicioFormato");
        CitaInfo.horafinformato = $(this).attr("horafinformato");
        //mostrarMenuContextual()

        //llenar etiquetas informativas en el dialogo de registro
        $("#lblFecha").text(CitaInfo.fecha);
        $("#lblHora").text(CitaInfo.horainicioformato);
        $("#lblHoraFinal").text(CitaInfo.horafinformato);
        $("#lblMedico").text(CitaInfo.nombremedico);
        $("#lblSede").text(CitaInfo.nombresede);
        $("#lblTipoCita").text(CitaInfo.tipocita);

        /*Configurar inputs escondidos con los valores utilizados para registrar la cita*/
        $("#idMedico").val(CitaInfo.idmedico);
        $("#idSede").val(CitaInfo.idsede);
        $("#idTipoCita").val(CitaInfo.idtipocita);
        $("#fecha").val(CitaInfo.fecha);
        $("#horaInicial").val(CitaInfo.horainicio);
        $("#horaFinal").val(CitaInfo.horafin);
        $("#nombreMedico").val(CitaInfo.nombremedico);
        $("#nombreSede").val(CitaInfo.nombresede);
        $("#dialogoConfirmarRegistroCita").dialog('open');

    });

    $("#dialogoConfirmarRegistroCita input.BtnCancelarRegistro").click(function(e) {
        $("#dialogoConfirmarRegistroCita").dialog('close');
        e.preventDefault();
    });

    /*Configuración click al reprogramar cita*/
    $("table.grid .linkReprogramar").click(function(e) {

        /*Inicializar dialogos en caso de que no esten ya inicializados*/
        if (!ClientApp.dialogosRegistroInicializados) {
            ClientApp.dialogosRegistroInicializados = true;
            configurarDialogos();
        }
        //Mostrar dialogo       
        CitaReprog.idmedico = $(this).attr("idmedicoReprog");
        CitaReprog.nombremedico = $(this).attr("nombreMedicoReprog");
        CitaReprog.idsede = $(this).attr("idSedeReprog");
        CitaReprog.nombresede = $(this).attr("nombreSedeReprog");
        CitaReprog.fecha = $(this).attr("fechaReprog");
        CitaReprog.fechaOriginalReprog = $(this).attr("fechaOriginalReprog");

        CitaReprog.horainicio = $(this).attr("horaInicialReprog");
        CitaReprog.horafin = $(this).attr("horaFinalReprog");
        CitaReprog.horainicioformato = $(this).attr("horaInicialFormatoReprog");
        CitaReprog.horafinformato = $(this).attr("horaFinalFormatoReprog");
        //Cuadrar las etiquetas
        $("#dialogoConfirmarReprogramacionCita").dialog("open");
        
        $("#fechaOriginal").text(CitaReprog.fechaOriginalReprog);
        $("#nuevaFecha").text(CitaReprog.fecha + "(" + CitaReprog.horainicioformato + ")");
        $("#nuevoMedico").text(CitaReprog.nombremedico);
        $("#nuevaSede").text(CitaReprog.nombresede);

        //Cuadrar los input hidden que serán enviados al servidor
        $("#horaInicialReprog").val(CitaReprog.horainicio);
        $("#horaFinalReprog").val(CitaReprog.horafin);
        $("#fechaReprog").val(CitaReprog.fecha);
        $("#idSedeReprog").val(CitaReprog.idsede);

        e.preventDefault();
    });

    $("#cancelarReprogramacion").click(function(e) {
        $("#dialogoConfirmarReprogramacionCita").dialog("close");
        e.preventDefault();
    });
}

/*
Valida que el intervalo que se vaya a agregar sea correcto
*/
function validarParametros() {
    
    var errorMsg = [];
    var valido = true;
    var duracion = $("#containerFiltro .duracionCita").val();
    /*Validar tipo de cita y duracion*/
    if ($("#containerFiltro .listaTiposCita").val() == "-1") {
        valido = false;
        errorMsg[errorMsg.length] = "Tipo de Cita requerido";
    }  
     if (Agenda.isNumeric(duracion) == false) {
        valido = false;
        errorMsg[errorMsg.length] = "Duración requerida";
    }

    if (Agenda.isNumeric(duracion) == true && duracion <=0 ) {
        valido = false;
        errorMsg[errorMsg.length] = "Duración debe ser mayor o igual a cero";
    }

    /*Validar rango de fechas*/
    if ($("#campoSelectRangoFechas select.selectRangoFechas").val() == "Otro") {

        var fechaInicio = $("#selectorRangoFechas input.rangoFechaInicio").datepicker("getDate");
        var fechaFin = $("#selectorRangoFechas input.rangoFechaFin").datepicker("getDate");
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
    var horarioEspecifico = $("#campoCheckHorarioEspecifico .horarioEspecificoCheck input:checked").length > 0;
    if ( horarioEspecifico ) {

        var horaInicio = $("#contenedorRangoHoras input.txtHoraInicio").timeEntry("getTime");
        var horaFin = $("#contenedorRangoHoras input.txtHoraFin").timeEntry("getTime");
        
        
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

/*Validacion cuando se programa cita*/
function validarParametrosRegistro() {
   // fixDialogos();
    var errorMsg = [];
    var valido = true;
    if ($("#telefonosRegistro").val().length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Teléfono(s) de contacto requerido";
    }

    if (errorMsg.length > 0)
        alert(errorMsg.join("\n"));
    return valido;
}


/*Validacion cuando se reprograma*/
function validarParametrosReprog() {
   // fixDialogos();
    var errorMsg = [];
    var valido = true;
    if ($("#nombreSolicitaReprog").val().length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Nombre de quien solicita el cambio requerido";
    }
    if ($("#telefonosRegistroReprog").val().length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Teléfono(s) de contacto requerido";
    }

    if ($("#contenedorSelectMedios .selectMedioReprog").val() == "-1") {
        valido = false;
        errorMsg[errorMsg.length] = "Medio de contacto requerido";
    }
    if(errorMsg.length>0)
        alert(errorMsg.join("\n"));
    return valido;
}
