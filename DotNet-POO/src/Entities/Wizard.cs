namespace DotNet_POO.src.Entities
{
  public class Wizard : Hero
  {

    //construtor
    public Wizard(string Name, int Level, string HeroType)
    {
      this.Name = Name;
      this.Level = Level;
      this.HeroType = HeroType;
    }
    public override string Attack()
    {
      return $"{this.Name} Lançou Magia";
    }

    public string Attack(int Bonus)
    {

      if (Bonus > 6)
      {
        return $"{this.Name} Lançou Magia efetiva com bonus de {Bonus}";
      }
      else
      {
        return $"{this.Name} Lançou uma Magia com força fraca e bonus de {Bonus}";

      }
    }
  }
}