$(document).ready(function() {
    initMaterialize();
    $(".button-collapse").sideNav();

    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.notificationsHub;
    // Create a function that the hub can call back to display messages.
    chat.client.showSuccess = function (message) {
        showSuccess(message);
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        $('.btn').click(function () {
            // Call the Send method on the hub.
            chat.server.send("pesho");
        });
    });
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
    $('.collapsible').collapsible();
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
    initMaterialize();
    Materialize.toast(message, 5000, 'green');
}
