namespace Mooc.Application.Contracts.Dto;

public class PagedResultRequestDto : IPagedResultRequest, ISortedResultRequest
{
    [Required]
    public int Page { get; set; } = 1;

    [Required]
    public int PageSize { get; set; } = 10;
    public string? Sort { get; set ; }
}
