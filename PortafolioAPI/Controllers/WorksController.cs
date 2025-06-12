using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(works);
        }

        [HttpGet("Work/{id}")]
        public async Task<IActionResult> GetWork(int id)
        {
            var work = workRepository.GetWork(id);
            if (work != null)
                return Ok(work);
            else
                return BadRequest();
        }



        #region Admin
        [Authorize(Roles = "admin")]
        [HttpGet("MyWorks")]
        public async Task<IActionResult> GetMyWorks()
        {
            return Ok();
        }
        [Authorize(Roles ="admin")]
        [HttpGet("MyWork/{id}")]
        public async Task<IActionResult> GetMyWork(int id)
        {
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("AddWork")]
        public async Task<IActionResult> AddWork(int id)
        {
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("UpdateWork")]
        public async Task<IActionResult> UpdateWork(int id)
        {
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteWork")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            return Ok();
        }

        #endregion
    }
}
