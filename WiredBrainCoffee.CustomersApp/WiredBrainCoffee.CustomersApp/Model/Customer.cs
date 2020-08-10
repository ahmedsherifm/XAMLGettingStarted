using WiredBrainCoffee.CustomersApp.Base;

namespace WiredBrainCoffee.CustomersApp.Model
{
    public class Customer : Observable
    {
        private string firstname;
        private string lastname;
        private bool isDeveloper;

        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }
        public string Lastname
        {
            get => lastname;
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }
        public bool IsDeveloper
        {
            get => isDeveloper;
            set
            {
                isDeveloper = value;
                OnPropertyChanged();
            }
        }
    }
}
