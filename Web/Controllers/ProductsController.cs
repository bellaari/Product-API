using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [ApiController]
    [Route("api/Products")]
    [EnableCors("CorsPolicy")]


    /*    [EnableCors]
    */
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork<Product> _product;
        private readonly DataContext _context;

        public ProductsController(IUnitOfWork<Product> product,
            DataContext context
            )
        {
            _product = product;
            _context = context;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Products.Include(x=>x.Category).OrderBy(x=>x.Name).ToList());
                //return Ok(_product.Entity.GetAll().OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.Products.Include(x => x.Category).OrderBy(x => x.Name).FirstOrDefault(x => x.Id == id));
                //return Ok(_product.Entity.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product model)
        {
            try
            {
                _product.Entity.Insert(model);
                _product.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product model)
        {
            try
            {
                var result = _product.Entity.GetById(id);
                if (result != null)
                {
                    result.Price = model.Price;
                    result.Quntity = model.Quntity;
                    result.Total = model.Total;
                    result.Descount = model.Descount;
                    result.CategoryId = model.CategoryId;

                    _product.Entity.Update(result);
                    _product.Save();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _product.Entity.Delete(id);
                _product.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
