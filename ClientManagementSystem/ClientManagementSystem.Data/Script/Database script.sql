Create Database BinaryCity;
Use BinaryCity;

--drop table ClientContacts
--drop table Clients
--drop table Contacts


CREATE TABLE Clients(
  ClientCode varchar(10) Not Null,
  Name varchar(50) Not Null,
  Deleted bit DEFAULT 0 Not Null,
  DateOfRecord datetime DEFAULT GETDATE() Not Null,
  DateModified datetime DEFAULT GETDATE() Not Null

  constraint PK_Client primary key clustered
  (
    ClientCode
  )
)
GO

CREATE TABLE Contacts(
  ContactId int Not Null identity(100,1),
  Name varchar(50) Not Null,
  Surname varchar(50) Not Null,
  Email varchar(50) Not Null,
  Deleted bit DEFAULT 0 Not Null,
  DateOfRecord datetime DEFAULT GETDATE() Not Null,
  DateModified datetime DEFAULT GETDATE() Not Null

  constraint PK_Contact primary key clustered
  (
    ContactId
  )
)
GO

CREATE TABLE ClientContacts(
  ClientCode varchar(10) Not Null,
  ContactId int Not Null,
  Deleted bit DEFAULT 0 Not Null,
  DateOfRecord datetime DEFAULT GETDATE() Not Null,
  DateModified datetime DEFAULT GETDATE() Not Null

  constraint FK_ClientContacts_Code foreign key (ClientCode) references Clients(ClientCode),
  constraint FK_ClientContacts_Id foreign key (ContactId) references Contacts(ContactId),
  constraint PK_ClientContacts primary key clustered
  (
    ClientCode,
	ContactId
  )
)
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Client_code ON Clients(ClientCode)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Contact_id ON Contacts(ContactId)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_ClientContact_code_id ON ClientContacts(ClientCode, ContactId)
GO

INSERT INTO Clients VALUES ('FNB001','FNB', 0, GETDATE(), GETDATE())
INSERT INTO Clients VALUES ('PRO123','Protea', 0, GETDATE(), GETDATE())
INSERT INTO Clients VALUES ('ITA001','IT', 0, GETDATE(), GETDATE())
INSERT INTO Clients VALUES ('MTN001','MTN', 0, GETDATE(), GETDATE())
INSERT INTO Clients VALUES ('MCD001','Mcdonald', 0, GETDATE(), GETDATE())

INSERT INTO Contacts VALUES ('Nathi', 'TheDev', 'spantshwa.lukho@gmail.com', 0, GETDATE(), GETDATE())
INSERT INTO Contacts VALUES ('AB', 'Consulting', 'admin@abconsulting.co.za', 0, GETDATE(), GETDATE())
INSERT INTO Contacts VALUES ('Mthatha', 'Waves', 'mthatha@dmax.co.za', 0, GETDATE(), GETDATE())

INSERT INTO ClientContacts VALUES ('FNB001', 100, 0, GETDATE(), GETDATE())
INSERT INTO ClientContacts VALUES ('PRO123', 100, 0, GETDATE(), GETDATE())
INSERT INTO ClientContacts VALUES ('FNB001', 101, 0, GETDATE(), GETDATE())
INSERT INTO ClientContacts VALUES ('FNB001', 102, 0, GETDATE(), GETDATE())

Select * From Clients
Select * From Contacts
Select * From ClientContacts
