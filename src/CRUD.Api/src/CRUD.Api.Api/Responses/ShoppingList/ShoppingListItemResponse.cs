namespace CRUD.Api.Api.Responses.ShoppingList;

public class ShoppingListItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}

public class ShoppingListItemDetailResponse : ShoppingListItemResponse
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}