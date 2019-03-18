using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLike.Shared;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using LifeLike.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGSUpskill.Controllers
{
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly ITableStorage _repo;

        public TableController(ITableStorage repo)
        {
            _repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.List().Result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_repo.GetItem(id).Result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Item value)
        {
            _repo.Create(value).Wait();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Item value)
        {
            _repo.Update(id, value).Wait();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _repo.Delete(new Item() { Id = id }).Wait();
            return Ok();
        }
    }
}
