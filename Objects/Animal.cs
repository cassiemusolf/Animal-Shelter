using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _breed;
    private DateTime _admittanceDate;
    private string _gender;
    private int _typeId;

    public Animal(string Name, string Breed, DateTime DateofAdmittance, string Gender, int TypeId, int Id = 0)
    {
      _name = Name;
      _breed = Breed;
      _admittanceDate = DateofAdmittance;
      _gender = Gender;
      _typeId = TypeId;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetBreed()
    {
      return _breed;
    }

    public DateTime GetAdmittance()
    {
      return _admittanceDate;
    }

    public string GetGender()
    {
      return _gender;
    }

    public int GetTypeId()
    {
      return _typeId;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animal(name, breed, admittance_date, gender, type_id) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalBreed, @AnimalDate, @AnimalGender, @AnimalTypeId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();

      SqlParameter breedParameter = new SqlParameter();
      breedParameter.ParameterName = "@AnimalBreed";
      breedParameter.Value = this.GetBreed();

      SqlParameter admittanceDateParameter = new SqlParameter();
      admittanceDateParameter.ParameterName = "@AnimalDate";
      admittanceDateParameter.Value = this.GetAdmittance();

      SqlParameter genderParameter = new SqlParameter();
      genderParameter.ParameterName = "@AnimalGender";
      genderParameter.Value = this.GetGender();

      SqlParameter typeIdParameter = new SqlParameter();
      typeIdParameter.ParameterName = "@AnimalTypeId";
      typeIdParameter.Value = this.GetTypeId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(breedParameter);
      cmd.Parameters.Add(admittanceDateParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(typeIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE id=@AnimalId", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@AnimalId";
      idParameter.Value = id;

      cmd.Parameters.Add(idParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      string foundBreed = null;
      DateTime foundDate = new DateTime();
      string foundGender = null;
      int foundType = 0;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundBreed = rdr.GetString(2);
        foundDate = rdr.GetDateTime(3);
        foundGender = rdr.GetString(4);
        foundType = rdr.GetInt32(5);
      }

      Animal foundAnimal = new Animal(foundName, foundBreed, foundDate, foundGender, foundType, foundId);

      return foundAnimal;

    }



    public override bool Equals(System.Object otherAnimal)
    {
      if(!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = (this.GetId() == newAnimal.GetId());
        bool nameEquality = (this.GetName() == newAnimal.GetName());
        bool breedEquality = (this.GetBreed() == newAnimal.GetBreed());
        bool admittanceDateEquality = (this.GetAdmittance() == newAnimal.GetAdmittance());
        bool genderEquality = (this.GetGender() == newAnimal.GetGender());
        bool typeEquality = (this.GetTypeId() == newAnimal.GetTypeId());
        return (idEquality && nameEquality && breedEquality && admittanceDateEquality && genderEquality && typeEquality);
      }
    }

    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal ORDER BY admittance_date;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalBreed = rdr.GetString(2);
        DateTime animalDateOfAdmittance = rdr.GetDateTime(3);
        string animalGender = rdr.GetString(4);
        int typeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalBreed, animalDateOfAdmittance, animalGender, typeId, animalId);
        allAnimals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAnimals;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM animal;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
