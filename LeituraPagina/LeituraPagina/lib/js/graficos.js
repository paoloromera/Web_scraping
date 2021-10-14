
var agrupamento = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
var cores = "#008080";
var cordaborda = "transparent";
var cordefundo = "#008080";
var legenda = false;
var dados = [98281.7, 102676.65, 0, 0];

function CriarGaugeRosca(chart, titulo, dados, cor, borda) {
    "use strict";
    feather.replace();
    Chart.defaults.global.defaultFontSize = 16;
    var ctx = chart;
    var myChart = new Chart(ctx, {
        type: "doughnut",
        data: {
            datasets: [
                {
                    data: dados,
                    backgroundColor: cor
                }
            ],
            labels: [titulo, ""]
        },
        options: {
            responsive: true,
            legend: {
                position: "top",
                display: false
            },
            title: {
                display: true,
                text: titulo + ": " + dados[0]
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        return data == dados[0] ? titulo + ": " + dados[0] : "";
                    }
                }
            }
        }
    });
}

function CriarGraficoRosca(chart, titulo, dados, agrupamentos, cores) {
    "use strict";
    feather.replace();
    Chart.defaults.global.defaultFontSize = 12;
    var ctx = chart;
    var myChart = new Chart(ctx, {
        type: "doughnut",
        data: {
            datasets: [
                {
                    data: dados,
                    backgroundColor: cores
                }
            ],
            labels: agrupamentos
        },
        options: {
            responsive: true,
            legend: {
                position: "bottom",
                display: true,
                labels: {
                    boxWidth: 15,
                    fontSize: 12
                }
            },
            title: {
                display: true,
                text: titulo
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },
            circumference: Math.PI,
            rotation: -Math.PI
        }
    });
}
