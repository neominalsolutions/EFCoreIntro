namespace EFCoreIntro.Data.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public short Stock { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

  }
}
