using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using OpenVisitor.Contracts;

namespace OpenVisitor.Services
{
    public class VisitorLogService
    {
        static List<Visitor> _list = new List<Visitor> {
            new Visitor { Name = "Kristofer Löfberg", Host = "Sten Löfberg", SignedInAt = DateTime.Now.Date.AddMinutes(480) },
            new Visitor { Name = "Anders Löfberg", Host = "Sten Löfberg", SignedInAt = DateTime.Now.Date.AddMinutes(543) }
        };

        public Task SignInVisitor(string name, string host)
        {
            _list.Add(new Visitor { Name = name, Host = host, SignedInAt = DateTime.Now });
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Visitor>> GetVisitorLog(DateTime date, string filter)
        {
            return Task.FromResult(
                _list.Where(x => x.SignedInAt.Date == date.Date
                                 && (filter.Length > 0
                                    ? ContainsCaseInsensitive(x.Name, filter)
                                      || ContainsCaseInsensitive(x.Host, filter)
                                    : true))
                .OrderByDescending(x => x.SignedInAt)
                .AsEnumerable()
                );
        }

        bool ContainsCaseInsensitive(string source, string value)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, CompareOptions.IgnoreCase) >= 0;
        }
    }
}