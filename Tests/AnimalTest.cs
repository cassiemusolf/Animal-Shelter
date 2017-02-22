using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalTest : IDisposable
  {
    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Animal_Shelter_Test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Animal.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Animal.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      DateTime date1 = new DateTime(2008, 4, 10);
      Animal animal1 = new Animal("Fido", "Husky", date1, "male", 1);
      Animal animal2 = new Animal("Fido", "Husky", date1, "male", 1);

      Assert.Equal(animal1, animal2);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      DateTime date = new DateTime(2008, 4, 10);
      Animal animal = new Animal("Barko", "Husky", date, "male", 1);
      animal.Save();

      List<Animal> result = Animal.GetAll();
      List<Animal> testResult = new List<Animal>{animal};

      Assert.Equal(testResult, result);
    }

    [Fact]
    public void Test_FindsAnimalInDatabase()
    {
      DateTime date1 = new DateTime(2008, 4, 10);
      Animal animal1 = new Animal("Fido", "Husky", date1, "male", 1);
      animal1.Save();

      Animal foundAnimal = Animal.Find(animal1.GetId());

      Assert.Equal(animal1, foundAnimal);

    }
  }
}
