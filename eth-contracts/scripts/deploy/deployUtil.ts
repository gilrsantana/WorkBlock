import { contractModel } from "../../models/contractModel";
import { ethers } from "hardhat";
import { insertContract } from "../context/context";

async function toDeployUtilContract() {
    const Util = await ethers.getContractFactory("UtilContract");
    const util = await Util.deploy();
    await util.deployed();

    const fs = require('fs');
    const data = fs.readFileSync('artifacts/contracts/UtilContract.sol/UtilContract.json', { encoding: 'utf8' });
    const parsedData = JSON.parse(data);
    const nameString = JSON.stringify(parsedData.contractName).replace('"', '').replace('"', '').toString();
    const abiString = JSON.stringify(parsedData.abi).toString();
    const bytecodeString = JSON.stringify(parsedData.bytecode).replace('"', '').replace('"', '').toString();
    const address = await util.address.toString();
    const contract: contractModel = {
        name: nameString,
        addressContract: await address,
        abi: abiString,
        bytecode: bytecodeString
    };
    await insertContract(contract);
}

toDeployUtilContract().catch((error) => {
    console.error('Error at toDeployUtilContract:' + error);
    process.exitCode = 1;
});