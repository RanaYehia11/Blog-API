using BlogsAPI.Data;
using BlogsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlogsAPI.Services
{
    public interface ICommentsService
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentById(int id);
        Comment AddComment(AddCommentDTO commentDto);
        Comment UpdateComment(int id, UpdateCommentDTO commentDto);
        Comment? DeleteComment(int id);
        IEnumerable<Comment> GetCommentsByPostId(int postId);
        IEnumerable<Comment> GetCommentsByAuthorId(int authorId);
    }

    public class CommentService : ICommentsService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public Comment GetCommentById(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public Comment AddComment(AddCommentDTO commentDto)
        {
            var comment = new Comment(commentDto);
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment UpdateComment(int id, UpdateCommentDTO commentDto)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return null;

            comment.Content = commentDto.Content;
            _context.SaveChanges();
            return comment;
        }

        public Comment? DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
            return comment;
        }

        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public IEnumerable<Comment> GetCommentsByAuthorId(int authorId)
        {
            return _context.Comments.Where(c => c.AuthorId == authorId).ToList();
        }
    }
}
