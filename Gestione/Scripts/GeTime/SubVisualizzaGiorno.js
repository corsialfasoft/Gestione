function ViewGiorno(dataS) {
    var date = new Date(dataS);
    $.getJSON(('../api/Getime/' + date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate()), function (data,iqxhr) {
        if (data != null) {
            var count = 0;
            var tabella_giorno = $('#tabella_giorno table');
            $('#tabella_giorno .panel-heading').html('Dettagli del Giorno ' + date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
            tabella_giorno.find('#permesso').html(data.OrePermesso);
            tabella_giorno.find('#malttie').html(data.OreMalattia);
            tabella_giorno.find('#ferie').html(data.OreFerie);
            data.OreLavorate.forEach(function (orelavorate) {
                var html = '';
                html += '<tr>';
                if (count == 0) {
                    html += '<td>Totale ore lavoro</td>' +
                        ' <td>' + data.TotOreLavorate + '</td > ';
                    count++;
                } else {
                    html += '<td></td>' +
                        '<td></td>';
                }
                html += '<td>' + orelavorate.nome + '</td>' +
                    '<td>' + orelavorate.oreGiorno + '</td>' +
                    '<td>' + orelavorate.descrizione + '</td>' +
                    '</tr>';
                tabella_giorno.find('#perOreL').before(html);
                $(document).trigger("SuccessVisualizzaGiorno");
            });
        } else {
            $(document).trigger("DateNotFound", dataS);
        }
    }).fail(function (data) {
        if(data.statusCode == 404)
            $(document).trigger("DateNotFound", dataS);
        if (data.statusCode == 500)
            $(document).trigger("ServerError");
    });
}