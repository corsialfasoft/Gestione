var uri = '../api/CV/';
function ListEspLav(id) {           //LOAD LISTA
    $('#ListEspLav').show();
   
    $('#ListEspLav').empty();
    $.getJSON(uri + '/' + id + '/EspLav')
        .done(function (data) {
            $.each(data, function (key, item) {
                '<div class="container">'
                $('#ListEspLav').append('<li class="list-group-item" >' + item.Qualifica + '<button style="float:right" onclick=LoadEspLav('+item.Id+')>DETTAGLIO</button></li>');
                '</div>';
            });
        });
}
function LoadEspLav(id) {         //DETTAGLIO
    //$("#AddE").hide();
    $("#HeadingEspLav").empty();
    $("#HeadingEspLav").append("Dettaglio Esperienza Lavorativa");
    
    var $form = $("#ModEspLav");
    $.getJSON('../api/EspLav/' + id)
        .done(function (data) {
            var $PerStud = $("#DettaglioEsp");
            $PerStud.find("input[id='esp_AI']").val(data.AnnoInizio);
            $PerStud.find("input[id='esp_AF']").val(data.AnnoFine);
            $PerStud.find("input[id='esp_qualifica']").val(data.Qualifica);
            $PerStud.find("input[id='esp_descrizione']").val(data.Descrizione);
            $PerStud.find("input[id='esp_id']").val(data.Id);
            $form.find("button[id='btn_add_esplav']").hide();
            $form.find("button[id='btn_del_esplav']").show();
            $form.find("button[id='btn_mod_esplav']").show();
            $(document).trigger("TriDettELShow");
        });
}

function DeleteEsp() {          //DELETE
    event.preventDefault();
    var $form = $("#ModEspLav");
    var idE = $form.find("input[id='esp_id']").val();
    var urlo = "../api/EspLav/Del/" + idE;
    $(document).trigger("TriHideAll");
    $.ajax({
        url: urlo,
        type: 'DELETE',
        success: function (result) {
            $('#DettEsp').hide(),
                $(document).trigger("TriDettELShow");
                ListEspLav()
        },
    });
}
function ModificaEspLav() {         //MODIFICA
    event.preventDefault();
    //event.preventDefault();
    var $form = $("#ModEspLav");
    var idP = $form.find("input[id='esp_id']").val();
    var urlo = "../api/EspLav/Put/" + idP;
    var el = {};
    el.Id = $form.find("input[id='esp_id']").val();
    el.AnnoInizio = $form.find("input[id='esp_AI']").val();
    el.AnnoFine = $form.find("input[id='esp_AF']").val();
    el.Qualifica = $form.find("input[id='esp_qualifica']").val();
    el.Descrizione = $form.find("input[id='esp_descrizione']").val();
    $(document).trigger("TriHideAll");
    $.ajax({
        url: urlo,
        method: "PUT",
        data: el,
        success: function (data) {
            $(document).trigger("TriDettELShow");
        }
    });
}
    
function AddEspLav() {              //AGGIUNGI
    event.preventDefault();
    var $div = $("#cv_Anag");
    var idCv = $div.find("input[id='cv_matricola']").val();
    var $form = $("#ModEspLav");
    var el = {};
    var url = "../api/CV/" + idCv + "/Add/EspLav";
    el.AnnoInizio = $form.find("input[id='esp_AI']").val();
    el.AnnoFine = $form.find("input[id='esp_AF']").val();
    el.Qualifica = $form.find("input[id='esp_qualifica']").val();
    el.Descrizione = $form.find("input[id='esp_descrizione']").val();
   
    $(document).trigger("TriDettELShow");
    var posting = $.post(url, el, idCv);
    posting.done(function () {
        AggiungiEspLav();
        ListEspLav(idCv);
    })
}
function AggiungiEspLav() {         //Serve solo a liberari i campi per l'aggiunta
    $("#HeadingEspLav").empty();
    $("#HeadingEspLav").append("Aggiungi Esperienza Lavorativa");
    //$div.val("Aggiungi Esperienza Lavorativa");
    var $form = $("#ModEspLav");
    $form.find("button[id='btn_mod_esplav']").hide();
    $form.find("button[id='btn_del_esplav']").hide();
    $form.find("button[id='btn_add_esplav']").show();
    $form.find("input[id='esp_AI']").val("");
    $form.find("input[id='esp_AF']").val("");
    $form.find("input[id='esp_qualifica']").val("");
    $form.find("input[id='esp_descrizione']").val("");
    $(document).trigger("TriDettELShow");
}