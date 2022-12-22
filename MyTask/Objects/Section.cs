using System.ComponentModel.DataAnnotations.Schema;

[Table("Sections")]
internal sealed class Section
{
    // Конструктори.
    public Section() { }

    // Властивості.
    public int SectionID { get; set; }
    public string SectionName { get; set; }
}