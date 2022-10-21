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

      // Positional pattern (using a tuple - hence "tuple pattern" in this special case)
      // Important: this works with any deconstructable type! Example coming up in CS9 sample.
      if (personInfo is ("Oli", var olisLastName))
        Console.WriteLine($"Found Oli, his last name is {olisLastName}.");

      // Nested property patterns:
      var customer = new Customer
      {
        Address = new Address { City = "Castle Douglas", Country = "UK" }
      };
      if (customer is { Address: { City: "Castle Douglas" } }) {
        Console.WriteLine("This customer lives around the corner");
      }
    }
  }
}
