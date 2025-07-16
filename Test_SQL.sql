--1.	Вывести менеджеров, у которых имеется номер телефона
SELECT Fio
FROM Managers
WHERE Phone IS NOT NULL;
--2.	Вывести кол-во продаж за 20 июня 2025

SELECT COUNT(*)
FROM Sells
WHERE Date = '2025-06-20';
--3.	Вывести среднюю сумму продажи с товаром 'Фанера'

SELECT AVG(Sum)
FROM Sells s
         JOIN Products p ON s.ID_Prod = p.ID
WHERE p.Name = 'Фанера';
--4.	Вывести фамилии менеджеров и общую сумму продаж для каждого с товаром 'ОСБ'

SELECT m.Fio, SUM(s.Sum) as TotalSales
FROM Managers m
         JOIN Sells s ON m.ID = s.ID_Manag
         JOIN Products p ON s.ID_Prod = p.ID
WHERE p.Name = 'ОСБ'
GROUP BY m.Fio;
--5.	Вывести менеджера и товар, который продали 22 августа 2024

SELECT m.Fio, p.Name
FROM Managers m
         JOIN Sells s ON m.ID = s.ID_Manag
         JOIN Products p ON s.ID_Prod = p.ID
WHERE s.Date = '2024-08-22';
--6.	Вывести все товары, у которых в названии имеется 'Фанера' и цена не ниже 1750

SELECT *
FROM Products
WHERE Name LIKE '%Фанера%'
  AND Cost >= 1750;
--7.	Вывести историю продаж товаров, группируя по месяцу продажи и наименованию товара

SELECT DATEPART(MONTH, s.Date) as Month,
       p.Name,
       SUM(s.Count)            as TotalCount,
       SUM(s.Sum)              as TotalSum
FROM Sells s
         JOIN Products p ON s.ID_Prod = p.ID
GROUP BY DATEPART(MONTH, s.Date), p.Name
ORDER BY Month, p.Name;

--8.	Вывести количество повторяющихся значений и сами значения из таблицы 'Товары', где количество повторений больше 1.
SELECT Name, COUNT(*) as DuplicateCount
FROM Products
GROUP BY Name
HAVING COUNT(*) > 1;