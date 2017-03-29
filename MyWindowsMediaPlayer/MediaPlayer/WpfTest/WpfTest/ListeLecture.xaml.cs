using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Logique d'interaction pour ListeLecture.xaml
    /// </summary>
    public partial class ListeLecture : Window
    {
        public ListeLecture(MainWindow AppMainWindow)
        {
            InitializeComponent();
            this.Top = AppMainWindow.Top;
            this.Left = AppMainWindow.Left;

            this.Height = AppMainWindow.ActualHeight;
            this.Width = AppMainWindow.ActualWidth;
            this.WindowState = AppMainWindow.WindowState;
        }

        private void go_close(object sender, EventArgs e)
        {
            ((MainWindow)this.Owner).Show();
        }

        private void Key_pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                this.Close();
                ((MainWindow)this.Owner).Show();
            }
        }
    }
}
