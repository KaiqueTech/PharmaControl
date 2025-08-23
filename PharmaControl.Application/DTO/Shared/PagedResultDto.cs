namespace PharmaControl.Application.DTO.Shared;

public class PagedResultDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<T>? Data { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public static PagedResultDto<T> Ok(List<T> data, int pageNumber, int pageSize, int totalRecords, string message = "")
        => new PagedResultDto<T>
        {
            Success = true,
            Data = data,
            Message = message,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };

    public static PagedResultDto<T> Fail(string message)
        => new PagedResultDto<T>
        {
            Success = false,
            Message = message,
            Data = new List<T>(),
            PageNumber = 0,
            PageSize = 0,
            TotalRecords = 0
        };
}