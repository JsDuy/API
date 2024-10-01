using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.SecondApproach;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.SecondApproach
{
    [Route("[controller]/[action]"), ApiController]
    public class AccountController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        public AccountController(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDTO>> Get(int id)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new AccountDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Account>> GetFull(int id)
        {
            return await _accountRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetList()
        {
            var entityList = await _accountRepository.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<AccountDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _accountRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _accountRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AccountDTO>> Create(AccountDTO dto)
        {
            int maxId = await _accountRepository.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Account();
            //newModel. = DateTime.Now;
            _mapper.Map(dto, newModel);

            if (await _accountRepository.CreateAsync(newModel) != null)
                return Ok(dto);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<AccountDTO>> Update(AccountDTO dto)
        {
            var entity = await _accountRepository.GetAsync(dto.Id);
            if (entity != null)
            {
                _mapper.Map(dto, entity);
                if (await _accountRepository.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }
    }
}
