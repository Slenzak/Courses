using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Media;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace Courses
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Form_Load;
            DataContext = this;
        }

        [DllImport("User32.dll")]
        private static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);

        private void Form_Load(object sender, RoutedEventArgs e)
        {
            const uint WDA_MONITOR = 1;
            var handle = new WindowInteropHelper(this).Handle;
            SetWindowDisplayAffinity(handle, WDA_MONITOR);
        }
        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand<object>((args) =>
                {
                    if (args is PasswordBox)
                    {
                        LoginPassword = ((PasswordBox)args).Password;
                        var pass = (TextBox)FindName("boxik");
                        if (pass != null)
                        {
                            pass.Text = QuickHash(LoginPassword);
                            
                        }
                        else
                        {
                            MessageBox.Show("Comeback later we are having issues");
                        }
                    }
                });
            }
        }
        string QuickHash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }

        public string LoginPassword { get; set; }
    }
}
