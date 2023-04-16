import { contractModel } from "../models/contractModel";
import { IContractModel } from "./IContractModel";

export interface IContractService {

    getContracts(): Promise<IContractModel[]>;
    getContractById(_id: string): Promise<IContractModel | null>;
    getContractByAddress(_address: string):Promise<IContractModel | null>;
    getContractByName(_name: string): Promise<IContractModel | null>;
    getContractAbi(_id: string): Promise<string>;
    getContractBytecode(_id: string): Promise<string>;
    insertContract(_model: IContractModel): Promise<boolean>;
    updateContract(_model: IContractModel): Promise<boolean>; 
}