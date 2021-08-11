using System;

namespace DataAccess.Models
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime PostDate { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }

        public PostDTO() { }

        public PostDTO(Guid id, Guid authorId, DateTime postDate, string topic, string body, string author)
        {
            Id = id;
            AuthorId = authorId;
            PostDate = postDate;
            Topic = topic;
            Body = body;
            Author = author;
        }

        public override string ToString()
        {
            return $"{PostDate} - {Topic}. \r\n {Body} \r\n Created by - {Author}";
        }
    }
}
