namespace MarioAuth.Models
{
    public class NewsViewModel
    {
        public List<News> News { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
