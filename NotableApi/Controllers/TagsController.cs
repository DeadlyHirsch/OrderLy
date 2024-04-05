using Microsoft.AspNetCore.Mvc;
using NotableApi.Models;
using NotableApi.Services;

namespace NotableApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly TagsService _tagsService;

        public TagsController(TagsService tagsService)
        {
            _tagsService = tagsService;
        }
        [HttpGet]
        public async Task<List<NoteTag>> Get() =>
        await _tagsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<NoteTag>> Get(string id)
        {
            var tag = await _tagsService.GetAsync(id);

            if (tag is null)
            {
                return NotFound();
            }

            return tag;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NoteTag newTag)
        {
            await _tagsService.CreateAsync(newTag);

            return CreatedAtAction(nameof(Get), new { id = newTag.Id }, newTag);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, NoteTag updatedTag)
        {
            var tag = await _tagsService.GetAsync(id);

            if (tag is null)
            {
                return NotFound();
            }

            updatedTag.Id = tag.Id;

            await _tagsService.UpdateAsync(id, updatedTag);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var note = await _tagsService.GetAsync(id);

            if (note is null)
            {
                return NotFound();
            }

            await _tagsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
