using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class AddingATech
{
    public static async Task<Results<Created<TechDetailsResponseModel>, BadRequest>> CanAddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator,
        [FromServices] IDocumentSession session,
        [FromServices] IHttpContextAccessor _httpContextAccessor)

    {

        //var user = _httpContextAccessor.HttpContext.User; // Don't Do This!!@
        var sub = _httpContextAccessor.HttpContext.User.Identity.Name;

        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest();
        }
        var entity = new TechEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ManagerSub = sub ?? "failed",
            Email = request.Email,
            Phone = request.Phone
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();
        return TypedResults.Created($"/techs/{entity.Id}", response);
    }
}
