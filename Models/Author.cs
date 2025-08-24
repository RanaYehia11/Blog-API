using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsAPI.Models
{
    public record AddAuthorDTO(string Name, string Email, string Bio);
    public record UpdateAuthorDTO(string Name, string Bio);

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime JoinDate { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Author()
        {
        }
        // Constructor to create an Author from AddAuthorDTO
        public Author(AddAuthorDTO author)
        {
            Name = author.Name;
            Email = author.Email;
            Bio = author.Bio;
            JoinDate = DateTime.Now; // Set the join date to the current date
        }
    }
}


