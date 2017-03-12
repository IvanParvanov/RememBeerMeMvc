$("#beerName")
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