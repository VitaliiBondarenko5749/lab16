using System.ComponentModel.DataAnnotations.Schema;

[Table("Countries")]
internal sealed class Country
{
    // Конструктори.
    public Country() { }

    // Властивості.
    public int CountryID { get; set; }
    public string CountryName { get; set; }
    public int Count_Buyers { get; set; }
}