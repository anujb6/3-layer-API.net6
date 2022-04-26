using Data_Access_layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "mew")]
    public class IssueController : ControllerBase
    {
      

        private Buisness_Logic_Layer.Managers.IssueBLL _BLL;
        public IssueController(Buisness_Logic_Layer.Managers.IssueBLL BLL)
        {
            _BLL = BLL;
        }

        //Get request
        [HttpGet]
        public async Task<IEnumerable<Issue>> Get() { 
          return  await _BLL.Get(); 
        }

        // Get Issue by ID
        [HttpGet("id")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Issue> GetById(int id)
        {
            return await _BLL.GetById(id);
        }

        //Post request
        [HttpPost]
        public async Task<bool> Post(Issue issue)
        {
            var res = await _BLL.Post(issue);
            return res;
        }
        //Post File
        [HttpPost("file")]
        public async Task<string> FilePost(IFormFile file)
        {
            return await _BLL.FilePost(file);
        }

        //Update data BY ID
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Update(int id, Issue issue)
        {
            return await _BLL.Update(id, issue);
        }

        //Delete data By ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Delete(int id)
        {
            return await _BLL.Delete(id);

        }

    }
}
