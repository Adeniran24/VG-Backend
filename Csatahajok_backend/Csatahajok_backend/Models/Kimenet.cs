namespace Csatahajok_backend.Models;

public class Kimenet
{
    public string Hajo { get; set; } = string.Empty;

    public string Csata { get; set; } = string.Empty;

    public string Eredmeny { get; set; } = string.Empty;

    public Hajo? HajoNavigation { get; set; }

    public Csata? CsataNavigation { get; set; }
}
