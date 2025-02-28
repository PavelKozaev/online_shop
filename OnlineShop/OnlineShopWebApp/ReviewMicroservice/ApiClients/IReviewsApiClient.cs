﻿using OnlineShopWebApp.ReviewMicroservice.ApiModels;

namespace OnlineShopWebApp.ReviewMicroservice.ApiClients
{
    public interface IReviewsApiClient
    {
        Task<List<ReviewApiModel>> GetByProductIdAsync(Guid productId);
        Task<bool> DeleteAsync(Guid reviewId);
        Task<RatingApiModel> GetRatingByProductIdAsync(Guid productId);
        Task<ReviewApiModel> CreateAsync(Guid productId, Guid userId, string text, int grade);
    }
}
