import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "../util/handlerEnv"

async function toDeployEmployerContract() {
    const admAddress = process.env.ADMINISTRATOR_ADDRESS ?? '';
    const Employer = await ethers.getContractFactory("EmployerContract");
    const employer = await Employer.deploy(admAddress);
    await employer.deployed()
    const key = "EMPLOYER_ADDRESS";

    handler(key, employer.address)
    console.log('\nEmployer successfull deployed at: ' + employer.address);

}

toDeployEmployerContract().catch((error) => {
    console.error('Error at toDeployEmployerContract:' + error);
    process.exitCode = 1;
});

