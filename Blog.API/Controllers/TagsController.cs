using Blog.Application.Features.Tags.CreateTag;
using Blog.Application.Features.Tags.DeleteTag;
using Blog.Application.Features.Tags.GetTagById;
using Blog.Application.Features.Tags.GetTags;
using Blog.Application.Features.Tags.UpdateTag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController(CreateTagHandler createHandler, GetTagsHandler getAllHandler, GetTagByIdHandler getByIdHandler, UpdateTagHandler updateHandler, DeleteTagHandler deleteHandler) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await getAllHandler.HandleAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await getByIdHandler.HandleAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "CanManageTags")]
        public async Task<IActionResult> Create(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var id = await createHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "CanManageTags")]
        public async Task<IActionResult> Update(Guid id, UpdateTagRequest request, CancellationToken cancellationToken)
        {
            await updateHandler.HandleAsync(id, request, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "CanManageTags")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await deleteHandler.HandleAsync(id, cancellationToken);

            return NoContent();
        }
    }
}