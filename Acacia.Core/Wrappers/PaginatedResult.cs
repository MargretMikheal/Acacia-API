namespace Acacia.Core.Wrappers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult()
        {
        }

        public PaginatedResult(List<T> data, int totalCount, int currentPage, int pageSize)
        {
            Items = new PaginationData<T>
            {
                Data = data,
                Links = new()
                {
                    Prev = currentPage > 1 ? $"?page={currentPage - 1}" : null,
                    Next = currentPage * pageSize < totalCount ? $"?page={currentPage + 1}" : null
                }
            };

            Meta = new PaginationMeta
            {
                CurrentPage = currentPage,
                From = ((currentPage - 1) * pageSize) + 1,
                LastPage = (int)Math.Ceiling(totalCount / (double)pageSize),
                Links = GeneratePaginationLinks(currentPage, totalCount, pageSize),
                Path = "",
                PerPage = pageSize,
                To = Math.Min(currentPage * pageSize, totalCount),
                Total = totalCount
            };
        }

        public PaginationData<T> Items { get; set; }
        public PaginationMeta Meta { get; set; }

        private List<PaginationLink> GeneratePaginationLinks(int currentPage, int totalCount, int pageSize)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var links = new List<PaginationLink>();

            links.Add(new PaginationLink
            {
                Url = currentPage > 1 ? $"?page={currentPage - 1}" : null,
                Label = "&laquo; Previous",
                Active = false
            });

            for (int i = 1; i <= totalPages; i++)
            {
                links.Add(new PaginationLink
                {
                    Url = $"?page={i}",
                    Label = i.ToString(),
                    Active = i == currentPage
                });
            }

            links.Add(new PaginationLink
            {
                Url = currentPage < totalPages ? $"?page={currentPage + 1}" : null,
                Label = "Next &raquo;",
                Active = false
            });

            return links;
        }
    }

    public class PaginationData<T>
    {
        public List<T> Data { get; set; }
        public PaginationSimpleLinks Links { get; set; }
    }

    public class PaginationMeta
    {
        public int CurrentPage { get; set; }
        public int From { get; set; }
        public int LastPage { get; set; }
        public List<PaginationLink> Links { get; set; }
        public string Path { get; set; }
        public int PerPage { get; set; }
        public int To { get; set; }
        public int Total { get; set; }
    }

    public class PaginationLink
    {
        public string Url { get; set; }
        public string Label { get; set; }
        public bool Active { get; set; }
    }
    public class PaginationSimpleLinks
    {
        public string Prev { get; set; }
        public string Next { get; set; }
    }

}
