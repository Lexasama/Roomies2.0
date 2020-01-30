CREATE VIEW rm2.vGroceryListItems 
AS
	SELECT
		g.GroceryListId,
		i.ItemId,
		i.ItemAmount,
		t.ItemName,
		t.UnitPrice
	FROM rm2.tGroceryList g 
		LEFT JOIN rm2.itGroceryListItem i ON i.GroceryListId = g.GroceryListId
		LEFT JOIN rm2.tItem t ON t.ItemId = i.ItemId
	WHERE g.GroceryListId <> 0 and i.ItemId <> 0;

