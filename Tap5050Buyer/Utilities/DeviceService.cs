using System;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace Tap5050Buyer
{
    public static class DeviceService
    {
        private static IDevice _device;

        public static IDevice Device
        {
            get
            {
                if (_device == null)
                {
                    _device = Resolver.Resolve<IDevice>();
                }
                return _device;
            }
        }
    }
}