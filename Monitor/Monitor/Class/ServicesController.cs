using System.ServiceProcess;

namespace Monitor.Class
{
    public class ServicesController
    {
        private ServiceController sc;
        public bool IsRunMonitorService { get; set; }

        public ServicesController(string nameService)
        {
            sc = new ServiceController(nameService);
            IsRunMonitorService = true;

            MonitorServiceAlwaysStartedAsync();
            //MonitorServiceAlwaysStopedAsync();
        }

        private ServiceControllerStatus GetStatus
        {
            get { return sc.Status; }
        }

        private bool IsRunning
        {
            get { return GetStatus == ServiceControllerStatus.Running; }
        }

        private bool IsStartPending
        {
            get { return GetStatus == ServiceControllerStatus.StartPending; }
        }

        private bool IsPaused
        {
            get { return GetStatus == ServiceControllerStatus.Paused; }
        }

        private bool IsPausePending
        {
            get { return GetStatus == ServiceControllerStatus.PausePending; }
        }

        private bool IsStopped
        {
            get { return GetStatus == ServiceControllerStatus.Stopped; }
        }

        private bool IsStopPending
        {
            get { return GetStatus == ServiceControllerStatus.StopPending; }
        }

        private bool IsContinuePending
        {
            get { return GetStatus == ServiceControllerStatus.ContinuePending; }
        }

        private void StartService()
        {
            sc.Start();
            Console.WriteLine("Iniciando Serviço: " + sc.DisplayName);
        }

        private void ContinueService()
        {
            sc.Continue();
            Console.WriteLine("Continuando Serviço: " + sc.DisplayName);
        }

        private void StopService()
        {
            sc.Stop();
            Console.WriteLine("Parando Serviço: " + sc.DisplayName);
        }

        private async Task MonitorServiceAlwaysStartedAsync()
        {
            await Task.Run(() => {
                try
                {
                    while (IsRunMonitorService)
                    {
                        Console.WriteLine("Verificando Serviço: " + sc.DisplayName);

                        if (IsStopped)
                        {
                            StartService();
                        }
                        else
                        if (IsPaused)
                        {
                            ContinueService();
                        }

                        sc.Refresh();
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[{nameof(MonitorServiceAlwaysStartedAsync)}]Erro Serviço: {sc.DisplayName} - {e.Message}");
                }
            });
        }

        private async Task MonitorServiceAlwaysStopedAsync()
        {
            await Task.Run(() => {
                try
                {
                    while (IsRunMonitorService)
                    {
                        Console.WriteLine("Verificando Serviço: " + sc.DisplayName);

                        if (IsRunning)
                        {
                            StopService();
                        }

                        sc.Refresh();
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[{nameof(MonitorServiceAlwaysStopedAsync)}]Erro Serviço: {sc.DisplayName} - {e.Message}");
                }
            });
        }
    }
}
