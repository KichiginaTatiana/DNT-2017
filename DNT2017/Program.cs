﻿using System;
using DNT2017.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DNT2017
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public IServiceProvider ConfigureContainer() =>
            new ServiceCollection()
                .AddTransient<ITransientService, TransientService>()
                .AddScoped<IScopedService, ScopedService>()
                .AddSingleton<ISingletonService, SingletonService>()
                .AddSingleton<ISingletomInstanceService>(new SingletonService("42"))
                .BuildServiceProvider();
    }
}