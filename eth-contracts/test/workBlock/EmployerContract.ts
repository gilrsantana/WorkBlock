import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('EmployerContract', () => {
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
        it("should not add an employer - Address not given", async () => {
            const { EmployerDeployed } = await loadFixture(setupFixture);
            const employerAddress = ethers.constants.AddressZero;
            const name = "CONSTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("Address not given.");
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
        it("should not add an employer - Name not given", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "";
            await expect(EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress))
                .to.rejectedWith("Legal address not given.");
        });
     });
    describe("Return an employer", () => {
        it("should return one employer ", async () => {
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
        it("should return a null employer ", async () => {
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
        it("should not return an employer by id - Sender is not administrator", async () => {
            const { EmployerDeployed, 
                    billy, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            await expect(EmployerDeployed.connect(billy).getEmployerById(0))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not return an employer by address - Sender is not administrator", async () => {
            const { EmployerDeployed, 
                    billy, 
                    employer} = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            await expect(EmployerDeployed.connect(billy).getEmployerByAddress(employerAddress))
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
    describe("Update employer", () => {
        it("should return an updated employer", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newName = "AMANDA";
            const newTaxId = 3333333333;
            const newLegalAddress = "MARY ST, 1999";
            await EmployerDeployed.updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress);
            const empr = await EmployerDeployed.getEmployerByAddress(newAddress);
            expect(empr.employerAddress).to.equal(newAddress);
            expect(empr.idEmployer).to.equal(0);
            expect(empr.taxId).to.equal(newTaxId);
            expect(empr.name).to.equal(newName);
            expect(empr.legalAddress).to.equal(newLegalAddress);
        });
        it("should not return an updated employer - Sender is not administrator", async () => {
            const { EmployerDeployed, 
                    employer, 
                    billy } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newName = "AMANDA";
            const newTaxId = 3333333333;
            const newLegalAddress = "MARY ST, 1999";
            await expect(EmployerDeployed.connect(billy)
                .updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("Sender is not administrator.");
        });
        it("should not return an updated employer - Address not given", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.constants.AddressZero;
            const newName = "AMANDA";
            const newTaxId = 3333333333;
            const newLegalAddress = "MARY ST, 1999";
            await expect(EmployerDeployed
                .updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("Address not given.");
        });
        it("should not return an updated employer - TaxId not given", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newName = "AMANDA";
            const newTaxId = 0;
            const newLegalAddress = "MARY ST, 1999";
            await expect(EmployerDeployed
                .updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not return an updated employer - Name not given", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newName = "";
            const newTaxId = 2222222222;
            const newLegalAddress = "MARY ST, 1999";
            await expect(EmployerDeployed
                .updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("Name not given.");
        });
        it("should not return an updated employer - Name not given", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newName = "SUCCESS COMPANY";
            const newTaxId = 2222222222;
            const newLegalAddress = "";
            await expect(EmployerDeployed
                .updateEmployer(employerAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("Legal address not given.");
        });
        it("should not return an updated employer - Employer not exists", async () => {
            const { EmployerDeployed, 
                    employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newLegalAddress = "MARY ST, 1999";
            await expect(EmployerDeployed
                .updateEmployer(newAddress, newAddress, newTaxId, newName, newLegalAddress))
                .to.rejectedWith("Employer not exists.");
        });
    });
    describe("Getting all employers", () => {
        it("should return three employers", async () => {
            const { EmployerDeployed, billy, john, alice, employer } = await loadFixture(setupFixture);
            const nameEmpr1 = "BILLY COMPANY";
            const taxIdEmpr1 = 1111111111;
            const legalAdd1 = "MATHEUS ST, 100";
            const nameEmpr2 = "JOHN COMPANY";
            const taxIdEmpr2 = 2222222222;
            const legalAdd2 = "MARK ST, 200";
            const nameEmpr3 = "ALICE COMPANY";
            const taxIdEmpr3 = 3333333333;
            const legalAdd3 = "LUCK ST, 300";
            await EmployerDeployed.addEmployer(billy.address, taxIdEmpr1, nameEmpr1, legalAdd1);
            await EmployerDeployed.addEmployer(john.address, taxIdEmpr2, nameEmpr2, legalAdd2);
            await EmployerDeployed.addEmployer(alice.address, taxIdEmpr3, nameEmpr3, legalAdd3);
            const employers = await EmployerDeployed.getAllEmployers();
            expect(employers.length).to.equal(3);
        });
        it("should not return employers - Sender is not Administrator", async () => {
            const { EmployerDeployed, billy, john, alice, jeff } = await loadFixture(setupFixture);
            const nameEmpr1 = "BILLY COMPANY";
            const taxIdEmpr1 = 1111111111;
            const legalAdd1 = "MATHEUS ST, 100";
            const nameEmpr2 = "JOHN COMPANY";
            const taxIdEmpr2 = 2222222222;
            const legalAdd2 = "MARK ST, 200";
            const nameEmpr3 = "ALICE COMPANY";
            const taxIdEmpr3 = 3333333333;
            const legalAdd3 = "LUCK ST, 300";
            await EmployerDeployed.addEmployer(billy.address, taxIdEmpr1, nameEmpr1, legalAdd1);
            await EmployerDeployed.addEmployer(john.address, taxIdEmpr2, nameEmpr2, legalAdd2);
            await EmployerDeployed.addEmployer(alice.address, taxIdEmpr3, nameEmpr3, legalAdd3);
            await expect(EmployerDeployed.connect(jeff).getAllEmployers())
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
    describe("Checking if employer exists", () => {
        it("should return true", async () => {
            const { EmployerDeployed, employer } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const result = await EmployerDeployed.checkIfEmployerExists(employerAddress);
            expect(result).to.equal(true);
        });
        it("should return false", async () => {
            const { EmployerDeployed, employer, billy } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            const result = await EmployerDeployed.checkIfEmployerExists(billy.address);
            expect(result).to.equal(false);
        });
        it("should not return - Sender is not Administrator", async () => {
            const { EmployerDeployed, employer, billy } = await loadFixture(setupFixture);
            const employerAddress = employer.address;
            const name = "CONTOSO COMPANY";
            const taxId = 1111111111;
            const legalAddress = "JACOB ST, 2023";
            await EmployerDeployed.addEmployer(employerAddress, taxId, name, legalAddress);
            await expect(EmployerDeployed.connect(billy).checkIfEmployerExists(employerAddress))
                    .to.rejectedWith("Sender is not administrator.");
        });
    });
});