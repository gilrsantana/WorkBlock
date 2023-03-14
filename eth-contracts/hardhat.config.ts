import * as dotenv from "dotenv";
dotenv.config( { path: "./.env" } );
import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-toolbox";
import 'solidity-coverage';
import 'hardhat-watcher';
import "@nomiclabs/hardhat-etherscan";

const config: HardhatUserConfig = {
  solidity: {
    version: "0.8.17",
    settings: {
      optimizer: {
        enabled: true,
        runs: 200
      },
    },
  },
  networks: {
    hardhat: {
      chainId: 1337
    },
    mumbai: {
      url: process.env.ALCHEMY_URL || "",
      accounts: process.env.PRIVATE_KEY !== undefined ? [process.env.PRIVATE_KEY] : []
    }
  },
  etherscan: {
    apiKey: process.env.POLYGON_API_KEY || "",
  },
  watcher: {
    compilation: {
      tasks: ['compile']
    },
    test: {
      tasks: [ { command: "test", params: { testFiles: ["{path}"] } } ],
      files: ["./test/**/*"],
      verbose: true
    }
  }
};

export default config;
