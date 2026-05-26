using System.Windows;
using System.Windows.Input;

namespace OpenMPLauncher.UI.Views
{
    /// <summary>
    /// Login window for authentication
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void UsernameTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UsernameTextBox.Focus();
        }

        private void PasswordBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordBox.Focus();
        }
    }
}
