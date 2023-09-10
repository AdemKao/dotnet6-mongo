using FirebaseAdmin.Auth;

namespace api.Services;

public class AuthenticationService : IAuthenticationService
{
    public async Task<string> RegisterAsync(string email, string password, string name)
    {
        var userArgs = new UserRecordArgs
        {
            Email = email,
            Password = password,
        };
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        return userRecord.Uid;
    }
}
