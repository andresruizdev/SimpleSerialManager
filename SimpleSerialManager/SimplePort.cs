using System.Diagnostics;
using System.IO.Ports;

namespace SimpleSerialManager
{
    public class SimplePort
    {
        SerialPort SerialPort = new SerialPort();
        string ReceivedData = "";

        public bool IsOpen
        {
            get => SerialPort.IsOpen;
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
                SerialPort.PortName = portName;
                SerialPort.BaudRate = bauds;
                SerialPort.DataBits = 8;
                SerialPort.StopBits = StopBits.One;
                //_serialPort.DataReceived += eventFunc;
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                SerialPort.Open();
                //TODO: Add delegate for received data
                if (IsOpen)
                {
                    Debug.WriteLine($"Se abrió correctamente el puerto {portName}");
                    return true;
                }
                    
            }
            Debug.WriteLine($"No se pudo abrir el puerto {portName}");
            return false;
            
        }

        public void Close()
        {
            if (IsOpen)
            {   //TODO: Remove delegate for received data before closing port
                SerialPort.Close();
            }
            
        }

        public void SendLine(string data)
        {
            SerialPort.WriteLine(data);
        }

        public void Send(byte[] buffer, int offset, int count)
        {
            SerialPort.Write(buffer, offset, count);
        }

        public void Send(char[] buffer, int offset, int count)
        {
            SerialPort.Write(buffer, offset, count);
        }

        public void Send(string data)
        {
            SerialPort.Write(data);
        }
    }
}