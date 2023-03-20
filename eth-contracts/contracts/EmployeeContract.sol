// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./AdministratorContract.sol";
import "./EmployerContract.sol";

contract EmployeeContract {

    struct Employee {
        uint256 idEmployee;
        address employeeAddress;
        uint256 taxId;
        string name;
        State stateOf;
        address employerAddress;
    }

    enum State { Inactive, Active }
    mapping (uint256 => Employee) private employees;
    address[] private addsEmployees;

    AdministratorContract private admin;
    EmployerContract private employer;

    constructor(address _adm, address _employer) {
        admin = AdministratorContract(_adm);
        employer = EmployerContract(_employer);
    }

    function addEmployee(address _address, 
                        string memory _name, 
                        uint256 _taxId, 
                        address _employerAddress) 
                        public{
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        require(employer.checkIfEmployerExists(_employerAddress), "Employer not exists.");
        require(!checkIfEmployeeExists(_address), "Employee already exists.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(_address != address(0), "Address not given.");
        require(_employerAddress != address(0), "Employer address not given.");

        employees[addsEmployees.length] = Employee(addsEmployees.length, 
                                                   _address, 
                                                   _taxId, 
                                                   _name, 
                                                   State.Active, 
                                                   _employerAddress);
        addsEmployees.push(_address);
    }

    function updateEmployee (address _addressKey, 
                             address _address, 
                             uint256 _taxId, 
                             string memory _name, 
                             State _state, 
                             address _employerAddress) 
                             public {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        require(employer.checkIfEmployerExists(_employerAddress), "Employer not exists.");
        require(_address != address(0), "Address not given.");
        require(_taxId != 0, "TaxId not given.");
        require(keccak256(abi.encodePacked(_name)) != keccak256(abi.encodePacked("")), "Name not given.");
        require(checkIfEmployeeExists(_addressKey), "Employee not exists.");
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
    }

    function getEmployeeById(uint256 _id) public view returns(Employee memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        return employees[_id];
    }

    function getEmployeeByAddress(address _address) public view returns (Employee memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        Employee memory e;
        for (uint256 i = 0; i < addsEmployees.length; i++) {
            if (addsEmployees[i] == _address) {
                e = employees[i];
                break;
            }
        }
        return e;
    }
    
    function getAllEmployees() public view returns (Employee[] memory) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        Employee[] memory result = new Employee[](addsEmployees.length);
        for (uint i = 0; i < addsEmployees.length; i++) {
            result[i] = employees[i];
        }
        return result;
    }
    
    function checkIfEmployeeExists(address _address) public view returns (bool){
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        for (uint i = 0; i < addsEmployees.length; i++)
            if(addsEmployees[i] == _address)
                return true;

        return false;
    }

    function getEmployerContract() public view returns (address _empContract) {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        return address(employer);
    }
}
