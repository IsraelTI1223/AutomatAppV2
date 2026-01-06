$(function () {
    'use strict';

    var $btnBulkLoadLDCOM = $('#btnBulkLoadDiscount'),
        $fileLDCOM = $('#fileLDCOM'),
        $feedback = $('#feedback'),
        $btnUploadFileModal = $('#btnUploadFileModal'),
        $lblFileName = $('#lblFileName'),
        $uploadFileModal = $('#uploadFileModal');
        

    function file_onChage(e) {
        $feedback.hide()
        $lblFileName.text((e.target.files.length > 0) ? e.target.files[0].name : 'Seleccionar archivo');
    }

    function btnUploadFileModal_onClick() {
              
        var Tipo = $("#selectCliente").val();        

        if (Tipo == 0) {
            swal("¡Error no ha seleccionado nignun cliente!", '', "error");
            return;
        }
        else {
            $fileLDCOM.val('');
            $feedback.hide();
            $('#uploadFileModal').modal('show');
        }

      
    }

    function btnBulkLoadLDCOM_onClick() {
        if ($fileLDCOM.val() !== '') {
            showLoading();
            $feedback.hide();
            var fileUpload = $fileLDCOM.get(0);
            var fData = new FormData();
            var file = fileUpload.files[0];
            //var val = $("#selectCliente option:selected").val();      
            var val = $("#selectCliente").val(); 
            var tipo = $("#subtipo option:selected").text()
            fData.append(file.name, file);
            fData.append("LayoutType", "LDCOM");
            fData.append("IdCliente", val);
            fData.append("tipoPerfil", tipo);

            $.ajax({
                type: "POST",
                url: "FileUpload",
                data: fData,
                contentType: false,
                processData: false,
                success: function (response) {
                    hideLoading();
                    if (response.Success) {
                        $uploadFileModal.modal('hide');
                        swal(response.Message, '', "success").then((value) => {
                            window.location.href = "/BulkLoadDiscount/Index";
                        });
                    } else {
                        swal(response.Message, '', "error");
                    }
                },
                error: function (e) {
                    hideLoading();
                    swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
                }
            })

        } else {
            $feedback.show();
        }
    }

    function bindEvents() {
        $btnBulkLoadLDCOM.bind('click', btnBulkLoadLDCOM_onClick);
        $btnUploadFileModal.bind('click', btnUploadFileModal_onClick);
        $fileLDCOM.bind('change', file_onChage);
    }

    var bulkLoadLdcom = (function () {
        return {
            initialize: function () {
                bindEvents();
                $('#tbulkLoadDiscount').dataTable({
                    responsive: true,
                    language: {
                        searchPlaceholder: 'Buscar',
                        url: 'https://cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'
                    }
                });
            }
        };
    })();

    $('#subtipo').change(function () {
        var fData = new FormData();
        var val = $("#subtipo").val(); 
        fData.append("IdSubtipo", val);
        showLoading();
        $('#selectCliente option').remove();

        $.ajax({
            type: "POST",
            url: "GetClientesBySubTipo",
            data: fData,
            contentType: false,
            processData: false,
            success: function (response) {
                hideLoading();
                $('#selectCliente').append('<option value="0">CLIENTES</option>');
                if (response.Success) {                
                   
                    $.each(response.Result, function (index, cliente) {
                        $('#selectCliente').append('<option value=' + cliente.IdCliente + '>' + cliente.Nombre + 'RFC:' + cliente.RFC + '</option>');
                    });
                    $('#selectCliente').focus();
                    
                } else {
                    swal(response.Message, '', "error");
                }
            },
            error: function (e) {
                hideLoading();
                swal("¡Error al intentar enviar el archivo al servidor!", '', "error");
            }
        })
    });

    bulkLoadLdcom.initialize();
});