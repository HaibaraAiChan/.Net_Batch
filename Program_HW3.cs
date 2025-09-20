//Questions
//1.Dscribe the problem generics address.
/*  Generics address the problem of type safety and code reusability in programming. 
    They allow developers to create classes, methods, and data structures that can operate 
        on different data types while ensuring type safety at compile time. 
    This means that you can write a single piece of code that works with various types 
        without sacrificing performance or risking runtime errors due to type mismatches. 
    Generics help eliminate the need for casting and boxing, which can lead to 
        performance overhead and potential errors. 
    By using generics, developers can create more flexible and maintainable code 
        that is easier to read and understand.
*/
//2.How would you create a list of strings, using the generic List class?
// List<string> myStringList = new List<string>();

//3.How many generic type parameters does the Dictionary class have?
// The Dictionary class has two generic type parameters: TKey and TValue. Dictionary<TKey, TValue>


//4.True/False. When a generic class has multiple type parameters, they must all match.
/* False. When a generic class has multiple type parameters, they do not all have to match.
            Each type parameter can be different and can represent different types.
            Dictionary<TKey, TValue> ：
            TKey can be string 
            TValue can be int 
*/
//5.What method is used to add items to a List object?
/* The method used to add items to a List object is the Add() method.
    Example:
    List<int> myList = new List<int>();
    myList.Add(10);
    myList.Add(20);
*/

//6.Name two methods that cause items to be removed from a List.
/* Two methods that cause items to be removed from a List are:
    1. Remove(T item): Removes the first occurrence of a specific object from the List.
    2. RemoveAt(int index): Removes the element at the specified index of the List.
*/

//7.How do you indicate that a class has a generic type parameter?
// Use angle brackets <>


//8.True/False. Generic classes can only have one generic type parameter.
/* False. Generic classes can have multiple generic type parameters.
    Example:
    public class MyGenericClass<T1, T2>
    {
        // Class implementation
    }
*/

//9. True/False. Generic type constraints limit what can be used for the generic type.
/* True. Generic type constraints limit what can be used for the generic type.
     Constraints can specify that the type must be a reference type, a value type, 
     have a parameterless constructor, or inherit from a specific base class or implement a specific interface.
     Example:
     public class MyGenericClass<T> where T : class
     {
         // Class implementation
     }
*/
//10.True/False. Constraints let you use the methods of the thing you are constraining to.
/* True. Constraints let you use the methods of the thing you are constraining to.
    Example:
    public class MyGenericClass<T> where T : IDisposable
    {
        public void DisposeItem(T item)
        {
            item.Dispose(); // You can call Dispose() because of the constraint
        }
    }
    where T : class - T  should be a reference type
    where T : struct - T should be a value type
    where T : new() - T should have a parameterless constructor
    where T : BaseClass - T should inherit from BaseClass
    where T : IxxxInterface - T should implement IxxxInterface
*/


//Practice Exercise
//Task 1:
// Define a generic class called MyStack<T> with the following requirements:
// Use Stack<T> internally to store the data.
//Implement a Count() method that returns the number of elements in the stack.
//Implement a Pop() method that returns and removes the top element of the stack.
//Implement a Push(T obj) method that adds an element to the stack.
//Finally, create an instance of MyStack<int>, push two integers into it, and print out the current number of elements in the stack.

using System;
using System.Collections.Generic;

public class MyStack<T>
{
    private Stack<T> stack;
    public MyStack()
    {
        stack = new Stack<T>();
    }
    public int Count()
    {
        return stack.Count;
    }
    public T Pop()
    {
        return stack.Pop();
    }
    public void Push(T obj)
    {
        stack.Push(obj);
    }
}

class Program
{
    static void Main()
    {
        
        var _stack = new MyStack<int>();
        _stack.Push(10);
        _stack.Push(20);
        Console.WriteLine($"Current number of elements in the stack: {_stack.Count()}");

        
        Console.WriteLine($"Popped: {_stack.Pop()}");
        Console.WriteLine($"Remaining elements: {_stack.Count()}");
    }
}




//Task 2:
// Create a generic repository pattern in C# with the following requirements:
//Define a generic interface IGenericRepository<T> where T : class.
//The interface should declare the following methods:
//Add(T item)
//Remove(T item)
//Save()
//IEnumerable<T> GetAll()
//T GetById(int id)
public interface IGenericRepository<T> where T : class
{
    void Add(T item);
    void Remove(T item);
    void Save();
    IEnumerable<T> GetAll();
    T GetById(int id);
}






//Implement a class GenericRepository<T> that inherits from IGenericRepository<T>.
//Use a private List<T> field to store the data.
//In the constructor, initialize the list as a new empty List<T>.
//Provide method implementations for Add, Remove, GetAll, GetById.No actual implementation is needed for Save.

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private List<T> items;
    //private IDbConnection dbConnection; // Assuming you have a database connection
    public GenericRepository()
    {
        items = new List<T>();
    }
    public void Add(T item)
    {
        items.Add(item);
    }
    public void Remove(T item)
    {
        items.Remove(item);
    }
    public void Save()
    {
        // No actual implementation needed for Save
    }
    public IEnumerable<T> GetAll()
    {
        return items;
    }
    public T GetById(int id)
    {
        // Assuming T has a property named "Id" of type int
        return items.FirstOrDefault(item => (int)item.GetType().GetProperty("Id").GetValue(item) == id);
    }
}