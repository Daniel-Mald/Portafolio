using PortafolioAPI.Models;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Models.Entities;

namespace PortafolioAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        ResponseWithMessage<string> CreateToken(Users u);
        ResponseWithMessage CreateUser(AddUserDTO dto);
        Task<ResponseWithMessage<string>> Login(LoginDTO dto);
    }
}