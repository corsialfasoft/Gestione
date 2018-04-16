Alter procedure DeleteCurriculum
	@idcurr nvarchar(50)
as
	begin transaction 
	declare @test int;
	set @test = (select C.IdCv from Curriculum c where c.Matricola=@idcurr);
	if @test is null
		begin 
			rollback transaction ;
			throw 66666 , 'id errato riprovare!' ,2;
		end
	else
		begin
			delete Competenze from Competenze cs where cs.IdCv = @test;
			delete EspLav from EspLav es where es.IdCv=@test;
			delete PercorsoStudi from PercorsoStudi ps where ps.IdCv=@test;
			delete Curriculum from Curriculum c where c.IdCv=@test;
			commit transaction;
		end
go

Alter procedure CercaEtaMinMax
	@e_min int , 
	@e_max int 
as
	Select c.Matricola from Curriculum c  where c.Eta between @e_min and @e_max ;
go
Create procedure CercaCognome
	@cognome nvarchar(40)
as
	Select c.Matricola from Curriculum c where c.cognome= @cognome;
go

Create procedure ModificaCurriculum
	@idcurr int ,
	@nomeM nvarchar(50),
	@cognomeM nvarchar(50),
	@etaM int,
	@matricolaM nvarchar(10),
	@emailM nvarchar(30),
	@residenzaM nvarchar(100),
	@telefonoM nvarchar(10)
as
	begin transaction
	declare @test int;
	set @test = (select c.IdCv from Curriculum c where c.IdCv=@idcurr);
	if	@test is null
		begin 
			rollback transaction ;
			throw 66666 , 'id errato riprovare!' ,2;
		end
	else
		begin 
		UPDATE Curriculum SET Nome= @nomeM , Cognome= @cognomeM , Eta= @etaM ,
				Matricola = @matricolaM ,Email = @emailM , Residenza = @residenzaM , Telefono= @telefonoM 
				where IdCv = @idcurr;
		commit transaction
		end
go

Create procedure CercaEta
	@eta int
as
	Select c.Matricola from Curriculum c Where c.Eta=@eta
go

exec CercaEta 22;
go

create procedure CercaCitta
	@citta nvarchar
as
	select c.IdCv from Curriculum c where c.residenza like '%'+@citta+'%';
go

create procedure CercaMatricola
	@matri nvarchar
as
	select c.IdCv From Curriculum c where c.matricola=@matri;
go

exec CercaMatricola 'Aaha11';	
	