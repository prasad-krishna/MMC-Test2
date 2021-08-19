
/*Acciones que se ejecutan al cargar*/
var ClientApp = ClientApp || {};
/*Formato global de fechas usado por el datepicker de jquery*/
/*Solo se deben utilizar los formatos soportados por jquery (no son los mismos de .NET)
d - day of month (no leading zero)
dd - day of month (two digit)
o - day of the year (no leading zeros)
oo - day of the year (three digit)
D - day name short
DD - day name long
m - month of year (no leading zero)
mm - month of year (two digit)
M - month name short
MM - month name long
y - year (two digit)
yy - year (four digit)
@ - Unix timestamp (ms since 01/01/1970)
 ! - Windows ticks (100ns since 01/01/0001)
'...' - literal text
'' - single quote
anything else - literal text
*/
ClientApp.formatoFecha = "dd/mm/yy";


$(function() {

    setupProfiler(false);
    fixCacheIe();
    console.profile("Bloque configuración common.js");
    console.profile("Configuracion control error");
    configurarControlError();
    console.profileEnd();
    console.profile("Ajustar iframe");
    ajustarIFrame();
    console.profileEnd();
    /*Configurar textAreas maxlength */
    console.profile("Configurar text areas");
    setMaxLength();
    console.profileEnd();
    console.profile("Configurar grids");
    configurarGrids();
    console.profileEnd();
    console.profileEnd();
});

/*Este es el fix para el bug que hace que se recarguen las imagenes y haya flickr (Solo se aplica para IE6)*/
function fixCacheIe() {
    if ($.browser.msie && parseFloat($.browser.version) < 7) {
        try {
            document.execCommand("BackgroundImageCache", false, true);
        } catch (err) { } 

    } 
}

function setupProfiler(enabled) {
    window.console = window.console || {};
    if (window.console.profile == undefined) {
        /*Para IE y otros browsers simplemente imprimimos los resultados*/
        window.colaProfilers = [];
        $("<div id='profilerInfo'></div>").appendTo($("body"));
        var refProfilerElem = $("#profilerInfo");
        window.console = {};
        window.console.profile = function(title) {
            if (enabled == false)
                return;
            //Agregar a la cola de profilers
            colaProfilers[colaProfilers.length] = { name: title, start: new Date(),deep:colaProfilers.length };
        };

        window.console.profileEnd = function() {
            if (enabled == false)
                return;
            var endTime = new Date();
            var currentProfiler = colaProfilers[colaProfilers.length - 1];
            var duration = endTime.valueOf() - currentProfiler.start.valueOf();
            colaProfilers.pop();
            refProfilerElem.append("<div style='margin-left:"+currentProfiler.deep*10+"'>"+currentProfiler.name + " (" + duration + " ms )</div>");
        };
    }
}

function configurarGrids() {
   
    /*Iluminar fila actual*/
    $("table.grid tr").hover(function() {
      //  $(this).children("td").addClass("filaSeleccionada");
       $(this).addClass("filaSeleccionada");

    }, function() {
       // $(this).children("td").removeClass("filaSeleccionada");
       $(this).removeClass("filaSeleccionada");

    });
   

  
    /*Cortar textos en grid y mostrarlos como tooltip*/
    $("table td.columnaGrid").each(function() {
        if ($(this).text().length > 30) {
            var texto = $(this).text();
            $(this).text(texto.substring(0, 29));
            /*Agregar tres puntos para mostrar el texto ...*/
            var link = $("<a href='#' onclick='return false;'>(...)</a>");
            $(link).attr("title", texto);
            $(this).append(link);

        }
    });
   


}

function ajustarIFrame() {
    /*Asignar el tamaño del IFRAME contenedor al tamaño del documento mas un margen*/
    if (parent != undefined && parent.document != undefined) {
        var iframe = parent.document.getElementById("ifrPageContent");
        if (iframe != null) {
            iframe.style.height = getAltoDocumento() + 150 + "px";
        }
    }
}
/*al hacer click en mostrar detalles error se muestra/esconde el dialog de errores*/
function configurarControlError() {
    $("a.linkDetallesError").click(function(e) {
        //toggle detalles
        $(this).siblings("div.detallesError").toggle();
        e.preventDefault();
    });
}

/* Funciones comunes para todos los modulos de la agenda*/

/*Definición de namespace Agenda*/
var Agenda = Agenda || {};

Agenda.isNumeric = function(num) {
    return !isNaN(parseInt(num))
}
/*Configuracion de menu contextual para los grids
Parametros
@selectorMenuContainer Selector para obtener el contenedor del menu
@selectorLinksMenu Selector para obtener los links que abriran el menu
@onShow , callback para llamar antes de mostrar el menu (usado para inicializacion de codigo)
Recibe como parametro el elemento link
*/
Agenda.setupMenuGrid = function(selectorMenu, selectorLinksMenu, onShow) {

    var menuContainer = $(selectorMenu);

    /*Crear el dialogo*/
    var dlg = $(menuContainer).dialog({
        bgiframe: true,
        width: 210,
        resizable: false,
        modal: false,
        autoOpen: false,
        open: function(event, ui) {

        },
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        }
    });

    /*configurar boton de cerrar dialogo*/
    $("a.linkCerrarMenu").click(function(ev) {
        $(dlg).dialog("close");
        ev.preventDefault();
    });

    /*Configurar links internos para que cierren el dialogo*/
    $("div.menuContextualGrid a:not(.linkCerrarMenu)").click(function(e) {
        $(dlg).dialog("close");
        e.preventDefault();
    });

    /*Configurar hover*/


    /*Esconder barra de titulo*/
    $(selectorMenu).siblings(".ui-dialog-titlebar").hide()
    /*Mostrar el dialogo cuando se hace click*/
    $(selectorLinksMenu).live("click", function(ev) {

        if (menuContainer.length == 0) {
            throw new Error("No se encontró elemento contenedor de menu con id:" + idMenuContainer);
        }
        // var locX = Math.round(ev.pageX - $(this).offset().left);
        // var locY = Math.round(ev.pageY - $(this).offset().top);
        var locX = Math.round($(this).offset().left);
        var locY = Math.round($(this).offset().top);
        var left = locX;
        var top = ev.pageY;
        /*A top tenemos que quitarle el scroll*/
        top = top - $(window).scrollTop();
        // alert("pos (left,top):" + left + " " + top);

        $(dlg).dialog("option", "position", [left, top]);
        $(dlg).dialog("open");
        if (onShow) {
            onShow($(this));
        }
        ev.preventDefault();
    });

    /*Configurar fieldsets expandibles*/
    $("fieldset.expandible .linkToggle").click(function(e) {
        $(this).closest("fieldset").children(".contenido").toggle();
        e.preventDefault();
    });
}

/*Retorna el tamaño del documento*/
function getAltoDocumento() {
    return Math.max(
        $(document).height(),
        $(window).height(),
    /* For opera: */
        document.documentElement.clientHeight
    );
};


var W3CDOM = document.createElement && document.getElementsByTagName;

/*Funcion que permite usar maxlength en textAreas*/
function setMaxLength() {
    if (!W3CDOM) return;
    var textareas = document.getElementsByTagName('textarea');
    var counter = document.createElement('div');
    counter.className = 'counter';
    for (var i = 0; i < textareas.length; i++) {
        if (textareas[i].getAttribute('maxlength')) {
            var counterClone = counter.cloneNode(true);
            counterClone.innerHTML = '<span>0</span>/' + textareas[i].getAttribute('maxlength');
          //  textareas[i].parentNode.insertBefore(counterClone, textareas[i].nextSibling);
            textareas[i].relatedElement = counterClone.getElementsByTagName('span')[0];
            textareas[i].onkeyup = textareas[i].onchange = checkMaxLength;
            textareas[i].onkeyup();
            $(textareas[i]).focus(function() {

                $(this)[0].onkeyup();
            });
        }
    }
}

function checkMaxLength() {
    var maxLength = this.getAttribute('maxlength');
    var currentLength = this.value.length;
    if (currentLength > maxLength) {
        this.relatedElement.className = 'textoFueraLimite';
        /*No permitir input de mas caracteres*/
        this.value = this.value.substring(0, maxLength - 1);

    }
    else
        this.relatedElement.className = '';
   // this.relatedElement.firstChild.nodeValue = currentLength;
}