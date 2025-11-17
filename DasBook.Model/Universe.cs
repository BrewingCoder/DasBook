using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DasBook.Model;

public class Universe : Entity
{
    [Column(TypeName = "varchar(Max)")]
    [MaxLength]
    public string? Description { get; set; }
}