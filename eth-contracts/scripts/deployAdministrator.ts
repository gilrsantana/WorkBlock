import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "./handlerEnv"



async function toDeployAdministratorContract() {
    const taxID = process.env.ADMINISTRATOR_TAXID ?? 0;
    const nameAdministrator = process.env.ADMINISTRATOR_NAME ?? '';
    const Administrator = await ethers.getContractFactory("AdministratorContract");
    const administrator = await Administrator.deploy(+taxID, nameAdministrator);
    await administrator.deployed()
    const key = "ADMINISTRATOR_ADDRESS";

    handler(key, administrator.address)
    console.log('\nAdministrator successfull deployed at: ' + administrator.address);

}

toDeployAdministratorContract().catch((error) => {
    console.error('Error at toDeployAdministratorContract:' + error);
    process.exitCode = 1;
});

