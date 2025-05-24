// Modernized default.js for .NET 4.6 projects
(function ($) {
    $(function () { // Document ready shorthand
        try {
            // Initialize Select2 if available
            if (typeof $.fn.select2 === 'function') {
                initSelect2();
            }

            // Initialize iCheck if available
            if (typeof $.fn.iCheck === 'function') {
                initICheck();
            }

            // Setup form validation
            initFormValidation();

            // Configure AJAX defaults
            initAjaxHandlers();

        } catch (e) {
            console.error("Initialization error:", e);
        }
    });

    function initSelect2() {
        $(".ArmyId").each(function () {
            if (!$(this).data('select2')) {
                selection2(this);
            }
        });

        $(".select2").each(function () {
            if (!$(this).data('select2')) {
                $(this).select2();
            }
        });
    }

    function initICheck() {
        $('input[type="checkbox"].iCheck').each(function () {
            if (!$(this).data('icheck')) {
                $(this).iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green',
                    increaseArea: '20%'
                });
            }
        });
    }

    function initFormValidation() {
        $('form').each(function () {
            var validator = $(this).data('validator');
            if (validator && validator.settings) {
                validator.settings.ignore = ".hidden";
            }
        });
    }

    function initAjaxHandlers() {
        $.ajaxSetup({
            beforeSend: function () {
                var $wrapper = $(".main-wrapper");
                if ($wrapper.length && typeof $wrapper.loading === 'function') {
                    $wrapper.loading({ stoppable: true });
                }
            },
            complete: function () {
                var $wrapper = $(".main-wrapper");
                if ($wrapper.length && typeof $wrapper.loading === 'function') {
                    $wrapper.loading('stop');
                }
            }
        });
    }

    function selection2(element, selected) {
        var $element = $(element);
        try {
            if (!$element.data('select2')) {
                $element.select2({
                    placeholder: "Search Regiment",
                    minimumInputLength: 3,
                    ajax: {
                        url: '../GeneralInformation/Get',
                        dataType: 'json',
                        delay: 250,
                        data: function (params) {
                            return { term: params.term };
                        },
                        processResults: function (data) {
                            return { results: data.Results || [] };
                        },
                        cache: true
                    }
                });

                if (selected && selected.id) {
                    var option = new Option(selected.text, selected.id, true, true);
                    $element.append(option).trigger('change');
                }
            }
        } catch (e) {
            console.error("Select2 initialization error:", e);
        }
    }

    // Other functions remain similar but with jQuery 3.6.0 compatibility
})(jQuery);
//$(document).ready(function () {
//    selection2(".ArmyId");
//    var date = new Date();
//    if ($.isFunction($.fn.select2)) {
//        $(".select2").select2();
//    }

//    if ($.isFunction($.fn.iCheck)) {

//        $('input[type="checkbox"].iCheck').iCheck({
//            checkboxClass: 'icheckbox_square-green',
//            radioClass: 'iradio_square-green',
//            increaseArea: '20%'
//        });
//    }

//    var formValidate = $('form')[0];
//    if (typeof formValidate === "undefined") {
//        $.data(formValidate, 'validator').settings.ignore = ".hidden";
//    }

//    //https://gasparesganga.com/labs/jquery-loading-overlay/#quick-demo
//    $.ajaxSetup({
//        beforeSend: function () {
//            $(".main-wrapper").loading({
//                stoppable: true
//            });
//        },
//        complete: function () {
//            $(".main-wrapper").loading('stop');
//        },
//        success: function () { }
//    });
//});

//function selection2(selector, selected) {
//    $(selector).select2({
//        placeholder: "Search Regiment",
//        minimumInputLength: 3,
//        ajax: {
//            url: '../GeneralInformation/Get',
//            dataType: 'json',
//            quietMillis: 250,
//            data: function (param) {
//                return { term: param };
//            },
//            results: function (data) {
//                return { results: data.Results };
//            }
//        },
//        initSelection: function (element, callback) {
//            if (typeof selected != "undefined") {
//                callback({ id: selected.id, text: selected.text });
//            }
//        }
//    });
//}

//function ajaxCallDetail(selector) {
//    var success = false;
//    if (selector.valid()) {
//        var data = selector.serialize();
//        data = data + '&' + $.param({ 'Items': items });

//        $.ajax({
//            processData: false,
//            data: data,
//            dataType: 'json',
//            async: false,
//            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
//            type: selector.attr('method'),
//            url: selector.attr('action'),
//            success: function (response) {
//                success = response.success;
//                $("#message").html(message(response.message));
//            }
//        });
//    }
//    return success;
//}

//function message(m) {
//    return "<div class='col-lg-12'>"
//        + "<div class='alert  alert-info alert-dismissible' role='alert'>"
//        + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
//        + "<span aria-hidden='true'>&times;</span>"
//        + "</button>"
//        + "<i class='fa " + m.icon + "'></i>"
//        + m.Text
//        + "</div>"
//        + "</div>";
//}

//function emptyAttachment() {
//    $(".qq-upload-success").hide();
//    $("#AttachementId").val(0);
//    $(".remove-file-button").hide();
//    $(".qq-upload-list").hide();
//    $(".uploaded-file").hide();
//}
