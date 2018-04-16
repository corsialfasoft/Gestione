create database GeTime;
use GeTime;
create table TipologiaOre(
	id int identity(1,1) primary key,
	nomeTipoOre varchar(20),
	descrizione varchar(50)
);
insert into TipologiaOre(nomeTipoOre,descrizione) values('HLavorate','sono le ore lavorate su una commessa'), ('HMalatia','sono le ore di malatia'), ('HPermesso','sono le di permesso richieste in un giorno'), ('HFerie','sono 8 ore di ferie')
create table Giorni(
	id int primary key identity(1,1),
	giorno date not null,
	idUtente nvarchar(10) not null, 
);
create table OreNonLavorative(
	id int primary key identity(1,1),
	tipoOre int foreign key references TipologiaOre not null,
	ore int not null,
	idGiorno int foreign key references Giorni not null
)

create table Commesse(
	id int primary key identity(1,1),
	nome nvarchar(50) not null unique,
	descrizione nvarchar(200),
	stimaOre int 
);
create table OreLavorative(
	idGiorno int foreign key references Giorni not null,
	idCommessa int foreign key references Commesse not null,
	ore int not null,
	primary key(idGiorno,idCommessa)
);


drop database GeTime;
