$(document).ready(function() {
    var url = window.location;
    $('.navbar .nav').find('.active').removeClass('active');
    $('.navbar .nav li a').each(function() {
        if (this.href === url.href) {
            $(this).parent().addClass('active');
        }
    });
});