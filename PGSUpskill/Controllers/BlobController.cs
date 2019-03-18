using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLike.Shared;
using LifeLike.Shared.BlobStorage;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using LifeLike.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGSUpskill.Controllers
{
    [Route("api/[controller]")]
    public class BlobController : Controller
    {
        private readonly IBlobStorage _repo;

        public BlobController(IBlobStorage repo)
        {
            _repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetList("Blob").Result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_repo.Get(id, "Blob").Result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(BlobItem model)
        {
            if (model.Stream == null)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var doc = Request.Form.Files[0];
                    model.Stream = doc.OpenReadStream();
                }
            }
            _repo.Create(model).Wait();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, BlobItem model)
        {
            if (model.Stream == null)
            {
                if (Request.Form.Files.Count > 0)           
                {
                    var doc = Request.Form.Files[0];
                    model.Stream = doc.OpenReadStream();
                }
            }
            _repo.Update(model).Wait();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _repo.Remove(id, "Blob").Wait();
            return Ok();
        }
    }
}
