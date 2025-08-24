using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsAPI.Models
{
    //DTOs for creating and updating comments
    public record AddCommentDTO(string Content, int AuthorId, int PostId);
    public record UpdateCommentDTO(int Id, string Content);
    public class Comment
    {
        public int Id { get; set; }      
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public Post Post { get; set; }
        public Author Author { get; set; }

        public Comment()
        {
        }
        // Constructor to create a Comment from AddCommentDTO
        public Comment(AddCommentDTO comment)
        {
            Content = comment.Content;
            AuthorId = comment.AuthorId;
            PostId = comment.PostId;
            CreatedDate = DateTime.Now; 
        }
    }
}
