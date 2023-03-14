import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "./handlerEnv"

async function toDeployPontoBlockContract() {
    const employee = process.env.EMPLOYEE_ADDRESS ?? '';
    const util = process.env.UTIL_ADDRESS ?? '';
    const timeZone = process.env.TIME_ZONE ?? 0;
    const PontoBlock = await ethers.getContractFactory("PontoBlock");
    const pontoBlock = await PontoBlock.deploy(employee, util, +timeZone);
    await pontoBlock.deployed()
    const key = "PONTOBLOCK_ADDRESS";

    handler(key, pontoBlock.address)
    console.log('\nPontoBlock successfull deployed at: ' + pontoBlock.address);
}

toDeployPontoBlockContract().catch((error) => {
    console.error('Error at toDeployPontoBlockContract:' + error);
    process.exitCode = 1;
});

