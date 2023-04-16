import { InsertResult, Repository, UpdateResult } from "typeorm";
import { IContractRepository } from "../interface/IContractRepository";
import { IContractModel } from "../interface/IContractModel";
import { contractModel } from "../models/contractModel";
import { AppDataSource } from "../database/data-source";

export class contractRepository implements IContractRepository {

    private async theRepository(): Promise<Repository<contractModel>> {
        if (AppDataSource.isInitialized) {
            return AppDataSource.getRepository(contractModel);
        } else {
            return (await AppDataSource.initialize()).getRepository(contractModel);
        }
    }

    async getContracts(): Promise<IContractModel[]> {
        const result = (await this.theRepository()).find();
        return result;
    }

    async getContractById(_id: string): Promise<IContractModel | null> {
        const result = (await this.theRepository()).findOne({ where: { id: _id } });
        return result;
    }

    async getContractByName(_name: string): Promise<IContractModel | null> {
        const result = (await this.theRepository()).findOne({ where: { name: _name } });
        return result;
    }

    async addContract(_contract: IContractModel): Promise<InsertResult> {     
        const result = (await this.theRepository()).insert(_contract);
        return result;
    }

    async updateContract(_contract: IContractModel): Promise<UpdateResult> {
        const result = (await this.theRepository())
        .update(_contract.id, {name: _contract.name, 
            addressContract: _contract.addressContract,
            abi: _contract.abi,
            bytecode: _contract.bytecode,
            createdAt: _contract.createdAt} );

        return result;
    }
    

}