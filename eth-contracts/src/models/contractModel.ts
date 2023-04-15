import { Entity, Column, CreateDateColumn, PrimaryColumn } from "typeorm";
import { v4 as uuid } from "uuid";
import { IContractModel } from "../interface/IContractModel";

@Entity({name: "Contracts"}) 
export class contractModel implements IContractModel {
	@PrimaryColumn({ type: "varchar", length: "100" })
	id: string 

    @Column({ type: "varchar", length: "100", unique: true, nullable: false })
    name: string 

    @Column({ type: "varchar", length: "100", nullable: false })
    addressContract: string 

    @Column({ type: "text" })
    abi: string 

    @Column({ type: "text" })
    bytecode: string 

    @CreateDateColumn({ type: "datetime" })
    createdAt: Date

    constructor() {
        if (!this.id)
            this.id = uuid();
    }
}
