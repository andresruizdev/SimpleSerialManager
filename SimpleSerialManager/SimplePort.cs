using System.IO.Ports;

namespace SimpleSerialManager
{
    public class SimplePort
    {
        SerialPort _serialPort = new SerialPort();

        public bool IsOpen
        {
            get => _serialPort.IsOpen;
        }

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
                //_serialPort.DataReceived += eventFunc;
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;

                //TODO: Add delegate for received data
                return true;
            }
            return false;
            
        }

        public void Close()
        {
            if (IsOpen)
            {   //TODO: Remove delegate for received data before closing port
                _serialPort.Close();
            }
            
        }

        public void SendLine(string data)
        {
            _serialPort.WriteLine(data);
        }

        public void Send(byte[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        public void Send(char[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        public void Send(string data)
        {
            _serialPort.Write(data);
        }
    }
}