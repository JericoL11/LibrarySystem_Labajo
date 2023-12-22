$(document).ready(function () {
    var myChart; // Variable to store the chart instance

    $('#viewReport').on('click', function () {

        var dfrom = $('#dateFrom').val();
        var dto = $('#dateTo').val();

        $.ajax({
            url: "/Report/GetReport",
            data: {
                dateFrom: dfrom,
                dateTo: dto,
            },
            type: "GET",
            dataType: "json",
            success: function (data) {

                var labels = [];
                var dateNo = [];
                for (var i = 0; i < data.dateResult.length; i++) {
                    labels.push(data.dateResult[i].return_date);
                    dateNo.push(data.dateResult[i].return_Count);
                };

               
                const ctr = document.getElementById('mychart');

                // Create a new chart
               new Chart(ctr, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Report View',
                            data: dateNo,
                            borderWidth: 1,

                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true,
                            }
                        }
                    }
                });

            },
            error: function (errorData) {
                console.log(errorData);
            }
        });

    }); 
}); 
