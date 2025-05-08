// ReSharper disable InconsistentNaming

namespace Temp;

internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Hello, World!");
  }

  private (T, U, T, U) BinarySearch<T, U>(Func<T, T, T> Split, Func<T, U> Cmp, (T x, U c_x, T y, U c_y) r) {
    T m = Split(r.x, r.y);
    U c_m = Cmp(m);
    return (r.c_x = c_m, c_m = r.c_y) switch
    {
      (true, false) => (m, c_m, r.y, r.c_y),
      (false, true) => (r.x, r.c_x, m, c_m),
      _ => throw new Exception("Not found")
    };
  }
}