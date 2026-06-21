using FluentValidation;

namespace OrderSphere.Application.Features.Vendor.Commands.ApplyVendor;

public sealed class ApplyVendorValidator : AbstractValidator<ApplyVendorCommand>
{
    public ApplyVendorValidator()
    {
        RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}