using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
abstract class Animal
{
    public string Name { get; set; }
}

class Dog : Animal
{
    public int BarkVolume { get; set; }
}

class Cat : Animal
{
    public bool LikesMilk { get; set; }
}
