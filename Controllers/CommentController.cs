using BlogsAPI.Services;
using BlogsAPI.Models;
using BlogsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentsService _commentService;

    public CommentController(ICommentsService commentService)
    {
        _commentService = commentService;
    }

    // Get all comments
    [HttpGet]
    public IActionResult GetAllComments()
    {
        try
        {
            var comments = _commentService.GetAllComments();
            var result = new ResultViewModel<IEnumerable<Comment>>
            {
                IsSuccess = true,
                Message = "Comments retrieved successfully",
                Data = comments
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<IEnumerable<Comment>>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving comments: {ex.Message}"
            });
        }
    }

    // Get comment by ID
    [HttpGet("{id}")]
    public IActionResult GetCommentById(int id)
    {
        try
        {
            var comment = _commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound(new ResultViewModel<Comment>
                {
                    IsSuccess = false,
                    Message = "Comment not found"
                });
            }

            var result = new ResultViewModel<Comment>
            {
                IsSuccess = true,
                Message = "Comment retrieved successfully",
                Data = comment
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<Comment>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving the comment: {ex.Message}"
            });
        }
    }

    // Add a new comment
    [HttpPost]
    public IActionResult AddComment([FromBody] AddCommentDTO commentDto)
    {
        try
        {
            var comment = _commentService.AddComment(commentDto);
            var result = new ResultViewModel<Comment>
            {
                IsSuccess = true,
                Message = "Comment added successfully",
                Data = comment
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<Comment>
            {
                IsSuccess = false,
                Message = $"An error occurred while adding the comment: {ex.Message}",
                Data = null
            });
        }
    }

    // Update an existing comment
    [HttpPut("{id}")]
    public IActionResult UpdateComment(int id, [FromBody] UpdateCommentDTO commentDto)
    {
        try
        {
            var comment = _commentService.UpdateComment(id, commentDto);
            if (comment == null)
            {
                return NotFound(new ResultViewModel<Comment>
                {
                    IsSuccess = false,
                    Message = "Comment not found"
                });
            }

            var result = new ResultViewModel<Comment>
            {
                IsSuccess = true,
                Message = "Comment updated successfully",
                Data = comment
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<Comment>
            {
                IsSuccess = false,
                Message = $"An error occurred while updating the comment: {ex.Message}",
                Data = null
            });
        }
    }


    // GET api/comments/post/{postId}
    [HttpGet("post/{postId}")]
    public IActionResult GetCommentsByPost(int postId)
    {
        try
        {
            var comments = _commentService.GetCommentsByPostId(postId);
            var result = new ResultViewModel<IEnumerable<Comment>>
            {
                IsSuccess = true,
                Message = "Comments retrieved successfully",
                Data = comments
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<IEnumerable<Comment>>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving comments: {ex.Message}"
            });
        }
    }

    // Delete a comment
    [HttpDelete("{id}")]
    public IActionResult DeleteComment(int id)
    {
        try
        {
            var comment = _commentService.DeleteComment(id);
            if (comment == null)
            {
                return NotFound(new ResultViewModel<Comment>
                {
                    IsSuccess = false,
                    Message = "Comment not found"
                });
            }

            var result = new ResultViewModel<Comment>
            {
                IsSuccess = true,
                Message = "Comment deleted successfully",
                Data = comment
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<Comment>
            {
                IsSuccess = false,
                Message = $"An error occurred while deleting the comment: {ex.Message}",
                Data = null
            });
        }
    }
}
