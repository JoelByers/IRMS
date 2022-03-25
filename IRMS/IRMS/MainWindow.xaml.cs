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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRMS
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

        public void OnMenuClick(object sender, RoutedEventArgs e)
        {
            if(sidebar.Visibility == System.Windows.Visibility.Collapsed)
            {
                sidebar.Visibility = System.Windows.Visibility.Visible;
                tabs.SetValue(Grid.ColumnProperty, 1);
            }
            else
            {
                sidebar.Visibility = System.Windows.Visibility.Collapsed;
                tabs.SetValue(Grid.ColumnProperty, 0);
            }
        }

        public void OnNotificationClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
