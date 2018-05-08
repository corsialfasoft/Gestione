
        var uri = '../api/CV/AAAA/Competenza';
        $(document).ready(function () {
            $("#Nuovo").hide();
            function LoadCompetenze() {
                $('#Competenza').empty();
                $.getJSON(uri)
                    .done(function (data) {
                        $.each(data, function (key, item) {
                    $('#Competenza').append('<li class="list-group-item" onclick=LoadCompetenza(' + item.ID + ')>' + item.Titolo + item.Livello + '</li>');
                        });
                    });
            }
            LoadCompetenze();
            $("#frmNuovo").submit(function (event, idCV) {
                event.preventDefault();
                var $form = $(this)
                var competenza = {};
                var url = $form.attr("action");
                competenza.Titolo = $form.find("textarea[name='_titolo']").val();
                competenza.Livello = $form.find("input[name='_livello']").val();
                
                var posting = $.post(url, competenza, idCV);
                posting.done(function () {
                    LoadCompetenze();
                    $form.find("textarea[name='_titolo']").val("");
                    $form.find("input[name='_livello']").val("");
                });
            });
        });

        function LoadS() {
            $('#Competenza').empty();
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        $('#Competenza').append('<li class="list-group-item" onclick=LoadCompetenza(' + item.ID + ')>' + item.Descrizione + '</li>');
                    });
                });
        }

        function LoadCompetenza(id) {
            $.getJSON(uri + '/' + id)
                .done(function (data) {
                    $('#DettaglioProdotto').html(
                        '<div class="panel panel-primary">' +
                        '<div class="panel-heading">' + data.ID + '</div>' +
                        '<div class="panel-body"> <p><small>' + data.Descrizione + '</small></p>' +
                        '<div class="form-group"><label>quantità</label> ' +
                        '<input disabled type="text" class="form-control" value="' + data.Quantita + '">' +
                        '<a class="btn btn-success" href="#" role="button">Modifica</a> &nbsp;' +
                        '<a class="btn btn-warning" onclick="Delete('+data.ID+')">Delete</a>' +
                        '</div>' +
                        '</div > ');
                });
        }
        //function AddProdotto() {

        //}

        function Delete(prod) {
            $.ajax({
                url: uri + '/' + prod,
                method: "delete",
                success: function (data) {
                    $('#DettaglioProdotto').hide();
                    LoadS();
                }/*).fail(function (data){
                 *  $("body").prepend(document.createTextNode("ERRORE!!!"));
                 *  });*/
            });
        }

        function NuovoProdotto() {
            $("#Elenco").hide();
            $("#Nuovo").show();
            $("#TabElenco").removeClass("active");
            $("#TabNuovo").addClass("active");
        }
        function ElencoProdotti() {
            $("#Nuovo").hide();
            $("#Elenco").show();
            $("#TabNuovo").removeClass("active");
            $("#TabElenco").addClass("active");
        }