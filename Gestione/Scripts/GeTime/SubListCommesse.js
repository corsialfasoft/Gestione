function ViewCommesse (nCom) {
    $.getJSON('../api/Commesse/' + nCom, function (data) {
        if (data != null && data.length > 0) {
            var html = '';
            for (var i = 0; i < data.length; i++) {
                html += '<tr>' +
                    '<th scope="col">' + (i + 1) + '</th>' +
                    '<th scope="col">' + data[i] +
                    '<span class="badge" style="float:right; color:black; background-color:lightgray">' +
                    '<button onclick="selectCommessa(\'' + data[i] +'\')">Seleziona</button>' +
                    '</span>' +
                    '</th>' +
                    '</tr>';
            }
            $('#tabella_commesse tbody').empty().append(html);
        }
    }).fail(function (data) {
        if (data.statusCode == 500)
            $(document).trigger("ServerError");
        else {
            $('#Message').empty().append(data.responseJSON.Message);
            viewGiorno($form.find("input[name='dateTime']").val());
        }
    });
}
function selectCommessa(nCommessa){
    $('#tabella_commesse').trigger("SelectedCommessa", nCommessa);
}
   /* html += '<input type="hidden" name="data" value=' + data + ' >' +
        '<input type="hidden" name="ore" value=' + ore + ' >';*/