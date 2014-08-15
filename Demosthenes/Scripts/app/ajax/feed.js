(function () {

    var $labelLoading = $('#feed-loading');
    var $feed = $('#feed');

    function RequestFeed() {
        $.ajax({
            type: 'get',
            url: '/Posts',
        })
        .done(function (data) {
            $labelLoading.hide();

            $feed.hide().html(data).show(500);
        });
    }


    $(document).ready(function () {
        RequestFeed();
    });
}());