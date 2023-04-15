import { contractModel } from "../../src/models/contractModel"
import { ethers } from "hardhat";
import { contractService } from "../../src/service/contractService";
import { IContractService } from "../../src/interface/IContractService";
import { v4 as uuid } from "uuid";
import { contractRepository } from "../../src/repositories/contractRepository";
import { AppDataSource } from "../../src/database/data-source";

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

toDeployUtilContract().catch((error) => {
    console.error('Error at toDeployUtilContract:' + error);
    process.exitCode = 1;
});

