using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class GettingATech
{
    public static async Task<Results<Ok<TechDetailsResponseModel>, NotFound>> GetTechAsync(Guid id, IDocumentSession session)
    {
        var response = await session.Query<TechEntity>()
            .Where(v => v.Id == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(response)
        };
    }

    public static async Task<Ok<IReadOnlyList<TechDetailsResponseModel>>> GetTechsAsync(IDocumentSession session)
    {
        var response = await session.Query<TechEntity>().ProjectToModel().ToListAsync();
        return TypedResults.Ok(response);
    }
}
