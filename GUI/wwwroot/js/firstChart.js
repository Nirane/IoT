function addData(data) {

    const dataPoints = [];

    const chart = new CanvasJS.Chart("chartContainer", {
        animationEnabled: true,
        theme: "light1",
        title: {
            text: "Temperature"
        },
        axisY: {
            title: "degrees Celsius",
            titleFontSize: 24,
            suffix: "°",
            crosshair: {
                enabled: true,
                valueFormatString: "#,##0.##°",
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
            yValueFormatString: "#,##0.## °",
            xValueFormatString: "MMM DD YYYY",
            dataPoints: dataPoints
        }]
    });

    for (let i = 0; i < data.length; i++) {
        dataPoints.push({
            x: new Date(data[i].x),
            y: data[i].y
        });
    }
    chart.render();

}
