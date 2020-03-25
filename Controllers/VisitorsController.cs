using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OpenVisitor.Contracts;

namespace OpenVisitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : Controller
    {
        static List<Visitor> _visitors = new List<Visitor>();

        [HttpGet]
        public IEnumerable<Visitor> Get() => _visitors.OrderByDescending(x => x.SignedInAt).AsEnumerable();

        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(Guid id) => _visitors.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public ActionResult<Visitor> Post(Visitor visitor)
        {
            visitor.Id = Guid.NewGuid();
            _visitors.Add(visitor);
            return CreatedAtAction(nameof(Get), new { id = visitor.Id }, visitor);
        }
    }
}