/** Logica de cliente Modulo de Agenda **/
//Namespace por defecto ClientApp
if (!window.console) {
    var console = { log: function() { } };
}

var ClientApp = ClientApp || {};
$(function() {

    try {
        $(".actualizarMedio").live('click', function(e) {
            var id = $(this).attr("idmedio");
            var name = $(this).attr("nombreMedio");
            $("#dialogoMedio .id_dialogoMedio").val(id);
            $("#dialogoMedio .name_dialogoMedio").val(name);
            $("#dialogoMedio").dialog('open');
            e.preventDefault();


        });
        $(".eliminarMedio").live('click', function(e) {
            var id = $(this).attr("idmedio");
            $("#dialogoBorrarMedios .idMedioABorrar").val(id);
            $("#dialogoBorrarMedios").dialog('open');
            e.preventDefault();

        });
        $(".BtnCerrarDialogoMedio").click(function(e) {
            $("#dialogoBorrarMedios").dialog('close');
            e.preventDefault();

        });

        $("#addMedioBtn").click(function(e) {
            $("#dialogoMedio .id_dialogoSedes").val("");
            $("#dialogoMedio .name_dialogoSedes").val("");
            $("#dialogoMedio").dialog('open');
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
    var dlg = $("#dialogoMedio").dialog({
        bgiframe: true,
        resizable: false,
        height: 100,
        width: 280,
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
    var dlg2 = $("#dialogoBorrarMedios").dialog({
        bgiframe: true,
        resizable: false,
        height: 80,
        width: 280,
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
    var dlg = $("#dialogoMedio").dialog();
    var dlg2 = $("#dialogoBorrarMedios").dialog();
    dlg.parent().appendTo(jQuery("form:first"));
    dlg2.parent().appendTo(jQuery("form:first"));
}
function validarParametros() {
    if ($("#dialogoMedio .name_dialogoMedio").val().length == 0) {
        alert("Nombre requerido");
        return false;
    }
    return true;
}