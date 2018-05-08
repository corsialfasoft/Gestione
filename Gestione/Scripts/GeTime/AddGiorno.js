$("#form_compila").ready(function () {
    var data = new Date();
    var max = data.getFullYear() + '-' + (data.getMonth() + 1) + '-' + data.getDate();
    data.setTime(data.getTime() - (180 * 24 * 60 * 60 * 1000));
    var min = data.getFullYear() + '-' + (data.getMonth() + 1) + '-' + data.getDate();
    $('#form_compila input[name=dateTime]').attr('min', min);
    $('#form_compila input[name=dateTime]').attr('max', max);

    $('#form_compila').submit(function () {
        event.preventDefault();
        var $form = $(this);
        if ($form.find('select :selected').text() == 'Ore di lavoro') {
            $.getJSON('../api/Commesse/' + $form.find("input[name='Commessa']").val(), function (data) {
                if (data != null && data.length > 0) {
                    if (data.length == 1) {
                        sendGiorno(data[0], $form.find("input[name='dateTime']").val(), $form.find("input[name='ore']").val());
                    } else {
                        $(document).trigger("ViewCommesseAddGiorno", [$form.find("input[name='Commessa']").val(), $form.find("input[name='dateTime']").val(), $form.find("input[name='ore']").val(), $form.find('select :selected').text()]);
                    }
                } else {
                    $('#Message').empty().append('Commessa non esistente');
                }
            }).fail(function (data) {
                $('#Message').empty().append(data.responseJSON.Message);
            });
        } else {
            $(document).trigger("ViewCommesseAddGiorno", [$form.find("input[name='Commessa']").val(), $form.find("input[name='dateTime']").val(), $form.find("input[name='ore']").val(), $form.find('select :selected').text()]);
        }
    });
})
function changeHTyp(value) {
    if (value == 'Ore di permesso' || value == 'Ore di malattia') {
        document.getElementById('commesse').disabled = true;
        document.getElementById('ore').value = "";
        document.getElementById('ore').setAttribute('max', '8');
        document.getElementById('ore').disabled = false;
    } else if (value == 'Ore di ferie') {
        document.getElementById('ore').value = 8;
        document.getElementById('ore').disabled = true;
        document.getElementById('commesse').disabled = true;
    } else if (value == 'Ore di lavoro' || value == '') {
        document.getElementById('commesse').disabled = false;
        document.getElementById('ore').value = "";
        if (value == 'Ore di lavoro')
            document.getElementById('ore').setAttribute('max', '14');
        else
            document.getElementById('ore').setAttribute('max', '8');
        document.getElementById('ore').disabled = false;
    }
}
function sendGiorno(nCommessa, data, ore, tipoOra) {
    var Giorno = {};
    Giorno.Data = data
    Giorno.Ore = ore
    Giorno.Commessa = nCommessa
    Giorno.TipoOre = 'Ore di lavoro';
    $.post('../api/Getime', Giorno, function (data) {
        $('#esito').empty().append('Il giorno ' +data+ " sono state aggiunte "+ ore);
    }).fail(function (data) {
        $(document).trigger("FailCompilaGiorno", [data.responseJSON.Message, data]);
    });
}