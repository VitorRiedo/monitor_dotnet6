using Monitor.Class;
using System.Diagnostics;
using System.ServiceProcess;


class Program
{
    public enum SimpleServiceCustomCommands
    {
        StopWorker = 128, RestartWorker, CheckWorker
    };

    static void Main(string[] args)
    {
        var ListServicesController = new List<ServicesController>();
        var ListNameServices = new List<string>() { "SrvMainSgaBot", "SgaSync" };

        ListNameServices.ForEach(nameService => ListServicesController.Add(new ServicesController(nameService)));

        Thread thread = new Thread(() =>
        {
            while (ListServicesController.Any(sc => sc.IsRunMonitorService))
            {
                Thread.Sleep(5000);
            }
        });

        thread.Start();
        thread.Join();
        //Task.Wait();

        //ServiceController[] scServices;
        //scServices = ServiceController.GetServices();

        //foreach (ServiceController scTemp in scServices)
        //{
        //    Console.WriteLine("Service = " + scTemp.ServiceName);
        //    if (ListNameServices.Contains(scTemp.ServiceName))
        //    {
        //        ServiceController sc = new ServiceController(scTemp.ServiceName);
        //        Console.WriteLine("Status = " + sc.Status);
        //        Console.WriteLine("Can Pause and Continue = " + sc.CanPauseAndContinue);
        //        Console.WriteLine("Can ShutDown = " + sc.CanShutdown);
        //        Console.WriteLine("Can Stop = " + sc.CanStop);
        //        if (sc.Status == ServiceControllerStatus.Stopped)
        //            if (sc.Status == ServiceControllerStatus.Stopped)
        //            {
        //                sc.Start();
        //                while (sc.Status == ServiceControllerStatus.Stopped)
        //                {
        //                    Thread.Sleep(1000);
        //                    sc.Refresh();
        //                }
        //            }
        //        // Issue custom commands to the service
        //        // enum SimpleServiceCustomCommands
        //        //    { StopWorker = 128, RestartWorker, CheckWorker };
        //        sc.ExecuteCommand((int)SimpleServiceCustomCommands.StopWorker);
        //        sc.ExecuteCommand((int)SimpleServiceCustomCommands.RestartWorker);
        //        sc.Pause();
        //        while (sc.Status != ServiceControllerStatus.Paused)
        //        {
        //            Thread.Sleep(1000);
        //            sc.Refresh();
        //        }
        //        Console.WriteLine("Status = " + sc.Status);
        //        sc.Continue();
        //        while (sc.Status == ServiceControllerStatus.Paused)
        //        {
        //            Thread.Sleep(1000);
        //            sc.Refresh();
        //        }
        //        Console.WriteLine("Status = " + sc.Status);
        //        sc.Stop();
        //        while (sc.Status != ServiceControllerStatus.Stopped)
        //        {
        //            Thread.Sleep(1000);
        //            sc.Refresh();
        //        }
        //        Console.WriteLine("Status = " + sc.Status);
        //        String[] argArray = new string[] { "ServiceController arg1", "ServiceController arg2" };
        //        sc.Start(argArray);
        //        while (sc.Status == ServiceControllerStatus.Stopped)
        //        {
        //            Thread.Sleep(1000);
        //            sc.Refresh();
        //        }
        //        Console.WriteLine("Status = " + sc.Status);
        //        // Display the event log entries for the custom commands
        //        // and the start arguments.
        //        EventLog el = new EventLog("Application");
        //        EventLogEntryCollection elec = el.Entries;
        //        foreach (EventLogEntry ele in elec)
        //        {
        //            if (ele.Source.IndexOf("SimpleService.OnCustomCommand") >= 0 |
        //                ele.Source.IndexOf("SimpleService.Arguments") >= 0)
        //                Console.WriteLine(ele.Message);
        //        }
        //    }
        //}
    }
}
