namespace Acacia.Data.Entities
{
    public class OilIngredient
    {
        public int OilId { get; set; }
        public Oil Oil { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public decimal Percentage { get; set; }
    }

}
