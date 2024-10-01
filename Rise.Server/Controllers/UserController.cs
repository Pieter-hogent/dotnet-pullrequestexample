using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Users;

namespace Rise.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Shared.Auth.Role.Administrator)]
public class UserController : ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;

    public UserController(IManagementApiClient managementApiClient)
    {
        _managementApiClient = managementApiClient;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto.Index>> GetUsers()
    {
        var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
        return users.Select(x => new UserDto.Index
        {
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Blocked = x.Blocked ?? false,
        });
    }
}