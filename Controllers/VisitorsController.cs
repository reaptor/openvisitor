// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OpenVisitor.Contracts;
using System.Globalization;

namespace OpenVisitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : Controller
    {
        static List<Visitor> _visitors = new List<Visitor> {
            new Visitor { Name = "Kristofer Löfberg", Host = "Sten Löfberg", SignedInAt = DateTime.Now.Date.AddMinutes(480) },
            new Visitor { Name = "Anders Löfberg", Host = "Sten Löfberg", SignedInAt = DateTime.Now.Date.AddMinutes(543) }
        };

        [HttpGet]
        public IEnumerable<Visitor> Get(string date, string? filter)
        {
            var dt = date.ISO8601StringToDate();
            return _visitors.Where(x => x.SignedInAt.Date == dt.Date
                                 && (filter != null && filter.Length > 0
                                    ? ContainsCaseInsensitive(x.Name, filter)
                                      || ContainsCaseInsensitive(x.Host, filter)
                                    : true))
                .OrderByDescending(x => x.SignedInAt)
                .AsEnumerable();
        }

        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(Guid id) => _visitors.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public ActionResult<Visitor> Post(Visitor visitor)
        {
            visitor.Id = Guid.NewGuid();
            visitor.SignedInAt = DateTime.Now;
            _visitors.Add(visitor);
            return CreatedAtAction(nameof(Get), new { id = visitor.Id }, visitor);
        }

        bool ContainsCaseInsensitive(string source, string value)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, CompareOptions.IgnoreCase) >= 0;
        }
    }
}