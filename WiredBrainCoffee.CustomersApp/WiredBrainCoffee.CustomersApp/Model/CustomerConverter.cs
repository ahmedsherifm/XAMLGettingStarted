namespace WiredBrainCoffee.CustomersApp.Model
{
    public static class CustomerConverter
    {
        public static Customer CreateCustomerFromString(string inputString)
        {
            var values = inputString.Split(',');
            return new Customer
            {
                Firstname = values[0],
                Lastname = values[1],
                IsDeveloper = bool.Parse(values[2])
            };
        }
    }
}
