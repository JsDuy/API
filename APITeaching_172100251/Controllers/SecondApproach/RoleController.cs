using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.SecondApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.SecondApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class RoleController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public RoleController(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RoleDTO>> Get(int id)
        {
            var entity = await _roleRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new RoleDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Role>> GetFull(int id)
        {
            return await _roleRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetList()
        {
            var entityList = await _roleRepository.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<RoleDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _roleRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _roleRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create(RoleDTO dto)
        {
            int maxId = await _roleRepository.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Role();
            //newModel. = DateTime.Now;
            _mapper.Map(dto, newModel);

            if (await _roleRepository.CreateAsync(newModel) != null)
                return Ok(dto);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<RoleDTO>> Update(RoleDTO dto)
        {
            var entity = await _roleRepository.GetAsync(dto.Id);
            if (entity != null)
            {
                _mapper.Map(dto, entity);
                if (await _roleRepository.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }
    }
}
