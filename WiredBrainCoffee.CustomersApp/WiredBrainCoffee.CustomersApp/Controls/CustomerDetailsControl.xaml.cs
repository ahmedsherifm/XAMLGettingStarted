using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Controls
{
    [ContentProperty(Name = nameof(Customer))]
    public sealed partial class CustomerDetailsControl : UserControl
    {
        public static readonly DependencyProperty CustomerProperty =
            DependencyProperty.Register("Customer", typeof(Customer), typeof(CustomerDetailsControl),
                new PropertyMetadata(null, CustomerChangedCallback));

        public CustomerDetailsControl()
        {
            this.InitializeComponent();
        }

        public Customer Customer
        {
            get { return (Customer)GetValue(CustomerProperty); }
            set { SetValue(CustomerProperty, value); }
        }

        private static void CustomerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CustomerDetailsControl customerDetailsControl)
            {
                var customer = e.NewValue as Customer;
                customerDetailsControl.txtFirstname.Text = customer?.Firstname ?? "";
                customerDetailsControl.txtLastname.Text = customer?.Lastname ?? "";
                customerDetailsControl.chkIsDeveloper.IsChecked = customer?.IsDeveloper ?? false;
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
