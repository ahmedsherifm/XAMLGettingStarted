using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Controls
{
    public sealed partial class CustomerDetailsControl : UserControl
    {
        private Customer _customer;

        public CustomerDetailsControl()
        {
            this.InitializeComponent();
        }

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                txtFirstname.Text = _customer?.Firstname ?? "";
                txtLastname.Text = _customer?.Lastname ?? "";
                chkIsDeveloper.IsChecked = _customer?.IsDeveloper ?? false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCustomer();
        }

        private void CheckBox_IsCheckedChanged(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();
        }

        private void UpdateCustomer()
        {
            if (Customer != null)
            {
                Customer.Firstname = txtFirstname.Text;
                Customer.Lastname = txtLastname.Text;
                Customer.IsDeveloper = chkIsDeveloper.IsChecked.GetValueOrDefault();
            }
        }
    }
}
