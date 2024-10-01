using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.FirstApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IService<District> _districtService;
        private readonly IMapper _mapper;

        public DistrictController(IService<District> districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetList()
        {
            var entityList = await _districtService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<DistrictDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<District>> Get(int id)
        {
            return await _districtService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<DistrictDTO>> Create(DistrictDTO dto)
        {
            int maxId = await _districtService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new District();
            _mapper.Map(dto, newModel);
            if (await _districtService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(DistrictDTO dto)
        {
            if (_districtService.CheckExists(dto.Id))
            {
                var entity = new District();
                _mapper.Map(dto, entity);
                if (await _districtService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _districtService.DeleteAsync(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
