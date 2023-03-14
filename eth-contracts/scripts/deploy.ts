import * as dotenv from "dotenv";
import { ethers } from "hardhat";
import * as fs from "fs";
import * as readline from "readline";
import * as Util from "./deployUtil";
import * as Administrator from "./deployAdministrator";

async function main() {

    // Util.toDeployUtilContract().then(() => {
    //     Administrator.toDeployAdministratorContract();
    // })
    // try {
    //     await Util.toDeployUtilContract();
    // } catch (error) {
    //     console.log(error + "util")
    // }
    // try {
    //     await Administrator.toDeployAdministratorContract();
    // } catch (error) {
    //     console.log(error + "administrator")
    // }
    
    // const path: string = './.env';
    // const encoding: string = 'utf8';
    // const taxID: number = 123456789;
    // const name: string = "GILMAR RIBEIRO SANTANA";
    // const timeZone: number = -3;


    // let rl = readline.createInterface({
    //     input: process.stdin,
    //     output: process.stdout
    // });

    // await rl.question('Type the tax ID of the Administrator:\n', (answer) => {
    //     taxID = parseInt(answer);
    // });

    // await rl.question('Type the name os the Administrator:\n', (answer) => {
    //     name = answer.toUpperCase();
    // });

    // rl.close();

    // UtilContract //////////////////////////////
    // const Util = await ethers.getContractFactory("UtilContract");
    // const util = await Util.deploy();
    // await util.deployed();
    // const utilAddress: string = "\nUTIL_ADDRESS=" + util.address;
    // fs.appendFileSync(path, utilAddress, 'utf8');
    // console.log('Util successfull deployed at: '+ util.address);
    
    // // AdministratorContract //////////////////////
    // const Administrator = await ethers.getContractFactory("AdministratorContract");
    // const administrator = await Administrator.deploy(taxID, name);
    // await administrator.deployed();
    // const administratorAddress: string = "\nADMINISTRATOR_ADDRESS=" + administrator.address;
    // fs.appendFileSync(path, administratorAddress, 'utf8');
    // console.log('\nAdministrator successfull deployed at: ' + administrator.address);

    // // EmployeeContract ///////////////////////////
    // const Employee = await ethers.getContractFactory("EmployeeContract");
    // const admAddress = await process.env.ADMINISTRATOR_ADDRESS !== undefined ? process.env.ADMINISTRATOR_ADDRESS : "";
    // console.log("the value:" + admAddress);
    // const employee = await Employee.deploy(admAddress);
    // await employee.deployed();
    // const employeeAddress: string = "\nEMPLOYEE_ADDRESS=" + employee.address;
    // fs.appendFileSync(path, employeeAddress, 'utf8');
    // console.log('\nEmployee successfull deployed at: ' + employee.address);

    // // PontoBlock ////////////////////////////////
    // const PontoBlock = await ethers.getContractFactory("PontoBlock");
    // const ponto_block = await PontoBlock.deploy(process.env.EMPLOYEE_ADDRESS !== undefined ?
    //                                             process.env.EMPLOYEE_ADDRESS : "",
    //                                             process.env.UTIL_ADDRESS !== undefined ?
    //                                             process.env.UTIL_ADDRESS : "",
    //                                             timeZone);
    // await ponto_block.deployed();
    // const ponto_blockAddress: string = "\nPONTOBLOCK_ADDRESS=" + ponto_block.address;
    // await fs.appendFileSync(path, ponto_blockAddress, 'utf8');
    // console.log('\PontoBlock successfull deployed at: ' + ponto_block.address);

    // // PontoBlock ////////////////////////////////
    // const PontoBlockReports = await ethers.getContractFactory("PontoBlockReports");
    // const pontoBlockReports = await PontoBlockReports.deploy(process.env.EMPLOYEE_ADDRESS !== undefined ?
    //                                             process.env.EMPLOYEE_ADDRESS : "",
    //                                             process.env.PONTOBLOCK_ADDRESS !== undefined ?
    //                                             process.env.PONTOBLOCK_ADDRESS : "",
    //                                             process.env.UTIL_ADDRESS !== undefined ?
    //                                             process.env.UTIL_ADDRESS : "",);
    // await pontoBlockReports.deployed();
    // const pontoBlockReportsAddress: string = "\nPONTOBLOCK_ADDRESS=" + pontoBlockReports.address;
    // await fs.appendFileSync(path, pontoBlockReportsAddress, 'utf8');
    // console.log('\PontoBlockReports successfull deployed at: ' + pontoBlockReports.address);

}

// main().catch((error) => {
//     console.error(error);
//     process.exitCode = 1;
// });