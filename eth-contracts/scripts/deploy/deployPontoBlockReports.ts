import { ethers } from "hardhat";
import { AppDataSource } from "../../src/database/data-source";
import { IContractService } from "../../src/interface/IContractService";
import { contractModel } from "../../src/models/contractModel";
import { contractService } from "../../src/service/contractService";
import { IContractModel } from "../../src/interface/IContractModel";
import { utilTools } from "../../src/util/utilTools";

async function toDeployPontoBlockReportsContract() {
    const service: IContractService = new contractService(new contractModel());
    const employee: any = await service.getContractByName("EmployeeContract");
    const ponto: any = await service.getContractByName("PontoBlock");
    const util: any = await service.getContractByName("UtilContract");
    const administrator: any = await service.getContractByName("AdministratorContract");

    const employeeAddress = employee.addressContract;
    const pontoAddresss = ponto.addressContract;
    const utilAddress = util.addressContract;
    const administratorAddress = administrator.addressContract;
    const PontoBlockReports = await ethers.getContractFactory("PontoBlockReports");
    const pontoBlockReports = await PontoBlockReports.deploy(employeeAddress, pontoAddresss, utilAddress, administratorAddress);
    await pontoBlockReports.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/PontoBlockReports.sol/PontoBlockReports.json', pontoBlockReports.address.toString())
    await utilTools.handlerService(service, contract);
}

toDeployPontoBlockReportsContract().catch((error) => {
    console.error('Error at toDeployPontoBlockReportsContract:' + error);
    process.exitCode = 1;
    AppDataSource.destroy();
});

