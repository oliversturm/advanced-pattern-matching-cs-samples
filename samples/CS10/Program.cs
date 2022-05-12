namespace CS10 {
  class Program {
    // Nested types, just like the C# 8 sample:
    public class Address {
      public string? City { get; set; }
      public string? Country { get; set; }
    }

    public class Customer {
      // ...
      public Address? Address { get; set; }
    }

    static void Main(string[] args) {
      // Nested property patterns, now shorter:
      var customer = new Customer
      {
        Address = new Address { City = "Castle Douglas", Country = "UK" }
      };
      if (customer is { Address.City: "Castle Douglas" }) {
        Console.WriteLine("This customer lives around the corner");
      }
    }
  }
}
