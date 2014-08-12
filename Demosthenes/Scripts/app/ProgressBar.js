
// Gets all progress-bar elements in a page and, for each one of them,
// changes its class to show an appropriate color

$(document).ready(function() {
    $('.progress-bar').each(function () {
        var $progress = $(this)

        var studentsEnrolled = $progress.attr("aria-valuenow")
        var maximumEnrollment = $progress.attr("aria-valuemax")
 
        var enrollment = studentsEnrolled / maximumEnrollment

        if (enrollment < .26) {
            // $progress.addClass("progress-bar-info")
        } else if (enrollment < .51) {
            $progress.addClass("progress-bar-success")
        } else if (enrollment < .76) {
            $progress.addClass("progress-bar-warning")
        } else {
            $progress.addClass("progress-bar-danger")
        }

    })
})