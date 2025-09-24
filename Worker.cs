using Application.UseCases;

namespace SAV
{
    public class EtlWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EtlWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Worker iniciado. Creando ámbito para el proceso ETL...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<ImportSalesUseCase>();

                try
                {
                    await useCase.ExecuteAsync();
                    Console.WriteLine("Pipeline de ETL finalizado con éxito.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: El pipeline falló. Detalles: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}
