using System.Diagnostics;
using System.IO.Ports;

namespace SimpleSerialManager
{
    public class SimplePort : SerialPort
    {

        public event EventHandler<ReceivedDataEventArgs> OnDataReceived;

        public static string[] GetAvailablePorts()
        {
            return GetPortNames();
        }

        public static List<string> GetAvailablePortsList()
        {
            return GetPortNames().ToList();
        }

        public bool Open(string portName, int bauds)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                PortName = portName;
                BaudRate = bauds;
                DataBits = 8;
                StopBits = StopBits.One;
                DataReceived += SerialPort_DataReceived;
                ReadTimeout = 500;
                WriteTimeout = 500;
                Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"{portName} open successfully");
                    return true;
                }

            }
            Debug.WriteLine($"Cannot open port with name {portName}");
            return false;

        }

        public bool Open(string portName)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                PortName = portName;
                BaudRate = 9600;
                DataBits = 8;
                StopBits = StopBits.One;
                DataReceived += SerialPort_DataReceived;
                ReadTimeout = 500;
                WriteTimeout = 500;
                Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"{portName} open successfully");
                    return true;
                }

            }
            Debug.WriteLine($"Cannot open port with name {portName}");
            return false;

        }

        public bool Open(string portName, int bauds, int dataBits)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                PortName = portName;
                BaudRate = bauds;
                DataBits = dataBits;
                StopBits = StopBits.One;
                DataReceived += SerialPort_DataReceived;
                ReadTimeout = 500;
                WriteTimeout = 500;
                Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"{portName} open successfully");
                    return true;
                }

            }
            Debug.WriteLine($"Cannot open port with name {portName}");
            return false;

        }

        public bool Open(string portName, int bauds, int dataBits, SimpleStopBits stopBits)
        {
            var avaiblePortsList = GetAvailablePortsList();
            if (avaiblePortsList.Contains(portName))
            {
                PortName = portName;
                BaudRate = bauds;
                DataBits = dataBits;
                StopBits = (StopBits)stopBits;
                DataReceived += SerialPort_DataReceived;
                ReadTimeout = 500;
                WriteTimeout = 500;
                Open();
                if (IsOpen)
                {
                    Debug.WriteLine($"{portName} open successfully");
                    return true;
                }

            }
            Debug.WriteLine($"Cannot open port with name {portName}");
            return false;

        }


        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsOpen)
            {
                int ReceivedBytes = BytesToRead;
                byte[] ReceivedDataBytes = new byte[ReceivedBytes];
                Read(ReceivedDataBytes, 0, ReceivedDataBytes.Length);
                var ReceivedData = System.Text.Encoding.Default.GetString(ReceivedDataBytes);
                Debug.Write($"{ReceivedBytes} bytes received from {PortName}: {ReceivedData}");
                OnDataReceived?.Invoke(this, new ReceivedDataEventArgs(ReceivedBytes, ReceivedData, ReceivedDataBytes));
            }
        }

        public void Close()
        {
            if (IsOpen)
            {   
                DataReceived -= SerialPort_DataReceived;
                base.Close();
                Debug.WriteLine($"Port closed successfully");
            }
            else
            {
                Debug.WriteLine($"Port is already closed");
            }
            
        }

        public void SendLine(string data)
        {
            WriteLine(data);
        }

        public void Send(byte[] buffer, int offset, int count)
        {
            Write(buffer, offset, count);
        }

        public void Send(char[] buffer, int offset, int count)
        {
            Write(buffer, offset, count);
        }

        public void Send(string data)
        {
            Write(data);
        }
    }
}