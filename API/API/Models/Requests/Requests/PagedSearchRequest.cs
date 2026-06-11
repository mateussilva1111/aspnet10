namespace API.Models.Requests.Requests
{
    public class PagedSearchRequest
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SearchColumn { get; set; }

        public string? Search { get; set; }
    }
}
