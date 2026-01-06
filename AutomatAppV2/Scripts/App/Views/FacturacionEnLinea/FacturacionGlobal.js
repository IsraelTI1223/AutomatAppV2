
$(function () {
    'use strict';

    $(document).ready(function () {


        $('#cbCadenas').multiselect({
            nonSelectedText: 'Lista de cadenas',
            enableFiltering: true,
            includeSelectAllOption: true,
            maxHeight: 400,
            buttonWidth: '150px',
            buttonTextAlignment: 'left',
            selectAllText: "Marcar Todas",
            allSelectedText: "Todas Seleccionadas",
            nSelectedText: "Cadenas"
        });

        $('#cbSucursalesNC').multiselect({
            nonSelectedText: 'Lista de Sucursales',
            enableFiltering: true,
            includeSelectAllOption: true,
            maxHeight: 400,
            buttonWidth: '150px',
            buttonTextAlignment: 'left',
            selectAllText: "Marcar Todas",
            allSelectedText: "Todas Seleccionadas",
            nSelectedText: "Sucursales"
        });


    });







    $("#frmResumenFactGlb").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            GetResumenFacturacionGlobalCadena();

        }

    });

    $("#frmResumenFactGlb").validate({
        messages: {
            ntxtFecha: {
                required: 'Campo obligatorio'
            },
            ncbCadenas: {
                required: 'Seleccione al menos una Cadena'
            }
        }
    });

    function GetResumenFacturacionGlobalCadena() {


        console.log($("#cbCadenas").val());
        const cadenas = $("#cbCadenas").val();

        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetResumenFacturacionGlobalCadena",
            type: "POST",
            dataType: "json",
            data: { Empresa_Id: cadenas.join(), Dia_Operacion: $("#txtFecha").val() },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    $('#dtResumenFactGlb').DataTable({
                        data: response.Result,
                        "destroy": true,
                        "processing": true,
                        dom: 'Bfrtip',
                        columns: [
                            { data: "Cadena", title: 'Cadena' },
                            { data: "IdTienda", title: 'Id Tienda' },
                            { data: "Nombre", title: 'Nombre Tienda' },
                            { data: "Fecha", title: 'Dia Operación' },
                            { data: "Estatus", title: 'Estatus' },
                            { data: "Uuid", title: 'UUID Factura Global' },
                            { data: "oMsg", title: 'Mensaje' }

                        ],
                        select: true,
                        scrollCollapse: true,
                        info: false,
                        paging: true,
                        responsive: true,
                        searching: true
                    });

                } else {
                    swal("Sin resultados", '', "error").then((value) => { });

                }
                hideLoading();

            },
            error: function (response) {

                swal(response.Message, '', "error").then((value) => { });

            }
        });

    }



    $("#frmResumenNC").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            GetResumenTicketsNCPuntos();

        }

    });

    $("#frmResumenNC").validate({
        messages: {
            ntxtFechaNC: {
                required: 'Campo obligatorio'
            },
            ncbCadenasNC: {
                required: 'Seleccione al menos una Cadena'
            },
            ncbSucursalesNC: {
                required: 'Seleccione al menos una Sucursal'
            }
        }
    });

    function GetResumenTicketsNCPuntos() {


        console.log($("#cbCadenas").val());
        const cadenas = $("#cbCadenasNC").val();

        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetResumenTicketsNCPuntos",
            type: "POST",
            dataType: "json",
            data: { Empresa_Id: $("#cbCadenasNC").val(), Dia_Operacion: $("#txtFechaNC").val(), Sucursales: String($("#cbSucursalesNC").val()) },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    $('#dtResumenNC').DataTable({
                        data: response.Result,
                        "destroy": true,
                        "processing": true,
                        dom: 'Bfrtip',
                        columns: [
                            //{ data: "NoControl", title: 'Cadena' },
                            { data: "IdTienda", title: 'Id Tienda' },
                            { data: "NombreSucursal", title: 'Nombre Tienda' },
                            { data: "Ticket", title: 'Ticket' },
                            { data: "Subtotal", title: 'Subtotal' },
                            { data: "DiaOperacion", title: 'Dia Operación' },
                            { data: "UuidGLB", title: 'UUID Factura Global' },
                            { data: "UuidNC", title: 'UUID NC' }

                        ],
                        select: true,
                        scrollCollapse: true,
                        info: false,
                        paging: true,
                        responsive: true,
                        searching: true
                    });

                } else {
                    swal("Sin resultados", '', "error").then((value) => { });

                }
                hideLoading();

            },
            error: function (response) {

                swal(response.Message, '', "error").then((value) => { });

            }
        });

    }

    $("#cbCadenasNC").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSucursalesNC').empty();



        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetSucursales",
            type: "POST",
            dataType: "json",
            data: { flag: 2, cadena: optionSelected.val() },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    var sucursales = "";

                    console.log(response)

                    if (response.Result != null) {

                        for (var i = 0; i < response.Result.length; i++) {
                            sucursales = sucursales + '<option value=' + response.Result[i].Id + '>' + response.Result[i].Valor + '</option>';
                        }
                    }

                    $("#cbSucursalesNC").append(sucursales);

                    $('#cbSucursalesNC').multiselect('rebuild');

                } else {
                    swal(response.Message, '', "error").then((value) => { });

                }
                hideLoading();

            },
            error: function (response) {

                swal(response.Message, '', "error").then((value) => { });

            }
        });




    });


});
