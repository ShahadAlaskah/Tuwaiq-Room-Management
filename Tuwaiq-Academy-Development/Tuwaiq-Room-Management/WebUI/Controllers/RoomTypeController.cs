using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.Core.Commands;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypesApi _roomTypeApi;

        public RoomTypeController(IRoomTypesApi roomTypeApi)
        {
            _roomTypeApi = roomTypeApi;
        }


        [ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int? page = 1, int? size = 10, string? query = null)
        {
            var roomType = await _roomTypeApi.Get(page: page, pageSize: size, query: query);

            return Ok(new TabulatorViewModel()
            {
                Data = roomType.Content!.Data,
                Last_page = roomType.Content!.Pagination.TotalPages
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var roomType = await _roomTypeApi.GetById(id);

            return Ok(roomType);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand model)
        {
            var roomType = await _roomTypeApi.Create(model);
            return Ok(roomType);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoomType(UpdateRoomTypeCommand model)
        {
            var roomType = await _roomTypeApi.Update(model);
            return Ok(roomType);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoomType(Guid id)
        {
            var deleteRoomType = await _roomTypeApi.Delete(id);

            return Ok(deleteRoomType);
        }
    }
}