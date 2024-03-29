﻿$(function () {

    $.datepicker.regional['ru'] = {
        closeText: 'Закрыть',
        prevText: 'Пред',
        nextText: 'След',
        currentText: 'Сегодня',
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
            'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
            'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
        dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
        dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
        dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        weekHeader: 'Не',
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: '',
        minDate: '-0D',
        maxDate: '+3M'
    };

    $("#DateOfDeparture").datepicker($.datepicker.regional["ru"]);

});

$(function () {
    $("[completesource]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("completesource") });
    });
});


    $(document).ready(function ()
{
        $('#registerFormId').validate({
            errorClass: 'help-block', 
            errorElement: 'div',
            errorPlacement: function (error, e) {
                e.parents('.form-group > div').append(error);
            },
            highlight: function (e) {

                $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
                $(e).closest('.help-block').remove();
            },
            success: function (e) {
                e.closest('.form-group').removeClass('has-success has-error');
                e.closest('.help-block').remove();
            },
            rules: {
                'FromStation': {
                    required: true,
                },

                'ToStation': {
                    required: true,

                },

                'DateOfDeparture': {
                    required: true,

                }
            },
            messages: {
                'FromStation': 'Будь ласка, введіть станцію.',

                'ToStation': {
                    required: 'Будь ласка, введіть станцію.',
                },

                'DateOfDeparture': {
                    required: 'Будь ласка, оберіть дату.',
                }
            }
        });
});
