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
        struct ReservationTimeSlot
        {
            public Button button;
            public int numSeats;
        };

        public MainWindow()
        {
            InitializeComponent();

            ReservationController reservationController = new ReservationController();
            ReservationTimeSlot[] reservationTimeSlots = new ReservationTimeSlot[reservationController.getNumTimeSlots()];
            Reservation res = new Reservation("Bob", "123-456-7890","10:00", "10:15", 4);

            reservationTimeSlots[0].button = RsvtnBtnTime1;
            reservationTimeSlots[1].button = RsvtnBtnTime2;
            reservationTimeSlots[2].button = RsvtnBtnTime3;
            reservationTimeSlots[3].button = RsvtnBtnTime4;
            reservationTimeSlots[4].button = RsvtnBtnTime5;
            reservationTimeSlots[5].button = RsvtnBtnTime6;
            reservationTimeSlots[6].button = RsvtnBtnTime7;

            for(int i = 0; i < reservationTimeSlots.Length; i++)
            {
                reservationTimeSlots[i].numSeats = reservationController.getNumSeats();
                reservationTimeSlots[i].button.Content = reservationTimeSlots[i].numSeats;
            }

            ReservationGrid.Items.Add(res);
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

        public void MenuRsvtnBtn(object sender, RoutedEventArgs e)
        {
            //Set Menu1Tab to Visible or Collased

            if(reservationTab.Visibility == System.Windows.Visibility.Collapsed)
            {
                reservationTab.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                reservationTab.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


    }
}
