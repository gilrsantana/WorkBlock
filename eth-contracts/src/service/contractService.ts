import { IContractService } from "../interface/IContractService";
import { IContractRepository } from "../interface/IContractRepository";
import { IContractModel } from "../interface/IContractModel";
import { contractRepository } from "../repositories/contractRepository";

export class contractService implements IContractService {
    
    repository: IContractRepository;

    constructor(_model: IContractModel) {
        this.repository = new contractRepository();
    }
    async getContractById(_id: string): Promise<IContractModel | null> {
        const result = await this.repository.getContractById(_id);
        return result;
    }
    getContractByAddress(_address: string): Promise<IContractModel | null> {
        throw new Error("Method not implemented.");
    }
    async getContractByName(_name: string): Promise<IContractModel | null> {
        const result = await this.repository.getContractByName(_name);
        return result;
    }
    getContractAbi(_id: string): Promise<string> {
        throw new Error("Method not implemented.");
    }
    getContractBytecode(_id: string): Promise<string> {
        throw new Error("Method not implemented.");
    }
    async insertContract(_model: IContractModel): Promise<boolean> {
        const result = await this.repository.addContract(_model);
        if (result.identifiers.length == 1)
            return true;
        else
            return false;
    }
    async updateContract(_model: IContractModel): Promise<boolean> {
        const result = await this.repository.updateContract(_model);
        if (result.affected)
            return true;
        else
            return false;
    }
    
    getContracts(): Promise<IContractModel[]> {
        return this.repository.getContracts();
    }
}
    
    
   
    
    
    // async getContractId(_contractModel: contractModel): Promise<string> {
    //     const contracts = await contractRepository.getContracts();
    //     if (contracts.length > 0) {
    //         contracts.forEach(c => {
    //             if (c.id != _contractModel.id)
    //             {
    //                 console.log(c);
    //                 console.log(c.id, c.name, c.addressContract, c.abi, c.bytecode);
    //                 return c.id;
    //             }
    //         });
    //     }
    //     return "abc123";
    // }

    // async getContractAddress(contractModel: contractModel): Promise<string | Error> {
    //     throw new Error("Method not implemented.");
    // }

    // async getContractName(contractModel: contractModel): Promise<string | Error> {
    //     throw new Error("Method not implemented.");
    // }

    // async getContractAbi(contractModel: contractModel): Promise<string | Error> {
    //     throw new Error("Method not implemented.");
    // }

    // async getContractBytecode(contractModel: contractModel): Promise<string | Error> {
    //     throw new Error("Method not implemented.");
    // }

    // async insertContract(_contractModel: contractModel): Promise<boolean | Error> {

    // }

    // async updateContract(contractModel: contractModel): Promise<boolean | Error> {
    //     console.log("metodo update")
    //     // throw new Error("Method not implemented.");
    //     return true;
    // }

