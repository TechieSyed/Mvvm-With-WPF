
create database MvvmDemoDb
go

use MvvmDemoDb
go

create table Employees
(
	Id int not null,
	Name varchar(30) not null,
	Age int not null,
	constraint pk_Employees primary key(Id)
)
go

create procedure udp_SelectAllEmployees
as
select * from Employees
go

create procedure udp_SelectEmployeeById
(
	@Id int
)
as
 select * from Employees where Id=@Id
go

create procedure udp_InsertEmployee
(
	@Id int,
	@Name varchar(30),
	@Age int
)
as
 insert into Employees values(@Id, @Name, @Age)
go

create procedure udp_UpdateEmployee
(
	@Id int,
	@Name varchar(30),
	@Age int
)
as
 update Employees set Name=@Name, Age=@Age where Id=@Id
go

create procedure udp_DeleteEmployee
(
	@Id int
)
as
 Delete from Employees where Id=@Id
go

 
 exec udp_InsertEmployee 101,'Jojo',25