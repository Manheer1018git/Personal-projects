using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace MinDequesNumber
{
    public class MinDequesNumber
    {
        public static int Solve(int[] data)
        {
            // we start our solve method by declaring two arrays which store the first and the last elements of all the deques created
            int[] First_elements = new int[data.Length];
            int[] Last_elements = new int[data.Length];

            // we declare an integer variable count which we use to calculate the minimum number of deques used
            int Count = 0;

            // we start our first for loop to iterate through the entire array of data
            for (int i = 0; i < data.Length; i++)
            {
                // we initialize a bool to check whether we added the element to an existing deque or not
                bool Element_added = false;

                // we declare a for loop to iterate through the number of deques we have created
                for (int j = 0; j < Count; j++)
                {
                    // we check if the element of data is smaller than the first element of the current deque
                    if (data[i] < First_elements[j])
                    {
                        // we initialize a bool to check if the addition of the element to the current deque[j] is possible or not
                        bool Addition_possible = true;

                        // we use a for loop to go through all the elements of the array of data
                        for (int k = 0; k < data.Length; k++)
                        {
                            // we check if any element of data is between the first element of the current deque and the data[i] which we need to add
                            // we do this to maintain the ascending order of the array at the end
                            // if not, we add the element at the index i to the deque
                            if (data[i] < data[k] && data[k] < First_elements[j])
                            {
                                Addition_possible = false;
                                break;
                            }
                        }
                        if (Addition_possible)
                        {
                            First_elements[j] = data[i];
                            Element_added = true;
                            break;
                        }
                    }
                    // we check if the element of data is greater than the last element of the current deque
                    if (data[i] > Last_elements[j])
                    {
                        // we initialize a bool to check if the addition of the element to the current deque[j] is possible or not
                        bool Addition_possible = true;

                        // we use a for loop to go through all the elements of the array of data
                        for (int k = 0; k < data.Length; k++)
                        {
                            // we check if any element of data is between the last element of the current deque and the data[i] which we need to add
                            // we do this to maintain the ascending order of the array at the end
                            // if not, we add the element at the index i to the current deque
                            if (data[i] > data[k] && data[k] > Last_elements[j])
                            {
                                Addition_possible = false;
                                break;
                            }
                        }
                        if (Addition_possible)
                        {
                            Last_elements[j] = data[i];
                            Element_added = true;
                            break;
                        }
                    }
                }
                // if the element cannot be added, we increase the count and add the current element to our array of the first and last elements as a new deque
                if (!Element_added)
                {
                    First_elements[Count] = data[i];
                    Last_elements[Count] = data[i];
                    Count++;
                }
            }

            return Count;
        }

    }
}
