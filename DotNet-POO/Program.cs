using System;
using DotNet_POO.src.Entities;

namespace DotNet_POO
{
  class Program
  {
    static void Main(string[] args)
    {
      Knight arus = new Knight("Arus", 23, "Kinight");
      Wizard wizard = new Wizard("Jennica", 23, "White Wizard");


      Console.WriteLine(wizard.Attack(1));
      Console.WriteLine(arus.Attack());
      Console.WriteLine(wizard.Attack(7));
    }
  }
}
