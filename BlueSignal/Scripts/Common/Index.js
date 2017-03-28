/*On Page Load Register the datepickers*/
$(document).ready(function () {
    DatePickerInitilization();
});






function DatePickerInitilization()
{
    $('[customdatepicker=datepickerwithtime]').each(function (item) {
        $(this).datetimepicker();
    });


    $('[customdatepicker=datepickerwithoutTime]').each(function (item) {
        $(this).datetimepicker(
            { format: 'L' }
            );
    });

    $('[customdatepicker=datepickerStartDate]').each(function (item) {
        $(this).datetimepicker(
            { format: 'L' }
            );
    });

    $('[customdatepicker=datepickerEnd]').each(function (item) {
        $(this).datetimepicker(
            {
                format: 'L',
                useCurrent: false
            }
            );
    });



    $("[customdatepicker=datepickerStartDate").on("dp.change", function (e) {
        $('[customdatepicker=datepickerEnd]').data("DateTimePicker").minDate(e.date);
    });
    $("[customdatepicker=datepickerEnd]").on("dp.change", function (e) {
        $('[customdatepicker=datepickerStartDate').data("DateTimePicker").maxDate(e.date);
    });
}