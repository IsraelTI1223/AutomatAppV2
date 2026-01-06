$(document).ready(function () {

    GetHistoricoConfiguraciones();

});

function GetHistoricoConfiguraciones() {

    showLoading();
    $.ajax({
        url: "/Sincronizador/GetHistoricoConfiguraciones",
        type: "POST",
        dataType: "json",
        //data: { Empresa_Id: $("#cbCadenas").val(), Dia_Operacion: $("#txtFecha").val(), Sucursales: String($("#cbSucursales").val()) },
        success: function (response) {

            console.log(response)

            if (response.Success) {

                $('#dtHistoricoConf').DataTable({
                    data: response.Result,
                    "destroy": true,
                    "processing": true,
                    dom: 'Bfrtip',
                    columns: [
                        { data: "Fecha_Registro", title: 'Fecha' },
                        { data: "Usuario", title: 'Responsable' },
                        { data: "Cadena", title: 'Cadena' },
                        { data: "Tiendas", title: 'Sucursal' },
                        { data: "Grupo", title: 'Grupo' },
                        { data: "Intervalo_Reintento", title: 'Intervalo' },
                        { data: "Proceso", title: 'Proceso' },
                        { data: "Reintentos", title: 'Reintentos' },
                        { data: "Inicio", title: 'Inicio' },
                        { data: "Fin", title: 'Fin' },
                        { data: "Estatus", title: 'Estatus' },

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
