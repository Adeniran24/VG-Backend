using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Csatahajok_backend.Models;

public class Hajo
{
    [Key]
    public string Nev { get; set; } = string.Empty;

    public string Osztaly { get; set; } = string.Empty;

    public int Felavatva { get; set; }

    public int AgyukSzama { get; set; }

    public int Kaliber { get; set; }

    public int Vizkiszoritas { get; set; }

    [JsonIgnore]
    public ICollection<Kimenet> Kimenetek { get; set; } = new List<Kimenet>();
}
