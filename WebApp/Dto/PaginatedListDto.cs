using Microsoft.EntityFrameworkCore;

namespace WebApp.Dto
{
    public class PaginatedListDto<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        
    }
}
