namespace Mooc.Application.Contracts.Dto;

public interface IPagedResultRequest
{
    /// <summary>
    /// page index
    /// </summary>
    int Page { get; set; }

    /// <summary>
    /// page size
    /// </summary>
    int PageSize { get; set; }
}
