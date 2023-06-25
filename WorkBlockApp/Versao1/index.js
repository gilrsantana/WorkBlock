import { errors, ethers } from "./library/ethers-5.1.esm.min.js";
import { pontoBlockAddress, pontoBlockReportsAddress, utilAddress, pontoBlockAbi, pontoBlockReportsABI, utilABI } from "./global/constants.js";

const connectButton = document.getElementById("btnConnect");
const startWorkButton = document.getElementById("btnStartWork");
const breakStartTimeButton = document.getElementById("btnBreakStartTime");
const breakEndTimeButton = document.getElementById("btnBreakEndTime");
const endWorkButton = document.getElementById("btnEndWork");
const btnHistoryButton = document.getElementById("btnHistory");
const txtAddress = document.getElementById("txtAddress");
const txtDate = document.getElementById("txtDate");

let account;

startWorkButton.disabled =
  breakStartTimeButton.disabled =
  breakEndTimeButton.disabled =
  endWorkButton.disabled =
  btnHistoryButton.disabled =
  txtAddress.disabled =
  txtDate.disabled =
  true;

txtAddress.value = txtDate.value = "";

connectButton.onclick = connect;
startWorkButton.onclick = startWorkFunction;
breakStartTimeButton.onclick = breakStartTimeFunction;
breakEndTimeButton.onclick = breakEndTimeFunction;
endWorkButton.onclick = endWorkFunction;
btnHistoryButton.onclick = getEmployeeRecordsFunction;

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
    await getEmployeeRecordsFunction().then(() => {
      if (document.getElementById("txtInicioResult").innerHTML == "") {
        startWorkButton.disabled = false;
      }
      if (document.getElementById("txtPausaInicioResult").innerHTML == "") {
        breakStartTimeButton.disabled = false;
      }
      if (document.getElementById("txtPausaFimResult").innerHTML == "") {
        breakEndTimeButton.disabled = false;
      }
      if (document.getElementById("txtFimResult").innerHTML == "") {
        endWorkButton.disabled = false;
      }
    });
    // startWorkButton.disabled =
    //   breakStartTimeButton.disabled =
    //   breakEndTimeButton.disabled =
    //   endWorkButton.disabled =
    //   btnHistoryButton.disabled =
    //   txtDate.disabled =
    //   false;
    // if (document.getElementById("txtInicioResult").value == "") {
    //   startWorkButton.disabled = false;
    // }
  } else {
    connectButton.innerHTML = "Please, install Metamask";
  }
}

async function startWorkFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(pontoBlockAddress, pontoBlockAbi, signer);
    try {
      const response = await contract.startWork();
      await listenForTransaction(response, provider);
      alert("Start Work success request!");
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
      await listenForTransaction(response, provider);
      alert("Break Start Time success request!");
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
      await listenForTransaction(response, provider);
      alert("Break End Time success request!");
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
      await listenForTransaction(response, provider);
      alert("End Work success request!");
    } catch (error) {
      alert(error.data.message);
    }
  }
}

async function getEmployeeRecordsFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();

    const contract = new ethers.Contract(pontoBlockReportsAddress, pontoBlockReportsABI, signer);
    const utilContract = new ethers.Contract(utilAddress, utilABI, signer);

    // fetch(`http://localhost:5075/v1/contracts/pontoblock/GetEmployeeRecords?address=${account}&timestamp=1685750054`)
    // .then(response => response.json())
    // .then(result => {
    //   // Handle the response data
    //   console.log(new Date(result.data.startWork).toUTCString());
    // })
    // .catch(error => {
    //   // Handle any errors
    //   console.error('Error:', error);
    // });

    try {
      const secondsUTC = Math.floor(new Date() / 1000);
      const referenceDate = await utilContract.getDate(secondsUTC);
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

      document.getElementById("resultHistory").innerHTML = "";
      document.getElementById("resultHistory").innerHTML +=
        "<br>Início da Jornada: " + inicioJornada;
      document.getElementById("resultHistory").innerHTML +=
        "<br>Início da Pausa: " + inicioPausa;
      document.getElementById("resultHistory").innerHTML +=
        "<br>Fim da Pausa: " + fimPausa;
      document.getElementById("resultHistory").innerHTML +=
        "<br>Fim da Jornada: " + fimJornada;

      inicioJornada != "Sem registro" ? (document.getElementById("txtInicioResult").innerHTML = formatarData(inicioJornada)) : "";
      inicioPausa != "Sem registro" ? (document.getElementById("txtPausaInicioResult").innerHTML = formatarData(inicioPausa)) : "";
      fimPausa != "Sem registro" ? (document.getElementById("txtPausaFimResult").innerHTML = formatarData(fimPausa)) : "";
      fimJornada != "Sem registro" ? (document.getElementById("txtFimResult").innerHTML = formatarData(fimJornada)) : "";
      //   document.getElementById("txtInicioResult").innerHTML = formatarData(inicioJornada);
      // document.getElementById("txtPausaInicioResult").innerHTML = formatarData(inicioPausa);
      // document.getElementById("txtPausaFimResult").innerHTML = formatarData(fimPausa);
      // document.getElementById("txtFimResult").innerHTML = formatarData(fimJornada);
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

function listenForTransaction(transactionResponse, provider) {
  return new Promise((resolve, reject) => {
    provider.once(transactionResponse.hash, (transactionReceipt) => {
      alert(`Completed with ${transactionReceipt.confirmations} confirmations`);
      resolve();
    });
  });
}
