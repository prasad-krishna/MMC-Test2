/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console = { log: function() { } };
}

var ClientApp = ClientApp || {};
//Flag que indica si se debe hacer pooling al servidor buscando las últimas alertas (se modifica desde el servidor)
ClientApp.AlertasActivas = true;

var CitaInfo = {};
$(function() {
    // $("#tabs").tabs();

    try {
        console.profile("Inicialización AgendaMedico.js");
        $("#regConsulta").click(function(e) {
            //Solo redireccionar si la cita esta en estado pendiente o en espera
            if (CitaInfo.estado == "Pendiente" || CitaInfo.estado == "Espera") {
                window.location = "../../interfaz_empleado/forma/AE_registroconsulta.aspx?employee_id=" + CitaInfo.idempleado + "&beneficiario_id=" + CitaInfo.idbeneficiario + "&cita_id=" + CitaInfo.id;
            }
            else {
                alert("Solo se permite registrar consulta para citas pendientes o en espera");
            }

            e.preventDefault();
        });
        configurarDialogos();
        configurarAlertas();
        /*Configurar menu contextual grid*/
        /*El método acepta una funcion que se ejecutará justo antes de mostrar el menú, la usamos para esconder los ítems que no aplican*/
        Agenda.setupMenuGrid("#divAccionesCita", ".linkMenuCitas", configurarCitaActual);
        console.profileEnd();

    } catch (e) {
        if (console && console.log) {
            console.log(e.toString());
            throw e;
        }
    }
});

/*Funcion que sera enviada como callback al configurar el menu contextual 
se encarga de asignar los atributos para la cita seleccionada*/
function configurarCitaActual(linkSeleccionado) {
    var id = $(linkSeleccionado).attr("idcita");
    var dia = $(linkSeleccionado).attr("dia");
    var nombrepaciente = $(linkSeleccionado).attr("nombrepaciente");
    var idempleado = $(linkSeleccionado).attr("idempleado");
    var idbeneficiario = $(linkSeleccionado).attr("idbeneficiario");
    var estadoCita = $(linkSeleccionado).attr("estado");
    CitaInfo.id = id;
    CitaInfo.dia = dia;
    CitaInfo.nombrepaciente = nombrepaciente;
    CitaInfo.idempleado = idempleado;
    CitaInfo.idbeneficiario = idbeneficiario;
    CitaInfo.estado = estadoCita;
    
    /*Determinar que items de menu mostrar*/
    var mostrarRegistrarConsulta = (CitaInfo.estado == "Pendiente" || CitaInfo.estado == "Espera");
    if (mostrarRegistrarConsulta) {
        $("#liRegistrar").show();
        $("#sinAcciones").hide();
    }
    else {
        $("#liRegistrar").hide();
        $("#sinAcciones").show();
    }
}


function configurarDialogos() {
    var dlg = $("#dialogoCancelarCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 460,
        width: 320,
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
    var dlg2 = $("#dialogoRegistrarRecordatorio").dialog({
        bgiframe: true,
        resizable: false,
        height: 410,
        width: 320,
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
        height: 410,
        width: 320,
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


}
//Necesaria para evitar que jquery saque los dialogs del form principal de webforms
function fixDialogos() {
    var dlg = $("#dialogoCancelarCita").dialog();
    var dlg2 = $("#dialogoRegistrarRecordatorio").dialog();
    var dlg3 = $("#dialogoRegistrarLlegadaPaciente").dialog();
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

/*Handler del evento de selección de cita  */
function schedulerCambioCitaSeleccionada(instancia, sender, args) {

    var ids = scheduler.GetSelectedAppointmentIds();
    var app = scheduler.GetAppointmentById(ids[0]);

}

function DefaultAppointmentMenuHandler(RaiseCallback, s, args) {
    if (args.item.GetItemCount() <= 0) {
        //alert(args.item.name);
        //ASPxClientScheduler1.ShowAppointmentFormByClientId(1);
        switch (args.item.name) {
            case "RegistrarConsulta":
                var ids = scheduler.GetSelectedAppointmentIds();
                var app = scheduler.GetAppointmentById(ids[0]);
                /*Solo permitir registrar consultas para citas pendientes o en espera*/
                if (app.statusIndex == 1 || app.statusIndex == 5) {
      
                    scheduler.RaiseCallback('USRAPTMENU|RegistrarConsulta');
                }
                else {
                    alert("Solo se permite registrar consulta para citas pendientes o en espera");
                }

                break;
            default:
                break;
        }
    }
}

/* Cache de alertas, utilizado para no mostrar alertas ya existentes */
var _cacheAlertas = [];
/*Configura las alertas de citas para el médico*/
function configurarAlertas() {
    //$.jGrowl.defaults.position = "top-right";
    $("#linkCerrarAlertas").click(function(e) {
        cerrarAlertas();
        e.preventDefault();
    });


    setInterval(function() {
        /*Hacer un request de ajax si las alertas estan activadas*/
        if (ClientApp.AlertasActivas) {
            var noCache = new Date().getTime();
            $.getJSON("ServiciosAgendaCliente.aspx?action=getAlertas", { "noCache": noCache }, function(data) {
                var alertas = data;
                for (var i = 0; i < alertas.length; i++) {
                    var alerta = alertas[i];
                    if (_cacheAlertas[alerta.Id] === undefined) {
                        /*No se encontró la alerta en el cache, asi que la mostramos  la agregamos*/
                        _cacheAlertas[alerta.Id] = alerta;
                        mostrarAlerta(alerta);
                    }
                }
            });
        }
    }, 8000);
}

function mostrarAlerta(alerta) {
    var elemento = $.achtung({ message: alerta.Description, timeout: 0 });
    $(elemento).append("<a href='#' onclick='cerrarAlertas();return false;'>Cerrar alertas</a>");
}

function cerrarAlertas() {
    $('.achtung').achtung('close');
}

function desactivarAlertas() {

    ClientApp.AlertasActivas = false;
}
