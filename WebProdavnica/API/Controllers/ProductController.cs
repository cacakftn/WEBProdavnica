using BusinessLayer.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productBusiness.GetAll();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return productBusiness.Get(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            productBusiness.Add(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            if (id != product.IdProduct) return;
            productBusiness.Update(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = productBusiness.Get(id);
            productBusiness.Delete(product);
        }
    }
}