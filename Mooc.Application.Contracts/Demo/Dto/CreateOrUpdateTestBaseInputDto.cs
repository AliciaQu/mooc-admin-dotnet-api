namespace Mooc.Application.Contracts.Demo.Dto;

public class CreateOrUpdateTestBaseInputDto : BaseEntityDto
{
    [Required]
    public virtual string Title { get; set; }
    public int Count { get; set; }

}
