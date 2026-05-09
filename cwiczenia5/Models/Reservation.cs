using System.ComponentModel.DataAnnotations;

namespace cwiczenia4.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "OrganizerName jest wymagane.")]
    [MinLength(1)]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Topic jest wymagane.")]
    [MinLength(1)]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Status jest wymagany.")]
    public string Status { get; set; } = "planned";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime musi być późniejsze niż StartTime.",
                [nameof(EndTime)]);
        }
    }
}
