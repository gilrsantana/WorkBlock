import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('EmployeeContract', () => {
    async function setupFixture() {
        const [owner, billy, john, alice, jeff] = await ethers.getSigners();
        const AdministratorContract = await ethers.getContractFactory('AdministratorContract');
        const taxId = 1234567890;
        const name = "GILMAR RIBEIRO SANTANA";
        const AdministratorDeployed = await AdministratorContract.deploy(taxId, name);
        await AdministratorDeployed.deployed();
        const admAddress = AdministratorDeployed.address;
        const EmployeeContract = await ethers.getContractFactory("EmployeeContract");
        const EmployeeDeployed = await EmployeeContract.deploy(admAddress);
        await EmployeeDeployed.deployed();
        return {
            EmployeeDeployed,
            owner,
            billy,
            john,
            alice, 
            jeff
        };
    }
    describe("Add an employee", () => {
        it("should add an employee", async () => {
            const { EmployeeDeployed, billy } = await loadFixture(setupFixture);
            const billyAddress = billy.address;
            const name = "BILLY";
            const taxId = 1111111111;
            await EmployeeDeployed.addEmployee(billyAddress, name, taxId);
            const employee = await EmployeeDeployed.getEmployeeById(0);
            const employees = await EmployeeDeployed.getAllEmployees();
            expect(employee.employeeAddress).to.equal(billyAddress);
            expect(employee.idEmployee).to.equal(0);
            expect(employee.taxId).to.equal(taxId);
            expect(employee.name).to.equal(name);
            expect(employee.stateOf).to.equal(1);
            expect(employees.length).to.equal(1);
        });
        it("should not add an employee - sender not administrator", async () => {
            const { EmployeeDeployed, alice, john } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await expect(EmployeeDeployed.connect(john)
                    .addEmployee(aliceAddress, name, taxId))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not add an employee - employee already exists", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId);
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId))
                .to.rejectedWith("Employee already exists.");
        });
        it("should not add an employee - TaxId not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 0;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not add an employee - Name not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name ="";
            const taxId = 1111111111;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId))
                .to.rejectedWith("Name not given.");
        });
        it("should not add an employee - Address not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = ethers.constants.AddressZero;
            const name ="ALICE";
            const taxId = 1111111111;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId))
                .to.rejectedWith("Address not given.");
        });
     });
    describe("Return an employee", () => {
        it("should return one employee ", async () => {
            const { EmployeeDeployed, alice} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const employee = await EmployeeDeployed.getEmployeeById(0);
            const sameEmployee = await EmployeeDeployed.getEmployeeByAddress(aliceAddress);
            expect(employee.employeeAddress).to.equal(aliceAddress);
            expect(sameEmployee.employeeAddress).to.equal(aliceAddress);
        });
        it("should return a null employee ", async () => {
            const { EmployeeDeployed, alice, billy} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const employeeNull = await EmployeeDeployed.getEmployeeByAddress(billy.address);
            const otherNullEmployee = await EmployeeDeployed.getEmployeeById(1);
            expect(employeeNull.employeeAddress).to.equal(ethers.constants.AddressZero);
            expect(otherNullEmployee.employeeAddress).to.equal(ethers.constants.AddressZero);
        });
        it("should not return an employee by id - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            await expect(EmployeeDeployed.connect(billy).getEmployeeById(0))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not return an employee by address - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            await expect(EmployeeDeployed.connect(billy).getEmployeeByAddress(aliceAddress))
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
    describe("Update employee", () => {
        it("should return an updated employee", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await EmployeeDeployed.updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState);
            const employee = await EmployeeDeployed.getEmployeeByAddress(newAddress);
            expect(employee.employeeAddress).to.equal(newAddress);
            expect(employee.idEmployee).to.equal(0);
            expect(employee.taxId).to.equal(newTaxId);
            expect(employee.name).to.equal(newName);
            expect(employee.stateOf).to.equal(0);
        });
        it("should not return an updated employee - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(EmployeeDeployed.connect(billy)
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Sender is not administrator.");
        });
        it("should not return an updated employee - Address not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId);
            const newAddress = ethers.constants.AddressZero;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Address not given.");
        });
        it("should not return an updated employee - TaxId not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId);
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 0;
            const newName = "AMANDA";
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not return an updated employee - Name not given", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "";
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Name not given.");
        });
        it("should not return an updated employee - Employee not exists", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(newAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Employee not exists.");
        });
    });
    describe("Getting all employees", () => {
        it("should return three employees", async () => {
            const { EmployeeDeployed, billy, john, alice } = await loadFixture(setupFixture);
            const nameEmp1 = "BILLY";
            const taxIdEmp1 = "1111111111";
            const nameEmp2 = "JOHN";
            const taxIdEmp2 = "2222222222";
            const nameEmp3 = "ALICE";
            const taxIdEmp3 = "3333333333";
            await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1);
            await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2);
            await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3);
            const employees = await EmployeeDeployed.getAllEmployees();
            expect(employees.length).to.equal(3);
        });
        it("should not return employees - Sender is not Administrator", async () => {
            const { EmployeeDeployed, billy, john, alice, jeff } = await loadFixture(setupFixture);
            const nameEmp1 = "BILLY";
            const taxIdEmp1 = "1111111111";
            const nameEmp2 = "JOHN";
            const taxIdEmp2 = "2222222222";
            const nameEmp3 = "ALICE";
            const taxIdEmp3 = "3333333333";
            await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1);
            await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2);
            await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3);
            await expect(EmployeeDeployed.connect(jeff).getAllEmployees())
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
    describe("Checking if employee exists", () => {
        it("should return true", async () => {
            const { EmployeeDeployed, john } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId)
            const result = await EmployeeDeployed.checkIfEmployeeExists(johnAddress);
            expect(result).to.equal(true);
        });
        it("should return false", async () => {
            const { EmployeeDeployed, billy, john } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId)
            const result = await EmployeeDeployed.checkIfEmployeeExists(billy.address);
            expect(result).to.equal(false);
        });
        it("should not return - Sender is not Administrator", async () => {
            const { EmployeeDeployed, billy, john } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId)
            await expect(EmployeeDeployed.connect(billy).checkIfEmployeeExists(johnAddress))
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
});