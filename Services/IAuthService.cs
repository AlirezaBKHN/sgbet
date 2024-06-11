using System.IdentityModel.Tokens.Jwt;
using sgbet.Dtos;

namespace sgbet.Services;

public interface IAuthService
{
    Task<string> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
}