function addData4(data) {

    const dataPoints = [];

    const chart = new CanvasJS.Chart("chartContainer4", {
        animationEnabled: true,
        theme: "light1",
        title: {
            text: "Pressure"
        },
        axisY: {
            title: "hPa",
            titleFontSize: 24,
            crosshair: {
                enabled: true,
                valueFormatString: "#,##0.##",
                snapToDataPoint: true
            }
        },
        axisX: {
            crosshair: {
                enabled: true,
                snapToDataPoint: true
            }
        },
        data: [{
            type: "line",
            yValueFormatString: "#,##0.##",
            xValueFormatString: "MMM DD hh mm ss",
            dataPoints: dataPoints
        }]
    });

    for (let i = 0; i < data.length; i++) {
        dataPoints.push({
            x: new Date(data[i].date),
            y: data[i].value
        });
    }
    chart.render();

}
