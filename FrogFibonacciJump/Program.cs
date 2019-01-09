using System;
using System.Collections.Generic;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution
{
    public static void Main(string[] args)
    {
        int[] intArray = new int[args.Length];
        for(int i = 0; i < args.Length; i++)
        {
            intArray[i] = int.Parse(args[i]);
        }
        solution(intArray);
    }
    public static int solution(int[] A)
    {
        // write your code in C# 6.0 with .NET 4.5 (Mono)

        if(A.Length == 0)
        {
            return 1;
        }
        if(A.Length == 1)
        {
            return 1;
        }

        //Get fibonacci numbers
        var fib = GetFibonacciNumbers(A.Length);

        int[] reached = new int[A.Length];

        foreach(var fibVal in fib)
        {
            if(fibVal - 1 == A.Length)
            {
                return 1;
            }
            if (fibVal != 0 && fibVal - 1 < A.Length && A[fibVal - 1] == 1)
            {
                reached[fibVal - 1] = 1; // First jump from the beginning
            }
        }

        int minJumps = int.MaxValue;

        for(int index = 0; index < A.Length; index++)
        {
            int jumpsToHere = reached[index];
            //We can't get here so leave this one
            if (jumpsToHere == 0)
            {
                continue;
            }
            
            foreach(var step in fib)
            {
                int newPosition = index + step;
                if(newPosition == A.Length)
                {
                    minJumps = Math.Min(minJumps, jumpsToHere + 1);
                    break;
                }

                //Do we jump further than the end or not end on a leaf? Leave this one if so.
                if(newPosition > A.Length || A[newPosition] == 0)
                {
                    continue;
                }

                //We must be on a leaf if we are here..
                if (reached[newPosition] == 0)
                {
                    reached[newPosition] = jumpsToHere + 1;
                }
                else
                {
                    reached[newPosition] = Math.Min(jumpsToHere + 1, reached[newPosition]);
                }

            }
        }
        if(minJumps == int.MaxValue)
        {
            return -1;
        }
        return minJumps;

    }

    public static List<int> GetFibonacciNumbers(int N)
    {
        List<int> fib = new List<int>();
        fib.Add(0);
        fib.Add(1);
        int latestValue = 1;
        int currentIndex = 1;
        while(latestValue <= N + 1)
        {
            currentIndex++;
            latestValue = fib[currentIndex - 1] + fib[currentIndex - 2];
            if (latestValue <= N + 1)
            {
                fib.Add(latestValue);
            }
        }
        return fib;
    }
}