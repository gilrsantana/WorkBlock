import { DataSource } from "typeorm";
import * as dotenv from "dotenv";
import { contractModel }  from "../models/contractModel";
import { CreateContractsTable1681567569268 } from "./migrations/1681567569268-CreateContractsTable";
dotenv.config({ path: "./.env" } );

export const AppDataSource = new DataSource({
    type: "mysql",
    host: process.env.TYPEORM_HOST || '',
    port: process.env.TYPEORM_PORT !== undefined ? parseInt(process.env.TYPEORM_PORT) : 0,
    username: process.env.TYPEORM_USERNAME || '',
    password: process.env.TYPEORM_PASSWORD || '',
    database: process.env.TYPEORM_DATABASE || '',
    synchronize: true,
    logging: true,
    entities: [contractModel],
    migrations: [CreateContractsTable1681567569268],
    subscribers: []
})