create database EmployeePayroll;
use EmployeePayroll;

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

create table Attendance(
AttendanceId int identity(1,1) primary key,
EmployeeId int not null,
PresentDay int,
AbsentDay int,
DailySalary float
);

alter table [Attendance] add constraint Attendance_EmployeeId_FK
foreign key (EmployeeId) references [Employee](EmployeeId)

create table Salary(
SalaryId int identity(1,1) primary key,
EmployeeId int not null,
Amount float,
SalaryDate date,
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

--SP to get all employee details
CREATE PROC spGetAllEmp
AS
BEGIN 
	SELECT * FROM Employee
END

--SP to get specific employee detail
CREATE PROC spGetSpecificEmpDetail
	@EmployeeId INT
AS
BEGIN 
	SELECT * FROM [Employee]
	WHERE EmployeeId = @EmployeeId
END

--SP to get specific employee detail
CREATE PROC spGetSpecificEmpDetailByEmail
	@Email varchar(50)
AS
BEGIN 
	SELECT * FROM [Employee]
	WHERE Email = @Email
END
--SP to update employee details
CREATE PROC spUpdateEmployeeDetails
    @EmployeeId int,
    @UserName varchar(50),
	@Email varchar(50),
	@Password varchar(50),
	@MobileNo varchar(12),
	@ProfileImage varchar(50),
	@Gender varchar(4),
	@Department varchar(50),
	@startDate date,
	@Note varchar(50),
	@count INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM [Employee] WHERE EmployeeId = @EmployeeId )
	BEGIN
		SET @count = @EmployeeId
		UPDATE [Employee]
		SET
			UserName = CASE WHEN @UserName='' THEN UserName ELSE @UserName END,
			Email = CASE WHEN @Email='' THEN Email ELSE @Email END, 
			Password= CASE WHEN @Password='' THEN Password ELSE @Password END, 
			MobileNo = CASE WHEN @MobileNo='' THEN MobileNo ELSE @MobileNo END,
			ProfileImage =CASE WHEN @ProfileImage='' THEN ProfileImage ELSE @ProfileImage END,
			Gender =CASE WHEN @Gender='' THEN Gender ELSE Gender END,
			Department =CASE WHEN @Department='' THEN Department ELSE @Department END,
			startDate =CASE WHEN @startDate='' THEN startDate ELSE @startDate END,
			Note =CASE WHEN @Note='' THEN Note ELSE @Note END
			WHERE
			EmployeeId = @EmployeeId;
	END
	ELSE
	BEGIN
		SET @count =NULL;
	END
END
--SP for employee login
CREATE PROC spUserLogin
    @EmployeeId int,
	@Email VARCHAR(50),
	@Password VARCHAR(50),
	@user INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM Employee WHERE Email=@Email)
	BEGIN 
		IF EXISTS(SELECT * FROM Employee WHERE Email=@Email AND Password=@Password AND EmployeeId=@EmployeeId)
		BEGIN
			SET @user = 2;
		END
		ELSE
		BEGIN
			SET @user = 1;
		END
	END
	ELSE
	BEGIN
		SET @user = NULL;
	END
END

--2nd SP for employee login
CREATE PROC spEmployeeLogin
	@Email VARCHAR(50),
	@Password VARCHAR(50),
	@user INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM Employee WHERE Email=@Email)
	BEGIN 
		IF EXISTS(SELECT * FROM Employee WHERE Email=@Email AND Password=@Password)
		BEGIN
			SET @user = 2;
		END
		ELSE
		BEGIN
			SET @user = 1;
		END
	END
	ELSE
	BEGIN
		SET @user = NULL;
	END
END

-- Employee Login
create procedure login (    
    @Email VARCHAR(50),
	@Password VARCHAR(50)
)   
as   
Begin   
    select * from Employee WHERE Email = @Email and Password = @Password
End

--SP to store employee details
create procedure sp_AddEmpAddress
(  
	@EmployeeId int,
	@Address varchar(100),
	@City varchar(50),
	@State varchar(50)
)   
as 
begin    
    Insert into Address (EmployeeId,Address,City,State)    
	Values (@EmployeeId,@Address, @City,@State)    
end

select * from Address

--SP to get specific employee address
CREATE PROC spGetSpecificEmpAddress
	@EmployeeId int
AS
BEGIN 
	SELECT * FROM [Address]
	WHERE EmployeeId = @EmployeeId
END

--SP to update employee address details
CREATE PROC spUpdateEmployeeAddress
    @EmployeeId int,
	@Address varchar(100),
	@City varchar(50),
	@State varchar(50),
	@count INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM [Address] WHERE EmployeeId = @EmployeeId )
	BEGIN
		SET @count = @EmployeeId
		UPDATE [Address]
		SET
			Address = CASE WHEN @Address='' THEN Address ELSE @Address END,
			City = CASE WHEN @City='' THEN City ELSE @City END, 
			State= CASE WHEN @State='' THEN State ELSE @State END
			WHERE
			EmployeeId = @EmployeeId;
	END
	ELSE
	BEGIN
		SET @count =NULL;
	END
END

--*****************************************************************************************
--SP to store employee Salary details
create procedure sp_AddEmpSalary
(  
	@EmployeeId int,
	@SalaryDate datetime,
	@Amount float,
	@PaySlip datetime
)   
as 
begin    
    Insert into Salary(EmployeeId,SalaryDate,Amount,PaySlip)    
	Values (@EmployeeId,@SalaryDate, @Amount,@PaySlip)    
end

select * from Salary

--SP to get specific employee  Salary
CREATE PROC spGetSpecificEmpSalaryDetail
	@EmployeeId int
AS
BEGIN 
	SELECT * FROM [Salary]
	WHERE EmployeeId = @EmployeeId
END

--SP to update employee Salary details
CREATE PROC spUpdateEmployeeSalary
    @EmployeeId int,
	@SalaryDate datetime,
	@Amount float,
	@PaySlip datetime,
	@count INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM [Address] WHERE EmployeeId = @EmployeeId )
	BEGIN
		SET @count = @EmployeeId
		UPDATE [Salary]
		SET
			SalaryDate = CASE WHEN @SalaryDate='' THEN SalaryDate ELSE @SalaryDate END,
			Amount = CASE WHEN @Amount='' THEN Amount ELSE @Amount END, 
			PaySlip= CASE WHEN @PaySlip='' THEN PaySlip ELSE @PaySlip END
			WHERE
			EmployeeId = @EmployeeId;
	END
	ELSE
	BEGIN
		SET @count =NULL;
	END
END

--***************************************************************************
--SP FOR PAYOUT
CREATE PROC spGetSalaryAmt
	@SalaryId int
AS
BEGIN 
	SELECT Amount FROM [Salary]
	WHERE SalaryId = @SalaryId
END

--SP to store employee Payout details
create procedure sp_AddEmpPayout
(  
	@SalaryId int,
	@CTC float,
	@PF float,
	@TAX float
)   
as 
begin    
    Insert into Payout(SalaryId,CTC,PF,TAX)    
	Values (@SalaryId,@CTC, @PF,@TAX)    
end

select * from Salary

--SP to get specific employee  Payout
CREATE PROC spGetSpecificEmpPayoutDetail
	@SalaryId int
AS
BEGIN 
	SELECT * FROM [Payout]
	WHERE SalaryId = @SalaryId
END

--SP to update employee Salary details
CREATE PROC spUpdateEmployeePayout
    @SalaryId int,
	@CTC float,
	@PF float,
	@TAX float,
	@count INT = NULL OUTPUT
AS
BEGIN
	IF EXISTS(SELECT * FROM [Salary] WHERE SalaryId = @SalaryId )
	BEGIN
		SET @count = @SalaryId
		UPDATE [Payout]
		SET
			SalaryId = CASE WHEN @SalaryId='' THEN SalaryId ELSE @SalaryId END,
			CTC = CASE WHEN @CTC='' THEN CTC ELSE @CTC END, 
			PF = CASE WHEN @PF='' THEN PF ELSE @PF END,
			TAX = CASE WHEN @TAX='' THEN TAX ELSE @TAX END
			WHERE
			SalaryId = @SalaryId;
	END
	ELSE
	BEGIN
		SET @count =NULL;
	END
END


	select * from Payout

--	AttendanceId int identity(1,1) primary key,
--EmployeeId int not null,
--PresentDay int,
--AbsentDay int,
--DailySalary float

---------------------------------------------------------
--SP to take attendence

create procedure sp_EmpAttendance
(  
	@EmployeeId int,
	@PresentDay int,
	@AbsentDay int,
	@DailySalary float
)   
as 
begin    
    Insert into Attendance(EmployeeId,PresentDay,AbsentDay,DailySalary)    
	Values (@EmployeeId,@PresentDay, @AbsentDay,@DailySalary)    
end

select PresentDay,DailySalary from Attendance where EmployeeId = 1

--SP FOR geting present day
CREATE PROC spGetPresentday
	@EmployeeId int
AS
BEGIN 
	SELECT PresentDay,DailySalary FROM [Attendance]
	WHERE EmployeeId = @EmployeeId
END

select * from Salary