using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortafolioAPI.Helpers;
using PortafolioAPI.Models;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Models.Entities;
using PortafolioAPI.Repositories.Interfaces;

namespace PortafolioAPI.Repositories
{
    public class AuthRepository : Repository<Users>, IAuthRepository
    {
        readonly LabsystePortafolioContext context;
        IConfiguration configuration;
        EncryptClass encrypt;
        public AuthRepository(EncryptClass encrypt, LabsystePortafolioContext context, IConfiguration configuration) : base(context)
        {
            this.context = context;
            this.configuration = configuration;
            this.encrypt = encrypt;
        }

        public async Task<ResponseWithMessage<string>> Login(LoginDTO dto)
        {
            ResponseWithMessage<string> r = new();
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.User == dto.User);

                if (user == null)
                {
                    r.Success = false;
                    r.AddError("User or password are incorrect");
                    return r;
                }
                if (user.Password == encrypt.Encrypt(dto.Password, user.Name))
                {
                    return CreateToken(user);

                }

                r.Success = false;
                r.AddError("User or password are incorrect");
                return r;

            }
            catch (Exception ex)
            {
                r.Success = false;
                r.AddError(ex.Message);
                return r;
            }
        }
        public ResponseWithMessage<string> CreateToken(Users u)
        {
            ResponseWithMessage<string> r = new();
            try
            {


                List<Claim> claims = new();
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]!));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
                claims.Add(new Claim("Id", u.Id.ToString()));

                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                claims.Add(new Claim("Username", u.User));
                claims.Add(new Claim("Name", u.Name));


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var token = new JwtSecurityToken(
                                configuration["Jwt:Issuer"]!,
                                configuration["Jwt:Audience"]!,
                                claims,
                                expires: DateTime.UtcNow.AddHours(1),
                                signingCredentials: signIn);
                string finalToken = new JwtSecurityTokenHandler().WriteToken(token);

                r.Success = true;
                r.Data = finalToken;
                return r;

            }
            catch (Exception x)
            {

                r.Success = false;
                r.AddError(x.Message);
                return r;
            }
        }
        public ResponseWithMessage CreateUser(AddUserDTO dto)
        {
            ResponseWithMessage r = new();
            if (string.IsNullOrWhiteSpace(dto.User) || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Password))
            {
                r.Success = false;
                r.AddError("It was not possible to create a user");
            }
            if (context.Users.Any(x => x.User == dto.User))
            {
                r.Success = false;
                r.AddError("It was not possible to create a user");
            }
            Users u = new()
            {
                Name = dto.Name,
                Password = encrypt.Encrypt(dto.Password, dto.Name)!,
                User = dto.User
            };
            try
            {
                Insert(u);
                r.Success = true;
                r.Message = "The user was created";
                return r;

            }
            catch (Exception ex)
            {
                r.AddError(ex.Message);
                return r;

            }
        }



    }
}
