using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Csatahajok_backend.Models;

public class Csata
{
    [Key]
    public string Nev { get; set; } = string.Empty;

    public DateOnly Kezdes { get; set; }

    public DateOnly Befejezes { get; set; }

    [JsonIgnore]
    public ICollection<Kimenet> Kimenetek { get; set; } = new List<Kimenet>();
}
