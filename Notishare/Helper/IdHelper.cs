using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Notishare.Helper
{
    internal static class IdHelper
    {

        internal static string GetDeviceId()
        {
            var cpuId = string.Empty;
            try
            {
                ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
                ManagementObjectCollection mbsList = mbs.Get();

                foreach (var mo in mbsList)
                {
                    cpuId = mo["ProcessorID"].ToString();
                }
                return cpuId;
            }
            catch (Exception) { return cpuId; }
        }
    }
}
