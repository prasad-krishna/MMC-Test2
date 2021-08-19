
function ValidarCaracteresEspeciales(objControl) {

    var checkStr = objControl.value;
    var allValid = true;

    if (checkStr == "") {
        return false;
    }
    else {

        for (i = 0; i < checkStr.length; i++) {

            if ((checkStr.substring(i, i + 1) == "'") ||
		                (checkStr.substring(i, i + 1) == "<") ||
		                (checkStr.substring(i, i + 1) == ">") ||
		                (checkStr.substring(i, i + 1) == "=") ||
		                (checkStr.substring(i, i + 1) == "*") ||
		                (checkStr.substring(i, i + 1) == "&") ||
		                (checkStr.substring(i, i + 1) == "")
		                ) {
                allValid = false;
            }

        }
        if (!allValid) {
            alert("No se permite este tipo de caracteres:\n ', <, >, =, *, &");
            objControl.focus();
            return false;
        }
    }

    return true;
}