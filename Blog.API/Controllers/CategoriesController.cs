using Blog.Application.Features.Categories.CreateCategory;
using Blog.Application.Features.Categories.DeleteCategory;
using Blog.Application.Features.Categories.GetCategories;
using Blog.Application.Features.Categories.GetCategoryById;
using Blog.Application.Features.Categories.UpdateCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(CreateCategoryHandler createHandler, GetCategoriesHandler getAllHandler, GetCategoryByIdHandler getByIdHandler, UpdateCategoryHandler updateHandler, DeleteCategoryHandler deleteHandler) : ControllerBase
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
        [Authorize(Policy = "CanManageCategories")]
        public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var id = await createHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "CanManageCategories")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            await updateHandler.HandleAsync(id, request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "CanManageCategories")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await deleteHandler.HandleAsync(id, cancellationToken);
            return NoContent();
        }
    }
}