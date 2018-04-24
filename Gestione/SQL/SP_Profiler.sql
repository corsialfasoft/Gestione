create procedure GetProfile
as
	SELECT utenti.matricola,utenti.nome,utenti.cognome,funzioni.nome FROM utenti
	INNER JOIN ruoli on utenti.fkruolo = codice.ruoli INNER JOIN funzioni on funzioni.codice = funzioniAssociate.fkFunzione
	INNER JOIN funzioniAssociate on funzioniAssociate.fkRuolo = ruoli.codice INNER JOIN sistemi on sistemi.codice = funzioni.fksistema
go