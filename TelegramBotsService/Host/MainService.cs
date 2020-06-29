using System.Collections.Generic;
using ServiceCommon;

namespace Host
{
    public class MainService
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
    }
}