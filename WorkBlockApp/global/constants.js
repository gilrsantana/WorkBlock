export const contractAddres = "0x3086762324120AA600B9203F5e78510B4d8e3378";

export const contractAbi = [
  {
    inputs: [
      { internalType: "address", name: "_emp", type: "address" },
      { internalType: "address", name: "_util", type: "address" },
      { internalType: "address", name: "_adm", type: "address" },
      { internalType: "int256", name: "_timeZone", type: "int256" },
    ],
    stateMutability: "nonpayable",
    type: "constructor",
  },
  {
    anonymous: false,
    inputs: [
      {
        indexed: true,
        internalType: "address",
        name: "address_",
        type: "address",
      },
      {
        indexed: true,
        internalType: "uint256",
        name: "breakEndtWork_",
        type: "uint256",
      },
      {
        indexed: false,
        internalType: "uint256",
        name: "timestamp_",
        type: "uint256",
      },
    ],
    name: "BreakEndWorkRegistered",
    type: "event",
  },
  {
    anonymous: false,
    inputs: [
      {
        indexed: true,
        internalType: "address",
        name: "address_",
        type: "address",
      },
      {
        indexed: true,
        internalType: "uint256",
        name: "breakStartWork_",
        type: "uint256",
      },
      {
        indexed: false,
        internalType: "uint256",
        name: "timestamp_",
        type: "uint256",
      },
    ],
    name: "BreakStartWorkRegistered",
    type: "event",
  },
  {
    anonymous: false,
    inputs: [
      {
        indexed: true,
        internalType: "address",
        name: "address_",
        type: "address",
      },
      {
        indexed: true,
        internalType: "uint256",
        name: "endWork_",
        type: "uint256",
      },
      {
        indexed: false,
        internalType: "uint256",
        name: "timestamp_",
        type: "uint256",
      },
    ],
    name: "EndWorkRegistered",
    type: "event",
  },
  {
    anonymous: false,
    inputs: [
      {
        indexed: true,
        internalType: "address",
        name: "address_",
        type: "address",
      },
      {
        indexed: true,
        internalType: "uint256",
        name: "startWork_",
        type: "uint256",
      },
      {
        indexed: false,
        internalType: "uint256",
        name: "timestamp_",
        type: "uint256",
      },
    ],
    name: "StartWorkRegistered",
    type: "event",
  },
  {
    inputs: [],
    name: "breakEndTime",
    outputs: [],
    stateMutability: "nonpayable",
    type: "function",
  },
  {
    inputs: [],
    name: "breakStartTime",
    outputs: [],
    stateMutability: "nonpayable",
    type: "function",
  },
  {
    inputs: [{ internalType: "address", name: "_newOwner", type: "address" }],
    name: "changeOwner",
    outputs: [],
    stateMutability: "nonpayable",
    type: "function",
  },
  {
    inputs: [],
    name: "endWork",
    outputs: [],
    stateMutability: "nonpayable",
    type: "function",
  },
  {
    inputs: [],
    name: "getCreationDateContract",
    outputs: [{ internalType: "uint256", name: "", type: "uint256" }],
    stateMutability: "view",
    type: "function",
  },
  {
    inputs: [
      { internalType: "address", name: "_address", type: "address" },
      { internalType: "uint256", name: "_date", type: "uint256" },
    ],
    name: "getEmployeeRecords",
    outputs: [
      {
        components: [
          { internalType: "uint256", name: "startWork", type: "uint256" },
          { internalType: "uint256", name: "endWork", type: "uint256" },
          { internalType: "uint256", name: "breakStartTime", type: "uint256" },
          { internalType: "uint256", name: "breakEndTime", type: "uint256" },
        ],
        internalType: "struct PontoBlock.EmployeeRecord",
        name: "",
        type: "tuple",
      },
    ],
    stateMutability: "view",
    type: "function",
  },
  {
    inputs: [],
    name: "getMoment",
    outputs: [{ internalType: "uint256", name: "", type: "uint256" }],
    stateMutability: "view",
    type: "function",
  },
  {
    inputs: [],
    name: "getOwner",
    outputs: [{ internalType: "address", name: "", type: "address" }],
    stateMutability: "view",
    type: "function",
  },
  {
    inputs: [],
    name: "getTimeZone",
    outputs: [{ internalType: "int256", name: "", type: "int256" }],
    stateMutability: "view",
    type: "function",
  },
  {
    inputs: [],
    name: "startWork",
    outputs: [],
    stateMutability: "nonpayable",
    type: "function",
  },
];