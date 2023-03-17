import * as dotenv from "dotenv";
dotenv.config( { path: "./.env" } );
import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-toolbox";
import 'solidity-coverage';
import 'hardhat-watcher';
import "@nomiclabs/hardhat-etherscan";
import { network } from "hardhat";

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
  defaultNetwork: "hardhat",
  networks: {
    hardhat: {
      chainId: 1337,
    },
    ganache: {
      url: "http://127.0.0.1:7545",
      accounts: ["08ec0a0b340dbb9b30787a3686dd086dcc9e34c255f7bf46fb0712d79616c120"],
      // chainId: 57788
    },
    hardhat_node: {
      url: "http://127.0.0.1:8545",
      accounts: ["ac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80"],
      // chainId: 57788
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
      files: ["./test/**/*.ts"],
      verbose: true
    }
  }
};

export default config;
