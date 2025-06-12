using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortafolioAPI.Helpers;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Models.Entities;
using PortafolioAPI.Repositories;

namespace PortafolioAPI.Controllers
{
    public class WorksController : Controller
    {
        IWorkRepository workRepository;
        public WorksController(IWorkRepository workRepository)
        {
            this.workRepository = workRepository;
        }
        [HttpGet("GetWorks")]
        public IActionResult IndexAsync()
        {
            var works =   workRepository.GetWorks();
            List<GetWorkDTO> worksList = new List<GetWorkDTO>();
            worksList = works.Select(x => new GetWorkDTO()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                UrlImage = $"{Request.Scheme}://{Request.Host}/Images/{x.Id}"
            }).ToList();
            return Ok(worksList);
        }

        [HttpGet("Work/{id}")]
        public IActionResult GetWork(int id)
        {
            var work = workRepository.GetWork(id);
            if (work != null)
            {
                return Ok(new GetWorkDTO()
                {
                    Id = work.Data!.Id
                    , Description = work.Data.Description,
                    Name = work.Data.Name,
                    UrlImage = $"{Request.Scheme}://{Request.Host}/Images/{id}"

                });
            }
                
            else
                return BadRequest();
        }
        [ResponseCache(Duration = 604800, Location = ResponseCacheLocation.Client)]
        [HttpGet("Images/{id}")]
        public IActionResult GetImage(int id)
        {
            var imagePath = Path.Combine("Imagenes", $"{id}.jpg");

            if (!System.IO.File.Exists(imagePath))
                return NotFound();

            var imageFileStream = System.IO.File.OpenRead(imagePath);
            return File(imageFileStream, "image/jpeg");
        }


        #region Admin
        [Authorize(Roles = "admin")]
        [HttpGet("MyWorks")]
        public IActionResult GetMyWorks()
        {
            int idUser = FromToken.GetId(User);
            var response = workRepository.GetMyWorks(idUser);
            if (response.Success)
            {
                List<GetWorkDTO> worksList = new List<GetWorkDTO>();
                worksList = response.Data!.Select(x => new GetWorkDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    UrlImage = $"{Request.Scheme}://{Request.Host}/Images/{x.Id}"
                }).ToList();
                return Ok(worksList);
            }
            else
            {
                return BadRequest(response.Errors);
            }

            
        }
        [Authorize(Roles ="admin")]
        [HttpGet("MyWork/{id}")]
        public IActionResult GetMyWork(int id)
        {
            var response = workRepository.GetWork(id);
            if(response.Success)
            {
                return Ok(new GetWorkDTO()
                {
                    Id = response.Data!.Id,
                    Description = response.Data.Description,
                    Name = response.Data.Name,
                    UrlImage = $"{Request.Scheme}://{Request.Host}/Images/{response.Data.Id}"
                });
            }
            else
            {
                return BadRequest(response.Errors);
            }
           
        }
        [Authorize(Roles = "admin")]
        [HttpPost("AddWork")]
        public async Task<IActionResult> AddWork([FromForm]AddWorkDTO dto)
        {
            int id = FromToken.GetId(HttpContext.User as ClaimsPrincipal);


            var response = workRepository.AddWork(new AddWorkDTOForRepos()
            {
                Description = dto.Description,
                Name = dto.Name,
                IdUser = id
            });
            if (response.Success)
            {
                //logica de la imagen
                if (dto.Image != null && dto.Image.Length > 0)
                {
                    var filePath = Path.Combine("Imagenes", $"{response.Data!.Id}.jpg");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(stream);
                    }
                }
            }
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("UpdateWork")]
        public async Task<IActionResult> UpdateWork(UpdateWorkDTO dto)
        {
            int idUser = FromToken.GetId(User);

            var response = workRepository.UpdateWork(new UpdateWorkDTOforRepos()
            {
                Description = dto.Description,
                Name = dto.Name,
                IdUser = idUser,
                IdWork = dto.IdWork
            });
            if (response.Success)
            {
                //editar imagen
                if (dto.Image != null && dto.Image.Length > 0)
                {
                    var filePath = Path.Combine("Imagenes", $"{dto.IdWork}.jpg");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(stream);
                    }
                }
                return Ok();
            }
            return BadRequest(response.Errors);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteWork")]
        public IActionResult DeleteWork(int id)
        {
            var response = workRepository.DeleteWork(FromToken.GetId(User), id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Errors);
        }

        #endregion
    }
}
