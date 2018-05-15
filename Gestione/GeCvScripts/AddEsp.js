$(document).ready(function () {
    AddEspLAv();
});

function AddEspLAv() {
    $('#AddEspLav').show();
    $("#frmNuovo").submit(function (event) {
        event.preventDefault();
        var $form = $(this);
        var EspLav = {};
        EspLav.Qualifica = $form.find("input[name='_mansione']").val();
        EspLav.Descrizione = $form.find("textarea[name='_descrizione']").val();
        EspLav.AnnoInizio = $form.find("input[name='_inizio']").val();
        EspLav.AnnoFine = $form.find("input[name='_fine']").val();
        var posting = $.post($form.attr("action"), EspLav);
        posting.done(function (data) {
            $(document).trigger("EspLavAdded", data);
            $("#Message").append(data);
        });
        $('#AddEspLav').hide();
        ListEspLav()
    });
}