using FluentValidation;

namespace CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;

public sealed class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
           .NotEmpty().WithMessage("kullanıcı id boş olamaz.")
           .NotNull().WithMessage("kullanıcı id boş olamaz.");

        RuleFor(x => x.RoleId)
           .NotEmpty().WithMessage("Rol id boş olamaz.")
           .NotNull().WithMessage("Rol id boş olamaz.");
    }
}
