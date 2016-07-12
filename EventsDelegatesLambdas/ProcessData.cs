using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesLambdas
{
    public class ProcessData
    {
        public static void Process(int x, int y, BizRulesDelegate del)
        {
            var result = del(x, y);
            Console.WriteLine(result);
        }

        public static void Process(int x, int y, Action<int, int> action)
        {
            Console.Write("Action processed: ");
            action(x, y);
        }

        public static void Process(int x, int y, Func<int, int, int> function)
        {
            var result = function(x, y);
            Console.WriteLine($"Function processed: {result}");
        }
    }
}
