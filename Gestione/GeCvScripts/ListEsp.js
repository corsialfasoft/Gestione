var uri = '../api/CV/EEEE/EspLav';
$(document).ready(function () {
    ListEspLav()
});
function ListEspLav() {
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#ListEspLav').append('<div class="col-md-6">' + item.Qualifica + '</div><div class="col-md-3"><button class="btn btn-default" onclick="DettEsp(' + item.Id + ')"role=button>Dettagli</button></div>'+
                 '<div><button class="btn btn-default" onclick="DelEsp(' + item.Id + ')"role=button>Elimina</button>' + '</div>');

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