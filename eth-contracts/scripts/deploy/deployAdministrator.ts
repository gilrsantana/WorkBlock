import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { IContractService } from "../../src/interface/IContractService";
import { contractService } from "../../src/service/contractService";
import { utilTools } from "../../src/util/utilTools";
import { IContractModel } from "../../src/interface/IContractModel";
import { AppDataSource } from "../../src/database/data-source";

async function toDeployAdministratorContract() {
    const taxID = process.env.ADMINISTRATOR_TAXID ?? 0;
    const nameAdministrator = process.env.ADMINISTRATOR_NAME ?? '';
    const Administrator = await ethers.getContractFactory("AdministratorContract");
    const administrator = await Administrator.deploy(+taxID, nameAdministrator);
    await administrator.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/AdministratorContract.sol/AdministratorContract.json', administrator.address.toString())
    const service: IContractService = new contractService(contract);
    await utilTools.handlerService(service, contract);
}

toDeployAdministratorContract().catch((error) => {
    console.error('Error at toDeployAdministratorContract:' + error);
    process.exitCode = 1;
    AppDataSource.destroy();
});



