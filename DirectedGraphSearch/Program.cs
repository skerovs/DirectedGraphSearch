using System;
using System.IO;
using DirectedGraphSearch.Services;
using DirectedGraphSearch.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DirectedGraphSearch.Config;
using DirectedGraphSearch.Services.Helpers;

namespace DirectedGraphSearch
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            RegisterConfiguration();
            RegisterServices();

            var fileService = _serviceProvider.GetService<IFileHelperService>();
            var graphService = _serviceProvider.GetService<IGraphService>();

            var directorySettings = _serviceProvider.GetService<IOptions<DirectorySettings>>().Value;

            var graphToProcess =
                graphService.ReadGraphFromString(fileService.FileReadAllText(directorySettings.GraphFilePath));

            var algorithmHelper = new Algorithms();
            var allPossiblePaths = algorithmHelper.GetPossiblePaths(graphToProcess);

            graphService.PrintGraph(graphToProcess);
            graphService.PrintMostExpensivePathInGraph(graphToProcess, allPossiblePaths);

            DisposeServices();
            Console.ReadLine();
        }

        private static void RegisterConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IFileHelperService, FileHelperService>();
            collection.AddTransient<IGraphService, GraphService>();
            collection.Configure<DirectorySettings>(_configuration.GetSection("DirectorySettings"));
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null) return;
            
            if (_serviceProvider is IDisposable)
                ((IDisposable)_serviceProvider).Dispose();
        }
    }
}
