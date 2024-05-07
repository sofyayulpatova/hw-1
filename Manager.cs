// Homework 1 is created by Sofya Yulpatova sy21002
/*
Create an interface "IOrderManager" with methods "AddOrder", "AddPerson", "AddProduct", "GetOrders", "GetProducts", and "GetPersons".
Create an interface "IDataManager" with methods:
 "print" returns information about all elements in the collections as text, 
"save" saves all collection data to a file, 
"load" loads all collection data from a file, 
"reset" erases all data,
"createTestData" creates test data.
Create a class or classes that implement the interfaces IOrderManager and IDataManager.
*/

using MyNamespace;
using System.Text;
using System.Xml.Serialization; 

namespace FiguresClasses
{
    [Serializable()]
    public class FigureXMLDataManager: IDataManager, IOrderManager
    
    {
        [XmlArray("OrdersList"), XmlArrayItem(typeof(Order), ElementName = "Order")]
        public List<Order> orders;
        public List<Person> persons;
        public List<Product> products;

        public string Path { get; set; }
      

        public void AddOrder(Order order)
        {
            orders.Add(order); // just adding to the list
            
             
        }

        public void AddPerson(Person person)
        {
            persons.Add(person); // just adding to the list
            
            
        }

        public void AddProduct(Product product)
        {
            products.Add(product); // just adding to the list
            
            
        }

        public List<Order> GetOrders()
        {
            return orders; // just list return
        }

        public List<Person> GetPersons()
        {
            return persons; // just list return
        }

        public List<Product> GetProducts()
        {
            return products; // just list return
        }

        public FigureXMLDataManager(string path)
        {
            Path = path;
            orders = new List<Order>(); // Initialize the orders list
            persons = new List<Person>(); // Initialize the persons list
            products = new List<Product>(); // Initialize the products list

        
        }
        public FigureXMLDataManager(){
            Path = "C:\\Test\\data.txt";
            orders = new List<Order>(); // Initialize the orders list
            persons = new List<Person>(); // Initialize the persons list
            products = new List<Product>(); // Initialize the products list

        }


        public string Print()
        {
            //  "print" returns information about all elements in the collections as text, 
            // was not sure what you have expected, but i will just return kinda everything as string

            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/stringbuilder

            StringBuilder result = new StringBuilder();
            result.AppendLine("Path:");
            result.AppendLine(Path);

            // adding product info
            result.AppendLine("Products: ");
            foreach (Product product in products){
                result.AppendLine(product.ToString()); // custom method
        
            }
            // adding persons info
            result.AppendLine("Persons: ");
            foreach (Person person in persons){
                result.AppendLine(person.ToString()); // custom method
            }

            //adding orders
            result.AppendLine("Orders: ");
            foreach (Order order in orders){
                result.AppendLine(order.ToString()); // custom method

            }
        return result.ToString();



        }
        // was difficult to do it :(
        // https://stackoverflow.com/questions/4266875/how-to-quickly-save-load-class-instance-to-file
        public bool Save(string path)
        {
            // "save" saves all collection data to a file, 
            FileStream fileStream = new FileStream(path, FileMode.Create);
            XmlSerializer formatter = new XmlSerializer(this.GetType());
            formatter.Serialize(fileStream, this);
            fileStream.Close();
            
            return true;

        }
           
        public bool Load(string path)
        {
            // https://stackoverflow.com/questions/4266875/how-to-quickly-save-load-class-instance-to-file
            // it was pretty difficult tbh, tried almost for an hour... 
            // load" loads all collection data from a file, 

            // file
            XmlSerializer formatter = new XmlSerializer(this.GetType()); 
            FileStream fileStream = new FileStream(path, FileMode.Open);
            FigureXMLDataManager manager = (FigureXMLDataManager)formatter.Deserialize(fileStream);
            fileStream.Close();
            // updating the object
            
            orders = manager.orders;
            persons = manager.persons;
            products = manager.products;
            Path = manager.Path;
            // just because the func is bool
            return true;        
            
        }

        
        public bool Reset()
        {
            // ""reset" erases all data,

            // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.clear?view=net-8.0
            orders.Clear();
            persons.Clear();
            products.Clear();
            Path = "";
            return true;

        }
        public bool CreateTestData()
        {
            // "createTestData" creates test data.

            // product creation
            AddProduct(new Product  { Name = "Laptop", Price = 999.99m });
            AddProduct(new Product { Name = "Smartphone", Price = 599.99m });
            AddProduct(new Product { Name = "Tablet", Price = 399.99m });
            AddProduct(new Product { Name = "Headphones", Price = 149.99m });
            
            


        
            // person creation, in vars because I will need them during the order creation
            Employee employee = new Employee { Name = "John", Surname = "Doe", Email = "john.doe@gmail.com", AgreementDate = DateTime.Now.AddDays(-1).Date, AgreementNr = 1};
            AddPerson(employee);

            Customer customer_1 = new Customer { Name = "Karlis", Surname = "Zakis", Email = "karlis.zakis@gmail.com"};
            AddPerson(customer_1);

            Customer customer_2 = new Customer { Name = "Lisa", Surname = "Liepina", Email = "lisa.liepina@gmail.com"};
            AddPerson(customer_2);


            // order creation
            
            Order order_1 = new Order(customer_1, employee); // will be created with the default date (today)
            //order_1.Customer = customer_1;
            //order_1.ResponsibleEmployee = employee;
            order_1.AddProduct(products[0], 3);
            AddOrder(order_1);

            Order order_2 = new Order(customer_2, employee, DateTime.Now.AddDays(-1).Date); // created yesterday
            //order_2.Customer = customer_2;
            //order_2.ResponsibleEmployee = employee;
            order_2.AddProduct(products[1], 1);
            AddOrder(order_2);

            Order order_3 = new Order(customer_2, employee);  // will be created with the default date (today)
            //order_3.Customer = customer_2;
            //order_3.ResponsibleEmployee = employee;
            order_3.AddProduct(products[2], 3);
            order_3.AddProduct(products[3], 1);
            AddOrder(order_3);

            // just because the func is bool
            return true;


        }




    }
}
