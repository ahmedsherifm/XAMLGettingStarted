using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.DataProvider;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel
    {
        private ICustomerDataProvider _customerDataProvider;

        public MainViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            Customers = new ObservableCollection<Customer>();
        }

        public ObservableCollection<Customer> Customers { get; }

        public async Task LoadAsync()
        {
            Customers.Clear();

            var customers = await _customerDataProvider.LoadCustomerAsync();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        public async Task SaveAsync()
        {
            await _customerDataProvider.SaveCustomersAsync(Customers);
        }
    }
}
