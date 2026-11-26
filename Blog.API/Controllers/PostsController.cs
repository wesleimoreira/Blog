using Blog.Application.Features.Posts.CreatePost;
using Blog.Application.Features.Posts.DeletePost;
using Blog.Application.Features.Posts.GetPostById;
using Blog.Application.Features.Posts.GetPublishedPosts;
using Blog.Application.Features.Posts.PublishPost;
using Blog.Application.Features.Posts.UnpublishPost;
using Blog.Application.Features.Posts.UpdatePost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(
        GetPostByIdHandler getPostByIdHandler,
        CreatePostHandler createPostHandler,
        UpdatePostHandler updatePostHandler,
        DeletePostHandler deletePostHandler,
        PublishPostHandler publishPostHandler,
        UnpublishPostHandler unpublishPostHandler,
        GetPublishedPostsHandler getPublishedPostsHandler) : ControllerBase
    {
        private readonly GetPostByIdHandler _getPostByIdHandler = getPostByIdHandler;
        private readonly CreatePostHandler _createPostHandler = createPostHandler;
        private readonly UpdatePostHandler _updatePostHandler = updatePostHandler;
        private readonly DeletePostHandler _deletePostHandler = deletePostHandler;
        private readonly PublishPostHandler _publishPostHandler = publishPostHandler;
        private readonly UnpublishPostHandler _unpublishPostHandler = unpublishPostHandler;
        private readonly GetPublishedPostsHandler _getPublishedPostsHandler = getPublishedPostsHandler;


        [Authorize(Policy = "CanManagePosts")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            var post = await _getPostByIdHandler.HandleAsync(id, currentUserId, isAdmin, cancellationToken);

            return Ok(post);
        }


        [Authorize(Policy = "CanManagePosts")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var authorId))
                return Unauthorized();

            var postId = await _createPostHandler.HandleAsync(request, authorId, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = postId }, new { id = postId });
        }

        [Authorize(Policy = "CanManagePosts")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            await _updatePostHandler.HandleAsync(
                id,
                request,
                currentUserId,
                isAdmin,
                cancellationToken);

            return NoContent();
        }

        [Authorize(Policy = "CanManagePosts")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            await _deletePostHandler.HandleAsync(
                id,
                currentUserId,
                isAdmin,
                cancellationToken);

            return NoContent();
        }

        [Authorize(Policy = "CanManagePosts")]
        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            await _publishPostHandler.HandleAsync(
                id,
                currentUserId,
                isAdmin,
                cancellationToken);

            return NoContent();
        }

        [Authorize(Policy = "CanManagePosts")]
        [HttpPost("{id:guid}/unpublish")]
        public async Task<IActionResult> Unpublish(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            await _unpublishPostHandler.HandleAsync(
                id,
                currentUserId,
                isAdmin,
                cancellationToken);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPublished([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? categoryId = null, [FromQuery] Guid? tagId = null, [FromQuery] string? search = null, CancellationToken cancellationToken = default)
        {
            if (page < 1)
                return BadRequest("Page must be greater than zero.");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100.");

            var result = await _getPublishedPostsHandler.HandleAsync(
                page,
                pageSize,
                categoryId,
                tagId,
                search,
                cancellationToken);

            return Ok(result);
        }
    }
}
