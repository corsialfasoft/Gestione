$("#frmNuovo").submit(function (event) {
    event.preventDefault();
    var $form = $(this);
    var corso = {};
    corso.Descrizione = $form.find("textarea[name='_descrizione']").val();
    corso.Inizio = $form.find("input[name='_inizio']").val();
    corso.Fine = $form.find("input[name='_fine']").val();
    corso.Nome = $form.find("input[name='_nome']").val();
    var posting = $.post($form.attr("action"), corso);
    posting.done(function (data) {
        //var content = $(data).find("#Message");
        $(document).trigger("CorsoAdded", data);
        $("#Message").append(data);
    });
});