using Microsoft.Data.SqlClient;
using System.Data;
using Domain;

namespace Infrastructure;

public class AlumnosDbContext
{
    private readonly string _connectionString;

    public AlumnosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Alumno> List()
    {
        var data = new List<Alumno>();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[foto] FROM [Alumno]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Alumno
                {
                    Id = (Guid)dr["Id"],
                    Nombre = (string)dr["Nombre"],
                    Edad = (int)dr["Edad"],
                        
                    Foto = dr["Foto"] == DBNull.Value ? null : (string)dr["Foto"]
                });
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public Alumno Details(Guid id)
    {
        var data = new Alumno();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[foto] FROM [Alumno] WHERE [Id] = @id", con);
        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.Nombre = (string)dr["Nombre"];
                data.Edad = (int)dr["Edad"];
                        
                data.Foto = dr["Foto"] == DBNull.Value ? null : (string)dr["Foto"];
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Create(Alumno data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Alumno] ([Id],[Nombre],[Edad],[foto]) VALUES (@id,@nombre,@edad,@foto)", con);
        cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("edad", SqlDbType.Int).Value = data.Edad;
        cmd.Parameters.Add("Foto", SqlDbType.NVarChar).Value = data.Foto ==  ? DBNull.Value : data.Foto;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Edit(Alumno data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Alumno] SET [Nombre] = @nombre, [Edad] = @edad, [foto] = @foto WHERE [Id] = @id", con);
        cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("edad", SqlDbType.Int).Value = data.Edad;        
        cmd.Parameters.Add("Foto", SqlDbType.NVarChar).Value = data.Foto == null ? DBNull.Value : data.Foto;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Delete(Guid id)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("DELETE FROM [Alumno] WHERE [Id] = @id", con);
        cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
}