using System;
using System.Net.NetworkInformation;
using System.Text;

namespace CheckShort_YC4
{
    public static class NetworkConnection
    {
        /// <summary>
        /// Indicates whether any network connection is available
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNetworkAvailable()
        {
            return IsNetworkAvailable(0);
        }

        /// <summary>
        /// Indicates whether any network connection is available.
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// </summary>
        /// <param name="minimumSpeed">The minimum speed required. Passing 0 will not filter connection using speed.</param>
        /// <returns>
        ///     <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNetworkAvailable(long minimumSpeed)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;

                // this allow to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                if (ni.Speed < minimumSpeed)
                    continue;

                // discard virtual cards (virtual box, virtual pc, etc.)
                if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;

                // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;

                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <returns></returns>
        public static bool PingNetwork(string hostNameOrAddress)
        {
            bool pingStatus = false;

            using (Ping p = new Ping())
            {
                string data = "ping";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                try
                {
                    PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                    pingStatus = (reply.Status == IPStatus.Success);
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectionNetWork()
        {
            bool isAvail = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            //System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged
            //System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged

            // Add the handlers to the NetworkChange events.
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += NetworkAddressChanged;

            return isAvail;
        }
        ///System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged
        ///System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged
        ///
        ///


        // Declare a method to handle NetworkAvailabilityChanged events.
        private static void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            // Report whether the network is now available or unavailable.
            if (e.IsAvailable)
            {
                Console.WriteLine("Network Available");
            }
            else
            {
                Console.WriteLine("Network Unavailable");
            }
        }
        // Declare a method to handle NetworkAdressChanged events.
        private static void NetworkAddressChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Current IP Addresses:");
            // Iterate through the interfaces and display information.
            foreach (NetworkInterface ni in
            NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation addr
                in ni.GetIPProperties().UnicastAddresses)
                {
                    Console.WriteLine(" - {0} (lease expires {1})",
                    addr.Address, DateTime.Now +
                    new TimeSpan(0, 0, (int)addr.DhcpLeaseLifetime));
                }
            }
        }
    }
}
