// SPDX-License-Identifier: MIT
pragma solidity >=0.8.17;

import "./EmployeeContract.sol";
import "./UtilContract.sol";
import "./AdministratorContract.sol";

contract PontoBlock {
    
    EmployeeContract private employee;
    UtilContract private util;
    AdministratorContract private admin;

    int private timeZone;
    int private oneHour = 3600;
    address private owner;
    uint private creationDate;
    mapping(address => mapping(uint256 => EmployeeRecord)) private employeeRecords;

    struct EmployeeRecord {
        uint256 startWork;
        uint256 endWork;
        uint256 breakStartTime;
        uint256 breakEndTime;
    }

    event StartWorkRegistered(address indexed address_, uint256 indexed startWork_, uint256 timestamp_);
    event EndWorkRegistered(address indexed address_, uint256 indexed endWork_, uint256 timestamp_);
    event BreakStartWorkRegistered(address indexed address_, uint256 indexed breakStartWork_, uint256 timestamp_);
    event BreakEndWorkRegistered(address indexed address_, uint256 indexed breakEndtWork_, uint256 timestamp_);

    constructor(address _emp, address _util, address _adm, int _timeZone) {
        employee = EmployeeContract(_emp);
        util = UtilContract(_util);
        admin = AdministratorContract(_adm);
        owner = msg.sender;
        creationDate = util.getDate(getMoment());
        timeZone = _timeZone;
    }

    modifier onlyOwner() {
        require(msg.sender == owner, "Only the owner can call this function.");
        _;
    }

    modifier onlyAdmin() {
        require(admin.checkIfAdministratorExists(msg.sender), "Sender is not administrator.");
        _;
    }

    modifier employeeAddedYet(address _address) {
        require(employee.checkIfEmployeeExists(_address), "Employee not registered.");
        _;
    }

    modifier activeEmployee() {
        require(employee.getEmployeeByAddress(msg.sender).stateOf == EmployeeContract.State.Active, "Employee is inactive.");
        _;
    }

    function startWork() 
        public 
        employeeAddedYet(msg.sender) 
        activeEmployee() {
        
        require(employeeRecords[msg.sender][util.getDate(getMoment())].startWork == 0, "Start of work already registered.");
        employeeRecords[msg.sender][util.getDate(getMoment())].startWork = getMoment();

        emit StartWorkRegistered(msg.sender, employeeRecords[msg.sender][util.getDate(getMoment())].startWork, block.timestamp);
    }

    function endWork() 
        public
        employeeAddedYet(msg.sender) 
        activeEmployee() {

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

        emit EndWorkRegistered(msg.sender, employeeRecords[msg.sender][util.getDate(getMoment())].endWork, block.timestamp);
    }

    function breakStartTime() 
        public
        employeeAddedYet(msg.sender) 
        activeEmployee() {

        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime == 0, "Start of break already registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].startWork != 0, "Start of work not registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].endWork == 0, "End of work already registered.");

        employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime = getMoment();

        emit BreakStartWorkRegistered(msg.sender, employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime, block.timestamp);
    }

    function breakEndTime() 
        public
        employeeAddedYet(msg.sender) 
        activeEmployee() {

        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime == 0, "End of break already registered.");
        require(employeeRecords[msg.sender][util.getDate(getMoment())].breakStartTime != 0, "Start of break not registered.");

        employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime = getMoment();

        emit BreakEndWorkRegistered(msg.sender, employeeRecords[msg.sender][util.getDate(getMoment())].breakEndTime, block.timestamp);
    }

    function getCreationDateContract() 
        public view 
        onlyAdmin()
        returns(uint) {

        return creationDate;
    }

    function getEmployeeRecords (address _address, uint256 _date) 
        public view 
        onlyAdmin()
        employeeAddedYet(_address)
        returns (EmployeeRecord memory) {

        return employeeRecords[_address][_date];
    }

    function getMoment() 
        public view 
        returns(uint256) {

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

    function changeOwner(address _newOwner) 
        public 
        onlyOwner() {
        owner = _newOwner;
    }

    function getOwner() 
        public view 
        onlyAdmin()
        returns (address) {

        return owner;
    }

    function getTimeZone() 
        public view 
        returns (int) {

        return timeZone;
    }

}

