
$(document).ready(function () {
    $("#MessaggeError").hide();
    $("#MessaggeWarning").hide();
})
function SvuotaCampi() {
    var $div = $('#parametri');
    $div.find("input[id='S_cognome']").val("");
    $div.find("input[id='S_chiava']").val("");
    $div.find("input[id='S_eta']").val("");
    $div.find("input[id='S_eta_max']").val("");
    $div.find("input[id='S_eta_min']").val("");

}

function Cerca() {
    var $div = $('#parametri')
    if ($div.find("input[id='S_cognome']").val() != "") {
        var url = '../api/CercaCognome/' + $div.find("input[id='S_cognome']").val();
    } else if ($div.find("input[id='S_chiava']").val() != "") {
        var url = '../api/CercaChiava/' + $div.find("input[id='S_chiava']").val();
    } else if ($div.find("input[id='S_eta']").val() != 0) {
        var url = '../api/CercaEta/' + $div.find("input[id='S_eta']").val();
    } else if ($div.find("input[id='S_eta_min']").val() != 0 && $div.find("input[id='S_eta_max']").val() != 0) {
        if ($div.find("input[id='S_eta_min']").val() < $div.find("input[id='S_eta_max']").val()) {
            var url = '../api/CercaMinMax/' + $div.find("input[id='S_eta_min']").val() + '/' + $div.find("input[id='S_eta_max']").val();
        } else {
            ErrorMessage();
            return;
        }
    }
    $.getJSON(url)
        .done(function (data) {
            if (data.length == 0) {
                WarningMessage();
            } else {
            $('#CVs').empty();
            $.each(data, function (key, item) {
                $('#CVs').append('<li class="list-group-item col-md-10"> Nome: ' + item.Nome + ' Cognome: ' + item.Cognome + ' Matricola:' + item.Matricola +
                    '<button class="btn btn-info col-md-2"  onclick="LoadCV(\'' + item.Matricola + '\')">Dettagli</button></li>');

                });
                $("#MessaggeError").hide();
                $("#MessaggeWarning").hide();
            $(document).trigger("TriSearch");

            }
        });
}

function ErrorMessage() {
    $('#MessaggeWarning').hide();
    $('#MessaggeError').empty();
    $("#MessaggeError").append('<span class="closebtn"  onclick="this.parentElement.style.display=\'none\';">&times;</span><strong> Danger!</strong > ');
    $("#MessaggeError").append("Parametri ERRATI!!");
    $('#MessaggeError').show();
    SvuotaCampi();
}
function WarningMessage() {
    $('#MessaggeError').hide();
    $('#MessaggeWarning').empty();
    $("#MessaggeWarning").append('<span class="closebtn"  onclick="this.parentElement.style.display=\'none\';">&times;</span><strong> Warning</strong > ');
    $("#MessaggeWarning").append("Sono spiacente! non ho trovato nulla <strong> : ( </strong>");
    $('#MessaggeWarning').show();
    SvuotaCampi();
}
