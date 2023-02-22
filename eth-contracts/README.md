# WORKBLOCK PROJECT

## Hardhat
- To initialize the project
```
npx hardhat init
```
- set: TypeScript project, add gitignore, current path as root project and add nomic dependencies

- Install other features
```
npm i dotenv hardhat-watcher solidity-coverage
```

### Configuring hardhat

- import watcher and coverage
```
import 'solidity-coverage';
import 'hardhat-watcher';
```

- configure solidity
```
solidity: {
    version: "0.8.17",
    settings: {
      optimizer: {
        enabled: true,
        runs: 200
      }
    }
  },
```

- configure networks
```
networks: {
    hardhat: {
      chainId: 1337
    }
  },
```

- configure watcher
```
watcher: {
    compilation: {
      tasks: ['compile']
    },
    test: {
      tasks: [ { command: "test", params: { testFiles: ["{path}"] } } ],
      files: [".test/**/*"],
      verbose: true
    }
  }
```

### Hardhat tests

- run tests once
```
npx hardhat test
```

- keep tests running
```
npx hardhat test && npx hardhat watch test
```

### Hardhat node blockchain

- Run the hardhat node blockchain
```
npx hardhat node
``` 



This project demonstrates a basic Hardhat use case. It comes with a sample contract, a test for that contract, and a script that deploys that contract.

Try running some of the following tasks:

```shell
npx hardhat help
npx hardhat test
REPORT_GAS=true npx hardhat test
npx hardhat node
npx hardhat run scripts/deploy.ts
```
