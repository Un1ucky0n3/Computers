namespace Computers.DTOs;

public class GetPCDetailsDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public decimal weight { get; set; }
    public int warranty { get; set; }
    public DateTime createdAt { get; set; }
    public int stock { get; set; }
}