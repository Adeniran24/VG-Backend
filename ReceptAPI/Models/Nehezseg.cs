using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Nehezseg
{
    public int Id { get; set; }

    public string Szint { get; set; } = null!;

    public string? Leiras { get; set; }

    public virtual ICollection<Recept>? Recepts { get; set; }
}
