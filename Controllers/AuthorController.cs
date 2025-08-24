using BlogsAPI.Services;
using BlogsAPI.Models;
using BlogsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Get all authors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            try
            {
                var authors = _authorService.GetAllAuthors();
                var result = new ResultViewModel<IEnumerable<Author>>
                {
                    IsSuccess = true,
                    Message = "Authors retrieved successfully",
                    Data = authors
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<IEnumerable<Author>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while retrieving authors: {ex.Message}"
                });
            }
        }

        // Get author by ID
        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            try
            {
                var author = _authorService.GetAuthor(id);
                if (author == null)
                {
                    return NotFound(new ResultViewModel<Author>
                    {
                        IsSuccess = false,
                        Message = "Author not found"
                    });
                }

                var result = new ResultViewModel<Author>
                {
                    IsSuccess = true,
                    Message = "Author retrieved successfully",
                    Data = author
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<Author>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while retrieving the author: {ex.Message}"
                });
            }
        }

        // Create a new author
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AddAuthorDTO dto)
        {
            try
            {
                var author = _authorService.AddAuthor(dto);
                var result = new ResultViewModel<Author>
                {
                    IsSuccess = true,
                    Message = "Author created successfully",
                    Data = author
                };
                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<Author>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while creating the author: {ex.Message}"
                });
            }
        }

        // Update an existing author
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorDTO dto)
        {
            try
            {
                var updatedAuthor = _authorService.UpdateAuthor(id, dto);
                if (updatedAuthor == null)
                {
                    return NotFound(new ResultViewModel<Author>
                    {
                        IsSuccess = false,
                        Message = "Author not found"
                    });
                }

                var result = new ResultViewModel<Author>
                {
                    IsSuccess = true,
                    Message = "Author updated successfully",
                    Data = updatedAuthor
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<Author>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while updating the author: {ex.Message}"
                });
            }
        }

        // Get all posts by Author
        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetPostsByAuthor(int id)
        {
            try
            {
                var posts = await _authorService.GetPosts(id);

                if (posts == null || !posts.Any())
                {
                    return NotFound(new ResultViewModel<IEnumerable<Post>>
                    {
                        IsSuccess = false,
                        Message = "No posts found for this author"
                    });
                }

                var result = new ResultViewModel<IEnumerable<Post>>
                {
                    IsSuccess = true,
                    Message = "Posts retrieved successfully",
                    Data = posts
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<IEnumerable<Post>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred while retrieving posts: {ex.Message}"
                });
            }
        }



        // Delete an author
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                var deletedAuthor = _authorService.DeleteAuthor(id);
                if (deletedAuthor != null)
                {
                    var result = new ResultViewModel<Author>
                    {
                        IsSuccess = true,
                        Message = "Author deleted successfully",
                        Data = deletedAuthor
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound(new ResultViewModel<Author>
                    {
                        IsSuccess = false,
                        Message = "Author not found"
                    });
                }
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
