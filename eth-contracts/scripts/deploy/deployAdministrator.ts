import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { contractModel} from "../../src/models/contractModel"
import { IContractService } from "../../src/interface/IContractService";
import { contractRepository } from "../../src/repositories/contractRepository";
import { AppDataSource } from "../../src/database/data-source";
import { contractService } from "../../src/service/contractService";
import { v4 as uuid } from "uuid";

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
        id: "",
        name: nameString,
        addressContract: await address,
        abi: abiString,
        bytecode: bytecodeString,
        createdAt: new Date()
    };
    const service: IContractService = new contractService(new contractRepository());

    const result = await service.getContractByName(nameString);

    if (result === null || result === undefined) {
        contract.id = uuid();
        if (await service.insertContract(contract))
            console.log(contract.name + " successfull insert")
        else
            console.log(contract.name + " not successfull insert")
    } else {
        contract.id = result.id;
        if (await service.updateContract(contract))
            console.log(contract.name + " successfull updated")
        else
            console.log(contract.name + " not successfull updated")
    }
    AppDataSource.destroy();

}

toDeployAdministratorContract().catch((error) => {
    console.error('Error at toDeployAdministratorContract:' + error);
    process.exitCode = 1;
});



