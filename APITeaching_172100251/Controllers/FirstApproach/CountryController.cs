using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.FirstApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IService<Country> _countryService;
        private readonly IMapper _mapper;

        public CountryController(IService<Country> countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetList()
        {
            var entityList = await _countryService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<CountryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            return await _countryService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<CountryDTO>> Create(CountryDTO dto)
        {
            int maxId = await _countryService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Country();
            _mapper.Map(dto, newModel);
            if (await _countryService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CountryDTO dto)
        {
            if (_countryService.CheckExists(dto.Id))
            {
                var entity = new Country();
                _mapper.Map(dto, entity);
                if (await _countryService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _countryService.DeleteAsync(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
