/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console = { log: function() { } };
}

var ClientApp = ClientApp || {};
$(function() {

    try {
        $(".actualizarSede").live('click', function(e) {
            var id = $(this).attr("idsede");
            var name = $(this).attr("nombreSede");
            var description = $(this).attr("descripcionSede");
            $("#dialogoSedes .id_dialogoSedes").val(id);
            $("#dialogoSedes .name_dialogoSedes").val(name);
            $("#dialogoSedes .descripcion_dialogoSedes").val(description);
            $("#dialogoSedes").dialog('open');
            e.preventDefault();

        });
        $(".eliminarSede").live('click', function(e) {
            var id = $(this).attr("idsede");
            $("#dialogoBorrarSedes .idSedeABorrar").val(id);
            $("#dialogoBorrarSedes").dialog('open');
            e.preventDefault();

        });
        $(".BtnCerrarDialogoSede").click(function(e) {
            e.preventDefault();
            $("#dialogoBorrarSedes").dialog('close');

        });
        $("#addSedeBtn").click(function(e) {
            $("#dialogoSedes .id_dialogoSedes").val("");
            $("#dialogoSedes .name_dialogoSedes").val("");
            $("#dialogoSedes .descripcion_dialogoSedes").val("");
            $("#dialogoSedes").dialog('open');
            e.preventDefault();

        });
        configurarDialogos();

    } catch (e) {
        if (console && console.log) {
            console.log(e.toString());
        }
    }


});
function configurarDialogos() {
    var dlg=$("#dialogoSedes").dialog({
        bgiframe: true,
        resizable: false,
        width: 280,
        modal: false,position:'top',
        autoOpen: false,
        open:function(event,ui){
            fixDialogos();
        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });
    var dlg2 = $("#dialogoBorrarSedes").dialog({
        bgiframe: true,
        resizable: false,
        height: 100,
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
    var dlg = $("#dialogoSedes").dialog();
    var dlg2 = $("#dialogoBorrarSedes").dialog();
    dlg.parent().appendTo(jQuery("form:first"));
    dlg2.parent().appendTo(jQuery("form:first"));
}

function validarParametrosSede() {
    var errorMsg = [];
    var valido = true;
    var nombre= $("#dialogoSedes .name_dialogoSedes").val();
    var desc = $("#dialogoSedes .descripcion_dialogoSedes").val();
    if (nombre.length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Nombre requerido";
    }
    if (desc.length == 0) {
        valido = false;
        errorMsg[errorMsg.length] = "Descripción requerida";
    }
    if (!valido) {
        alert(errorMsg.join("\n"));
    }
    return valido;    
}