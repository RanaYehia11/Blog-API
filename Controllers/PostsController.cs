using BlogsAPI.Services;
using BlogsAPI.Models;
using BlogsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;




namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]



    public class PostsController : ControllerBase
    {
        public readonly IPostsService _postsService;
        // Constructor injection for IPostsService
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }
        // GET: api/posts
        [HttpGet]
        public IActionResult GetAllPosts()
        {
            try
            {
                var posts = _postsService.GetAllPosts();
                var result = new Models.ResultViewModel<IEnumerable<Post>>()
                {
                    IsSuccess = true,
                    Message = "Posts retrieved successfully",
                    Data = posts.Result
                };
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                var result = new Models.ResultViewModel<IEnumerable<Post>>()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, result);
            }

        }
        // GET: api/posts/{id}
        [HttpGet("{id}")]

        public IActionResult GetPostById(int id)
        {
            try
            {
                var post = _postsService.GetPost(id);
                if (post.Result == null)
                {
                    var notFoundResult = new Models.ResultViewModel<Post>()
                    {
                        IsSuccess = false,
                        Message = "Post not found",
                        Data = null
                    };
                    return NotFound(notFoundResult);
                }
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = true,
                    Message = "Post retrieved successfully",
                    Data = post.Result
                };
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, result);
            }
        }

        // GET: api/posts/{postId}/comments
        [HttpGet("{postId}/comments")]

        public IActionResult GetCommentsByPostId(int postId)
        {
            try
            {
                var comments = _postsService.GetCommentsByPostId(postId);
                var result = new Models.ResultViewModel<IEnumerable<Comment>>()
                {
                    IsSuccess = true,
                    Message = "Comments retrieved successfully",
                    Data = comments.Result
                };
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                var result = new BlogsAPI.Models.ResultViewModel<IEnumerable<Comment>>()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, result);
            }

        }

        // POST: api/posts  
        [HttpPost]

        public IActionResult CreatePost([FromBody] AddPostDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    var badRequestResult = new Models.ResultViewModel<Post>()
                    {
                        IsSuccess = false,
                        Message = "Invalid post data",
                        Data = null
                    };
                    return BadRequest(badRequestResult);
                }

                var createdPost = _postsService.AddPost(dto);
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = true,
                    Message = "Post created successfully",
                    Data = createdPost.Result
                };
                return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Result.Id }, result);
            }
            catch (System.Exception ex)
            {
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, result);
            }
        }

        // PUT: api/posts/{id}

        [HttpPut("{id}")]

        public IActionResult UpdatePost(int id, [FromBody] UpdatePostDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    var badRequestResult = new Models.ResultViewModel<Post>()
                    {
                        IsSuccess = false,
                        Message = "Invalid post data",
                        Data = null
                    };
                    return BadRequest(badRequestResult);
                }
                var updatedPost = _postsService.UpdatePost(id, dto);
                if (updatedPost.Result == null)
                {
                    var notFoundResult = new Models.ResultViewModel<Post>()
                    {
                        IsSuccess = false,
                        Message = "Post not found",
                        Data = null
                    };
                    return NotFound(notFoundResult);
                }
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = true,
                    Message = "Post updated successfully",
                    Data = updatedPost.Result
                };
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                var result = new Models.ResultViewModel<Post>()
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, result);
            }

        }


        // DELETE: api/posts/{id}

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest(new ResultViewModel<Post>
                {
                    IsSuccess = false,
                    Message = "Invalid post ID",
                    Data = null
                });
            }
            try
            {
                var isDeleted = _postsService.DeletePost(id);
                if (isDeleted == null)
                {
                    return NotFound(new ResultViewModel<Post>
                    {
                        IsSuccess = false,
                        Message = "Post not found",
                        Data = null
                    });
                }
                return Ok(new ResultViewModel<Post>
                {
                    IsSuccess = true,
                    Message = "Post deleted successfully",
                    Data = null
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<object>
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }





        }
    }
}
