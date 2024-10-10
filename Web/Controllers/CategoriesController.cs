using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    [EnableCors("CorsPolicy")]


    /*    [EnableCors]
    */
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork<Category> _category;

        public CategoriesController(IUnitOfWork<Category> category)
        {
            _category = category;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_category.Entity.GetAll().OrderBy(x=>x.Name).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_category.Entity.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category model)
        {
            try
            {
                _category.Entity.Insert(model);
                _category.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category model)
        {
            try
            {
                var result = _category.Entity.GetById(id);
                if(result != null)
                {
                    result.Name = model.Name;
                    _category.Entity.Update(result);
                    _category.Save();
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _category.Entity.Delete(id);
                _category.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
