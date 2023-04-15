import { DataSource } from "typeorm";
import * as dotenv from "dotenv";
import { IConnection } from "../interface/IConnection";
import { AppDataSource } from "../database/data-source";


dotenv.config({ path: "./.env" } );

export class SqlConnection implements IConnection {

    getConnection(): DataSource  {     
        return AppDataSource;
    } 
}