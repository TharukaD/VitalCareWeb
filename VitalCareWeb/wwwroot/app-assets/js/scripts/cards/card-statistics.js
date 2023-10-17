/*=========================================================================================
    File Name: card-statistics.js
    Description: intialize advance card statistics
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
    Author: Pixinvent
    Author URL: hhttp://www.themeforest.net/user/pixinvent
==========================================================================================*/
(function(window, document, $) {
  "use strict";

  /*****************************************************
   *               Grouped Card Statistics              *
   *****************************************************/
  var rtl = false;
  if ($("html").data("textdirection") == "rtl") rtl = true;

  $(".knob").knob({
    rtl: rtl,
    draw: function() {
      var ele = this.$;
      var style = ele.attr("style");
      style = style.replace("bold", "normal");
      var fontSize = parseInt(ele.css("font-size"), 10);
      var updateFontSize = Math.ceil(fontSize * 1.65);
      style = style.replace("bold", "normal");
      style = style + "font-size: " + updateFontSize + "px;";
      var icon = ele.attr("data-knob-icon");
      ele.hide();
      $('<i class="knob-center-icon ' + icon + '"></i>')
        .insertAfter(ele)
        .attr("style", style);

      // "tron" case
      if (this.$.data("skin") == "tron") {
        this.cursorExt = 0.3;

        var a = this.arc(this.cv), // Arc
          pa, // Previous arc
          r = 1;

        this.g.lineWidth = this.lineWidth;

        if (this.o.displayPrevious) {
          pa = this.arc(this.v);
          this.g.beginPath();
          this.g.strokeStyle = this.pColor;
          this.g.arc(
            this.xy,
            this.xy,
            this.radius - this.lineWidth,
            pa.s,
            pa.e,
            pa.d
          );
          this.g.stroke();
        }

        this.g.beginPath();
        this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
        this.g.arc(
          this.xy,
          this.xy,
          this.radius - this.lineWidth,
          a.s,
          a.e,
          a.d
        );
        this.g.stroke();

        this.g.lineWidth = 2;
        this.g.beginPath();
        this.g.strokeStyle = this.o.fgColor;
        this.g.arc(
          this.xy,
          this.xy,
          this.radius - this.lineWidth + 1 + (this.lineWidth * 2) / 3,
          0,
          2 * Math.PI,
          false
        );
        this.g.stroke();

        return false;
      }
    }
  });


  /************************************************
   *               Sparkline Charts                *
   ************************************************/

  var sparkLineDraw = function() {
    /******************
     *   Line Charts   *
     ******************/
    // Total Cost
    $("#sp-line-total-cost").sparkline(
      [14, 12, 4, 9, 3, 6, 11, 10, 13, 9, 14, 11, 16, 20, 15],
      {
        type: "line",
        width: "100%",
        height: "100px",
        lineColor: "#FF9149",
        fillColor: "#FF9149",
        spotColor: "",
        minSpotColor: "",
        maxSpotColor: "",
        highlightSpotColor: "",
        highlightLineColor: "",
        chartRangeMin: 0,
        chartRangeMax: 20
      }
    );

    // Total Sales
    $("#sp-line-total-sales").sparkline(
      [14, 12, 4, 9, 3, 6, 11, 10, 13, 9, 14, 11, 16, 20, 15],
      {
        type: "line",
        width: "100%",
        height: "100px",
        lineColor: "#28D094",
        fillColor: "#28D094",
        spotColor: "",
        minSpotColor: "",
        maxSpotColor: "",
        highlightSpotColor: "",
        highlightLineColor: "",
        chartRangeMin: 0,
        chartRangeMax: 20
      }
    );

    // Total Revenue
    $("#sp-line-total-revenue").sparkline(
      [14, 12, 4, 9, 3, 6, 11, 10, 13, 9, 14, 11, 16, 20, 15],
      {
        type: "line",
        width: "100%",
        height: "100px",
        lineColor: "#FF4961",
        fillColor: "#FF4961",
        spotColor: "",
        minSpotColor: "",
        maxSpotColor: "",
        highlightSpotColor: "",
        highlightLineColor: "",
        chartRangeMin: 0,
        chartRangeMax: 20
      }
    );

    /*****************
     *   Bar Charts   *
     *****************/
    $("#sp-bar-total-cost").sparkline(
      [
        5,
        6,
        7,
        8,
        9,
        10,
        12,
        13,
        15,
        14,
        13,
        12,
        10,
        9,
        8,
        10,
        12,
        14,
        15,
        16,
        17,
        14,
        12,
        11,
        10,
        8
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 2,
        barSpacing: 4,
        barColor: "#FF9149"
      }
    );

    $("#sp-bar-total-sales").sparkline(
      [
        5,
        6,
        7,
        8,
        9,
        10,
        12,
        13,
        15,
        14,
        13,
        12,
        10,
        9,
        8,
        10,
        12,
        14,
        15,
        16,
        17,
        14,
        12,
        11,
        10,
        8
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 2,
        barSpacing: 4,
        barColor: "#28D094"
      }
    );

    $("#sp-bar-total-revenue").sparkline(
      [
        5,
        6,
        7,
        8,
        9,
        10,
        12,
        13,
        15,
        14,
        13,
        12,
        10,
        9,
        8,
        10,
        12,
        14,
        15,
        16,
        17,
        14,
        12,
        11,
        10,
        8
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 2,
        barSpacing: 4,
        barColor: "#FF4961"
      }
    );

    /*************************
     *   Stacked Bar Charts   *
     *************************/
    $("#sp-stacked-bar-total-cost").sparkline(
      [
        [8, 10],
        [12, 8],
        [9, 14],
        [8, 11],
        [10, 13],
        [7, 11],
        [8, 14],
        [9, 12],
        [10, 11],
        [12, 14],
        [8, 12],
        [9, 12],
        [9, 14]
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 4,
        barSpacing: 6,
        stackedBarColor: ["#4CAF50", "#FFEB3B"]
      }
    );

    $("#sp-stacked-bar-total-sales").sparkline(
      [
        [8, 10],
        [12, 8],
        [9, 14],
        [8, 11],
        [10, 13],
        [7, 11],
        [8, 14],
        [9, 12],
        [10, 11],
        [12, 14],
        [8, 12],
        [9, 12],
        [9, 14]
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 4,
        barSpacing: 6,
        stackedBarColor: ["#FF5722", "#009688"]
      }
    );

    $("#sp-stacked-bar-total-revenue").sparkline(
      [
        [8, 10],
        [12, 8],
        [9, 14],
        [8, 11],
        [10, 13],
        [7, 11],
        [8, 14],
        [9, 12],
        [10, 11],
        [12, 14],
        [8, 12],
        [9, 12],
        [9, 14]
      ],
      {
        type: "bar",
        width: "100%",
        height: "30px",
        barWidth: 4,
        barSpacing: 6,
        stackedBarColor: ["#E91E63", "#00BCD4"]
      }
    );

    /**********************
     *   Tristate Charts   *
     **********************/
    $("#sp-tristate-bar-total-cost").sparkline(
      [1, 1, 0, 1, -1, -1, 1, -1, 0, 0, 1, 1, 0, -1, 1, -1],
      {
        type: "tristate",
        height: "30",
        posBarColor: "#ffeb3b",
        negBarColor: "#4caf50",
        barWidth: 4,
        barSpacing: 5,
        zeroAxis: false
      }
    );

    $("#sp-tristate-bar-total-sales").sparkline(
      [1, 1, 0, 1, -1, -1, 1, -1, 0, 0, 1, 1, 0, -1, 1, -1],
      {
        type: "tristate",
        height: "30",
        posBarColor: "#009688",
        negBarColor: "#FF5722",
        barWidth: 4,
        barSpacing: 5,
        zeroAxis: false
      }
    );

    $("#sp-tristate-bar-total-revenue").sparkline(
      [1, 1, 0, 1, -1, -1, 1, -1, 0, 0, 1, 1, 0, -1, 1, -1],
      {
        type: "tristate",
        height: "30",
        posBarColor: "#00BCD4",
        negBarColor: "#E91E63",
        barWidth: 4,
        barSpacing: 5,
        zeroAxis: false
      }
    );

    // Total Revenue
    $("#sp-line-total-profit").sparkline(
      [14, 12, 4, 9, 3, 6, 11, 10, 13, 9, 14, 11, 16, 20, 15],
      {
        type: "line",
        width: "100%",
        height: "50px",
        lineColor: "#E91E63",
        fillColor: "",
        spotColor: "",
        minSpotColor: "",
        maxSpotColor: "",
        highlightSpotColor: "",
        highlightLineColor: "",
        chartRangeMin: 0,
        chartRangeMax: 20
      }
    );
  };

  var sparkResize;

  $(window).resize(function(e) {
    clearTimeout(sparkResize);
    sparkResize = setTimeout(sparkLineDraw, 500);
  });
  sparkLineDraw();
})(window, document, jQuery);
