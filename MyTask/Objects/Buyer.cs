using System.ComponentModel.DataAnnotations.Schema;

[Table("Buyers")]
internal sealed class Buyer
{
    // Конструктори.
    public Buyer() { }

    // Властивості.
    public int BuyerID { get; set; }
    public string FullName { get; set; }
    public string Sex { get; set; }
    public string Email { get; set; }
    public int City_ID { get; set; }
    public City City { get; set; }
    public Country Country { get; set; }
    public int Count_Buyers { get; set; }
    public List<Section> Sections { get; set; } = new List<Section>();
    public List<Good> Goods { get; set; } = new List<Good>();
}