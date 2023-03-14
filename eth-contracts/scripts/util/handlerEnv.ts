import * as dotenv from "dotenv";
dotenv.config({ path: "./.env" } );
import * as fs from "fs";
import * as os from "os";


export function handler(key: any, value: any) {
    const encoding: any = process.env.ENCODING || '';
    const path: any = process.env.DOT_ENV_PATH || '';
    const ENV_VARS = fs.readFileSync(path, 'utf8').split(os.EOL);
    var line = -1;

    ENV_VARS.forEach(i => {
        if (i.match(new RegExp(key))) {
            line = ENV_VARS.indexOf(i);
        }
    });

    if (line !== -1) {
        ENV_VARS.splice(line, 1, `${key}=${value}`);
        fs.writeFileSync("./.env", ENV_VARS.join(os.EOL));
        console.log("\nKey " + key + " updated at environment file.\n")
    } else {
        fs.appendFile(path, '\n'+key+'='+value,
            { encoding: encoding, mode: 0o666, flag: "a" },
            (err) => {
                if (err) {
                    console.log(err);
                }
                else {
                    console.log("\nAddress of contract recorded at environment file.\n");
                }
            });
    }

}

