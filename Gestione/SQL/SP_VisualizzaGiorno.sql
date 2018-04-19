use GeTime
go

create procedure SP_VisualizzaGiorno
	@Data date,
	@IdUtente nvarchar(20)
as
	begin transaction;
	begin try
		select 4, ol.ore, c.nome, c.descrizione, c.id
			from Commesse c inner join OreLavorative ol on c.id = ol.idCommessa
							inner join Giorni g on ol.idGiorno = g.id
			where g.giorno = @Data and g.idUtente = @IdUtente
			union all
		select  onl.tipoOre, onl.ore, '','', 1
			from OreNonLavorative onl inner join Giorni g on onl.idGiorno = g.id
			where g.giorno = @Data and g.idUtente = @IdUtente;
	end try
	begin catch
		select
			error_number() as MamboNumber5,
			error_message() as DataNonTrovata;
			rollback transaction;
	end catch
	commit transaction;
go

drop procedure SP_VisualizzaGiorno
go

insert into Giorni (giorno, idUtente) values ('2018-04-16', 'admin');
insert into Commesse (nome, descrizione) values ('MVC', 'lavorato su mvc');
insert into OreLavorative (idGiorno, idCommessa, ore) values (1, 1, 2);
insert into Commesse (nome, descrizione) values ('EF', 'lavorato su ef');
insert into OreLavorative (idGiorno, idCommessa, ore) values (1, 2, 1);
insert into Commesse (nome, descrizione) values ('SQL', 'lavorato su sql');
insert into OreLavorative (idGiorno, idCommessa, ore) values (1, 3, 1);
insert into OreNonLavorative(tipoOre, ore, idGiorno) values (2, 2, 1);
insert into OreNonLavorative(tipoOre, ore, idGiorno) values (3, 2, 1);
insert into Giorni (giorno, idUtente) values ('2018-04-14', 'admin');
insert into OreNonLavorative(tipoOre, ore, idGiorno) values (4, 8, 2);
go