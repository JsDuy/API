using APITeaching_172100251.DTO;
using APITeaching_172100251.Models;
using APITeaching_172100251.Repositories.Simple;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeaching_172100251.Controllers.Simple
{
    [Route("[controller]/[action]"), ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetList()
        {
            var entityList = await _productService.GetsAsync();
            if (entityList != null)
            {
                var dtoList = new List<ProductDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _productService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO dto)
        {
            int maxId = await _productService.GetsAsync()
                                    .ContinueWith(task => task.Result.Max(d => (int?)d.Id) ?? 0);
            dto.Id = maxId + 1; // Tự động tăng ID bằng cách lấy maxId + 1
            //Mapp data model --> newModel
            var newModel = new Product();
            _mapper.Map(dto, newModel);
            if (await _productService.CreateAsync(newModel) != null)
                return Ok(dto);

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO dto)
        {
            if (_productService.CheckExists(dto.Id))
            {
                var entity = new Product();
                _mapper.Map(dto, entity);
                if (await _productService.UpdateAsync(entity) != null)
                    return Ok(dto);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

    }
}
