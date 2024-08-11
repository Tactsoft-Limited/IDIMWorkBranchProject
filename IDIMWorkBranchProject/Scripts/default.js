$(document).ready(function () {
    selection2(".ArmyId");
    var date = new Date();
    if ($.isFunction($.fn.select2)) {
        $(".select2").select2();
    }

    //if ($.isFunction($.fn.datepicker)) {
    //    $(".datepicker").datepicker({
    //        format: 'yyyy/mm/dd'
    //    });
    //}
 
    if ($.isFunction($.fn.iCheck)) {

        $('input[type="checkbox"].iCheck').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
            increaseArea: '20%'
        });
    }

    var formValidate = $('form')[0];
    if (typeof formValidate === "undefined") {
        $.data(formValidate, 'validator').settings.ignore = ".hidden";
    }

    //https://gasparesganga.com/labs/jquery-loading-overlay/#quick-demo
    $.ajaxSetup({
        beforeSend: function () {
            $(".main-wrapper").loading({
                stoppable: true
            });
        },
        complete: function () {
            $(".main-wrapper").loading('stop');
        },
        success: function () { }
    });
});

function selection2(selector, selected) {
    $(selector).select2({
        placeholder: "Search Regiment",
        minimumInputLength: 3,
        ajax: {
            url: '../GeneralInformation/Get',
            dataType: 'json',
            quietMillis: 250,
            data: function (param) {
                return { term: param };
            },
            results: function (data) {
                return { results: data.Results };
            }
        },
        initSelection: function (element, callback) {
            if (typeof selected != "undefined") {
                callback({ id: selected.id, text: selected.text });
            }
        }
    });
}

function ajaxCallDetail(selector) {
    var success = false;
    if (selector.valid()) {
        var data = selector.serialize();
        data = data + '&' + $.param({ 'Items': items });

        $.ajax({
            processData: false,
            data: data,
            dataType: 'json',
            async: false,
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            type: selector.attr('method'),
            url: selector.attr('action'),
            success: function (response) {
                success = response.success;
                $("#message").html(message(response.message));
            }
        });
    }
    return success;
}

function message(m) {
    return "<div class='col-lg-12'>"
        + "<div class='alert  alert-info alert-dismissible' role='alert'>"
        + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
        + "<span aria-hidden='true'>&times;</span>"
        + "</button>"
        + "<i class='fa " + m.icon + "'></i>"
        + m.Text
        + "</div>"
        + "</div>";
}

function emptyAttachment() {
    $(".qq-upload-success").hide();
    $("#AttachementId").val(0);
    $(".remove-file-button").hide();
    $(".qq-upload-list").hide();
    $(".uploaded-file").hide();
}
