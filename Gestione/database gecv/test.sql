select * from Curriculum

Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Panino',22,'AAAA')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Panino',22,'BBBB')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Panino',22,'CCCC')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Panino',22,'DDDD')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Panino',22,'EEEE')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Franzoso',23,'FFFF')
Insert into Curriculum (nome,Cognome,Eta,Matricola) values ('Pino','Urdue',29,'GGGGG')

sELECT * FROM EspLav

Insert into EspLav (DESCRIZIONE,idCV) values ('binzinaro',1)
Insert into EspLav (DESCRIZIONE,idCV) values ('panettiere',2)
Insert into EspLav (DESCRIZIONE,idCV) values ('parcheggiatore',3)
Insert into EspLav (DESCRIZIONE,idCV) values ('birdwatcher',4)
Insert into EspLav (DESCRIZIONE,idCV) values ('bagnigno',5)

Select * from Competenze
Insert into Competenze (tipo,IdCv) values ('Primo tipo',1)
Insert into Competenze (tipo,IdCv) values ('Secondo tipo',2)
Insert into Competenze (tipo,IdCv) values ('Terzo tipo',3)
Insert into Competenze (tipo,IdCv) values ('Quarto tipo',4)
Insert into Competenze (tipo,IdCv) values ('Quinto tipo',5)

Select * from PercorsoStudi

iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Primina',1)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Seconda',2)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Terza',3)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Quarta',4)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Quinta',5)

sELECT * FROM Curriculum c inner join EspLav el on c.IdCv=el.IdCv
							inner join Competenze co on c.IdCv =co.IdCv
							inner join PercorsoStudi ps on c.IdCv=ps.IdCv
							where c.Matricola = 'BBBB'
							go;
Create procedure SearchByID
	@id int
as
	select * from ProdottiSet where ID=@id;
	go


exec dbo.SearchbyId 1;

Alter Procedure CreaRichiesta
@date date
as
Insert into RichiesteSet (data) values (@date)
declare @Richiesta int
Select IDENT_CURRENT('RichiesteSet')

exec dbo.CreaRichiesta '2014/04/05'

Create Procedure CreaOrdini
@idRichiesta int,
@idProdotti int,
@quantita int
as
declare @ControlloRichiesta int
set @ControlloRichiesta = (Select top 1 id from RichiesteSet where id=@idRichiesta)
begin
if @ControlloRichiesta is null
rollback transaction
end
declare @ControlloProdotti int
set @ControlloProdotti = (Select top 1 id from ProdottiSet where id=@idProdotti)
begin
if @ControlloProdotti is null
rollback transaction
end
Insert into RichiesteProdotti(Richieste_Id,Prodotti_Id,quantita) values (@idRichiesta,@idProdotti,@quantita)