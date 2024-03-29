﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence
{
	static class Configurations
	{
		static public string ConnectionString
		{
			get
			{
				ConfigurationManager configurationManager = new();
				configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/EcommerceAPI.API"));
				configurationManager.AddJsonFile("appsettings.json");

				return configurationManager.GetConnectionString("PostgreSQL");
			}
		}
	}
}
