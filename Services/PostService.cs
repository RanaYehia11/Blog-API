using BlogsAPI.Data;
using BlogsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsAPI.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPost(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<Post> AddPost(AddPostDTO post);
        Task<Post?> UpdatePost(int id, UpdatePostDTO newPost);
        Task DeletePost(int id);
    }

    public class PostsService : IPostsService
    {
        private readonly AppDbContext _context;

        public PostsService(AppDbContext context)  // Constructor injection for DbContext
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetPost(int id)
        {
            return await _context.Posts
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await _context.Comments
                                 .Where(c => c.PostId == postId)
                                 .ToListAsync();
        }

        public async Task<Post> AddPost(AddPostDTO dto)
        {
            var post = new Post(dto);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> UpdatePost(int id, UpdatePostDTO dto)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return null;
            }

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.UpdatedDate = DateTime.Now; // Update the updated date to the current date

            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
