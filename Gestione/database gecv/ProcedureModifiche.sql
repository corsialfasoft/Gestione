alter procedure ModEspLav
	@matricola nvarchar(10),
	@annoIdaMod int, @annoFdaMod int,
	@qualificaDaMod nvarchar(50),
	@descrDaMod nvarchar(200),
	@annoIMod int , @annoFMod int ,
	@qualificaMod nvarchar(50),
	@descrMod nvarchar(200)
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
			begin
			declare @idEsp int;
			set @idEsp = (select e.IdEl from EspLav e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
							e.AnnoI= @annoIdaMod and e.Qualifica= @qualificaDaMod and e.Descrizione= @descrDaMod );
			if @idEsp is null 
				throw 66666 , 'Id ESP LAV NON TROVATO Errata!!!!!! RIPROVA' ,2;
			else
				begin
				update EspLav set AnnoI = @annoIMod , AnnoF= @annoFMod, Qualifica = @qualificaMod , Descrizione=@descrMod
						 where IdEl = @idEsp;
				end
			end
go

exec ModEspLav 'AAAA',0,0,'srfg','binzinaro',1,2,'fatto','rifatto'

Alter procedure ModPerStud
	@matricola nvarchar(10),
	@annoIdaMod int, @annoFdaMod int,
	@titoloDaMod nvarchar(50),
	@descrDaMod nvarchar(200),
	@annoIMod int , @annoFMod int ,
	@titoloMod nvarchar(50),
	@descrMod nvarchar(200)
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
			begin
			declare @idpers int;
			set @idpers = (select e.IdPs from PercorsoStudi e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
							e.AnnoI= @annoIdaMod and e.Titolo= @titoloDaMod and e.Descrizione= @descrDaMod );
			if @idpers is null 
				throw 66666 , 'Id PERCORSO STUDI NON TROVATO Errata!!!!!! RIPROVA' ,2;
			else
				begin
				update PercorsoStudi  set AnnoI = @annoIMod , AnnoF= @annoFMod, Titolo = @titoloMod , Descrizione=@descrMod
						 where IdPs = @idpers;
				end
			end
go

alter procedure ModComp
	@matricola nvarchar(10),
	@titoloDaMod nvarchar(50),
	@livDaMod int , 
	@titoloMod nvarchar(50),
	@livMod int
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin 
			declare @idComp int;
			set @idComp = (select c.IdCs from Competenze c where c.Tipo= @titoloDaMod and c.Livello=@livDaMod and c.IdCv=@idcurr);
			if @idComp is null
				throw 66666 , 'Competenza Errata!!!!!! RIPROVA' ,2;
			else 
				begin
					UPDATE Competenze set Tipo= @titoloMod , Livello= @livMod where IdCs=@idComp;
				end
			end
go

create procedure ModificaCV
	@nome nvarchar(50),
	@cognome nvarchar(50),
	@eta int,
	@matr nvarchar(10),
	@email nvarchar(30),
	@residenza nvarchar(100),
	@telefono nvarchar(10)
as
	update Curriculum  set Nome=@nome,Cognome=@cognome,Eta=@eta,Email=@email,Residenza=@residenza,Telefono=@telefono
		where Matricola=@matr;
go

Create procedure DelEspLav
	@matricola nvarchar(10),
	@annoIdaDel int, @annoFdaDel int,
	@qualificaDaDel nvarchar(50),
	@descrDaDel nvarchar(200)
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
			begin
			declare @idEsp int;
			set @idEsp = (select e.IdEl from EspLav e where e.IdCv= @idcurr and e.AnnoF=@annoFdaDel and
							e.AnnoI= @annoIdaDel and e.Qualifica= @qualificaDaDel and e.Descrizione= @descrDaDel );
			if @idEsp is null 
				throw 66666 , 'Id ESP LAV NON TROVATO Errata!!!!!! RIPROVA' ,2;
			else
				begin
				Delete EspLav where idEl =@idEsp
				end
			end
go

Create procedure DelComp
	@matricola nvarchar(10),
	@titolo nvarchar(50),
	@livello int
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin 
			declare @idComp int;
			set @idComp = (select c.IdCs from Competenze c where c.Tipo= @titolo and c.Livello=@livello and c.IdCv=@idcurr);
			if @idComp is null
				throw 66666 , 'Competenza Errata!!!!!! RIPROVA' ,2;
			else 
				begin
					Delete Competenze where idCs=@idComp
				end
			end
go

Select * from Competenze
Insert Competenze (tipo,livello,IdCv) values ('ABCD',22,11)