var uri = '../api/CV/EEEE/EspLav';
$(document).ready(function () {
    ListEspLav()
});
function ListEspLav() {
    $('#ListEspLav').empty();
    $('#DettEspLav').hide();
    $('#AddEspLav').hide();

    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#ListEspLav').append('<div id="ciao" class="col-md-8">' + item.Qualifica + '</div><div class="col-md-2"><button class="btn btn-default" onclick="LoadEspLav(' + item.Id + ')"role=button>Dettagli</button></div></div><div class="col-md-2"><button class="btn btn-default" onclick="DelEsp(' + item.Id + ')"role=button>Elimina</button></div>');

            });
        });
}
function DelEsp(id) {
    $.ajax({
        url: '../api/CV/EEEE/EspLav/' + id,
        type: 'DELETE',
        success: function (result) {
                ListEspLav()
        },
    });
}

   