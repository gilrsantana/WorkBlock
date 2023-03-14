import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "../util/handlerEnv";

async function toDeployPontoBlockReportsContract() {
    const employee = process.env.EMPLOYEE_ADDRESS ?? '';
    const ponto = process.env.PONTOBLOCK_ADDRESS ?? '';
    const util = process.env.UTIL_ADDRESS ?? '';
    const PontoBlockReports = await ethers.getContractFactory("PontoBlockReports");
    const pontoBlockReports = await PontoBlockReports.deploy(employee, ponto, util);
    await pontoBlockReports.deployed()
    const key = "PONTOBLOCKREPORTS_ADDRESS";
    handler(key, pontoBlockReports.address)
    console.log('\nPontoBlockReports successfull deployed at: ' + pontoBlockReports.address);
}

toDeployPontoBlockReportsContract().catch((error) => {
    console.error('Error at toDeployPontoBlockReportsContract:' + error);
    process.exitCode = 1;
});

