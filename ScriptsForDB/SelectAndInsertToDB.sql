USE BankOnline;
--insert
INSERT INTO Phone (phone_number, suma, id_user) VALUES
('0994567890', 0, 1),
('0987654321', 75.00, 2),
('0972223333', 20.00, 3);

INSERT INTO MyUser (surname, name, pass, id_tel) VALUES
('Пилипчук', 'Іван', 'pass123', 1),
('Котенко', 'Олександра', 'pass456', 2),
('Дмитрюк', 'Дмитро', null, 3);

INSERT INTO CARD (card_number, balance, pincode, id_user) VALUES
('1111222233334444', 3000.00, '1234', 1),
('5555666677778888', 500.00, '5678', 2),
('9999000011112222', 750.00, '9101', 3);

INSERT INTO Residence (city, street, home, gas, electricity, internet) VALUES
('Київ', 'Шевченка', '10А', 50.00, 100.00, 30.00),
('Львів', 'Франка', '5Б', 40.00, 90.00, 25.00),
('Одеса', 'Бандери', '22', 45.00, 95.00, 28.00);

INSERT INTO UserResidence (id_user, id_residence) VALUES
(1, 1),
(2, 2),
(3, 3);

INSERT INTO CARD (card_number, balance, pincode, id_user) VALUES
('3333444455556666', 2500.00, '4321', 1)

--select
Select * from CARD
select * from History
select * from MyUser
select * from Phone
select * from Residence
select * from UserResidence