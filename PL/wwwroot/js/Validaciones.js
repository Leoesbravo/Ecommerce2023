function SoloLetras(e, idTextBox, idLabel) {
    var caracter = e.key;
    if (!/^[a-zA-Z]/g.test(caracter)) {
        $('#' + idLabel).text("Solo se permiten letras");
        $('#' + idLabel).css({ "color": "red" });
        $('#' + idTextBox).css({ "border-color": "red" });
        $('#' + "basic-addon12").css({ "border-color": "red", "background-color": "#f8d7da" });
        return false;
    }
    else {
        $('#' + idLabel).text("");
        $('#' + idLabel).css({ "color": "green" });
        $('#' + idTextBox).css({ "border-color": "green" });
        $('#' + "basic-addon12").css({ "border-color": "green", "background-color": "#5CB443" });

    }
}
function SoloNumeros(e, idTextBox, idLabel) {
    var caracter = e.key;
    if (!/^\d+$/g.test(caracter)) {
        $('#' + idLabel).text("Solo se permiten numeros");
        $('#' + idLabel).css({ "color": "red" });
        $('#' + idTextBox).css({ "border-color": "red" });
        $('#' + "basic-addon1").css({ "border-color": "red", "background-color": "#f8d7da" });
        return false;
    }
    else {
        $('#' + idLabel).text("");
        $('#' + idLabel).css({ "color": "green" });
        $('#' + idTextBox).css({ "border-color": "green" });
        $('#' + "basic-addon1").css({ "border-color": "green", "background-color": "#5CB443" });

    }
}