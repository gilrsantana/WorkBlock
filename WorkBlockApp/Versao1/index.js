import { errors, ethers } from "./library/ethers-5.1.esm.min.js";
import {
  pontoBlockAddress,
  pontoBlockReportsAddress,
  utilAddress,
  pontoBlockAbi,
  pontoBlockReportsABI,
  utilABI,
  endPoint,
  timeZone
} from "./global/constants.js";

const connectButton = document.getElementById("btnConnect");
const startWorkButton = document.getElementById("btnStartWork");
const breakStartTimeButton = document.getElementById("btnBreakStartTime");
const breakEndTimeButton = document.getElementById("btnBreakEndTime");
const endWorkButton = document.getElementById("btnEndWork");
const txtAddress = document.getElementById("txtAddress");
const btnHistorico = document.getElementById("btnHistorico");

let account;

startWorkButton.disabled =
  breakStartTimeButton.disabled =
  breakEndTimeButton.disabled =
  endWorkButton.disabled =
  txtAddress.disabled =
  true;

connectButton.onclick = connect;
startWorkButton.onclick = startWorkFunction;
breakStartTimeButton.onclick = breakStartTimeFunction;
breakEndTimeButton.onclick = breakEndTimeFunction;
endWorkButton.onclick = endWorkFunction;
btnHistorico.onclick = getHistoric;

async function connect() {
  if (typeof window.ethereum !== "undefined") {
    let accounts = await window.ethereum.request({
      method: "eth_requestAccounts",
    });
    connectButton.innerHTML = "Connected";

    account = accounts[0];
    let address = document.getElementById("txtAddress");
    address.innerHTML = account;
    address.value = account;
    btnHistorico.style.display = "block";
    getEmployeeData();
    await getEmployeeRecordsFunction().then(() => {
      handleCards();
    });
  } else {
    connectButton.innerHTML = "Please, install Metamask";
    connectButton.addEventListener('click', function () {
      window.open('https://metamask.io/download/');
    });



  }
}

function handleCards() {
  if (document.getElementById("txtInicioResult").innerHTML == "") {
    startWorkButton.disabled = false;
    let field = document.getElementById("fieldInicioJornada");
    field.style.backgroundColor = "#10c027";
    field.style.color = "#000";
  }
  if (document.getElementById("txtPausaInicioResult").innerHTML == "") {
    breakStartTimeButton.disabled = false;
    let field = document.getElementById("fieldInicioPausa");
    field.style.backgroundColor = "#1e5ed6";
    field.style.color = "#000";
  }
  if (document.getElementById("txtPausaFimResult").innerHTML == "") {
    breakEndTimeButton.disabled = false;
    let field = document.getElementById("fieldFimPausa");
    field.style.backgroundColor = "#facc00";
    field.style.color = "#000";
  }
  if (document.getElementById("txtFimResult").innerHTML == "") {
    endWorkButton.disabled = false;
    let field = document.getElementById("fieldFimJornada");
    field.style.backgroundColor = "#df3c51";
    field.style.color = "#000";
  }
}

async function startWorkFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockAddress, pontoBlockAbi, signer);
    try {
      const response = await contract.startWork();
      await listenForTransaction(response, provider).then(async () => {
        await getEmployeeRecordsFunction().then(() => {
          if (document.getElementById("txtInicioResult").innerHTML != "") {
            startWorkButton.disabled = true;
            let field = document.getElementById("fieldInicioJornada");
            field.style.backgroundColor = "#076614";
            field.style.color = "#efefef";
          }
        });
      });
      alert("Início de jornada registrado com sucesso!");
    } catch (error) {
      alert(error.data.message);
    }
  }
}

async function breakStartTimeFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockAddress, pontoBlockAbi, signer);
    try {
      const response = await contract.breakStartTime();
      await listenForTransaction(response, provider).then(async () => {
        await getEmployeeRecordsFunction().then(() => {
          if (document.getElementById("txtPausaInicioResult").innerHTML != "") {
            breakStartTimeButton.disabled = true;
            let field = document.getElementById("fieldInicioPausa");
            field.style.backgroundColor = "#1c3c77";
            field.style.color = "#efefef";
          }
        });
      });
      alert("Início de pausa registrado com sucesso!");
    } catch (error) {
      alert(error.data.message);
    }
  }
}

async function breakEndTimeFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockAddress, pontoBlockAbi, signer);
    try {
      const response = await contract.breakEndTime();
      await listenForTransaction(response, provider).then(async () => {
        await getEmployeeRecordsFunction().then(() => {
          if (document.getElementById("txtPausaFimResult").innerHTML != "") {
            breakEndTimeButton.disabled = true;
            let field = document.getElementById("fieldFimPausa");
            field.style.backgroundColor = "#8b740a";
            field.style.color = "#efefef";
          }
        });
      });
      alert("Fim de pausa registrado com sucesso!");
    } catch (error) {
      alert(error.data.message);
    }
  }
}

async function endWorkFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockAddress, pontoBlockAbi, signer);
    try {
      const response = await contract.endWork();
      await listenForTransaction(response, provider).then(async () => {
        await getEmployeeRecordsFunction().then(() => {
          if (document.getElementById("txtFimResult").innerHTML != "") {
            endWorkButton.disabled = true;
            let field = document.getElementById("fieldFimJornada");
            field.style.backgroundColor = "#74222d";
            field.style.color = "#efefef";
          }
        });
      });
      alert("Fim de Jornada registrado com sucesso!");
    } catch (error) {
      alert(error.data.message);
    }
  }
}

function getEmployeeData() {
  var url = `${endPoint}employeecontract/Get/${account}`;
  var xhr = new XMLHttpRequest();
  xhr.open('GET', url, true);

  xhr.onload = function () {
    if (xhr.status >= 200 && xhr.status < 400) {
      var resultRequest = JSON.parse(xhr.responseText);
      setEmployeeName(resultRequest.data.name);
      getEmployerName(resultRequest.data.employerAddress);
    } else {
      console.error('Ocorreu um erro:', xhr.status);
    }
  };

  xhr.onerror = function () {
    console.error('Erro de conexão');
  };

  xhr.send();
}

function setEmployeeName(name) {
  document.getElementById("txtNome").value = name;
}

function setEmployer(employer) {
  document.getElementById("txtEmpregador").value = employer;
}

function getEmployerName(employerAddress) {
  var url = `${endPoint}employercontract/Get/${employerAddress}`;
  var xhr = new XMLHttpRequest();
  xhr.open('GET', url, true);

  xhr.onload = function () {
    if (xhr.status >= 200 && xhr.status < 400) {
      var resultRequest = JSON.parse(xhr.responseText);
      setEmployer(resultRequest.data.name);
    } else {
      console.error('Ocorreu um erro:', xhr.status);
    }
  };

  xhr.onerror = function () {
    console.error('Erro de conexão');
  };

  xhr.send();
}

async function getEmployeeRecordsFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockReportsAddress, pontoBlockReportsABI, signer);
    const utilContract = new ethers.Contract(utilAddress, utilABI, signer);

    try {
      const secondsUTC = Math.floor(new Date() / 1000);
      const referenceDate = await utilContract.getDate(secondsUTC + timeZone);
      const response = await contract.getWorkTimesFromEmployeeAtDate(
        account,
        referenceDate
      );

      const inicioJornada =
        response[0]._hex != 0x00
          ? new Date(parseInt(response[0]._hex) * 1000).toString()
          : "Sem registro";

      const inicioPausa =
        response[2]._hex != 0x00
          ? new Date(parseInt(response[2]._hex) * 1000).toString()
          : "Sem registro";

      const fimPausa =
        response[3]._hex != 0x00
          ? new Date(parseInt(response[3]._hex) * 1000).toString()
          : "Sem registro";

      const fimJornada =
        response[1]._hex != 0x00
          ? new Date(parseInt(response[1]._hex) * 1000).toString()
          : "Sem registro";

      inicioJornada != "Sem registro" ? (document.getElementById("txtInicioResult").innerHTML = formatarData(inicioJornada)) : "";
      inicioPausa != "Sem registro" ? (document.getElementById("txtPausaInicioResult").innerHTML = formatarData(inicioPausa)) : "";
      fimPausa != "Sem registro" ? (document.getElementById("txtPausaFimResult").innerHTML = formatarData(fimPausa)) : "";
      fimJornada != "Sem registro" ? (document.getElementById("txtFimResult").innerHTML = formatarData(fimJornada)) : "";

    } catch (error) {
      console.log(error);
    }
  }
}

function formatarData(data) {
  var data = new Date(data);

  var dia = data.getDate();
  var mes = data.getMonth() + 1; // O mês é zero-indexado
  var ano = data.getFullYear();
  var horas = data.getHours();
  var minutos = data.getMinutes();
  var segundos = data.getSeconds();

  // Formatação dos valores para dois dígitos, caso necessário
  dia = dia < 10 ? "0" + dia : dia;
  mes = mes < 10 ? "0" + mes : mes;
  horas = horas < 10 ? "0" + horas : horas;
  minutos = minutos < 10 ? "0" + minutos : minutos;
  segundos = segundos < 10 ? "0" + segundos : segundos;

  return dia + "/" + mes + "/" + ano + " " + horas + ":" + minutos + ":" + segundos;
}

async function getHistoric() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockReportsAddress, pontoBlockReportsABI, signer);
    const utilContract = new ethers.Contract(utilAddress, utilABI, signer);

    try {
      let secondsUTC = Math.floor(new Date() / 1000);
      const endDate = await utilContract.getDate(secondsUTC + timeZone);

      const currentDate = new Date();
      const thirtyDaysAgo = new Date();
      thirtyDaysAgo.setDate(currentDate.getDate() - 20);
      const timestampMilliseconds = thirtyDaysAgo.getTime();

      secondsUTC = Math.floor(timestampMilliseconds / 1000);
      const startDate = await utilContract.getDate(secondsUTC + timeZone);
      const response = await contract.getWorkTimeFromEmployeeBetweenTwoDates(
        account,
        startDate,
        endDate
      );

      const dateArray = [];
      const startWorkArray = [];
      const endWorkArray = [];
      const startPauseArray = [];
      const endPauseArray = [];

      for (let i = 0; i < response[0].length; i++) {
        dateArray.push(parseInt(response[0][i]._hex, 16).toString());
      }

      for (let i = 0; i < response[1].length; i++) {
        startWorkArray.push(parseInt(response[1][i]._hex, 16).toString());
      }

      for (let i = 0; i < response[2].length; i++) {
        endWorkArray.push(parseInt(response[2][i]._hex, 16).toString());
      }

      for (let i = 0; i < response[3].length; i++) {
        startPauseArray.push(parseInt(response[3][i]._hex, 16).toString());
      }

      for (let i = 0; i < response[4].length; i++) {
        endPauseArray.push(parseInt(response[4][i]._hex, 16).toString());
      }

      const divElement = document.getElementById("historico");

      const tableElement = document.createElement("table");
      tableElement.className += "table-striped table-bordered text-center w-100 table-success";

      const headerRow = document.createElement("tr");

      const headerCell1 = document.createElement("th");
      headerCell1.textContent = "Data";

      const headerCell2 = document.createElement("th");
      headerCell2.textContent = "Início";

      const headerCell3 = document.createElement("th");
      headerCell3.textContent = "Pausa";

      const headerCell4 = document.createElement("th");
      headerCell4.textContent = "Retorno";

      const headerCell5 = document.createElement("th");
      headerCell5.textContent = "Saída";

      headerRow.appendChild(headerCell1);
      headerRow.appendChild(headerCell2);
      headerRow.appendChild(headerCell3);
      headerRow.appendChild(headerCell4);
      headerRow.appendChild(headerCell5);

      tableElement.appendChild(headerRow);

      for (let i = 0; i < dateArray.length; i++) {
        const row = document.createElement("tr");
        const rowCell1 = document.createElement("td");
        rowCell1.textContent = formatDate(dateArray[i]);

        const rowCell2 = document.createElement("td");
        rowCell2.textContent = startWorkArray[i] !== "0" ? formatTime(startWorkArray[i]) : "--:--:--";

        const rowCell3 = document.createElement("td");
        rowCell3.textContent = startPauseArray[i] !== "0" ? formatTime(startPauseArray[i]) : "--:--:--";

        const rowCell4 = document.createElement("td");
        rowCell4.textContent = endPauseArray[i] !== "0" ? formatTime(endPauseArray[i]) : "--:--:--";

        const rowCell5 = document.createElement("td");
        rowCell5.textContent = endWorkArray[i] !== "0" ? formatTime(endWorkArray[i]) : "--:--:--";

        row.appendChild(rowCell1);
        row.appendChild(rowCell2);
        row.appendChild(rowCell3);
        row.appendChild(rowCell4);
        row.appendChild(rowCell5);

        tableElement.appendChild(row);
      }
      
      divElement.appendChild(tableElement);
    } catch (error) {
      console.log(error);
    }
  }
}

function formatDate(date) {
  return date.substring(6) + "/" + date.substring(4, 6) + "/" + date.substring(0, 4);
}

function formatTime(value) {
  let date = new Date(+value);
  let hours = date.getHours();
  let minutes = date.getMinutes();
  let seconds = date.getSeconds();

  hours = hours < 10 ? "0" + hours : hours;
  minutes = minutes < 10 ? "0" + minutes : minutes;
  seconds = seconds < 10 ? "0" + seconds : seconds;
  return hours + ":" + minutes + ":" + seconds;
}

function listenForTransaction(transactionResponse, provider) {
  return new Promise((resolve, reject) => {
    provider.once(transactionResponse.hash, (transactionReceipt) => {
      alert(`Operação realizada com ${transactionReceipt.confirmations} confirmações!`);
      resolve();
    });
  });
}
