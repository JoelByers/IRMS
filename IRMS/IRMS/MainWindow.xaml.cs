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
            string partySizeString = RsvtnTextBoxNewSize.Text;
            int partySize = -1;

            if(name == "")
            {
                RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                RsvtnTextBlockInvalidMessage.Text = "Invalid Name: Name must be at least one character";
            }
            else if(!phoneNumberIsValid(phoneNumber))
            {
                RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                RsvtnTextBlockInvalidMessage.Text = "Invalid Phone: Must be in format xxx-xxx-xxxx";
            }
            else if(!partySizeStringIsValid(partySizeString))
            {
                RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                RsvtnTextBlockInvalidMessage.Text = "Invalid Party Size: Must be a number";

            }
            else if(!timeStringIsValid(expectedTime))
            {
                RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                RsvtnTextBlockInvalidMessage.Text = "Invalid Time: Must be in the format xx:xx";
            }
            else
            {
                partySize = Int32.Parse(partySizeString);

                if (partySize > 0 && partySize < 100)
                {
                    RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Hidden;

                    Reservation newReservation = new Reservation(name, phoneNumber, expectedTime, "10:00", partySize);
                    reservationController.createReservation(newReservation);
                    reservationTimeSlots[reservationController.timeToIndex(expectedTime)].numSeats -= 4 * (int)Math.Ceiling((double)partySize / 4.0);
                    reservationTimeSlots[reservationController.timeToIndex(expectedTime)].button.Content = reservationTimeSlots[reservationController.timeToIndex(expectedTime)].numSeats.ToString();
                    RsvtnNewGrid.Visibility = System.Windows.Visibility.Hidden;
                    ReservationScrollViewer.SetValue(Grid.RowProperty, 1);

                    RsvtnTextBoxNewName.Text = "";
                    RsvtnTextBoxNewNumber.Text = "";
                    RsvtnTextBoxNewTime.Text = "";
                    RsvtnTextBoxNewSize.Text = "";
                }
                else
                {
                    RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                    RsvtnTextBlockInvalidMessage.Text = "Invalid Party Size: Must be in range 1-99";
                }
            }
        }

        private bool phoneNumberIsValid(string phoneString)
        {
            bool isValid = true;

            if(phoneString.Length != 12)
            {
                isValid = false;
            }
            else if(phoneString.ToCharArray()[3] != '-')
            {
                isValid = false;
            }
            else if (phoneString.ToCharArray()[7] != '-')
            {
                isValid = false;
            }
            else
            {
                try
                {
                    Int32.Parse(phoneString.Substring(0, 3));
                    Int32.Parse(phoneString.Substring(4, 3));
                    Int32.Parse(phoneString.Substring(8, 4));

                }
                catch (FormatException)
                {
                    isValid = false;
                }
            }

            return isValid;

        }

        private bool partySizeStringIsValid(string sizeString)
        {
            bool isValid = true;

            try
            {
                Int32.Parse(sizeString);
            }
            catch(FormatException)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool timeStringIsValid(string timeString)
        {
            bool isValid = true;

            if(timeString.Length != 5 && timeString.Length != 4)
            {
                isValid = false;
            }
            else if(timeString.ToCharArray()[timeString.Length - 3] != ':')
            {
                isValid = false;
            }
            else
            {
                try
                {
                    Int32.Parse(timeString.Substring(timeString.Length - 2, 2));

                    if(timeString.Length == 5)
                    {
                        Int32.Parse(timeString.Substring(0, 2));
                    }
                    else
                    {
                        Int32.Parse(timeString.Substring(0, 1));
                    }
                }
                catch(FormatException)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        public void RsvtnBtnNewClick (object sender, RoutedEventArgs e)
        {
            RsvtnNewGrid.Visibility = System.Windows.Visibility.Visible;
            ReservationScrollViewer.SetValue(Grid.RowProperty, 2);
        }
        public void RsvtnBtnCancelClick(object sender, RoutedEventArgs e)
        {
            RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Hidden;
            RsvtnNewGrid.Visibility = System.Windows.Visibility.Hidden;
            ReservationScrollViewer.SetValue(Grid.RowProperty, 1);

            RsvtnTextBoxNewName.Text = "";
            RsvtnTextBoxNewNumber.Text = "";
            RsvtnTextBoxNewTime.Text = "";
            RsvtnTextBoxNewSize.Text = "";
        }

        public void RsvtnBtnTime1Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(0);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 0);
        }

        public void RsvtnBtnTime2Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(1);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 2);
        }

        public void RsvtnBtnTime3Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(2);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 4);
        }

        public void RsvtnBtnTime4Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(3);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 6);
        }

        public void RsvtnBtnTime5Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(4);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 8);
        }

        public void RsvtnBtnTime6Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(5);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 10);
        }
        public void RsvtnBtnTime7Clk(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(6);
            RsvtnTimeIndicator.SetValue(Grid.ColumnProperty, 13);
        }
    }
}
