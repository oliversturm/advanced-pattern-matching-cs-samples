// ReSharper disable All

using System.Text.Json;

namespace CS11 {
  class Program {
    static void Main(string[] args) {
      int[] numbers = { 1, 3, 42 };
      // The following match expressions are "slice patterns" - rather unintuitive naming in my eyes, but what can we do.
      // Exact match
      Console.WriteLine($"numbers is [1, 3, 42]: {numbers is [1, 3, 42]}");
      // Must be correct length, so this is false
      Console.WriteLine($"numbers is [1, 3]: {numbers is [1, 3]}");
      // Can use wildcards
      Console.WriteLine($"numbers is [1, 3, _]: {numbers is [1, 3, _]}");
      // Still must be correct length!
      Console.WriteLine($"numbers is [1, 3, _, _]: {numbers is [1, 3, _, _]}");

      // Really don't care about length? Use ..
      Console.WriteLine($"numbers is [.., 42]: {numbers is [.., 42]}");

      // Cool stuff, match with embedded patterns - this is true
      Console.WriteLine($"numbers is [1, 3, >10]: {numbers is [1, 3, > 10]}");
      // ... and this is false
      Console.WriteLine($"numbers is [1, 3, >100]: {numbers is [1, 3, > 100]}");
      // Other combinations possible, this is true
      Console.WriteLine($"numbers is [.. , <= 10, >= 10]: {numbers is [.., <= 10, >= 10]}");
      // The .. operator can be used in the middle as well. It can also match a zero-length part of the list.
      int[] moreNumbers = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100];
      Console.WriteLine(
        $"moreNumbers is [<= 50, <= 50, .., > 50, >= 100]: {moreNumbers is [<= 50, <= 50, .., > 50, >= 100]}");
      Console.WriteLine(
        $"numbers is [1, 3, .., 42]: {numbers is [1, 3, .., 42]}");
      // Note that .. can be used only once. This is invalid.
      // Console.WriteLine(
      //   $"moreNumbers is [<= 50, <= 50, .., > 50, >= 100]: {moreNumbers is [.., <= 50, <= 50, .., > 50, >= 100]}");

      // And one more strange combination:
      Console.WriteLine($$"""numbers is [1, .. { Length: 2 }]: {{numbers is [1, .. { Length: 2 }]}}""");

      // You can split a list into head and tail:
      if (numbers is [var x, .. var xs]) {
        Console.WriteLine($"Head: {x}");
        Console.WriteLine($"Tail: {xs}");
      }

      // So we can do nice functional-style stuff:
      Console.WriteLine($"Sum of numbers: {Sum(numbers)}");

      // Talking about spans -- the second new pattern matching feature in
      // C# 11 is related to spans and strings.

      string WhatsNext(ReadOnlySpan<char> spanString) => spanString switch
      {
        "one" => "two",
        "two" => "three",
        _ => "That's it!"
      };

      Console.WriteLine($"One. Next: '{WhatsNext("one".AsSpan())}'");

      //----------------------------------------------
      //----------------------------------------------
      // WE'LL GET BACK TO THIS DEMO IN A LITTLE WHILE
      //----------------------------------------------
      //----------------------------------------------

      //return;

      #region FP stuff for the second part of the demo

      // Technically, only C# 12 allows collection expressions - we're using them here anyway, for brevity
      List<double> values = [1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0];
      Console.WriteLine($"Downsampled: {JsonSerializer.Serialize(Downsample(values))}");

      List<List<int>> m1 = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
      Console.WriteLine($"Transpose: {JsonSerializer.Serialize(Transpose(m1))}");
      Console.WriteLine($"Transpose: {JsonSerializer.Serialize(Transpose_(m1))}");

      #endregion
    }

    // Now we're talking -- note that the use of Span<T> magically solves
    // a perf problem you would have if you used an Array type here.
    static int Sum(Span<int> l) => l switch
    {
      [] => 0,
      [var x, .. var xs] => x + Sum(xs)
    };

    #region FP stuff for the second part of the demo

    // From F#:
    // let rec downsample : float list -> float list =
    //   function
    //   | [] -> []
    //   | h1::h2::t -> 0.5 * (h1 + h2) :: downsample t
    //   | [_] -> invalid_arg "downsample"

    static List<double> Downsample(List<double> values) => values switch
    {
      [var x1, var x2, .. var xs] => new List<double> { (x1 + x2) / 2 }.Concat(Downsample(xs)).ToList(),
      _ => []
    };

    // From F#
    // let rec transpose = function
    //   | (_ :: _) :: _ as xss ->
    //     List.map List.head xss :: transpose (List.map List.tail xss)
    //   | _ -> []

    static List<List<int>> Transpose(List<List<int>> s) => s switch
    {
      [[_, .. _], .. _] =>
        new List<List<int>> { s.Select(x => x.First()).ToList() }
          .Concat<List<int>>(
            Transpose(
              s.Select(x => x.Skip(1).ToList()).ToList()
            )).ToList(),
      _ => []
    };

    // Construction helpers:

    static List<R> Map<T, R>(Func<T, R> f, List<T> l) => l.Select(f).ToList();
    static T Head<T>(List<T> l) => l[0];
    static List<T> Tail<T>(List<T> l) => l.Skip(1).ToList();
    static List<T> Cons<T>(T head, List<T> tail) => new List<T> { head }.Concat(tail).ToList();

    // Much closer now!

    static List<List<int>> Transpose_(List<List<int>> s) => s switch
    {
      [[_, .. _], .. _] => Cons(Map(Head, s), Transpose_(Map(Tail, s))),
      _ => []
    };

    #endregion
  }
}