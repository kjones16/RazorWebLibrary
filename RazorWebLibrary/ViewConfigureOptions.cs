using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace RazorWebLibrary
{
    public class ViewConfigureOptions : IPostConfigureOptions<RazorViewEngineOptions>
    {
        private readonly IHostingEnvironment _environment;

        public ViewConfigureOptions(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void PostConfigure(string name, RazorViewEngineOptions options)
        {
            if (_environment.IsDevelopment())
            {
                // Looks for the physical file on the disk so it can pick up any view changes.
                options.FileProviders.Add(new PhysicalFileProvider(Path.Combine(_environment.ContentRootPath, $"..\\{GetType().Assembly.GetName().Name}")));
            }
        }
    }
}
