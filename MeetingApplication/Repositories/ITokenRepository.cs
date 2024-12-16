namespace MeetingApplicationAPI.Repositories;

using Microsoft.AspNetCore.Identity;


public interface ITokenRepository
{
    string CreateJWTToken(IdentityUser users, List<string> roles);
}