/*=========================================================================================
    File Name: dashboard-ecommerce.js
    Description: dashboard-ecommerce
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

// Todo App variables
var todoNewTasksidebar = $(".todo-new-task-sidebar"),
  appContentOverlay = $(".app-content-overlay"),
  sideBarLeft = $(".sidebar-left"),
  todoTaskListWrapper = $(".todo-task-list-wrapper"),
  todoItem = $(".todo-item"),
  selectAssignLable = $(".select2-assign-label"),
  selectUsersName = $(".select2-users-name"),
  avatarUserImage = $(".avatar-user-image"),
  updateTodo = $(".update-todo"),
  addTodo = $(".add-todo"),
  markCompleteBtn = $(".mark-complete-btn"),
  newTaskTitle = $(".new-task-title"),
  taskTitle = $(".task-title"),
  noResults = $(".no-results"),
  assignedAvatarContent = $(".assigned .avatar .avatar-content"),
  todoAppMenu = $(".todo-app-menu");

// badge colors object define here for badge color
var badgeColors = {
  "Frontend": "badge-primary",
  "Backend": "badge-success",
  "Issue": "badge-danger",
  "Design": "badge-warning",
  "Wireframe": "badge-info",
}

$(function () {
  "use strict";

  // if it is not touch device
  if (!$.app.menu.is_touch_device()) {
    // Sidebar scrollbar
    if ($('.todo-application .sidebar-menu-list').length > 0) {
      var sidebarMenuList = new PerfectScrollbar('.sidebar-menu-list', {
        theme: "dark",
        wheelPropagation: false
      });
    }

    //  New task scrollbar
    if (todoNewTasksidebar.length > 0) {
      var todo_new_task_sidebar = new PerfectScrollbar('.todo-new-task-sidebar', {
        theme: "dark",
        wheelPropagation: false
      });
    }

    // Task list scrollbar
    if ($('.todo-application .todo-task-list').length > 0) {
      var sidebar_todo = new PerfectScrollbar('.todo-task-list', {
        theme: "dark",
        wheelPropagation: false
      });
    }
  }
  // if it is a touch device
  else {
    $('.sidebar-menu-list').css("overflow", "scroll");
    $('.todo-new-task-sidebar').css("overflow", "scroll");
    $('.todo-task-list').css("overflow", "scroll");
  }

  // Single Date Picker
  $('.pickadate').daterangepicker({
    singleDatePicker: true,
    showDropdowns: true,
    locale: {
      format: 'MM/DD'
    }
  });

  // dragable list
  dragula([document.getElementById("todo-task-list-drag")], {
    moves: function (el, container, handle) {
      return handle.classList.contains("handle");
    }
  });


  // select assigner
  selectUsersName.select2({
    placeholder: "Unassigned",
    dropdownAutoWidth: true,
    width: '100%'
  });

  // select label
  selectAssignLable.select2({
    dropdownAutoWidth: true,
    width: '100%'
  });

  // Sidebar scrollbar
  if ($('.todo-application .sidebar-menu-list').length > 0) {
    var sidebarMenuList = new PerfectScrollbar('.sidebar-menu-list', {
      theme: "dark",
      wheelPropagation: false
    });
  }

  //  New task scrollbar
  if (todoNewTasksidebar.length > 0) {
    var todo_new_task_sidebar = new PerfectScrollbar('.todo-new-task-sidebar', {
      theme: "dark",
      wheelPropagation: false
    });
  }

  // Task list scrollbar
  if ($('.todo-application .todo-task-list').length > 0) {
    var sidebar_todo = new PerfectScrollbar('.todo-task-list', {
      theme: "dark",
      wheelPropagation: false
    });
  }

  // New compose message compose field
  //var composeEditor = new Quill('.snow-container .compose-editor', {
  //  modules: {
  //    toolbar: '.compose-quill-toolbar'
  //  },
  //  placeholder: 'Add Description..... ',
  //  theme: 'snow'
  //});

  ////Assigner Comment Quill editor
  //var commentEditor = new Quill('.snow-container .comment-editor', {
  //  modules: {
  //    toolbar: '.comment-quill-toolbar'
  //  },
  //  placeholder: 'Write a Comment...',
  //  theme: 'snow'
  //});

  // **************Sidebar Left**************//
  // -----------------------------------------

  // Main menu toggle should hide app menu
  $('.menu-toggle').on('click', function () {
    sideBarLeft.removeClass('show');
    appContentOverlay.removeClass('show');
    todoNewTasksidebar.removeClass('show');
  });

  //on click of app overlay removeclass show from sidebar left and overlay
  appContentOverlay.on('click', function () {
    sideBarLeft.removeClass('show');
    appContentOverlay.removeClass('show');
  });

  // Add class active on click of sidebar menu's filters
  todoAppMenu.find(".list-group a").on('click', function () {
    var $this = $(this);
    todoAppMenu.find(".active").removeClass('active');
    $this.addClass("active")
  });

  // On sidebar close click hide sidebarleft and overlay
  $(".todo-application .sidebar-close-icon").on('click', function () {
    sideBarLeft.removeClass('show');
    appContentOverlay.removeClass('show');
  });

  // **************New Task sidebar**************//
  // ---------------------------------------------

  // add new task
  addTodo.on("click", function () {
    // check task assigned or not
    function renderAvatar(src) {
      if (src !== undefined) {
        return '<img src="' + src + '"alt="avatar" height="30" width="30" >'
      } else {
        return '<div class="avatar-content"><i class="ft-user font-medium-4"></i></div>'
      }
    };
    // if add task field are fiill and create a new task
    if (taskTitle.val().length > 0) {
      var titleTask = taskTitle.val(),
        selectAssign = $(".select2-users-name option:selected").val(),
        $randomID = Math.floor((Math.random() * 100) + Date.now()), //generate random id
        selectedVal = $(".select2-assign-label option:selected"),
        selectedTags = [];
      selectedVal.each(function () {
        selectedTags.push($(this).text());
      })
      var newTags = selectedTags.map(function (tag) {
        // map through every tag and create badges accordingly.
        return '<span class="badge ' + badgeColors[tag] + ' badge-pill ml-25"> ' + tag + ' </span>' //badge created here
      })

      var avatarSRC = todoTaskListWrapper.find("[data-name='" + selectAssign + "']").find(".avatar img").attr("src"); //Img src find if data name matches with list
      todoTaskListWrapper.append(
        // append a new task in task list
        '<li class="todo-item" data-name="' + selectAssign + '">' +
        '<div class="todo-title-wrapper d-flex justify-content-between align-items-center">' +
        '<div class="todo-title-area d-flex">' +
        '<i class="ft-more-vertical handle"></i>' +
        '<div class="custom-control custom-checkbox">' +
        '<input type="checkbox" class="custom-control-input" id="' + $randomID + '">' +
        '<label class="custom-control-label" for="' + $randomID + '"></label>' + '</div>' +
        '<p class="todo-title mx-50 m-0 truncate">' + titleTask + '</p>' +
        '</div>' +
        '<div class="todo-item-action d-flex align-items-center">' + newTags.join("") +
        '<div class="avatar ml-1">' + renderAvatar(avatarSRC) +
        '</div>' +
        '<a class="todo-item-favorite ml-50">' +
        '<i class="ft-star"></i>' + '</a>' +
        '<a class="todo-item-delete ml-50">' + '<i class="ft-trash-2"></i>' + '</a>' +
        '</div></div></li>');
      // new task sidebar, overlay hide
      todoNewTasksidebar.removeClass('show');
      appContentOverlay.removeClass('show');
      selectAssignLable.attr("disabled", "true");
    } else {
      // new task sidebar, overlay hide
      todoNewTasksidebar.removeClass('show');
      appContentOverlay.removeClass('show');
      selectAssignLable.attr("disabled", "true");
    }
  });

  // On Click of Close Icon btn, cancel btn and overlay remove show class from new task sidebar and overlay
  // and reset all form fields
  $(".close-icon, .cancel-btn, .app-content-overlay, .mark-complete-btn").on('click', function () {
    todoNewTasksidebar.removeClass('show');
    appContentOverlay.removeClass('show');
    setTimeout(function () {
      todoNewTasksidebar.find('textarea').val("");
      //var compose_editor = $(".compose-editor .ql-editor");
      //compose_editor[0].innerHTML = "";
      //var comment_editor = $(".comment-editor .ql-editor");
      //comment_editor[0].innerHTML = "";
      selectAssignLable.attr("disabled", "true");
    }, 100)
  });

  // on click of add label icon select 2 display
  $(".add-tags").on("click", function () {
    if (selectAssignLable.is('[disabled]')) {
      selectAssignLable.removeAttr("disabled");
    } else {
      selectAssignLable.attr("disabled", "true");
    }
  });

  // Update Task
  updateTodo.on("click", function () {
    todoNewTasksidebar.removeClass('show');
    appContentOverlay.removeClass('show');
    selectAssignLable.attr("disabled", "true");
  });

  // ************Rightside content************//
  // -----------------------------------------

  // Search filter for task list
  $(document).on("keyup", ".todo-search", function () {
    todoItem = $(".todo-item");
    $('.todo-item').css('animation', 'none')
    var value = $(this).val().toLowerCase();
    if (value != "") {
      todoItem.filter(function () {
        $(this).toggle($(this).text().toLowerCase().includes(value));
      });
      var tbl_row = $(".todo-item:visible").length; //here tbl_test is table name

      //Check if table has row or not
      if (tbl_row == 0) {
        if (!noResults.hasClass('show')) {
          noResults.addClass('show');
        }
      } else {
        noResults.removeClass('show');
      }
    } else {
      // If filter box is empty
      todoItem.show();
      if (noResults.hasClass('show')) {
        noResults.removeClass('show');
      }
    }
  });
  // on Todo Item click show data in sidebar
  var globalThis = ""; // Global variable use in multiple function
  todoTaskListWrapper.on('click', '.todo-item', function (e) {
    var $this = $(this);
    globalThis = $this;

      blockUI();


      var taskId = $this.data("task-id")
      $.ajax({
          url: "/tasks/details?id=" + taskId,
          method: "get",
          data: {
              modal:true
          },
          before: function () {
              
          },
          success: function (response) {
              unblockUI();
              $("#DivModalTarget2").html(response)

              // update button has remove class d-none & add class d-none in add todo button
            
          }
      });


   
    

  }).on('click', '.todo-item-favorite', function (e) {
    e.stopPropagation();
      var $this = $(this);
      $this.toggleClass("warning");
      $this.find("i").toggleClass("bxs-star");

      var taskId = $this.data("task-id")
      
      $.ajax({
          url: "/tasks/markFavourite?id=" + taskId,
          method: "get",
          data: {

          },
          before: function () {
          },
          success: function (response) {
              
              $("#DivModalTarget").html(response);
          }
      });

      
  }).on('click', '.todo-item-delete', function (e) {
      e.stopPropagation();
      
      var $this = $(this);
      var taskId = $this.data("task-id")

      Swal.fire({
          title: 'Are you sure?',
          text: "This will cancel the selected task!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, cancel it!'
      }).then((result) => {
          if (result.isConfirmed) {

              $.ajax({
                  url: "/tasks/deleteTask?id=" + taskId,
                  method: "get",
                  data: {

                  },
                  before: function () {
                  },
                  success: function (response) {

                      $this.closest('.todo-item').remove();
                      $("#DivModalTarget2").html(response);

                  }
              });

              
             
          }
      })

      



      
  }).on('click', '.custom-checkbox', function (e) {
    e.stopPropagation();
  });

  // Complete task strike through
  todoTaskListWrapper.on('click', ".todo-item .custom-control-input", function (e) {

      
      var $this = $(this);
      var taskId = $this.data("task-id")
      
      $.ajax({
          url: "/tasks/markCompleted?id=" + taskId,
          method: "get",
          data: {

          },
          before: function () {
          },
          success: function (response) {

              $("#DivModalTarget").html(response);
              $this.closest('.todo-item').toggleClass("completed");

          }
      });
      
  });

  // Complete button click action
  markCompleteBtn.on("click", function () {
        globalThis.addClass("completed");
        globalThis.find(".custom-control-input").prop("checked", true);
      selectAssignLable.attr("disabled", "true");


      
  });

  // Todo sidebar toggle
  $('.sidebar-toggle').on('click', function (e) {
    e.stopPropagation();
    sideBarLeft.toggleClass('show');
    appContentOverlay.addClass('show');
  });

  // sorting task list item
  $(".ascending").on("click", function () {
    todoItem = $(".todo-item");
    $('.todo-item').css('animation', 'none')
    todoItem.sort(sort_li).appendTo(todoTaskListWrapper);

    function sort_li(a, b) {
      return ($(b).find('.todo-title').text()) < ($(a).find('.todo-title').text()) ? 1 : -1;
    }
  });

  // descending sorting
  $(".descending").on("click", function () {
    todoItem = $(".todo-item");
    $('.todo-item').css('animation', 'none')
    todoItem.sort(sort_li).appendTo(todoTaskListWrapper);

    function sort_li(a, b) {
      return ($(b).find('.todo-title').text()) > ($(a).find('.todo-title').text()) ? 1 : -1;
    }
  });
});

$(window).on("resize", function () {
  // remove show classes from sidebar and overlay if size is > 992
  if ($(window).width() > 992) {
    if (appContentOverlay.hasClass('show')) {
      sideBarLeft.removeClass('show');
      appContentOverlay.removeClass('show');
      todoNewTasksidebar.removeClass("show");
    }
  }
});
