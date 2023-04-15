import { InsertResult, UpdateResult } from "typeorm";
import { IContractModel } from "./IContractModel";

export interface IContractRepository {
    getContracts(): Promise<IContractModel[]>;
    getContractById(_id: string): Promise<IContractModel | null>;
    getContractByName(_name: string): Promise<IContractModel | null>;
    addContract(_contract: IContractModel): Promise<InsertResult>;
    updateContract(_contract: IContractModel): Promise<UpdateResult>;
}