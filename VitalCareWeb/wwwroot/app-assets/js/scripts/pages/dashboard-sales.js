/*=========================================================================================
    File Name: dashboard-analytics.js
    Description: intialize advance cards
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
    Author: Pixinvent
    Author URL: hhttp://www.themeforest.net/user/pixinvent
    ==========================================================================================*/
$(window).on("load", function () {
  // Revenue - CharJS Line
  Chart.defaults.derivedLine = Chart.defaults.line;
  var draw = Chart.controllers.line.prototype.draw;
  var custom = Chart.controllers.line.extend({
    draw: function () {
      draw.apply(this, arguments);
      var ctx = this.chart.chart.ctx;
      var _stroke = ctx.stroke;
      ctx.stroke = function () {
        ctx.save();
        ctx.shadowColor = "#ffb6c0";
        ctx.shadowBlur = 30;
        ctx.shadowOffsetX = 0;
        ctx.shadowOffsetY = 20;
        _stroke.apply(this, arguments);
        ctx.restore();
      };
    }
  });


  //Total Earnings

      /********************************************
      *               Monthly Sales               *
      ********************************************/
      




  //Sessions by Browser
  // -----------------------------------
//  Morris.Donut({
//    element: "sessions-browser-donut-chart",
//    data: [
//      {
//        label: "Chrome",
//        value: 3500
//      },
//      {
//        label: "Firefox",
//        value: 2500
//      },
//      {
//        label: "Safari",
//        value: 2000
//      },
//      {
//        label: "Opera",
//        value: 1000
//      },
//      {
//        label: "Internet Explorer",
//        value: 500
//      }
//    ],
//    resize: true,
//    colors: ["#40C7CA", "#FF7588", "#2DCEE3", "#FFA87D", "#16D39A"]
//  });


    /*************************************************
      *               Cost Revenue Stats               *
      *************************************************/
    new Chartist.Line('#cost-revenue', {
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
        series: [
            [
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 5}
             
            ]
        ]
    }, {
        low: 0,
        high: 18,
        fullWidth: true,
        showArea: true,
        showPoint: true,
        showLabel: false,
        axisX: {
            showGrid: false,
            showLabel: false,
            offset: 0
        },
        axisY: {
            showGrid: false,
            showLabel: false,
            offset: 0
        },
        chartPadding: 0,
        plugins: [
            Chartist.plugins.tooltip()
        ]
    }).on('draw', function(data) {
        if (data.type === 'area') {
            data.element.attr({
                'style': 'fill: #28D094; fill-opacity: 0.2'
            });
        }
        if (data.type === 'line') {
            data.element.attr({
                'style': 'fill: transparent; stroke: #28D094; stroke-width: 4px;'
            });
        }
        if (data.type === 'point') {
            var circle = new Chartist.Svg('circle', {
                cx: [data.x], cy:[data.y], r:[7],
            }, 'ct-area-circle');
            data.element.replace(circle);
        }
    });
});
