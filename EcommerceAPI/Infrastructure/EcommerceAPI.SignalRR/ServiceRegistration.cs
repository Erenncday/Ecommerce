using EcommerceAPI.Application.Abstractions.Hubs;
using EcommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.SignalR
{
	public static class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection collection)
		{
			collection.AddTransient<IProductHubService, ProductHubService>();
			collection.AddSignalR();
		}
	}
}
