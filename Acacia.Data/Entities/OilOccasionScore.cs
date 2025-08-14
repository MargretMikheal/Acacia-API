namespace Acacia.Data.Entities
{
    public class OilOccasionScore
    {
        public int Id { get; set; }
        public int OilId { get; set; }
        public Oil Oil { get; set; }

        public decimal? Formal { get; set; }
        public decimal? Casual { get; set; }
        public decimal? Night { get; set; }
        public decimal? Sport { get; set; }
    }
}
