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
create table Funzioni(
	id int identity(1,1) not null primary key,
	sistema nvarchar(50) not null,
	descrizione nvarchar(100) not null
);
create table TipoUtenteFunzioni(
	idtipoUtente int foreign key references TipoUtente not null,
	idFunzione int foreign key references Funzioni not null
);
INSERT INTO TipoUtente(tipo) VALUES('admin');
INSERT INTO TipoUtente(tipo) VALUES('professore');
INSERT INTO TipoUtente(tipo) VALUES('dipendente');
INSERT INTO TipoUtente(tipo) VALUES('studente');
INSERT INTO TipoUtente(tipo) VALUES('sconosciuto');
INSERT INTO Utente(Matricola,Nome,Cognome,Users,Passwd,fkTipo) VALUES('admin','admin','admin','admin','admin',1);
INSERT INTO Utente(Matricola,Nome,Cognome,Users,Passwd,fkTipo) VALUES('default','default','default','default','default',5);
INSERT INTO Funzioni(sistema,descrizione) VALUES ('GeTime','compila il giorno'),('GeTime','visualizza commessa'),('GeTime','visualizza giorno'),('GeTime','visualizza main page getime'),('GeCo','aggiugi corso'),('GeCo','aggiugi lezione'),('GeCo','modifica lezione'),('GeCo','visualizza elenco corsi'),('GeCo','visualizza i tuoi corsi'),('GeCo','iscriviti ad un corso'),('GeCo','ricerca corso'),('GeCv','visualizza main page gecv'),('GeCv','aggiungi cv'),('GeCv','modifica cv'),('GeCv','elimina cv'),('GeCv','ricerca cv'),('GeCv','carica cv');
INSERT INTO TipoUtenteFunzioni(idtipoUtente,idFunzione) values(1,,(1,),(1,),(1,),(1,),(1,),(1,),(1,)
INSERT INTO TipoUtenteFunzioni(idtipoUtente,idFunzione) values(2,1),(2,2),(2,3),(2,4),(2,6),(2,7),(2,8),(2,9),(2,11),(2,12),(2,14),(2,16);
INSERT INTO TipoUtenteFunzioni(idtipoUtente,idFunzione) values(3,1),(3,2),(3,3),(3,4),(3,8),(3,9),(3,10),(3,11),(3,12),(3,14),(3,16);
insert into TipoUtenteFunzioni(idtipoUtente,idFunzione) values (4,8),(4,9),(4,10),(4,11),(4,12),(4,13),(4,15),(4,17);
insert into TipoUtenteFunzioni(idtipoUtente,idFunzione) values (5,4),(5,8),(5,11),(5,12),(5,17);

