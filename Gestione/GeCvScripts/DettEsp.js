var uri = '../api/CV/EEEE/EspLav';
function LoadEspLav(idEspLav) {
    $('#DettEsp').show();
    $('#DettEspLav').show()
        $.getJSON(uri + '/' + idEspLav)
            .done(function (data) {
                $('#DettEsp').html(
                    '<div class="container">' +
                    '<div class="row">' +
                    '<div class="col-md-3">Inizio</div>' +
                    '<div class="col-md-3"><input type="number" id="ai" class="form-control" name="_inizio" value="' + data.AnnoInizio + '" /></div>' +
                    '<div class="col-md-3">Fine</div>' +
                    '<div class="col-md-3"><input type="number" id="af" class="form-control" name="_fine" value="' + data.AnnoFine + '" /></div></div>' +
                    '<div class="row">' +
                    '<div class="col-md-3">Qualifica</div>' +
                    '<div class="col-md-3"><input type="text" id="qua" class="form-control" name="_qualifica" value="' + data.Qualifica + '" /></div>' +
                    '<div class="col-md-3">Descrizione</div>' +
                    '<div class="col-md-3"><input type="text" id="desc" class="form-control" name="_desricione" value="' + data.Descrizione + '" /></div></div></div>' +
                    '<button class="btn btn-default" onclick="DelEsp(' + data.Id + ')"role=button>Elimina</button>' + '</div>' + '<div>' +
                    '<button class="btn btn-default" onclick="ModEsp(' + data.Id + ')"role=button>Modifica</button>' + '</div>' + '<div>'
                );
            });
    }
function ModEsp(id) {
    var esp = {};
    esp.AnnoInizio = $("#ai").val();
    esp.AnnoFine = $('#af').val();
    esp.Qualifica = $('#qua').val();
    esp.Descrizione = $('#desc').val();
    $.ajax({
        data: esp,
        dataType: "application/json",
        url: '../api/CV/EEEE/EspLav/' + id,
        type: 'PUT',
        success: function (result) {
            $('#DettEsp').hide(),
                ListEspLav()
        },
    });
}
function DelEsp(id) {
    $.ajax({
        url: '../api/CV/EEEE/EspLav/' + id,
        type: 'DELETE',
        success: function (result) {
            $('#DettEsp').hide(),
                ListEspLav()
        },
    });
}