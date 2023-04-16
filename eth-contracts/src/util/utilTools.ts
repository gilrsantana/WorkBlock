import { AppDataSource } from "../database/data-source";
import { IContractModel } from "../interface/IContractModel";
import { IContractService } from "../interface/IContractService";
import { v4 as uuid } from "uuid";
import { contractModel } from "../models/contractModel";

export class utilTools {

    static async handlerService(service: IContractService, model: IContractModel) {
        const result = await service.getContractByName(model.name);

        if (result === null || result === undefined) {
            model.id = uuid();
            if (await service.insertContract(model))
                console.log(model.name + " successfull insert")
            else
                console.log(model.name + " not successfull insert")
        } else {
            model.id = result.id;
            if (await service.updateContract(model))
                console.log(model.name + " successfull updated")
            else
                console.log(model.name + " not successfull updated")
        }
        AppDataSource.destroy();
    }

    static buildContract(path: string, address: string): IContractModel {

        const fs = require('fs');
        const data = fs.readFileSync(path, { encoding: 'utf8' });
        const parsedData = JSON.parse(data);
        const nameString = JSON.stringify(parsedData.contractName).replace('"', '').replace('"', '').toString();
        const abiString = JSON.stringify(parsedData.abi).toString();
        const bytecodeString = JSON.stringify(parsedData.bytecode).replace('"', '').replace('"', '').toString();
    
        const contract: contractModel = {
            id: "",
            name: nameString,
            addressContract: address,
            abi: abiString,
            bytecode: bytecodeString,
            createdAt: new Date()
        };
    
        return contract;
    }

}


