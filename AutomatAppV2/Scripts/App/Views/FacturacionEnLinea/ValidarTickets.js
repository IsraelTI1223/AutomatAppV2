
$(function () {
    'use strict';

    $(document).ready(function () {


        $('#cbSucursales').multiselect({
            nonSelectedText: 'Lista de Sucursales',
            enableFiltering: true,
            includeSelectAllOption: true,
            maxHeight: 400,
            buttonWidth: '150px',
            buttonTextAlignment: 'left',
            selectAllText: "Marcar Todas",
            allSelectedText: "Todas Seleccionados",
            nSelectedText: "Sucursales"
        });

        $('#cbSucursalesTran').multiselect({
            nonSelectedText: 'Lista de Sucursales',
            enableFiltering: true,
            includeSelectAllOption: true,
            maxHeight: 400,
            buttonWidth: '150px',
            buttonTextAlignment: 'left',
            selectAllText: "Marcar Todas",
            allSelectedText: "Todas Seleccionados",
            nSelectedText: "Sucursales"
        });




    });



      

    $("#cbCadenas").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSucursales').empty();



        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetSucursales",
            type: "POST",
            dataType: "json",
            data: { flag : 2 ,cadena: optionSelected.val() },
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

                    $("#cbSucursales").append(sucursales);

                    $('#cbSucursales').multiselect('rebuild');

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

    $("#frmBusquedaXsuc").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            getDetalleTickets();

        }

    });

    $("#frmBusquedaXsuc").validate({
        messages: {
            ntxtFecha: {
                required: 'Campo obligatorio'
            },
            ncbCadenas: {
                required: 'Seleccione al menos una Cadena'
            },
            ncbSucursales: {
                required: 'Seleccione al menos una sucursal'
            }
        }
    });

    function getDetalleTickets() {

        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetDetalleEnvioTickets",
            type: "POST",
            dataType: "json",
            data: { Empresa_Id: $("#cbCadenas").val(), Dia_Operacion: $("#txtFecha").val(), Sucursales: String($("#cbSucursales").val()) },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    $('#dtDetTickets').DataTable({
                        data: response.Result,
                        "destroy": true,
                        "processing": true,
                        dom: 'Bfrtip',
                        columns: [
                            { data: "Cadena", title: 'Cadena' },
                            { data: "Nombre_Tienda", title: 'Tienda' },
                            { data: "N_Ticket", title: 'Ticket' },
                            { data: "Dia_Operacion", title: 'Dia Operación' },
                            { data: "Numero_Peticion", title: 'Numero Petición' },
                            { data: "HoraHub", title: 'Hora Hub' },
                            { data: "HoraPeticion", title: 'Hora Petición' },
                            { data: "Estatus_Hub", title: 'Estatus Hub' },
                            { data: "Estatus_FACE", title: 'Estatus FACE' },
                            { data: "Errores", title: 'Errores' },
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






    $("#frmBusquedaTicket").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            getHistoriaTicket();

        }

    });

    $("#frmBusquedaTicket").validate({
        messages: {
            nTicket: {
                required: 'Campo obligatorio'
            }
        }
    });

    function getHistoriaTicket() {


        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetTicketsHistEnvio",
            type: "POST",
            dataType: "json",
            data: { Empresa_Id: $("#cbCadenasTicket").val(), Sucursal: $("#cbSucursalesTicket").val(), Ticket: $("#txtTicket").val() },
            success: function (response) {


                if (response.Success) {

                    //console.log(response.Result[0])

                    $("#divPaso1").text(response.Result[0].HoraNube == '' ? '?' : response.Result[0].HoraNube);
                    $("#divPaso2").text(response.Result[0].HoraHub == '' ? '?' : response.Result[0].HoraHub);
                    $("#divPaso3").text(response.Result[0].HoraPeticion == '' ? '?' : response.Result[0].HoraPeticion);
                    $("#divPaso4").text(response.Result[0].HoraAtencion == '' ? '?' : response.Result[0].HoraAtencion);

                    $("#cardTicket").show();








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

    $("#cbCadenasTicket").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSucursalesTicket').empty();



        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetSucursales",
            type: "POST",
            dataType: "json",
            data: { flag : 2 ,cadena: optionSelected.val() },
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

                    
                    $("#cbSucursalesTicket").append("<option value='' disabled='disabled' selected>Selecciona...</option>");
                    $("#cbSucursalesTicket").append(sucursales);


                    

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


    $("#cbCadenasTran").change(function () {
        var optionSelected = $(this).find("option:selected");


        $('#cbSucursalesTran').empty();



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

                    $("#cbSucursalesTran").append(sucursales);

                    $('#cbSucursalesTran').multiselect('rebuild');

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

    $("#frmResumenTransacciones").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            getResumenTransacciones();

        }

    });

    $("#frmResumenTransacciones").validate({
        messages: {
            txtFechaTran: {
                required: 'Campo obligatorio'
            },
            ncbCadenasTran: {
                required: 'Seleccione al menos una Cadena'
            },
            cbSucursalesTran: {
                required: 'Seleccione al menos una sucursal'
            }
        }
    });

    function getResumenTransacciones() {

        showLoading();
        $.ajax({
            url: "/FacturacionEnLinea/GetResumenTransacciones",
            type: "POST",
            dataType: "json",
            data: { Empresa_Id: $("#cbCadenasTran").val(), Dia_Operacion: $("#txtFechaTran").val(), Sucursales: String($("#cbSucursalesTran").val()) },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    $('#dtResumenTran').DataTable({
                        data: response.Result,
                        "destroy": true,
                        "processing": true,
                        dom: 'Bfrtip',
                        columns: [
                            { data: "IdTienda", title: 'Id Tienda' },
                            { data: "Nombre", title: 'Nombre Tienda' },
                            { data: "Fecha", title: 'Dia Operación' },
                            { data: "TicketsEnviados", title: 'Tickets Enviados' },
                            { data: "TicketsCancelados", title: 'Tickets Cancelados' },
                            { data: "TicketsNC", title: 'Tickets en NC' }
                            
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


});
