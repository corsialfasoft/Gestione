var uri = '../api/CV';
function CercaCv() {
    $('#CVs').empty();
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#CVs').append('<li class="list-group-item"> Nome: ' + item.Nome +' Cognome: '+ item.Cognome +' Matricola:'+ item.Matricola +
                    '<button class="btn btn-info"  onclick="LoadCV(\''+ item.Matricola + '\')">Dettagli</button></li>');
            });
        });
}