(function () {

    function RequestFeed() {
        var $labelloading, $feed;

        $labelLoading = $('#feed-loading');
        $feed         = $('#feed');

        $.ajax({
            type: 'get',
            url: '/Posts',
        })
        .done(function (data) {
            $labelLoading.hide();

            $feed.hide().html(data).fadeIn();
        });
    }


    $(document).ready(function () {
        RequestFeed();
    });
}());