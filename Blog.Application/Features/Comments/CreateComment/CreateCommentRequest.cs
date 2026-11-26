namespace Blog.Application.Features.Comments.CreateComment
{
    public sealed record CreateCommentRequest(string Content, Guid PostId);
}
