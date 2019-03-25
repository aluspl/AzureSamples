using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLike.Shared;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGSUpskill.Controllers
{
    [Route("api/[controller]")]
    public class MongoController : Controller
    {
        private readonly IUnitOfWork _unit;
        private IRepository<Item> _repo;


        public MongoController(IServiceProvider provider)
        {
           
            _unit = provider.GetServices<IUnitOfWork>().FirstOrDefault(p => p.Provider == LifeLike.Shared.Enums.Provider.MongoDB);
            _repo = _unit.Get<Item>();
        }

        public IEnumerable<IUnitOfWork> UnitOfWorks { get; }
        public MongoController(IEnumerable<IUnitOfWork> unitOfWorks)
        {
            UnitOfWorks = unitOfWorks;
        }

        public IUnitOfWork Get(LifeLike.Shared.Enums.Provider provider)
        {
            return UnitOfWorks.FirstOrDefault(p => p.Provider == provider);
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetOverview());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_repo.GetDetail(p=>p.Id==id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Item value)
        {
            _repo.Add(value);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Item value)
        {
            _repo.Update(new Item() { Id = id }, value);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _repo.Delete(new Item() { Id = id });
            return Ok();
        }
    }
}
