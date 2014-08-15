(function () {

    var $form,
        $list,
        $q,
        searchHandler    = undefined,
        previousValue    = undefined,
        timeBeforeSearch = 500;

    function Search() {
        $.ajax({
            url: $form.attr('action'),
            type: $form.attr('method'),
            data: $form.serialize()
        })
        .always(function (data) {
            $list.hide().html(data).fadeIn();
        });

        return false;
    }

    function SearchAfter() {
        if (searchHandler) {
            clearTimeout(searchHandler);
            searchHandler = null;
        }

        if ($q.val() != previousValue) {
            previousValue = $q.val();
            searchHandler = setTimeout(Search, timeBeforeSearch);
        }
    }

    function GetPage() {
        var $a = $(this);

        var options = {
            url: $a.attr('href'),
            type: 'get',
            data: $form.serialize(),
        };

        $
            .ajax(options)
            .done(function (data) {
                $list.hide().html(data).fadeIn();
            });

        return false;
    }

    $(document).ready(function () {
        $form = $('#form-search');
        $list = $('#list-search');
        $q    = $('#q');

        $list.on('click', '.pagedList a', GetPage);
        $form.submit(Search);
    });
}());