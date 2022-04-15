using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Remember to use debugger that was talked about in class and step in each function to find and fix the error.
            // These are here to help debug. Try to get all the test cases to pass.

            DebugExample debug = new DebugExample();
            //Should return Hello Word
            Console.WriteLine(debug.RemoveCharIndex("Hello World", 9));
            //Should return Norman
            string[] sheepNames = new string[] { "Norman", "Cooper", "Ferdinand", "Roger", "Norman", "Dougal", "Diablo", "Shrek", "Oliver", "Tom", "Linus", "Dougal", "Huck", "Owen", "Owen", "Russel", "Luke", "Raymond", "Tom", "Lars", "Norman" };
            Console.WriteLine(debug.CountingSheep(sheepNames));
            //Should return an integer
            Console.WriteLine(debug.MathOperations()); // should be int and it is. 
            //Should return 1.524
            Console.WriteLine(debug.ConvertFromInchesToMeters(60));
            Console.ReadLine();
        }
    }
}
