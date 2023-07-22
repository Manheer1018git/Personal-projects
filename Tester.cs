using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinDequesNumber
{
    class Tester
    {
        static void Main(string[] args)
        {
            List<int> correct = new List<int>(TestGenerator.Count());
            List<int> incorrect = new List<int>(TestGenerator.Count());
            int scores = 0;

            for (int i = 0; i < TestGenerator.Count(); i++)
            {
                try
                {
                    int[] a1 = null;
                    int result = TestGenerator.Generate(i, out a1);
                    Console.WriteLine("\nAttempting test instance {0} with data = [{1}]\nThe expected answer is {2}.", i, String.Join(",",a1), result);
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    int answer = MinDequesNumber.Solve(a1);
                    if (result == answer)
                    {
                        scores++;
                        correct.Add(i);
                        Console.WriteLine(" :: SUCCESS (Time elapsed {0})", watch.Elapsed);
                    }
                    else
                    {
                        incorrect.Add(i);
                        Console.WriteLine(" :: FAILED with an incorrect answer of {0}", answer);
                    }
                }
                catch (Exception e)
                {
                    incorrect.Add(i);
                    Console.WriteLine(" :: FAILED with the runtime error {1}", i, e.ToString());
                }
            }

            Console.WriteLine("\nSummary: {0} tests out of {1} passed", scores, TestGenerator.Count());
            Console.WriteLine("Tests passed ({1} to {2}): {0}", correct.Count == 0 ? "none" : string.Join(", ", correct), 0, TestGenerator.Count());
            Console.WriteLine("Tests failed ({1} to {2}): {0}", incorrect.Count == 0 ? "none" : string.Join(", ", incorrect), 0, TestGenerator.Count());

            Console.ReadKey();
        }

    }
}