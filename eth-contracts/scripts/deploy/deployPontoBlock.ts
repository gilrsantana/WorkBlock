import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { IContractService } from "../../src/interface/IContractService";
import { contractModel } from "../../src/models/contractModel";
import { contractService } from "../../src/service/contractService";
import { IContractModel } from "../../src/interface/IContractModel";
import { utilTools } from "../../src/util/utilTools";
import { AppDataSource } from "../../src/database/data-source";

async function toDeployPontoBlockContract() {
    const service: IContractService = new contractService(new contractModel());
    const administrator: any = await service.getContractByName("AdministratorContract");
    const employee: any = await service.getContractByName("EmployeeContract");
    const util: any = await service.getContractByName("UtilContract");

    const employeeAddress = employee.addressContract;
    const utilAddress = util.addressContract;
    const adminAddress = administrator.addressContract;
    const timeZone = process.env.TIME_ZONE ?? 0;
    const PontoBlock = await ethers.getContractFactory("PontoBlock");
    const pontoBlock = await PontoBlock.deploy(employeeAddress, utilAddress, adminAddress, +timeZone);
    await pontoBlock.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/PontoBlock.sol/PontoBlock.json', pontoBlock.address.toString())
    await utilTools.handlerService(service, contract);
}

toDeployPontoBlockContract().catch((error) => {
    console.error('Error at toDeployPontoBlockContract:' + error);
    process.exitCode = 1;
    AppDataSource.destroy();
});

