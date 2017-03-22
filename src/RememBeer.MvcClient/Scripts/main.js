var signalR;

$(document).ready(function () {
    initMaterialize();
    $(".button-collapse").sideNav();

    $(document).on(
        'click',
        '#toast-container .toast .close, #notify .close',
        function () {
            var target = $(this).attr("data-target");
            if (target) {
                $(target).fadeOut();
            } else {
                $(this).parent().fadeOut();
            }
        });

    $("#content").on(
        'click',
        '.pagination',
        function (ev) {
            var $target = $(ev.target);
            if ($target.is("a")) {
                var href = $target.attr("href");
                history.pushState({}, "", href);
            }
        });

    signalR = $.connection.notificationsHub;

    signalR.client.showNotification = function (message, username, lat, lon) {
        message = htmlEncode(message);
        var text = '<p>';
        if (lat && lon) {
            lat = encodeURIComponent(lat);
            lon = encodeURIComponent(lon);
            text += "<a class=\"white-text\" target=\"_blank\" href=\"https://www.google.com/maps/dir//" + lat + "," + lon + "\"><i class=\"fa fa-2x fa-map-marker\"></i> </a>";
        }
        text += "<strong>" + username + "</strong> says: <br />" + message;

        text = text + "</p>";
        notifier.showNotification(text);
    };

    signalR.client.onFollowerReviewCreated = function (id, user) {
        var text = "<a class=\"white-text\" target=\"_blank\" href=\"/reviews/details/" +
            id +
            "\">User <strong>" +
            user +
            "</strong> has posted a new review.</a>";
        notifier.showNotification(text);
    }

    $.connection.hub.start().done(function () {
        $("#btn-send-message").click(eventManager.sendMessage);
    });
});

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
    $.validator.unobtrusive.parse($("form"));
    $('.modal').modal();
    $('.materialboxed').materialbox();
    $('.collapsible').collapsible();
}

var notifier = (function () {
    function prepareText(text) {
        return text + "<a class='close'><i class=\"fa fa-lg fa-times\" aria-hidden=\"true\"></i></a>";
    }

    return {
        showFailure: function (message) {
            initMaterialize();
            Materialize.toast(
                prepareText(message),
                5000,
                'red');
        },
        showSuccess: function (message) {
            initMaterialize();
            Materialize.toast(
                prepareText(message),
                5000,
                'green');
        },
        handleAjaxError: function (response) {
            var message;
            if (!response || !response.statusText || response.statusText.toLowerCase() === "error") {
                message = 'There was a problem with your request. Please try again.';
            } else {
                message = response.statusText;
            }

            notifier.showFailure(message);
        },
        showNotification: function (message) {
            Materialize.toast(
                prepareText(message),
                10000000,
                'blue-grey small');
        }
    }
})();

var requester = {
    postJson: function (url, formData, success) {
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: success,
            error: function (error) {
                handleAjaxError(error);
            }
        });
    },
    post: function (url, formData, success) {
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            contentType: false,
            processData: false,
            success: success,
            error: function (error) {
                handleAjaxError(error);
            }
        });
    }
};

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

var eventManager = {
    notifyReviewCreated: function () {
        initMaterialize();
        signalR.server.notifyReviewCreated();
    },
    sendMessage: function () {
        function processMessage(message, lat, lon) {
            if (message && message.replace(/\s/g, "").length > 0) {
                signalR.server.sendMessage(message, lat, lon).fail(function (error) {
                    notifier.showFailure('Message could not be sent! Try again...');
                })
                    .done(function () {
                        $('#message').val('');
                        $('#notify').fadeOut();
                        notifier.showSuccess('Message has been sent!');
                    });
            }
        }

        function handleGeoError(message) {
            $("#lbl-location").html("Geolocation is not supported by this browser.");
            var $cb = $("#cb-location");
            $cb.prop("checked", "");
            if ($cb.is("disabled")) {
                processMessage(message, null, null);
            } else {
                $cb.attr("disabled", "disabled");
            }
        }

        var $input = $('#message');
        var message = $input.val();

        if ($('#cb-location').is(':checked')) {
            if (!navigator.geolocation) {
                handleGeoError(message);
            }

            navigator.geolocation.getCurrentPosition(
                function (position) {
                    processMessage(message, position.coords.latitude, position.coords.longitude);
                },
                function (error) {
                    handleGeoError(message);
                });
        } else {
            processMessage(message, null, null);
        }

    },
    attachImageUpload: function () {
        $("#imgUploadForm").change(function () {
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
                function (response) {
                    var $this = $("#imgUploadForm");
                    var parent = $this.parent();
                    parent.empty();

                    var imgEl = $("<img>").addClass("materialboxed responsive-img").attr("src", response.url);
                    parent.prepend(imgEl);

                    notifier.showSuccess('Image has been successfully updated!');
                });
        });
    },
    attachCreateReview: function () {
        $("#create-form").submit(function (ev) {
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
                    function (response) {
                        $("#loading").hide();
                        $('.modal').modal('close');
                        $("#content").html(response);
                        eventManager.notifyReviewCreated();
                    });
            }
        });
    },
    attachBeerTypeAutocomplete: function () {
        $(".type-autocomplete")
            .autocomplete({
                serviceUrl: "/breweries/types",
                paramName: "name",
                dataType: "json",
                showNoSuggestionNotice: true,
                transformResult: function (response) {
                    return {
                        suggestions: $.map(
                            response.data,
                            function (dataItem) {
                                return {
                                    value: dataItem.Type,
                                    data: dataItem.Id
                                };
                            })
                    };
                },
                onSelect: function (suggestion) {
                    $("#TypeId").val(suggestion.data);
                }
            });
    },
    attachBeerAutocomplete: function () {
        $(".beer-autocomplete")
            .autocomplete({
                serviceUrl: "/Beers",
                paramName: "name",
                dataType: "json",
                showNoSuggestionNotice: true,
                transformResult: function (response) {
                    return {
                        suggestions: $.map(
                            response,
                            function (dataItem) {
                                return {
                                    value: dataItem.Name + ", " + dataItem.BreweryName,
                                    data: dataItem.Id
                                };
                            })
                    };
                },
                onSelect: function (suggestion) {
                    $("#hiddenBeerId").val(suggestion.data);
                }
            });

        $('#createNew').modal('close');
        $('.modal-overlay').remove();
    }
}