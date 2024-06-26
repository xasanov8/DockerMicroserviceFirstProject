﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.UI.Persistanse;

namespace Student.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IEnumerable<Models.Student> Get()
        {
            return [.. _appDbContext.Students];
        }

        [HttpGet]
        public async Task<Models.Student> GetById(int id)
        {
            var pr = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            return pr;
        }

        [HttpPost]
        public async Task<Models.Student> Post([FromBody] Models.Student product)
        {
            var result = await _appDbContext.Students.AddAsync(product);

            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut]
        public async Task<Models.Student> Put(int id, [FromBody] Models.Student product)
        {
            var pr = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            pr.Name = product.Name;
            pr.Description = product.Description;
            var result = _appDbContext.Students.Update(pr).Entity;
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        [HttpDelete]
        public async Task<Models.Student> Delete(int id)
        {
            var pr = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
            var result = _appDbContext.Students.Remove(pr).Entity;

            await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
