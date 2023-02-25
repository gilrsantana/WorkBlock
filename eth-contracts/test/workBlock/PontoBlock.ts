import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('PontoBlock', () => {
    async function setupFixture() {
        const [owner, billy, john, alice, jeff, rachel] = await ethers.getSigners();
        const UtilContract = await ethers.getContractFactory('UtilContract');
        const UtilDeployed = await UtilContract.deploy();
        await UtilDeployed.deployed();
        const AdministratorContract = await ethers.getContractFactory('AdministratorContract');
        const taxId = 1234567890;
        const name = "GILMAR RIBEIRO SANTANA";
        const AdministratorDeployed = await AdministratorContract.deploy(taxId, name);
        await AdministratorDeployed.deployed();
        const EmployeeContract = await ethers.getContractFactory("EmployeeContract");
        const EmployeeDeployed = await EmployeeContract.deploy(AdministratorDeployed.address);
        await EmployeeDeployed.deployed();
        const nameEmp1 = "BILLY";
        const taxIdEmp1 = 1111111111;
        const nameEmp2 = "JOHN";
        const taxIdEmp2 = 2222222222;
        const nameEmp3 = "ALICE";
        const taxIdEmp3 = 3333333333;
        const nameEmp4 = "RACHEL";
        const taxIdEmp4 = 4444444444;
        await EmployeeDeployed.addEmployee(billy.address, nameEmp1, taxIdEmp1);
        await EmployeeDeployed.addEmployee(john.address, nameEmp2, taxIdEmp2);
        await EmployeeDeployed.addEmployee(alice.address, nameEmp3, taxIdEmp3);
        await EmployeeDeployed.addEmployee(rachel.address, nameEmp4, taxIdEmp4);
        const inactiveEmployee =  await EmployeeDeployed.getEmployeeByAddress(rachel.address);
        await EmployeeDeployed.updateEmployee(  inactiveEmployee.employeeAddress
                                              , inactiveEmployee.employeeAddress
                                              , inactiveEmployee.taxId
                                              , inactiveEmployee.name
                                              , 0);
        const PontoBlock = await ethers.getContractFactory("PontoBlock");
        const PontoBlockDeployed = await PontoBlock.deploy(EmployeeDeployed.address, UtilDeployed.address);
        await PontoBlockDeployed.deployed();
        return {
            PontoBlockDeployed,
            UtilDeployed,
            EmployeeDeployed,
            owner,
            billy,
            john,
            alice, 
            jeff, 
            rachel
        };
    }
    describe("Add a start of work", () => {
        it("should start of work", async () => {
            const { PontoBlockDeployed, 
                    billy, 
                    john, 
                    alice } = await loadFixture(setupFixture);
            const transaction1 = await PontoBlockDeployed.connect(billy).startWork();
            const transaction2 = await PontoBlockDeployed.connect(john).startWork();
            const transaction3 = await PontoBlockDeployed.connect(alice).startWork();
            const regex = /^0x/;
            expect(regex.test(transaction1.hash)).to.true;
            expect(regex.test(transaction2.hash)).to.true;
            expect(regex.test(transaction3.hash)).to.true;
        });
        it("should not start work - Employee not registered", async () => {
            const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(jeff).startWork())
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not start work - Employee is inactive", async () => {
            const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(rachel).startWork())
                .to.rejectedWith("Employee is inactive.");
        });
        it("should not start work - Start of work already registered", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await expect(PontoBlockDeployed.connect(john).startWork())
                .to.rejectedWith("Start of work already registered.");
        });
     });
     describe("Add an end of work", () => {
        it("should end of work", async () => {
            const { PontoBlockDeployed, 
                    billy, 
                    john, 
                    alice } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(billy).startWork();
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(alice).startWork();
            const transaction1 = await PontoBlockDeployed.connect(billy).endWork();
            const transaction2 = await PontoBlockDeployed.connect(john).endWork();
            const transaction3 = await PontoBlockDeployed.connect(alice).endWork();
            const regex = /^0x/;
            expect(regex.test(transaction1.hash)).to.true;
            expect(regex.test(transaction2.hash)).to.true;
            expect(regex.test(transaction3.hash)).to.true;
        });
        it("should not end of work - Employee not registered", async () => {
            const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(jeff).endWork())
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not end work - Employee is inactive", async () => {
            const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(rachel).endWork())
                .to.rejectedWith("Employee is inactive.");
        });
        it("should not end of work - End of work already registered.", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(john).endWork();
            await expect(PontoBlockDeployed.connect(john).endWork())
                .to.rejectedWith("End of work already registered.");
        });
        it("should not end of work - Start of work not registered.", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(john).endWork())
                .to.rejectedWith("Start of work not registered.");
        });
     });
     describe("Add an break start time", () => {
        // it("should break start work", async () => {
        //     const { PontoBlockDeployed, 
        //             billy, 
        //             john, 
        //             alice } = await loadFixture(setupFixture);
        //     const transaction1 = await PontoBlockDeployed.connect(billy).startWork();
        //     const transaction2 = await PontoBlockDeployed.connect(john).startWork();
        //     const transaction3 = await PontoBlockDeployed.connect(alice).startWork();
        //     const regex = /^0x/;
        //     expect(regex.test(transaction1.hash)).to.true;
        //     expect(regex.test(transaction2.hash)).to.true;
        //     expect(regex.test(transaction3.hash)).to.true;
        // });
        // it("should not start work - Employee not registered", async () => {
        //     const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
        //     await expect(PontoBlockDeployed.connect(jeff).startWork())
        //             .to.rejectedWith("Employee not registered.");
        // });
        // it("should not start work - Employee is inactive", async () => {
        //     const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
        //     await expect(PontoBlockDeployed.connect(rachel).startWork())
        //         .to.rejectedWith("Employee is inactive.");
        // });
        // it("should not start work - Start of work already registered", async () => {
        //     const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
        //     await PontoBlockDeployed.connect(john).startWork();
        //     await expect(PontoBlockDeployed.connect(john).startWork())
        //         .to.rejectedWith("Start of work already registered.");
        // });
     });
     describe("Add an break end time", () => {
        // it("should start work", async () => {
        //     const { PontoBlockDeployed, 
        //             billy, 
        //             john, 
        //             alice } = await loadFixture(setupFixture);
        //     const transaction1 = await PontoBlockDeployed.connect(billy).startWork();
        //     const transaction2 = await PontoBlockDeployed.connect(john).startWork();
        //     const transaction3 = await PontoBlockDeployed.connect(alice).startWork();
        //     const regex = /^0x/;
        //     expect(regex.test(transaction1.hash)).to.true;
        //     expect(regex.test(transaction2.hash)).to.true;
        //     expect(regex.test(transaction3.hash)).to.true;
        // });
        // it("should not start work - Employee not registered", async () => {
        //     const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
        //     await expect(PontoBlockDeployed.connect(jeff).startWork())
        //             .to.rejectedWith("Employee not registered.");
        // });
        // it("should not start work - Employee is inactive", async () => {
        //     const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
        //     await expect(PontoBlockDeployed.connect(rachel).startWork())
        //         .to.rejectedWith("Employee is inactive.");
        // });
        // it("should not start work - Start of work already registered", async () => {
        //     const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
        //     await PontoBlockDeployed.connect(john).startWork();
        //     await expect(PontoBlockDeployed.connect(john).startWork())
        //         .to.rejectedWith("Start of work already registered.");
        // });
     });
});