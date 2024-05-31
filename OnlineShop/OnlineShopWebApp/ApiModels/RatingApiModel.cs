namespace OnlineShopWebApp.ApiModels
{
    public class RatingApiModel
    {
        public Guid ProductId { get; set; }
        public double AverageGrade { get; set; }
        public int ReviewsCount { get; set; }
    }
}
