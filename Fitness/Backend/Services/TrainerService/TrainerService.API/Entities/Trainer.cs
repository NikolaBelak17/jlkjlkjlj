using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TrainerService.API.Entities
{
    public class Trainer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Bio { get; set; }
        public List<TrainingType> TrainingTypes { get; set; } = new List<TrainingType>();

        /*public List<Review> Reviews {get; set;}
        public decimal Rating 
        { get
            {
            	if (Reviews==null || !Reviews.Any())
            		return 0;
            		
                int totalRating = 0;
                foreach(var review in Reviews)
                {
                    totalRating+=review.Rating;
                }
                return (decimal) totalRating/Reviews.Count;
                
            } 
        }*/


    }
}
