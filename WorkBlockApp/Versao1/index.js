import { errors, ethers } from "./ethers-5.1.esm.min.js";
import { contractAddres, contractAbi } from "./constants.js";

const connectButton = document.getElementById("btnConnect");
const startWorkButton = document.getElementById("btnStartWork");
const breakStartTimeButton = document.getElementById("btnBreakStartTime");
const breakEndTimeButton = document.getElementById("btnBreakEndTime");
const endWorkButton = document.getElementById("btnEndWork");
const btnHistoryButton = document.getElementById("btnHistory");
const txtAddress = document.getElementById("txtAddress");
const txtDate = document.getElementById("txtDate");

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

    let account = accounts[0];
    let address = document.getElementById("txtAddress");
    address.innerHTML = account;
    address.value = account;
    startWorkButton.disabled =
      breakStartTimeButton.disabled =
      breakEndTimeButton.disabled =
      endWorkButton.disabled =
      btnHistoryButton.disabled =
      txtDate.disabled =
        false;
  } else {
    connectButton.innerHTML = "Please, install Metamask";
  }
}

async function startWorkFunction() {
  if (typeof window.ethereum !== "undefined") {
    const provider = new ethers.providers.Web3Provider(window.ethereum);
    const signer = provider.getSigner();
    const contract = new ethers.Contract(contractAddres, contractAbi, signer);
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
    const contract = new ethers.Contract(contractAddres, contractAbi, signer);
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
    const contract = new ethers.Contract(contractAddres, contractAbi, signer);
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
    const contract = new ethers.Contract(contractAddres, contractAbi, signer);
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
    const contract = new ethers.Contract(contractAddres, contractAbi, signer);

    fetch('http://localhost:5075/v1/contracts/pontoblock/GetEmployeeRecords?address=0xE98806d8998CAE1A45338C5F138438C70B273c05&timestamp=1685750054')
    .then(response => response.json())
    .then(result => {
      // Handle the response data
      console.log(new Date(result.data.startWork).toUTCString());
    })
    .catch(error => {
      // Handle any errors
      console.error('Error:', error);
    });

    try {
      const response = await contract.getEmployeeRecords(
        txtAddress.value,
        txtDate.value
      );

      const inicioJornada =
        response[0]._hex != 0x00
          ? new Date(parseInt(response[0]._hex) * 1000).toUTCString()
          : "Sem registro";

      const inicioPausa =
        response[2]._hex != 0x00
          ? new Date(parseInt(response[2]._hex) * 1000).toUTCString()
          : "Sem registro";

      const fimPausa =
        response[3]._hex != 0x00
          ? new Date(parseInt(response[3]._hex) * 1000).toUTCString()
          : "Sem registro";

      const fimJornada =
        response[1]._hex != 0x00
          ? new Date(parseInt(response[1]._hex) * 1000).toUTCString()
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
    } catch (error) {
      console.log(error);
    }
  }
}

function listenForTransaction(transactionResponse, provider) {
  return new Promise((resolve, reject) => {
    provider.once(transactionResponse.hash, (transactionReceipt) => {
      alert(`Completed with ${transactionReceipt.confirmations} confirmations`);
      resolve();
    });
  });
}
