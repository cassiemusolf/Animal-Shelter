using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace AnimalShelter
{
  public class TypeTest : IDisposable
  {
    public TypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Animal_Shelter_Test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_TypeEmptyAtFirst()
    {
      int result = Type.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Dispose()
    {
      Type.DeleteAll();
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Type firstType = new Type("Husky");
      Type secondType = new Type("Husky");

      Assert.Equal(firstType, secondType);
    }

    [Fact]
    public void Test_Save_SavesTypeToDatabase()
    {
      Type testType = new Type("Husky");
      testType.Save();

      List<Type> result = Type.GetAll();
      List<Type> testList = new List<Type>{testType};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsTypeInDatabase()
    {
      Type newType = new Type("Husky");
      newType.Save();

      Type foundType = Type.Find(newType.GetId());

      Assert.Equal(newType, foundType);
    }
  }
}
