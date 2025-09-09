USE Northwind;
GO
--1.List all cities that have both Employees and Customers.
--select * from Customers;
--select * from Employees;
select distinct c.city 
from Customers as c;

--select distinct e.city 
--from  Employees as e;

--solution 1:
--select distinct c.city 
--from Customers as c
--inner join Employees as e
--on c.city = e.city;

--solution 2:
select city from customers
intersect
select city from Employees;

--2 List all cities that have Customers but no Employee.
--a.      Use sub-query
--solution 1:
select distinct c.city
from customers as c
where c.city not in (
	select city
	from Employees as e
	where e.city is not null
);

--solution 2
SELECT DISTINCT c.city
FROM Customers AS c
WHERE NOT EXISTS (
    SELECT 1
    FROM Employees AS e
    WHERE e.city = c.city
);
--b.      Do not use sub-query
select distinct c.city
from customers as c 
left join  Employees as e
on e.city = c.city
where e.city is null;


--3.  List all products and their total order quantities throughout all orders.
select  p.productname, sum(od.quantity) as total_quantity
from Products as p
left join [Order Details] as od
on p.ProductID = od.ProductID
group by p.productname;


--4.  List all Customer Cities and total products ordered by that city.


select distinct c.city, sum(od.quantity) as quantity_orders_of_current_city
from Customers as c
left join orders as o
on c.CustomerID = o.CustomerID
left join [Order Details] as od
on o.OrderID = od.OrderID
group by c.City;


select distinct c.city, sum(od.quantity) as quantity_orders_of_current_city
from Customers as c
join orders as o
on c.CustomerID = o.CustomerID
join [Order Details] as od
on o.OrderID = od.OrderID
where exists (
	select 1
	from orders o2
	where o2.CustomerID = c.CustomerID
)
group by c.City;

--5. List all Customer Cities that have at least two customers.
select c.city 
from Customers as c
group by c.city
having count(*) >=2;
--a.      Use union
select c.city 
from Customers as c
group by c.city
having count(*) >=2
union
select c.city 
from Customers as c
group by c.city
having count(*) >=2
;

--b.      Use sub-query and no union
select distinct city
from Customers
where city in (
	select city 
	from customers
	group by City
	having count(*) >=2
);


--6.List all Customer Cities that have ordered at least two different kinds of products.

select c.city
from customers as c
join orders as o
on c.CustomerID = o.CustomerID
join [Order Details] as od
on od.OrderID = o.OrderID
group by c.city
having count(distinct od.ProductID)>=2;


--7. List all Customers who have ordered products, but have the ‘ship city’ on the order different from their own customer cities.
select distinct c.contactname
from customers as c
join orders as o
on c.CustomerID = o.CustomerID
where o.ShipCity != c.City;

--8. List 5 most popular products, their average price, and the customer city that ordered most quantity of it.
with TopProducts as (
	select top 5 
		p.productid,
		p.productName,
		AVG(od.UnitPrice*(1-od.Discount)) as avg_price,
		sum(od.quantity) as total_quantity
	from
		products as p
	join 
		[Order Details] as od on p.ProductID = od.ProductID
	group by 
		p.productid, p.ProductName
	order by
		total_quantity desc
),
ProductCityQuantities as (
	select 
		p.productName,
		c.city,
		sum(od.quantity) as city_quantity,
		rank() over (partition by p.productname order by sum(od.quantity) desc) as rank
		
	from
		products as p
	join 
		[Order Details] as od on p.ProductID = od.ProductID
	join 
		orders as o on o.OrderID = od.OrderID
	join 
		Customers as c on c.CustomerID = o.CustomerID
	where
		p.ProductName in (select productname from TopProducts)
	group by 
		c.city, p.ProductName
	
)
select 
	tp.ProductName, 
	tp.avg_price, 
	pcq.City,
	pcq.city_quantity as max_city_quantity,
	tp.total_quantity as total_product_quantity

from 
	TopProducts as tp
join
	ProductCityQuantities as pcq on tp.ProductName = pcq.ProductName
where
	pcq.rank=1
order by
	tp.total_quantity desc;

;



--9.List all cities that have never ordered something but we have employees there.
--a.      Use sub-query
SELECT DISTINCT e.City as employee_city
FROM Employees e
WHERE e.City NOT IN (
    SELECT DISTINCT c.City
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
);

--solution 2
SELECT DISTINCT e.City as employee_city
FROM Employees e
WHERE NOT EXISTS (
    SELECT 1
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    WHERE c.City = e.City
);

--b.      Do not use sub-query
SELECT e.City as employee_city
FROM Employees e
LEFT JOIN Customers c ON e.City = c.City
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
LEFT JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY e.City
HAVING COUNT(od.OrderID) = 0;

--10.List one city, if exists, that is the city from where the employee sold most orders (not the product quantity) is, 
--and also the city of most total quantity of products ordered from. (tip: join  sub-query)
--solution 1
WITH OrderCounts AS (
    -- Subquery to count orders per employee city
    SELECT 
        e.City,
        COUNT(o.OrderID) AS TotalOrders
    FROM 
        Orders o
    JOIN 
        Employees e ON o.EmployeeID = e.EmployeeID
    GROUP BY 
        e.City
),
ProductQuantities AS (
    -- Subquery to sum product quantities per employee city
    SELECT 
        e.City,
        SUM(od.Quantity) AS TotalQuantity
    FROM 
        Orders o
    JOIN 
        Employees e ON o.EmployeeID = e.EmployeeID
    JOIN 
        [Order Details] od ON o.OrderID = od.OrderID
    GROUP BY 
        e.City
)
-- Main query to find the cities with the highest orders and quantities
SELECT 
    (SELECT top 1 City FROM OrderCounts ORDER BY TotalOrders DESC ) AS CityWithMostOrders,
    (SELECT top 1 City FROM ProductQuantities ORDER BY TotalQuantity DESC ) AS CityWithMostQuantity;


--solution 2
SELECT 
    -- subqury：most order city
    (SELECT top 1 e.City
     FROM Orders o
     JOIN Employees e ON o.EmployeeID = e.EmployeeID
     GROUP BY e.City
     ORDER BY COUNT(o.OrderID) DESC
     ) AS CityWithMostOrders,

    -- subquery : most product city
    (SELECT top 1 e.City
     FROM Orders o
     JOIN Employees e ON o.EmployeeID = e.EmployeeID
     JOIN [Order Details] od ON o.OrderID = od.OrderID
     GROUP BY e.City
     ORDER BY SUM(od.Quantity) DESC
     ) AS CityWithMostQuantity;



--11.How do you remove the duplicates record of a table?
DROP TABLE IF EXISTS clear_orders;

SELECT DISTINCT * into clear_orders
FROM Orders;
select * from clear_orders;