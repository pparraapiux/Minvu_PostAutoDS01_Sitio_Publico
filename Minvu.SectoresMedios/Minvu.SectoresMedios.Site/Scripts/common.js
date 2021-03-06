function ShowLoading() {
    $('#modalCargando').modal({
        keyboard: false,
        backdrop: 'static'
    });
}

function CloseLoading() {
    $('#modalCargando').modal('hide');
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('.solo-numero').keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });
    //Minvu.botonVolverArriba();
});

function cortadecimales(monto)
{
    var flag = parseFloat(monto.value);
    if (monto.toString().length > 0) {
        if (flag.toString() == "NaN") {
            document.getElementById(monto.id).value = "";
        }
        else {
            var monto0 = monto.value.toString().replace(",", ".");
            var v = parseFloat(monto0);
            var a = Math.floor(v * 100) / 100;
            var monto1 = a.toFixed(2);
            var monto2 = 0;
            monto2 = monto1.toString().replace(".", ",");
            document.getElementById(monto.id).value = monto2;
        }
    }
}

function cortaEspacios(txt) {
    if (txt.value.length > 1) {
        document.getElementById(txt.id).value = txt.value.replace("  ", " ");
    } else {
        document.getElementById(txt.id).value = txt.value.replace(" ", "");
    }
}

function cortaGuiones(txt)
{
    if (txt.value.length > 1) { // posicion 0 y 1
        if (txt.value.indexOf("-") != -1) {

            if (txt.value.indexOf("-") != (txt.value.length - 2)) {
                var largo = txt.value.length;
                for (var i = 0 ; i < largo; i++) {

                    if (txt.value[0] == "-") {
                        txt.value = txt.value.substr(1);
                    }

                    if (txt.value[txt.value.length - 1] == "-") {
                        txt.value = txt.value.substr(0, txt.value.length - 1);
                    }
                }
            }
            document.getElementById(txt.id).value = txt.value;
        }
    } else {
        txt.value = txt.value.replace("-", "");
        document.getElementById(txt.id).value = txt.value;
    }
}

//Variable en la cual se setea el lenguaje para las grillas, jquery Datatables
var lenguajeGrilla = {
    "infoEmpty" : "",
    "lengthMenu": "Mostrar _MENU_ registros por página",
    "info": "Página _PAGE_ de _PAGES_",
    "zeroRecords": "No se encontraron registros",
    "sLoadingRecords": "Cargando...",
    "sProcessing": "Procesando...",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sSearch": "Buscar registros:",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "oPaginate": {
        "sFirst": "Primero",
        "sPrevious": "Anterior",
        "sNext": "Siguiente",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending":  ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

function getProvincias(region, idControl, comuna, localidad) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append('<option value="">--Seleccione--</option>');

    $('#' + comuna.id).find('option').remove().end();
    $('#' + comuna.id).append('<option value="">--Seleccione--</option>');

    $('#' + localidad.id).find('option').remove().end();
    $('#' + localidad.id).append('<option value="0">--Seleccione--</option>');


    if (region.value != null) {
        $.post('/Common/Provincias'
        , { idRegion: region.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function getComunas(region, provincia, idControl, localidad) { 
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append('<option value="">--Seleccione--</option>');

    $('#' + localidad.id).find('option').remove().end();
    $('#' + localidad.id).append('<option value="0">--Seleccione--</option>');

    if (region.value != null && provincia.value != null) {
        $.post('/Common/Comunas'
        , { idRegion: region.value, idProvincia: provincia.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function getComunasSinProvincia(ddlRegion, idDdlComuna, idComunaSelected) {
    var regid = $('#' + ddlRegion).val();
    $('#' + idDdlComuna).find('option').remove().end();
    //$('#' + idDdlComuna).append('<option value="">-- Seleccione --</option>');
    if (regid == '') { regid = '0' }
    if (regid != null && regid != "") {
        $.post('/Common/Comunas'
        , { idRegion: regid, idProvincia: 0 }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idDdlComuna).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function getHabitantesPorComuna(ddlComuna, idTextBox)
{
    var comid = $('#' + ddlComuna).val();
    if (comid != null && comid != "") {
        $.post('/Common/HabitantesPorComuna'
        , { idComuna: comid }
        , function (data) {
            $('#' + idTextBox).val(data);
            $('#' + idTextBox).prettynumber();
        });
    }
}

function getLocalidad(Comuna, idControl) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append($('<option>', {
        value: 0,
        text: '--Seleccione--'
    }));

    if (Comuna.value != null) {
        $.post('/Common/Localidades'
        , { idComuna: Comuna.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function getEntidad(rut,strRegion)
{
    if (rut.val() != null) {
        var aux = rut.val().replace('.', '');
        aux = aux.replace('.', '');

        if (VerificaRut(aux) == false) {
            CloseLoading();
            $('#ED_entNom').val("");
            $('#ModalError').find('.modal-body').text('El RUT ingresado no es válido.')
            $('#ModalError').modal('show');
            return;
        }
        else
        {
            ShowLoading();
            rut.attr("disabled", "true")
            rut.removeAttr("disabled");

            $.post('/Common/Entidad'
            , { rutCompleto: rut.val(), region: strRegion }
            , function (data) {
                $('#ED_entId').val(data.entId);
                $('#ED_entNom').val(data.entNom);
                CloseLoading();
            })
        }

    }
}


function getInmobiliaria(rut, nombre) {

    //if ($(rut).val() == "") {
    //    $(rut).popover("show");
    //    return;
    //}
    if (rut.value != null) {

        var aux = rut.value.replace('.', '');
        aux = aux.replace('.', '');

        if (VerificaRut(aux) == false) {
            CloseLoading();
            document.getElementById('lblNombre').innerText = " ";
            $('#ModalError').find('.modal-body').text('El RUT ingresado no es válido.')
            $('#ModalError').modal('show');
            //$(rut).popover("show");
            return;
        }

    }

    if (rut.value != null) {
        ShowLoading();


        //remueve validaciones del input-fields
        $('#_txtRut').addClass('input-validation-valid');
        $('#_txtRut').removeClass('input-validation-error');
        //remueve mensajes de validacion input-fields
        $('#idSpanRut').hide();

        //$('#_txtRut .field-validation-error').addClass('field-validation-valid');
        //$('#_txtRut .field-validation-error').removeClass('field-validation-error');
        //remueve validaciones summary 
        //$('#_txtRut .validation-summary-errors').addClass('validation-summary-valid');
        //$('#_txtRut .validation-summary-errors').removeClass('validation-summary-errors')

        $.post('/Common/Inmobiliaria'
            , { rutCompleto: rut.value }
            , function (data) {
                if (data.estado == "VIGENTE") {
                    //$('#lblnombre').append(data.razonSocial);
                    $('#lblNombre').empty();
                    //  $('#' + nombre.id).removeAttr(data.razonSocial)
                    $('#lblNombre').append(data.razonSocial);
                    document.getElementById('lblNombre').innerText = " ";
                    document.getElementById('lblNombre').innerText = data.razonSocial;
                    document.getElementById('razonSocialInmobiliaria').value = data.razonSocial;
                    document.getElementById('rutInmobiliaria').value = data.rutConcatenado;
                    document.getElementById('estadoInmobiliaria').value = data.estado;
                    CloseLoading();


                }
                else if (data.estado == "NULL") {
                    CloseLoading();
                    document.getElementById('lblNombre').innerText = " ";
                    $('#ModalError').find('.modal-body').text('El RUT ingresado no existe en los registros técnicos o no es válido.')
                    document.getElementById('razonSocialInmobiliaria').value = "";
                    $('#ModalError').modal('show');
                    return;
                }
                else {
                    CloseLoading();
                    document.getElementById('lblNombre').innerText = " ";
                    $('#ModalError').find('.modal-body').text('La empresa constructora no se encuentra vigente en los Registros Técnicos.  Debe regularizar su situación para participar en un proceso de postulación.')
                    document.getElementById('estadoInmobiliaria').value = data.estado;
                    document.getElementById('razonSocialInmobiliaria').value = "";
                    $('#ModalError').modal('show');
                    return;
                }

            }
    )
    }
}

function traerDatosCuenta(rut,cuenta,banco,montoCuenta,statusCuenta)
{

    var error = false;
    if (banco.selectedIndex == 0) {
        $(banco).popover("show");
        error = true;
    }
    else
    {
        //remueve validaciones del input-fields
        $('#_ddlBanco').addClass('input-validation-valid');
        $('#_ddlBanco').removeClass('input-validation-error');
        //remueve mensajes de validacion input-fields
        $('#spanBanco').hide();
    }
    if (rut.value == null ||rut.value == "") {
        $(rut).popover("show");
        error = true;
    }
    else
    {
        //remueve mensajes de validacion input-fields
        $('#spanRutTitular').hide();
    }
    if (cuenta.value == null || cuenta.value == "") {
        $(cuenta).popover("show");
        error = true;
    }
    else {
        //remueve validaciones del input-fields
        $('#_txtCuenta').addClass('input-validation-valid');
        $('#_txtCuenta').removeClass('input-validation-error');
        //remueve mensajes de validacion input-fields
        $('#spanCuenta').hide();
    }

    var aux = rut.value.replace('.', '');
    aux = aux.replace('.', '');
    if (!VerificaRut(aux)) {
        $('#ModalError').find('.modal-body').text('El RUT ingresado no es válido.')
        $('#ModalError').modal('show');
        error = true;
    }
    else
    {
        //remueve validaciones del input-fields
        $('#_txtRut_').addClass('input-validation-valid');
        $('#_txtRut_').removeClass('input-validation-error');
    }

    if (error) { return; }
       
   if (rut.value !=null && cuenta.value !=null && banco.value !=null)
   {
        ShowLoading();
        $.post('/Common/datosCuenta', { rutCompleto: rut.value, banco: banco.value, cuenta: cuenta.value }
        ,function (data)
        {
            document.getElementById("lblMontoAhorroAcreditado").innerText = ": "+ data.SaldoCuentaMes;
            document.getElementById("lblEstadoCuenta").innerText = ": " + data.statusCuenta;
            $("#AhorroAcreditado").val(data.SaldoCuentaMes);
            $("#estadoCuenta").val(data.statusCuenta);
            $('#hiddenCuentaBanco').val(cuenta.value);
            CloseLoading();
            if (data.SaldoCuentaMes == null)
            {
                $("#lblMontoAhorroAcreditado").text("Datos no encontrados.");
                $("#lblEstadoCuenta").text("No es posible confirmar estado de la cuenta.");
                $("#estadoCuenta").val("ERROR");
                CloseLoading();
            }            
        }
        )
    }
}



function EliminarAporteAdicional(idTr, idChck,idTdMonto)
{

    document.getElementById(idChck.value).value = false;

    var valorTr = parseFloat(document.getElementById(idTr.value).cells[0].innerHTML.toString().replace(",","."));
    var total = 0;
    var valorAporte = parseFloat(document.getElementById('idTotalAporteAdicional').value.replace(",", ".")).toFixed(2);

    total = (parseFloat(valorAporte) - parseFloat(valorTr));

    valorAporte = total;
    document.getElementById('totalAporteAdicional').innerHTML = ":" + total.toString().replace(".",",");
    document.getElementById('idTotalAporteAdicional').value = total.toString().replace(".", ",");
    // $("#" + idTr.value).hide();
    $("#" + idTr.value).hide();
    $("#deleteAdicional").modal('hide');
    document.getElementById(idTdMonto.id).innerHTML = "0";
    AcreditacionAporteAdicional(valorAporte);

}

function EliminarAporteTercero(idTr,idChck) {
    $("#" + idTr.id).hide();

    document.getElementById(idChck.value).value = false;

    var valorTr = parseFloat(idTr.cells[0].innerHTML);
    var total = 0;
    var valorAporte = Number(document.getElementById('idTotalAporteTercero').value);
    
    total = (valorAporte - valorTr);

    valorAporte = total;
    document.getElementById('totalAporteTercero').innerHTML = ":" + total.toString();
    document.getElementById('idTotalAporteTercero').value = total;
    $("#" + idTr.value).hide();
    $("#deleteTercero").modal('hide');
}

function SumaAporteAdicional(tabla)
{
    var total = 0;
    var checkAporte = "";
    var variable = 0;

    if(tabla.id == "tablaLlamado"){
        checkAporte = $.trim($("#checkA" + 0).attr("value"));
        for (var i = 1; i < tabla.rows.length; i++) {
            if (checkAporte == "true" || checkAporte == "True") {
                variable = parseFloat(tabla.rows[i].cells[0].innerHTML.toString().replace(",",".")); 
                total += variable
            }
            checkAporte = $.trim($("#checkA" + i).attr("value"));
        }
    } else if (tabla.id == "tablaLlamado2") {
        checkAporte = $.trim($("#checkT" + 0).attr("value"));
        for (var i = 1; i < tabla.rows.length; i++) {
            if (checkAporte == "true" || checkAporte == "True") {
                variable = parseFloat(tabla.rows[i].cells[0].innerHTML.toString().replace(",", "."));
                total += variable;
            }
            checkAporte = $.trim($("#checkT" + i).attr("value"));
        }
    }
    return total;
}

function getListadoAporte(Monto, Institucion, Fecha, tabla) {
    /*
    * Se comprueba que los datos solicitados sean correctos.
    *
    */
    var error = false;
    if (Institucion.selectedIndex == 0) {
        $(Institucion).popover("show");
        error = true;
    }

    if ($(Monto).val() == "") {
        $(Monto).popover("show");
        error = true;
    }

    if ($(Fecha).val() == "") {
        $(Fecha).popover("show");
        error = true;
    }

    if (error) return;

    var valor = Institucion.options[Institucion.selectedIndex].text;
    var a = tabla.rows.length - 1;
    var tipoIngreso = tabla.id == "tablaLlamado" ? 1 : 2;
    //1 aporte adicional
    //2 terceros
    $.post('/Common/ListadoAporte',
            { montoAporte: Monto.value, indiceInstitucion: Institucion.value, FechaAporte: Fecha.value, nombreInstitucion: valor, tipo: tipoIngreso },
            function (data) {
                $.each(data, function (i, item) {
                    if (parseFloat(item.totalMontoUF).toFixed(2) == 0)
                    {
                        //Aporte Adicional: maximo 6 meses de antiguedad
                        $('#ModalError').find('.modal-body').text('Ingrese un monto mayor a 0 en Total Monto en UF.')
                        $('#ModalError').modal('show');
                        return;
                    }

                    if (item.nombreInstitucion != "ERROR") {
                        var MyDate_String_Value = item.fechaAporte;
                        var value = new Date(parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, '')));

                        var date = (value.getDate() > 9 ? value.getDate() : "0" + value.getDate()) +
                                                                      "-" +
                                                          ((value.getMonth() + 1) > 9 ? (value.getMonth() + 1) : "0" + (value.getMonth() + 1)) +
                                                                      "-" +
                                                          value.getFullYear();
                        var ufMonto = parseFloat(item.totalMontoUF).toFixed(2);
                    }
                    else if (item.nombreInstitucion == "ERROR" && tipoIngreso == 1) {
                        //Aporte Adicional: maximo 6 meses de antiguedad
                        $('#ModalError').find('.modal-body').text('La fecha del aporte adicional debe ser menor o igual al día de la postulación, ' +
                                                                    'con un máximo de seis meses de antigüedad.')
                        $('#ModalError').modal('show');
                        return;
                    }
                    else if (item.nombreInstitucion == "ERROR" && tipoIngreso == 2) {
                        //Aporte Terceros: maximo 12 meses de antiguedad
                        $('#ModalError').find('.modal-body').text('La fecha del aporte de terceros debe ser menor o igual al '
                                                            + 'último día hábil del mes anterior a la postulación, con un máximo de doce meses de antigüedad.')
                        $('#ModalError').modal('show');
                        return;
                    }


                    if (tabla.id == "tablaLlamado" && item.nombreInstitucion != "ERROR") {
                        $(

                    "<tr id='Adi" + a + "' >" +
                     "<input name='listAporteAdicional[" + a + "].totalMontoUF' type='hidden' value='" + item.totalMontoUF.toString().replace(".",",") + "'>" + //' id='"+ "montoAd"+a+"'
                     "<input name='listAporteAdicional[" + a + "].nombreInstitucion' type='hidden' value='" + item.nombreInstitucion + "'>" +
                     "<input name='listAporteAdicional[" + a + "].idInstitucion' type='hidden' value='" + item.idInstitucion + "'>" +
                     "<input name='listAporteAdicional[" + a + "].fechaAporte' type='hidden' value='" + date + "'> " +
                     "<input name='listAporteAdicional[" + a + "].checkEstado' type='hidden' value='true' id='checkA" + a + "'> " +
                    "<td id='TdAdi"+a+"'>" + item.totalMontoUF.toString().replace(".",",") + " </td>" +
                    "<td>" + item.nombreInstitucion + "</td>" +
                    "<td>" + date + "</td>" +
                    "<td>" + "<input type='button' value='Eliminar' class='btn btn-block' data-toggle='modal' data-target='#deleteAdicional' onclick='DeleteAdicional(Adi" + a + ", checkA" + a + ", TdAdi"+a+")' />" + "</td>" +
                    "</tr>"
                    ).appendTo("#" + tabla.id + " tbody")
                        var monto = SumaAporteAdicional(tabla);
                        AcreditacionAporteAdicional(monto)
                        document.getElementById('idTotalAporteAdicional').value = monto.toString().replace(".", ",");
                        document.getElementById('totalAporteAdicional').innerHTML = ":" + monto.toString().replace(".", ",");
                        Fecha.value = "";
                        Monto.value = "";
                        $('#' + Institucion.id).val('').trigger('chosen:updated');
                    }
                    else if (tabla.id == "tablaLlamado2" && data.nombreInstitucion != "ERROR") {
                        $(
                         "<tr id='tercero" + a + "'>" +
                         "<input name='listAporteTercero[" + a + "].totalMontoUF' type='hidden' value='" + item.totalMontoUF.toString().replace(".", ",") + "'>" + // id='"+ "montoTer"+a+"'
                         "<input name='listAporteTercero[" + a + "].nombreInstitucion' type='hidden' value='" + item.nombreInstitucion + "'>" +
                         "<input name='listAporteTercero[" + a + "].idInstitucion' type='hidden' value='" + item.idInstitucion + "'>" +
                         "<input name='listAporteTercero[" + a + "].fechaAporte' type='hidden' value='" + date + "'> " +
                          "<input name='listAporteTercero[" + a + "].checkEstado' type='hidden' value='true' id='checkT" + a + "'> " +
                         "<td>" + item.totalMontoUF.toString().replace(".", ",") + " </td>" +
                        "<td>" + item.nombreInstitucion + "</td>" +
                        "<td>" + date + "</td>" +
                        "<td>" + "<input type='button' value='Eliminar' class='btn btn-block' data-toggle='modal' data-target='#deleteTercero' onclick='DeleteTercero(tercero" + a + ", checkT" + a + ")'/></td></tr>"

                        ).appendTo("#" + tabla.id + " tbody")
                        var monto = SumaAporteAdicional(tabla);
                        AcreditacionAporteAdicional(monto);
                        document.getElementById('idTotalAporteTercero').value = monto.toString().replace(".", ",");;
                        document.getElementById('totalAporteTercero').innerHTML = ":" + monto.toString().replace(".", ",");
                        Fecha.value = "";
                        Monto.value = "";
                        $('#' + Institucion.id).val('').trigger('chosen:updated');
                    }
                });

            }
        )

}

function ConfirmarCancelar()
{
    var consulta = confirm('¿Esta seguro que desea cancelar?');
    if (consulta)
    {
        alert(" ");
        
    }
    else { alert(" ")}
    
}

function getListadoRepresentante(Rut, Cargo, tabla) {
    var valor = Cargo.options[Cargo.selectedIndex].text;//selecciona el cargo
    var ru_ = Rut.value;
    ru_ = ru_.replace(".", "");
    ru_ = ru_.replace(".", "");
    var a = tabla.rows.length - 1;
    var tdCoo = false;
    var tdJur = false;
    if (tabla.id == "tablaRepresentanteJu")
    {
        $(".tdInt").each(function () {
            var _rut = $(this).html();
            _rut = _rut.replace(" ", "");
            if (ru_.toString() == _rut.toString()) {
                tdJur = true;
            }
        })
    }   
    if (tabla.id == "tablaRepresentanteCo")
    {
        $(".tdInt").each(function () {
            var _rut = $(this).html();
            _rut = _rut.replace(" ", "");
            if (ru_.toString() == _rut.toString()) {
                tdCoo = true;
            }
        })
    }
        $.post('/Common/ListadoRepresentante',
        { rut: ru_, indiceCargo: Cargo.value, nombreCargo: valor },
        function (data) {
            $.each(data, function (i, item) {

                if (item.type != 1) {
                    if(item.wbs_r != 1){
                        if (item.estaFallecido == false && item.esMayorEdad) {
                            if (tabla.id == "tablaRepresentanteJu") {
                                if(tdJur == false)
                                {
                                    $(
                                       "<tr id='AnteJur" + a + "' >" +
                                           "<input name='listRepresentanteGrupo[" + a + "].rut_' type='hidden' value='" + item.rut_ + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].nombre_cargo' type='hidden' value='" + item.nombre_cargo + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].id_cargo' type='hidden' value='" + item.id_cargo + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].ApellidoPaterno' type='hidden' value='" + item.ApellidoPaterno + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].ApellidoMaterno' type='hidden' value='" + item.ApellidoMaterno + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].Nombres' type='hidden' value='" + item.Nombres + "'>" +
                                           "<input name='listRepresentanteGrupo[" + a + "].checkEstado' type='hidden' value='true' id='check" + a + "'>" +
                                           "<td class='tdInt' id=tdInt"+a+">" + item.rut_ + " </td>" +
                                           "<td>" + item.nombre_cargo + " </td>" +
                                           "<td>" + item.ApellidoPaterno + "</td>" +
                                           "<td>" + item.ApellidoMaterno + "</td>" +
                                           "<td>" + item.Nombres + "</td>" +
                                           "<td>" + "<input type='button' value='Eliminar' class='btn btn-block' data-toggle='modal' data-target='#deleteModal' data-dismiss='modal' onclick='DeleteRepresentante(AnteJur" + a + ", check" + a + ", tdInt" + a + ")'/></td>" +
                                       "</tr>"
                                       ).appendTo("#" + tabla.id + " tbody")

                                    $('#modalCargando').modal('hide')
                                    document.getElementById('_txtRut').value = ""

                                    $('#ddlTipoCargo').val('').trigger('chosen:updated');

                                    $("#btnAgregar").attr('disabled', true);
                                    //document.getElementById('ddlTipoCargo').value=""
                                }
                                else {
                                    $('#errorModalJur').find('.modal-body').text('El RUT ya ha sido ingresado.')
                                    $('#errorModalJur').modal('show');
                                    $('#modalCargando').modal('hide');
                                }

                            }
                            else if (tabla.id == "tablaRepresentanteCo") {
                                if(tdCoo == false)
                                {
                                    $(
                               "<tr id='AnteCo" + a + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].rut_' type='hidden' value='" + item.rut_ + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].nombre_cargo' type='hidden' value='" + item.nombre_cargo + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].id_cargo' type='hidden' value='" + item.id_cargo + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].ApellidoPaterno' type='hidden' value='" + item.ApellidoPaterno + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].ApellidoMaterno' type='hidden' value='" + item.ApellidoMaterno + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].Nombres' type='hidden' value='" + item.Nombres + "'>" +
                                   "<input name='listRepresentanteGrupo[" + a + "].checkEstado' type='hidden' value='true' id='check" + a + "'>" +
                               "<td class='tdInt' id=tdInt" + a + ">" + item.rut_ + " </td>" +
                               "<td>" + item.nombre_cargo + " </td>" +
                               "<td>" + item.ApellidoPaterno + "</td>" +
                               "<td>" + item.ApellidoMaterno + "</td>" +
                               "<td>" + item.Nombres + "</td>" +
                               "<td>" + "<input type='button' value='Eliminar' class='btn btn-block' data-toggle='modal' data-target='#deleteModal' data-dismiss='modal' onclick='DeleteRepresentante(AnteCo" + a + ", check" + a + ", tdInt" + a + ")'/></td>" +
                               "</tr>"
                               //EliminarRepresentante
                               ).appendTo("#" + tabla.id + " tbody")

                                    $('#modalCargando').modal('hide');

                                    $('#modalCargando').modal('hide')
                                    document.getElementById('_txtRut').value = ""

                                    $('#ddlTipoCargo').val('').trigger('chosen:updated');
                                    $("#btnAgregar").attr('disabled', true);
                                }
                                else
                                {
                                    $('#errorModalCoop').find('.modal-body').text('El RUT ya ha sido ingresado.')
                                    $('#errorModalCoop').modal('show')
                                    $('#modalCargando').modal('hide')
                                }

                            }
                        } else {
                            if (item.estaFallecido) {
                                if (tabla.id == 'tablaRepresentanteJu') {
                                    $('#errorModalJur').find('.modal-body').text('El RUT ingresado corresponde a una persona fallecida.')
                                    $('#errorModalJur').modal('show')
                                    $('#modalCargando').modal('hide')
                                } else {
                                    $('#errorModalCoop').find('.modal-body').text('El RUT ingresado corresponde a una persona fallecida.')
                                    $('#errorModalCoop').modal('show')
                                    $('#modalCargando').modal('hide')
                                }

                            }
                            if (item.esMayorEdad == false) {
                                if (tabla.id == 'tablaRepresentanteJu') {
                                    $('#errorModalJur').find('.modal-body').text('El RUT ingresado corresponde a una persona menor de edad.')
                                    $('#errorModalJur').modal('show')
                                    $('#modalCargando').modal('hide')
                                } else {
                                    $('#errorModalCoop').find('.modal-body').text('El RUT ingresado corresponde a una persona menor de edad.')
                                    $('#errorModalCoop').modal('show')
                                    $('#modalCargando').modal('hide')
                                }
                            }
                            if (tdJur == true) {
                                if (tabla.id == 'tablaRepresentanteJu') {
                                    $('#errorModalJur').find('.modal-body').text('El RUT ya ha sido ingresado.')
                                    $('#errorModalJur').modal('show')
                                    $('#modalCargando').modal('hide')
                                }
                            }
                            if (tdCoo == true) {
                                if (tabla.id == 'tablaRepresentanteCo') {
                                    $('#errorModalCoop').find('.modal-body').text('El RUT ya ha sido ingresado.')
                                    $('#errorModalCoop').modal('show')
                                    $('#modalCargando').modal('hide')
                                }
                            }
                        }
                    } else {
                        if (tabla.id == 'tablaRepresentanteJu') {
                            $('#errorModalJur').find('.modal-body').text('El RUT ingresado no se encuentra en el registro civil.')
                            $('#errorModalJur').modal('show')
                            $('#modalCargando').modal('hide')
                        } else {
                            $('#errorModalCoop').find('.modal-body').text('El RUT ingresado no se encuentra en el registro civil.')
                            $('#errorModalCoop').modal('show')
                            $('#modalCargando').modal('hide')
                        }
                    }
                }
                else {
                    if (tabla.id == 'tablaRepresentanteJu') {
                        $('#errorModalJur').find('.modal-body').text('El RUT ingresado no es válido.')
                        $('#errorModalJur').modal('show')
                        $('#modalCargando').modal('hide')
                    } else {
                        $('#errorModalCoop').find('.modal-body').text('El RUT ingresado no es válido.')
                        $('#errorModalCoop').modal('show')
                        $('#modalCargando').modal('hide')
                    }
                }
            });


        }
    )

   
}

function EliminarRepresentante(idTr, idChck,idTd)
{
    $('#' + idTr.value).hide();
    $('#' + idTd.value).removeClass("tdInt");
    document.getElementById(idChck.value).value = false;
    $("#deleteModal").modal('toggle');
    
}

function EliminarIntegrante(idTr, idChck, idTd, idGrupo, rut) {
    var idGrupo_ = idGrupo.value;
    var rut_ = rut.value;

    $.post('/Common/verificaEliminacion',
        { rut: rut_, idGrupo: idGrupo_ },
        function (data) {
            $.each(data, function (i, item) {
                if (item.type == 1) {
                    $('#errorModal').find('.modal-body').text('El integrante no se puede eliminar, debido a que se encuentra con postulación vigente.')
                    $('#errorModal').modal('show')
                    $('#modalCargando').modal('hide')
                    $('#deleteModal').modal('toggle')
                }
                else if (item.type == 2) {
                    $('#errorModal').find('.modal-body').text('El integrante no se puede eliminar, debido a que tiene un beneficio vigente no pagado.')
                    $('#errorModal').modal('show')
                    $('#modalCargando').modal('hide')
                    $('#deleteModal').modal('toggle')
                }
                else if (item.type == 0) {
                    //se puede eliminar 
                    $('#' + idTr.value).hide();
                    $('#' + idTd.value).removeClass("tdInt");
                    document.getElementById(idChck.value).value = false;
                    $("#deleteModal").modal('toggle');
                }
            });
        }
    )

    
    

}

function getListadoIntegrante(Rut, tabla) {
    var ru_ = Rut.value;
    ru_ = ru_.replace(".", "");
    ru_ = ru_.replace(".", "");
    var a = tabla.rows.length - 1;
    var j = tabla.rows.length;

    var tdInt = false;

         $(".tdInt").each(function () {
            var _rut = $(this).html();
            _rut = _rut.replace(" ", "");
            if (ru_.toString() == _rut.toString()) {
                tdInt = true;
            }
        })

    $.post('/Common/ListadoIntegrante',
            { rut: ru_ },
            function (data) {
                $.each(data, function (i, item) {
                    if (item.type != 1)
                    {
                        if (item.wbs_r != 1) {
                            if (item.estaFallecido == false && item.esMayorEdad && item.pertenece_a_otro_grupo == false && tdInt == false) {
                                if (tabla.id == "tablaIntegrantes") {
                                    $(
                                    "<tr id='Integrante" + a + "' >" +
                                    "<td>" + j + "</td>" +
                                    "<input name='listIntegranteGrupo[" + a + "].rut_' type='hidden' value='" + item.rut_ + "' id='rut_"+ a +"'>" +
                                    "<input name='listIntegranteGrupo[" + a + "].ApellidoPaterno' type='hidden' value='" + item.ApellidoPaterno + "'>" +
                                    "<input name='listIntegranteGrupo[" + a + "].ApellidoMaterno' type='hidden' value='" + item.ApellidoMaterno + "'>" +
                                    "<input name='listIntegranteGrupo[" + a + "].Nombres' type='hidden' value='" + item.Nombres + "'>" +
                                    "<input name='listIntegranteGrupo[" + a + "].checkEstado' type='hidden' value='true' id='check" + a + "'>" +
                                    "<td class='tdInt' id=tdInt" + a + ">" + item.rut_ + " </td>" +
                                    "<td>" + item.ApellidoPaterno + "</td>" +
                                    "<td>" + item.ApellidoMaterno + "</td>" +
                                    "<td>" + item.Nombres + "</td>" +
                                    "<td>" + "<input type='button' value='Eliminar' class='btn btn-block'  data-toggle='modal' data-target='#deleteModal' data-dismiss='modal' onclick='Delete(Integrante" + a + ", check" + a + ", tdInt" + a + ", rut_" + a + ", idgrupo)'/></td>" +
                                    "</tr>"
                                    ).appendTo("#" + tabla.id + " tbody")

                                    $('#modalCargando').modal('hide')

                                    document.getElementById('_txtRut').value = ""
                                    $("#btnAgregar").attr('disabled', true);
                                }
                            } else {
                                if (tdInt == true) {
                                    $('#errorModal').find('.modal-body').text('El RUT ya ha sido ingresado.')
                                    $('#errorModal').modal('show')
                                    $('#modalCargando').modal('hide')
                                } else
                                    if (item.estaFallecido) {
                                        $('#errorModal').find('.modal-body').text('El RUT ingresado corresponde a una persona fallecida.')
                                        $('#errorModal').modal('show')
                                        $('#modalCargando').modal('hide')
                                    } else
                                        if (item.esMayorEdad == false) {
                                            $('#errorModal').find('.modal-body').text('El RUT ingresado corresponde a una persona menor de edad.')
                                            $('#errorModal').modal('show')
                                            $('#modalCargando').modal('hide')
                                        } else
                                            if (item.pertenece_a_otro_grupo) {
                                                $('#errorModal').find('.modal-body').text('El RUT ingresado corresponde a un integrante de otro grupo.')
                                                $('#errorModal').modal('show')
                                                $('#modalCargando').modal('hide')
                                            }
                            }
                        } else {
                            if (item.wbs_r == 1) {
                                $('#errorModal').find('.modal-body').text('El RUT ingresado no se encuentra en el registro civil.')
                                $('#errorModal').modal('show')
                                $('#modalCargando').modal('hide')
                            }
                        }
                    } else {
                        if (tabla.id == 'tablaIntegrantes') {
                            $('#errorModal').find('.modal-body').text('El RUT ingresado no es válido.')
                            $('#errorModal').modal('show')
                            $('#modalCargando').modal('hide')
                        }
                    }
                });
            }
        )

}

function Habilitar(Rut, Cargo) {
    if (Rut.value != "" && Cargo.value != "") {
        $("#btnAgregar").attr('disabled', false);
    }
    else {
        $("#btnAgregar").attr('disabled', true);
    }
}

function cargarCapitulosPostulacionPersona(llamado, idControl, proyecto, obras) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    $('#' + proyecto.id).find('option').remove().end();
    $('#' + proyecto.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    $('#' + obras.id).find('option').remove().end();
    $('#' + obras.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    if (llamado.value != null) {
        $.post('/Common/obtenerCapitulosPostulacion'
        , { idLlamado: llamado.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function cargarProyectosCapitulosPersona(capitulo, idControl, obras) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    $('#' + obras.id).find('option').remove().end();
    $('#' + obras.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    if (capitulo.value != null) {
        $.post('/Common/obtenerProyectosCapitulos'
        , { idOferta: capitulo.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}

function cargarObrasProyectosPersona(proyecto, idControl) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append($('<option>', {
        value: "",
        text: '-- Seleccione --'
    }));

    if (proyecto.value != null) {
        $.post('/Common/obtenerObrasProyectos'
        , { idProyecto: proyecto.value }
        , function (data) {
            $.each(data, function (i, item) {
                $('#' + idControl.id).append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        });
    }
}


function DeleteAdicional(idTr, idChck, idTdAdi) {
    document.getElementById("hiddenTr1").value = idTr.id;
    document.getElementById("hiddenCheck1").value = idChck.id;
    document.getElementById("hiddenTdMonto").value = idTdAdi.id;
}

function SumarIncrementos(idHidden, idDisplay, txtMonto )
{
    cortadecimales(txtMonto);
    var total = 0;
    $(".Incrementos").each(function () {
        var aux = 0;
        aux = $(this).val().toString().replace(",", ".");
        total += Number(aux);
    });

    if(total.value != "NaN")
    {
        document.getElementById(idHidden.id).value = total.toString().replace(".", ",");
        document.getElementById(idDisplay.id).innerHTML = ": " + total.toString().replace(".", ",");
    }

}

function HabilitarAcreditaciones(chck) {
    var i = chck.id.replace("checkAcre", "");
    var idIncremento = "0";

    switch (i) {
        case "271":
            idIncremento = "10";
            break;
        case "272":
            idIncremento = "11";
            break;
        case "273":
            idIncremento = "12";
            break;
        case "274":
            idIncremento = "13";
            break;
        case "286":
            idIncremento = "14";
            break;
            //case "271":
            //    idIncremento = "15";
            //    break;
        case "287":
            idIncremento = "16";
            break;
        case "288":
            idIncremento = "17";
            break;
        case "88":
            idIncremento = "-5";
            //case "273":
            //    idIncremento = "18";
            //    break;
    }

    if (idIncremento != "-5") {
        var checkIncremento = document.getElementById("checkIncr" + idIncremento);
        if (checkIncremento == null && i == "273") {
            idIncremento = "18";
            checkIncremento = document.getElementById("checkIncr" + idIncremento);
        } else if (checkIncremento == null && i == "271") {
            idIncremento = "15";
            checkIncremento = document.getElementById("checkIncr" + idIncremento);
        }

        if (checkIncremento != null) {
            if (!checkIncremento.checked) {
                var descripcionAcredita = $.trim($("#" + i).html()); //document.getElementById(idAcreditacion); //
                document.getElementById(i).innerHTML = chck.checked ? descripcionAcredita + " (***)" : descripcionAcredita.replace(" (***)", "");
                var descripcionIncremento = $.trim($("#" + idIncremento).html());
                document.getElementById(idIncremento).innerHTML = chck.checked ? descripcionIncremento + " (***)" : descripcionIncremento.replace(" (***)", "");

            }
        }
    } else {
        var campo1 = $.trim($("#idTotalMontoAporte").html()).replace(":","");
        var campo2 = $.trim($("#idInstitucionAporte").html()).replace(":", "");
        var campo3 = $.trim($("#idFechaAporte").html()).replace(":", "");
        document.getElementById("idTotalMontoAporte").innerHTML = chck.checked ? campo1 + " (*):" : campo1.replace(" (*)", ":");
        document.getElementById("idInstitucionAporte").innerHTML = chck.checked ? campo3 + " (*):" : campo3.replace(" (*)", ":");
        document.getElementById("idFechaAporte").innerHTML = chck.checked ? campo2 + " (*):" : campo2.replace(" (*)", ":");

        var descripcionAcredita = $.trim($("#displayAcrAporteAdicional").html());
        //if(descripcionAcredita.indexOf('(*)') == -1){
     //   document.getElementById("displayAcrAporteAdicional").innerHTML = chck.checked && descripcionAcredita.indexOf('(*)') == -1? descripcionAcredita + " (*)" : descripcionAcredita.replace(" (*)", "");
        //}
    }
}

function HabilitarIncremento(chck, txtIncremento)
{
    var i = chck.id.replace("checkIncr", "");
    var idAcreditacion = "0";

    switch (i) {
        case "10":
            idAcreditacion = "271";
            break;
        case "11":
            idAcreditacion = "272";
            break;
        case "12":
            idAcreditacion = "273";
            break;
        case "13":
            idAcreditacion = "274";
            break;
        case "14":
            idAcreditacion = "286";
            break;
        case "15":
            idAcreditacion = "271";
            break;
        case "16":
            idAcreditacion = "287";
            break;
        case "17":
            idAcreditacion = "288";
            break;
        case "18":
            idAcreditacion = "273";
            break;
    }
        
    var checkAcreditacion = document.getElementById("checkAcre" + idAcreditacion); //$.trim($("#checkAcre" + idAcreditacion).html());
    if(checkAcreditacion!=null){
        if (!checkAcreditacion.checked) {
            var descripcionIncremento = $.trim($("#" + i).html());
            document.getElementById(i).innerHTML = chck.checked ? descripcionIncremento + " (***)" : descripcionIncremento.replace(" (***)", "");
            var descripcionAcredita = $.trim($("#" + idAcreditacion).html()); //document.getElementById(idAcreditacion); //
            document.getElementById(idAcreditacion).innerHTML = chck.checked ? descripcionAcredita + " (***)" : descripcionAcredita.replace(" (***)", "");
        }
    }

    var res = document.getElementById(txtIncremento.id).disabled;
    if(res)
    {
        document.getElementById(txtIncremento.id).disabled = false;
    }
    else
    {
        document.getElementById(txtIncremento.id).disabled = true;
        document.getElementById(txtIncremento.id).value = '';
    }
}

function reqAporteAdicional(accion) {
    var largo = document.getElementById("tablaLlamado").rows.length; //$.trim($("#tablaLlamado").html());//.rows.length;
    var campo1 = $.trim($("#idTotalMontoAporte").html()).replace(":", "");
    var campo2 = $.trim($("#idInstitucionAporte").html()).replace(":", "");
    var campo3 = $.trim($("#idFechaAporte").html()).replace(":", "");

    var AcreditacionVigente = false;
    var checkAporte = false;
    var chck = document.getElementById("checkAcre88"); //checkAcreditacion
    if (!chck.checked) {
        var primerPosicion= $.trim($("#checkA" + 0).attr("value"));
        if (largo == 1 && primerPosicion == "") {
            AcreditacionVigente = true;
        } else {
            for (var i = 0; i < largo; i++) {
                checkAporte = $.trim($("#checkA" + i).attr("value"));
                if (accion == "agregar") {
                    if (checkAporte == "true" || checkAporte == "") {
                        AcreditacionVigente = true;
                        break;
                    }
                } else if (accion == "eliminar") {
                    if (checkAporte == "true") {
                        AcreditacionVigente = true;
                        break;
                    }
                }
            }
        }
        if (AcreditacionVigente) {
            document.getElementById("idTotalMontoAporte").innerHTML = (campo1.indexOf("(*)")==-1) ? campo1 + " (*):" : campo1 + ":";
            document.getElementById("idInstitucionAporte").innerHTML = (campo2.indexOf("(*)") == -1) ? campo2 + " (*):" : campo2 + ":";
            document.getElementById("idFechaAporte").innerHTML = (campo3.indexOf("(*)") == -1) ? campo3 + " (*):" : campo3 + ":";
        } else {
            document.getElementById("idTotalMontoAporte").innerHTML = (campo1.indexOf("(*)") != -1) ? campo1.replace(" (*)", ":") : campo1 + ":";
            document.getElementById("idInstitucionAporte").innerHTML = (campo2.indexOf("(*)") != -1) ? campo2.replace(" (*)", ":") : campo2 + ":";
            document.getElementById("idFechaAporte").innerHTML = (campo3.indexOf("(*)") != -1) ? campo3.replace(" (*)", ":") : campo3 + ":";
        }
    }
}

function VerificaRut(rut) {
    if (rut.toString().trim() != '') {

        var caracteres = new Array();
        var serie = new Array(2, 3, 4, 5, 6, 7);
        var dig = rut.toString().substr(rut.toString().length - 1, 1);
        rut = rut.toString().substr(0, rut.toString().length - 2);

        for (var i = 0; i < rut.length; i++) {
            caracteres[i] = parseInt(rut.charAt((rut.length - (i + 1))));
        }

        var sumatoria = 0;
        var k = 0;
        var resto = 0;

        for (var j = 0; j < caracteres.length; j++) {
            if (k == 6) {
                k = 0;
            }
            sumatoria += parseInt(caracteres[j]) * parseInt(serie[k]);
            k++;
        }

        resto = sumatoria % 11;
        dv = 11 - resto;

        if (dv == 10) {
            dv = "K";
        }
        else if (dv == 11) {
            dv = 0;
        }

        if (dv.toString().trim().toUpperCase() == dig.toString().trim().toUpperCase())
            return true;
        else
            return false;
    }
    else {
        return false;
    }
}

function AcreditacionAporteAdicional(valorAporte)
{
    var aux = document.getElementById('lblDescripcionAporte').value.toString();
    if(valorAporte >0)
    {
        document.getElementById('displayAcrAporteAdicional').innerHTML = aux.replace(" (*)", "") + " (*)";
        document.getElementById('lblDescripcionAporte').value = aux.replace(" (*)", "") + " (*)";
        document.getElementById('chckObligatoriedadAporte').value = true;
    }
    else
    {
        var check = document.getElementById("checkAcre88");
        if (aux.indexOf('(*)') != -1) {
            if (!check.checked) {
                aux = aux.replace('(*)', '');
                document.getElementById('displayAcrAporteAdicional').innerHTML = aux;
                document.getElementById('lblDescripcionAporte').value = aux;
            }
        }
        document.getElementById('chckObligatoriedadAporte').value = false;
    }
}

function AcreditacionViviendaPatrimonial(checkVivienda) {
    var aux = document.getElementById('lblDescripcionPatrimonial').value.toString();
    if (checkVivienda.checked) {
        //BUscar label, hidden display y que sean requeridos y con (*)
        if (aux.indexOf('(*)') == -1) {
            document.getElementById('chckObligatoriedadPatrimonial').value = true;
            document.getElementById('displayAcrPatrimonial').innerHTML = aux + " (*)";
            document.getElementById('lblDescripcionPatrimonial').value = aux + " (*)";
        }
        else {
            document.getElementById('chckObligatoriedadPatrimonial').value = true;
        }
    }
    else {
        //quitarles los asteriscos y requeridos :)
        if (aux.indexOf('(*)') != -1) {
            aux = aux.replace('(*)', '');
            document.getElementById('chckObligatoriedadPatrimonial').value = false;
            document.getElementById('displayAcrPatrimonial').innerHTML = aux;
            document.getElementById('lblDescripcionPatrimonial').value = aux;
        }
        else {
            if (document.getElementById('displayAcrPatrimonial').innerHTML.indexOf('(*)') != -1) {
                document.getElementById('displayAcrPatrimonial').innerHTML = document.getElementById('displayAcrPatrimonial').innerHTML.replace('(*)', '');

            }
            document.getElementById('chckObligatoriedadPatrimonial').value = false;
        }
    }
}

function CheckBancoOnline(labelVb, ddlBanco)
{
 //   var idBanco = ddlBanco.value;
    $.post('/Common/verificaBanco'
         , { idBanco: ddlBanco.value }
         , function (data) {
             if (data == "True") {
                 labelVb.innerHTML = "VB Entidad Financiera no en línea"
             }
             else {

                 labelVb.innerHTML = "VB Entidad Financiera no en línea (*)"
             }
         }
     )
}

function AcreditacionAporteAdicionalReq(AporteAdicional,check)
{
    var valor = parseFloat(AporteAdicional.value == "" ? 0 : AporteAdicional.value);
    var campo1 = $.trim($("#idTotalMontoAporte").html()).replace(":", "");
    var campo2 = $.trim($("#idInstitucionAporte").html()).replace(":", "");
    var campo3 = $.trim($("#idFechaAporte").html()).replace(":", "");
    if(check.checked)
    {
        document.getElementById("idTotalMontoAporte").innerHTML = campo1.indexOf('(*)') == -1 ? campo1 + " (*)" : campo1;
        document.getElementById("idInstitucionAporte").innerHTML = campo2.indexOf('(*)') == -1 ? campo2 + " (*)" : campo2;
        document.getElementById("idFechaAporte").innerHTML = campo3.indexOf('(*)') == -1 ? campo3 + " (*)" : campo3;
    }
     else
    {
        if(valor >0)
        {

            document.getElementById("idTotalMontoAporte").innerHTML = campo1.indexOf('(*)') == -1 ? campo1 + " (*)" : campo1;
            document.getElementById("idInstitucionAporte").innerHTML = campo2.indexOf('(*)') == -1 ? campo2 + " (*)" : campo2;
            document.getElementById("idFechaAporte").innerHTML = campo3.indexOf('(*)') == -1 ? campo3 + " (*)" : campo3;
        }
        else
        {
            document.getElementById("idTotalMontoAporte").innerHTML = campo1.indexOf('(*)') != -1 ? campo1.replace("(*)", "") : campo1;
            document.getElementById("idInstitucionAporte").innerHTML = campo2.indexOf('(*)') != -1 ? campo2.replace("(*)", "") : campo2;
            document.getElementById("idFechaAporte").innerHTML = campo3.indexOf('(*)') != -1 ? campo3.replace("(*)", "") : campo3;
        }
    }
    //if(descripcionAcredita.indexOf('(*)') == -1){
    //   document.getElementById("displayAcrAporteAdicional").innerHTML = chck.checked && descripcionAcredita.indexOf('(*)') == -1? descripcionAcredita + " (*)" : descripcionAcredita.replace(" (*)", "");
    //}
}

function LimpiarLabelBanco()
{
    document.getElementById("lblMontoAhorroAcreditado").innerHTML = "";
    document.getElementById("lblEstadoCuenta").innerHTML = ":";
    document.getElementById("AhorroAcreditado").value = "";
    document.getElementById("estadoCuenta").value = "";

}

function Limpia_NA_ahorro(campo) {
    document.getElementById("lblMontoAhorroAcreditado").innerHTML = ": N/A";
    document.getElementById("lblEstadoCuenta").innerHTML = ": N/A";
    document.getElementById("AhorroAcreditado").value = "";
    document.getElementById("estadoCuenta").value = "";
}

function SuelosSalinosAcreditacion(region)
{
    var idRegion = region.value;
    if (idRegion == 15 || idRegion == 1 || idRegion ==2)
    {
        $("#checkAcre272").attr('disabled', false);
        $("#checkIncr11").attr("disabled", false);

        $("#checkAcre272").prop("checked", false);
        $("#checkIncr11").prop("checked", false);

    }
    else
    {
        $("#checkAcre272").attr('disabled', true);
        $("#checkIncr11").attr("disabled", true);
        $("#Incr11").attr('disabled', true);

        $("#checkAcre272").prop("checked", false);
        $("#checkIncr11").prop("checked", false);
 

        var descIncremento = $.trim($("#11").html());
        var descAcr = $.trim($("#272").html());
        if(descIncremento.indexOf('(***)') != -1)
        {
            document.getElementById("11").innerHTML = descIncremento.replace("(***)", "");
        }
        if (descAcr.indexOf('(***)') != -1) {
            document.getElementById("272").innerHTML = descAcr.replace("(***)", "");
        }

        if( $("#Incr11").val().length>0)
        {
            var auxMonto = $("#Incr11").val();
            var auxTotal = $("#idTotalIncrementos").val();
            var auxResta = parseFloat(auxTotal) - parseFloat(auxMonto);
            $("#idTotalIncrementos").val(auxResta);
            document.getElementById("lblTotalIncrementos").innerHTML = ": "+ auxResta;
        }

        $("#Incr11").val('');
        //idTotalIncrementos  = hidden
        //lblTotalIncrementos = div
    }
}

function Acreditacion_AporteAdicional_requerido(totalAporte, check) {
    var valor = parseFloat(totalAporte.value == "" ? 0 : totalAporte.value);
    var campo1 = $.trim($("#displayAcrAporteAdicional").html()).replace(":", "");

    if (valor == 0) {
        document.getElementById("displayAcrAporteAdicional").innerHTML = campo1.indexOf('(*)') == -1 ? (check.checked ? campo1 + " (*)" : campo1.replace(" (*)", "")) : campo1.replace(" (*)", "");
        document.getElementById("lblDescripcionAporte").innerHTML = campo1.indexOf('(*)') == -1 ? (check.checked ? campo1 + " (*)" : campo1.replace(" (*)", "")) : campo1.replace(" (*)", "");
    }


}