using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.IO.Ports;

namespace ArduinoSerialMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort port;
        //public string[] ports;
        //public int[] bauds;
        public MainWindow()
        {
            InitializeComponent();
            port = new SerialPort(portList.Text, Int32.Parse(baudRate.Text));


        }
        private void ConnectPort(object sender, RoutedEventArgs e)
        {
            if (!port.IsOpen)
            {

                this.Dispatcher.Invoke(() =>
                {
                    port.PortName = portList.Text;
                    port.BaudRate = Int32.Parse(baudRate.Text);
                    if (SerialPort.GetPortNames().ToList().Contains(portList.Text))
                    {
                        port.Open();
                        ThreadStart recieve = new(Recieve);
                        Thread thread = new(recieve);
                        thread.Start();
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            output.AppendText($" No Board Available at {port.PortName} {Int32.Parse(baudRate.Text)}\n");
                        });
                    }
                });
            }
            else
            {
                port.Close();
                connect.Content = "Connect";
            }

        }

        public void Recieve()
        {
            if (port.IsOpen)
            {
                this.Dispatcher.Invoke(() =>
                {
                    connect.Content = "DISCONNECT";
                });
                while (port.IsOpen)
                {
                this.Dispatcher.Invoke(() =>
                {
                    if (output.Text != null && output.Text != "")
                    {

                        output.AppendText("\n");

                    }
                    else
                    {
                        try
                        {

                            this.Dispatcher.Invoke(() =>
                            {

                                output.AppendText(port.ReadLine());
                            });

                            Thread.Sleep(1000);
                        }
                        catch (Exception)
                        {
                            port.Close();
                            this.Dispatcher.Invoke(() =>
                            {
                                output.AppendText("\n Device Disconnected ");
                                connect.Content = "Connect";
                            });
                        }
                    }
                });
                 }
            }
        }

        private void sendData(object sender, RoutedEventArgs e)
        {
            ThreadStart send = new ThreadStart(Recieve);
            Thread thread = new Thread(send);
            thread.Start();
        }
        public void Send()
        {

        }

    }
}