using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public Post() { }
        public Post(string topic, string body, string author)
        {
            Id = Guid.NewGuid();
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
