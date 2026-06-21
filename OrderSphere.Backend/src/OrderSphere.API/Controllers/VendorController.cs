using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSphere.Application.Features.Vendor.Commands.ApplyVendor;
using OrderSphere.Application.Features.Vendor.Commands.ApproveVendor;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Domain.Constants;

namespace OrderSphere.API.Controllers;

[ApiController]
[Route("api/vendor")]
public class VendorController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;

    public VendorController(IMediator mediator, ICurrentUser currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;

    }

    [Authorize]
    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyVendorCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("approve")]
    public async Task<IActionResult> Approve(ApproveVendorCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}