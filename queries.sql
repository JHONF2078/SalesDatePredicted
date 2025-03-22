--get the last order date and the next predicted order date for each customer

WITH OrderIntervals AS (
    SELECT 
        o.CustId,
        o.OrderDate,
        LAG(o.OrderDate) OVER (PARTITION BY o.CustId ORDER BY o.OrderDate) AS PreviousOrderDate
    FROM 
        Sales.Orders o
)
SELECT 
    c.CompanyName AS [Customer Name],
    MAX(o.OrderDate) AS [Last Order Date],
    DATEADD(DAY, AVG(DATEDIFF(DAY, oi.PreviousOrderDate, oi.OrderDate)), MAX(o.OrderDate)) AS [Next Predicted Order]
FROM 
    Sales.Orders o
JOIN 
    Sales.Customers c ON o.CustId = c.CustId
JOIN 
    OrderIntervals oi ON o.CustId = oi.CustId AND o.OrderDate = oi.OrderDate
WHERE 
    oi.PreviousOrderDate IS NOT NULL
GROUP BY 
    c.CompanyName, o.CustId
ORDER BY 
    c.CompanyName;



--get the order  for a specific customer
SELECT OrderId, RequiredDate, ShippedDate, ShipName, ShipAddress
FROM Sales.Orders
WHERE CustId = @CustId;


--get employee full names
SELECT EmpId, FirstName + ' ' + LastName AS FullName
FROM HR.Employees;


--get shippers
SELECT ShipperId, CompanyName
FROM Sales.Shippers