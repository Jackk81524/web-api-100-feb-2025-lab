using FluentValidation;
using SoftwareCatalog.Api.Techs.Endpoints;

namespace SoftwareCatalog.Api.Techs;

public static class Extensions
{
    public static IServiceCollection AddTechs(this IServiceCollection services)
    {
        //services.AddScoped<VendorSlugGenerator>();
        //services.AddScoped<ICheckForUniqueVendorSlugs, VendorDataService>();

        services.AddScoped<IValidator<TechCreateModel>, TechCreateModelValidator>();
        //services.AddScoped<ICheckForVendorExistenceForCatalog, VendorDataService>();

        services.AddAuthorizationBuilder()
            .AddPolicy("canAddTechs", p =>
            {
                p.RequireRole("manager");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapTechs(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("techs").WithTags("Added Techs").WithDescription("Techs Added by their Manager");

        group.MapPost("/", AddingATech.CanAddTechAsync).RequireAuthorization("canAddTechs");
        group.MapGet("/{id}", GettingATech.GetTechAsync);
        group.MapGet("/", GettingATech.GetTechsAsync);
        return group;

    }
}
