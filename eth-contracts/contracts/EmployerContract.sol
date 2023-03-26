// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./AdministratorContract.sol";

contract EmployerContract {

    AdministratorContract private admin;
    mapping (uint256 => Employer) private employers;
    address[] private addsEmployers;

    struct Employer {
        uint256 idEmployer;
        address employerAddress;
        uint256 taxId;
        string name;
        string legalAddress;
    }

    event EmployerAdded(address indexed from_, 
                        address indexed address_, 
                        string name_, 
                        uint256 taxId_, 
                        string legalAddress_,
                        uint256 timestamp_);

    event EmployerUpdated(address indexed from_, 
                          address indexed oldAddress_, 
                          address indexed newAddress_, 
                          string name_, 
                          uint256 taxId_, 
                          string legaAddress_,
                          uint256 timestamp_);

    constructor(address _adm) {
        admin = AdministratorContract(_adm);
    }

    modifier onlyAdmin {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        _;
    }

    modifier employerNotAddedYet(address _address) {
        require(!checkIfEmployerExists(_address), "Employer already exists.");
        _;
    }

    modifier employerAddedYet(address _address) {
        require(checkIfEmployerExists(_address), "Employer not exists.");
        _;
    }

    function addEmployer (address _address, 
                          uint256 _taxId, 
                          string memory _name, 
                          string memory _legalAddress) 
             public 
             onlyAdmin()
             employerNotAddedYet(_address) {

        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(keccak256(abi.encodePacked(_legalAddress)) != keccak256(abi.encodePacked("")), "Legal address not given.");
        require(_address != address(0), "Address not given.");

        employers[addsEmployers.length] = Employer(addsEmployers.length, _address, _taxId, _name, _legalAddress);
        addsEmployers.push(_address);

        emit EmployerAdded(msg.sender, _address, _name, _taxId, _legalAddress, block.timestamp);
    }

    function updateEmployer (address _addressKey, 
                             address _address, 
                             uint256 _taxId, 
                             string memory _name, 
                             string memory _legalAddress) 
             public 
             onlyAdmin()
             employerAddedYet(_addressKey) {

        require(_address != address(0), "Address not given.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(keccak256(abi.encodePacked(_legalAddress)) != keccak256(abi.encodePacked("")), "Legal address not given.");

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

        emit EmployerUpdated(msg.sender, _addressKey, _address, _name, _taxId, _legalAddress, block.timestamp);
    }

    function getEmployerById (uint256 _id) 
             public view 
             onlyAdmin()
             returns (Employer memory) {

        return employers[_id];
    }

    function getEmployerByAddress (address _address) 
             public view 
             onlyAdmin()
             returns (Employer memory) {
        
        Employer memory e;
        for (uint256 i = 0; i < addsEmployers.length; i++) {
            if (addsEmployers[i] == _address) {
                e = employers[i];
                break;
            }
        }
        return e;
    }
    
    function getAllEmployers() 
             public view 
             onlyAdmin()
             returns (Employer[] memory) {

        Employer[] memory result = new Employer[](addsEmployers.length);
        for (uint i = 0; i < addsEmployers.length; i++) {
            result[i] = employers[i];
        }
        return result;
    }
    
    function checkIfEmployerExists (address _address) 
             public view 
             onlyAdmin()
             returns (bool){

        for (uint i = 0; i < addsEmployers.length; i++)
            if(addsEmployers[i] == _address)
                return true;

        return false;
    }
}
