using System.ComponentModel.DataAnnotations.Schema;

[Table("Shares")]
internal sealed class Share 
{
    // Конструктори.
    public Share() { }

    // Властивості.
    public int ShareID { get; set; }
    public DateTime ShareStartDate { get; set; }
    public DateTime ShareFinishDate { get; set; }
    public List<Country> Countries { get; set; } = new List<Country>();
    public Good Good { get; set; }
}
