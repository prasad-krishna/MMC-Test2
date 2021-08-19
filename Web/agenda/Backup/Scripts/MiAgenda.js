/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console ={ log: function() { } };
}

var ClientApp = ClientApp || {};
ClientApp.AlertasActivas = true;
$(function() {

    try {
        /*Configuracion de grid de seleccion de medicos*/
        //Inicializar grid
        jQuery("gridSeleccionMedicos").jqGrid({
            datatype: getMedicos,
            mtype: 'POST',
            colNames: ['Id', 'Nombre', 'Especialidad'],
            colModel: [
                  { name: 'Id', index: 'id', width: 10, align: 'left', hidden: true },
                  { name: 'Nombre', index: 'Nombre', width: 150, align: 'left' },
                  { name: 'Especialidad', index: 'Especialidad', hidden: true, width: 150, align: 'left' }
                ],
            pager: jQuery("pagerMedicos"),
            rowNum: 20,
            rowList: [2, 10, 20],
            sortname: 'Nombre',
            sortorder: 'asc',
            prmNames: { page: "startPage", rows: "pageLength", sort: "sortBy", order: "sortDir", search: "_search", nd: "nd" },
            viewrecords: true,
            imgpath: '../content/images',
            caption: 'Médicos',
            height: 400,
            width: 400,
            multiSelect: true,
            onSelectRow: function(id) {

            }
        });
        $("#linkCambiarMedico").click(function() {
            $("#dialogoSeleccionMedico").dialog('open');
        });
        configurarDialogos();
        configurarPrototipo();
        
        console.log("inicializacion ok");


    } catch (e) {
        if (console && console.log) {
            console.log(e.toString());
        }
    }


});

function configurarDialogos() {
    $("#dialogoSeleccionMedico").dialog({
        bgiframe: true,
        resizable: false,
        height: 400,
        width:480,
        modal: false,position:'top',
        autoOpen: false,
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        },
        buttons: {
            'Aceptar': function() {
                $(this).dialog('close');
            },
            'Cancelar': function() {
                $(this).dialog('close');
            }
        }
    });
    $("#appointmentForm").dialog({
        bgiframe: true,
        resizable: false,
        height: 320,
        width:300,
        modal: false,position:'top',
        autoOpen: false,
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        },
        buttons: {
            'Aceptar': function() {
                $(this).dialog('close');
            },
            'Cancelar': function() {
                $(this).dialog('close');
            }
        }
    }); 
    $("#dialogoNavigator").dialog({
        bgiframe: true,
        resizable: false,
        height: 275,
        width:290,
        modal: false,position:'top',
        autoOpen: false,
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }

    });
    $("#linkMostrarNavigator").click(function(ev) {
        $("#dialogoNavigator").dialog("open");
        ev.preventDefault();
    });
}

function getMedicos(postData) {
    //No hacer hada si aun no ha sido seleccionada ninguna iteracion
    jQuery("gridSeleccionMedicos")[0].addJSONData("Id", { page: 1, total: 1, records: 1,
        rows: [{ id: 1, cell: [1,"nombre","sede"] }
                                                        ]
    });
}
function DefaultAppointmentMenuHandler(RaiseCallback, s, args) {
    if (args.item.GetItemCount() <= 0) {
        //alert(args.item.name);
        //ASPxClientScheduler1.ShowAppointmentFormByClientId(1);
        switch (args.item.name) {
            case "Abrir":
                var ids = scheduler.GetSelectedAppointmentIds();
                if (ids != null && ids.length == 0) {
                    return;
                }
                mostrarLoading();
                scheduler.GetAppointmentProperties(ids[0], 'Id;Subject;Location;Start;End;Description;ContactInfo', mostrarDetallesCita);
                break;
            case "RegistrarCita":
                $("#appointmentForm").dialog("open");
                break;
            case "RegistrarCitaUrgencia":
                $("#appointmentForm").dialog("open");
                break;
            case "Cancelar":
                var ids = scheduler.GetSelectedAppointmentIds();
                var app = scheduler.GetAppointmentById(ids[0]);
                scheduler.DeleteAppointment(app);
                break;
            case "EnEspera":
                var ids = scheduler.GetSelectedAppointmentIds();
                var app = scheduler.GetAppointmentById(ids[0]);
                app.SetLabelId(5);
                scheduler.UpdateAppointment(app);
                break;
            case "Completada":
                var ids = scheduler.GetSelectedAppointmentIds();
                var app = scheduler.GetAppointmentById(ids[0]);
                app.SetLabelId(3);
                scheduler.UpdateAppointment(app);
                break;
            default:
                //sdf
        }
    }
}

/*Carga la cita del servidor y muestra el formulario de edicion de la cita*/
function cargarCitaYMostrarDetalles(idCita) {
    //scheduler.GetAppointmentProperties(idCita, 'Id;Subject;Location;Start;End;Description;ContactInfo', mostrarDetallesCita);
    $("#appointmentForm #nombrePaciente").val("Juan Camilo Zapata");
    $("#appointmentForm #duracion").val("20");
    $("#appointmentForm #notasAdicionales").val("Notas");
    $("#appointmentForm").dialog("open");
}

function mostrarDetallesCita(values) {
    $("#appointmentForm #nombrePaciente").val(values[1]);
    var app = scheduler.GetAppointmentById(values[0]);
    $("#appointmentForm #duracion").val(app.GetDuration() / 60000);
    $("#appointmentForm #notasAdicionales").val(values[5]);
    $("#appointmentForm").dialog("open");
    esconderLoading();
}
function mostrarLoading() {
}
function esconderLoading() {
}
function mostrarDialogoUrgencia() {
    $("#appointmentForm #textFechaCita").val(new Date());
    $("#appointmentForm #duracion").val("30");
    $("#appointmentForm").dialog("open");
    
}


function configurarPrototipo() {

    
    
    //Llenar la tabla con todos los medicos iniciales
    llenarTablaMedicos(medicos);
    //Simular busqueda de usuarios
    $("#linkBusquedaRapida").click(function(ev) {
        ev.preventDefault();
        filtrarMedicos();
    });
    //Detectar onkeydown, y sobrescribir comportamiento cuando hace enter
    $("#txtBusquedaMedico").keydown(function(ev) {
        if (ev.keyCode == '13') {
            ev.preventDefault();
            filtrarMedicos();
        }
    });
}

function filtrarMedicos() {
    var texto = $("#txtBusquedaMedico").val();
    //Simular el filtrado con la lista de medicos.
    var medicosFiltrados = [];
    for (var i = 0; i < medicos.length; i++) {
        if (medicos[i].toUpperCase().lastIndexOf(texto.toUpperCase()) > 0) {
            medicosFiltrados.push(medicos[i]);
        }
    }
    llenarTablaMedicos(medicosFiltrados);
}

function llenarTablaMedicos(arrMedicos) {
    var html = [];
     var grid = $("#prototipoListaMedicos tbody");
     $(grid).html("");   
     for (var i = 0; i < arrMedicos.length; i++) {
       
        html[html.length] = "<tr>";
        html[html.length] = "<td style='width:80%'>";
        html[html.length] = arrMedicos[i];
        html[html.length] = "</td>";
        html[html.length] = "<td>";
        html[html.length] = "Especialidad" + i;
        html[html.length] = "</td>";
        html[html.length] = "</tr>";
    }
    $(grid).append(html.join(" "));
        //Configurar estilo de grid.
    $("#prototipoListaMedicos tr:even").css("background-color", "#F4F4F8");
    $("#prototipoListaMedicos tr:odd").css("background-color", "#EFF1F1");
    $("#prototipoListaMedicos tr").hover(
        function(data){
            $(this).addClass("selectedRow");
        },function(data){
             $(this).removeClass("selectedRow");   
        }
    );
    }

    function desactivarAlertas() {

        ClientApp.AlertasActivas = false;
    }
