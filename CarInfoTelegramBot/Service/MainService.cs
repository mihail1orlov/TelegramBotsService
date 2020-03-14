using System.Collections.Generic;
using System.ServiceProcess;
using ServiceCommon;

namespace CarInfoTelegramBotService
{
    public class MainService : ServiceBase
    {
        private readonly IEnumerable<IService> _services;

        public MainService(IEnumerable<IService> services)
        {
            _services = services;
        }

        public void StartSvc()
        {
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        public void StopSvc()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }
        }

        protected override void OnStart(string[] args)
        {
            StartSvc();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            StopSvc();
            base.OnStop();
        }
    }
}