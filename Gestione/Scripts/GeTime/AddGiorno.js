$(document).on('FailCompilaGiorno', function (event, message, data) {
    $('#Message').empty().append(message);
    $('#tabella_commesse').empty().hide();
    $('#compila').show();
    $('#giorno').empty().load('GeTimePartial/SubVisualizzaGiorno.html', data, function (response, status) {
        if (status == 'success') {
            ViewGiorno(data);
            $('#giorno').show();
        }
    });
});
$(document).ready(function () {
    $('#tabella_commesse').hide();
    $('#giorno').hide();
})
$(document).on('SuccessCompilaGiorno', function (event, data) {
    $('#compila').empty().load("GeTimePartial/SubAddGiorno.html", function (response, status) {
        if (status == 'success') {
            $("esito").empty().html(data);
            $('#tabella_commesse').empty().hide();
            $('#giorno').empty().hide();
        } 
    })
})
$(document).on('ViewCommesseAddGiorno', function (event, nCommessa, data, ore) {
    $('#Message').empty();
    $('#compila').hide();
    $('#giorno').hide();
    $('#tabella_commesse').load('GeTimePartial/SubListCommesse.html', data, function (response, status) {
        if (status == 'success') {
            ViewCommesse(nCommessa);
            $('#tabella_commesse').on('SelectedCommessa', function (event, nCommessa) {
                sendGiorno(nCommessa, data, ore);
            })
            $('#tabella_commesse').show();
        }
    });
});
$(document).on("ServerError", function () {
    $('#Message').empty().html("ERRORE SERVER").show();
});