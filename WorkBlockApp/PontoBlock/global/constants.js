export const endPoint = "https://workblock-api.azurewebsites.net/v1/contracts/";

export const pontoBlockAddress = "0x6BdD0030280da0170E9fe8544b18D2134925F3EE";

export const pontoBlockAbi = [
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_emp",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_util",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_adm",
                "type": "address"
            },
            {
                "internalType": "int256",
                "name": "_timeZone",
                "type": "int256"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "constructor"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "address_",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "breakEndtWork_",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "timestamp_",
                "type": "uint256"
            }
        ],
        "name": "BreakEndWorkRegistered",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "address_",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "breakStartWork_",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "timestamp_",
                "type": "uint256"
            }
        ],
        "name": "BreakStartWorkRegistered",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "address_",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "endWork_",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "timestamp_",
                "type": "uint256"
            }
        ],
        "name": "EndWorkRegistered",
        "type": "event"
    },
    {
        "anonymous": false,
        "inputs": [
            {
                "indexed": true,
                "internalType": "address",
                "name": "address_",
                "type": "address"
            },
            {
                "indexed": true,
                "internalType": "uint256",
                "name": "startWork_",
                "type": "uint256"
            },
            {
                "indexed": false,
                "internalType": "uint256",
                "name": "timestamp_",
                "type": "uint256"
            }
        ],
        "name": "StartWorkRegistered",
        "type": "event"
    },
    {
        "inputs": [],
        "name": "breakEndTime",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "breakStartTime",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_newOwner",
                "type": "address"
            }
        ],
        "name": "changeOwner",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "endWork",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getCreationDateContract",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_address",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "_date",
                "type": "uint256"
            }
        ],
        "name": "getEmployeeRecords",
        "outputs": [
            {
                "components": [
                    {
                        "internalType": "uint256",
                        "name": "startWork",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "endWork",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "breakStartTime",
                        "type": "uint256"
                    },
                    {
                        "internalType": "uint256",
                        "name": "breakEndTime",
                        "type": "uint256"
                    }
                ],
                "internalType": "struct PontoBlock.EmployeeRecord",
                "name": "",
                "type": "tuple"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getMoment",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getOwner",
        "outputs": [
            {
                "internalType": "address",
                "name": "",
                "type": "address"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "getTimeZone",
        "outputs": [
            {
                "internalType": "int256",
                "name": "",
                "type": "int256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [],
        "name": "startWork",
        "outputs": [],
        "stateMutability": "nonpayable",
        "type": "function"
    }
];

export const pontoBlockReportsAddress = "0x5A45be5c3D5BE4E286D8999E866C370Da6ec19e2";

export const pontoBlockReportsABI = [
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_emp",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_ponto",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_util",
                "type": "address"
            },
            {
                "internalType": "address",
                "name": "_adm",
                "type": "address"
            }
        ],
        "stateMutability": "nonpayable",
        "type": "constructor"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_employee",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "_startDate",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_endDate",
                "type": "uint256"
            }
        ],
        "name": "getWorkTimeFromEmployeeBetweenTwoDates",
        "outputs": [
            {
                "internalType": "uint256[]",
                "name": "_date",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_startWork",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_endWork",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_breakStartTime",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_breakEndTime",
                "type": "uint256[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_date",
                "type": "uint256"
            }
        ],
        "name": "getWorkTimesForAllEmployeesAtDate",
        "outputs": [
            {
                "internalType": "address[]",
                "name": "_empAddress",
                "type": "address[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_startWork",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_endWork",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_breakStartTime",
                "type": "uint256[]"
            },
            {
                "internalType": "uint256[]",
                "name": "_breakEndTime",
                "type": "uint256[]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_startDate",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_endDate",
                "type": "uint256"
            }
        ],
        "name": "getWorkTimesForAllEmployeesBetweenTwoDates",
        "outputs": [
            {
                "internalType": "uint256[]",
                "name": "_date",
                "type": "uint256[]"
            },
            {
                "internalType": "address[][]",
                "name": "_empAddress",
                "type": "address[][]"
            },
            {
                "internalType": "uint256[][]",
                "name": "_startWork",
                "type": "uint256[][]"
            },
            {
                "internalType": "uint256[][]",
                "name": "_endWork",
                "type": "uint256[][]"
            },
            {
                "internalType": "uint256[][]",
                "name": "_breakStartTime",
                "type": "uint256[][]"
            },
            {
                "internalType": "uint256[][]",
                "name": "_breakEndTime",
                "type": "uint256[][]"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "address",
                "name": "_employee",
                "type": "address"
            },
            {
                "internalType": "uint256",
                "name": "_date",
                "type": "uint256"
            }
        ],
        "name": "getWorkTimesFromEmployeeAtDate",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "_startWork",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_endWork",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_breakStartTime",
                "type": "uint256"
            },
            {
                "internalType": "uint256",
                "name": "_breakEndTime",
                "type": "uint256"
            }
        ],
        "stateMutability": "view",
        "type": "function"
    }
];

export const utilAddress = "0x5C0e0C4100D838A70E0AF4378028bA66AB6aFBEd";

export const utilABI = [
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "timestamp",
                "type": "uint256"
            }
        ],
        "name": "getDate",
        "outputs": [
            {
                "internalType": "uint256",
                "name": "yearMonthDay",
                "type": "uint256"
            }
        ],
        "stateMutability": "pure",
        "type": "function"
    },
    {
        "inputs": [
            {
                "internalType": "uint256",
                "name": "_time",
                "type": "uint256"
            }
        ],
        "name": "validateTime",
        "outputs": [
            {
                "internalType": "bool",
                "name": "",
                "type": "bool"
            }
        ],
        "stateMutability": "pure",
        "type": "function"
    }
];

export const timeZone = -3 * 3600;