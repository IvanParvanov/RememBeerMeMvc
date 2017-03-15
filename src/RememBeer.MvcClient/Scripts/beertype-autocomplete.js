(function () {
    $("#type")
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
})();