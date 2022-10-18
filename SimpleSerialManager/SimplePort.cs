using System.IO.Ports;

namespace SimpleSerialManager
{
    public class SimplePort
    {
        SerialPort _serialPort = new SerialPort();

        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public static List<string> GetAvailablePortsList()
        {
            return SerialPort.GetPortNames().ToList();
        }
    }
}