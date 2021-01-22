create database MY_DATA;
go
use MY_DATA;
GO
create table Course(
	CourseID int not null,
	Title nvarchar(255) not null,
	Credits nvarchar(255) not null,
	primary key(CourseID)
);
GO
create table Student(
	ID int not null IDENTITY(1,1),
	LastName nvarchar(50) not null,
	FirstMidName nvarchar(50) not null,
	EnrollmentDate date not null,
	primary key(ID)
);
GO
create table Enrollment(
	EnrollmentID int not null IDENTITY(1,1),
	CourseID int not null,
	StudentID int not null,
	Grade int,
	primary key(EnrollmentID),
	foreign key(CourseID) references Course(CourseID),
	foreign key(StudentID) references Student(ID)
);

SELECT * FROM dbo.Enrollment as e INNER JOIN dbo.Student as s on e.StudentID = s.ID

CREATE VIEW [FullInfor] AS
SELECT e.EnrollmentID as ID_enrollment, s.FirstMidName as NameStudent, c.Title as Title_c
FROM (dbo.Enrollment as e Inner join dbo.Student as s on e.StudentID = s.ID) inner join dbo.Course as c on e.CourseID = c.CourseID;

Select * from FullInfor