using System;

namespace Domain.Models
{
    public class BlogPost
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Category Category { get; set; }
        public DateTime PostDate { get; set; }
    }
}