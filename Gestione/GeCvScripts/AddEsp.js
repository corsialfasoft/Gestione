$(document).ready(function () {
    $("#frmNuovo").submit(function (event) {
        event.preventDefault();
        var $form = $(this);
        var EspLav = {};
        EspLav.Descrizione = $form.find("textarea[name='_descrizione']").val();
        EspLav.Inizio = $form.find("input[name='_inizio']").val();
        EspLav.Fine = $form.find("input[name='_fine']").val();
        EspLav.Qualifica = $form.find("input[name='_mansione']").val();
        var posting = $.post($form.attr("action"), EspLav);
        posting.done(function (data) {
            //var content = $(data).find("#Message");
            $(document).trigger("EspLavAdded", data);
            $("#Message").append(data);
        });
    });  
});