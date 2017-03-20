var signalR;

$(document).ready(function () {
    initMaterialize();
    $(".button-collapse").sideNav();

    $(document).on('click', '#toast-container .toast .close', function () {
        $(this).parent().fadeOut(function () {
            $(this).remove();
        });
    });

    $("#content").on('click', '.pagination', function(ev) {
        var $target = $(ev.target);
        if ($target.is("a")) {
            var href = $target.attr("href");
            history.pushState({}, "", href);
        }
    });

    signalR = $.connection.notificationsHub;
    signalR.client.showSuccess = function (message) {
        showSuccess(message);
    };

    signalR.client.onFollowerReviewCreated = function (id, user) {
        var text = "<a class=\"white-text\" target=\"_blank\" href=\"/reviews/details/" + id + "\">User <strong>" + user + "</strong> has posted a new review.</a>";
        showNotification(text);
    }

    $.connection.hub.start().done(function() {

    });
});

function showNotification(text) {
    Materialize.toast(text + "<a class='close'><i class=\"fa fa-lg fa-times\" aria-hidden=\"true\"></i></a>", 10000000, 'blue-grey');
}

function updateModal(_this) {
    var id = $(_this).attr("data-id");
    $("#hidden-review-id").val(id);
    $("#form1").attr("data-ajax-update", "#review-" + id);
}

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

var requester = {
    postJson: function(url, formData, success) {
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: success,
            error: function(error) {
                handleAjaxError(error);
            }
        });
    },
    post: function(url, formData, success) {
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            contentType: false,
            processData: false,
            success: success,
            error: function(error) {
                handleAjaxError(error);
            }
        });
    }
};

var eventManager = {
    notifyReviewCreated: function () {
        signalR.server.notifyReviewCreated();
    },
    attachImageUpload: function() {
        $("#imgUploadForm").change(function() {
            var formData = new FormData();
            var input = document.getElementById("imgUpload");
            var totalFiles = input.files.length;
            for (var i = 0; i < totalFiles; i++) {
                var file = input.files[i];

                formData.append("image", file);
            }
            var antiforgery = $(this).find('input[name=__RequestVerificationToken]').val();
            formData.append('__RequestVerificationToken', antiforgery);

            formData.append("id", $("#review-id").val());

            requester.postJson(
                '/reviews/changeimage',
                formData,
                function(response) {
                    var $this = $("#imgUploadForm");
                    var parent = $this.parent();
                    parent.empty();

                    var imgEl = $("<img>").addClass("materialboxed responsive-img").attr("src", response.url);
                    parent.prepend(imgEl);

                    showSuccess('Image has been successfully updated!');
                });
        });
    },
    attachCreateReview: function() {
        $("#create-form").submit(function(ev) {
            ev.preventDefault();
            ev.stopPropagation();
            var $form = $(this);
            if ($form.valid()) {
                var form = $('#create-form')[0];
                var formData = new FormData(form);
                $("#loading").show();

                requester.post(
                    '/reviews',
                    formData,
                    function(response) {
                        $("#loading").hide();
                        $('.modal').modal('close');
                        $("#content").html(response);
                        eventManager.notifyReviewCreated();
                    });
            }
        });
    },
    attachBeerTypeAutocomplete: function() {
        $(".type-autocomplete")
            .autocomplete({
                serviceUrl: "/breweries/types",
                paramName: "name",
                dataType: "json",
                showNoSuggestionNotice: true,
                transformResult: function(response) {
                    return {
                        suggestions: $.map(
                            response.data,
                            function(dataItem) {
                                return {
                                    value: dataItem.Type,
                                    data: dataItem.Id
                                };
                            })
                    };
                },
                onSelect: function(suggestion) {
                    $("#TypeId").val(suggestion.data);
                }
            });
    },
    attachBeerAutocomplete: function() {
        $(".beer-autocomplete")
            .autocomplete({
                serviceUrl: "/Beers",
                paramName: "name",
                dataType: "json",
                showNoSuggestionNotice: true,
                transformResult: function(response) {
                    return {
                        suggestions: $.map(
                            response,
                            function(dataItem) {
                                return {
                                    value: dataItem.Name + ", " + dataItem.BreweryName,
                                    data: dataItem.Id
                                };
                            })
                    };
                },
                onSelect: function(suggestion) {
                    $("#hiddenBeerId").val(suggestion.data);
                }
            });

        $('#createNew').modal('close');
        $('.modal-overlay').remove();
    }
}