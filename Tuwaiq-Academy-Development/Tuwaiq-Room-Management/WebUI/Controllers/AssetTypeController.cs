using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.Core.Commands;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        private readonly IAssetTypesApi _assetTypeApi;

        public AssetTypeController(IAssetTypesApi assetTypeApi)
        {
            _assetTypeApi = assetTypeApi;
        }


        [ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int? page = 1, int? size = 10, string? query = null)
        {
            var assetType = await _assetTypeApi.Get(page: page, pageSize: size, query: query);

            return Ok(new TabulatorViewModel()
            {
                Data = assetType.Content.Data!,
                Last_page = assetType.Content!.Pagination.TotalPages
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var assetType = await _assetTypeApi.GetById(id);

            return Ok(assetType);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAssetType(CreateAssetTypeCommand model)
        {
            var assetType = await _assetTypeApi.Create(model);
            return Ok(assetType);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAssetType(UpdateAssetTypeCommand model)
        {
            var assetType = await _assetTypeApi.Update(model);
            return Ok(assetType);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAssetType(Guid id)
        {
            var deleteAssetType = await _assetTypeApi.Delete(id);

            return Ok(deleteAssetType);
        }
    }
}