using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;

namespace OpenVisitor.Services
{
    public class History
    {
        private readonly IServiceProvider serviceProvider;

        public History(IServiceProvider ServiceProvider)
        {
            serviceProvider = ServiceProvider;
        }
        public async Task Back()
        {
            var jsRuntime = serviceProvider.GetService<IJSRuntime>();
            await jsRuntime.InvokeVoidAsync("history.back");
        }
    }
}