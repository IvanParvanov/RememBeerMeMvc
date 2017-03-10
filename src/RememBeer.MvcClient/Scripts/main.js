$(document).ready(function() {
    $(".dropdown-button").dropdown({
        inDuration: 300,
        outDuration: 225,
        hover: true,
        belowOrigin: true,
        alignment: 'right'
    });

    $('.modal').modal();
    $('select').material_select();
});

function scrollToTop() {
    window.scrollTo(0, 0);
}

function initMaterialize() {
    Materialize.updateTextFields();
    $('select').material_select();
}

