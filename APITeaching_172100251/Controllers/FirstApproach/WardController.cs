using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.FirstApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class WardController : ControllerBase
    {
        private readonly IService<Ward> _wardService;
        private readonly IMapper _mapper;

        public WardController(IService<Ward> wardService, IMapper mapper)
        {
            _wardService = wardService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardDTO>>> GetList()
        {
            var entityList = await _wardService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<WardDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ward>> Get(int id)
        {
            return await _wardService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<WardDTO>> Create(WardDTO dto)
        {
            int maxId = await _wardService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Ward();
            _mapper.Map(dto, newModel);
            if (await _wardService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(WardDTO dto)
        {
            if (_wardService.CheckExists(dto.Id))
            {
                var entity = new Ward();
                _mapper.Map(dto, entity);
                if (await _wardService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _wardService.DeleteAsync(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
