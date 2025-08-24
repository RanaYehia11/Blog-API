using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsAPI.Models
{
    //DTOs for creating and updating posts
    public record AddPostDTO(string Title, string Content, int AuthorId);
    public record UpdatePostDTO(int Id, string Title, string Content);
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int AuthorId { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public Author Author { get; set; }

        public Post()
        {
        }
        // Constructor to create a Post from AddPostDTO 
        public Post(AddPostDTO post)
        {
            Title = post.Title;
            Content = post.Content;
            AuthorId = post.AuthorId;
            CreatedDate = DateTime.Now; // Set the created date to the current date
            UpdatedDate = DateTime.Now; // Set the updated date to the current date
        }


    }
}
