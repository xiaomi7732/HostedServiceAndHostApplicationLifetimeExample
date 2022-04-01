namespace HostingStartupAfterHostReady;

internal class TargetService : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;

    public TargetService(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Before host ready...");
        _lifetime.ApplicationStarted.Register(()=>{
            Console.WriteLine("Code after application host has fully started.");
        });

        _lifetime.ApplicationStopping.Register(()=>{
            Console.WriteLine("Application is stopping, but not stopped yet.");
        });

        _lifetime.ApplicationStopped.Register(()=>{
            Console.WriteLine("Application is stopped. The host is not yet.");
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping the host.");
        return Task.CompletedTask;
    }
}