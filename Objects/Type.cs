using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class Type
  {
    private string _name;
    private int _id;

    public Type(string Name, int Id = 0)
    {
      _name = Name;
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

    public override bool Equals(System.Object otherType)
    {
      if(!(otherType is Type))
      {
        return false;
      }
      else
      {
        Type newType = (Type) otherType;
        bool idEquality = this.GetId() == newType.GetId();
        bool nameEquality = this.GetName() == newType.GetName();
        return (idEquality && nameEquality);
      }
    }

    public static List<Type> GetAll()
    {
      List<Type> foundList = new List<Type>();
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM type", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Type newType = new Type(name, id);
        foundList.Add(newType);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundList;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO type(name) OUTPUT INSERTED.id VALUES(@TypeName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@TypeName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Type Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM type WHERE id=@TypeId;", conn);
      SqlParameter typeParameter = new SqlParameter();
      typeParameter.ParameterName = "@TypeId";
      typeParameter.Value = id.ToString();
      cmd.Parameters.Add(typeParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;

      while(rdr.Read())
      {
          foundId = rdr.GetInt32(0);
          foundName = rdr.GetString(1);
      }

      Type foundType = new Type(foundName, foundId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundType;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM type", conn);

      cmd.ExecuteNonQuery();
    }
  }
}
