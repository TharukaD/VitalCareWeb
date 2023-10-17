blockUI = function () {
    $.blockUI({ message: '<h5 class="pt-1">Please wait...</h5>', baseZ: 2000 });
};

unblockUI = function () {
    $.unblockUI();
};

reloadPage = function (data) {

    setTimeout(function () {
        window.location.reload();
    },
        1000);

};

onAjaxSuccess = function (data) {

    if (data.success === true) {
        toastr.success(data.message, 'Successful', { "showMethod": "fadeIn", "hideMethod": "fadeOut", timeOut: 2000 });

        reloadPage();
    } else {
        toastr.success(data.message, 'Successful', { "showMethod": "fadeIn", "hideMethod": "fadeOut", timeOut: 2000 });

    }
};

onAjaxError = function (xhr) {
    toastr.error("An error occurred.", 'Error', { "showMethod": "fadeIn", "hideMethod": "fadeOut", timeOut: 2000 });
}

onError = function (data) {


    $.notify({
        icon: "mdi mdi-alert",
        title: "Error",
        message: "An error occurred"
    },
        {
            placement: {
                align: "right",
                from: "top"
            },
            showProgressbar: false,
            timer: 500,
            z_index: 10000,
            type: "danger",
            template:
                '<div data-notify="container" class=" bootstrap-notify alert " role="alert" style="min-width:380px">' +
                '<div class="progress" data-notify="progressbar">' +
                '<div class="progress-bar bg-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                "</div>" +
                '<div class="media "> <div class="avatar m-r-10 avatar-sm"> <div class="avatar-title bg-{0} rounded-circle"><span data-notify="icon"></span></div> </div>' +
                '<span class="opacity-75 pt-1" data-notify="message">{2}</span></div>' +
                '<a href="{3}" target="{4}" data-notify="url"></a>' +
                ' <button type="button" aria-hidden="true" class="close" data-notify="dismiss"><span>x</span></button></div></div>'

        });
};

clearModal = function () {

    $(".modal-backdrop").remove();
};

toastSuccess = function (message) {

    toastr.success(message, 'Successful', { "showMethod": "fadeIn", "hideMethod": "fadeOut", timeOut: 20000 });
};

toastError = function (message) {
   
    toastr.error(message, 'Error', { "showMethod": "fadeIn", "hideMethod": "fadeOut", timeOut: 20000 });


};

function setUrlParameter(url, key, value) {
    var key = encodeURIComponent(key),
        value = encodeURIComponent(value);

    var baseUrl = url.split("?")[0],
        newParam = key + "=" + value,
        params = "?" + newParam;

    if (url.split("?")[1] == undefined) { // if there are no query strings, make urlQueryString empty
        urlQueryString = "";
    } else {
        urlQueryString = "?" + url.split("?")[1];
    }

    // If the "search" string exists, then build params from it
    if (urlQueryString) {
        var updateRegex = new RegExp("([\?&])" + key + "[^&]*");
        var removeRegex = new RegExp("([\?&])" + key + "=[^&;]+[&;]?");

        if (typeof value === "undefined" || value === null || value === "") { // Remove param if value is empty
            params = urlQueryString.replace(removeRegex, "$1");
            params = params.replace(/[&;]$/, "");

        } else if (urlQueryString.match(updateRegex) !== null) { // If param exists already, update it
            params = urlQueryString.replace(updateRegex, "$1" + newParam);

        } else if (urlQueryString == "") { // If there are no query strings
            params = "?" + newParam;
        } else { // Otherwise, add it to end of query string
            params = urlQueryString + "&" + newParam;
        }
    }

    // no parameter was set so we don't need the question mark
    params = params === "?" ? "" : params;

    return baseUrl + params;
}





//common functions:

function setUrlParameter(url, key, value) {
    console.log(url);
    console.log(key);
    console.log(value);

    var key = encodeURIComponent(key),
        value = encodeURIComponent(value);

    var baseUrl = url.split('?')[0],
        newParam = key + '=' + value,
        params = '?' + newParam;

    if (url.split('?')[1] == undefined) { // if there are no query strings, make urlQueryString empty
        urlQueryString = '';
    } else {
        urlQueryString = '?' + url.split('?')[1];
    }

    // If the "search" string exists, then build params from it
    if (urlQueryString) {
        var updateRegex = new RegExp('([\?&])' + key + '[^&]*');
        var removeRegex = new RegExp('([\?&])' + key + '=[^&;]+[&;]?');

        if (typeof value === 'undefined' || value === null || value === '') { // Remove param if value is empty
            params = urlQueryString.replace(removeRegex, "$1");
            params = params.replace(/[&;]$/, "");

        } else if (urlQueryString.match(updateRegex) !== null) { // If param exists already, update it
            params = urlQueryString.replace(updateRegex, "$1" + newParam);

        } else if (urlQueryString == '') { // If there are no query strings
            params = '?' + newParam;
        } else { // Otherwise, add it to end of query string
            params = urlQueryString + '&' + newParam;
        }
    }

    // no parameter was set so we don't need the question mark
    params = params === '?' ? '' : params;

    return baseUrl + params;
}



// Credit David Walsh (https://davidwalsh.name/javascript-debounce-function)
// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.
function debounce(func, wait, immediate) {
    var timeout;

    return function executedFunction() {
        var context = this;
        var args = arguments;

        var later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };

        var callNow = immediate && !timeout;

        clearTimeout(timeout);

        timeout = setTimeout(later, wait);

        if (callNow) func.apply(context, args);
    };
}

(function (window, undefined) {
    'use strict';

    
    $.fn.select2.defaults.set("theme", "bootstrap4");

    //fix modal backdrop when multiple dialogs are shown.
    $(document).on("show.bs.modal",
        ".modal",
        function () {
            var zIndex = 1040 + (10 * $(".modal:visible").length);
            $(this).css("z-index", zIndex);
            setTimeout(function () {
                $(".modal-backdrop").not(".modal-stack").css("z-index", zIndex - 1).addClass("modal-stack");
            },
                0);
        });


    $('a[data-toggle="tab"]').on('shown.bs.tab',
        function (e) {
            var newUrl = setUrlParameter(location.href, "tab", $(e.target).attr("aria-controls"));
            history.pushState(null, null, newUrl);

        });

    var searchParams = new URLSearchParams(window.location.search);
    var tab = searchParams.get("tab");

    $("a[href='#" + tab + "']").trigger("click");




   
})(window);