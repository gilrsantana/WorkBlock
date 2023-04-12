import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { contractModel } from "../../models/contractModel";
import { insertContract } from "../context/context";



async function toDeployAdministratorContract() {
    const taxID = process.env.ADMINISTRATOR_TAXID ?? 0;
    const nameAdministrator = process.env.ADMINISTRATOR_NAME ?? '';
    const Administrator = await ethers.getContractFactory("AdministratorContract");
    const administrator = await Administrator.deploy(+taxID, nameAdministrator);
    await administrator.deployed();

    const fs = require('fs');
    const data = fs.readFileSync('artifacts/contracts/AdministratorContract.sol/AdministratorContract.json', { encoding: 'utf8' });
    const parsedData = JSON.parse(data);
    const nameString = JSON.stringify(parsedData.contractName).replace('"', '').replace('"', '').toString();
    const abiString = JSON.stringify(parsedData.abi).toString();
    const bytecodeString = JSON.stringify(parsedData.bytecode).replace('"', '').replace('"', '').toString();
    const address = await administrator.address.toString();
    const contract: contractModel = {
        name: nameString,
        addressContract: await address,
        abi: abiString,
        bytecode: bytecodeString
    };
    await insertContract(contract);

}

toDeployAdministratorContract().catch((error) => {
    console.error('Error at toDeployAdministratorContract:' + error);
    process.exitCode = 1;
});

