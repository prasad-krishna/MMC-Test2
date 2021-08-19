//Inicio
//Auto:Diego Montejano Avila
//Proyecto: Auditoria 2014
//Fecha: 2014/09/17
//Observaciones: Valida el ingreso de caracteres a los controles
//var validKeys = [8, 9, 39, 37, 40, 38];
var validKeys = [8, 9];
function isCharSecure(event) {
    event = event || window.event // IE does not pass event to the function
    var key = event.keyCode == 0 || event.keyCode == null || event.keyCode == undefined ? event.charCode : event.keyCode;
    var Expr = new RegExp(/[a-zA-Z0-9\_\@\.\-\?\,\¿\¡\!\*\/\+\ñ\Ñ\á\é\í\ó\ú\Á\É\Í\Ó\Ú\ ]$/);
    if (Expr.test(String.fromCharCode(key)))
        return true;

    return validaKeys(key);

}
function isNumeric(event) {
    event = event || window.event // IE does not pass event to the function
    var key = event.keyCode == 0 || event.keyCode == null || event.keyCode == undefined ? event.charCode : event.keyCode;


    var Expr = new RegExp(/[0-9]$/);
    if (Expr.test(String.fromCharCode(key)))
        return true;

    return validaKeys(key);

}

function validaKeys(key) {

    for (var keyCode in validKeys) {
        if (validKeys[keyCode] == key)
            return true;
    }
    return false;
}

function ValidaInput() {

    //Validamos numeros
    $(".numero").keypress(function (event) {
        return isNumeric(event);
    });

    //Validamos cadena
    $(".cadena").keypress(function (event) {
        return isCharSecure(event);
    });
/*
    //Moneda
    $.each($(".moneda"), function (pos, val) {
        $(val).maskMoney({ showSymbol: true });
    });

    //Fraccion
    $(".fraccion").keypress(function (event) {

        event = event || window.event // IE does not pass event to the function
        var key = event.keyCode == 0 || event.keyCode == null || event.keyCode == undefined ? event.charCode : event.keyCode;

        if (!isNumeric(event)) {
            var Expr = new RegExp(/[\/\ ]$/);
            if (!Expr.test(String.fromCharCode(key)))
                return false;

            if (($(this).val().indexOf("/") > -1 && String.fromCharCode(key) == "/") || ($(this).val().indexOf(" ") > -1 && String.fromCharCode(key) == " "))
                return false;
            if (($(this).val().indexOf("/") > -1 && String.fromCharCode(key) == " "))
                return false;
            if (($.Right($(this).val(), 1) == " " && String.fromCharCode(key) == "/"))
                return false;

        }
        return true;
    });

    $(".fraccion").focusout(function (event) {
        try {

            eval($(this).val().replace(" ", ""));
            if ($(this).val().indexOf("/") == -1 && $(this).val().indexOf(" ") > -1)
                $(this).val($(this).val().replace(" ", ""));
            if ($(this).val().indexOf("/") > -1)
                $(this).val(simplificaFraccion($(this).val()));
        }
        catch (e) {
            alert(e.message);
            $(this).val("");
            var control = $(this);
            MsgBox("Error", "El valor introducido para la fracción no es valido", function () { $(control).focus() }, false);
        }
    });*/
}