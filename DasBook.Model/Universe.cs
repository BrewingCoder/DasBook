using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DasBook.Model.Enums;

namespace DasBook.Model;

public class Universe : Entity
{
    [Column(TypeName = "varchar(Max)")]
    [MaxLength]
    public string? Description { get; set; }
    
    public TimeScaleMode TimeScale { get; set; }
    
    [NotMapped]
    public long DescriptionLength => Description?.Length ?? 0;
}