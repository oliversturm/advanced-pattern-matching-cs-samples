// ReSharper disable All

namespace CS11 {
  class Program {
    static void Main(string[] args) {
      int[] numbers = { 1, 3, 42 };
      // Exact match
      Console.WriteLine($"numbers is [1,3,42]: {numbers is [1, 3, 42]}");
      // Must be correct length, so this is false
      Console.WriteLine($"numbers is [1,3]: {numbers is [1, 3]}");
      // Can use wildcards
      Console.WriteLine($"numbers is [1,3,_]: {numbers is [1, 3, _]}");
      // Still must be correct length!
      Console.WriteLine($"numbers is [1,3,_,_]: {numbers is [1, 3, _, _]}");

      // Really don't care about length? Use ..
      Console.WriteLine($"numbers is [..,42]: {numbers is [.., 42]}");

      // Cool stuff, match with embedded patterns - this is true
      Console.WriteLine($"numbers is [1,3,>10]: {numbers is [1, 3, > 10]}");
      // ... and this is false
      Console.WriteLine($"numbers is [1,3,>100]: {numbers is [1, 3, > 100]}");

      // You can split a list into head and tail:
      if (numbers is [var x, .. var xs]) {
        Console.WriteLine($"Head: {x}");
        Console.WriteLine($"Tail: {xs}");
      }

      // So we can do nice functional-style stuff:
      Console.WriteLine($"Sum of numbers: {Sum(numbers)}");

      // Talking about spans -- the second new pattern matching feature in
      // C# 11 is related to spans and strings.

      string WhatsNext(ReadOnlySpan<char> spanString) => spanString switch {
        "one" => "two",
        "two" => "three",
        _ => "That's it!"
      };

      Console.WriteLine($"One. Next: '{WhatsNext("one".AsSpan())}'");
    }

    // Now we're talking -- note that the use of Span<T> magically solves
    // a perf problem you would have if you used an Array type here.
    static int Sum(Span<int> l) => l switch {
      [] => 0,
      [var x, .. var xs] => x + Sum(xs)
    };
  }
}