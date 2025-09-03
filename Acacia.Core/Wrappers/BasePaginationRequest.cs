namespace Acacia.Core.Wrappers
{
    public class BasePaginationRequest
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;

        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public Dictionary<string, string> Filters { get; set; } = new();
    }
}
