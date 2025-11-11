using BuildingBlocks.Application.EventBus;

namespace Fintrack.Planning.Publisher.HostedServices;

public sealed class IntegrationEventPublisher(
    IServiceScopeFactory scopeFactory,
    ILogger<IntegrationEventPublisher> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var eventLogService = scope.ServiceProvider.GetRequiredService<IIntegrationEventLogService>();

            var pendingEventLogs = await eventLogService.RetrieveEventLogsPendingToPublishAsync(stoppingToken);

            foreach (var eventLog in pendingEventLogs)
            {
                try
                {
                    await eventLogService.MarkEventAsInProgressAsync(eventLog.EventId, stoppingToken);
                    await eventLogService.MarkEventAsPublishedAsync(eventLog.EventId, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", eventLog.EventId);
                    await eventLogService.MarkEventAsFailedAsync(eventLog.EventId, stoppingToken);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
