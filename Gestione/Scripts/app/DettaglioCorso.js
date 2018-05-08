function LoadCorso(id,element) {
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $(element).html(
                '<div class="panel panel-primary">' +
                '<div class="panel-heading">' + data.Nome + '</div>' +
                '<div class="panel-body"> <p><small>' + data.Descrizione + '</small></p>' +
                '<div class="form-group"><label>Data Inizio</label> ' +
                '<input disabled type="text" class="form-control" value="' + data.Inizio + '">' +
                '<div class="form-group"><label>Data Fine</label> ' +
                '<input disabled type="text" class="form-control" value="' + data.Fine + '"></div>' +

                '<a class="btn btn-success" href="#" role="button">Modifica</a> &nbsp;' +
                '<a class="btn btn-warning" href="#" role="button">Elimina</a>' +
                '</div>' +
                '</div > ');
            $('#Lezioni').empty();
            $('#Lezioni').append("<ul class='list-group' >");
            $.each(data.Lezioni, function (key, item) {
                $('#Lezioni').append("<li class='list-group-item'>" + item.Nome + "</li>");
            });
            $('#Lezioni').append("</ul>");
        });
}