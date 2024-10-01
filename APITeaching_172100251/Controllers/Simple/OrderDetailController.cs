using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.Simple;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.Simple
{
    [Route("[controller]/[action]"), ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetList()
        {
            var entityList = await _orderDetailService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<OrderDetailDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> Get(int id)
        {
            return await _orderDetailService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailDTO>> Create(OrderDetailDTO dto)
        {
            int maxId = await _orderDetailService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new OrderDetail();
            _mapper.Map(dto, newModel);
            if (await _orderDetailService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(OrderDetailDTO dto)
        {
            if (_orderDetailService.CheckExists(dto.Id))
            {
                var entity = new OrderDetail();
                _mapper.Map(dto, entity);
                if (await _orderDetailService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _orderDetailService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
