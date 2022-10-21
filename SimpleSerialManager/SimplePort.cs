using System.Diagnostics;
using System.IO.Ports;

namespace SimpleSerialManager
{
    public class SimplePort
    {
        SerialPort SerialPort = new SerialPort();

        public event EventHandler<ReceivedDataEventArgs> OnDataReceived;

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
            if (avaiblePortsList.Contains(portName))
            {
                SerialPort.PortName = portName;
                SerialPort.BaudRate = bauds;
                SerialPort.DataBits = 8;
                SerialPort.StopBits = StopBits.One;
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                SerialPort.Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"Se abrió correctamente el puerto {portName}");
                    return true;
                }

            }
            Debug.WriteLine($"No se pudo abrir el puerto {portName}");
            return false;

        }

        public bool Open(string portName)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                SerialPort.PortName = portName;
                SerialPort.BaudRate = 9600;
                SerialPort.DataBits = 8;
                SerialPort.StopBits = StopBits.One;
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                SerialPort.Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"Se abrió correctamente el puerto {portName}");
                    return true;
                }

            }
            Debug.WriteLine($"No se pudo abrir el puerto {portName}");
            return false;

        }

        public bool Open(string portName, int bauds, int dataBits)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                SerialPort.PortName = portName;
                SerialPort.BaudRate = bauds;
                SerialPort.DataBits = dataBits;
                SerialPort.StopBits = StopBits.One;
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                SerialPort.Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"Se abrió correctamente el puerto {portName}");
                    return true;
                }

            }
            Debug.WriteLine($"No se pudo abrir el puerto {portName}");
            return false;

        }

        public bool Open(string portName, int bauds, int dataBits, SimpleStopBits stopBits)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                SerialPort.PortName = portName;
                SerialPort.BaudRate = bauds;
                SerialPort.DataBits = dataBits;
                SerialPort.StopBits = (StopBits)stopBits;
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.ReadTimeout = 500;
                SerialPort.WriteTimeout = 500;
                SerialPort.Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"Se abrió correctamente el puerto {portName}");
                    return true;
                }

            }
            Debug.WriteLine($"No se pudo abrir el puerto {portName}");
            return false;

        }


        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsOpen)
            {
                int ReceivedBytes = SerialPort.BytesToRead;
                byte[] ReceivedDataBytes = new byte[ReceivedBytes];
                SerialPort.Read(ReceivedDataBytes, 0, ReceivedDataBytes.Length);
                var ReceivedData = System.Text.Encoding.Default.GetString(ReceivedDataBytes);
                Debug.Write($"{ReceivedBytes} bytes received from {SerialPort.PortName}: {ReceivedData}");
                OnDataReceived?.Invoke(this, new ReceivedDataEventArgs(ReceivedBytes, ReceivedData, ReceivedDataBytes));
            }
        }

        public void Close()
        {
            if (IsOpen)
            {   //TODO: Remove delegate for received data before closing port
                SerialPort.DataReceived -= SerialPort_DataReceived;
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