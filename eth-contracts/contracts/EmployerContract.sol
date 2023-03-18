// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./AdministratorContract.sol";

contract EmployerContract {

    struct Employer {
        uint256 idEmployer;
        address employerAddress;
        uint256 taxId;
        string name;
        string legalAddress;
    }

    AdministratorContract private admin;
    mapping (uint256 => Employer) private employers;
    address[] private addsEmployers;

    constructor(address _adm) {
        admin = AdministratorContract(_adm);
    }

    function addEmployer(address _address, uint256 _taxId, string memory _name, string memory _legalAddress) public{
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        require(!checkIfEmployerExists(_address), "Employer already exists.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(keccak256(abi.encodePacked(_legalAddress)) != keccak256(abi.encodePacked("")), "Legal address not given.");
        require(_address != address(0), "Address not given.");

        employers[addsEmployers.length] = Employer(addsEmployers.length, _address, _taxId, _name, _legalAddress);
        addsEmployers.push(_address);
    }

    function updateEmployer (address _addressKey, address _address, uint256 _taxId, string memory _name, string memory _legalAddress) public {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        require(_address != address(0), "Address not given.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(keccak256(abi.encodePacked(_legalAddress)) != keccak256(abi.encodePacked("")), "Legal address not given.");
        require(checkIfEmployerExists(_addressKey), "Employer not exists.");

        bool difAdd;
        address add;
        uint256 _id;

        for (uint256 i = 0; i < addsEmployers.length; i++) {
            if (addsEmployers[i] ==  _addressKey) {
                _id = i;
                break;
            }
        }

        if (employers[_id].employerAddress != _address) {
            difAdd = true;
            add = employers[_id].employerAddress;
        }

        employers[_id] = Employer(_id, _address, _taxId, _name, _legalAddress);

        if (difAdd) {
            for (uint256 i = 0; i < addsEmployers.length; i++) {
                if (addsEmployers[i] == add) {
                    addsEmployers[i] = _address;
                    break;
                }
            }
        }
    }

    function getEmployerById(uint256 _id) public view returns(Employer memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        return employers[_id];
    }

    function getEmployerByAddress(address _address) public view returns (Employer memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        Employer memory e;
        for (uint256 i = 0; i < addsEmployers.length; i++) {
            if (addsEmployers[i] == _address) {
                e = employers[i];
                break;
            }
        }
        return e;
    }
    
    function getAllEmployers() public view returns (Employer[] memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        Employer[] memory result = new Employer[](addsEmployers.length);
        for (uint i = 0; i < addsEmployers.length; i++) {
            result[i] = employers[i];
        }
        return result;
    }
    
    function checkIfEmployerExists(address _address) public view returns (bool){
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        for (uint i = 0; i < addsEmployers.length; i++)
            if(addsEmployers[i] == _address)
                return true;

        return false;
    }
}
