// ReSharper disable All

namespace CS9
{
  class Program
  {
    // Relational patterns (>= operator et al.)

    // Logical patterns ("or", also "and" and "not")
    //   Note: parens ( and ) can be used to determine precedence
    bool IsPastMidYear(DateTime dt) => dt is { Month: >= 7 };

    bool IsThisYearOrLast(DateTime dt) => dt is { Year: 2022 or 2021 };

    bool IsSummer(DateTime dt) =>
      dt is { Month: 7 or 8 } or { Month: 6, Day: >= 21 } or { Month: 9, Day: < 21 };

    // Class with "Deconstruct" implementation
    class Order
    {
      public int ItemCount { get; set; }
      public double ItemPrice { get; set; }

      public void Deconstruct(out int itemCount, out double itemPrice)
      {
        itemCount = ItemCount;
        itemPrice = ItemPrice;
      }
    }

    public enum OrderValue
    {
      ValuableDueToHighCount,
      ValuableDueToHighItemPrice,
      ValuableDueToHighTotal,
      NotValuable,
    }

    // Positional patterns with "discard" placeholders - note that
    //   the element lists must be "complete"!
    static OrderValue OrderValueCategory(Order o) =>
      o switch
      {
        // Labels are optional here but can be useful
        // for readability
        Order(itemCount: < 0, itemPrice: _) => throw new ArgumentException(
          "Positive itemCounts please!"
        ),
        (_, < 0) => throw new ArgumentException("Positive itemPrices please!"),
        (>= 100, _) => OrderValue.ValuableDueToHighCount,
        (_, >= 1000) => OrderValue.ValuableDueToHighItemPrice,

        // Assign variable names for further processing
        var (c, p) when c * p > 1000 => OrderValue.ValuableDueToHighTotal,

        _ => OrderValue.NotValuable,
      };

    static void Main(string[] args)
    {
      var order = new Order { ItemCount = 30, ItemPrice = 50 };
      // Let's see if this order is valuable
      var category = OrderValueCategory(order);
      Console.WriteLine($"This order is categorized as: {category}");

      // Let's see that linked list
      LinkedList<int> list = new LinkedList<int>.Node(
        1,
        new LinkedList<int>.Node(2, new LinkedList<int>.Node(3, new LinkedList<int>.Empty()))
      );
      PrintList(list);
      PrintListWithPropertyMatch(list);
    }

    // disable exhaustion check - we know these matches are exhaustive for our purposes, but the compiler doesn't
#pragma warning disable CS8509

    static void PrintList<T>(LinkedList<T> list)
    {
      string getPart(LinkedList<T> l) =>
        l switch
        {
          LinkedList<T>.Node(var value, var next) => $"(N {value} {getPart(next)})",
          LinkedList<T>.Empty => "(E)",
        };
      Console.WriteLine(getPart(list));
    }

    // Technically better, but more verbose - property match syntax means that we don't rely on position alone
    static void PrintListWithPropertyMatch<T>(LinkedList<T> list)
    {
      string getPart(LinkedList<T> l) =>
        l switch
        {
          LinkedList<T>.Node { Value: var value, Next: var next } => $"(N {value} {getPart(next)})",
          LinkedList<T>.Empty => "(E)",
        };
      Console.WriteLine(getPart(list));
    }
  }
#pragma warning restore CS8509

  // Linked list with discriminated unions based on records
  public abstract record LinkedList<T>
  {
    public sealed record Node(T Value, LinkedList<T> Next) : LinkedList<T>;

    public sealed record Empty : LinkedList<T>;
  }
}
