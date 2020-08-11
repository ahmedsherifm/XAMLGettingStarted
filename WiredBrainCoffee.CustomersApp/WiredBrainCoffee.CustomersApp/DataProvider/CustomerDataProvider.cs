using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.DataProvider
{
    public class CustomerDataProvider : ICustomerDataProvider
    {
        private static readonly string _customerFileName = "customers.json";
        private static readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        public async Task<IEnumerable<Customer>> LoadCustomerAsync()
        {
            var storageFile = await _localFolder.TryGetItemAsync(_customerFileName) as StorageFile;
            List<Customer> customerList = null;

            if (storageFile == null)
            {
                customerList = new List<Customer>
                {
                    new Customer {Firstname = "Thomas", Lastname = "Huber", IsDeveloper = true},
                    new Customer {Firstname = "Anna", Lastname = "Rockstar", IsDeveloper = true},
                    new Customer {Firstname = "Julia", Lastname = "Master"},
                    new Customer {Firstname = "Urs", Lastname = "Meier", IsDeveloper = true},
                    new Customer {Firstname = "Sara", Lastname = "Ramone"},
                    new Customer {Firstname = "Elsa", Lastname = "Queen"},
                    new Customer {Firstname = "Alex", Lastname = "Baier", IsDeveloper = true},
                };
            }
            else
            {
                using (var stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    using (var dataReader = new DataReader(stream))
                    {
                        await dataReader.LoadAsync((uint)stream.Size);
                        var json = dataReader.ReadString((uint)stream.Size);
                        customerList = JsonConvert.DeserializeObject<List<Customer>>(json);
                    }
                }
            }

            return customerList;
        }

        public async Task SaveCustomersAsync(IEnumerable<Customer> customers)
        {
            var storageFile = await _localFolder.CreateFileAsync(_customerFileName, CreationCollisionOption.ReplaceExisting);

            using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var dataWriter = new DataWriter(stream))
                {
                    var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
                    dataWriter.WriteString(json);
                    await dataWriter.StoreAsync();
                }
            }
        }
    }
}
