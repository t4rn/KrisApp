$(function () {

    $("input[data-otf-autocomplete]").each(createAutocomplete);

    function createAutocomplete() {
        var $input = $(this);

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        };

        $input.autocomplete(options);
    }

    function submitAutocompleteForm(event, ui) {

        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();
    };
});