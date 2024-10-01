using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.SecondApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.SecondApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class RoleUserController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IRepository<RoleUser> _roleuserRepository;
        private readonly IMapper _mapper;
        public RoleUserController(IRepository<RoleUser> roleuserRepository, IMapper mapper)
        {
            _roleuserRepository = roleuserRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RoleUserDTO>> Get(int id)
        {
            var entity = await _roleuserRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new RoleUserDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<RoleUser>> GetFull(int id)
        {
            return await _roleuserRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUserDTO>>> GetList()
        {
            var entityList = await _roleuserRepository.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<RoleUserDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _roleuserRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _roleuserRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleUserDTO>> Create(RoleUserDTO dto)
        {
            int maxId = await _roleuserRepository.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new RoleUser();
            //newModel. = DateTime.Now;
            _mapper.Map(dto, newModel);

            if (await _roleuserRepository.CreateAsync(newModel) != null)
                return Ok(dto);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<RoleUserDTO>> Update(RoleUserDTO model)
        {
            var entity = await _roleuserRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _roleuserRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
