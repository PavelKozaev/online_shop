using OnlineShopWebApp.ReviewMicroservice.ApiModels;

namespace OnlineShopWebApp.ReviewMicroservice.ApiClients
{
    public class ReviewsApiClient : IReviewsApiClient
    {
        private readonly HttpClient httpClient;

        public ReviewsApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:7274/");
        }

        // Получение списка отзывов по продукту
        public async Task<List<ReviewApiModel>> GetByProductIdAsync(Guid productId)
        {
            var response = await httpClient.GetAsync($"reviews/Product/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ReviewApiModel>>();
        }

        // Удаление отзыва
        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            var response = await httpClient.DeleteAsync($"reviews/{reviewId}");
            return response.IsSuccessStatusCode;
        }

        // Получение рейтинга продукта
        public async Task<RatingApiModel> GetRatingByProductIdAsync(Guid productId)
        {
            var response = await httpClient.GetAsync($"reviews/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RatingApiModel>();
        }

        // Создание нового отзыва
        public async Task<ReviewApiModel> CreateAsync(Guid productId, Guid userId, string text, int grade)
        {
            var newReview = new
            {
                productId,
                userId,
                text,
                grade
            };

            var response = await httpClient.PostAsJsonAsync("reviews", newReview);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReviewApiModel>();
        }
    }
}
