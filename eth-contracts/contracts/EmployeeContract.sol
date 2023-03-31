// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./AdministratorContract.sol";
import "./EmployerContract.sol";
import "./UtilContract.sol";

contract EmployeeContract {

    AdministratorContract private admin;
    EmployerContract private employer;
    UtilContract private util;
    mapping (uint256 => Employee) private employees;
    address[] private addsEmployees;

    struct Employee {
        uint256 idEmployee;
        address employeeAddress;
        uint256 taxId;
        string name;
        uint256 begginingWorkDay;
        uint256 endWorkDay;
        State stateOf;
        address employerAddress;
    }

    enum State { Inactive, Active }
    
    event EmployeeAdded(address indexed from_, 
                        address indexed address_, 
                        string name_, 
                        uint256 taxId_, 
                        uint256 begginingWorkDay_,
                        uint256 endWorkDay_,
                        address employerAddress_,
                        uint256 timestamp_);

    event EmployeeUpdated(address indexed from_, 
                          address indexed oldAddress_, 
                          address indexed newAddress_, 
                          uint256 begginingWorkDay_,
                          uint256 endWorkDay_,
                          address employerAddress_,
                          State state_,
                          uint256 timestamp_);

    constructor(address _adm, address _employer, address _util) {
        admin = AdministratorContract(_adm);
        employer = EmployerContract(_employer);
        util = UtilContract(_util);
    }

    modifier onlyAdmin() {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        _;
    }

    modifier employeeNotAddedYet(address _address) {
        require(!checkIfEmployeeExists(_address), "Employee already exists.");
        _;
    }

    modifier employeeAddedYet(address _address) {
        require(checkIfEmployeeExists(_address), "Employee not exists.");
        _;
    }

    modifier employerAddedYet(address _address) {
        require(employer.checkIfEmployerExists(_address), "Employer not exists.");
        _;
    }

    modifier validWorkday(uint256 _beggining, uint256 _end) {
        require(util.validateTime(_beggining), "Not valid beggining work day.");
        require(util.validateTime(_end), "Not valid end work day.");
        require(_beggining < _end, "Beggining Work Day must be less than End Work Day.");
        _;
    }

    function addEmployee (address _address, 
                          string memory _name, 
                          uint256 _taxId, 
                          uint256 _begginingWorkDay, 
                          uint256 _endWorkDay, 
                          address _employerAddress) 
             public 
             onlyAdmin() 
             employerAddedYet(_employerAddress)
             employeeNotAddedYet(_address) 
             validWorkday(_begginingWorkDay, _endWorkDay) {

        require(_address != address(0), "Address not given.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(_employerAddress != address(0), "Employer address not given.");

        employees[addsEmployees.length] = Employee(addsEmployees.length, 
                                                   _address, 
                                                   _taxId, 
                                                   _name, 
                                                   _begginingWorkDay,
                                                   _endWorkDay,
                                                   State.Active, 
                                                   _employerAddress);
        addsEmployees.push(_address);

        emit EmployeeAdded(msg.sender, _address, _name, _taxId, _begginingWorkDay, _endWorkDay, _employerAddress, block.timestamp);
    }

    function updateEmployee (address _addressKey, 
                             address _address, 
                             uint256 _taxId, 
                             string memory _name, 
                             uint256 _begginingWorkDay, 
                             uint256 _endWorkDay, 
                             State _state, 
                             address _employerAddress) 
             public 
             onlyAdmin() 
             employerAddedYet(_employerAddress)
             employeeAddedYet(_addressKey) 
             validWorkday(_begginingWorkDay, _endWorkDay) {

        require(_address != address(0), "Address not given.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(_employerAddress != address(0), "Employer address not given.");

        bool difAdd;
        address add;
        uint256 _id;

        for (uint256 i = 0; i < addsEmployees.length; i++) {
            if (addsEmployees[i] ==  _addressKey) {
                _id = i;
                break;
            }
        }

        if (employees[_id].employeeAddress != _address) {
            difAdd = true;
            add = employees[_id].employeeAddress;
        }

        employees[_id] = Employee(_id, 
                                  _address, 
                                  _taxId, 
                                  _name, 
                                  _begginingWorkDay,
                                  _endWorkDay,
                                  _state, 
                                  _employerAddress);

        if (difAdd) {
            for (uint256 i = 0; i < addsEmployees.length; i++) {
                if (addsEmployees[i] == add) {
                    addsEmployees[i] = _address;
                    break;
                }
            }
        }

         emit EmployeeUpdated(msg.sender, _addressKey, _address, _begginingWorkDay, _endWorkDay, _employerAddress, _state,  block.timestamp);
    }

    function getEmployeeById
             (uint256 _id) 
             public view 
             onlyAdmin()
             returns(Employee memory) {

        return employees[_id];
    }

    function getEmployeeByAddress
             (address _address) 
             public view 
             onlyAdmin()
             returns (Employee memory) {

        Employee memory e;
        for (uint256 i = 0; i < addsEmployees.length; i++) {
            if (addsEmployees[i] == _address) {
                e = employees[i];
                break;
            }
        }
        return e;
    }
    
    function getAllEmployees() 
             public view 
             onlyAdmin()
             returns (Employee[] memory) {

        Employee[] memory result = new Employee[](addsEmployees.length);
        for (uint i = 0; i < addsEmployees.length; i++) {
            result[i] = employees[i];
        }
        return result;
    }
    
    function checkIfEmployeeExists
             (address _address) 
             public view 
             onlyAdmin()
             returns (bool){

        for (uint i = 0; i < addsEmployees.length; i++)
            if(addsEmployees[i] == _address)
                return true;

        return false;
    }

    function getEmployerContract() 
             public view 
             onlyAdmin()
             returns (address _empContract) {

        return address(employer);
    }
}
