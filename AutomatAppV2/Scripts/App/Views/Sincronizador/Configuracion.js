

//$(function () {
//    'use strict';

//const { stringify } = require("querystring");

$(document).ready(function () {

    CargaConfiguraciones();


    $("#btnGuardar").click(function () {
        swal({
            title: '¿Desea aplicar la configuración?',
            buttons: ["Cancelar", "Aceptar"],
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */

            if (result) {
                Guardar();
            }
        });

    });

    $('#cbCadenas').multiselect({
        nonSelectedText: 'Lista de Cadenas',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todas",
        allSelectedText: "Todas Seleccionadas",
        nSelectedText: "Cadenas Seleccionadas",
        onDropdownHidden: function (event) {

            LimpiarMultiSelect($("#cbSucursales"));

            if (String($("#cbCadenas").val()) != '') {

                GetComboDataByFlag("cbSucursales", 2, String($("#cbCadenas").val()));
            }

        }
    });
    $('#ddlTablasNo').multiselect({
        nonSelectedText: 'Lista de tablas para agregar',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todas",
        allSelectedText: "Todas Seleccionadas",
        nSelectedText: "Tablas Seleccionadas"
    });
    $('#ddlTablasSi').multiselect({
        nonSelectedText: 'Lista de tablas agregadas',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todas",
        allSelectedText: "Todas Seleccionadas",
        nSelectedText: "Tablas Seleccionadas"
    });

    $('#cbSucursales').multiselect({
        nonSelectedText: 'Lista de Sucursales',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todas",
        allSelectedText: "Todas Seleccionados",
        nSelectedText: "Sucursales Seleccionadas"
    });

    $('#cbGrupos').multiselect({
        nonSelectedText: 'Grupos de Consulta',
        enableFiltering: true,
        includeSelectAllOption: true,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todos",
        allSelectedText: "Todos Seleccionados",
        nSelectedText: "Grupos Seleccionados"
    });

    $('#cbProceso').multiselect({
        nonSelectedText: 'Horas de Consulta',
        enableFiltering: false,
        includeSelectAllOption: false,
        maxHeight: 400,
        buttonWidth: '250px',
        buttonTextAlignment: 'left',
        selectAllText: "Marcar Todas",
        allSelectedText: "Todas Seleccionadas",
        nSelectedText: "Horas seleccionadas",
        onChange: function (option, checked) {
            return;
            // Get selected options.
            var selectedOptions = $('#cbProceso option:selected');

            if (selectedOptions.length >= 5) {
                // Disable all other checkboxes.
                var nonSelectedOptions = $('#cbProceso option').filter(function () {
                    return !$(this).is(':selected');
                });

                nonSelectedOptions.each(function () {
                    var input = $('input[value="' + $(this).val() + '"]');
                    input.prop('disabled', true);
                    input.closest('.multiselect-option').addClass('disabled');
                });
            }
            else {
                // Enable all checkboxes.
                $('#cbProceso option').each(function () {
                    var input = $('input[value="' + $(this).val() + '"]');
                    input.prop('disabled', false);
                    input.closest('.multiselect-option').removeClass('disabled');
                });
            }
        }
    });
});

GetComboDataByFlag("cbCadenas", 1, "");
GetComboDataByFlag("cbGrupos", 3, "");



//$('#cbCadenas').onDropdownHide(function () {


//    var optionSelected = $(this).find("option:selected");

//    GetComboDataByFlag("cbSucursales", 1, optionSelected);

//});


function LimpiarMultiSelect(obj) {
    $(obj).find("option").remove();
    $(obj).multiselect('rebuild');
}

function fnCancelar() {
    if (String($("#cbCadenas").val()) == "") {
        swal("Seleccione una cadena", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbSucursales").val()) == "") {
        swal("Seleccione una sucursal", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbSucursales").val()).split(",").length > 1) {
        swal("Seleccione solamente una sucursal", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbGrupos").val()) == "") {
        swal("Seleccione la sincronización", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbGrupos").val()).split(",").length > 1) {
        swal("Seleccione solamente un grupo", '', "error").then((value) => { });
        return;
    }
    showLoading();

    $.ajax({

        url: "/Sincronizador/CancelaConfiguracion",
        type: "POST",
        dataType: "json",
        data: { tiendas: $("#cbSucursales").val(), grupos: $("#cbGrupos").val() },
        success: function (response) {
            if (response.Result) {
                if (response.Result[0].Error < 0)
                    swal(response.Result[0].Descripcion, '', "error").then((value) => { });
                else
                    swal(response.Result[0].Descripcion, '').then((value) => { window.top.location.reload(); });
            }
            else
                swal('Ocurrio un error', '', "error").then((value) => { });

            hideLoading();
        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });

}
function GuardarGrupo() {

    if (String($("#txtGrupo").val()) == "") {
        swal("Ingrese un nombre para el grupo", '', "error").then((value) => { });
        return;
    }
    if (String($("#txtDescripcionGrupo").val()) == "") {
        swal("Ingrese una descripción para el grupo", '', "error").then((value) => { });
        return;
    }

    showLoading();
    var TablasAgregar = new Array();
    $.each($("#ddlTablasSi").find("option"), function () {
        TablasAgregar.push($(this).val());
    });

    $.ajax({

        url: "/Sincronizador/GuardaGrupo",
        type: "POST",
        dataType: "json",
        data: {
            IdGrupo: $("#ddlGrupos").val() == "" || $("#ddlGrupos").val() == null ? "0" : $("#ddlGrupos").val(),
            Grupo: $("#txtGrupo").val(),
            Activo: $("#chkGrupoActivo").is(":checked"),
            Descripcion: $("#txtDescripcionGrupo").val(),
            Tablas: TablasAgregar

        },
        success: function (response) {
            if (response.Result) {
                if (response.Result[0].Error < 0)
                    swal(response.Result[0].Descripcion, '', "error").then((value) => { });
                else
                    swal(response.Result[0].Descripcion, '').then((value) => {
                        fnCargarGruposEdit();
                        GetComboDataByFlag("cbGrupos", 3, "");
                    });
            }
            else
                swal('Ocurrio un error', '', "error").then((value) => { });

            hideLoading();
        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });
}
function Guardar() {

    if (String($("#cbCadenas").val()) == "") {
        swal("Seleccione una cadena", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbSucursales").val()) == "") {
        swal("Seleccione una sucursal", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbGrupos").val()) == "") {
        swal("Seleccione la sincronización", '', "error").then((value) => { });
        return;
    }
    if (String($("#cbProceso").val()) == "") {
        swal("Seleccione una proceso", '', "error").then((value) => { });
        return;
    }

    showLoading();

    $.ajax({

        url: "/Sincronizador/GuardaConfiguracion",
        type: "POST",
        dataType: "json",
        data: { tiendas: $("#cbSucursales").val(), grupos: $("#cbGrupos").val(), hora: $("#cbProceso").val(), reintentos: $("#cbReintentos").val(), intervalo: $("#cbIntervalos").val() },
        success: function (response) {
            if (response.Result) {
                if (response.Result[0].Error < 0)
                    swal(response.Result[0].Descripcion, '', "error").then((value) => { });
                else
                    swal(response.Result[0].Descripcion, '').then((value) => { window.top.location.reload(); });
            }
            else
                swal('Ocurrio un error', '', "error").then((value) => { });

            hideLoading();
        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });
}
function fnCargarGruposEdit() {

    showLoading();
    $("#btnNuevoGrupo").click();
    $("#ddlGrupos").find("option").remove();
    $("#ddlTablasNo").find("option").remove()
    $("#ddlTablasSi").find("option").remove()
    $.ajax({

        url: "/Sincronizador/GetComboDataByFlag",
        type: "POST",
        dataType: "json",
        data: { flag: 5, filter1: "" },
        success: function (response) {
            if (response.Success) {


                $("#ddlGrupos").append($("<option value='0'>Seleccione</option>"));
                if (response.Result != null) {

                    for (var i = 0; i < response.Result.length; i++) {

                        //tablasNo = ' + JSON.stringify(response.Result[i].Op1) + ' tablasSi = ' + JSON.stringify(response.Result[i].Op2) + '
                        var op = $('<option value=' + response.Result[i].Id + ' desc="' + response.Result[i].Descripcion + '" >' + response.Result[i].Valor + '</option>');
                        $(op).attr("data-activo", response.Result[i].Activo);
                        $(op).attr("data-tablasno", response.Result[i].Op1);
                        $(op).attr("data-tablassi", response.Result[i].Op2);
                        $("#ddlGrupos").append($(op));
                    }
                }





            } else {

                swal(response.Message, '', "error").then((value) => { });

            }
            hideLoading();

        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });
}

function GetComboDataByFlag(component, flag, filter1) {


    var idComponent = "#" + component;

    $(idComponent).empty();

    showLoading();

    $.ajax({

        url: "/Sincronizador/GetComboDataByFlag",
        type: "POST",
        dataType: "json",
        data: { flag: flag, filter1: filter1 },
        success: function (response) {



            if (response.Success) {

                var sucursales = "";

                console.log(response)

                if (response.Result != null) {

                    for (var i = 0; i < response.Result.length; i++) {
                        sucursales = sucursales + '<option value=' + response.Result[i].Id + '>' + response.Result[i].Valor + '</option>';
                    }
                }

                $(idComponent).append(sucursales);

                $(idComponent).multiselect('rebuild');

                if (flag == 2 && TiendaSelected) {

                    AsignaCombo($("#cbSucursales"), TiendaSelected);
                    TiendaSelected = null;
                }

            } else {

                swal(response.Message, '', "error").then((value) => { });

            }
            hideLoading();

        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });
}
var TiendaSelected = null;
function CargaDatos(ID_Cadena, Tienda, Intervalo_Reintento, Horas_Ejecucion, Reintentos, GruposId) {

    //console.log(ID_Cadena, Tienda, Intervalo_Reintento, Horas_Ejecucion, Reintentos, GruposId);
    AsignaCombo($("#cbCadenas"), ID_Cadena);
    TiendaSelected = Tienda
    LimpiarMultiSelect($("#cbSucursales"));
    if (String($("#cbCadenas").val()) != '')
        GetComboDataByFlag("cbSucursales", 2, String($("#cbCadenas").val()));
    AsignaCombo($("#cbGrupos"), GruposId);
    AsignaCombo($("#cbProceso"), Horas_Ejecucion);
    $("#cbReintentos").val(Reintentos);
    $("#cbIntervalos").val(Intervalo_Reintento);
    $("#lnkConfigurar").click();
    $("#btnGuardar").focus();
}
function fnNuevo() {
    CargaDatos("", "", "5", "", "1", "");
}
function AsignaCombo(combo, data) {
    var dataarray = data.split(",");
    $(combo).val(dataarray);
    $(combo).multiselect("refresh");
}

function CargaConfiguraciones() {

    showLoading();
    $("#tbConfiguracion").dataTable();
    $.ajax({

        url: "/Sincronizador/GetConfiguraciones",
        type: "POST",
        dataType: "json",
        data: {},
        success: function (response) {

            var table = $("#tbConfiguracion").dataTable();
            table.fnDestroy()
            table.fnClearTable();
            table.fnDraw();

            var table = $("#tbConfiguracion").dataTable({
                //"ordering": false,
                responsive: false,
                //"columnDefs": [
                //    { "orderable": false, "targets": [0, 1, 2, 3, 4, 5] },
                //    { "visible": false, "targets": 0 }
                //    /*  { "orderable": true, "targets": [1, 2, 3] }*/
                //],
                columns: [
                    { "data": "Cadena_Descripcion" },
                    { "data": "TiendaDescripcion" },
                    { "data": "Grupos" },
                    { "data": "Horas_Ejecucion" },
                    { "data": "Reintentos" },
                    { "data": "Intervalo_Reintento" },
                    { "data": "Estatus" },
                    {
                        "data": null, render: function (a, b, c) {
                            var span = "<span style='cursor:pointer' class='fa fa-edit' onclick=\"CargaDatos('" + a.ID_Cadena + "','" + a.ClaveTienda + "','" + a.Intervalo_Reintento + "','" + a.Horas_Ejecucion + "','" + a.Reintentos + "','" + a.GruposId + "')\"></span>";

                            return span;
                        }
                    }
                ],
                data: response,
                language: {
                    searchPlaceholder: 'Buscar',
                    url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                }
            });


            hideLoading();

        },
        error: function (response) {
            console.log(response)
            //swal("Error al cargar las configuraciones", '', "error").then((value) => { });

        }
    });
}

function fnAgregarTablas() {

    if ($("#ddlTablasNo").find("option:selected").length) {
        $.each($("#ddlTablasNo").find("option:selected"), function () {
            var op = this;
            $(op).remove();
            var opNew = $('<option value=' + $(op).val() + ' desc="' + $(op).attr("desc") + '" >' + $(op).text() + '</option>');
            $("#ddlTablasSi").append($(opNew));
        });
        $("#ddlTablasSi").multiselect('rebuild');
        $("#ddlTablasNo").multiselect('rebuild');
    }
    else {
        swal("No ha seleccionado ninguna tabla", '', "error").then((value) => { });
        return;
    }

}
function fnQuitarTablas() {
    if ($("#ddlTablasSi").find("option:selected").length) {
        $.each($("#ddlTablasSi").find("option:selected"), function () {
            var op = this;
            $(op).remove();
            var opNew = $('<option value=' + $(op).val() + ' desc="' + $(op).attr("desc") + '" >' + $(op).text() + '</option>');
            $("#ddlTablasNo").append($(opNew));
        });
        $("#ddlTablasSi").multiselect('rebuild');
        $("#ddlTablasNo").multiselect('rebuild');
    }
    else {
        swal("No ha seleccionado ninguna tabla", '', "error").then((value) => { });
        return;
    }

}
function fnCargaGrupo(obj) {
    if ($(obj).val() == "0") {
        $("#txtGrupo").val("");
        $("#txtDescripcionGrupo").val("");

    }
    $("#ddlTablasNo").find("option").remove();
    $("#ddlTablasSi").find("option").remove();
    $("#ddlTablasNo").empty();
    $("#ddlTablasSi").empty();

    $("#txtGrupo").val($(obj).find("option:selected").text());

    if ($("#chkGrupoActivo").is(":checked")) {
        $("#chkGrupoActivo").click();
    }
 

    if ($(obj).find("option:selected").attr("data-activo") == "true") {
 
        $("#chkGrupoActivo").click();
    }

    $("#txtDescripcionGrupo").val($(obj).find("option:selected").attr("desc"));

    var tablasSi = new Array();

    var tablasNo = new Array();
    try {
        if ($(obj).find("option:selected").attr("data-tablassi"))
            tablasSi = JSON.parse($(obj).find("option:selected").attr("data-tablassi"));//JSON.parse($(obj).find("option:selected").attr("tablassi"));        
        if ($(obj).find("option:selected").attr("data-tablasno"))
            tablasNo = JSON.parse($(obj).find("option:selected").attr("data-tablasno"));//JSON.parse();
    } catch (e) {

    }



    for (var i in tablasSi) {

        $("#ddlTablasSi").append($("<option value='" + tablasSi[i].tabla + "'>" + tablasSi[i].tabla + "</option>"));
    }
    for (var i in tablasNo) {
        $("#ddlTablasNo").append($("<option value='" + tablasNo[i].tabla + "'>" + tablasNo[i].tabla + "</option>"));
    }
    $("#ddlTablasSi").multiselect('rebuild');
    $("#ddlTablasNo").multiselect('rebuild');
}
function fnGruposNuevo() {
    $("#ddlTablasNo").find("option").remove();
    $("#ddlTablasSi").find("option").remove();
    $("#ddlTablasNo").empty();
    $("#ddlTablasSi").empty();
    $("#ddlTablasSi").multiselect('rebuild');
    $("#ddlTablasNo").multiselect('rebuild');
}
var tablas = new Array();
function fnCargaTablas() {

    showLoading();
    $.ajax({

        url: "/Sincronizador/GetComboDataByFlag",
        type: "POST",
        dataType: "json",
        data: { flag: 4, filter1: "" },
        success: function (response) {

            var table = $("#tbTablas").dataTable();
            table.fnDestroy()
            table.fnClearTable();
            table.fnDraw();
            tablas = JSON.parse(response.Result[0].Valor);
            var table = $("#tbTablas").dataTable({

                responsive: false,

                columns: [
                    { "data": "Tabla_Origen" },
                    { "data": "Tabla_Destino" },
                    { "data": "Orden_Consulta" },
                    {
                        "data": null, render: function (a, b, c) {

                            var span = "<span style='cursor:pointer' class='fa fa-edit' onclick=fnCargaTabla(" + a.Id_Grupo_Tabla + ")></span>";

                            return span;
                        }
                    },
                ],
                data: JSON.parse(response.Result[0].Valor),
                language: {
                    searchPlaceholder: 'Buscar',
                    url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                }
            });
 
            hideLoading();

        },
        error: function (response) {

            console.log(response)
            //swal("Error al cargar las configuraciones", '', "error").then((value) => { });

        }
    });
}
function fnCargaTabla(id) {


    var data = null;

    for (var i in tablas) {
        if (tablas[i].Id_Grupo_Tabla == id) {
            data = tablas[i];
        }
    }

    if (data == null)
        return;

    $("#txtId_Grupo_Tabla").val(data.Id_Grupo_Tabla);
    $("#txtTablaOrigen").val(data.Tabla_Origen);
    $("#txtTablaDestino").val(data.Tabla_Destino);
    $("#txtOrden").val(data.Orden_Consulta);
    $("#txtNombreComun").val(data.NombreComun);

    if (data.Activo) {

        if (!$("#chkTabla").is(":checked"))
            $("#chkTabla").click();

    }
    else {
        if ($("#chkTabla").is(":checked"))
            $("#chkTabla").click();
    }

    if (data.Solo_Sincronizacion_Manual) {

        if (!$("#chkCargaManual").is(":checked"))
            $("#chkCargaManual").click();

    }
    else {
        if ($("#chkCargaManual").is(":checked"))
            $("#chkCargaManual").click();
    }

}
function fnNuevaTabla() {
    $("#txtId_Grupo_Tabla").val("");
    if ($("#chkTabla").is(":checked"))
        $("#chkTabla").click();
    if (!$("#chkCargaManual").is(":checked"))
        $("#chkCargaManual").click();
    
}
function GuardarTabla() {

    if (String($("#txtTablaOrigen").val()) == "") {
        swal("Ingrese una tabla origen", '', "error").then((value) => { });
        return;
    }
    if (String($("#txtTablaDestino").val()) == "") {
        swal("Ingrese una tabla destino", '', "error").then((value) => { });
        return;
    }
    if (String($("#txtOrden").val()) == "") {
        swal("Ingrese un orden", '', "error").then((value) => { });
        return;
    }

    showLoading();



    $.ajax({

        url: "/Sincronizador/GuardaTabla",
        type: "POST",
        dataType: "json",
        data: {
            TablaOrigen: $("#txtTablaOrigen").val(),
            TablaDestino: $("#txtTablaDestino").val(),
            Orden: $("#txtOrden").val(),
            Activo: $("#chkTabla").is(":checked"),
            Solo_Sincronizacion_Manual: $("#chkCargaManual").is(":checked"),
            Id: $("#txtId_Grupo_Tabla").val() == "" ? "0" : $("#txtId_Grupo_Tabla").val(),
            NombreComun: $("#txtNombreComun").val(),
        },
        success: function (response) {
            if (response.Result) {
                if (response.Result[0].Error < 0)
                    swal(response.Result[0].Descripcion, '', "error").then((value) => { });
                else
                    swal(response.Result[0].Descripcion, '').then((value) => {
                        fnCargaTablas();
                        $("#btnNuevaTabla").click();
                    });
            }
            else
                swal('Ocurrio un error', '', "error").then((value) => { });

            hideLoading();
        },
        error: function (response) {

            swal(response.Message, '', "error").then((value) => { });

        }
    });
}
//});
