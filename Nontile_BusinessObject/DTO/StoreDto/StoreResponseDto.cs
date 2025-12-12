namespace Nontitle_BusinessObject.DTO.StoreDto;

public class StoreResponseDto
{
    public string StoreId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? Images { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}

