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
    });
    
});