
CREATE DATABASE binarycity;
use binarycity;

CREATE TABLE `clients` (
  `ClientCode` varchar(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Deleted` tinyint(1) NOT NULL DEFAULT '0',
  `DateOfRecord` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ClientCode`),
  UNIQUE KEY `IX_Client_code` (`ClientCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `contacts` (
  `ContactId` int AUTO_INCREMENT  NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Surname` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Deleted` tinyint(1) NOT NULL DEFAULT '0',
  `DateOfRecord` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ContactId`),
  UNIQUE KEY `IX_Contact_id` (`ContactId`)
) AUTO_INCREMENT = 100 ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `clientcontacts` (
  `ClientCode` varchar(10) NOT NULL,
  `ContactId` int NOT NULL,
  `Deleted` tinyint(1) NOT NULL DEFAULT '0',
  `DateOfRecord` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ClientCode`,`ContactId`),
  UNIQUE KEY `IX_ClientContact_code_id` (`ClientCode`,`ContactId`),
  KEY `FK_ClientContacts_Id` (`ContactId`),
  CONSTRAINT `FK_ClientContacts_Code` FOREIGN KEY (`ClientCode`) REFERENCES `clients` (`ClientCode`),
  CONSTRAINT `FK_ClientContacts_Id` FOREIGN KEY (`ContactId`) REFERENCES `contacts` (`ContactId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO Clients (clientCode, surname, deleted, dateofRecord, dateModified) VALUES ('FNB001','FNB', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Clients (clientCode, surname, deleted, dateofRecord, dateModified) VALUES ('PRO123','Protea', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Clients (clientCode, surname, deleted, dateofRecord, dateModified) VALUES ('ITA001','IT', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Clients (clientCode, surname, deleted, dateofRecord, dateModified) VALUES ('MTN001','MTN', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Clients (clientCode, surname, deleted, dateofRecord, dateModified) VALUES ('MCD001','Mcdonald', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO Contacts (name, surname, email, deleted, dateofRecord, dateModified) VALUES ('Nathi', 'TheDev', 'spantshwa.lukho@gmail.com', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Contacts (name, surname, email, deleted, dateofRecord, dateModified) VALUES ('AB', 'Consulting', 'admin@abconsulting.co.za', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO Contacts (name, surname, email, deleted, dateofRecord, dateModified) VALUES ('Mthatha', 'Waves', 'mthatha@dmax.co.za', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO clientcontacts (clientCode, ContactId, deleted, dateofRecord, dateModified) VALUES ('FNB001', 100, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO clientcontacts (clientCode, ContactId, deleted, dateofRecord, dateModified) VALUES ('PRO123', 100, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO clientcontacts (clientCode, ContactId, deleted, dateofRecord, dateModified) VALUES ('FNB001', 101, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO clientcontacts (clientCode, ContactId, deleted, dateofRecord, dateModified) VALUES ('FNB001', 102, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
