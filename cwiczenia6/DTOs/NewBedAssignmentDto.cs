using System.ComponentModel.DataAnnotations;

namespace cwiczenia6.DTOs;

public class NewBedAssignmentDto
{
    [Required]
    public DateTime From { get; set; }

    public DateTime? To { get; set; }

    [Required]
    public string BedType { get; set; } = null!;

    [Required]
    public string Ward { get; set; } = null!;
}
