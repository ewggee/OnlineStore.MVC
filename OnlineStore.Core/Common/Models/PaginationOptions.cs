using Microsoft.Extensions.Configuration;

namespace OnlineStore.Core.Common.Models
{
    public class PaginationOptions
    {
        [ConfigurationKeyName("PageSize")]
        public int PageSize { get; set; }
    }
}
