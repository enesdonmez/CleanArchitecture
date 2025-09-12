using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands;

internal class CreateNewTokenByRefreshTokenCommandValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User bilgisi boş olamaz.")
            .NotNull().WithMessage("User bilgisi boş olamaz.");

        RuleFor(x => x.RefreshToken)
           .NotEmpty().WithMessage("RefreshToken bilgisi boş olamaz.")
           .NotNull().WithMessage("RefreshToken bilgisi boş olamaz.");
    }
}
