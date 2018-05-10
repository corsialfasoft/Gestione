var uri = '../api/CV';
$(document).ready(function () {
    //$('#AddCV').submit(function (event) {
    //    event.preventDefault();
    //    var $form = $(this)
    //    var CV = {};
    //    var url = $form.attr("action");
    //    CV.Nome = $form.find("input[id='cv_nome']").val();
    //    CV.Cognome = $form.find("input[id='cv_cognome']").val();
    //    CV.Eta = $form.find("input[id='cv_eta']").val();
    //    CV.Email = $form.find("input[id='cv_email']").val();
    //    CV.Residenza = $form.find("input[id='cv_residenza']").val();
    //    CV.Telefono = $form.find("input[id='cv_telefono']").val();
    //    var posting = $.post(url, CV);
    //    posting.done(function (data) {
    //        $("#Messagge").append(data);
    //    });
    //});
    // LoadCV("BBBB");
    $('#ModCV').submit(function (event) {
        event.preventDefault();
        var $form = $("#ModCV")
        var CV = {};
        var url = $form.attr("action");
        CV.Nome = $form.find("input[id='cv_nome']").val();
        CV.Cognome = $form.find("input[id='cv_cognome']").val();
        CV.Eta = $form.find("input[id='cv_eta']").val();
        CV.Email = $form.find("input[id='cv_email']").val();
        CV.Residenza = $form.find("input[id='cv_residenza']").val();
        CV.Telefono = $form.find("input[id='cv_telefono']").val();
        CV.Matricola = $form.find("input[id='cv_matricola']").val();
        var posting = $.post(url, CV);
        posting.done(function (data) {
            $("#Message").append(data);
        });
        //$.ajax({
        //    method: "POST",
        //    url: url,
        //    data: CV
        //}).done(function (msg) {
        //        alert("Data Saved: " + msg);
        //});

    });
    
});
function LoadCV(id) {


    $.getJSON(uri + '/' + id)
        .done(
            function (data) {
                var $div = $('#cv_Anag')
                $div.find("input[id='cv_nome']").val(data.Nome);
                $div.find("input[id='cv_cognome']").val(data.Cognome);
                $div.find("input[id='cv_eta']").val(data.Eta);
                $div.find("input[id='cv_email']").val(data.Email);
                $div.find("input[id='cv_residenza']").val(data.Residenza);
                $div.find("input[id='cv_telefono']").val(data.Telefono);
                $div.find("input[id='cv_matricola']").val(data.Matricola);

            });
}