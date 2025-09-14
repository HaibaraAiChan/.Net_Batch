

//All the answers should be written inside Program.cs.
//Short-answer (theory) questions → should be written as comments (// or /* ... */).


//oop questions → all classes should be placed in the same Program.cs
//OOP Q&A
//1.What are the six combinations of access modifier keywords and what do they do?
/*
 * public, private, protected, internal, protected internal, private protected
 * */

//2.What is the difference between the static, const, and readonly keywords when applied to a type member?
/*
 * static: belongs to the type itself rather than to a specific instance of the type
 * const: compile-time constant, must be initialized at the time of declaration
 * readonly: runtime constant, can be initialized in the constructor
 * */

//What does a constructor do?
/*
 * A constructor is a special method that is called when an instance of a class is created. 
 * It is used to initialize the object's properties and allocate resources.
 * */

//Why is the partial keyword useful?
/*
 * The partial keyword allows a class, struct, or interface to be split into multiple files. 
 * This is useful for organizing large classes or for separating auto-generated code from user-written code.
 * */

//What is a tuple?
/*
 * A tuple is a data structure that can hold a fixed number of items of different types. 
 * It is useful for returning multiple values from a method or for grouping related data together.
 * */

//What does the C# record keyword do?
/*
 * The record keyword is used to define a reference type that provides built-in functionality for value-based equality, 
 * immutability, and concise syntax for defining data-centric types.
 * */

//What does overloading and overriding mean?
/*
 * Overloading: defining multiple methods with the same name but different parameter lists
 * Overriding: providing a new implementation for a method in a derived class that is already defined in the base class
 * */

//What is the difference between a field and a property?
/*
 * Field: a variable that is declared directly in a class or struct
 * Property: a member that provides a flexible mechanism to read, write, or compute the value of a private field
 * */

//How do you make a method parameter optional?
/*
 * By providing a default value for the parameter in the method signature
 * */

//What is an interface and how is it different from an abstract class?
/*
 * Interface: a contract that defines a set of methods and properties that a class must implement.
 *                  C# 8.0 and later versions allow interfaces to have default implementations for methods.
 * Abstract class: a class that cannot be instantiated and may contain abstract methods that must be implemented by derived classes.
 *                  the abstract class can also contain concrete methods with implementations.
 *                  but the abstract function can't have a body, the derived class must provide the body.
 * */

//What accessibility level are members of an interface by default ?
/*
 * public
 * */

//True / False : Polymorphism allows derived classes to provide different implementations of the same method.
/*
 * True
 * */

//True/False: The override keyword is used to indicate that a method in a derived class is providing its own implementation.
/*
 * True
 * */

//True / False: The new keyword is used to indicate that a method in a derived class is providing its own implementation.
/*
 * False
 * */

//True / False: Abstract methods can be used in a normal (non-abstract) class.
/*
 * False
 * */

//True / False: Normal(non -abstract) methods can be used in an abstract class.
/*
 * True
 * */

//True / False: Derived classes can override methods that were virtual in the base class.
/*
 * True
 * */

//True / False: Derived classes can override methods that were abstract in the base class.
/*
 * True
 * */

//True / False: Derived classes must override the abstract methods from the base class.
/*
 * True
 * */

//True / False: In a derived class, you can override a method that was neither virtual nor abstract in the base class.
/*
 * False
 * */

//True / False: A class that implements an interface does not have to provide an implementation for all of the members of the interface.
/*
 * False
 * */

//True / False: A class that implements an interface is allowed to have other members in addition to the interface members.
/*
 * True
 * */

//True / False: A class can inherit from more than one base class.
/*
 * False
 * */

//True / False: A class can implement more than one interface.
/*
 * True
 * */

//Create 3 classes in Program.cs:
//a.Person class
//--Create an abstract class Person with the following members:
//----An Id property (int).
//----A private field salary with a public property Salary that only accepts positive values; throw an exception if a negative value is assigned.
//----A DateOfBirth property (DateTime).
//----An Address property (List of strings).
public abstract class  Person
{
    public int Id;
    private decimal salary;
    private DateTime DateOfBirth;
    private List<string> Address = new List<string>();
    public decimal Salary
    {
        get { return salary; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Salary must be a positive value.");
            }
            salary = value;
        }
    }

}


//b. Instructor class
//Create a class Instructor that inherits from Person.
//Add a DepartmentId property (int).

public class Instructor : Person
{
    public int DepartmentId { get; set; }
}


//c. Student class
//Create a class Student that inherits from Person.
//Add a property SelectedCourses, which is a list of Course objects.

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
}
public class  Student: Person
{
    public List<Course> SelectedCourses { get; set; } = new List<Course>();
    
    public List<Course> SelectCourses()
    {
        return SelectedCourses;
    }
    
}