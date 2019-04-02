using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using LifeLike.Shared;
using LifeLike.Shared.Enums;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using LifeLike.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGSUpskill.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _search;

        public SearchController(ISearchService search)
        {
            _search = search;
        }



        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_search.GetMany(id));
        }

        // POST api/<controller>
        [HttpGet("detail/{id}")]
        public IActionResult GetDetail(string id)
        {
            return Ok(_search.Get(id));
        }
        [HttpPost]
        public IActionResult Create([FromBody] Order order )
        {
            _search.Create(order);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _search.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Order order)
        {
            _search.Update(id,order);
            return Ok();
        }
    }
}
