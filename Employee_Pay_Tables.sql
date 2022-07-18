create database EmployeePayroll;

create table Employee(
EmployeeId int identity(1,1) primary key,
UserName varchar(50) NOT NULL,
Email varchar(50) NOT NULL,
Password varchar(50) NOT NULL,
MobileNo varchar(12) NOT NULL,
ProfileImage varchar(50) NOT NULL,
Gender varchar(4) NOT NULL,
Department varchar(50) NOT NULL,
startDate date,
Note varchar(50) NOT NULL
);

create table Salary(
SalaryId int identity(1,1) primary key,
EmployeeId int not null,
SalaryDate date,
Amount float,
PaySlip date
);

alter table [Salary] add constraint Salary_EmployeeId_FK
foreign key (EmployeeId) references [Employee](EmployeeId)

create table Address(
AddressId int identity(1,1) primary key,
EmployeeId int not null,
Address varchar(100) NOT NULL,
City varchar(50) NOT NULL,
State varchar(50) NOT NULL
);

alter table [Address] add constraint Address_EmployeeId_FK
foreign key (EmployeeId) references [Employee](EmployeeId)

create table Payout(
PayoutId int identity(1,1) primary key,
SalaryId int not null,
CTC float,
PF float,
TAX float
);

alter table [Salary] add constraint Payout_SalaryId_FK
foreign key (SalaryId) references [Salary](SalaryId)

--SP to store employee details
create procedure sp_EmpRegister
(  
	@UserName varchar(50),
	@Email varchar(50),
	@Password varchar(50),
	@MobileNo varchar(12),
	@ProfileImage varchar(50),
	@Gender varchar(4),
	@Department varchar(50),
	@startDate date,
	@Note varchar(50)
)   
as 
begin    
    Insert into Employee (UserName,Email,Password,MobileNo,ProfileImage,Gender,Department,startDate,Note)    
	Values (@UserName,@Email, @Password,@MobileNo,@ProfileImage,@Gender,@Department,@startDate,@Note)    
end

select * from Employee