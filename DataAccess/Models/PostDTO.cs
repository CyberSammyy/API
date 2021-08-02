using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public PostDTO() { }
        public PostDTO(Guid id, string topic, string body, string author)
        {
            Id = id;
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
