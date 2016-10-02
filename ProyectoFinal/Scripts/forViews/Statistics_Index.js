﻿$(document).ready(function () { //Cuando cargo el documento, seteo los handlers para los botones
    //TODO setear llamadas en handlers de los botones

    
    Config.call(Config.url.barChart, 'GET', '', BarChart.sucess, BarChart.error);
    Config.call(Config.url.lineChart, 'GET', '', LineChart.sucess, LineChart.error);
    Config.call(Config.url.pieChart, 'GET', '', PieChart.sucess, PieChart.error);

    setTimeout(function () {
        $("#gif").hide();
    }, 2000);




    //BUTTON HANDLERS
    $('#selectChart').change(function () {
        var className = ".".concat($(this).val());
        $(".chart").hide(); //hide all charts

        if (className == ".all") {
            $("body").find(".chart").slideDown(1000); //show all
        }
        else {
            $("body").find(className).slideDown(1000); //show the selected chart
        }
    });

    $('#addDataToLineChart').click(function () {
        var YEARS = LineChart.originalYears;
        var ABONOS = LineChart.originalAbonos;

        if (LineChart.config.data.datasets.length > 0 && LineChart.config.data.labels.length < YEARS.length) {
            var year = YEARS[LineChart.config.data.labels.length % YEARS.length];
            LineChart.config.data.labels.push(year);

            $.each(LineChart.config.data.datasets, function (i, dataset) {
                dataset.data.push(ABONOS[dataset.data.length]);
            });

            window.myLine.update();
        }
    });

    $('#removeDataToLineChart').click(function () {
        if (LineChart.config.data.labels.length > 1) {
            LineChart.config.data.labels.splice(-1, 1); // remove the label first

            LineChart.config.data.datasets.forEach(function (dataset, datasetIndex) {
                dataset.data.pop();
            });

            window.myLine.update();
        }
    });

});

//Init Global Configuration
var Config = { //Variable que contiene una llamada AJAX genérica y una propiedad para setear las urls a usar
    url: {
        barChart: '/Statistics/BarChart',
        lineChart: '/Statistics/LineChart',
        pieChart: '/Statistics/PieChart'
    },

    call: function (url, type, parameters, successCallBack, errorCallBack) {
        $.ajax({
            type: type,
            url: url,
            async: false,
            data: JSON.stringify(parameters),
            contentType: 'application/json;',
            dataType: 'json',
            success: successCallBack,
            error: errorCallBack
        });
    }
};
//End Global Configuration




//Init BarChart Configuration
var BarChart = { //Se encarga de manejar la respuesta de la llamada ajax, de preparar la data para el Chart y de cargar el mismo con los datos

    men: [],

    women: [],

    data: {
        labels: ["Edad 18-35", "Edad 36-50", "Edad 51-64", "Edad 65-", ],
        datasets: [{
            label: 'Hombres',
            backgroundColor: "rgba(220,220,220,0.5)",
            yAxisID: "y-axis-1",
            data: []
        }, {
            label: 'Mujeres',
            backgroundColor: "rgba(151,187,205,0.5)",
            yAxisID: "y-axis-1",
            data: []
        }]

    },

    sucess: function (data) {
        if (data.Result == "OK") {
            console.log(data);
            for (i = 0; i < data.Men.length; i++) {
                BarChart.men[i] = data.Men[i];
            }
            for (i = 0; i < data.Women.length; i++) {
                BarChart.women[i] = data.Women[i];
            }
            BarChart.data.datasets[0].data = BarChart.men;
            BarChart.data.datasets[1].data = BarChart.women;
        }
    },

    error: function (xhr, textStatus, errorThrown) {
        console.log('Error');
    }
};
//End BarChart Configuration

//Init LineChart Configuration
var LineChart = {
    setColors : function(){
        $.each(LineChart.config.data.datasets, function(i, dataset) {
            dataset.borderColor = 'rgba(94,201,117,0.4)'; //randomColor(0.4);
            dataset.backgroundColor = 'rgba(127,154,103,0.5)'; //randomColor(0.5);
            dataset.pointBorderColor = 'rgba(146,72,35,0.7)'; //randomColor(0.7);
            dataset.pointBackgroundColor = 'rgba(211,195,47,0.5)'; //randomColor(0.5);
			
            dataset.pointBorderWidth = 1;
        });
    },

    originalYears: [],
    originalAbonos: [],

    config: {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: "Cantidad de inscripciones",
                data: [],
                fill: true,
                borderDash: [5, 5],
            }]
        },
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Line Chart'
            },
            tooltips: {
                mode: 'label',
                callbacks: {
                }
            },
            hover: {
                mode: 'dataset'
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        show: true,
                        labelString: 'Month'
                    },
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        show: true,
                        labelString: 'Value'
                    },
                    ticks: {
                        suggestedMin: 0,
                    }
                }]
            }
        }
    },

    sucess: function (data) {
        if (data.Result == "OK") {
            console.log(data);
            for (var i = 0; i < data.Data.length; i++) {
                var year = data.Data[i].Year.toString();
                var abonos = data.Data[i].CantAbonos.toString();

                LineChart.config.data.labels.push(year);
                LineChart.originalYears.push(year);

                LineChart.config.data.datasets[0].data.push(abonos);
                LineChart.originalAbonos.push(abonos);

                LineChart.setColors();
                var ctxLine = document.getElementById("canvasLine").getContext("2d");
                window.myLine = new Chart(ctxLine, LineChart.config);
            }
        }
    },

    error: function (xhr, textStatus, errorThrown) {
        console.log('Error');
    }
};
//End LineChart Configuration

var PieChart = {

    config: {
        type: 'pie',
        data: {
            datasets: [{
                data: [],
                backgroundColor: [
                    "#F7464A",
                    "#46BFBD",
                    "#FDB45C",
                    "#949FB1",
                    "#4D5360",
					"#82a43a",
					"#893AA4",
					"#308850",
					"#4068E0",
					"#FFD800",
					"#000080",
					"#D070D0",
					"#80FFD0", 
					"#B8860B",
					"#5F9EA0"
                ],
            }],
            labels: [] //Activity names
        },
        options: {
            responsive: true
        }
    },

    sucess: function (data) {
        if (data.Result == "OK") {
            console.log(data);

            //Set values
            var arrayData = PieChart.config.data.datasets[0].data;
            var labels = PieChart.config.data.labels;

            for (var i = 0; i < data.Data.length; i++) {
                arrayData.push(data.Data[i].CantAbonos);
                labels.push(data.Data[i].ActivityName);
            }
            //Set values

            var ctxPie = document.getElementById("canvasPie").getContext("2d");
            window.myPie = new Chart(ctxPie, PieChart.config);
        }
    },

    error: function (xhr, textStatus, errorThrown) {
        console.log('Error');
    }
}


//Init Charts creation
window.onload = function () { //Carga los charts en los canvas correspondientes
    var ctxBar = document.getElementById("canvasBar").getContext("2d");
    window.myBar = Chart.Bar(ctxBar, {
        data: BarChart.data,
        options: {
            responsive: true,
            hoverMode: 'label',
            hoverAnimationDuration: 400,
            stacked: false,
            title: {
                display: true,
                text: "Asistencias de este año clasificadas por rango de edades"
            },
            scales: {
                yAxes: [{
                    type: "linear", // only linear but allow scale type registration. This allows extensions to exist solely for log scale for instance
                    display: true,
                    position: "left",
                    id: "y-axis-1",
                    max: 300
                }],
            }
        }
    });

    //LineChart.setColors();
    //var ctxLine = document.getElementById("canvasLine").getContext("2d");
    //window.myLine = new Chart(ctxLine, LineChart.config);

};
//End Charts creation


