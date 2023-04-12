import * as dotenv from "dotenv";
dotenv.config({ path: "./.env" } );
import mysql from 'mysql';

export function myConnection() {
    const host: any = process.env.HOST || '';
    const user: any = process.env.USER_DB || '';
    const password: any = process.env.PASSWORD || '';
    const database: any = process.env.DATABASE || '';

    const connection = mysql.createConnection({
                            host: host,
                            user: user,
                            password: password,
                            database: database });
    
    return connection;
}