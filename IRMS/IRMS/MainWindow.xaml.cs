/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Main Window GUI control
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

namespace IRMS
{
    public partial class MainWindow : Window
    {
        struct ReservationTimeSlot
        {
            public Button button;
            public int numSeats;
        };

        private static ReservationController reservationController = new ReservationController();
        private static ReservationTimeSlot[] reservationTimeSlots = new ReservationTimeSlot[reservationController.getNumTimeSlots()];
        private static CustomerProfiles customerProfiles = new CustomerProfiles();
        private static SalesController salesController = new SalesController();
        private static PromotionController promotionController = new PromotionController();
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

            BrushConverter converter = new System.Windows.Media.BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
            SalesViewBeef.Background = brush;

            ReservationGrid.ItemsSource = reservationController.getReservationsAtTime(0);
            SalesItemsGrid.ItemsSource = salesController.getCurrentSaleList();
            MenuItemsGrid.ItemsSource = salesController.getBeefItemsList();
            SalesCouponDataGrid.ItemsSource = promotionController.getCouponList();
            SalesAppliedCouponsDataGrid.ItemsSource = salesController.getAppliedCoupons();
        }

        public void OnMenuClick(object sender, RoutedEventArgs e)
        {
            if(sidebar.Visibility == System.Windows.Visibility.Collapsed)
            {
                sidebar.Visibility = System.Windows.Visibility.Visible;
                Tabs.SetValue(Grid.ColumnProperty, 1);
            }
            else
            {
                sidebar.Visibility = System.Windows.Visibility.Collapsed;
                Tabs.SetValue(Grid.ColumnProperty, 0);
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
                Tabs.SelectedItem = reservationTab;
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
            else if(reservationController.timeToIndex(expectedTime) < 0 || reservationController.timeToIndex(expectedTime) >= reservationController.getNumTimeSlots())
            {
                RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Visible;
                RsvtnTextBlockInvalidMessage.Text = "Invalid Time: Must be in range 6:00 - 12:00";
            }
            else
            {
                partySize = Int32.Parse(partySizeString);

                if (partySize > 0 && partySize < 100)
                {
                    RsvtnTextBlockInvalidMessage.Visibility = System.Windows.Visibility.Hidden;

                    Reservation newReservation = new Reservation(name, phoneNumber, expectedTime, expectedTime, partySize);
                    newReservation.lateTime = reservationController.getLateTime(newReservation, customerProfiles);
                    Trace.WriteLine(newReservation.lateTime);
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

        public void ReservationRemoveReservation(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    reservationTimeSlots[reservationController.timeToIndex(((Reservation)row.Item).expectedTime)]
                        .numSeats += 4 * (int)Math.Ceiling((double)((Reservation)row.Item).partySize / 4.0);

                    reservationTimeSlots[reservationController.timeToIndex(((Reservation)row.Item).expectedTime)]
                        .button.Content = reservationTimeSlots[reservationController.timeToIndex(((Reservation)row.Item)
                        .expectedTime)].numSeats.ToString();

                    reservationController.removeReservation((Reservation)row.Item);
                    break;
                }
            }
        }

        public void ReservationCellChanged(object sender, EventArgs e)
        {
            if(ReservationGrid.SelectedItems != null)
            {
                //Reservation r = (Reservation)ReservationGrid.SelectedItems.GetType;
                Trace.WriteLine("this " + ReservationGrid.SelectedItems.GetType() + " " + ReservationGrid.SelectedItems.Count);
                foreach(var c in ReservationGrid.SelectedItems)
                {
                    Trace.WriteLine("a " + c.GetType());
                }
            }

            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                Trace.WriteLine(vis.GetType());
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Trace.WriteLine("Changed " + ((Reservation)row.Item).expectedTime);

                    break;
                }
            }
        }

        public void MenuSalesBtn(object sender, RoutedEventArgs e)
        {
            if (SalesTab.Visibility == System.Windows.Visibility.Collapsed)
            {
                SalesTab.Visibility = System.Windows.Visibility.Visible;
                Tabs.SelectedItem = SalesTab;
            }
            else
            {
                SalesTab.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void SalesViewBeefClick(object sender, RoutedEventArgs e)
        {
            MenuItemsGrid.ItemsSource = salesController.getBeefItemsList();

            BrushConverter converter = new System.Windows.Media.BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
            SalesViewBeef.Background = brush;
            SalesViewPork.Background = Brushes.LightGray;
            SalesViewChicken.Background = Brushes.LightGray;
            SalesViewDrinks.Background = Brushes.LightGray;

        }
        public void SalesViewPorkClick(object sender, RoutedEventArgs e)
        {
            MenuItemsGrid.ItemsSource = salesController.getPorkItemsList();

            BrushConverter converter = new System.Windows.Media.BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
            SalesViewPork.Background = brush;
            SalesViewBeef.Background = Brushes.LightGray;
            SalesViewChicken.Background = Brushes.LightGray;
            SalesViewDrinks.Background = Brushes.LightGray;
        }
        public void SalesViewChickenClick(object sender, RoutedEventArgs e)
        {
            MenuItemsGrid.ItemsSource = salesController.getChickenItemsList();

            BrushConverter converter = new System.Windows.Media.BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
            SalesViewChicken.Background = brush;
            SalesViewPork.Background = Brushes.LightGray;
            SalesViewBeef.Background = Brushes.LightGray;
            SalesViewDrinks.Background = Brushes.LightGray;
        }
        public void SalesViewDrinksClick(object sender, RoutedEventArgs e)
        {
            MenuItemsGrid.ItemsSource = salesController.getDrinkItemsList();

            BrushConverter converter = new System.Windows.Media.BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
            SalesViewDrinks.Background = brush;
            SalesViewPork.Background = Brushes.LightGray;
            SalesViewChicken.Background = Brushes.LightGray;
            SalesViewBeef.Background = Brushes.LightGray;
        }

        public void SalesAddMenuItem(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    salesController.addSaleItem((MenuItem)row.Item);
                    break;
                }
            }

            SalesItemsGrid.Items.Refresh();
            updateSalesCostText();
        }
        public void SalesRemoveMenuItem(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    salesController.removeItem((SaleItem)row.Item);
                    break;
                }
            }

            SalesItemsGrid.Items.Refresh();
            updateSalesCostText();
        }

        public void SalesViewCouponsClick(object sender, RoutedEventArgs e)
        {
            if(SalesMenuGrid.Visibility == System.Windows.Visibility.Visible)
            {
                SalesMenuGrid.Visibility = System.Windows.Visibility.Hidden;
                SalesCouponGrid.Visibility = System.Windows.Visibility.Visible;
                SalesMakePaymentGrid.Visibility = System.Windows.Visibility.Hidden;
                SalesViewCouponsBtn.Content = "Add Items";
            }
            else
            {
                SalesMenuGrid.Visibility = System.Windows.Visibility.Visible;
                SalesCouponGrid.Visibility = System.Windows.Visibility.Hidden;
                SalesMakePaymentGrid.Visibility = System.Windows.Visibility.Hidden;
                SalesViewCouponsBtn.Content = "View Coupons";
            }
        }

        public void SalesApplyCouponsClick(object sender, RoutedEventArgs e)
        {
            salesController.addCoupon((Coupon)SalesCouponDataGrid.SelectedItem);
            updateSalesCostText();
        }

        public void SalesRemoveAppliedCoupon(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    salesController.removeCoupon((Coupon)row.Item);
                    break;
                }
            }

            updateSalesCostText();
        }

        private void updateSalesCostText()
        {
            SalesTotalTextBlock.Text = salesController.getTotalCost().ToString();
            SalesTaxTextBlock.Text = salesController.getTotalTax().ToString();
            SalesDiscountTextBlock.Text = salesController.getTotalDiscount().ToString();
            SalesInitialCostTextBlock.Text = salesController.getInitialCost().ToString();
        }

        public void SalesEnterPaymentClick(object sender, RoutedEventArgs e)
        {
            if(SalesMakePaymentGrid.Visibility == System.Windows.Visibility.Hidden)
            {
                SalesMakePaymentGrid.Visibility = System.Windows.Visibility.Visible;
                SalesCouponGrid.Visibility = System.Windows.Visibility.Hidden;
                SalesMenuGrid.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        public void SalesCashSaleClick(object sender, RoutedEventArgs e)
        {
            if(salesController.getIsCashSale())
            {
                salesController.setIsCashSale(false);
                SalesCashSaleBtn.Background = Brushes.LightGray;
            }
            else
            {
                salesController.setIsCashSale(true);
                BrushConverter converter = new System.Windows.Media.BrushConverter();
                Brush brush = (Brush)converter.ConvertFromString("#FFF96C");
                SalesCashSaleBtn.Background = brush;
            }
        }

        public void SalesSubmitSaleClick(object sender, RoutedEventArgs e)
        {
            salesController.resetSale();
            updateSalesCostText();
            SalesCardNumberTextBox.Text = "";
            SalesCardDateTextBox.Text = "";
            SalesCardNaemTextBox.Text = "";
            SalesCardCVVTextBox.Text = "";
            SalesPartyTextBox.Text = "";
        }
    }
}
