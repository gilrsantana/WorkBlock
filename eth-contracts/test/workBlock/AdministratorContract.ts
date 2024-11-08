import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('AdministratorContract', () => {
    async function setupFixture() {
        const [owner, billy, john, alice] = await ethers.getSigners();
        const AdministratorContract = await ethers.getContractFactory('AdministratorContract');
        const taxId = 1234567890;
        const name = "GILMAR RIBEIRO SANTANA";
        const AdministratorDeployed = await AdministratorContract.deploy(taxId, name);
        await AdministratorDeployed.deployed();
        return {
            AdministratorDeployed,
            owner,
            billy,
            john,
            alice,
            name,
            taxId
        };
    }
    describe("Contract was initialized", () => {
        it("should return one administrator ", async () => {
            const { AdministratorDeployed, owner, name, taxId } = await loadFixture(setupFixture);
            const adm = await AdministratorDeployed.getAdministrator(0);
            const adms = await AdministratorDeployed.getAllAdministrators();
            expect(adm.administratorAddress).to.equal(owner.address);
            expect(adm.idAdministrator).to.equal(0);
            expect(adm.taxId).to.equal(taxId);
            expect(adm.name).to.equal(name);
            expect(adm.stateOf).to.equal(1);
            expect(adms.length).to.equal(1);
        });
    });
    describe("Add an administrator", () => {
        it("should add an administrator", async () => {
            const { AdministratorDeployed, billy } = await loadFixture(setupFixture);
            const billyAddress = billy.address;
            const name = "BILLY";
            const taxId = 1111111111;
            await AdministratorDeployed.addAdministrator(billyAddress, name, taxId);
            const adm = await AdministratorDeployed.getAdministrator(1);
            const adms = await AdministratorDeployed.getAllAdministrators();
            expect(adm.administratorAddress).to.equal(billyAddress);
            expect(adm.idAdministrator).to.equal(1);
            expect(adm.taxId).to.equal(taxId);
            expect(adm.name).to.equal(name);
            expect(adm.stateOf).to.equal(1);
            expect(adms.length).to.equal(2);
        });
        it("should not add an administrator - sender not administrator", async () => {
            const { AdministratorDeployed, alice, john } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await expect(AdministratorDeployed.connect(john).
                addAdministrator(aliceAddress, name, taxId))
                .to.rejectedWith("Sender must be administrator.");
        });
        it("should not add an administrator - sender is not active", async () => {
            const { AdministratorDeployed, alice, john } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newState = 0;
            await AdministratorDeployed.updateAdministrator(aliceAddress, aliceAddress, taxId, name, newState);
            const johnAddress = john.address;
            const nameJohn = "JOHN";
            const taxIdJohn = 44444444444;
            await expect(AdministratorDeployed.connect(alice).addAdministrator(johnAddress, nameJohn, taxIdJohn))
                .to.rejectedWith("Administrator is not active.");
        });
        it("should not add an administrator - administrator already exists", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.
            addAdministrator(aliceAddress, name, taxId)
            await expect(AdministratorDeployed.
                addAdministrator(aliceAddress, name, taxId))
                .to.rejectedWith("Administrator already exists.");
        });
    });
    describe("Return an administrator", () => {
        it("should return one administrator ", async () => {
            const { AdministratorDeployed, alice} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const adm = await AdministratorDeployed.getAdministrator(1);
            expect(adm.administratorAddress).to.equal(aliceAddress);
        });
        it("should return a null administrator ", async () => {
            const { AdministratorDeployed, alice} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const admNull = await AdministratorDeployed.getAdministrator(2);
            expect(admNull.administratorAddress).to.equal(ethers.constants.AddressZero);
        });
        it("should not return an administrator - Sender is not administrator", async () => {
            const { AdministratorDeployed, alice, john} = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            await expect(AdministratorDeployed.connect(john).getAdministrator(2))
                    .to.rejectedWith("Sender must be administrator.");
        });
        it("should not return an administrator - sender is not active", async () => {
            const { AdministratorDeployed, alice, john } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newState = 0;
            await AdministratorDeployed.updateAdministrator(aliceAddress, aliceAddress, taxId, name, newState);
            const johnAddress = john.address;
            const nameJohn = "JOHN";
            const taxIdJohn = 44444444444;
            await AdministratorDeployed.addAdministrator(johnAddress, nameJohn, taxIdJohn)
            await expect(AdministratorDeployed.connect(alice).getAdministrator(1))
                .to.rejectedWith("Administrator is not active.");
        });
    });
    describe("Update administrator", () => {
        it("should return an updated administrator", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await AdministratorDeployed.updateAdministrator(aliceAddress, newAddress, newTaxId, newName, newState);
            const adm = await AdministratorDeployed.getAdministrator(1);
            expect(adm.administratorAddress).to.equal(newAddress);
            expect(adm.idAdministrator).to.equal(1);
            expect(adm.taxId).to.equal(newTaxId);
            expect(adm.name).to.equal(newName);
            expect(adm.stateOf).to.equal(0);
        });
        it("should not return an updated administrator - Sender is not administrator", async () => {
            const { AdministratorDeployed, alice, billy } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(AdministratorDeployed.connect(billy).
                updateAdministrator(aliceAddress, newAddress ,newTaxId, newName, newState))
                .to.rejectedWith("Sender must be administrator.");
        });
        it("should not return an updated administrator - Sender is not active", async () => {
            const { AdministratorDeployed, alice, john } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newState = 0;
            await AdministratorDeployed.updateAdministrator(aliceAddress, aliceAddress, taxId, name, newState);
            const johnAddress = john.address;
            const nameJohn = "JOHN";
            const taxIdJohn = 44444444444;
            await AdministratorDeployed.addAdministrator(johnAddress, nameJohn, taxIdJohn)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            await expect(AdministratorDeployed.connect(alice).
                updateAdministrator(johnAddress, newAddress,newTaxId, newName, newState))
                .to.rejectedWith("Administrator is not active.");
        });
        it("should not return an updated administrator - Address not given", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId);
            const newAddress = ethers.constants.AddressZero;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(AdministratorDeployed
                .updateAdministrator(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Address not given.");
        });
        it("should not return an updated administrator - TaxId not given", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId);
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 0;
            const newName = "AMANDA";
            const newState = 0;
            await expect(AdministratorDeployed
                .updateAdministrator(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("TaxId not given.");
        });
        it("should not return an updated administrator - Name not given", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId);
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "";
            const newState = 0;
            await expect(AdministratorDeployed
                .updateAdministrator(aliceAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Name not given.");
        });
        it("should not return an updated administrator - Administrator not exists", async () => {
            const { AdministratorDeployed, alice } = await loadFixture(setupFixture);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newAddress = ethers.Wallet.createRandom().address;
            const newTaxId = 3333333333;
            const newName = "AMANDA";
            const newState = 0;
            await expect(AdministratorDeployed
                .updateAdministrator(newAddress, newAddress, newTaxId, newName, newState))
                .to.rejectedWith("Administrator not exists.");
        });
    });
    describe("Getting all administrators", () => {
        it("should not return four administrators - Sender is not administrator", async () => {
            const { AdministratorDeployed, billy, john, alice } = await loadFixture(setupFixture);
            const nameAdm1 = "BILLY";
            const taxIdAdm1 = "1111111111";
            const nameAdm2 = "JOHN";
            const taxIdAdm2 = "2222222222";
            await AdministratorDeployed.addAdministrator(billy.address, nameAdm1, taxIdAdm1);
            await AdministratorDeployed.addAdministrator(john.address, nameAdm2, taxIdAdm2);
            await expect(AdministratorDeployed.connect(alice).
                getAllAdministrators()).to.rejectedWith("Sender must be administrator.");
        });
        it("should not return four administrators - Sender is not active", async () => {
            const { AdministratorDeployed, billy, john, alice } = await loadFixture(setupFixture);
            const nameAdm1 = "BILLY";
            const taxIdAdm1 = "1111111111";
            const nameAdm2 = "JOHN";
            const taxIdAdm2 = "2222222222";
            await AdministratorDeployed.addAdministrator(billy.address, nameAdm1, taxIdAdm1);
            await AdministratorDeployed.addAdministrator(john.address, nameAdm2, taxIdAdm2);
            const aliceAddress = alice.address;
            const name = "ALICE";
            const taxId = 2222222222;
            await AdministratorDeployed.addAdministrator(aliceAddress, name, taxId)
            const newState = 0;
            await AdministratorDeployed.updateAdministrator(aliceAddress, aliceAddress, taxId, name, newState);
            await expect(AdministratorDeployed.connect(alice).
                getAllAdministrators()).to.rejectedWith("Administrator is not active.");
        });
        it("should return four administrators", async () => {
            const { AdministratorDeployed, billy, john, alice } = await loadFixture(setupFixture);
            const nameAdm1 = "BILLY";
            const taxIdAdm1 = "1111111111";
            const nameAdm2 = "JOHN";
            const taxIdAdm2 = "2222222222";
            const nameAdm3 = "ALICE";
            const taxIdAdm3 = "3333333333";
            await AdministratorDeployed.addAdministrator(billy.address, nameAdm1, taxIdAdm1);
            await AdministratorDeployed.addAdministrator(john.address, nameAdm2, taxIdAdm2);
            await AdministratorDeployed.addAdministrator(alice.address, nameAdm3, taxIdAdm3);
            const adms = await AdministratorDeployed.getAllAdministrators();
            expect(adms.length).to.equal(4);
        });
    });
    describe("Checking if administrator exists", () => {
        it("should return true", async () => {
            const { AdministratorDeployed, owner } = await loadFixture(setupFixture);
            const result = await AdministratorDeployed.checkIfAdministratorExists(owner.address);
            expect(result).to.equal(true);
        });
        it("should return false", async () => {
            const { AdministratorDeployed, billy } = await loadFixture(setupFixture);
            const result = await AdministratorDeployed.checkIfAdministratorExists(billy.address);
            expect(result).to.equal(false);
        });
    });
    describe("Checking if administrator is active", () => {
        it("should return true", async () => {
            const { AdministratorDeployed, owner } = await loadFixture(setupFixture);
            const result = await AdministratorDeployed.checkIfAdministratorIsActive(owner.address);
            expect(result).to.equal(true);
        });
        it("should return false", async () => {
            const { AdministratorDeployed, billy } = await loadFixture(setupFixture);
            await AdministratorDeployed.addAdministrator(billy.address, "BILLY", 123456789);
            await AdministratorDeployed.updateAdministrator(billy.address, billy.address, 123456789, "BILLY", 0);
            const result = await AdministratorDeployed.checkIfAdministratorIsActive(billy.address);
            expect(result).to.equal(false);
        });
    });
});