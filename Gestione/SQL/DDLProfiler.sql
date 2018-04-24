CREATE DATABASE Profilatura
go
USE Profilatura
go
CREATE TABLE sistemi(
    codice INT IDENTITY (1,1) primary key NOT NULL,
    nome VARCHAR(50) NULL,
)
go
CREATE TABLE ruoli(
    codice INT IDENTITY(1,1) primary key NOT NULL,
    nome VARCHAR(50) NULL,
)
go
CREATE TABLE utenti(
    matricola INT IDENTITY(1,1) primary key NOT NULL,
    nome VARCHAR(50),
    cognome VARCHAR(50),
    username VARCHAR(50),
    passwd VARCHAR(50),
	fkruolo INT FOREIGN KEY REFERENCES ruoli not null
)
go
CREATE TABLE funzioni(
    codice INT IDENTITY(1,1) primary key NOT NULL,
    nome VARCHAR(50),
	fksistema INT FOREIGN KEY REFERENCES sistemi
)
go
CREATE TABLE funzioniAssociate(
	id INT IDENTITY(1,1) NOT NULL primary key,
    fkRuolo INT FOREIGN KEY REFERENCES ruoli,
    fkFunzione INT FOREIGN KEY REFERENCES funzioni
)
go