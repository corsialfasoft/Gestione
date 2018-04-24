create database Profilatura
go

use Profilatura
go
CREATE TABLE sistemi (
    codice INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    nome VARCHAR (20) NOT NULL,
);
CREATE TABLE funzioni (
    codice INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    sistema INT NULL FOREIGN KEY REFERENCES sistemi,
    nome VARCHAR (20) NOT NULL,
	descrizione nvarchar(100) not null
);
CREATE TABLE ruoli (
    codice INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    nome VARCHAR (20) NOT NULL
);
CREATE TABLE funzioniAssociate(
    codiceRuolo INT NOT NULL  FOREIGN KEY REFERENCES ruoli,
    codiceFunzione INT NOT NULL FOREIGN KEY REFERENCES funzioni,
	PRIMARY KEY(codiceRuolo,codiceFunzione)
);
CREATE TABLE utenti (
    matricola INT IDENTITY (1, 1) NOT NULL PRIMARY KEY ,
    nome VARCHAR (50) NULL,
    cognome VARCHAR (50) NULL,
    username VARCHAR (20) NULL,
    passwd VARCHAR (20) NULL,
    ruolo INT NULL FOREIGN KEY REFERENCES ruoli,
);

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

set implicit_transactions on
begin try
	INSERT INTO ruoli(nome) VALUES('admin');
	declare @idR int = (select IDENT_CURRENT('ruoli'))
	INSERT INTO ruoli(nome) VALUES('professore');
	INSERT INTO ruoli(nome) VALUES('dipendente');
	INSERT INTO ruoli(nome) VALUES('studente');
	INSERT INTO ruoli(nome) VALUES('sconosciuto');
	INSERT INTO utenti(Matricola,nome,cognome,username,passwd,ruolo) VALUES('admin','admin','admin','admin','admin',@idR);
	INSERT INTO utenti(Matricola,nome,cognome,username,passwd,ruolo) VALUES('default','default','default','default','default',@idR+5);
	INSERT INTO sistemi(nome) Values ('GeTime');
	declare @idS int = (select IDENT_CURRENT('sistemi'));
	INSERT INTO sistemi(nome) Values ('GeCo');
	INSERT INTO sistemi(nome) Values ('GeCv');
	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS,'AddGiorno','compila il giorno');
	declare @idFGeTime int = (select IDENT_CURRENT('funzioni'));
	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS,'VisualizzaCommessa','visualizza commessa'),(@idS,'VisualizzaMese','visualizza tutto il mese'),(@idS,'VisualizzaGiorno','visualizza giorno'),(@idS,'GTMainPage','visualizza main page getime');

	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS+1,'AddCorso','aggiugi corso');
	declare @idFGeCorso int = (select IDENT_CURRENT('funzioni'));
	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS+1,'AddLezione','aggiugi lezione'),(@idS+1,'ModLezione','modifica lezione'),(@idS+1,'VisualizzaCorsi','visualizza elenco corsi'),(@idS+1,'VisualizzaTuoiCorsi','visualizza i tuoi corsi'),(@idS+1,'IscrizioneCorso','iscriviti ad un corso'),(@idS+1,'RecercaCorso','ricerca corso'),(@idS+1,'GCMainPage','visualizza main page gecv')

	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS+2,'AddCv','aggiungi cv');
	declare @idFGeCv int = (select IDENT_CURRENT('funzioni'));
	INSERT INTO funzioni(sistema,nome,descrizione) VALUES (@idS+2,'modCv','modifica cv'),(@idS+2,'EliminaCv','elimina cv'),(@idS+2,'RicercaCv','ricerca cv'),(@idS+2,'CaricaCv','carica cv');
	--funzioni di admin
	INSERT INTO funzioniAssociate(codiceRuolo,codiceFunzione) values(@idR,,(@idR,),(@idR,),(@idR,),(@idR,),(@idR,),(@idR,),(@idR,)
	--funzioni di professore
	INSERT INTO funzioniAssociate(codiceRuolo,codiceFunzione) values(@idR+1,@idFGeTime),(@idR+1,@idFGeTime+1),(@idR+1,@idFGeTime+2),(@idR+1,@idFGeTime+3),(@idR+1,@idFGeTime+4),(@idR+1,@idFGeCorso+1),(@idR+1,@idFGeCorso+2),(@idR+1,@idFGeCorso+3),(@idR+1,@idFGeCorso+4),(@idR+1,12),(@idR+1,14),(@idR+1,16);
	--funzioni di dipendente
	INSERT INTO funzioniAssociate(codiceRuolo,codiceFunzione) values(@idR+2,1),(@idR+2,2),(3,3),(@idR+2,4),(@idR+2,8),(@idR+2,9),(@idR+2,10),(@idR+2,11),(@idR+2,12),(@idR+2,14),(@idR+2,16);
	--funzioni di studente
	insert into funzioniAssociate(codiceRuolo,codiceFunzione) values (@idR+3,8),(@idR+3,9),(@idR+3,10),(@idR+3,11),(@idR+3,12),(@idR+3,13),(@idR+3,15),(@idR+3,17);
	--funzioni di sconosciuto
	insert into funzioniAssociate(codiceRuolo,codiceFunzione) values (@idR+4,4),(@idR+4,8),(@idR+4,11),(@idR+4,12),(@idR+4,17);
commit tran
end try
begin catch
	rollback tran
end catch
