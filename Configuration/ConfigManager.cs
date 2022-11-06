using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Configuration
{
    public class ConfigManager: IAppConfiguration
    {
        private readonly IConfiguration _configuration;

        public ConfigManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string DBConnectionString
        {
            get
            {
                return this._configuration["ConnectionStrings:DatabaseConnectionString"];
            }
        }
    }
}
