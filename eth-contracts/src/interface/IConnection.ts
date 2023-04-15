import { DataSource } from "typeorm";

export interface IConnection {
    getConnection(): DataSource;
}