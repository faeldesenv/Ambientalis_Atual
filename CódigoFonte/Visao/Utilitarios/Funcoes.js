// Início do código de Aumentar/ Diminuir a letra
// Para usar coloque o comando: "javascript:mudaTamanho('tag_ou_id_alvo', -1);" para diminuir
// e o comando "javascript:mudaTamanho('tag_ou_id_alvo', +1);" para aumentar
var tagAlvo = new Array('p'); //pega todas as tags p//
// Especificando os possíveis tamanhos de fontes, poderia ser: x-small, small...
var tamanhos = new Array('9px', '10px', '11px', '12px', '13px', '14px', '15px');
var tamanhoInicial = 2;

function mudaTamanho(idAlvo, acao) {
    if (!document.getElementById) return
    var selecionados = null, tamanho = tamanhoInicial, i, j, tagsAlvo;
    tamanho += acao;
    if (tamanho < 0) tamanho = 0;
    if (tamanho > 6) tamanho = 6;
    tamanhoInicial = tamanho;
    if (!(selecionados = document.getElementById(idAlvo))) selecionados = document.getElementsByTagName(idAlvo)[0];

    selecionados.style.fontSize = tamanhos[tamanho];

    for (i = 0; i < tagAlvo.length; i++) {
        tagsAlvo = selecionados.getElementsByTagName(tagAlvo[i]);
        for (j = 0; j < tagsAlvo.length; j++) tagsAlvo[j].style.fontSize = tamanhos[tamanho];
    }
}


function goTop(acceleration, time) {
    acceleration = acceleration || 0.1;
    time = time || 16;
    var dx = 0;
    var dy = 0;
    var bx = 0;
    var by = 0;
    var wx = 0;
    var wy = 0;
    if (document.documentElement) {
        dx = document.documentElement.scrollLeft || 0;
        dy = document.documentElement.scrollTop || 0;
    }

    if (document.body) {
        bx = document.body.scrollLeft || 0;
        by = document.body.scrollTop || 0;
    }
    var wx = window.scrollX || 0;
    var wy = window.scrollY || 0;
    var x = Math.max(wx, Math.max(bx, dx));
    var y = Math.max(wy, Math.max(by, dy));
    var speed = 1 + acceleration;
    window.scrollTo(Math.floor(x / speed), Math.floor(y / speed));
    if (x > 0 || y > 0) {
        var invokeFunction = "goTop(" + acceleration + ", " + time + ")"
        window.setTimeout(invokeFunction, time);
    }
}

function printDiv(id, pg) {
    var oPrint, oJan;
    oPrint = window.document.getElementById(id).innerHTML;
    oJan = window.open(pg);
    oJan.document.write(oPrint);
    oJan.window.print();
    oJan.document.close();
    oJan.focus();
}

 