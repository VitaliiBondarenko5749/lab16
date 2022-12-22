USE OOP_lab16
GO
INSERT INTO Countries(CountryName)
VALUES('Україна'), ('Німеччина'), ('Франція'), ('Італія')

INSERT INTO Cities(CityName, Country_ID)
VALUES ('Чернівці', 1), ('Київ', 1), ('Івано-Франківськ', 1),
('Кельн', 2), ('Мюнхен', 2), ('Берлін', 2),
('Париж', 3), ('Марсель', 3), ('Ліон', 3),
('Тренто', 4), ('Рим', 4), ('Мілан', 4)

INSERT INTO Shares(ShareStartDate, ShareFinishDate)
VALUES('2022-01-01', '2022-01-12'),
('2022-02-01', '2022-02-12'),
('2022-03-01', '2022-03-12'),
('2022-04-01', '2022-04-12')

INSERT INTO Countries_Shares(Country_ID, Share_ID)
VALUES(1, 1), (1, 3),
(2, 2), (2, 2),
(3, 1), (3, 3),
(4, 2), (4, 2)

INSERT INTO Sections(SectionName)
VALUES('Телефони'), ('Ноутбуки'), ('Меблі')

INSERT INTO Goods(GoodName, Section_ID, Share_ID)
VALUES ('Телефон SAMSUNG', 1, 1), ('Телефон Apple Iphone', 1, 2),
('Ноутбук HP', 2, 3), ('Ноутбук LENOVO', 2, 4),
('Шафа', 3, 1), ('Стіл', 3, 4)

INSERT INTO Buyers(FullName, Sex, Email, City_ID)
VALUES('Бондаренко Віталій Віталійович', 'M', 'bvetal2@gmail.com', 1), 
('Краус Павло Павлович', 'M', 'kpavlo2@gmail.com', 3),
('Краус Богдан Павлович', 'M', 'kbogdan2@gmai.com', 5),
('Васкан Володимир Володимирович', 'M', 'vvaskan2@gmail.com', 7),
('Кравчук Максим Дмитрович', 'M', 'kmaks23@gmail.com', 9)

INSERT INTO Buyers_Sections(Buyer_ID, Section_ID)
VALUES(1, 1), (2, 2), (3, 3), (4, 1), (5, 2)
GO