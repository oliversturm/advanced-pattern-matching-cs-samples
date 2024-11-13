// ReSharper disable All

namespace CS8 {
  class Program {
    // This is a "switch expression"
    static int CalcResult(int input) => input switch
    {
      1 => 2,
      2 => 3,
      _ => throw new ArgumentException("Rien ne va plus")
    };

    // Property Pattern:
    bool IsJune(DateTime dt) => dt is { Month: 6 };

    public class Address {
      public string? City { get; set; }
      public string? Country { get; set; }
    }

    public class Customer {
      // ...
      public Address? Address { get; set; }
    }

    static void Main(string[] args) {
      // This is a tuple, supported since C# 7
      (string, string) personInfo = ("Oli", "Sturm");
      var (firstName, lastName) = personInfo;
      // firstName ist jetzt "Oli" und lastName ist "Sturm"

      // Positional pattern (using a tuple - hence "tuple pattern" in
      // this particular case)
      // Important: this works with any deconstructable type! Example
      // for a class with a deconstructor included in CS9 sample.
      if (personInfo is ("Oli", var olisLastName))
        Console.WriteLine($"Found Oli, his last name is {olisLastName}.");

      // Nested property patterns:
      var customer = new Customer { Address = new Address { City = "Castle Douglas", Country = "UK" } };
      if (customer is { Address: { City: "Castle Douglas" } }) {
        Console.WriteLine("This customer lives around the corner");
      }

      // Prize question: what is the point of this?
      if (customer.Address is { } a) {
        // Work with a -- it is not null and matches the (empty, but
        //   extensible) property pattern.
        // a is a copy of the object reference, which may be useful in multi-
        //   threading scenarios.
        // Some people really like this pattern, but in most cases
        // if (customer.Address is not null) { ... } is just a good.
        // (And btw, "is not null" is not really faster than "!= null" -- but
        //   it could be, and it expresses intention more explicitly.

        // Perhaps best answer: it's like a "with" statement, allowing
        // short access to the property path now. And note: a is not
        // an empty object here, it's the same type as the source
        // at customer.Address.
        Console.WriteLine($"Address.City is {a.City}");
      }
    }
  }
}