CREATE PROCEDURE RecuperaIdCv
	@Matricola nvarchar(10)
AS
SELECT IdCv FROM Curriculum WHERE Matricola=@Matricola;
GO

Alter PROCEDURE CercaParolaChiava
	@parola nvarchar(20)
AS
SELECT C.Matricola FROM Curriculum C
INNER JOIN PercorsoStudi PS ON C.IdCv = PS.IdCv 
INNER JOIN EspLav EL ON C.IdCv = EL.IdCv 
INNER JOIN Competenze CS ON C.IdCv = CS.IdCv 
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