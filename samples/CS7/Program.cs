// ReSharper disable All

namespace CS7 {
  public class DeadPerson {
    public string Name { get; init; } = "";
    public virtual bool IsAlive => false;
  }

  public class Zombie : DeadPerson {
    public override bool IsAlive => true;
  }

  class Program {
    static void CheckZombie(DeadPerson p) {
      switch (p) {
        case Zombie z:
          Console.WriteLine($"{z.Name} is a zombie, run!");
          break;
        default:
          Console.WriteLine($"Looks like {p.Name} is safe.");
          break;
      }
    }

    static void Main(string[] args) {
      string s = "This is a string";

      if (s is string x)
        Console.WriteLine($"Turns out s is the string '{x}'");

      var d1 = new DeadPerson { Name = "Abe Lincoln" };
      var d2 = new Zombie { Name = "Windows 7" };

      CheckZombie(d1);
      CheckZombie(d2);

      Console.WriteLine("My heartfelt apologies in case anybody finds my zombie sample offensive.");
    }
  }
}