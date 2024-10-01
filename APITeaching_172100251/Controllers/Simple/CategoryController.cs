using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.Simple;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.Simple
{
    [Route("[controller]/[action]"), ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetList()
        {
            var entityList = await _categoryService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<CategoryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return await _categoryService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO dto)
        {
            int maxId = await _categoryService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Category();
            _mapper.Map(dto, newModel);
            if (await _categoryService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO dto)
        {
            if (_categoryService.CheckExists(dto.Id))
            {
                var entity = new Category();
                _mapper.Map(dto, entity);
                if (await _categoryService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
