var uri = '../api/CV/';
$(document).ready(function () {
    //$("#Nuovo").hide();
    //LoadCompetenze();
    //$("#frmNuovo").submit(function (event, idCV) {
    //    event.preventDefault();
    //    var $form = $(this)
    //    var competenza = {};
    //    var url = $form.attr("action");
    //    competenza.Titolo = $form.find("textarea[name='_titolo']").val();
    //    competenza.Livello = $form.find("input[name='_livello']").val();
                
    //    var posting = $.post(url, competenza, "AAAA");
    //    posting.done(function () {
    //        LoadCompetenze();
    //        $form.find("textarea[name='_titolo']").val("");
    //        $form.find("input[name='_livello']").val("");
    //    });
    //});

//function LoadS() {
//    $('#Competenza').empty();
//    $.getJSON(uri)
//        .done(function (data) {
//            $.each(data, function (key, item) {
//                $('#Competenza').append('<li class="list-group-item" onclick=LoadCompetenza(' + item.Id + ')>' + item.Titolo + item.Livello + '</li>');
//            });
//        });
//}
});
function AddComp() {
    event.preventDefault();
    var $div = $("#cv_Anag");
    var idCv = $div.find("input[id='cv_matricola']").val();
    var $form = $("#New_Comp");
    var competenza = {};
    var url = "../api/CV/" + idCv + "/Add/Competenza";
    competenza.Titolo = $form.find("input[name='_titolo']").val();
    competenza.Livello = $form.find("input[name='_livello']").val();

    var posting = $.post(url, competenza, idCv);
    posting.done(function () {
        $(document).trigger("TriHideAll");
    })
}
    function LoadCompetenze(id) {
        $('#ElencoCompetenze').empty();
        //var $div = $("#cv_Anag");
       // var idCv = $div.find("input[id='cv_matricola']").val();
        $.getJSON(uri +'/'+ id + '/Competenza')
            .done(function (data) {
                $.each(data, function (key, item) {
                    $('#ElencoCompetenze').append('<li class="list-group-item">' + item.Titolo + item.Livello + '<button style="float:right"  onclick=LoadCompetenza(' + item.Id + ')>Dettagli</button></li>');
                });
            });
        
    }

function LoadCompetenza(id) {
    $("#HeadingComp").empty();
    $("#HeadingComp").append("Dettaglio Competenza");
    var $form = $("#ModComp");
    var $div = $("#cv_Anag");
    var idCv = $div.find("input[id='cv_matricola']").val();
    $.getJSON('../api/Competenza/'+id)
        .done(function (data) {
            var $comp = $("#DettaglioCompetenze");
            $comp.find("input[id='comp_titolo']").val(data.Titolo);
            $comp.find("input[id='comp_liv']").val(data.Livello);
            $comp.find("input[id='comp_id']").val(data.Id);
            $form.find("button[id='btn_add_comp']").hide();
            $form.find("button[id='btn_del_comp']").show();
            $form.find("button[id='btn_mod_comp']").show();
            $(document).trigger("TriDettCompShow");
        });
}
//function AddProdotto() {

//}
function AddCompetenza() {          //AGGIUNGI COMPETENZA
    event.preventDefault();
    var $form = $("#ModComp");
    var $div = $("#cv_Anag");
    var idCv = $div.find("input[id='cv_matricola']").val();
    var idC = $form.find("input[id='comp_id']").val();
    var urrr = "../api/CV/" + idCv + "/Add/Competenza";
    var Comp = {};
    Comp.Titolo = $form.find("input[id='comp_titolo']").val();
    Comp.Livello = $form.find("input[id='comp_liv']").val();
    var posting = $.post(urrr, Comp, idCv);
    
    posting.done(function () {
        $(document).trigger("TriHideAll");
        ClearComp();
        LoadCompetenze(idCv);
    })
}
function ClearComp() {
    $("#HeadingComp").empty();
    $("#HeadingComp").append("Aggiungi Competenza");
    var $form = $("#ModComp");
    $form.find("button[id='btn_mod_comp']").hide();
    $form.find("button[id='btn_del_comp']").hide();
    $form.find("button[id='btn_add_comp']").show();
    $form.find("input[id='comp_titolo']").val("");
    $form.find("input[id='comp_liv']").val("");
    $(document).trigger("TriDettCompShow");
}

function ModificaCompetenze() {
    event.preventDefault();
    var $form = $("#ModComp");
    var idC = $form.find("input[id='comp_id']").val();
    var urrr = "../api/Competenza/Put/" + idC;
    var Comp = {};
    Comp.Id = $form.find("input[id='comp_id']").val();
    Comp.Titolo = $form.find("input[id='comp_titolo']").val();
    Comp.Livello = $form.find("input[id='comp_liv']").val();
    alert("rrr:" + urrr + "   - Comp:" + Comp);
    $.ajax({
        url: urrr,
        method: "PUT",
        data: Comp,
        success: function (data) {
            $(document).trigger("TriHideAll");
            
        }/*).fail(function (data){
            *  $("body").prepend(document.createTextNode("ERRORE!!!"));
            *  });*/
    });
}

function DeleteComp() {
    event.preventDefault();
    var $form = $("#ModComp");
    var idC = $form.find("input[id='comp_id']").val();
    var urlo = "../api/Competenza/Del/" + idC;
    $.ajax({
        url: urlo,
        method: "Delete",
        success: function (data) {
            $(document).trigger("TriHideAll");
        }/*).fail(function (data){
            *  $("body").prepend(document.createTextNode("ERRORE!!!"));
            *  });*/
    });
}

