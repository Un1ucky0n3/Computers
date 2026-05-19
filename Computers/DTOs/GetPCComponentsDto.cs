namespace Computers.DTOs;

public class GetPCComponentsDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public decimal weight { get; set; }
    public int warranty { get; set; }
    public DateTime createdAt { get; set; }
    public int stock { get; set; }
    public List<ComponentAmountDto> components { get; set; }
}