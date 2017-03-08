function pageLoad(sender, args) {
    $("#MainContent_BeerNameTextbox")
        .autocomplete({
            serviceUrl: "/api/Beers",
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
                $("#MainContent_HiddenBeerId").val(suggestion.data);
            }
        });

    $('#createNew').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}