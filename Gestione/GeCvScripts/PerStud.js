var percor = '/api/Cv/AAAA/PerStud';

//$(document).ready(function () {
//    //$('#Aggiungi').hide();
//    LoadPerStud();
//});
function LoadPerStud() {
    //var prova = percorso + '/AAAA/PerStud';
    $("#Percorso").empty();
    $.getJSON('../api/Cv/AAAA/PerStud')
        .done(function (data) {
            $.each(data, function (key, item) {
                $("#Percorso").append('<li class="list-group-item" onclick=LoadPerStudi(' + item.Id + ')>' + item.Descrizione + '</li>');
            });
        });
}
function LoadPerStudi(id) {
    $.getJSON(percor + "/" + id)
        .done(function (data){
            $("#DettaglioPercorso").html(
                '<div class="panel panel-primary">' +
                '<table class="table">' +
                '<div class="col-md-1"><b> Titolo </b></div>' +
                '<div class="col-md-5"><b> Descrizione </b></div>' +
                '<div class="col-md-3"><b> Anno Inizio </b></div>' +
                '<div class="col-md-3"><b> Anno Fine </b></div>' +
                '</table>' +
                '<table class="table">' +
                '<div class="col-md-1">' + data.Titolo + '</div>' +
                '<div class="col-md-5">' + data.Descrizione + '</div>' +
                '<div class="col-md-3">' + data.AnnoInizio + '</div>' +
                '<div class="col-md-3">' + data.AnnoFine + '</div>' +
                '<a onclick="ModificaPerStud(' + data.Id + ')" > Modifica</a > ' +
                '<a onclick="DelPerStud(' + data.Id + ')">Elimina</a>' +
                '</table>' +
                '</div>');
    });
}
function DelPerStud(id) {
    var urldel = percor + "/" + id;
    $.ajax({
        url: urldel,
        type: "delete"
    })
        .done(function (data) {
            LoadPerStud();
            $("#DettaglioPercorso").empty();
        });
}

$(document).ready(function () {
    $("#Aggiungi").submit(function (event) {
        event.preventDefault();
        var $form = $(this);
        var percorso = {};
        var url = $form.attr("action");
        percorso.Titolo = $form.find("input[name='_titolo']").val();
        percorso.Descrizione = $form.find("input[name='_descrizione']").val();
        percorso.AnnoInizio = $form.find("input[name='_AnnoInizio']").val();
        percorso.AnnoFine = $form.find("input[name='_AnnoFine']").val();
        var posting = $.post(url, percorso);
    });
});