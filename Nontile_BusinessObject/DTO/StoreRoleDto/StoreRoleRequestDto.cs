namespace Nontitle_BusinessObject.DTO.StoreRoleDto;

public class StoreRoleRequestDto
{
    public string? StoreId { get; set; }
    public string? Name { get; set; }
    public List<string>? Permission { get; set; }
}