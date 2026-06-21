using MediatR;
using OrderSphere.Application.Common.Models;

namespace OrderSphere.Application.Features.Vendor.Commands.ApproveVendor;

public sealed record ApproveVendorCommand(Guid ApplicationId) : IRequest<Result>;