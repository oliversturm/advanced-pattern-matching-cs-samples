namespace CS8 {
  class Program {
    // This is a "switch expression"
    static int CalcResult(int input) => input switch
    {
      1 => 2,
      2 => 3,
      _ => throw new ArgumentException("Weiter geht's nicht")
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
      // Positional pattern (using a tuple - hence "tuple pattern")
      // Important: this works with any deconstructable type!
      (string, string) personInfo = ("Oli", "Sturm");
      var (firstName, lastName) = personInfo;
      // firstName ist jetzt "Oli" und lastName ist "Sturm"

      // Nested property patterns:
      var customer = new Customer
      {
        Address = new Address { City = "Castle Douglas", Country = "UK" }
      };
      if (customer is { Address: { City: "Castle Douglas" } }) {
        Console.WriteLine("Dieser Kunde wohnt gleich um die Ecke");
      }
    }
  }
}
