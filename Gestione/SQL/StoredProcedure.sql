create procedure SP_VisualizzaCommessa
	@idC int,
	@idU nvarchar(10)
as 
	select G.id,G.giorno,OL.ore,C.id,C.nome,C.descrizione
	from Giorni G inner join OreLavorative OL on G.id=OL.idGiorno inner join Commesse C on OL.idCommessa=C.id
	where G.idUtente=@idU and C.id=@idC;
go
create procedure SP_CercaCommessa
	@nomeCommessa nvarchar(50)
as	
	select id,nome,descrizione,stimaOre, (select SUM(OL.ore)
										  from OreLavorative OL inner join Commesse C1 on OL.idCommessa=C1.id
										  where C.id=C1.id) as OreTotLavorate
	from Commesse C
	where nome = @nomeCommessa;
go
insert into Giorni(giorno,idUtente) values ('2018-04-13','12342'),('2018-04-14','12342'),('2018-04-15','12342');
insert into Commesse(nome,descrizione,stimaOre) values('GeTime','non so cosa fare',100);
insert into OreLavorative(idGiorno,idCommessa,ore) values(1,1,10);
insert into OreLavorative(idGiorno,idCommessa,ore) values(2,1,10);
insert into OreLavorative(idGiorno,idCommessa,ore) values(3,1,10);