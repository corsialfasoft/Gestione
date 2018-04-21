CREATE DATABASE GECV;
USE GECV;
CREATE TABLE Curriculum (
	IdCv int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nome nvarchar(50),
	Cognome nvarchar(50),
	Eta int,
	Matricola nvarchar(10)not null,
	Email nvarchar(30),
	Residenza nvarchar(100),
	Telefono nvarchar(10)
);
CREATE TABLE PercorsoStudi (
	IdPs int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AnnoI int,
	AnnoF int,
	Titolo nvarchar(50),
	Descrizione nvarchar(200),
	IdCv int FOREIGN KEY REFERENCES Curriculum not null
);
CREATE TABLE EspLav (
	IdEl int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AnnoI int,
	AnnoF int,
	Qualifica nvarchar(50),
	Descrizione nvarchar(200),
	IdCv int FOREIGN KEY REFERENCES Curriculum not null
);
CREATE TABLE Competenze (
	IdCs int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Tipo nvarchar(50),
	Livello int,	
	IdCv int FOREIGN KEY REFERENCES Curriculum not null
);
