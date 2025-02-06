using FluentValidation;

namespace SoftwareCatalog.Api.Techs;

public record TechCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Phone).NotEmpty().Unless(v => v.Email != null && v.Email != "");
        RuleFor(v => v.Email).NotEmpty().Unless(v => v.Phone != null && v.Phone != "");
    }
}

public record TechDetailsResponseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
