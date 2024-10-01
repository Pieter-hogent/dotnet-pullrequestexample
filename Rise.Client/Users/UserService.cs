using System;
using System.Net.Http.Json;
using Rise.Shared.Users;

namespace Rise.Client.Users;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task<IEnumerable<UserDto.Index>> GetUsersAsync()
    {
        var users = await httpClient.GetFromJsonAsync<IEnumerable<UserDto.Index>>("user");
        return users!;
    }
}
