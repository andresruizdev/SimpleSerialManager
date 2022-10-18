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

        public bool Open(string portName, int bauds)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName)){
                _serialPort.PortName = portName;
                _serialPort.BaudRate = bauds;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;

                //TODO: Add delegate for received data
                return true;
            }
            return false;
            
        }

        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }

        public void Close()
        {
            if (IsOpen())
            {   //TODO: Remove delegate for received data before closing port
                _serialPort.Close();
            }
            
        }
    }
}