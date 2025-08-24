using BlogsAPI.Data;
using BlogsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogsAPI.Services;

public interface IAuthorService
{
    IEnumerable<Author> GetAllAuthors();
    Author? GetAuthor(int id);
    Author AddAuthor(AddAuthorDTO dto);
    Author? UpdateAuthor(int id, UpdateAuthorDTO dto);
    Author? DeleteAuthor(int id);
    Task<IEnumerable<Post>> GetPosts(int authorId);
}

public class AuthorService : IAuthorService
{
    
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)  // Constructor injection for DbContext
    {
        _context = context;  
    }

    public IEnumerable<Author> GetAllAuthors()
    {
        return _context.Authors.ToList();
    }

    public Author? GetAuthor(int id)
    {
        return _context.Authors
                       .Include(a => a.Posts)
                       .Include(a => a.Comments)
                       .FirstOrDefault(a => a.Id == id);
    }

    public Author AddAuthor(AddAuthorDTO dto)
    {
        var author = new Author(dto); 
        _context.Authors.Add(author);
        _context.SaveChanges();
        return author;
    }

    public Author? UpdateAuthor(int id, UpdateAuthorDTO dto)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return null;
        }

        author.Name = dto.Name;
        author.Bio = dto.Bio;
        _context.SaveChanges();
        return author;
    }

    public Author? DeleteAuthor(int id)
    {
        var author = _context.Authors.Find(id);
        if (author == null)
        {
            return null;
        }

        _context.Authors.Remove(author);
        _context.SaveChanges();
        return author;
    }

    public async Task<IEnumerable<Post>> GetPosts(int authorId)
    {
        return await _context.Posts
            .Where(p => p.AuthorId == authorId)
            .ToListAsync();
    }
}
