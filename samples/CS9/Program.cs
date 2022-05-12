namespace CS9 {
  class Program {
    // Relational patterns (>= operator et al.)
    // Logical patterns ("or", also "and" and "not")
    //   Note: parens ( and ) can be used to determine precedence
    bool IsPastMidYear(DateTime dt) => dt is { Month: >= 7 };
    bool IsThisYearOrLast(DateTime dt) => dt is { Year: 2022 or 2021 };
    bool IsSummer(DateTime dt) => dt is
    { Month: 7 or 8 } or
    { Month: 6, Day: >= 21 } or
    { Month: 9, Day: < 21 };

    // Class with "Deconstruct" implementation
    class Order {
      public int ItemCount { get; set; }
      public double ItemPrice { get; set; }
      public void Deconstruct(out int itemCount, out double itemPrice) {
        itemCount = ItemCount;
        itemPrice = ItemPrice;
      }
    }

    public enum OrderValue {
      ValuableDueToHighCount,
      ValuableDueToHighItemPrice,
      ValuableDueToHighTotal,
      NotValuable,
    }

    // Positional patterns with placeholders - note that
    //   the element lists must be "complete"!
    static OrderValue OrderValueCategory(Order o) =>
      o switch
      {
        ( < 0, _) => throw new ArgumentException("Positive itemCounts please!"),
        (_, < 0) => throw new ArgumentException("Positive itemPrices please!"),
        ( >= 100, _) => OrderValue.ValuableDueToHighCount,
        (_, >= 1000) => OrderValue.ValuableDueToHighItemPrice,

        // Assign variable names for further processing
        var (c, p) when c * p > 1000 => OrderValue.ValuableDueToHighTotal,

        _ => OrderValue.NotValuable
      };


    static void Main(string[] args) {
    }
  }
}
