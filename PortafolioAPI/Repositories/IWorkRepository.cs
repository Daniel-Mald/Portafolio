using PortafolioAPI.Models;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Models.Entities;

namespace PortafolioAPI.Repositories
{
    public interface IWorkRepository
    {
        ResponseWithMessage AddWork(AddWorkDTOForRepos dto);
        ResponseWithMessage DeleteWork(int idUser, int idWork);
        ResponseWithMessage<IEnumerable<Work>> GetMyWorks(int idUser);
        ResponseWithMessage<Work> GetWork(int id);
        ResponseWithMessage UpdateWork(UpdateWorkDTOforRepos dto);
        bool UserExist(int id);
        IEnumerable<Work> GetWorks();
    }
}