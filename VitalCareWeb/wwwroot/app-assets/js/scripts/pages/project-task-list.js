/*=========================================================================================
    File Name: project-task-list.js
    Description: Project task datables configurations
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


$(document).ready(function () {
  /********************************************
  *        js of Order by the grouping        *
  ********************************************/

  var groupingTable = $('.row-grouping').DataTable({
    responsive: false,
    autoWidth: false,
     rowReorder: true,
    "columnDefs": [
      { "orderable": false, "targets": '_all' }
    ],
    "displayLength": 10,
    "drawCallback": function (settings) {
      var api = this.api();
      var rows = api.rows({ page: 'current' }).nodes();
      var last = null;

      api.column(4, { page: 'current' }).data().each(function (group, i) {
        if (last !== group) {
          $(rows).eq(i).before(
            '<tr class="group"><td colspan="7">' + group + '</td></tr>'
          );

          last = group;
        }
      });
    }
  });

  $('.row-grouping tbody').on('click', 'tr.group', function () {
      var currentOrder = groupingTable.order()[1];
      console.log(currentOrder);

    if (currentOrder[0] === 4 && currentOrder[1] === 'asc') {
        groupingTable.order([4, 'desc']).draw();
    }
    else {
        groupingTable.order([4, 'asc']).draw();
    }
  });

  //$('select').select2({
  //  dropdownAutoWidth: true,
  //  width: '100%'
  //});

  // checkbox check all on click
  $("input#checkboxsmallall").on("click", function () {
    if ($("input:checked#checkboxsmallall").length > 0) {
      $("input:not(:checked)").prop('checked', true);
    }
    else {
      $("input:checked").prop('checked', false);
    }
  });

});
