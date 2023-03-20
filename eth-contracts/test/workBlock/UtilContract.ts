import { loadFixture } from '@nomicfoundation/hardhat-network-helpers';
import { expect } from 'chai';
import { ethers } from 'hardhat';

describe('UtilContract', () => {
    async function setupFixture() {
        const [owner, billy, john, alice] = await ethers.getSigners();
        const UtilContract = await ethers.getContractFactory('UtilContract');
        const UtilDeployed = await UtilContract.deploy();
        await UtilDeployed.deployed();
        return {
            UtilDeployed,
            owner,
            billy,
            john,
            alice
        };
    }
    describe("Returned value", () => {
        it("should return a formated date", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const timeStamp = 1677071503;
            const returnedValue = await UtilDeployed.getDate(timeStamp);
            expect(returnedValue).to.equal(20230222);
        });
        it("should reject zero value", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const timeStamp = 0;
            await expect(UtilDeployed.getDate(timeStamp)).to.rejectedWith(
                    "Timestamp should be more than zero."
                );
        });
        it("should reject negative value", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const timeStamp = -1;
            await expect(UtilDeployed.getDate(timeStamp)).to.rejectedWith();
        });
        it("should return a formated date - leap year", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const timeStamp = 1078060303;
            const returnedValue = await UtilDeployed.getDate(timeStamp);
            expect(returnedValue).to.equal(20040229);
        });
        it("should return a formated date - leap year", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const timeStamp = 951829903;
            const returnedValue = await UtilDeployed.getDate(timeStamp);
            expect(returnedValue).to.equal(20000229);
        });
    });
    describe("Time validation ", () => {
        it("should return true - 00h 00m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 0;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return true - 23h 59m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2359;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return true - 00h 01m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 1;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return true - 23h 00m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2300;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return true - 00h 59m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 59;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return true - 17h 17m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 1717;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(true);
        });
        it("should return false - 00h 60m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 60;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 00h 99m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 99;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 24h 00m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2400;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 24h 01m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2401;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 24h 59m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2459;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 24h 60m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 2460;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 99h 99m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 9999;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
        it("should return false - 9999999h 99m", async () => {
            const { UtilDeployed } = await loadFixture(setupFixture);
            const time = 999999999;
             const returnedValue = await UtilDeployed.validateTime(time);
            expect(returnedValue).to.equal(false);
        });
    });
});