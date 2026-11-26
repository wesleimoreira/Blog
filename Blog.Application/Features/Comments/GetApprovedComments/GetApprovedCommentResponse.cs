namespace Blog.Application.Features.Comments.GetApprovedComments
{
    public sealed record GetApprovedCommentResponse(
     Guid Id,
     string Content,
     string UserName,
     DateTime CreatedAt);
}
