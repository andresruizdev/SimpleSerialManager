using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSerialManager
{
    public class ReceivedPacket
    {
        public int ReceivedBytes { get; }
        public string ReceivedData { get; }
        public byte[] ReceivedDataInBytes { get; }

        public ReceivedPacket(int receivedBytes, string receivedData, byte[] receivedDataBytes)
        {
            ReceivedBytes = receivedBytes;
            ReceivedData = receivedData;
            ReceivedDataInBytes = receivedDataBytes;
        }
    }

    public class ReceivedDataEventArgs : EventArgs
    {
        public ReceivedPacket Packet
        {
            get;
        }

        public ReceivedDataEventArgs(int receivedBytes, string receivedData, byte[] receivedDataBytes)
        {
            Packet = new ReceivedPacket(receivedBytes, receivedData, receivedDataBytes);
        }
    }
}
