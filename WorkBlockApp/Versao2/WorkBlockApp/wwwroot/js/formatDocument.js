function loadEventCPF() {
    const inputCPF = document.getElementById("txtCPF");
    inputCPF.addEventListener("keyup", formatarCPF);
}

function loadEventCNS() {
    const inputCNS = document.getElementById("txtCNS");
    inputCNS.addEventListener("keyup", formatarCNS);
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