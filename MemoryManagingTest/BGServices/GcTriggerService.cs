namespace MemoryManagingTest.BGServices
{
    public class GcTriggerService : BackgroundService
    {
        private readonly long _memoryThreshold;
        private readonly TimeSpan _checkInterval;

        public GcTriggerService(long memoryThresholdInBytes, TimeSpan checkInterval)
        {
            _memoryThreshold = memoryThresholdInBytes;
            _checkInterval = checkInterval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_checkInterval, stoppingToken);

                long memoryUsed = GC.GetTotalMemory(false);
                Console.WriteLine($"Current memory usage: {memoryUsed / 1024 / 1024} MB");

                if (memoryUsed > _memoryThreshold)
                {
                    Console.WriteLine($"Memory usage exceeded {_memoryThreshold / 1024 / 1024} MB. Triggering GC.");

                    GC.Collect(2, GCCollectionMode.Forced, true, true);
                }
            }
        }
    }
}
