namespace CRUD.Api.Api.Responses;

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public PaginatedResponse(List<T> items, int currentPage, int perPage, int totalItems)
    {
        ArgumentNullException.ThrowIfNull(items);

        Items = items;
        TotalItems = totalItems;
        TotalPages = Convert.ToInt32(Math.Ceiling(totalItems / (double)perPage));
        PreviousPage = currentPage > 1 ? currentPage - 1 : null;
        NextPage = currentPage < TotalPages ? currentPage + 1 : null;
    }
}