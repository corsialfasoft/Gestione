var percor = '../api/CV/';

//$(document).ready(function () {
//    //$('#Aggiungi').hide();
//    LoadPerStud();
//});
function LoadElencoPerStud(id) {
    $("#Percorso").empty();
    $.getJSON(uri + '/' + id + '/PerStud')
        .done(function (data) {
            $.each(data, function (key, item) {
                $("#Percorso").append('<li class="list-group-item" >' + item.Titolo + '<button onclick=LoadDettPerStudi(' + item.Id + ')>DETTAGLI</button></li>');
            });
        });
}
function LoadDettPerStudi(id) {         //DETTAGLIO 
    $.getJSON('../api/PerStud/' + id)
        .done(function (data) {
            var $PerStud = $("#DettaglioPercorso");
            $PerStud.find("input[id='perstud_AI']").val(data.AnnoInizio);
            $PerStud.find("input[id='perstud_AF']").val(data.AnnoFine);
            $PerStud.find("input[id='perstud_titolo']").val(data.Titolo);
            $PerStud.find("input[id='perstud_descrizione']").val(data.Descrizione);
            $PerStud.find("input[id='perstud_id']").val(data.Id);
        });
}
function DelPerStud() {                 //DELETE
    event.preventDefault();
    var $form = $("#ModPerStud");
    var idP = $form.find("input[id='perstud_id']").val();
    var urlo = "../api/PerStud/Del/" + idP;
    $.ajax({
        url: urlo,
        method: "DELETE",
        success: function (data) {

        }
    });
}
function ModificaPerStud() {            //MODIFICA
    event.preventDefault();
    var $form = $("#ModPerStud");
    var idP = $form.find("input[id='perstud_id']").val();
    var urlo = "../api/PerStud/Put/" + idP;
    var ps = {};
    ps.Id = $form.find("input[id='perstud_id']").val();
    ps.AnnoInizio = $form.find("input[id='perstud_AI']").val();
    ps.AnnoFine = $form.find("input[id='perstud_AF']").val();
    ps.Titolo = $form.find("input[id='perstud_titolo']").val();
    ps.Descrizione = $form.find("input[id='perstud_descrizione']").val();
    $.ajax({
        url: urlo,
        method: "PUT",
        data: ps,
        success: function (data) {

        }
    });

}
function AddPerStud() {
    event.preventDefault();
    var $div = $("#cv_Anag");
    var idCv = $div.find("input[id='cv_matricola']").val();
    var $form = $("#New_PerStud");
    var ps = {};
    var url = "../api/CV/" + idCv + "/Add/PerStud";
    ps.AnnoInizio = $form.find("input[name='_AnnoInizio']").val();
    ps.AnnoFine = $form.find("input[name='_AnnoFine']").val();
    ps.Titolo = $form.find("input[name='_titolo']").val();
    ps.Descrizione = $form.find("input[name='_descrizione']").val();
    var posting = $.post(url, ps, idCv);
    posting.done(function () {

    })
}
/*
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
*/