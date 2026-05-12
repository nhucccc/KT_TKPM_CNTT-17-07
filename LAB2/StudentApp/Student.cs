using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentApp
{
    public class Student
    {
        // MongoDB dùng ObjectId làm _id, ánh xạ sang string Id cho dễ dùng
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("Age")]
        public int Age { get; set; }

        // Grade: A, B, C, D, F
        [BsonElement("Grade")]
        public string Grade { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[{Id[^6..]}] {Name} | {Age} tuổi | {Email} | {Address} | Xếp loại: {Grade}";
        }
    }
}
