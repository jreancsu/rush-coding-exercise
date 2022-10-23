// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RushCodingExercise;
using RushCodingExercise.Interfaces;
using RushCodingExercise.Services;

var services = new ServiceCollection();

services
    .AddSingleton<IPhotoAlbumService, PhotoAlbumService>()
    .AddSingleton<IConsoleService, ConsoleService>()
    .AddSingleton<ConsoleExecutor>();

services
    .AddHttpClient<IPhotoAlbumService, PhotoAlbumService>(client =>
    {
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    });

var serviceProvider = services
    .BuildServiceProvider();

await serviceProvider
    .GetService<ConsoleExecutor>()
    !.Execute();
