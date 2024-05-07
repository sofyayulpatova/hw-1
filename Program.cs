// Homework 1 is created by Sofya Yulpatova sy21002
using MyNamespace;

/*


Create a console application with the following code:





*/




namespace  FiguresClasses
{

    class Program
    {
        static void Main(string[] args)
        {
            string path = "data.txt";
            //code to set path   
        
            var dm = new FigureXMLDataManager(path = path);
            dm.CreateTestData();
            Console.WriteLine(dm.Print());
            dm.Save(path);
            dm.Reset();
            Console.WriteLine(dm.Print());
            dm.Load(path);
            Console.WriteLine(dm.Print());
            Console.ReadLine();
        }
    }
    public interface IDataManager
    {
        public string Print();

        public bool Save(string path);

        public bool Load(string path);

        public bool CreateTestData();

        public bool Reset();

    }


    public interface IOrderManager
    {
        void AddOrder(Order order);
        void AddPerson(Person person);
        void AddProduct(Product product);
        List<Order> GetOrders();
        List<Product> GetProducts();
        List<Person> GetPersons();
    }

}
