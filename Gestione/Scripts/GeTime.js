function changeHTyp(value) {
    if (value == 'Ore di permesso' || value == 'Ore di malattia') {
        // document.getElementById('commesse').hidden = true;
        document.getElementById('commesse').disabled = true;
        document.getElementById('ore').value = "";
        document.getElementById('ore').disabled = false;
    }
    if (value == 'Ore di ferie') {
        document.getElementById('ore').value = 8;
        document.getElementById('ore').disabled = true;
        document.getElementById('commesse').disabled = true;
    }
    if (value == 'Ore di lavoro') {
        document.getElementById('commesse').disabled = false;
        document.getElementById('ore').value = "";
        document.getElementById('ore').disabled = false;
    }
}
//onChange = "javascript:changeHTyp(this.value);