$(document).ready(function() {
    initMaterialize();
    $(".button-collapse").sideNav();
});

function scrollToTop() {
    window.scrollTo(0, 0);
}

function initMaterialize() {
    $(".dropdown-button").dropdown({
        inDuration: 300,
        outDuration: 225,
        hover: true,
        belowOrigin: true,
        alignment: 'right'
    });
    Materialize.updateTextFields();
    $('select').material_select();
    $.validator.unobtrusive.parse($("form"));
    $('.modal').modal();
    $('.materialboxed').materialbox();

    //$('ul.pagination').on(
    //    "click",
    //    function(ev) {
    //        var $target = $(ev.target);

    //        if (history.pushState && $target.is("a")) {
    //            var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname + $target.attr("href");
    //            window.history.pushState({ path: newurl }, '', newurl);
    //        }
    //    });
}

function handleAjaxError(response) {
    var message;
    if (!response || !response.statusText || response.statusText.toLowerCase() === "error") {
        message = 'There was a problem with your request. Please try again.';
    } else {
        message = response.statusText;
    }
    
    Materialize.toast(message, 5000, 'red');
}

function showSuccess(message) {
    Materialize.toast(message, 5000, 'green');
}