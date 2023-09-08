CREATE OR ALTER TRIGGER UpdateTotalPrice
ON OrderItems
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	UPDATE Orders
	SET TotalPrice = (
        SELECT SUM(Price * Quantity)
        FROM OrderItems
        WHERE OrderItems.OrderId = Orders.Id
    )
END
