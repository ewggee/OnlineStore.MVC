namespace OnlineStore.Contracts.Common
{
    /// <summary>
    /// Пагинированный запрос.
    /// </summary>
    public class PagedRequest
    {
        /// <summary>
        /// Количество на странице.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
