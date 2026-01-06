function graficarPorcentajeCumplimientoFecha(idnegociacion, idGrafica, divEstadisticasId) {

    var url = window.location.protocol + "//" + window.location.host + "/NegociacionesEstadisticas/GetTopPorcentajeCumplimiento?=" +  + "?=10?idMayorista=0"

    const data = {
        IdNegociacion: idnegociacion,
        NumRegistros: 10,
        idMayorista: '0'
    };

	//Llamada a controlador por medio de ajax
	$.ajax({
		url: url,
        method: 'POST',
        contentType: 'application/json', // Tipo de contenido
        data: JSON.stringify(data), // Convierte los datos a JSON
        success: function (dataResult) {
            //validar si tiene registros
            if (typeof dataResult.grupos != 'undefined' && dataResult.grupos != null) {
                if (dataResult.grupos.length > 0) {
                    crearGraficoPorcentajeCumplimientoFecha(dataResult, idGrafica); // Llamar a la función para crear el gráfico
                    if (divEstadisticasId != "") {
                        $("#" + divEstadisticasId).show("slow");
                    }
                }
            }
		},
		error: function (error) {
			console.error('Error:', error);
		}
	});

}

// Función para crear el gráfico
function crearGraficoPorcentajeCumplimientoFecha(dataResult, idGrafica) {
    // Obtener las fechas únicas
    const groups = dataResult.grupos;
    console.log(groups);
    const labels = [...new Set(groups.flatMap(grupo => grupo.datos.map(dato => dato.fecha)))];
    console.log(labels);

    // Preparar los datasets
    const datasets = groups.map(grupo => ({
        label: grupo.label,
        data: labels.map(fecha => {
            const dato = grupo.datos.find(d => d.fecha === fecha);
            return dato ? dato.porcentaje : 0;
        }),
        backgroundColor: `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 0.2)`,
        borderColor: `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 1)`,
        borderWidth: 1
    }));

    // Crear el gráfico
    const ctx = document.getElementById(idGrafica).getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    //max: 100,
                    ticks: {
                        callback: function (value) {
                            return value + '%';
                        }
                    }
                },
                x: {
                    type: 'category',
                    ticks: {
                        autoSkip: false
                    }
                }
            }
        }
    });
}

/*
	$.get(url, function (data) {

		const grupos = data.grupos;

		const dataResult = grupos.map(grupo => grupo.datos.map(dato => dato.porcentaje));

		var ctx = document.getElementById(idGrafica).getContext('2d');
		var myChart = new Chart(ctx, {
			type: 'line',
			data: {
				labels: data.labels,
				datasets: [{
					label: 'Porcentaje de cumplimiento',
					data: dataResult.data,
					backgroundColor: 'rgba(255, 99, 132, 0.2)',
					borderColor: 'rgba(255, 99, 132, 1)',
					borderWidth: 1
				}]
			},
			options: {
				scales: {
					yAxes: [{
						ticks: {
							beginAtZero: false
						}
					}]
				}
			}
		});
	});

}*/