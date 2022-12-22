using System.ComponentModel.DataAnnotations.Schema;

[Table("Goods")]
internal sealed class Good
{
    // Конструктори.
    public Good() { }

    // Властивості.
    public int GoodID { get; set; }
    public string GoodName { get; set; }
    public int Section_ID { get; set; }
    public int Share_ID { get; set; }
    public Section Section { get; set; }
    public Share Share { get; set; }
}