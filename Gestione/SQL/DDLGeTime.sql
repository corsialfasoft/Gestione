create database GeTime;
use GeTime;
create table TipologiaOre(
	id int identity(1,1) primary key,
	nomeTipoOre varchar(20),
	descrizione varchar(50)
);
insert into TipologiaOre(nomeTipoOre,descrizione) values ('Malattia','sono le ore di malattia'), ('Permesso','sono le ore di permesso richieste in un giorno'), ('Ferie','sono 8 ore di ferie'), ('Lavorative','sono le ore lavorate su una commessa')
create table Giorni(
	id int primary key identity(1,1),
	giorno date not null,
	idUtente nvarchar(10) not null, 
);
create table OreNonLavorative(
	tipoOre int foreign key references TipologiaOre not null,
	ore int not null,
	idGiorno int foreign key references Giorni not null
	primary key(idGiorno,tipoOre)
);

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

--TestVisualizzaGiorno 
insert into Giorni(giorno,idUtente) values ('2000-01-01','GeTimeTestVisualizzaGiorno','questo Giorno viene utilizzata per il test')
declare @idG int = IDENT_CURRENT('Giorni');
insert into Commesse(nome,descrizione,stimaOre) values ('GeTime TestVisualizzaGiorno','questa commessa viene utilizzata per il test',5)
declare @idC int = IDENT_CURRENT('Commesse');
insert into OreLavorativa(idGiorno,idCommessa,ore) values (@idG,@idC,4)
insert into OreNonLavorativa(tipoOre,ore,idGiorno) values (1,4,@idG)

--TestVisualizzaCommessa 
insert into Commesse(nome,descrizione,stimaOre) values ('GeTime TestVisualizzaCommessa','questa commessa viene utilizzata per il test',5)

declare @idC1 int = IDENT_CURRENT('Commesse');
insert into Giorni(giorno,idUtente) values ('2000-01-01','GeTimeTestVisualizzaCommessa','questo Giorno viene utilizzata per il test')
declare @idG1 int = IDENT_CURRENT('Giorni');
insert into OreLavorativa(idGiorno,idCommessa,ore) values (@idG1,@idC1,4)

insert into Giorni(giorno,idUtente) values ('2000-01-02','GeTimeTestVisualizzaCommessa','questo Giorno viene utilizzata per il test')
declare @idG2 int = IDENT_CURRENT('Giorni');
insert into OreLavorativa(idGiorno,idCommessa,ore) values (@idG2,@idC1,4)
--TestCompilaOreLavorative

insert into Commesse(nome,descrizione,stimaOre) values ('GeTime TestCompilaOreL','questa commessa viene utilizzata per il test',5)
drop database GeTime;

