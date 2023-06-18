function loadEventCPF() {
    const inputCPF = document.getElementById("txtCPF");
    inputCPF.addEventListener("keyup", formatarCPF);
}

function loadEventCNS() {
    const inputCNS = document.getElementById("txtCNS");
    inputCNS.addEventListener("keyup", formatarCNS);
}

function loadEventCNPJ() {
    const inputCNPJ = document.getElementById("txtCNPJ");
    inputCNPJ.addEventListener("keyup", formatarCNPJ);
}

function loadEventCEP() {
    const inputCEP = document.getElementById("txtCEP");
    inputCEP.addEventListener("keyup", formatarCEP);
}

function formatarCPF(e) {
    let v = e.target.value.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    e.target.value = v;
}

function formatarCNS(e) {
    let v = e.target.value.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1 $2");
    v = v.replace(/(\d{4})(\d)/, "$1 $2");
    v = v.replace(/(\d{4})(\d)/, "$1 $2");
    v = v.replace(/(\d{4})(\d{1,2})$/, "$1 $2");
    e.target.value = v;
}

function formatarCNPJ(e) {
    let v = e.target.value.replace(/\D/g, "");  
    v = v.replace(/^(\d{2})(\d)/, "$1.$2");
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2");
    v = v.replace(/(\d{4})(\d)/, "$1-$2");
    e.target.value = v;
}

function formatarCEP(e) {
    let v = e.target.value.replace(/\D/g, "");
    v = v.replace(/^(\d{2})(\d)/, "$1.$2");
    v = v.replace(/^(\d{3})(\d)/, "$1-$2");
    v = v.replace(/(\d{3})(\d{1,3})/, "$1-$2");
    e.target.value = v;
}