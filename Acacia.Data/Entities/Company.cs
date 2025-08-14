namespace Acacia.Data.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<Oil> Oils { get; set; }
}
