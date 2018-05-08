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