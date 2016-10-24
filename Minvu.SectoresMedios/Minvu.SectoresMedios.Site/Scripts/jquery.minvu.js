var Minvu = {
    isDebug: false,

    isAspForms: false,

    /*
    * Funcion de inicialización, se ejecuta en evento onDocumentReady y
    * al finalizar un postback en caso de windows forms.
    */

    load: function () {
        Minvu.logger.debug("Se inicia la carga...");

        /*
        * Checkea si es una aplicacion asp.net forms. Ademas se define si la aplicacion
        * esta en modo debug, dependiendo de la compilacion del sistema.
        * 
        * Observación: Esto es solo compatible con Forms y no con Framework MVC.
        */

        if (typeof Sys != 'undefined') {
            Minvu.isAspForms = true;
            Minvu.isDebug = Sys.Debug.isDebug;
            Minvu.logger.debug("Windows forms modo debug...");
        }

        Minvu.minValidaciones();
        Minvu.minContador();
        //Minvu.onDebug();
    },

    register: function () {

        Minvu.load();
        try {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(Minvu.load);
        } catch (e) {
            //Minvu.logger.log("No se ha cargado ajax webforms. [" + e.message + "]");
        }
    },

    /*
    * Funciones de log.
    */


    logger: {

        log: function (msg) { if (window.console) console.warn(msg); },

        debug: function (msg) { if (Minvu.isDebug && window.console) console.debug(msg); }

    },


    base64decode: function (s) { return Base64.decode(s); },

    base64encode: function (s) { return Base64.encode(s); },

    esEntero: function (n) { return +n === parseInt(n); },

    padding: function (value, padding) {
        var str = "" + value;
        var pad = padding;
        return pad.substring(0, pad.length - str.length) + str;
    },

    /*
    * OnDebug: Función que se ejecuta solo si esta en modo debug.
    */

    onDebug: function () {
        if (!Minvu.isDebug) return;

        $("body").append(Minvu.base64decode("PGRpdiBpZD0ibW9kb0RlYnVnSW5mbyIgc3R5bGU9InotaW5kZXg6OTk5OTk7cG9zaXRpb246Zml4ZWQ7cmlnaHQ6NXB4O2JvdHRvbTo1cHg7b3BhY2l0eTogMC43NTtwYWRkaW5nOiAxNXB4OyAgbWFyZ2luLWJvdHRvbTogMjBweDsgIGJvcmRlcjogMXB4IHNvbGlkIHRyYW5zcGFyZW50OyAgYm9yZGVyLXJhZGl1czogNHB4OyBjb2xvcjogI2E5NDQ0MjsgIGJhY2tncm91bmQtY29sb3I6ICNmMmRlZGU7ICBib3JkZXItY29sb3I6ICNlYmNjZDE7Zm9udC1mYW1pbHk6ICZxdW90O0hlbHZldGljYSBOZXVlJnF1b3Q7LEhlbHZldGljYSxBcmlhbCxzYW5zLXNlcmlmOyI+PHN0cm9uZz5Nb2RvIERlYnVnPC9zdHJvbmc+PC9kaXY+"));
    }
};


Minvu.posicionCursor = function (el) {
    var CURSOR_POSITION = 0;
    var input = el[0];
    if ('selectionStart' in input) {
        // Standard-compliant browsers
        return input.selectionStart;
    } else if (document.selection) {
        // IE
        input.focus();
        var sel = document.selection.createRange();
        var selLen = document.selection.createRange().text.length;
        sel.moveStart('character', -input.value.length);
        return sel.text.length - selLen;
    }
}

/*
* Validaciones
*
*/



Minvu.minValidaciones = function () {
    
    $(".NumerosEnteros").addClass("validarRegex").data("validacion", "[^0-9]+").data("valparam", "g");
    $(".DigitoVerificador").addClass("validarRegex").data("validacion", "[^0-9kK]").data("valparam", "g");
    $(".DigitoVerificadorE").addClass("validarRegex").data("validacion", "[^0-9kKeE]").data("valparam", "g");
    $(".Letras").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ]").data("valparam", "g");
    $(".Letras2").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ0-9]").data("valparam", "g");
    $(".CaracteresValidos").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9.,]").data("valparam", "g");
    $(".CaracteresCorreoElectronico").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9_@.,-]").data("valparam", "g");
    $(".CaracteresValidosFecha").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");
    $(".CaracteresValidosObservaciones").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9.,;:()-]").data("valparam", "g");
    $(".ExpresionFechaValida").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");
    $(".ExpresionRutValido").addClass("validarRegex").data("validacion", "[^0-9kK.-]").data("valparam", "g");
    $(".ExpresionRutValido2").addClass("validarRegex").data("validacion", "[^0-9kK-]").data("valparam", "g");
    //$(".NumerosDecimales").addClass("validarRegex").data("validacion", "[^0-9]{1,6}(\,?\.?[0-9]{1,2})?").data("valparam", "g");
    $(".NumerosDecimales").addClass("validarRegex").data("validacion", "[^0-9,]").data("valparam", "g");
    $(".NumerosGuion").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");


    //"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$"

    $(".validarRegex").each(function () {
        var ELEMENTO = $(this);
        if (Minvu.esEntero(ELEMENTO.attr("maxlength"))) {
            ELEMENTO.data("length", ELEMENTO.attr("maxlength"));
        }

        if (ELEMENTO.is("[maxlength]")) ELEMENTO.removeAttr("maxlength");

    }).bind("drop paste keydown", function (e) {
        var ELEMENTO = $(this);

        if (e.type == "keydown" && (e.keyCode == 8 || e.keyCode == 46 || e.keyCode == 13 || (e.keyCode >= 37 && e.keyCode <= 40))) {
        //if (e.type == "keydown"){
            return true;
        }

        setTimeout(function () {
            var ORIGINALSTR = ELEMENTO.val();
            var RESULTADO = ELEMENTO.val().replace(new RegExp(ELEMENTO.data("validacion"), ELEMENTO.data("valparam")), "");

            if (typeof ELEMENTO.data("length") != "undefined" && Number(ELEMENTO.data("length")) > 0) {
                //if (RESULTADO.length > Number(ELEMENTO.data("length")))
                    RESULTADO = RESULTADO.substr(0, ELEMENTO.data("length"));
            }

            if(ORIGINALSTR != RESULTADO)
                ELEMENTO.val(RESULTADO);

            if (ELEMENTO.is(".ValChange"))
                ELEMENTO.trigger("change");

        }, 0)
    });
};

Minvu.Trim = function (x) {
    return x.replace(/^\s+|\s+$/gm, '');
}

$.fn.actualizaHref = function (controlId) {
    var controlBase = $(this);
    var controlCopia = $('#' + controlId);
    var basehref = controlCopia.attr('href');

    controlBase.keyup(function (e) {
        if ((e.key >= 0 && e.key <= 9) || e.keyCode == 8 /*backspace*/) {
            if (controlBase.val() != '') {
                //controlCopia.attr('href', basehref + '/?numviv=' + 0);
                Minvu.calcularPorcentajes();
            }
            //else {
            //    controlCopia.attr('href', basehref + '/?numviv=' + controlBase.val());
            //}
        }
    });
}

Minvu.CheckBoxExcluyentes = function (groupName) {
    $("."+groupName).on('click', function () {
        // in the handler, 'this' refers to the box clicked on
        var $box = $(this);
        if ($box.is(":checked")) {
            // the name of the box is retrieved using the .attr() method
            // as it is assumed and expected to be immutable
            //var group = "input:checkbox[name='" + $box.attr("name") + "']";
            // the checked state of the group/box on the other hand will change
            // and the current value is retrieved using .prop() method
            $('.'+groupName).prop("checked", false);
            $box.prop("checked", true);
        } else {
            $box.prop("checked", false);
        }
    });
}

Minvu.calcularPorcentajes = function () {
    if ($("#numViviendasProyecto").val() != '') {
        var totalViviendas = parseInt($("#numViviendasProyecto").val());

        var vulnerables = 0;
        var medios = 0;
        $("[data-tipologia]").each(function () {
            if ($(this).val() != '') {
                if ($(this).data('tipologia') == '1') {
                    vulnerables = vulnerables + parseInt($(this).val());
                }
                else {
                    medios = medios + parseInt($(this).val());
                }
            }    
        });

        // asignar valores
        var porcVuln = 0;
        var porcMedios = 0;
        if (totalViviendas > 0) {
            porcVuln = (vulnerables * 100) / totalViviendas;
            porcMedios = (medios * 100) / totalViviendas;
        }

        $("#porcentajeVivVulnerables").val(Minvu.FormateaDecimales(porcVuln, 2));
        $("#porcentajeVivMedios").val(Minvu.FormateaDecimales(porcMedios, 2));

        $(".porcentajes").each(function () {
            if ($(this).data('numviviendas') != '') {
                if (totalViviendas > 0) {
                    var viv = parseInt($(this).data('numviviendas'));
                    var porc = (viv * 100) / totalViviendas;
                    $(this).html(Minvu.FormateaDecimales(porc, 2) + "%");
                }
            }
        });
    }
    else
    {
        $("#porcentajeVivVulnerables").val(Minvu.FormateaDecimales(0.00, 2));
        $("#porcentajeVivMedios").val(Minvu.FormateaDecimales(0.00, 2));
    }
}


Minvu.FormateaDecimales = function (value, precision) {
    var number = value.toFixed(precision);
    var str = number.toString();
    str = str.replace('.', ',');
    return str;
}

/*
* Contador para textbox
*/
Minvu.minContador = function () {
    $("textarea[data-contador]").bind("drop paste keyup", function () {
        var ELEMENTO = $(this);
        setTimeout(function () {
            var CONTADOR = $("#" + ELEMENTO.data("contador"));
            //var MENSAJE = "<strong>" + ELEMENTO.val().length + "/" + ELEMENTO.data("length") + "</strong> caracteres";
            var MENSAJE = "<strong>" + (parseInt(ELEMENTO.data("length")) - parseInt(ELEMENTO.val().length)) + "</strong> caracteres disponibles";
            if (CONTADOR.is("input")) CONTADOR.val(MENSAJE);
            else CONTADOR.html(MENSAJE);
        }, 0);
    }).trigger("paste");
}

Minvu.botonVolverArriba = function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn("fast", function () {
                $('#back-to-top').fadeTo("fast", 0.5);
            });
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });

    $('#back-to-top').tooltip('show');
}


/*
* PostbackOrLoad
*/
Minvu.$ = function (fn) {
    if (typeof fn != "function") return;

    $(fn);

    try {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(fn);
    } catch (e) { /* no me importa!!! */ }
}

/*
* Comprobar si jQuery existe.
*/
if (typeof jQuery == 'undefined') {
    document.body.innerHTML += Minvu.base64decode("PGRpdiBpZD0idmFsaWRhdGlvbk1lc3NhZ2UiIHN0eWxlPSJwb3NpdGlvbjpmaXhlZDt3aWR0aDo0MDBweDtsZWZ0OjUwJTttYXJnaW4tbGVmdDotMjAwcHg7dG9wOjEwcHg7b3BhY2l0eTogMC45O3BhZGRpbmc6IDE1cHg7ICBtYXJnaW4tYm90dG9tOiAyMHB4OyAgYm9yZGVyOiAxcHggc29saWQgdHJhbnNwYXJlbnQ7ICBib3JkZXItcmFkaXVzOiA0cHg7IGNvbG9yOiAjYTk0NDQyOyAgYmFja2dyb3VuZC1jb2xvcjogI2YyZGVkZTsgIGJvcmRlci1jb2xvcjogI2ViY2NkMTtmb250LWZhbWlseTogJnF1b3Q7SGVsdmV0aWNhIE5ldWUmcXVvdDssSGVsdmV0aWNhLEFyaWFsLHNhbnMtc2VyaWY7Ij48c3Ryb25nPsKhQWR2ZXJ0ZW5jaWEhPC9zdHJvbmc+IGpRdWVyeSBubyBoYSBzaWRvIGNhcmdhZG8uPC9kaXY+");
    Minvu.log("No se pudo cargar jQuery.")
} else {
    $(Minvu.register);
}

/*
* EXTRAS
*/
//BASE 64
function StringBuffer() { this.buffer = [] } function Utf8EncodeEnumerator(e) { this._input = e; this._index = -1; this._buffer = [] } function Base64DecodeEnumerator(e) { this._input = e; this._index = -1; this._buffer = [] } StringBuffer.prototype.append = function (t) { this.buffer.push(t); return this }; StringBuffer.prototype.toString = function e() { return this.buffer.join("") }; var Base64 = { codex: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = new StringBuffer; var n = new Utf8EncodeEnumerator(e); while (n.moveNext()) { var r = n.current; n.moveNext(); var i = n.current; n.moveNext(); var s = n.current; var o = r >> 2; var u = (r & 3) << 4 | i >> 4; var a = (i & 15) << 2 | s >> 6; var f = s & 63; if (isNaN(i)) { a = f = 64 } else if (isNaN(s)) { f = 64 } t.append(this.codex.charAt(o) + this.codex.charAt(u) + this.codex.charAt(a) + this.codex.charAt(f)) } return t.toString() }, decode: function (e) { var t = new StringBuffer; var n = new Base64DecodeEnumerator(e); while (n.moveNext()) { var r = n.current; if (r < 128) t.append(String.fromCharCode(r)); else if (r > 191 && r < 224) { n.moveNext(); var i = n.current; t.append(String.fromCharCode((r & 31) << 6 | i & 63)) } else { n.moveNext(); var i = n.current; n.moveNext(); var s = n.current; t.append(String.fromCharCode((r & 15) << 12 | (i & 63) << 6 | s & 63)) } } return t.toString() } }; Utf8EncodeEnumerator.prototype = { current: Number.NaN, moveNext: function () { if (this._buffer.length > 0) { this.current = this._buffer.shift(); return true } else if (this._index >= this._input.length - 1) { this.current = Number.NaN; return false } else { var e = this._input.charCodeAt(++this._index); if (e == 13 && this._input.charCodeAt(this._index + 1) == 10) { e = 10; this._index += 2 } if (e < 128) { this.current = e } else if (e > 127 && e < 2048) { this.current = e >> 6 | 192; this._buffer.push(e & 63 | 128) } else { this.current = e >> 12 | 224; this._buffer.push(e >> 6 & 63 | 128); this._buffer.push(e & 63 | 128) } return true } } }; Base64DecodeEnumerator.prototype = { current: 64, moveNext: function () { if (this._buffer.length > 0) { this.current = this._buffer.shift(); return true } else if (this._index >= this._input.length - 1) { this.current = 64; return false } else { var e = Base64.codex.indexOf(this._input.charAt(++this._index)); var t = Base64.codex.indexOf(this._input.charAt(++this._index)); var n = Base64.codex.indexOf(this._input.charAt(++this._index)); var r = Base64.codex.indexOf(this._input.charAt(++this._index)); var i = e << 2 | t >> 4; var s = (t & 15) << 4 | n >> 2; var o = (n & 3) << 6 | r; this.current = i; if (n != 64) this._buffer.push(s); if (r != 64) this._buffer.push(o); return true } } }

//window._originalAlert = window.alert;
//window.alert = function (text) {
//    var bootStrapAlert = function () {
//        if (!$.fn.modal.Constructor)
//            return false;
//        if ($('#windowAlertModal').length == 1)
//            return true;
//        $('body').append(' \
//    <div id="windowAlertModal" class="modal fade" data-backdrop="static" role="dialog"> \
//      <div class="modal-body"> \
//        <p> alert text </p> \
//      </div> \
//      <div class="modal-footer"> \
//        <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cerrar</button> \
//      </div> \
//    </div> \
//    ');
//        return true;
//    }
//    if (bootStrapAlert()) {
//        $('#windowAlertModal .modal-body p').text(text);
//        $('#windowAlertModal').modal();
//    } else {
//        console.log('bootstrap was not found');
//        window._originalAlert(text);
//    }
//}
//window._originalConfirm = window.confirm;
//window.confirm = function (text) {
//    var initTemplate = function () {
//        if ($('#windowConfirmModal').length == 1)
//            return true;
//        $('body').append(' \
//        <div id="windowConfirmModal" class="modal fade" data-backdrop="static" role="dialog"> \
//        <div class="modal-dialog"> \
//        <div class="modal-content"> \
//          <div class="modal-body"> \
//            <p> alert text </p> \
//          </div> \
//          <div class="modal-footer"> \
//            <div class="pull-left"><button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button></div> \
//            <div class="pull-right"><button class="btn btn-success" data-dismiss="modal" aria-hidden="true">Aceptar</button></div> \
//          </div> \
//        </div> \
//        </div> \
//        </div> \
//      ');
//    }

//    var bootStrapConfirm = function () {
//        if (!$.fn.modal.Constructor)
//            return false;

//        $('body').off('click', '#windowConfirmModal .btn-success');
//        $('body').off('click', '#windowConfirmModal .btn-danger');

//        function confirm() { return true; }
//        function deny() { return false; }

//        $('body').on('click', '#windowConfirmModal .btn-success', confirm);
//        $('body').on('click', '#windowConfirmModal .btn-danger', deny);

//        return true;
//    }

//    initTemplate()

//    if (bootStrapConfirm()) {
//        $('#windowConfirmModal .modal-body p').text(text);
//        $('#windowConfirmModal').modal();
//    } else {
//        console.log('bootstrap was not found');
//        cb(window._originalConfirm(text));
//    }
//}