//
//
$(document).ready(function() {
    $('.btn-submitter').click(function () {
        console.log('.btn-submitter will be submitted.');

        $(this).parents('form:first').submit();
        return false;
    });
});