using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.Core.Commands;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorsApi _floorApi;

        public FloorController(IFloorsApi floorApi)
        {
            _floorApi = floorApi;
        }


        //[ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int? page = 1, int? size = 10, string? query = null)
        {
            var floor = await _floorApi.Get(page: page, pageSize: size, query: query);

            return Ok(new TabulatorViewModel()
            {
                Data = floor.Content.Data!,
                Last_page = floor.Content!.Pagination.TotalPages
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var floor = await _floorApi.GetById(id);

            return Ok(floor);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateFloor(CreateFloorCommand model)
        {
            var floor = await _floorApi.Create(model);
            return Ok(floor);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateFloor(UpdateFloorCommand model)
        {
            var floor = await _floorApi.Update(model);
            return Ok(floor);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteFloor(Guid id)
        {
            var deleteFloor = await _floorApi.Delete(id);

            return Ok(deleteFloor);
        }
    }
}