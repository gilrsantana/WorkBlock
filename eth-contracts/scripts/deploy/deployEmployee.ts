import * as dotenv from "dotenv";
dotenv.config();
import { ethers } from "hardhat";
import { handler } from "../util/handlerEnv"

async function toDeployEmployeeContract() {
    const admAddress = process.env.ADMINISTRATOR_ADDRESS ?? '';
    const emprAddress = process.env.EMPLOYER_ADDRESS ?? '';
    const utilAddress = process.env.UTIL_ADDRESS ?? '';
    const Employee = await ethers.getContractFactory("EmployeeContract");
    const employee = await Employee.deploy(admAddress,emprAddress, utilAddress);
    await employee.deployed()
    const key = "EMPLOYEE_ADDRESS";

    handler(key, employee.address)
    console.log('\nEmployee successfull deployed at: ' + employee.address);

}

toDeployEmployeeContract().catch((error) => {
    console.error('Error at toDeployEmployeeContract:' + error);
    process.exitCode = 1;
});

