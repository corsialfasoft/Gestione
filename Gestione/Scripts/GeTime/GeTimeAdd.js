$(document).ready(function () {
    $(document).on('ViewCommesseAddGiorno', function (event, nCommessa, data, ore) {
        $('#compila').hide();
        $('#tabella_commesse').load('ListCommesse.html');
        $(document).ready(function () {
            ViewCommesse(nCommessa);
            $('#tabella_commesse').on('SelectedCommessa', function (event, nCommessa) {
                sendGiorno(nCommessa, data, ore);
            })
        });
    });
    $(document).on('FailCompilaGiorno', function (event, message, data) {
        $('#Message').empty().append(message);
        $('#giorno').empty().load('VisuallizzaGiorno.html').ready(function () {
            ViewGiorno(data);
        });
    });
})