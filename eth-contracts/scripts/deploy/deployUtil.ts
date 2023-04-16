import { ethers } from "hardhat";
import { contractService } from "../../src/service/contractService";
import { IContractService } from "../../src/interface/IContractService";
import { utilTools } from "../../src/util/utilTools";
import { IContractModel } from "../../src/interface/IContractModel";

async function toDeployUtilContract() {
    const Util = await ethers.getContractFactory("UtilContract");
    const util = await Util.deploy();
    await util.deployed();

    const contract: IContractModel = utilTools.buildContract('artifacts/contracts/UtilContract.sol/UtilContract.json', util.address.toString())
    const service: IContractService = new contractService(contract);
    
    await utilTools.handlerService(service, contract);
}

toDeployUtilContract().catch((error) => {
    console.error('Error at toDeployUtilContract:' + error);
    process.exitCode = 1;
});

