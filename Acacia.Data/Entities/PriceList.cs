namespace Acacia.Data.Entities;

public class PriceList : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Oil> Oils { get; set; }
    public ICollection<PriceListItem> PriceListItems { get; set; }
}
