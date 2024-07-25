using MongoDB.Bson;
using MongoDB.Driver;
using ReviewService.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Common.Data
{
    public class ReviewContextSeed
    {
        public static void SeedData(IMongoCollection<Review> reviewCollection)
        {
            bool existReviews = reviewCollection.Find(p => true).Any();
            if (!existReviews)
            {
                reviewCollection.InsertManyAsync(GetPreconfiguredReviews());
            }
        }

        private static IEnumerable<Review> GetPreconfiguredReviews()
        {
            return new List<Review>()
            {
                new Review()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TrainerName = "John Doe",
                    Comment = "Great strength training session!",
                    Rating = 8
                },
                new Review()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TrainerName = "John Doe",
                    Comment = "Loved the yoga class, very relaxing.",
                    Rating = 8
                },
                new Review()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TrainerName = "John Doe",
                    Comment = "High intensity workout, perfect for burning calories.",
                    Rating = 10
                },
                new Review()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TrainerName = "Jane Smith",
                    Comment = "Good session, but could use more variety.",
                    Rating = 6
                },
                new Review()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TrainerName = "Mike Johnson",
                    Comment = "Very knowledgeable instructor.",
                    Rating = 9
                }
            };
        }
    }
}
