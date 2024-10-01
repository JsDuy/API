using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.FirstApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.FirstApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IService<Address> _addressService;
        private readonly IMapper _mapper;

        public AddressController(IService<Address> addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetList()
        {
            var entityList = await _addressService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<AddressDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(int id)
        {
            return await _addressService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<AddressDTO>> Create(AddressDTO dto)
        {
            int maxId = await _addressService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Address();
            _mapper.Map(dto, newModel);
            if (await _addressService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(AddressDTO dto)
        {
            if (_addressService.CheckExists(dto.Id))
            {
                var entity = new Address();
                _mapper.Map(dto, entity);
                if (await _addressService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _addressService.DeleteAsync(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
