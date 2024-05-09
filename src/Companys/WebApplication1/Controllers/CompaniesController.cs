using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.API.Persistance;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Companies> Get()
        {
            return [.. _appDbContext.Companies];
        }

        [HttpGet]
        public async Task<Companies> GetById(int id)
        {
            var pr = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            return pr;
        }

        [HttpPost]
        public async Task<Companies> Post([FromBody] Companies product)
        {
            var result = await _appDbContext.Companies.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut]
        public async Task<Companies> Put(int id, [FromBody] Companies product)
        {
            var pr = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Companies.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete]
        public async Task<Companies> Delete(int id)
        {
            var pr = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Companies.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
