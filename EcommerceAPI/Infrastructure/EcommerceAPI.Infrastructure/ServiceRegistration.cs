using EcommerceAPI.Application.Abstractions.Storage;
using EcommerceAPI.Infrastructure.Enums;
using EcommerceAPI.Infrastructure.Services;
using EcommerceAPI.Infrastructure.Services.Storage;
using EcommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IStorageService, StorageService>();
		}

		public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
		{
			serviceCollection.AddScoped<IStorage, T>();
		}

		public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
		{
			switch (storageType) 
			{
				case StorageType.Local :
					serviceCollection.AddScoped<IStorage, LocalStorage>();
					break;

				case StorageType.Azure :
					serviceCollection.AddScoped<IStorage, LocalStorage>();
					break;

				case StorageType.AWS :
					break;

				default :
					serviceCollection.AddScoped<IStorage, LocalStorage>();
					break;
			}
		}
	}
}
