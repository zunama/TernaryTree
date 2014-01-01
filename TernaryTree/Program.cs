using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TernaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] lines = System.IO.File.ReadAllLines(@"WordList2.txt");

            // shuffling cuts insertion time almost by half on large sets and helps with searches.
            ShuffleArray(lines);

            AutocompleteDictionary tree = new AutocompleteDictionary();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (string line in lines)
                tree.AddWord(line);

            stopWatch.Stop();
            
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Tree Built In: " + elapsedTime);

            Console.WriteLine(string.Format("Loaded into tree: {0}.", tree.WordsStored));
            Console.WriteLine(string.Format("Nodes in tree: {0}.", tree.NodeStored));
            Console.WriteLine(string.Format("Count of words in tree: {0}", tree.GetAllPossibleWords().Count()));

            Console.WriteLine();

            // Looking for all possible words starting with 'univer'
            string startwith = "uni";

            IList<string> possibleWords = new List<string>();

            stopWatch.Restart();

            for (int i = 0; i < 100; i++)
            {
                possibleWords = tree.GetPossibleWords(startwith);
            }

            stopWatch.Stop();

            Console.WriteLine("{0} words starting with {1}:", possibleWords.Count, startwith);
            Console.WriteLine();

            Console.ReadLine();

            foreach (string word in possibleWords)
                Console.WriteLine(word);

            Console.WriteLine();

            ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);

            Console.ReadLine();

        }

        static void ShuffleArray(string[] array)
        {
            for (int i = 0; i < 5; i++)
            {
                Random gen = new Random();
                int end = array.Length;
                while (end > 1)
                {
                    int randomIndex = gen.Next(end--);
                    string temp = array[end];
                    array[end] = array[randomIndex];
                    array[randomIndex] = temp;
                }
            }

        }
    }
}
