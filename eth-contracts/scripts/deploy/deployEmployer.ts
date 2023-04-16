import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "../util/handlerEnv"
import { IContractService } from "../../src/interface/IContractService";
import { contractService } from "../../src/service/contractService";
import { contractModel } from "../../src/models/contractModel";
import { IContractModel } from "../../src/interface/IContractModel";
import { utilTools } from "../../src/util/utilTools";

async function toDeployEmployerContract() {
    const service: IContractService = new contractService(new contractModel());
    const administrator: any = await service.getContractByName("AdministratorContract")
    const admAddress: string = administrator.addressContract;

    const Employer = await ethers.getContractFactory("EmployerContract");
    const employer = await Employer.deploy(admAddress);
    await employer.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/EmployerContract.sol/EmployerContract.json', employer.address.toString())
    await utilTools.handlerService(service, contract);
}

toDeployEmployerContract().catch((error) => {
    console.error('Error at toDeployEmployerContract:' + error);
    process.exitCode = 1;
});

