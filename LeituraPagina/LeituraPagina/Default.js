$(document).ready(function () {
   DrawChart();
});

function DrawChart() {
    var ctx = document.getElementById('chart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        },
        data: {
            labels: eval(HiddenLabel.value),
            datasets: [{
                label: 'Top 10 Word Counter Chart',
                data: eval(HiddenData.value),
                backgroundColor: 'rgba(255, 70, 122, 0.5)'
            }]
        }
    });
}