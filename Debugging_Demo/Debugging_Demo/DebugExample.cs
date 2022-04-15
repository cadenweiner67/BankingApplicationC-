using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging_Demo
{
    public class DebugExample
    {
        /// <summary>
        /// Takes a index and string and deletes the char at that index from the string.
        /// </summary>
        /// <param name="baseString">the original string.</param>
        /// <param name="indexLoc">The index to delete at.</param>
        /// <returns>Modified baseString.</returns>
        public string RemoveCharIndex(string baseString, int indexLoc)
        {
            for(int i = 0; i < baseString.Length; i++)
            {
                if(i == indexLoc)
                {
                    baseString = baseString.Remove(i, 1);
                }
            }
            return baseString;
        }
        /// <summary>
        /// Converts Inches to Meters.
        /// </summary>
        /// <param name="inches">the given measurement of inches.</param>
        /// <returns>a conversion to meters.</returns>
        public double ConvertFromInchesToMeters(int inches)
        {
            double meters = ((double) inches) * 0.0254F;
            return meters;
        }

        /// <summary>
        /// This function finds the most common name out of the sheep array.
        /// </summary>
        /// <param name="sheepArray">a array of sheep names.</param>
        /// <returns>the most common sheep name.</returns>
        public string CountingSheep(string[] sheepArray)
        {
            //Loops through array and makes a frequency table of names
            Dictionary<string, int> sheepNames = new Dictionary<string, int>();
            foreach (string sheep in sheepArray)
            {
                if(sheepNames.ContainsKey(sheep))
                {
                    sheepNames[sheep] = sheepNames[sheep] + 1; // should add for a frequency table.
                }
                else
                {
                    sheepNames.Add(sheep, 1);
                }
            }
            //Finds the most common sheep name
            KeyValuePair<string, int> mostCommonName = new KeyValuePair<string, int>("Bob", 0);
            foreach (var index in sheepNames)
            {
                if (index.Value > mostCommonName.Value)
                {
                    mostCommonName = index;
                }
            }
            return mostCommonName.Key.ToString();
        }
        /// <summary>
        /// Does basic math operations.
        /// </summary>
        /// <returns>the final result.</returns>
        public int MathOperations()
        {
            //You can set the number z to a different value
            int x = 10;
            int y = 3;
            int z = 3;

            int a = 20 / (x - (y + z));
            return a;
        }

        
    }
}
