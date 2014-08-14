//
//
$(document).ready(function() {
    $('.btn-submitter').click(function () {
        $(this).parents('form:first').submit();
        return false;
    });
});