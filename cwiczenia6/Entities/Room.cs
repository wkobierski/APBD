namespace cwiczenia6.Entities;

public class Room
{
    public string Id { get; set; } = null!;

    public int WardId { get; set; }

    public int HasTv { get; set; }

    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();

    public virtual Ward Ward { get; set; } = null!;
}
