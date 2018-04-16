create database GeCorsi;

create table Corsi(
	id int identity(1,1) primary key not null,
	nome nvarchar(50) not null,
	descrizione nvarchar (200),
	dInizio date,
	dFine date
);

create table Lezioni (
	id int identity(1,1) primary key not null,
	durata nvarchar(50) not null,
	idCorsi int foreign key references Corsi
);

create table Studenti(
	matr nvarchar(10) primary key not null,
	nome varchar (15) not null,
	cognome varchar(15) not null
);

create table StudentiCorsi(
	idCorso int foreign key references Corso,
	idStudenti int foreign key references Studenti,
	primary key(idCorso,idStudenti)
);