using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesLambdas
{
    public delegate int BizRulesDelegate(int x, int y);

    public class Program
    {
        static void Main(string[] args)
        {
            var custs = SeedCustomerData();

            /*
            var phnCusts = custs.Where(
                delegate(Customer c){
                    if (c.City == "Phoenix") return true;
                    else return false;});
            */
            var phnCusts = custs.Where(c => c.City == "Phoenix");

            foreach(var customer in phnCusts)
                Console.WriteLine(customer.City);

            #region Working with Func and Action
            /*
            var data = new ProcessData();

            BizRulesDelegate sumDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;

            Func<int, int, int> addFunction = (x, y) => x + y;
            Func<int, int, int> multiplyFunction = (x, y) => x + y;
            Action<int, int> addAction = (x, y) => Console.WriteLine(x + y);

            ProcessData.Process(2, 3, sumDel);
            ProcessData.Process(2, 3, multiplyDel);
            ProcessData.Process(2, 3, addAction);
            ProcessData.Process(2, 3, multiplyFunction);
            */
            #endregion

            #region Comments to understand flow
            /* My custom procedure will work like this:
             *      #Initialize
             *        var worker = new Worker();
             * 
             *      #This first encapsulates the method(Worker.TestWorkPerformed) in a new WorkerPerformedDelegate,
             *      #then adds that delegate as a handler to the workPerformedEvent
             *          worker.workPerformedEvent += new Worker.WorkPerformedDelegate(Worker.TestWorkPerformed);
             *      
             *      -----------------------
             *      #Note: A similar form with reduced code, if you don't need to explicitly define the delegate,
             *      #is to use 'new EventHandler<T>(SomeMethod)'
             *      
             *      #In our case it would be: new EventHandler<WorkPerformedEventArgs>(TestWorkPerformed)
             *      
             *      #You would then not need to have declared the delegate 'WorkPerformedDelegate'
             *      #EventHandler is a delegate, and specified <EventArgs> assumes that your SomethingEventArgs class
             *      #derives from EventArgs
             *      
             *      #Essentially  EventHandler<MyEventArgs>
             *      #is the same as delegate SomeHandler(object sender, MyEventArgs e)
             *      #which is similar to EventHandler(object sender, MyEventArgs e)
             *      -----------------------
             *      
             *      #This method raises the event every hour
             *          worker.DoWorkExplicit(9, WorkType.GoToMeetings);
             */
            #endregion

            #region Comments to understand better
            /* This:
             * worker.workPerformed += new EventHandler<WorkPerformedEventArgs>(
             *   delegate(object sender, WorkPerformedEventArgs e) {
             *      Console.WriteLine($"Hours worked: {e.Hours}, doing: {e.WorkType}"); });
             * 
             * ...is the same as:
             * worker.workPerformed += new EventHandler<WorkPerformedEventArgs>(
             *   (sender, e) => Console.WriteLine($"Hours worked: {e.Hours}, doing: {e.WorkType}"));
             * 
             * ...due to the compiler infering the needed 'new EventHandler..etc.', is the same as:
             * worker.workPerformed += (sender, e) => Console.WriteLine($"Hours worked: {e.Hours}, doing: {e.WorkType}");  
             * 
             * ...which in turn, is the same as:
             * worker.workPerformed += WorkPerformed;
             *
             */
            #endregion

            var worker = new Worker();
            worker.workPerformed += (sender, e) => Console.WriteLine($"Hours worked: {e.Hours}, doing: {e.WorkType}");
            worker.workCompleted += (sender, e) => Console.WriteLine("Done!");
            worker.DoWork(8, WorkType.GenerateReports);
        }

        private static IEnumerable<Customer> SeedCustomerData() => new List<Customer>{
                new Customer {Id = 1, City = "Phoenix", FirstName = "John", LastName = "Anderson" },
                new Customer {Id = 2, City = "Denver", FirstName = "Jane", LastName = "Davidson" },
                new Customer { Id = 3, City = "Seattle", FirstName = "Andrew", LastName = "Carmichael" },
                new Customer { Id = 4, City = "New York", FirstName = "Mustafa", LastName = "Ahmed"}};

        private static void WorkCompleted(object sender, EventArgs e) =>
            Console.WriteLine("Done!");

        private static void WorkPerformed(object sender, WorkPerformedEventArgs e) =>
            Console.WriteLine($"Hours worked: {e.Hours}, doing: {e.WorkType}");
    }
}
