using MediatR;
using OrderSphere.Application.Common.Models;

namespace OrderSphere.Application.Features.Vendor.Commands.ApplyVendor;

public sealed record ApplyVendorCommand(
    string BusinessName,
    string? Description
) : IRequest<Result<Guid>>;