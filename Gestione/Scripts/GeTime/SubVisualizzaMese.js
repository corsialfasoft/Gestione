function ViewMese(anno, mese) {
    $.getJSON("../api/Getime/" + anno + "/" + mese, function (data) {
        if (data.Length > 0) {
            var Nday = new Date(anno, mese, 0).getDate();
            var tr = $("#tabella_mese > tr : first ");
            for (var index = 1; index <= Nday; index++) {
                tr.html(' <td style="padding:0; background: #a6a6a6;">' +
                    '<form action="" method="post">' +
                    '<input type="hidden" name="data" value="' + index + '" />' +
                    '<input type="submit" value="@index" class="btn btn-link" style="color:white; background: #a6a6a6; padding:0;" />' +
                    '</form>' +
                    '</td>');
            }
            tr = tr.next-sibling()
            for (var index = 1; index <= Nday; index++) {
                var sindex = IndexOf(data, anno + "-" + mese + "/" + index);
                if (sindex >= 0) {
                    tr.html('<td style="background: #0080ff; padding: 0;">' + data[sindex] + '</td>');
                } else
                    tr.html('<td />');
            }
            var tr = tr.next-sibling()
            for (var index = 1; index <= Nday; index++) {
                var sindex = IndexOf(data, anno + "-" + mese + "/" + index);
                if (sindex >= 0) {
                    tr.html('<td style="background:#e6e600; padding:0;">' + data[sindex] + '</td>');
                } else
                    tr.html('<td />');
            }
            var tr = tr.next-sibling()
            for (var index = 1; index <= Nday; index++) {
                var sindex = IndexOf(data, anno + "-" + mese + "/" + index);
                if (sindex >= 0) {
                    tr.html('<td style="background:#33cc33; padding:0;">' + data[sindex] + '</td>');
                } else
                    tr.html('<td />');
            }
            var tr = tr.next - sibling()
            for (var index = 1; index <= Nday; index++) {
                var sindex = IndexOf(data, anno + "-" + mese + "/" + index);
                if (sindex >= 0) {
                    tr.html('<td style="background:#cb3434; padding:0;">' + data[sindex] + '</td>');
                } else
                    tr.html('<td />');
            }
        } else
            $(document).trigger("EmptyVisualizzaMese",[anno,mese])
    }).   
}

function IndexOf(giorniMese, date) {
    $.each(giorniMese, function (key, element) {
        if (new Date(data[index].data).toLocaleDateString() == new Date().toLocaleDateString()) {
            return key;
        }
    });
    return -1;
}
