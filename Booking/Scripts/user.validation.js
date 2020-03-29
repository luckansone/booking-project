$(document).ready(function () {
    $('#personFormId').validate({
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
            '[0].Name': {
                required: true,
            },

            '[0].Surname': {
                required: true,

            },

            '[0].Patronymic': {
                required: true,

            },
            '[0].DateOfBirth': {
                required: true
            },

            '[0].Email': {
                required: true,
                email: true

            },

            '[0].Phone': {
                required: true,
            },
            '[1].Name': {
                required: true,
            },

            '[1].Surname': {
                required: true,

            },

            '[1].Patronymic': {
                required: true,

            },
            '[1].DateOfBirth': {
                required: true
            },

            '[1].Email': {
                required: true,
                email: true

            },

            '[1].Phone': {
                required: true,
            },
            '[2].Name': {
                required: true,
            },

            '[2].Surname': {
                required: true,

            },

            '[2].Patronymic': {
                required: true,

            },
            '[2].DateOfBirth': {
                required: true
            },

            '[2].Email': {
                required: true,
                email: true

            },

            '[2].Phone': {
                required: true,
            },
            '[3].Name': {
                required: true,
            },

            '[3].Surname': {
                required: true,

            },

            '[3].Patronymic': {
                required: true,

            },
            '[3].DateOfBirth': {
                required: true
            },

            '[3].Email': {
                required: true,
                email: true

            },

            '[3].Phone': {
                required: true,
            }
        },
        messages: {
            '[0].Name': 'Будь ласка, введіть імя',

            '[0].Surname': {
                required: 'Будь ласка, введіть прізвище.'
            },

            '[0].Patronymic': {
                required: 'Будь ласка, введіть по батькові.',
            },

            '[0].DateOfBirth': {
                required: 'Будь ласка, оберіть дату.',
                email: 'Будь ласка, введіть правильний адрес.'
            },

            '[0].Email': {
                required: 'Будь ласка, введіть email.',
            },

            '[0].Phone': {
                required: 'Будь ласка, введіть телефон.',
            },

            '[1].Name': 'Будь ласка, введіть імя',

            '[1].Surname': {
                required: 'Будь ласка, введіть прізвище.'
            },

            '[1].Patronymic': {
                required: 'Будь ласка, введіть по батькові.',
            },

            '[1].DateOfBirth': {
                required: 'Будь ласка, оберіть дату.',
                email: 'Будь ласка, введіть правильний адрес.'
            },

            '[1].Email': {
                required: 'Будь ласка, введіть email.',
            },

            '[1].Phone': {
                required: 'Будь ласка, введіть телефон.',
            },
            '[2].Name': 'Будь ласка, введіть імя',

            '[2].Surname': {
                required: 'Будь ласка, введіть прізвище.'
            },

            '[2].Patronymic': {
                required: 'Будь ласка, введіть по батькові.',
            },

            '[2].DateOfBirth': {
                required: 'Будь ласка, оберіть дату.',
                email: 'Будь ласка, введіть правильний адрес.'
            },

            '[2].Email': {
                required: 'Будь ласка, введіть email.',
            },

            '[2].Phone': {
                required: 'Будь ласка, введіть телефон.',
            },
            '[3].Name': 'Будь ласка, введіть імя',

            '[3].Surname': {
                required: 'Будь ласка, введіть прізвище.'
            },

            '[3].Patronymic': {
                required: 'Будь ласка, введіть по батькові.',
            },

            '[3].DateOfBirth': {
                required: 'Будь ласка, оберіть дату.',
                email: 'Будь ласка, введіть правильний адрес.'
            },

            '[3].Email': {
                required: 'Будь ласка, введіть email.',
            },

            '[3].Phone': {
                required: 'Будь ласка, введіть телефон.',
            }
        }
    });
});

$(function () {

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
        yearSuffix: ''
    };

    $("input[name='[0].DateOfBirth']").datepicker($.datepicker.regional["ru"]);
    $("input[name='[1].DateOfBirth']").datepicker($.datepicker.regional["ru"]);
    $("input[name='[3].DateOfBirth']").datepicker($.datepicker.regional["ru"]);
    $("input[name='[4].DateOfBirth']").datepicker($.datepicker.regional["ru"]);

});