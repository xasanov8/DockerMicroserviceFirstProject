using Blog.API.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Models.Blog> Get()
        {
            return [.. _appDbContext.Blogs];
        }

        [HttpGet]
        public async Task<Models.Blog> GetById(int id)
        {   
            var pr = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id);

            return pr;
        }

        [HttpPost]
        public async Task<Models.Blog> Post([FromBody] Models.Blog product)
        {
            var result = await _appDbContext.Blogs.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut]
        public async Task<Models.Blog> Put(int id, [FromBody] Models.Blog product)
        {
            var pr = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Blogs.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete]
        public async Task<Models.Blog> Delete(int id)
        {
            var pr = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Blogs.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
