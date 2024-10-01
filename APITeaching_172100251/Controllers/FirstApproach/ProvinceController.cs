using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.FirstApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IService<Province> _provinceService;
        private readonly IMapper _mapper;

        public ProvinceController(IService<Province> provinceService, IMapper mapper)
        {
            _provinceService = provinceService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetList()
        {
            var entityList = await _provinceService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<ProvinceDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> Get(int id)
        {
            return await _provinceService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<ProvinceDTO>> Create(ProvinceDTO dto)
        {
            int maxId = await _provinceService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Province();
            _mapper.Map(dto, newModel);
            if (await _provinceService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProvinceDTO dto)
        {
            if (_provinceService.CheckExists(dto.Id))
            {
                var entity = new Province();
                _mapper.Map(dto, entity);
                if (await _provinceService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _provinceService.DeleteAsync(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
