
$(function () {
    'use strict';

    $("#cbCadenas").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSubCadenas').empty();
        $('#cbSucursal').empty();
        $('#cbProveedor').empty();

        showLoading();
        $.ajax({
            url: "/AltaHub/GetSubCadenaHub",
            type: "POST",
            dataType: "json",
            data: { flag: 2, cadena: optionSelected.val() },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    var sucursales = "";

                    console.log(response)

                    if (response.Result != null) {

                        sucursales = sucursales + '<option value="Selecciona..." disabled="disabled" selected>Selecciona...</option> ';

                        for (var i = 0; i < response.Result.length; i++) {
                            sucursales = sucursales + '<option value=' + response.Result[i].Id + '>' + response.Result[i].Valor + '</option>';
                        }
                    }

                    $("#cbSubCadenas").append(sucursales);
                    $("#cbSucursal").append('<option value="Selecciona..." disabled="disabled" selected>Selecciona...</option> ');

                    $("#cbProveedor").append('<option selected="selected" disabled="disabled" >Selecciona...</option> ');
                    $("#cbProveedor").append('<option value="1" id="idBlue" > BLUE (Tarjetón) </option> ');
                    $("#cbProveedor").append('<option value="2" id="idDaport" > DAPORT </option> ');

                    $('#Idinputs').hide();

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





    $("#cbCadenasM").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSubCadenasM').empty();
        $('#cbSucursalM').empty();
        $('#cbProveedorM').empty();

        showLoading();
        $.ajax({
            url: "/AltaHub/GetSubCadenaHub",
            type: "POST",
            dataType: "json",
            data: { flag: 2, cadena: optionSelected.val() },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    var sucursales = "";

                    console.log(response)

                    if (response.Result != null) {

                        sucursales = sucursales + '<option value="Selecciona..." disabled="disabled" selected>Selecciona...</option> ';

                        for (var i = 0; i < response.Result.length; i++) {
                            sucursales = sucursales + '<option value=' + response.Result[i].Id + '>' + response.Result[i].Valor + '</option>';
                        }
                    }

                    $("#cbSubCadenasM").append(sucursales);
                    $("#cbSucursalM").append('<option value="Selecciona..." disabled="disabled" selected>Selecciona...</option> ');

                    $("#cbProveedorM").append('<option selected="selected" disabled="disabled" >Selecciona...</option> ');
                  
                    $("#cbProveedorM").append('<option value="2" id="idDaport" > DAPORT </option> ');

                    $('#Idinputs').hide();

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








    $("#cbSubCadenas").change(function () {
        var optionSelected = $(this).find("option:selected");
        //var valueSelected = optionSelected.val();
        //var textSelected = optionSelected.text();

        $('#cbSucursal').empty();


        showLoading();
        $.ajax({
            url: "/AltaHub/GetSucursalesHub",
            type: "POST",
            dataType: "json",
            data: { flag: 3, cadena: optionSelected.val() },
            success: function (response) {

                console.log(response)

                if (response.Success) {

                    var sucursales = "";

                    console.log(response)

                    if (response.Result != null) {

                        sucursales = sucursales + '<option value="Selecciona..." disabled="disabled" selected>Selecciona...</option> ';
                        for (var i = 0; i < response.Result.length; i++) {
                            sucursales = sucursales + '<option value=' + response.Result[i].Id + '>' + response.Result[i].Valor + '</option>';
                        }
                    }

                    $("#cbSucursal").append(sucursales);
                    $('#Idinputs').hide();

                } else {
                    swal("No se encontraron resultados", '', "error").then((value) => { });

                }
                hideLoading();

            },
            error: function (response) {

                swal("Error desconocido", '', "error").then((value) => { });

            }
        });

    });


    $("#cbProveedor").change(function () {
        var optionSelected = $(this).find("option:selected");

        if (optionSelected.val() == 1)
            $('#Idinputs').show();

        if (optionSelected.val() == 2)
            $('#Idinputs').hide();

    });


    $("#frmBusquedaHub").validate({
        messages: {
            ncbCadenas: {
                required: 'Campo obligatorio'
            },
            ncbSubCadenas: {
                required: 'Campo obligatorio'
            },
            ncbSucursal: {
                required: 'Campo obligatorio'
            },
            ncbProveedeor: {
                required: 'Campo obligatorio'
            }
        }

    });




    $("#frmBusquedaHub").on("submit", function (event) {

        if ($(this).valid()) {
            event.preventDefault();
            setDataHub();

        }

    });

    function setDataHub() {

        console.log($("#idCad").val());
        console.log($("#idSuc").val());

        showLoading();
        $.ajax({
            url: "/GeneraAltaHub/GeneraAltaFunction",
            type: "POST",
            dataType: "json",
            data: { dat1: $('#cbCadenas option:selected').val(), dat2: $("#cbSubCadenas").val(), dat3: $("#cbSucursal").val(), dat4: $("#cbProveedor").val(), dat5: $("#idCad").val(), dat6: $("#idSuc").val() },
            success: function (response) {
                console.log(response)

                if (response[4] == "true") {

                    const blob1 = new Blob([response[0]], { type: 'text' });
                    if (window.navigator.msSaveOrOpenBlob) {
                        window.navigator.msSaveBlob(blob1, "Grupo4-Script8");
                    }
                    else {
                        const elem = window.document.createElement('a');
                        elem.href = window.URL.createObjectURL(blob1);
                        elem.download = "Grupo4-Script8";
                        document.body.appendChild(elem);
                        elem.click();
                        document.body.removeChild(elem);
                    }

                    const blob2 = new Blob([response[1]], { type: 'text' });
                    if (window.navigator.msSaveOrOpenBlob) {
                        window.navigator.msSaveBlob(blob2, "API-Access-Table");
                    } else {
                        const elem = window.document.createElement('a');
                        elem.href = window.URL.createObjectURL(blob2);
                        elem.download = "API-Access-Table";
                        document.body.appendChild(elem);
                        elem.click();
                        document.body.removeChild(elem);
                    }

                    const blob3 = new Blob([response[2]], { type: 'text' });
                    if (window.navigator.msSaveOrOpenBlob) {
                        window.navigator.msSaveBlob(blob3, "Grupo4-Script9");
                    }
                    else {
                        const elem = window.document.createElement('a');
                        elem.href = window.URL.createObjectURL(blob3);
                        elem.download = "Grupo4-Script9";
                        document.body.appendChild(elem);
                        elem.click();
                        document.body.removeChild(elem);
                    }

                    const blob4 = new Blob([response[3]], { type: 'text' });
                    if (window.navigator.msSaveOrOpenBlob) {
                        window.navigator.msSaveBlob(blob4, "Grupo4-Script7");
                    }
                    else {
                        const elem = window.document.createElement('a');
                        elem.href = window.URL.createObjectURL(blob4);
                        elem.download = "Grupo4-Script7";
                        document.body.appendChild(elem);
                        elem.click();
                        document.body.removeChild(elem);
                    }

                    swal("EXITO", '', "success").then((value) => { });

                    setTimeout('location.reload()', 5000);

                } else {

                    swal("ERROR", response[5], "error").then((value) => { });
                    setTimeout('location.reload()', 5000);
                }
                hideLoading();

            },
            error: function (response) {
                setTimeout('location.reload()', 5000);
                swal("ERROR", '', "error").then((value) => { });


            }
        });
    }

});