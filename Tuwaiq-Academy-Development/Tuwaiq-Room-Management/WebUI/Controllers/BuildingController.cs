using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.Core.Commands;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingsApi _buildingApi;

        public BuildingController(IBuildingsApi buildingApi)
        {
            _buildingApi = buildingApi;
        }


        //[ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int? page = 1, int? size = 10, string? query = null)
        {
            var building = await _buildingApi.Get(page: page, pageSize: size, query: query);

            return Ok(new TabulatorViewModel()
            {
                Data = building.Content.Data!,
                Last_page = building.Content!.Pagination.TotalPages
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var building = await _buildingApi.GetById(id);

            return Ok(building);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBuilding(CreateBuildingCommand model)
        {
            var building = await _buildingApi.Create(model);
            return Ok(building);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateBuilding(UpdateBuildingCommand model)
        {
            var building = await _buildingApi.Update(model);
            return Ok(building);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            var deleteBuilding = await _buildingApi.Delete(id);

            return Ok(deleteBuilding);
        }
    }
}