namespace Acacia.Data.Entities
{
    public class OilSeasonScore
    {
        public int Id { get; set; }
        public int OilId { get; set; }
        public Oil Oil { get; set; }

        public decimal? Summer { get; set; }
        public decimal? Winter { get; set; }
        public decimal? Fall { get; set; }
        public decimal? Spring { get; set; }
    }
}
