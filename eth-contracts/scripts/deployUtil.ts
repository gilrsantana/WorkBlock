import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "./handlerEnv"


async function toDeployUtilContract() {
    const Util = await ethers.getContractFactory("UtilContract");
    const util = await Util.deploy();
    await util.deployed();
    const key = "UTIL_ADDRESS";

    handler(key, util.address);
    console.log('Util successfull deployed at: ' + util.address);
}

toDeployUtilContract().catch((error) => {
    console.error('Error at toDeployUtilContract:' + error);
    process.exitCode = 1;
});