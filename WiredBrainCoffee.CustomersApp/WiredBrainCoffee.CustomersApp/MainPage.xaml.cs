using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.DataProvider;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        private CustomerDataProvider _customerDataProvider;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            App.Current.Suspending += Current_Suspending;
            _customerDataProvider = new CustomerDataProvider();
            RequestedTheme = App.Current.RequestedTheme == ApplicationTheme.Dark
                ? ElementTheme.Dark
                : ElementTheme.Light;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            customerListView.Items.Clear();

            var customers = await _customerDataProvider.LoadCustomerAsync();
            foreach (var customer in customers)
            {
                customerListView.Items.Add(customer);
            }
        }

        private async void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await _customerDataProvider.SaveCustomersAsync(customerListView.Items.OfType<Customer>());
            deferral.Complete();
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer { Firstname = "New" };
            customerListView.Items.Add(customer);
            customerListView.SelectedItem = customer;
        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = customerListView.SelectedItem as Customer;
            if(customer != null)
            {
                customerListView.Items.Remove(customer);
            }
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            var column = Grid.GetColumn(customerListGrid);
            var newColumn = column == 0 ? 2 : 0;

            Grid.SetColumn(customerListGrid, newColumn);
            moveSymbolIcon.Symbol = newColumn == 0 ? Symbol.Forward : Symbol.Back;
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = customerListView.SelectedItem as Customer;
            customerDetailsControl.Customer = customer;
        }

        private void ButtonToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            RequestedTheme = RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
        }
    }
}
