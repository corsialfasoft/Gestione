create procedure ModEspLav
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
		begin 
			rollback transaction ;
			throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
		end
	else
			begin
			declare @idEsp int;
			set @idEsp = (select e.IdEl from EspLav e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
							e.AnnoI= @annoIdaMod and e.Qualifica= @qualificaDaMod and e.Descrizione= @descrDaMod );
			if @idEsp is null 
				begin
				rollback transaction ;
				throw 66666 , 'Id ESP LAV NON TROVATO Errata!!!!!! RIPROVA' ,2;
				end
			else
				begin
				update EspLav set AnnoI = @annoIMod , AnnoF= @annoFMod, Qualifica = @qualificaMod , Descrizione=@descrMod
						 where IdEl = @idEsp;
				commit transaction
				end
			end
go

create procedure ModPerStud
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
		begin 
			rollback transaction ;
			throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
		end
	else
			begin
			declare @idpers int;
			set @idpers = (select e.IdPs from PercorsoStudi e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
							e.AnnoI= @annoIdaMod and e.Titolo= @titoloDaMod and e.Descrizione= @descrDaMod );
			if @idpers is null 
				begin
				rollback transaction ;
				throw 66666 , 'Id PERCORSO STUDI NON TROVATO Errata!!!!!! RIPROVA' ,2;
				end
			else
				begin
				update PercorsoStudi  set AnnoI = @annoIMod , AnnoF= @annoFMod, Titolo = @titoloMod , Descrizione=@descrMod
						 where IdPs = @idpers;
				commit transaction
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
		begin 
			rollback transaction ;
			throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
		end
	else
		begin 
			begin transaction 
			declare @idComp int;
			set @idComp = (select c.IdCs from Competenze c where c.Tipo= @titoloDaMod and c.Livello=@livDaMod and c.IdCv=@idcurr);
			if @idComp is null
				begin 
				rollback transaction ;
				throw 66666 , 'Competenza Errata!!!!!! RIPROVA' ,2;
				end
			else 
				begin
				UPDATE Competenze set Tipo= @titoloMod , Livello= @livMod where IdCs=@idComp;
				commit transaction
				end
			end
go