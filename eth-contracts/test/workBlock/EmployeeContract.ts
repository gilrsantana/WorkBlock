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
        const emprAdd = employer.address;
        const emprTaxId = 5555555555;
        const emprName = "CONTOSO COMPANY";
        const emprLegalAdd = "JACOB ST, 123.";
        await EmployerDeployed.addEmployer(emprAdd, emprTaxId, emprName, emprLegalAdd);
        const emprAddress = EmployerDeployed.address;
        const admAddress = AdministratorDeployed.address;
        const UtilContract = await ethers.getContractFactory('UtilContract');
        const UtilDeployed = await UtilContract.deploy();
        await UtilDeployed.deployed();
        const utilAddress = UtilDeployed.address;
        const EmployeeContract = await ethers.getContractFactory("EmployeeContract");
        const EmployeeDeployed = await EmployeeContract.deploy(admAddress, emprAddress, utilAddress);
        await EmployeeDeployed.deployed();
        await AdministratorDeployed.addAdministrator(EmployeeDeployed.address,
                                                    "EmployeeContract",
                                                    9999999999);
        
        return {
            EmployeeDeployed,
            EmployerDeployed,
            owner,
            employer,
            billy,
            john,
            alice, 
            jeff
        };
    }
    describe("Add an employee", () => {
        it("should add an employee", async () => {
            const { EmployeeDeployed, billy, employer } = await loadFixture(setupFixture);
            const billyAddress = billy.address;
            const name = "BILLY";
            const taxId = 1111111111;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(billyAddress, name, taxId, beggining, end, empr);
            const employee = await EmployeeDeployed.getEmployeeById(0);
            const employees = await EmployeeDeployed.getAllEmployees();
            expect(employee.employeeAddress).to.equal(billyAddress);
            expect(employee.idEmployee).to.equal(0);
            expect(employee.taxId).to.equal(taxId);
            expect(employee.name).to.equal(name);
            expect(employee.begginingWorkDay).to.equal(beggining);
            expect(employee.endWorkDay).to.equal(end);
            expect(employee.stateOf).to.equal(1);
            expect(employees.length).to.equal(1);
        });
        it("should not add an employee - sender not administrator", async () => {
            const { EmployeeDeployed, alice, john, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await expect(EmployeeDeployed.connect(john)
                    .addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                    .to.rejectedWith("Sender must be administrator and be active.");
        });
        it("should not add an employee - Employer not exists", async () => {
            const { EmployeeDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = alice.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Employer not exists.");
        });
        it("should not add an employee - employee already exists", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr);
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Employee already exists.");
        });
        it("should not add an employee - TaxId not given", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 0;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not add an employee - Name not given", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name ="";
            const taxId = 1111111111;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Name not given.");
        });
        it("should not add an employee - Address not given", async () => {
            const { EmployeeDeployed, employer } = await loadFixture(setupFixture);
            const aliceAddress = ethers.constants.AddressZero;
            const name ="ALICE";
            const taxId = 1111111111;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Address not given.");
        });
        it("should not add an employee - Not valid beggining work day", async () => {
            const { EmployeeDeployed, employer, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name ="ALICE";
            const taxId = 1111111111;
            const beggining = 3200;
            const end = 1800;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Not valid beggining work day.");
        });
        it("should not add an employee - Not valid end work day", async () => {
            const { EmployeeDeployed, employer, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name ="ALICE";
            const taxId = 1111111111;
            const beggining = 800;
            const end = 1897;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Not valid end work day.");
        });
        it("should not add an employee - Beggining Work Day must be less than End Work Day", async () => {
            const { EmployeeDeployed, employer, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name ="ALICE";
            const taxId = 1111111111;
            const beggining = 800;
            const end = 800;
            const empr = employer.address;
            await expect(EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr))
                .to.rejectedWith("Beggining Work Day must be less than End Work Day.");
        });
     });
    describe("Return an employee", () => {
        it("should return one employee ", async () => {
            const { EmployeeDeployed, alice, employer} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const employee = await EmployeeDeployed.getEmployeeById(0);
            const sameEmployee = await EmployeeDeployed.getEmployeeByAddress(aliceAddress);
            expect(employee.employeeAddress).to.equal(aliceAddress);
            expect(sameEmployee.employeeAddress).to.equal(aliceAddress);
        });
        it("should return a null employee ", async () => {
            const { EmployeeDeployed, alice, billy, employer} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const employeeNull = await EmployeeDeployed.getEmployeeByAddress(billy.address);
            const otherNullEmployee = await EmployeeDeployed.getEmployeeById(1);
            expect(employeeNull.employeeAddress).to.equal(ethers.constants.AddressZero);
            expect(otherNullEmployee.employeeAddress).to.equal(ethers.constants.AddressZero);
        });
        it("should not return an employee by id - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy, employer} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            await expect(EmployeeDeployed.connect(billy).getEmployeeById(0))
                    .to.rejectedWith("Sender must be administrator and be active.");
        });
        it("should not return an employee by address - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy, employer} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            await expect(EmployeeDeployed.connect(billy).getEmployeeByAddress(aliceAddress))
                    .to.rejectedWith("Sender must be administrator and be active.");
        });
    });
    describe("Update employee", () => {
        it("should return an updated employee", async () => {
            const { EmployeeDeployed, 
                    EmployerDeployed,
                    alice, 
                    employer } = await loadFixture(setupFixture);
            const aNewEmprAddress = ethers.Wallet.createRandom().address;
            await EmployerDeployed.addEmployer(aNewEmprAddress,
                                                666666666,
                                                "NEW COMPANY",
                                                "NEW ST,123");
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await EmployeeDeployed.updateEmployee(aliceAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, aNewEmprAddress);
            const employee = await EmployeeDeployed.getEmployeeByAddress(newAddress);
            expect(employee.employeeAddress).to.equal(newAddress);
            expect(employee.idEmployee).to.equal(0);
            expect(employee.taxId).to.equal(newTaxId);
            expect(employee.name).to.equal(newName);
            expect(employee.stateOf).to.equal(0);
            expect(employee.employerAddress).to.equal(aNewEmprAddress);
        });
        it("should not return an updated employee - Sender is not administrator", async () => {
            const { EmployeeDeployed, alice, billy, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await expect(EmployeeDeployed.connect(billy)
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, empr))
                .to.rejectedWith("Sender must be administrator and be active.");
        });
        it("should not return an updated employee - Address not given", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr);
            const newAddress = ethers.constants.AddressZero;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, empr))
                .to.rejectedWith("Address not given.");
        });
        it("should not return an updated employee - TaxId not given", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr);
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 0;
            const newName = "AMANDA";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, empr))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not return an updated employee - Name not given", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(aliceAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, empr))
                .to.rejectedWith("Name not given.");
        });
        it("should not return an updated employee - Employee not exists", async () => {
            const { EmployeeDeployed, alice, employer } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(aliceAddress, name, taxId, beggining, end, empr)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newBeggining = 930;
            const newEnd = 1730;
            const newState = 0;
            await expect(EmployeeDeployed
                .updateEmployee(newAddress, newAddress, newTaxId, newName, newBeggining, newEnd, newState, empr))
                .to.rejectedWith("Employee not exists.");
        });
    });
    describe("Getting all employees", () => {
        it("should return three employees", async () => {
            const { EmployeeDeployed, billy, john, alice, employer } = await loadFixture(setupFixture);
            const nameEmp1 = "BILLY";
            const taxIdEmp1 = "1111111111";
            const nameEmp2 = "JOHN";
            const taxIdEmp2 = "2222222222";
            const nameEmp3 = "ALICE";
            const taxIdEmp3 = "3333333333";
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1, beggining, end, empr);
            await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2, beggining, end, empr);
            await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3, beggining, end, empr);
            const employees = await EmployeeDeployed.getAllEmployees();
            expect(employees.length).to.equal(3);
        });
        it("should not return employees - Sender is not Administrator", async () => {
            const { EmployeeDeployed, billy, john, alice, jeff, employer } = await loadFixture(setupFixture);
            const nameEmp1 = "BILLY";
            const taxIdEmp1 = "1111111111";
            const nameEmp2 = "JOHN";
            const taxIdEmp2 = "2222222222";
            const nameEmp3 = "ALICE";
            const taxIdEmp3 = "3333333333";
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1, beggining, end, empr);
            await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2, beggining, end, empr);
            await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3, beggining, end, empr);
            await expect(EmployeeDeployed.connect(jeff).getAllEmployees())
                    .to.rejectedWith("Sender must be administrator and be active.");
        });
    });
    describe("Checking if employee exists", () => {
        it("should return true", async () => {
            const { EmployeeDeployed, john, employer } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId, beggining, end, empr)
            const result = await EmployeeDeployed.checkIfEmployeeExists(johnAddress);
            expect(result).to.equal(true);
        });
        it("should return false", async () => {
            const { EmployeeDeployed, billy, john, employer } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId, beggining, end, empr)
            const result = await EmployeeDeployed.checkIfEmployeeExists(billy.address);
            expect(result).to.equal(false);
        });
        it("should not return - Sender is not Administrator", async () => {
            const { EmployeeDeployed, billy, john, employer } = await loadFixture(setupFixture);
            const johnAddress = john.address;
            const name = "JOHN";
            const taxId = 2222222222;
            const beggining = 800;
            const end = 1800;
            const empr = employer.address;
            await EmployeeDeployed.addEmployee(johnAddress, name, taxId, beggining, end, empr)
            await expect(EmployeeDeployed.connect(billy).checkIfEmployeeExists(johnAddress))
                    .to.rejectedWith("Sender must be administrator and be active.");
        });
    });
    describe("Getting Employer Contract Address", () => {
        it("should returns the address", async () => {
            const { EmployeeDeployed, 
                    EmployerDeployed } = await loadFixture(setupFixture);
            const empr = EmployerDeployed.address;
            const theAddress = await EmployeeDeployed.getEmployerContract();
            expect(theAddress === empr).to.equal(true);
        });
        it("should not return - Sender is not Administrator", async () => {
            const { EmployeeDeployed, 
                    billy } = await loadFixture(setupFixture);
            await expect(EmployeeDeployed.connect(billy).getEmployerContract())
                        .to.rejectedWith("Sender must be administrator and be active.");
        });
    });
});