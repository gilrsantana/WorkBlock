# WorkBlock

## Presentation
WorkBlock is a time control system that uses Blockchain technology.

Yes, with WorkBlock it is possible to manage the work journey of employees in a secure way, protected against data loss and have accurate reports without the need for dedicated devices for this purpose, as it is possible to access with any computer or smartphone.

## Architecture
WorkBlock is divided into 4 distinct and integrated modules that ensure the decoupling and high cohesion of the application, allowing for planned growth.

### 1- Contract Development Module
In this module, the basis for smart contract development is with smart contracts developed in Solidity with the HardHat development environment. In this environment, unit tests are created, deploy on the Blockchain and storage of smart contract data in an SQL database.


### 2- API Module
This module contains the system API responsible for interacting with the Blockchain and delivering data that is consumed by a Frontend client.
This module is also responsible for performing access validations and registering data modifications in an SQL database for a log query in parallel to the blockchain records.
The technology used to develop this module was .NET, combined with the Entity Framework ORM, along with the Nethereum library.

### 3- Manager Module
This module is responsible for interacting with the API to manage the different profiles for registering system participants and issuing customized reports.
The technology used to develop this module was Razor Pages from the .NET ecosystem. 

### 4- Timekeeping Module
This module is responsible for the employee’s timekeeping. In it, the employee authenticates themselves in the system and is authorized to access the timekeeping record on the Blockchain.
To ensure that there is no interference from third parties in this process, the interaction takes place directly with the timekeeping smart contract, ensuring the integrity and non-participation of third parties that could interfere or tamper with the process. The interaction with the API Module takes place for authentication with the system. 
This module has the premise of being very light so that it can be easily loaded by mobile devices, even in places with difficult internet connection. It was developed directly in HTML, CSS, and Javascript along with the EthersJS library.


