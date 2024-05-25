using Microsoft.AspNetCore.Mvc;
using OrderLy_API.Models;
using OrderLy_API.Services;

namespace OrderLy_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {
        private readonly VendorService _vendorService;
        public VendorController(VendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<List<Vendor>> Get()
        {
            return await _vendorService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vendor>> Get(string id)
        {
            var vendor = await _vendorService.GetAsync(id);

            if (vendor is null)
            {
                return NotFound();
            }

            return vendor;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Vendor newVendor)
        {
            await _vendorService.CreateAsync(newVendor);

            return CreatedAtAction(nameof(Get), new { id = newVendor.Id }, newVendor);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Vendor updatedVendor)
        {
            var vendor = await _vendorService.GetAsync(id);

            if (vendor is null)
            {
                return NotFound();
            }

            updatedVendor.Id = vendor.Id;

            await _vendorService.UpdateAsync(id, updatedVendor);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var vendor = await _vendorService.GetAsync(id);
            if (vendor is null)
            {
                return NotFound();
            }
            await _vendorService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("/api/Vendor/Count")]
        public async Task<int> GetVendorCount()
        {
            var count = await _vendorService.GetAsync();
            return count.Count;
        }
    }
}
