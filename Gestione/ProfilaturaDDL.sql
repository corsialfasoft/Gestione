create database Profilatura
go

use Profilatura
go

create table TipoUtente(
	Id int identity(1,1) not null primary key,
	Tipo varchar(20)
)
go

create table Utente(
	Matricola nvarchar(20) not null primary key,
	Nome varchar(20),
	Cognome varchar(20),
	Users varchar(20),
	Passwd nvarchar(20),
	fkTipo int foreign key references TipoUtente
)
go


INSERT INTO TipoUtente(tipo) VALUES('admin');
INSERT INTO TipoUtente(tipo) VALUES('professore');
INSERT INTO TipoUtente(tipo) VALUES('dipendente');
INSERT INTO TipoUtente(tipo) VALUES('studente');
INSERT INTO Utente(Matricola,Nome,Cognome,Users,Passwd,fkTipo) VALUES('admin','admin','admin','admin','admin',1);
