using System.Net.NetworkInformation;

namespace com.ih.util.generic.ConsumeServices.Utils;

public static class NetworkPingUtil
{
    public static IPStatus Ping(string ipOrDomain)
    {
        try
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send(ipOrDomain);

                if (reply is not null)
                {
                    return reply.Status;
                }

                return IPStatus.Unknown;
            }
        }
        catch (PingException)
        {
            return IPStatus.HardwareError;
        }
    }
}