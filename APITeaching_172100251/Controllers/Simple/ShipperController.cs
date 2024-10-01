using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.Simple;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.Simple
{
    [Route("[controller]/[action]"), ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;
        private readonly IMapper _mapper;

        public ShipperController(IShipperService shipperService, IMapper mapper)
        {
            _shipperService = shipperService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> GetList()
        {
            var entityList = await _shipperService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<ShipperDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipper>> Get(int id)
        {
            return await _shipperService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<ShipperDTO>> Create(ShipperDTO dto)
        {
            int maxId = await _shipperService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Shipper();
            _mapper.Map(dto, newModel);
            if (await _shipperService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(ShipperDTO dto)
        {
            if (_shipperService.CheckExists(dto.Id))
            {
                var entity = new Shipper();
                _mapper.Map(dto, entity);
                if (await _shipperService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _shipperService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
