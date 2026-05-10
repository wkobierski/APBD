namespace cwiczenia5.DTOs;

public class GetPcWithComponentsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public ICollection<GetPcComponentDto> Components { get; set; } = [];
}
