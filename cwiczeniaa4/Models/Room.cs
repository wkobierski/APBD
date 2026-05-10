using System.ComponentModel.DataAnnotations;

namespace cwiczenia4.Models;

public class Room
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name jest wymagane.")]
    [MinLength(1)]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "BuildingCode jest wymagane.")]
    [MinLength(1)]
    public string BuildingCode { get; set; } = string.Empty;

    public int Floor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity musi być większe od zera.")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }

    public bool IsActive { get; set; }
}
