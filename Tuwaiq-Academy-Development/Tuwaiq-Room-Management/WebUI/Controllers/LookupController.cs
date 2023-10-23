using Microsoft.AspNetCore.Mvc;
using SDK;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupApi _lookupApi;

        public LookupController(ILookupApi lookupapi)
        {
            _lookupApi = lookupapi;
        }


        [ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAssetTypesLookUp()
        {
            var assetTypeCategory = await _lookupApi.GetAssetTypes();
            return Ok(assetTypeCategory.Content);
        }
    }
}