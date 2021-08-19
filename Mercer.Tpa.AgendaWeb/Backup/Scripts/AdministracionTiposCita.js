/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console = { log: function() { } };
}

var ClientApp = ClientApp || {};
$(function() {

    try {
        $(".actualizarTipoCita").live('click',function(e) {
            var id = $(this).attr("idtipocita");
            var name = $(this).attr("nombretipocita");
            var duracion = $(this).attr("duraciontipocita");
            $("#dialogoTiposCita .id_dialogoTiposCita").val(id);
            $("#dialogoTiposCita .name_dialogoTiposCita").val(name);
            $("#dialogoTiposCita .duracion_dialogoTipoCita").val(duracion);
            $("#dialogoTiposCita").dialog('open');
            e.preventDefault();
        });
        $(".eliminarTipoCita").live('click', function(e) {
            var id = $(this).attr("idtipocita");
            $("#dialogoBorrarTiposCita .idTipoCitaABorrar").val(id);
            $("#dialogoBorrarTiposCita").dialog('open');
            e.preventDefault();

        });
        $("#addTipoCitaBtn").click(function(e) {
            $("#dialogoTiposCita .id_dialogoTiposCita").val("");
            $("#dialogoTiposCita .name_dialogoTiposCita").val("");
            $("#dialogoTiposCita .duracion_dialogoTipoCita").val("");
            $("#dialogoTiposCita").dialog('open');
            e.preventDefault();

        });
        $(".BtnCerrarDialogoTipoCita").click(function(e) {
            e.preventDefault();
            $("#dialogoBorrarTiposCita").dialog('close');

        });
        configurarDialogos();

    } catch (e) {
        if (console && console.log) {
            console.log(e.toString());
        }
    }


});
function configurarDialogos() {
    var dlg = $("#dialogoTiposCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 150,
        width: 280,
        modal: false,position:'top',
        autoOpen: false,
        open: function(event, ui) {
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });
    var dlg2 = $("#dialogoBorrarTiposCita").dialog({
        bgiframe: true,
        resizable: false,
        height: 80,
        width: 200,
        modal: false,position:'top',
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
    var dlg = $("#dialogoTiposCita").dialog();
    var dlg2 = $("#dialogoBorrarTiposCita").dialog();
    dlg.parent().appendTo(jQuery("form:first"));
    dlg2.parent().appendTo(jQuery("form:first"));
}

function validarCamposCita() {
    var errorMsg = [];
    var valido = true;
    var nombre = $("#dialogoTiposCita .name_dialogoTiposCita").val();
    var duracion = $("#dialogoTiposCita .duracion_dialogoTipoCita").val();
    if (nombre.length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Nombre requerido";

    }
    if (duracion.length == 0 || Agenda.isNumeric(duracion)==false) {
        valido = false;
        errorMsg[errorMsg.length] = "Duración en minutos requerida";
    }

    if (Agenda.isNumeric(duracion) && duracion <= 0) {
        valido = false;
        errorMsg[errorMsg.length] = "La duración debe ser mayor a cero";
    }

    if (!valido) {
        alert(errorMsg.join("\n"));
        return false;
    }
    
    return true;
    
}