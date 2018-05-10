var uri = '../api/CV/{idCV}/Competenza';
$(document).ready(function () {
    //$("#Nuovo").hide();
    function LoadCompetenze(idCV) {
        $('#Competenza').empty();
        $.getJSON(uri)
            .done(function (data) {
                $.each(data, function (key, item) {
            $('#Competenza').append('<li class="list-group-item" onclick=LoadCompetenza(' + item.Id + ')>' + item.Titolo + item.Livello + '</li>');
                });
            });
    }
    LoadCompetenze();
    $("#frmNuovo").submit(function (event, idCV) {
        event.preventDefault();
        var $form = $(this)
        var competenza = {};
        var url = $form.attr("action");
        competenza.Titolo = $form.find("textarea[name='_titolo']").val();
        competenza.Livello = $form.find("input[name='_livello']").val();
                
        var posting = $.post(url, competenza, "AAAA");
        posting.done(function () {
            LoadCompetenze();
            $form.find("textarea[name='_titolo']").val("");
            $form.find("input[name='_livello']").val("");
        });
    });
});

function LoadS() {
    $('#Competenza').empty();
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#Competenza').append('<li class="list-group-item" onclick=LoadCompetenza(' + item.Id + ')>' + item.Titolo + item.Livello + '</li>');
            });
        });
}

function LoadCompetenza(id) {
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#DettaglioCompetenze').html(
                '<div class="panel panel-primary">' +
                '<div class="panel-heading">' + data.Id + '</div>' +
                '<div class="panel-body"> <p><small>' + data.Titolo + '</small></p>' +
                '<div class="form-group"><label>livello</label> ' +
                '<input disabled type="text" class="form-control" value="' + data.Livello + '">' +
                '<a class="btn btn-success" onclick="Modifica(' + data.Id +')">Modifica</a> &nbsp;' +
                '<a class="btn btn-warning" onclick="Delete('+data.Id+')">Delete</a>' +
                '</div>' +
                '</div > ');
        });
}
//function AddProdotto() {

//}

function Modifica(idC) {
    $.ajax({
        url: uri + '/' + idC,
        method: "put",
        success: function (data) {
            $('#DettaglioCompetenze').hide();
            LoadS();
        }/*).fail(function (data){
            *  $("body").prepend(document.createTextNode("ERRORE!!!"));
            *  });*/
    });
}

function Delete(prod) {
    $.ajax({
        url: uri + '/' + prod,
        method: "delete",
        success: function (data) {
            $('#DettaglioCompetenze').hide();
            LoadS();
        }/*).fail(function (data){
            *  $("body").prepend(document.createTextNode("ERRORE!!!"));
            *  });*/
    });
}

function NuovoProdotto() {
    $("#Elenco").hide();
    $("#Nuovo").show();
    $("#TabElenco").removeClass("active");
    $("#TabNuovo").addClass("active");
}
function ElencoProdotti() {
    $("#Nuovo").hide();
    $("#Elenco").show();
    $("#TabNuovo").removeClass("active");
    $("#TabElenco").addClass("active");
}