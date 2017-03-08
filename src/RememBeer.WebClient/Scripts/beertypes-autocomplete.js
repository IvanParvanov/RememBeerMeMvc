function pageLoad(sender, args) {
    $("#MainContent_BeerTypeTextbox")
        .autocomplete({
            serviceUrl: "/api/BeerTypes",
            paramName: "type",
            dataType: "json",
            showNoSuggestionNotice: true,
            transformResult: function (response) {
                return {
                    suggestions: $.map(
                        response,
                        function (dataItem) {
                            return {
                                value: dataItem.Type,
                                data: dataItem.Id
                            };
                        })
                };
            },
            onSelect: function (suggestion) {
                $("#MainContent_HiddenBeerTypeId").val(suggestion.data);
            }
        });
}