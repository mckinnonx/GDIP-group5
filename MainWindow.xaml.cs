using System.Windows;
using System.IO.Ports;
using System.IO;
using System.Windows.Media;
using System.Threading.Tasks;

/// NuGet\Install-Package System.IO.Ports -Version 8.0.0
/// use above code in package manager to install system.io.ports if visual studios feels like pretending it isn't an installed package
namespace GDIPFU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool seesaw = false;
        SerialPort port;
        public void MainWindow_Load(object sender, EventArgs e)
        {
            
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string messageFromArduino;
            messageFromArduino = port.ReadLine();
            textBox.Text = messageFromArduino;
        }
        private void PortWrite(string message)
        {
            try
            {
                indicator.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0)); //changes a status rectangle to show if the send worked
                textBox.Text = message;
                port.WriteLine(message);
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));//white if message goes through
            }
            catch
            {
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));//red if message fails to send
            }
        }
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (port == null)
                {
                    indicator.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    port = new SerialPort();
                    port.PortName = COM.Text;
                    port.BaudRate = 9600;
                    port.Open();
                }
                else
                {
                    indicator.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    port.Close(); //lazy man's try catch function in case port is already open
                    port = new SerialPort();
                    port.PortName = COM.Text;
                    port.BaudRate = 9600;
                    port.Open();
                }
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));//white if message goes through
                string temp = "S0:" + S0.Value.ToString() + ",S1:" + S1.Value.ToString() + ",S2:" + S2.Value.ToString() + ",S3:" + S3.Value.ToString() + ",S4:" + S4.Value.ToString() + ",S5:" + S5.Value.ToString() + "\n";
                foreach (string s in temp.Split(","))
                {
                    PortWrite(s);
                }
            }
            catch
            {
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            current.Text += "S0:" + S0.Value.ToString() + ",S1:" + S1.Value.ToString() + ",S2:" + S2.Value.ToString() + ",S3:" + S3.Value.ToString() + ",S4:" + S4.Value.ToString() + ",S5:" + S5.Value.ToString() + "\n";
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string[] temp = current.Text.Split("\n");
            current.Text = "";
            for (int i = 0; i + 2 < temp.Length; i++)
            {
                current.Text += temp[i] + "\n";
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            preset3.Text = current.Text;
        }
        private void preset1Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] temp1 = preset1.Text.Split("\n");
                PortWrite("SAVE");
                if (temp1.Length == Waypointer.Value)
                {
                    foreach (string s in temp1)
                    {
                        string[] temp2 = s.Split(",");
                        foreach (string s2 in temp2)
                        {
                            PortWrite(s2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void preset2Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] temp1 = preset2.Text.Split("\n");
                PortWrite("SAVE");
                if (temp1.Length == Waypointer.Value)
                {
                    foreach (string s in temp1)
                    {
                        string[] temp2 = s.Split(",");
                        foreach (string s2 in temp2)
                        {
                            PortWrite(s2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void preset3Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] temp1 = preset3.Text.Split("\n");
                PortWrite("SAVE");
                if (temp1.Length == Waypointer.Value)
                {
                    foreach (string s in temp1)
                    {
                        string[] temp2 = s.Split(",");
                        foreach (string s2 in temp2)
                        {
                            PortWrite(s2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void currentSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] temp1 = current.Text.Split("\n");
                PortWrite("SAVE");
                if (temp1.Length == Waypointer.Value)
                {
                    foreach (string s in temp1)
                    {
                        string[] temp2 = s.Split(",");
                        foreach (string s2 in temp2)
                        {
                            PortWrite(s2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            string preset;
            try
            {
                //                S0.Value = 1;
                StreamReader sr1 = new StreamReader(directoryPath.Text + "\\preset1.txt"); ///thank you microsoft tutorials
//                S0.Value = 2;
                preset = sr1.ReadLine();
                //                S0.Value = 3;
                while (preset != null)
                {
                    preset1.Text += preset + "\n";
                    preset = sr1.ReadLine();
                }
                //                S0.Value = 4;
                sr1.Close();
                //                S0.Value = 5;
                StreamReader sr2 = new StreamReader(directoryPath.Text + "\\preset2.txt"); ///thank you copy paste
//                S0.Value = 6;
                preset = sr2.ReadLine();
                //                S0.Value = 7;
                while (preset != null)
                {
                    preset2.Text += preset + "\n";
                    preset = sr2.ReadLine();
                }
                //                S0.Value = 8;
                sr2.Close();
                //                S0.Value = 9;
                StreamReader sr3 = new StreamReader(directoryPath.Text + "\\preset3.txt"); ///thank you copy paste
//                S0.Value = 10;
                preset = sr3.ReadLine();
                //                S0.Value = 11;
                while (preset != null)
                {
                    preset3.Text += preset + "\n";
                    preset = sr3.ReadLine();
                }
                //                S0.Value = 12;
                sr3.Close();
            }
            catch
            {
            } //neatly contained preset loader
        }
        private void bottleneck()
        {
            string temp = "S0:" + S0.Value.ToString() + ",S1:" + S1.Value.ToString() + ",S2:" + S2.Value.ToString() + ",S3:" + S3.Value.ToString() + ",S4:" + S4.Value.ToString() + ",S5:" + S5.Value.ToString() + "\n";
            foreach (string s in temp.Split(","))
            {
                PortWrite(s);
            }
            seesaw = false;
        }
        private async void delay()
        {
            await Task.Delay(500);
            bottleneck();
        }
        private void S0_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }
        private void S1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }
        private void S2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }
        private void S3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }
        private void S4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }
        private void S5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (seesaw == false)
            {
                seesaw = true;
                delay();
            }
        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                if (port != null)
                {
                    port.Close();
                }
            }
            catch
            {
                indicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PortWrite("RESET");
            }
            catch { }
        }
    }
}