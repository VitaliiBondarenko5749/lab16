USE OOP_lab16
GO
INSERT INTO Countries(CountryName)
VALUES('������'), ('ͳ�������'), ('�������'), ('�����')

INSERT INTO Cities(CityName, Country_ID)
VALUES ('�������', 1), ('���', 1), ('�����-���������', 1),
('�����', 2), ('������', 2), ('�����', 2),
('�����', 3), ('�������', 3), ('˳��', 3),
('������', 4), ('���', 4), ('̳���', 4)

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
VALUES('��������'), ('��������'), ('����')

INSERT INTO Goods(GoodName, Section_ID, Share_ID)
VALUES ('������� SAMSUNG', 1, 1), ('������� Apple Iphone', 1, 2),
('������� HP', 2, 3), ('������� LENOVO', 2, 4),
('����', 3, 1), ('���', 3, 4)

INSERT INTO Buyers(FullName, Sex, Email, City_ID)
VALUES('���������� ³���� ³��������', 'M', 'bvetal2@gmail.com', 1), 
('����� ����� ��������', 'M', 'kpavlo2@gmail.com', 3),
('����� ������ ��������', 'M', 'kbogdan2@gmai.com', 5),
('������ ��������� �������������', 'M', 'vvaskan2@gmail.com', 7),
('������� ������ ���������', 'M', 'kmaks23@gmail.com', 9)

INSERT INTO Buyers_Sections(Buyer_ID, Section_ID)
VALUES(1, 1), (2, 2), (3, 3), (4, 1), (5, 2)
GO