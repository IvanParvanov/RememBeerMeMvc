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

    $.ajax({
        type: "POST",
        url: '/reviews/changeimage',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            var $this = $("#imgUploadForm");
            var parent = $this.parent();
            parent.empty();

            var imgEl = $("<img>").addClass("materialboxed responsive-img").attr("src", response.url);
            parent.prepend(imgEl);

            showSuccess('Image has been successfully updated!');
        },
        error: function (error) {
            handleAjaxError(error);
        }
    });
});