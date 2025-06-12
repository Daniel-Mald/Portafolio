using Microsoft.EntityFrameworkCore;
using PortafolioAPI.Models;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Models.Entities;
using PortafolioAPI.Repositories.Interfaces;

namespace PortafolioAPI.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        readonly LabsystePortafolioContext context;
        public WorkRepository(LabsystePortafolioContext context) : base(context)
        {
            this.context = context;
        }
        public bool UserExist(int id)
        {
            return context.Users.Any(x => x.Id == id);
        }
        public IEnumerable<Work> GetWorks()
        {
            return GetAll();
        }
        public ResponseWithMessage<Work> AddWork(AddWorkDTOForRepos dto)
        {
            ResponseWithMessage<Work> r = new();
            try
            {
                if (!UserExist(dto.IdUser))
                {
                    r.AddError("User doesn't exist");
                }
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    r.AddError("No name");
                }
                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    r.AddError("No description");
                }

                if (r.ErrorsExist())
                {
                    r.Success = false;

                }
                else
                {
                    var w = context.Add(new Work()
                    {
                        IdUser = dto.IdUser,
                        Description = dto.Description,
                        Name = dto.Name
                    });
                    context.SaveChanges();
                    r.Data = w.Entity;
                    r.Success = true;
                }
                return r;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.AddError(ex.Message);
                return r;
            }

        }
        public ResponseWithMessage UpdateWork(UpdateWorkDTOforRepos dto)
        {
            Work? w = Get(dto.IdWork);
            ResponseWithMessage r = new();
            try
            {
                if (w == null)
                {
                    r.Success = false;
                    r.AddError("The work doesn't exist");
                    return r;
                }
                if (!UserExist(dto.IdUser))
                {
                    r.AddError("User doesn't exist");
                }
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    r.AddError("No name");
                }
                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    r.AddError("No description");
                }

                if (r.ErrorsExist())
                {
                    r.Success = false;

                }
                else
                {
                    w.Description = dto.Description;
                    w.Name = dto.Name;
                    Update(w);
                    r.Success = true;
                }
                return r;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.AddError(ex.Message);
                return r;

            }



        }
        public ResponseWithMessage DeleteWork(int idUser, int idWork)
        {
            Work? w = Get(idWork);
            ResponseWithMessage r = new();
            try
            {
                if (w == null)
                {
                    r.Success = false;
                    r.AddError("The work doesn't exist");
                    return r;
                }
                if (!UserExist(idUser))
                {
                    r.AddError("User doesn't exist");
                }

                if (r.ErrorsExist())
                {
                    r.Success = false;

                }
                else
                {

                    Delete(w);
                    r.Success = true;
                }
                return r;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.AddError(ex.Message);
                return r;
            }


        }
        public ResponseWithMessage<IEnumerable<Work>> GetMyWorks(int idUser)
        {
            return new ResponseWithMessage<IEnumerable<Work>>()
            {
                Data = context.Work.Where(x => x.IdUser == idUser),
                Success = true
            };
        }
        public ResponseWithMessage<Work> GetWork(int id)
        {
            var x = Get(id);

            return new ResponseWithMessage<Work>()
            {
                Data = x,
                Success = x != null
            };
        }
    }
}
