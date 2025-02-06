using Riok.Mapperly.Abstractions;
using SoftwareCatalog.Api.Techs;

[Mapper]
public static partial class TechMappers
{
    public static partial IQueryable<TechDetailsResponseModel> ProjectToModel(this IQueryable<TechEntity> entity);

    //[MapProperty(nameof(VendorEntity.Slug), nameof(VendorDetailsResponseModel.Id))]
    //[MapperIgnoreSource(nameof(VendorEntity.Id))]
    public static partial TechDetailsResponseModel MapToModel(this TechEntity entity);
}