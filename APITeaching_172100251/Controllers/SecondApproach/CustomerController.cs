using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.SecondApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.SecondApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class CustomerController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        public CustomerController(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            var entity = await _customerRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new CustomerDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetFull(int id)
        {
            return await _customerRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetList()
        {
            var entityList = await _customerRepository.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<CustomerDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _customerRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _customerRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create(CustomerDTO dto)
        {
            int maxId = await _customerRepository.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Customer();
            //newModel. = DateTime.Now;
            _mapper.Map(dto, newModel);

            if (await _customerRepository.CreateAsync(newModel) != null)
                return Ok(dto);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> Update(CustomerDTO dto)
        {
            var entity = await _customerRepository.GetAsync(dto.Id);
            if (entity != null)
            {
                _mapper.Map(dto, entity);
                if (await _customerRepository.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }
    }
}
