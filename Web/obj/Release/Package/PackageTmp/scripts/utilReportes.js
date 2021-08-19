function dhtmlLoadScript(url){
   var e = document.createElement("script");
   e.src = url;
   e.type="text/javascript";
   document.getElementsByTagName("head")[0].appendChild(e);
}
function expandir(id,request,xml){
    var divid = document.getElementById("div"+id);
    var frameid = document.getElementById("frameid"+id);
    if(divid.style.visibility == "hidden"){
        divid.style.visibility = "visible";
        divid.style.height = "auto";
        divid.style.fontSize = "10px";
        frameid.src='reporteInterno.aspx?xml='+xml+"&"+request;
        frameid.height = 500;
        frameid.width = 1000;
    }
}
function colapsar(id){
    var divid = document.getElementById("div"+id);
    var frameid = document.getElementById("frameid"+id);
    if(divid.style.visibility == "visible"){
        divid.style.visibility = "hidden";
        divid.style.height = "0px";
        divid.style.fontSize = "0px";
        frameid.height = 0;
        frameid.width = 0;
    }
}

function seleccionarEntidad(NOMBREENTIDAD, TABLABASE, IDTABLABASE, NOMBRETABLABASE, POSICIONCAMPO, CADENACONEXION, CORPORATIVO, CONDICION) {
    var post = "";
    post = "formaSeleccionarEntidadReporte.aspx?nombreEntidad=" + NOMBREENTIDAD + "&tabla=" + TABLABASE + "&campo_valor=" + IDTABLABASE + "&campo_nombre=" + NOMBRETABLABASE + "&nombre_campo=" + POSICIONCAMPO + "&titulo_campo=" + NOMBREENTIDAD + "&conexion=" + CADENACONEXION + "&corporativo=" + CORPORATIVO + "&condicion=" + CONDICION;
    window.open(post,'Seleccionador','width=500,height=500,top=50,left=50,resizable=yes,toolbars=no,menubars=no');
}
