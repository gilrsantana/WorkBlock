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
        const PontoBlockDeployed = await PontoBlock.deploy(EmployeeDeployed.address, UtilDeployed.address, -3);
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
                    UtilDeployed,
                    john } = await loadFixture(setupFixture);
            PontoBlockDeployed.connect(john).startWork();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await (await PontoBlockDeployed.connect(john)
                                        .getEmployeeRecords(john.address, myDate))
                                        .startWork;
            expect(record.toNumber() > 0).to.true;
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
                    UtilDeployed,
                    billy } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(billy).startWork();
            await PontoBlockDeployed.connect(billy).endWork();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await (await PontoBlockDeployed.connect(billy).getEmployeeRecords(billy.address, myDate)).endWork;
            expect(record.toNumber() > 0).to.true;
        });
        it("should end of work and break end time", async () => {
            const { PontoBlockDeployed, UtilDeployed, billy } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(billy).startWork();
            await PontoBlockDeployed.connect(billy).breakStartTime();
            await PontoBlockDeployed.connect(billy).endWork();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await (await PontoBlockDeployed.connect(billy).getEmployeeRecords(billy.address, myDate)).breakEndTime;
            await expect(record.toNumber() > 0).to.true;
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
        it("should break start time", async () => {
            const { PontoBlockDeployed, 
                    UtilDeployed,
                    billy } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(billy).startWork();
            await PontoBlockDeployed.connect(billy).breakStartTime();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await (await PontoBlockDeployed.connect(billy).getEmployeeRecords(billy.address, myDate)).breakStartTime;
            await expect(record.toNumber() > 0).to.true;
        });
        it("should not break start time - Employee not registered", async () => {
            const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(jeff).breakStartTime())
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not break start time - Employee is inactive", async () => {
            const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(rachel).breakStartTime())
                .to.rejectedWith("Employee is inactive.");
        });
        it("should not break start time - Start of break already registered", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(john).breakStartTime();
            await expect(PontoBlockDeployed.connect(john).breakStartTime())
                .to.rejectedWith("Start of break already registered.");
        });
        it("should not break start time - End of work already registered", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(john).endWork();
            await expect(PontoBlockDeployed.connect(john).breakStartTime())
                .to.rejectedWith("End of work already registered.");
        });
     });
     describe("Add an break end time", () => {
        it("should break end time", async () => {
            const { PontoBlockDeployed, 
                    UtilDeployed,
                    john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(john).breakStartTime();
            await PontoBlockDeployed.connect(john).breakEndTime();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await (await PontoBlockDeployed.connect(john).getEmployeeRecords(john.address, myDate)).breakEndTime;
            await expect(record.toNumber() > 0).to.true;
        });
        it("should not break end time - Employee not registered", async () => {
            const { PontoBlockDeployed, jeff } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(jeff).breakEndTime())
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not break end time - Employee is inactive", async () => {
            const { PontoBlockDeployed, rachel } = await loadFixture(setupFixture);
            await expect(PontoBlockDeployed.connect(rachel).breakEndTime())
                .to.rejectedWith("Employee is inactive.");
        });
        it("should not break end time - End of break already registered", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await PontoBlockDeployed.connect(john).breakStartTime();
            await PontoBlockDeployed.connect(john).breakEndTime();
            await expect(PontoBlockDeployed.connect(john).breakEndTime())
                .to.rejectedWith("End of break already registered.");
        });
        it("should not break end time - Start of break not registered", async () => {
            const { PontoBlockDeployed, john } = await loadFixture(setupFixture);
            await PontoBlockDeployed.connect(john).startWork();
            await expect(PontoBlockDeployed.connect(john).breakEndTime())
                .to.rejectedWith("Start of break not registered.");
        });
     });
    describe("get a contract date", () => {
        it("should get a contract date", async () => {
            const { PontoBlockDeployed, 
                    UtilDeployed } = await loadFixture(setupFixture);
            const contractDate = await PontoBlockDeployed.getCreationDateContract();
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate =await UtilDeployed.getDate((await block).timestamp);
            await expect(myDate.toNumber() === contractDate.toNumber()).to.true;
        });
    });

});