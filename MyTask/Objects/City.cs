using System.ComponentModel.DataAnnotations.Schema;

[Table("Cities")]
internal sealed class City
{
    // Конструктори.
    public City() { }

    // Властивості.
    public int CityID { get; set; }
    public string CityName { get; set; }
    public Country Country { get; set; }
    public int Count_Cities { get; set; }
    public float AVG_Cities { get; set; }
    public int Count_Buyers { get; set; }
}