import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('EmployeeContract', () => {
    async function setupFixture() {
        const [owner, billy, john, alice, jeff, employer] = await ethers.getSigners();
        const AdministratorContract = await ethers.getContractFactory('AdministratorContract');
        const taxId = 1234567890;
        const name = "GILMAR RIBEIRO SANTANA";
        const AdministratorDeployed = await AdministratorContract.deploy(taxId, name);
        await AdministratorDeployed.deployed();
        const EmployerContract = await ethers.getContractFactory('EmployerContract');
        const EmployerDeployed = await EmployerContract.deploy(AdministratorDeployed.address);
        await EmployerDeployed.deployed();
        return {
            EmployerDeployed,
            owner,
            employer,
            billy,
            john,
            alice, 
            jeff
        };
    }
    describe("Add an employer", () => {
        it("should add an employer", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const empr = await EmployerDeployed.getEmployerById(0);
            const emprs = await EmployerDeployed.getAllEmployers();
            expect(empr.employerAddress).to.equal(employerAddress);
            expect(empr.idEmployer).to.equal(0);
            expect(empr.taxId).to.equal(taxId);
            expect(empr.name).to.equal(name);
            expect(empr.legalAddress).to.equal(legalAddress);
            expect(emprs.length).to.equal(1);
        });
        it("should not add an employer - sender not administrator", async () => {
            const { EmployerDeployed, alice, john, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await expect(EmployerDeployed.connect(john)
                    .addEmployer(employerAddress, taxId, name, legalAddress))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not add an employer - employer already exists", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("Employer already exists.");
        });
        it("should not add an employer - TaxId not given", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 0;
            const legalAddress = "JACOB ST, 2023";
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not add an employer - Name not given", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("Name not given.");
        });
        it("should not add an employer - Address not given", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = ethers.constants.AddressZero;
            const name = "CONSTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("Address not given.");
        });
     });
    describe("Return an employee", () => {
        it("should return one employee ", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const empr = await EmployerDeployed.getEmployerById(0);
            const sameEmployee = await EmployerDeployed.getEmployerByAddress(employerAddress);
            expect(empr.employerAddress).to.equal(employerAddress);
            expect(sameEmployee.employerAddress).to.equal(employerAddress);
        });
        it("should return a null employee ", async () => {
            const { EmployerDeployed, employer, billy } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const employerNull = await EmployerDeployed.getEmployerByAddress(billy.address);
            const otherNullEmployer = await EmployerDeployed.getEmployerById(1);
            expect(employerNull.employerAddress).to.equal(ethers.constants.AddressZero);
            expect(otherNullEmployer.employerAddress).to.equal(ethers.constants.AddressZero);
        });
    //     it("should not return an employee by id - Sender is not administrator", async () => {
    //         const { EmployeeDeployed, alice, billy, employer} = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         await expect(EmployeeDeployed.connect(billy).getEmployeeById(0))
    //                 .to.rejectedWith("Sender is not administrator.");
    //     });
    //     it("should not return an employee by address - Sender is not administrator", async () => {
    //         const { EmployeeDeployed, alice, billy, employer} = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         await expect(EmployeeDeployed.connect(billy).getEmployeeByAddress(aliceAddress))
    //                 .to.rejectedWith("Sender is not administrator.");
    //     });
    });
    // describe("Update employee", () => {
    //     it("should return an updated employee", async () => {
    //         const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         const newAddress = ethers.Wallet.createRandom().address;
    //         const newTaxId = 3333333333;
    //         const newName = "AMANDA";
    //         const newState = 0;
    //         await EmployeeDeployed.updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState, empr);
    //         const employee = await EmployeeDeployed.getEmployeeByAddress(newAddress);
    //         expect(employee.employeeAddress).to.equal(newAddress);
    //         expect(employee.idEmployee).to.equal(0);
    //         expect(employee.taxId).to.equal(newTaxId);
    //         expect(employee.name).to.equal(newName);
    //         expect(employee.stateOf).to.equal(0);
    //     });
    //     it("should not return an updated employee - Sender is not administrator", async () => {
    //         const { EmployeeDeployed, alice, billy, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         const newAddress = ethers.Wallet.createRandom().address;
    //         const newTaxId = 3333333333;
    //         const newName = "AMANDA";
    //         const newState = 0;
    //         await expect(EmployeeDeployed.connect(billy)
    //             .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState, empr))
    //             .to.rejectedWith("Sender is not administrator.");
    //     });
    //     it("should not return an updated employee - Address not given", async () => {
    //         const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr);
    //         const newAddress = ethers.constants.AddressZero;
    //         const newTaxId = 3333333333;
    //         const newName = "AMANDA";
    //         const newState = 0;
    //         await expect(EmployeeDeployed
    //             .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState, empr))
    //             .to.rejectedWith("Address not given.");
    //     });
    //     it("should not return an updated employee - TaxId not given", async () => {
    //         const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr);
    //         const newAddress = ethers.Wallet.createRandom().address;
    //         const newTaxId = 0;
    //         const newName = "AMANDA";
    //         const newState = 0;
    //         await expect(EmployeeDeployed
    //             .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState, empr))
    //             .to.rejectedWith("TaxId not given.");
    //     });
    //     it("should not return an updated employee - Name not given", async () => {
    //         const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         const newAddress = ethers.Wallet.createRandom().address;
    //         const newTaxId = 3333333333;
    //         const newName = "";
    //         const newState = 0;
    //         await expect(EmployeeDeployed
    //             .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState, empr))
    //             .to.rejectedWith("Name not given.");
    //     });
    //     it("should not return an updated employee - Employee not exists", async () => {
    //         const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
    //         const aliceAddress = alice.address;
    //         const name = "ALICE";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, empr)
    //         const newAddress = ethers.Wallet.createRandom().address;
    //         const newTaxId = 3333333333;
    //         const newName = "AMANDA";
    //         const newState = 0;
    //         await expect(EmployeeDeployed
    //             .updateEmployee(newAddress, newAddress, newTaxId, newName, newState, empr))
    //             .to.rejectedWith("Employee not exists.");
    //     });
    // });
    // describe("Getting all employees", () => {
    //     it("should return three employees", async () => {
    //         const { EmployeeDeployed, billy, john, alice, employer } = await loadFixture(setupFixture);
    //         const nameEmp1 = "BILLY";
    //         const taxIdEmp1 = "1111111111";
    //         const nameEmp2 = "JOHN";
    //         const taxIdEmp2 = "2222222222";
    //         const nameEmp3 = "ALICE";
    //         const taxIdEmp3 = "3333333333";
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1, empr);
    //         await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2, empr);
    //         await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3, empr);
    //         const employees = await EmployeeDeployed.getAllEmployees();
    //         expect(employees.length).to.equal(3);
    //     });
    //     it("should not return employees - Sender is not Administrator", async () => {
    //         const { EmployeeDeployed, billy, john, alice, jeff, employer } = await loadFixture(setupFixture);
    //         const nameEmp1 = "BILLY";
    //         const taxIdEmp1 = "1111111111";
    //         const nameEmp2 = "JOHN";
    //         const taxIdEmp2 = "2222222222";
    //         const nameEmp3 = "ALICE";
    //         const taxIdEmp3 = "3333333333";
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1, empr);
    //         await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2, empr);
    //         await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3, empr);
    //         await expect(EmployeeDeployed.connect(jeff).getAllEmployees())
    //                 .to.rejectedWith("Sender is not administrator.");
    //     });
    // });
    // describe("Checking if employee exists", () => {
    //     it("should return true", async () => {
    //         const { EmployeeDeployed, john, employer } = await loadFixture(setupFixture);
    //         const johnAddress = john.address;
    //         const name = "JOHN";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(johnAddress, name, taxId, empr)
    //         const result = await EmployeeDeployed.checkIfEmployeeExists(johnAddress);
    //         expect(result).to.equal(true);
    //     });
    //     it("should return false", async () => {
    //         const { EmployeeDeployed, billy, john, employer } = await loadFixture(setupFixture);
    //         const johnAddress = john.address;
    //         const name = "JOHN";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(johnAddress, name, taxId, empr)
    //         const result = await EmployeeDeployed.checkIfEmployeeExists(billy.address);
    //         expect(result).to.equal(false);
    //     });
    //     it("should not return - Sender is not Administrator", async () => {
    //         const { EmployeeDeployed, billy, john, employer } = await loadFixture(setupFixture);
    //         const johnAddress = john.address;
    //         const name = "JOHN";
    //         const taxId = 2222222222;
    //         const empr = employer.address;
    //         await EmployeeDeployed.addEmployee(johnAddress, name, taxId, empr)
    //         await expect(EmployeeDeployed.connect(billy).checkIfEmployeeExists(johnAddress))
    //                 .to.rejectedWith("Sender is not administrator.");
    //     });
    // });
    // describe("Getting Employer Contract Address", () => {
    //     it("should returns the address", async () => {
    //         const { EmployeeDeployed, 
    //                 EmployerDeployed } = await loadFixture(setupFixture);
    //         const empr = EmployerDeployed.address;
    //         const theAddress = await EmployeeDeployed.getEmployerContract();
    //         expect(theAddress === empr).to.equal(true);
    //     });
    //     it("should not return - Sender is not Administrator", async () => {
    //         const { EmployeeDeployed, 
    //                 billy } = await loadFixture(setupFixture);
    //         await expect(EmployeeDeployed.connect(billy).getEmployerContract())
    //                     .to.rejectedWith("Sender is not administrator.");
    //     });
    // });
});