var uri = '../api/Getime';
$(document).on("ServerError", function () {
    $('#Message').empty().html("ERRORE SERVER").show();
});
$(document).ready(function () {
    var data = new Date();
    var max = data.getFullYear() + '-' + ("0" + (data.getMonth() + 1)).slice(-2) + '-' + ("0" + data.getDate()).slice(-2);
    $("#DATA").attr('max', max);
    $('#Message').hide();
    $("#cerca").submit(function (event) {
        $('#Message').hide();
        event.preventDefault();
        var data = $('#DATA').val();
        $('#giorno').hide();
        $('#giorno').load("GeTimePartial/SubVisualizzaGiorno.html", function (response, status) {
            if (status == 'success') {
                $(document).on("DateNotFound", function (event, date) {
                    $('#Message').empty().html("Data " + data.ToDateString() + " non è stata trovata!").show();
                    $('#giorno').empty();
                });
                ViewGiorno(data);
                $(document).on("SuccessVisualizzaGiorno", function () {
                    $('#giorno').show();
                });
            }
        })
    });
});