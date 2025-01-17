using Autofac;
using Autofac.Extensions.DependencyInjection;
using chessAPI;
using chessAPI.business.interfaces;
using chessAPI.models.player;
using chessAPI.models.game;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Serilog.Events;
using System.Collections.Generic;
using System.Web;

//Serilog logger (https://github.com/serilog/serilog-aspnetcore)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("chessAPI starting");
    var builder = WebApplication.CreateBuilder(args);

    var connectionStrings = new connectionStrings();
    builder.Services.AddOptions();
    builder.Services.Configure<connectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
    builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

    // Two-stage initialization (https://github.com/serilog/serilog-aspnetcore)
    builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom
             .Configuration(context.Configuration)
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning).ReadFrom
             .Services(services).Enrich
             .FromLogContext().WriteTo
             .Console());

    // Autofac como inyección de dependencias
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new chessAPI.dependencyInjection<int, int>()));
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseMiddleware(typeof(chessAPI.customMiddleware<int>));
    app.MapGet("/", () =>
    {
        return "hola mundo";
    });

    app.MapPost("player", 
    [AllowAnonymous] async(IPlayerBusiness<int> bs, clsNewPlayer newPlayer) => Results.Ok(await bs.addPlayer(newPlayer)));
    
    //get player by id
    app.MapGet("player",
    [AllowAnonymous] async (IPlayerBusiness<int> bs, int id) => Results.Ok(await bs.getPlayer(id)));


    app.MapPut("player",
    [AllowAnonymous] async (IPlayerBusiness<int> bs, clsPlayer<int> playerToUpdate) => {
        var res = await bs.updatePlayer(playerToUpdate);
        return res != null ? Results.Ok() : Results.NotFound();
    }); 

    //new game
    app.MapPost("game", 
    [AllowAnonymous] async(IGameBusiness<int> bs, clsNewGame newGame) => Results.Ok(await bs.newGame(newGame)));

    //start game 
    app.MapPut("game",
    [AllowAnonymous] async (IGameBusiness<int> bs, clsGame<int> gameToUpdate) => {
        var res = await bs.startGame(gameToUpdate);
        return res != null ? Results.Ok() : Results.NotFound();
    });

    app.MapGet("/players/get/{id:int}", 
    [AllowAnonymous] async(IPlayerBusiness<int> bs, int id) => Results.Ok(await bs.getPlayer(id)));

    // app.MapPut("players/update", 
    // [AllowAnonymous] async(IPlayerBusiness<int> bs, clsNewPlayer dataUpdate) => Results.Ok(await bs.updatePlayer(dataUpdate)));

    app.MapGet("game/get/{id:int}", 
    [AllowAnonymous] async(IGameBusiness<int> bs, int id) => Results.Ok(await bs.getInfoGame(id)));

    app.MapPut("game/update", 
    [AllowAnonymous] async(IGameBusiness<int> bs,  clsWinner winner) => Results.Ok(await bs.setWinner(winner)));


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "chessAPI terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
