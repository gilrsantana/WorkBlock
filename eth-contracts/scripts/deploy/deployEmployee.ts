import { ethers } from "hardhat";
import { IContractService } from "../../src/interface/IContractService";
import { contractModel } from "../../src/models/contractModel";
import { contractService } from "../../src/service/contractService";
import { IContractModel } from "../../src/interface/IContractModel";
import { utilTools } from "../../src/util/utilTools";
import { AppDataSource } from "../../src/database/data-source";

async function toDeployEmployeeContract() {
    const service: IContractService = new contractService(new contractModel());
    const administrator: any = await service.getContractByName("AdministratorContract");
    const employer: any = await service.getContractByName("EmployerContract");
    const util: any = await service.getContractByName("UtilContract");

    const admAddress = administrator.addressContract;
    const emprAddress = employer.addressContract;
    const utilAddress = util.addressContract;
    const Employee = await ethers.getContractFactory("EmployeeContract");
    const employee = await Employee.deploy(admAddress,emprAddress, utilAddress);
    await employee.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/EmployeeContract.sol/EmployeeContract.json', employee.address.toString())
    await utilTools.handlerService(service, contract);
}

toDeployEmployeeContract().catch((error) => {
    console.error('Error at toDeployEmployeeContract:' + error);
    process.exitCode = 1;
    AppDataSource.destroy();
});

