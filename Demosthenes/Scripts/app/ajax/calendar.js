(function () {

    // possible days that appear in the calendar, matching the enum DayOfWeek
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

    function Calendar(times, classes) {
        this.ClassesOnDay = [];
        /**
         * Add classes into the schedule.
         */
        this.AddClasses = function (classes) {
            for (var i = 0; i < classes.length; i++) {
                this.AddClass(classes[i]);
            }
            return this;
        }

        /**
         * Add a class into the schedule.
         */
        this.AddClass = function (c) {
            for (var i = 0; i < c.Schedules.length; i++) {
                var starting = c.Schedules[i].Starting.Ticks.toString();
                this.ClassesOnDay[c.Schedules[i].Day][starting] = c;
            }
        }

        this.ExportCalendar = function () {
            var $calendarElement = $('<table />')
                .addClass('table table-hover table-striped table-bordered');

            var $header = $('<thead />')
                .append($('<tr />'));

            var $body = $('<tbody />');

            // inserting titles for days
            $header
                    .children().first()
                    .append($('<th />')
                        .html('#'));

            for (var i = 0; i < days.length; i++) {
                $header
                    .children().first()
                    .append($('<th />')
                        .html(days[i]));
            }

            for (i = 0; i < times.length; i++) {
                var time = new Date(0, 0, 0, times[i].Hours, times[i].Minutes);
                var ticks = times[i].Ticks;

                // inserting titles for starting times
                var $row = $('<tr />')
                    .append($('<td />')
                        .html(time.toTimeString().substring(0, 5)))

                var c
                for (var j = 0; j < days.length; j++) {
                    var $cell = $('<td />');

                    if (c = this.ClassesOnDay[j][ticks]) {
                        $cell
                            .append(c.CourseTitle + ' <span class="label label-default" title="Class ' + c.Id + '">' + c.Id + '</span>');
                    }
                    $row.append($cell);
                }

                $body.append($row);
            }

            $calendarElement.append($header);
            $calendarElement.append($body);

            // return calendar DOM element created
            return $calendarElement;
        }

        // # constructor region
        for (var i = 0; i < days.length; i++) {
            this.ClassesOnDay.push({});
        }

        this.AddClasses(classes);
        // # end of constructor region
    }

    function RequestCalendar() {
        var $calendar, $calendarLoadBtn, $calendarContainer;

        $calendar  = $('#calendar');
        $loadBtn   = $('#calendar-load-btn');
        $container = $('#calendar-container');

        $loadBtn.button('loading')

        $.ajax({
            type: 'get',
            url: '/Classes/Calendar',
        })
        .done(function (data) {
            $loadBtn.hide()
            .children('#calendar-load-text')
            .attr({
                class: 'disabled',
                disabled: 'disabled'
            })
            .html('Loading calendar...');

            $container
                .hide()
                .html(new Calendar(data.times, data.classes).ExportCalendar())
                .show(500);
        })
        .fail(function (data) {
            $loadBtn.show();
        })
        .always(function () {
            $loadBtn.button('reset');
        });
    }

    $(document).ready(function () {
        $('#calendar-load-btn').click(function () {
            RequestCalendar();
        });

        RequestCalendar();
    });
}());
