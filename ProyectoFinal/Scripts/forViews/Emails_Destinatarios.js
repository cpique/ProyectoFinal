﻿//Init Global Configuration
var Config = { //Variable que contiene una llamada AJAX genérica y una propiedad para setear las urls a usar
    template: '',

    url: {
        preview: '/Emails/SendEmailToClients'
    },

    call: function (url, type, object, successCallBack, errorCallBack) {
        $.ajax({
            type: type,
            url: url,
            data: JSON.stringify(object),
            contentType: 'application/json;',
            dataType: 'json',
            success: successCallBack,
            error: errorCallBack
        });
    },

    successCallBack: function (response) {
        if (response.Result = "OK") {
            window.location.href = response.Redirect;
        }
        else {
            //TODO
        }
    },

    errorCallBack: function (xhr, textStatus, errorThrown) {
        //TODO
    },

    buildData: function () {
        var typeSelected = $("input[type='radio']:checked").data("destinatary-type")
        var IDSelected =  $("input[type='radio']:checked").attr("id")
        return { Type: typeSelected, ID: IDSelected }
    }

};
//End Global Configuration

$(document).ready(function () {
    $("#btnSendCommunication").click(function () {
        Config.call(Config.url.preview, 'POST', Config.buildData(), Config.successCallBack, Config.errorCallBack);
    })
});