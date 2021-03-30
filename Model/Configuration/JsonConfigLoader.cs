using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Model.Configuration
{
    public class JsonConfigLoader : IConfigLoader
    {
        private readonly IHostEnvironment _hostEnvironment;

        public JsonConfigLoader(IHostEnvironment hostingEnvironment, IOptions<SnakesAndLaddersConfig> config)
        {
            _hostEnvironment = hostingEnvironment;
        }

        public ConfigModel Load(string fileName)
        {
            try
            {
                string levelJson = LoadFile(_hostEnvironment.ContentRootPath + "/" + fileName);
                var configModel = JsonSerializer.Deserialize<ConfigModel>
                    (levelJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return configModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private string LoadFile(string resource)
        {
            using var sr = new StreamReader(resource);
            return sr.ReadToEnd();
        }
    }
}
