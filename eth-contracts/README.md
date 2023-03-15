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

### Runing tests

- Add test script alias in package.json
```
"scripts": {
    "block": "npx hardhat node",
    "test": "npx hardhat test && npx hardhat watch test",
    "test:coverage": "npx hardhat coverage"
  },
```

- npm run block => Run a blockchain local node

- npm run test => Run the tests written in the test folder and keep watching the tests

- npm run test:coverage => Run the coverage of tests in application

### Deploy contract

- To deploy:
```
npx hardhat run scripts/deploy.ts --network mumbai
```

### Verify contract

- To Verify contract:
```
npx hardhat verify --network mumbai <contract address> <optional constructor parameter1> <optional constructor parameter2> <optional constructor parameter3> ...
```

## Solidity 2 UML

- Solidity 2 UML is a plugin that creates a diagram from your contracts
- For more instructions visit the repository of project: https://github.com/naddison36/sol2uml

### Install

- npm link sol2uml --only=production

### Generating Class Diagram

- At CLI: `sol2uml class ./contracts`
