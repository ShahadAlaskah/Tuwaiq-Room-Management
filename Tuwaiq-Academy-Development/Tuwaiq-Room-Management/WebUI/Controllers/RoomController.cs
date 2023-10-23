using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.Core.Commands;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomsApi _roomApi;

        public RoomController(IRoomsApi roomApi)
        {
            _roomApi = roomApi;
        }


        //[ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int? page = 1, int? size = 10, string? query = null)
        {
            var room = await _roomApi.Get(page: page, pageSize: size, query: query);

            return Ok(new TabulatorViewModel()
            {
                Data = room.Content.Data!,
                Last_page = room.Content!.Pagination.TotalPages
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var room = await _roomApi.GetById(id);

            return Ok(room);
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAssets(Guid id)
        {
            var assets = await _roomApi.GetAssets(id);

            return Ok(assets);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand model)
        {
            var room = await _roomApi.Create(model);
            return Ok(room);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAssetToRoom(AddAssetToRoomCommand model)
        {
            var asset = await _roomApi.AddAssetToRoom(model);
            return Ok(asset);
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand model)
        {
            var room = await _roomApi.Update(model);
            return Ok(room);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var deleteRoom = await _roomApi.Delete(id);

            return Ok(deleteRoom);
        }
    }
}