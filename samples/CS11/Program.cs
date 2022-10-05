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


      // Docs at https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#list-patterns
      // show an example with CSV import -- cool idea, but of course
      // the CSV content is always a list of strings so advanced
      // patterns can't be used for non-string content.

      // What the docs don't tell you (at least I didn't see it
      // anywhere -- found it in GitHub), you can split head and tail
      // On second read, I found this:
      // "The var pattern can capture a single element, or a range of elements."
      // I imagine that somebody thought this would be sufficient.
      if (numbers is [var x, .. var xs]) {
        Console.WriteLine($"Head: {x}");
        Console.WriteLine($"Tail: {xs}");
      }

      // So we can do nice functional-style stuff:
      Console.WriteLine($"Sum of numbers: {Sum(numbers)}");
    }

    // Now we're talking
    static int Sum(int[] l) => l switch
    {
      [] => 0,
      [var x, .. var xs] => x + Sum(xs)
    };
  }
}
