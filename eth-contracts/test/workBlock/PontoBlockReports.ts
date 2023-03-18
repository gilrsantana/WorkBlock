import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('PontoBlockReports', () => {
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
        const PontoBlockDeployed = await PontoBlock.deploy(EmployeeDeployed.address, 
                                                           UtilDeployed.address,
                                                           AdministratorDeployed.address,
                                                           -3);
        await PontoBlockDeployed.deployed();
        await AdministratorDeployed.addAdministrator(PontoBlockDeployed.address,
                                                    "PontoBlock",
                                                    9999999999);
        await PontoBlockDeployed.connect(billy).startWork();
        await PontoBlockDeployed.connect(billy).breakStartTime();
        await PontoBlockDeployed.connect(billy).breakEndTime();
        await PontoBlockDeployed.connect(billy).endWork();
        await PontoBlockDeployed.connect(john).startWork();
        await PontoBlockDeployed.connect(john).breakStartTime();
        await PontoBlockDeployed.connect(john).breakEndTime();
        await PontoBlockDeployed.connect(john).endWork();
        await PontoBlockDeployed.connect(alice).startWork();
        await PontoBlockDeployed.connect(alice).breakStartTime();
        await PontoBlockDeployed.connect(alice).breakEndTime();
        await PontoBlockDeployed.connect(alice).endWork();
        const PontoBlockReportsContract = await ethers.getContractFactory('PontoBlockReports');
        const ReportsDeployed = await PontoBlockReportsContract.deploy(EmployeeDeployed.address,
                                                                       PontoBlockDeployed.address,
                                                                       UtilDeployed.address,
                                                                       AdministratorDeployed.address);
        await ReportsDeployed.deployed();

        await AdministratorDeployed.addAdministrator(ReportsDeployed.address,
                                                    "PontoBlockReports",
                                                    9999999999);
        return {
            ReportsDeployed,
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
    describe("Work times from employee at date", () => {
        it("should returns work times", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = await UtilDeployed.getDate((await block).timestamp);
            
            const record = await ReportsDeployed.getWorkTimesFromEmployeeAtDate(alice.address, myDate);
            expect(record._startWork.toNumber() > 0).to.true;
            expect(record._endWork.toNumber() > 0).to.true;
            expect(record._breakStartTime.toNumber() > 0).to.true;
            expect(record._breakEndTime.toNumber() > 0).to.true;
        });
        it("should returns work times - Sender is the employee searched", async () => {
            const { ReportsDeployed, 
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = await UtilDeployed.getDate((await block).timestamp);
            
            const record = await ReportsDeployed.connect(alice).getWorkTimesFromEmployeeAtDate(alice.address, myDate);
            expect(record._startWork.toNumber() > 0).to.true;
            expect(record._endWork.toNumber() > 0).to.true;
            expect(record._breakStartTime.toNumber() > 0).to.true;
            expect(record._breakEndTime.toNumber() > 0).to.true;
        });
        it("should not returns work times - Employee not registered", async () => {
            const { ReportsDeployed, UtilDeployed,jeff } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = await UtilDeployed.getDate((await block).timestamp);
            await expect(ReportsDeployed.getWorkTimesFromEmployeeAtDate(jeff.address, myDate))
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not returns work times - Sender is not administrator", async () => {
            const { ReportsDeployed, 
                    UtilDeployed,
                    jeff,
                    rachel } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = await UtilDeployed.getDate((await block).timestamp);
            await expect(ReportsDeployed.connect(rachel)
                        .getWorkTimesFromEmployeeAtDate(jeff.address, myDate))
                    .to.rejectedWith("Sender is not authorized.");
        });
        it("should not returns work times - Sender is not the employee searched", async () => {
            const { ReportsDeployed, 
                    UtilDeployed,
                    jeff,
                    rachel } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = await UtilDeployed.getDate((await block).timestamp);
            await expect(ReportsDeployed.connect(rachel)
                        .getWorkTimesFromEmployeeAtDate(jeff.address, myDate))
                    .to.rejectedWith("Sender is not authorized.");
        });
    });
    describe("Work times from employee between two dates", () => {
        it("should not returns work times between two dates - Sender is not administrator", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    jeff,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.connect(alice).
                        getWorkTimeFromEmployeeBetweenTwoDates(jeff.address, myDate1, myDate2))
                        .to.rejectedWith("Sender is not authorized.");
        });
        it("should not returns work times between two dates - Sender is not the employee searched", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    jeff,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.connect(alice)
                        .getWorkTimeFromEmployeeBetweenTwoDates(jeff.address, myDate1, myDate2))
                        .to.rejectedWith("Sender is not authorized.");
        });
        it("should not returns work times between two dates - Employee not registered", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    jeff } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimeFromEmployeeBetweenTwoDates(jeff.address, myDate1, myDate2))
                    .to.rejectedWith("Employee not registered.");
        });
        it("should not returns work times between two dates - Start date must be less than end date", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimeFromEmployeeBetweenTwoDates(alice.address, myDate2, myDate1))
                    .to.rejectedWith("Start date must be less than end date.");
        });
        it("should not returns work times between two dates - Start date must be equals or grather than creationDate", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimeFromEmployeeBetweenTwoDates(alice.address, myDate1, myDate2))
                    .to.rejectedWith("Start date must be equals or grather than creationDate.");
        });
        it("should not returns work times between two dates - End date must be equals or less than today", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate1 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp + 86400;
            const myDate2 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimeFromEmployeeBetweenTwoDates(alice.address, myDate1, myDate2))
                    .to.rejectedWith("End date must be equals or less than today.");
        });
    });
    describe("Work times for all employees at date", () => {
        it("should returns work times", async () => {
            const { ReportsDeployed, 
                    UtilDeployed,
                    billy,
                    john,
                    alice,
                    rachel } = await loadFixture(setupFixture);
     
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            const record = await ReportsDeployed.getWorkTimesForAllEmployeesAtDate(myDate);
            expect (record._empAddress.length === 4).to.true;
            expect (record._empAddress[0] === billy.address).to.true;
            expect (record._empAddress[1] === john.address).to.true;
            expect (record._empAddress[2] === alice.address).to.true;
            expect (record._empAddress[3] === rachel.address).to.true;
        });
        it("should not returns work times - Sender is not administrator", async () => {
            const { ReportsDeployed, 
                    UtilDeployed,
                    billy } = await loadFixture(setupFixture);
     
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp);
            await expect(ReportsDeployed.connect(billy)
                    .getWorkTimesForAllEmployeesAtDate(myDate))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not returns work times - Start date must be equals or grather than creationDate", async () => {
            const { ReportsDeployed, 
                    UtilDeployed } = await loadFixture(setupFixture);
     
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate = UtilDeployed.getDate((await block).timestamp - 86400);
            await expect(ReportsDeployed.getWorkTimesForAllEmployeesAtDate(myDate))
                        .to.rejectedWith("Start date must be equals or grather than creationDate.");

        });
    });
    describe("Work times from all employee between two dates", () => {
        it("should not returns work times between two dates - Sender is not administrator", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.connect(alice)
                    .getWorkTimesForAllEmployeesBetweenTwoDates(myDate2, myDate1))
                    .to.rejectedWith("Sender is not administrator.");
        });
        it("should not returns work times between two dates - Start date must be less than end date", async () => {
            const { ReportsDeployed,
                    UtilDeployed } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimesForAllEmployeesBetweenTwoDates(myDate2, myDate1))
                    .to.rejectedWith("Start date must be less than end date.");
        });
        it("should not returns work times between two dates - Start date must be equals or grather than creationDate", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate2 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp - 86400;
            const myDate1 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimesForAllEmployeesBetweenTwoDates(myDate1, myDate2))
                    .to.rejectedWith("Start date must be equals or grather than creationDate.");
        });
        it("should not returns work times between two dates - End date must be equals or less than today", async () => {
            const { ReportsDeployed,
                    UtilDeployed,
                    alice } = await loadFixture(setupFixture);
            const blkNumber = ethers.provider.getBlockNumber();
            const block = ethers.provider.getBlock(blkNumber);
            const myDate1 = await UtilDeployed.getDate((await block).timestamp);
            const value2 = await (await block).timestamp + 86400;
            const myDate2 = await UtilDeployed.getDate(value2);

            await expect(ReportsDeployed.getWorkTimesForAllEmployeesBetweenTwoDates(myDate1, myDate2))
                    .to.rejectedWith("End date must be equals or less than today.");
        });
    });
});