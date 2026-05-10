namespace cwiczenia5.Entities;

public class PcComponent
{
    public int PcId { get; set; }
    public string ComponentCode  { get; set; } = string.Empty;
    public int Amount { get; set; }
    
    public Pc Pc { get; set; }
    public Component Component { get; set; }
    
}