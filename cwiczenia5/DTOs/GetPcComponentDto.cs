namespace cwiczenia5.DTOs;

public class GetPcComponentDto
{
    public int Amount { get; set; }
    public GetComponentDto Component { get; set; } = new();
}
