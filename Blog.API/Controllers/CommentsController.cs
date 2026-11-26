
using Blog.Application.Features.Comments.ApproveComment;
using Blog.Application.Features.Comments.CreateComment;
using Blog.Application.Features.Comments.DeleteComment;
using Blog.Application.Features.Comments.GetApprovedComments;
using Blog.Application.Features.Comments.RejectComment;
using Blog.Application.Features.Comments.UpdateComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(
    CreateCommentHandler createCommentHandler,
    GetApprovedCommentsHandler getApprovedCommentsHandler,
    ApproveCommentHandler approveCommentHandler,
    RejectCommentHandler rejectCommentHandler,
    UpdateCommentHandler updateCommentHandler,
    DeleteCommentHandler deleteCommentHandler) : ControllerBase
{
    // Create comment
    [HttpPost]
    [Authorize(Policy = "CanReadPosts")]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var commentId = await createCommentHandler.HandleAsync(request, userId, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            id = commentId
        });
    }

    // Get approved comments by post
    [HttpGet("post/{postId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetApprovedByPost(Guid postId, CancellationToken cancellationToken)
    {
        var comments = await getApprovedCommentsHandler.HandleAsync(postId, cancellationToken);

        return Ok(comments);
    }

    // Update comment
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CanReadPosts")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var isAdmin = User.IsInRole("Admin");

        await updateCommentHandler.HandleAsync(id, request, userId, isAdmin, cancellationToken);

        return NoContent();
    }

    // Delete comment
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CanReadPosts")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var isAdmin = User.IsInRole("Admin");

        await deleteCommentHandler.HandleAsync(id, userId, isAdmin, cancellationToken);

        return NoContent();
    }

    // Approve comment
    [HttpPost("{id:guid}/approve")]
    [Authorize(Policy = "CanManageUsers")]
    public async Task<IActionResult> Approve(Guid id, CancellationToken cancellationToken)
    {
        await approveCommentHandler.HandleAsync(id, cancellationToken);

        return NoContent();
    }

    // Reject comment
    [HttpPost("{id:guid}/reject")]
    [Authorize(Policy = "CanManageUsers")]
    public async Task<IActionResult> Reject(Guid id, CancellationToken cancellationToken)
    {
        await rejectCommentHandler.HandleAsync(id, cancellationToken);

        return NoContent();
    }
}
