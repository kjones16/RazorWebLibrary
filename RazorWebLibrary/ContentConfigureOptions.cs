using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace RazorWebLibrary
{
    public class ContentConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IHostingEnvironment _environment;

        public ContentConfigureOptions(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;

            IFileProvider fileProvider;

            if (_environment.IsDevelopment())
            {
                // Looks at the physical files on the disk so it can pick up changes to files under wwwroot while the application is running is Visual Studio.
                fileProvider = new PhysicalFileProvider(Path.Combine(_environment.ContentRootPath, $"..\\{GetType().Assembly.GetName().Name}\\wwwroot"));
            }
            else
            {
                // When deploying use the files that are embedded in the assembly.
                fileProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");
            }

            options.FileProvider = new CompositeFileProvider(options.FileProvider, fileProvider);
        }
    }
}
