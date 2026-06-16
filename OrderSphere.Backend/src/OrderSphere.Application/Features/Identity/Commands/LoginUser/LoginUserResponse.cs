namespace OrderSphere.Application.Features.Identity.Commands.LoginUser;

public sealed record LoginUserResponse(string AccessToken, string RefreshToken);
    