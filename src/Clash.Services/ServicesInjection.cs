using Autofac;
using Clash.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clash.Services
{
    public static class ServicesInjection
    {
        public static void RegisterServices(this ContainerBuilder container)
        {
            // Register your services here
            container.RegisterType<DeviceServer>().As<IDevicesServer>();
        }
    }
}
