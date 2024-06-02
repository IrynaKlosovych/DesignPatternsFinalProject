CREATE DATABASE BankOnline;
USE BankOnline;

CREATE TABLE Phone(
	id_phone int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	phone_number varchar(10) not null,
	suma decimal not null,
	id_user int UNIQUE not null
);

CREATE TABLE MyUser (
  id_user int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  surname varchar(50) not null,
  name varchar(50) not null,
  pass varchar(50),
  id_tel INT UNIQUE REFERENCES Phone(id_phone) not null
);

CREATE TABLE CARD(
	id_card int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	card_number varchar(16) not null,
	balance decimal not null,
	pincode varchar(4) not null,
	id_user INT REFERENCES MyUser(id_user) not null
);

CREATE TABLE Residence(
	id_residence int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	city varchar(20) not null,
	street varchar(20) not null,
	home varchar(10) not null,
	gas decimal not null,
	electricity decimal not null, 
	internet decimal not null
);

CREATE TABLE UserResidence(
	id_user INT REFERENCES MyUser(id_user) not null,
	id_residence INT REFERENCES Residence(id_residence) not null
);

CREATE TABLE History(
	id_history INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	date DATETIME not null,
	description varchar(500) not null,
	suma decimal not null,
	id_user INT REFERENCES MyUser(id_user) not null
);

ALTER TABLE History
ADD id_card INT REFERENCES CARD(id_card) not null;

