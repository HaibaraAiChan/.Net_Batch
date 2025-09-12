//HW1 for C#

//###  Introduction to C# and Data Types – Questions
//01 What type would you choose for the following “numbers”?

//A person’s telephone number
/* string */

//A person’s height
/* float or double */

//A person’s age
/* byte, short, int (depending on the expected range)*/

//A person’s gender (Male, Female, Prefer Not To Answer)
// enum or string

//A person’s salary
// decimal

//A book’s ISBN
// string

//A book’s price
// decimal

//A book’s shipping weight
// float or double

//A country’s population
// int or long (depending on the expected range)

//The number of stars in the universe
// long or BigInteger 

//The number of employees in each of the small or medium businesses in the United Kingdom
// (up to about 50,000 employees per business)
// int

//2. What are the differences between value type and reference type variables?
/* --Value types store the actual data, 
 * --while reference types store a reference (or address) to the data. 
 * --Value types are typically stored on the stack, 
 * --while reference types are stored on the heap. 
 * --Value types include primitive types like int, float, and struct, 
 * --while reference types include classes, arrays, and strings. 
 */
// What is boxing and unboxing?
/* --Boxing is the process of converting a value type to a reference type by wrapping it in an object. 
 * --Unboxing is the reverse process of extracting the value type from the object. 
 * --Boxing and unboxing can have performance implications, as boxing involves memory allocation on the heap.
 */


//3. What is meant by the terms managed resource and unmanaged resource in .NET?
/* --Managed resources are those that are handled by the .NET runtime's garbage collector, 
 * --which automatically manages memory allocation and deallocation for these resources. 
 * --Examples of managed resources include objects created from .NET classes, such as strings, arrays, and custom objects.
 * 
 * --Unmanaged resources, on the other hand, are resources that are not directly managed by the .NET runtime. 
 * --These resources typically include system resources like file handles, 
 * ----database connections, network sockets, and memory allocated through native code (e.g., using C or C++ libraries). 
 * --Since unmanaged resources are not automatically cleaned up by the garbage collector, 
 * -----developers need to explicitly release them when they are no longer needed, 
 * -----often by implementing the IDisposable interface and using the Dispose pattern.
 */

//4. What is the purpose of the Garbage Collector in .NET?
/* --The Garbage Collector (GC) in .NET is responsible for automatic memory management. 
 * --Its primary purpose is to identify and reclaim memory that is no longer in use by the application, 
 * --thereby preventing memory leaks and optimizing memory usage. 
 * --The GC periodically checks for objects that are no longer reachable from the application's root references 
 * -----and frees up the memory occupied by those objects. 
 * --This allows developers to focus on writing code without having to manually manage memory allocation and deallocation,
 * -----reducing the likelihood of memory-related errors.
 */

//### Controlling Flow and Converting Types – Questions
//What happens when you divide an int variable by 0?
/* It throws a DivideByZeroException. */

//What happens when you divide a double variable by 0?
/* It results in positive infinity (Infinity) or negative infinity (-Infinity) depending on the sign of the numerator. 
 * If the numerator is zero, it results in NaN (Not a Number). */

//What happens when you overflow an int variable (assign a value beyond its range)?
/* 1.It wraps around to the minimum value of the int type (i.e., it behaves like modulo arithmetic).
*  2.If checked context is used, it throws an OverflowException.
*/

//What is the difference between x = y++; and x = ++y;?
/* 
 * 1. x = y++; is a post-increment operation. It assigns the current value of y to x, 
 *    and then increments y by 1.
 * 2. x = ++y; is a pre-increment operation. It increments y by 1 first, 
 *    and then assigns the new value of y to x.
 */

//What is the difference between break, continue, and return when used inside a loop statement?
/* 
 * 1. break: It immediately exits the loop, terminating its execution and transferring control 
 *           to the statement following the loop.
 * 2. continue: It skips the current iteration of the loop and proceeds to the next iteration, 
 *           effectively ignoring any remaining code in the loop body for that iteration.
 * 3. return: It exits the entire method in which the loop is contained, returning control to the caller of the method. 
 *           If used inside a loop, it will terminate both the loop and the method.
 */

//What are the three parts of a for statement and which of them are required?
/* 
 * 1. Initialization: This part is executed once at the beginning of the loop. 
 *                    It is used to initialize loop control variables.
 * 2. Condition: This part is evaluated before each iteration of the loop. 
 *               If the condition is true, the loop body is executed; 
 *               if false, the loop terminates.
 * 3. Iteration: This part is executed after each iteration of the loop body. 
 *               It is typically used to update loop control variables.
 * 
 * Only the condition part is required; the initialization and iteration parts are optional.
 */

//What is the difference between the = and == operators?
/* 
 * 1. = is the assignment operator, used to assign a value to a variable.
 * 2. == is the equality operator, used to compare two values for equality.
 */

//Does the following statement compile? for ( ; true; ) ;
/* Yes, it compiles. This is an infinite loop that will run indefinitely because the condition is always true. 
 * The initialization and iteration parts are omitted, which is allowed in a for loop.
 */

//What interface must an object implement to be enumerated by the foreach statement?
/* The object must implement the IEnumerable interface to be enumerated by the foreach statement. 
 * Additionally, the IEnumerator interface is used to provide the actual iteration functionality.
 */

//Coding：
//1. How can we find the minimum and maximum values, as well as the number of bytes,
//for the following data types: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal?
/* You can use the MinValue and MaxValue properties of each data type to find the minimum and maximum values. 
 * The number of bytes can be found using the sizeof operator. 
 * Here is an example code snippet that demonstrates this:  
 */
//using System;
//using System.Runtime.CompilerServices;

//class Program
//{
//    static void Main()
//    {
//        PrintTypeInfo<sbyte>();
//        PrintTypeInfo<byte>();
//        PrintTypeInfo<short>();
//        PrintTypeInfo<ushort>();
//        PrintTypeInfo<int>();
//        PrintTypeInfo<uint>();
//        PrintTypeInfo<long>();
//        PrintTypeInfo<ulong>();
//        PrintTypeInfo<float>();
//        PrintTypeInfo<double>();
//        PrintTypeInfo<decimal>();
//    }

//    static void PrintTypeInfo<T>() where T : struct
//    {
//        Console.WriteLine($"Type: {typeof(T).Name}");
//        Console.WriteLine($"Min Value: {GetMinValue<T>()}");
//        Console.WriteLine($"Max Value: {GetMaxValue<T>()}");
//        Console.WriteLine($"Size (bytes): {Unsafe.SizeOf<T>()}");
//        Console.WriteLine();
//    }

//    static object GetMinValue<T>() where T : struct => typeof(T) switch
//    {
//        var t when t == typeof(sbyte) => sbyte.MinValue,
//        var t when t == typeof(byte) => byte.MinValue,
//        var t when t == typeof(short) => short.MinValue,
//        var t when t == typeof(ushort) => ushort.MinValue,
//        var t when t == typeof(int) => int.MinValue,
//        var t when t == typeof(uint) => uint.MinValue,
//        var t when t == typeof(long) => long.MinValue,
//        var t when t == typeof(ulong) => ulong.MinValue,
//        var t when t == typeof(float) => float.MinValue,
//        var t when t == typeof(double) => double.MinValue,
//        var t when t == typeof(decimal) => decimal.MinValue,
//        _ => throw new NotSupportedException($"Type {typeof(T)} not supported")
//    };

//    static object GetMaxValue<T>() where T : struct => typeof(T) switch
//    {
//        var t when t == typeof(sbyte) => sbyte.MaxValue,
//        var t when t == typeof(byte) => byte.MaxValue,
//        var t when t == typeof(short) => short.MaxValue,
//        var t when t == typeof(ushort) => ushort.MaxValue,
//        var t when t == typeof(int) => int.MaxValue,
//        var t when t == typeof(uint) => uint.MaxValue,
//        var t when t == typeof(long) => long.MaxValue,
//        var t when t == typeof(ulong) => ulong.MaxValue,
//        var t when t == typeof(float) => float.MaxValue,
//        var t when t == typeof(double) => double.MaxValue,
//        var t when t == typeof(decimal) => decimal.MaxValue,
//        _ => throw new NotSupportedException($"Type {typeof(T)} not supported")
//    };
//}


//2. Write a method in C# called FizzBuzz that takes an integer num and prints numbers from 1 up to num, but:
//Print Fizz if the number is divisible by 3.
//Print Buzz if the number is divisible by 5.
//Print FizzBuzz if the number is divisible by both 3 and 5.
//Otherwise, print the number itself.


//class Program
//{
//    static void Main()
//    {
//        Program p = new Program();
//        p.FizzBuzz(20); // Replace 20 with any number you want
//    }

//    public void FizzBuzz(int num)
//    {
//        for (int i = 1; i <= num; i++)
//        {
//            if (i % 3 == 0 && i % 5 == 0)
//            {
//                Console.WriteLine("FizzBuzz");
//            }
//            else if (i % 3 == 0)
//            {
//                Console.WriteLine("Fizz");
//            }
//            else if (i % 5 == 0)
//            {
//                Console.WriteLine("Buzz");
//            }
//            else
//            {
//                Console.WriteLine(i);
//            }
//        }
//    }
//}




//3. What will happen if this code executes?
//int max = 500;
//for (byte i = 0; i < max; i++)
//{
//    Console.WriteLine(i);
//}
/* 
 * This code will result in an infinite loop. 
 * The byte data type can only hold values from 0 to 255. 
 * When i reaches 255 and is incremented, it will overflow and wrap around to 0. 
 * Since the condition i < max (500) will always be true for a byte, 
 * the loop will never terminate, causing it to print numbers from 0 to 255 repeatedly.
 */

//4.Two Sum
// Given an array of integers nums and an integer target,
// return indices of the two numbers such that they add up to target.
//You may assume that each input would have exactly one solution.
//You may not use the same element twice.
//You can return the answer in any order.
using System;
using System.Collections.Generic;
class TwoSumProgram { 
    static void Main()
    { 
        TwoSumProgram p = new TwoSumProgram();
        p.TwoSum();
    }
    public void TwoSum()
    {
        Dictionary<int, int> numDict = new Dictionary<int, int>();
        int[] nums = { 2, 7, 11, 15 };
        int target = 9;
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (numDict.ContainsKey(complement))
            {
                Console.WriteLine($"Indices: {numDict[complement]}, {i}");
                return;
            }
            numDict[nums[i]] = i;
        }
        Console.WriteLine("No two sum solution found.");
    }

}