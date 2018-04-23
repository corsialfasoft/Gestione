CREATE PROCEDURE RecuperaIdCv
	@Matricola nvarchar(10)
AS
SELECT IdCv FROM Curriculum WHERE Matricola=@Matricola;
GO

Create PROCEDURE CercaParolaChiava
	@parola nvarchar(20)
AS
SELECT C.Matricola FROM Curriculum C
left JOIN PercorsoStudi PS ON C.IdCv = PS.IdCv 
left JOIN EspLav EL ON C.IdCv = EL.IdCv 
left JOIN Competenze CS ON C.IdCv = CS.IdCv 
WHERE C.Nome like '%'+ @parola +'%'
OR C.Cognome like '%'+ @parola +'%'
OR C.email like '%'+ @parola +'%'
OR C.Residenza like '%'+ @parola +'%'
OR PS.Titolo like '%'+ @parola +'%'
OR PS.Descrizione like '%'+ @parola +'%'
OR EL.Qualifica like '%'+ @parola +'%'
OR EL.Descrizione like '%'+ @parola +'%'
OR CS.Tipo like '%'+ @parola +'%'
GO

CREATE PROCEDURE CercaLingua
	@competenza nvarchar(20)
AS
SELECT C.IdCv FROM Curriculum C
INNER JOIN Competenze CS ON C.IdCv = CS.IdCv
WHERE CS.Tipo like '%'+ @competenza +'%'
GO

Alter Procedure GetCV
	@Matricola nvarchar(10)
as
select top 1 c.nome,c.cognome,c.eta,c.matricola,c.email,c.residenza,c.telefono
	from Curriculum c where c.Matricola=@Matricola;
go

Alter Procedure GetComp
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select c.Livello,c.Tipo from Competenze c where c.IdCv=@idc;
go

Alter procedure GetPerStudi
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select p.AnnoI,p.AnnoF,p.Titolo,p.Descrizione from PercorsoStudi p where p.IdCv=@idc;
go

Alter procedure GetEspLav
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select e.AnnoI,e.AnnoF,e.Qualifica,e.Descrizione from EspLav e where e.IdCv=@idc;
go
