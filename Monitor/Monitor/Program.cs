using Monitor.Class;


class Program
{
    static void Main(string[] args)
    {
        var ListServicesController = new List<ServicesController>();
        var ListNameServices = new List<KeyValuePair<string, tpMonitorService>>()
        {
            new KeyValuePair<string, tpMonitorService>("SrvMainSgaBot",  tpMonitorService.AlwaysStoped),
            new KeyValuePair<string, tpMonitorService>("SgaSync",        tpMonitorService.AlwaysStarted)
        };
        ListNameServices.ForEach(nameService => ListServicesController.Add(new ServicesController(nameService.Key, nameService.Value)));

        Thread thread = new Thread(() =>
        {
            while (ListServicesController.Any(sc => sc.IsRunMonitorService))
            {
                Thread.Sleep(5000);
            }
        });

        thread.Start();
        //thread.Join();
        //Task.Wait(); 
    }
}
