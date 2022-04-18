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
using System.Collections.ObjectModel;

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

        private static ReservationController reservationController = new ReservationController();
        private static ReservationTimeSlot[] reservationTimeSlots = new ReservationTimeSlot[reservationController.getNumTimeSlots()];
        
        public MainWindow()
        {
            InitializeComponent();

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

            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(0);
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

        public void RsvtnBtnCreateClick(object sender, RoutedEventArgs e)
        {
            string name = RsvtnTextBoxNewName.Text;
            string phoneNumber = RsvtnTextBoxNewNumber.Text;
            string expectedTime = RsvtnTextBoxNewTime.Text;
            int partySize = Int32.Parse(RsvtnTextBoxNewSize.Text.Trim());

            Reservation newReservation = new Reservation(name, phoneNumber, expectedTime, "10:00",partySize);
            reservationController.createReservation(newReservation);
            reservationTimeSlots[reservationController.timeToIndex(expectedTime)].numSeats--;
            reservationTimeSlots[reservationController.timeToIndex(expectedTime)].button.Content = reservationTimeSlots[reservationController.timeToIndex(expectedTime)].numSeats;
        }

        public void RsvtnBtnNewClick (object sender, RoutedEventArgs e)
        {
            RsvtnNewGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public void RsvtnBtnTime1Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(0);
        }

        public void RsvtnBtnTime2Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(1);
        }

        public void RsvtnBtnTime3Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(2);
        }

        public void RsvtnBtnTime4Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(3);
        }

        public void RsvtnBtnTime5Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(4);
        }

        public void RsvtnBtnTime6Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(5);
        }
        public void RsvtnBtnTime7Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(6);
        }
    }
}
