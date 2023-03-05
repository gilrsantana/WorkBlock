// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./EmployeeContract.sol";
import "./UtilContract.sol";

contract PontoBlock {

    struct EmployeeRecord {
        uint256 startWork;
        uint256 endWork;
        uint256 breakStartTime;
        uint256 breakEndTime;
    }

    int private timeZone;
    int private oneHour = 3600;
    address owner;
    EmployeeContract private employee;
    UtilContract private util;
    uint private creationDate;
    mapping(address => mapping(uint256 => EmployeeRecord)) private employeeRecords;


    constructor(address _emp, address _util, int _timeZone) {
        employee = EmployeeContract(_emp);
        util = UtilContract(_util);
        owner = msg.sender;
        creationDate = util.getDate(getMoment());
        timeZone = _timeZone;
    }

    function startWork() public {
        require(employee.checkIfEmployeeExists(msg.sender), "Employee not registered.");
        require(employee.getEmployeeByAddress(msg.sender).stateOf == EmployeeContract.State.Active, "Employee is inactive.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].startWork == 0, "Start of work already registered.");
        
        employeeRecords[msg.sender][util.getDate(getMoment())].startWork = getMoment();
    }

    function endWork() public {
        require(employee.checkIfEmployeeExists(msg.sender), "Employee not registered.");
        require(employee.getEmployeeByAddress(msg.sender).stateOf == EmployeeContract.State.Active, "Employee is inactive.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].endWork == 0, "End of work already registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].startWork != 0, "Start of work not registered.");

        uint256 one_hour = 3600;
        if (employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime != 0 &&
            employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime == 0)
        {
                if ((getMoment() - employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime)
                        > one_hour)
                {
                    employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime =
                        employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime + one_hour;
                }
                else
                {
                    employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime = getMoment();
                }
        }

        employeeRecords[msg.sender][util.getDate(getMoment())].endWork = getMoment();
    }

    function breakStartTime() public {
        require(employee.checkIfEmployeeExists(msg.sender), "Employee not registered.");
        require(employee.getEmployeeByAddress(msg.sender).stateOf == EmployeeContract.State.Active, "Employee is inactive.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime == 0, "Start of break already registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].startWork != 0, "Start of work not registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].endWork == 0, "End of work already registered.");

        employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime = getMoment();
    }

    function breakEndTime() public {
        require(employee.checkIfEmployeeExists(msg.sender), "Employee not registered.");
        require(employee.getEmployeeByAddress(msg.sender).stateOf == EmployeeContract.State.Active, "Employee is inactive.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime == 0, "End of break already registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime != 0, "Start of break not registered.");

        employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime = getMoment();
    }

    function getCreationDateContract() public view returns(uint) {
        return creationDate;
    }

    function getEmployeeRecords(address _address, uint256 _date) public view returns (EmployeeRecord memory) {
        require(employee.checkIfEmployeeExists(_address), "Employee not registered.");
        return employeeRecords[_address][_date];
    }

    function getMoment() internal view returns(uint256) {
        int adjust = timeZone * oneHour;
        uint256 moment;
        if (adjust < 0) {
            adjust = adjust * -1;
            moment = block.timestamp - uint256(adjust);
        } else {
            moment = block.timestamp + uint256(adjust);
        }
        return moment;
    }
}

