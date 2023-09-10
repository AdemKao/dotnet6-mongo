using System.Security.Claims;
using System.Security.Cryptography;
using api.Dtos;
using api.Models;
using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace api.Services;

public class SelfAuthenticationService : ISelfAuthenticationService
{
    private readonly IMongoCollection<User> users;
    private readonly IConfiguration configuration;

    public SelfAuthenticationService(IMongoDbSettings settings, IMongoClient mongoClient, IConfiguration configuration)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        users = database.GetCollection<User>(settings.UserCollectionName);
        this.configuration = configuration;
    }
    public async Task<User> RegisterAsync(UserDto userDto)
    {
        _CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        User user = new User()
        {
            UserName = userDto.UserName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await users.InsertOneAsync(user);
        return user;
    }
    public async Task<string> LoginAsync(UserDto userDto)
    {
        var isVerify = await _VerifyPasswordHash(userDto);
        if (!isVerify)
        {
            return String.Empty;
        }
        return await _CreateToken(userDto);
    }

    public async Task<User> GetAsync(UserDto userDto)
    {
        // In this simple test, we use username is the unique key
        return await _GetAsync(userDto);
    }
    private void _CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private async Task<bool> _VerifyPasswordHash(UserDto userDto)
    {
        var user = await _GetAsync(userDto);
        if (user == null) { return false; }
        using (var hmac = new HMACSHA512(user.PasswordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
            // return computeHash == user.PasswordHash;
            return computeHash.SequenceEqual(user.PasswordHash);
        }
    }
    private async Task<string> _CreateToken(UserDto userDto)
    {
        try
        {
            var user = await _GetAsync(userDto);
            if (user == null) { return String.Empty; }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var _token = configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_token));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return String.Empty;
    }
    private async Task<User> _GetAsync(UserDto userDto) =>
    await users.Find(user => user.UserName == userDto.UserName).FirstOrDefaultAsync();

}
