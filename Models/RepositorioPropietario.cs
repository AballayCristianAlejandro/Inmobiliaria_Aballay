using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_Aballay.Models
{
    public class RepositorioPropietario
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Db_Inmobiliaria_Aba;Trusted_Connection=True;MultipleActiveResultSets=true";
        public RepositorioPropietario()
        {

        }
		public int Alta(Propietario p)
		{
			int res = -1;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Propietarios (Nombre, Apellido, Dni, Telefono, Email, Clave) " +
					$"VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave);" +
					"SELECT SCOPE_IDENTITY();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (SqlCommand comm = new SqlCommand(sql, conn))
				{
					comm.CommandType = CommandType.Text;
					comm.Parameters.AddWithValue("@nombre", p.Nombre);
					comm.Parameters.AddWithValue("@apellido", p.Apellido);
					comm.Parameters.AddWithValue("@dni", p.Dni);
					comm.Parameters.AddWithValue("@telefono", p.Telefono);
					comm.Parameters.AddWithValue("@email", p.Email);
					comm.Parameters.AddWithValue("@clave", p.Clave);
					conn.Open();
					res = Convert.ToInt32(comm.ExecuteScalar());
					p.IdPropietario = res;
					conn.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Propietarios WHERE IdPropietario = @id";
				using (SqlCommand comm = new SqlCommand(sql, conn))
				{
					comm.CommandType = CommandType.Text;
					comm.Parameters.AddWithValue("@id", id);
					conn.Open();
					res = comm.ExecuteNonQuery();
					conn.Close();
				}
			}
			return res;
		}
		public int Modificacion(Propietario p)
		{
			int res = -1;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Propietarios SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email, Clave=@clave " +
					$"WHERE IdPropietario = @id";
				using (SqlCommand comm = new SqlCommand(sql, conn))
				{
					comm.CommandType = CommandType.Text;
					comm.Parameters.AddWithValue("@nombre", p.Nombre);
					comm.Parameters.AddWithValue("@apellido", p.Apellido);
					comm.Parameters.AddWithValue("@dni", p.Dni);
					comm.Parameters.AddWithValue("@telefono", p.Telefono);
					comm.Parameters.AddWithValue("@email", p.Email);
					comm.Parameters.AddWithValue("@clave", p.Clave);
					comm.Parameters.AddWithValue("@id", p.IdPropietario);
					conn.Open();
					res = comm.ExecuteNonQuery();
					conn.Close();
				}
			}
			return res;
		}

		public IList<Propietario> ObtenerTodos()
		{
			IList<Propietario> res = new List<Propietario>();
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave" +
					$" FROM Propietarios";
				using (SqlCommand comm = new SqlCommand(sql, conn))
				{
					comm.CommandType = CommandType.Text;
					conn.Open();
					var reader = comm.ExecuteReader();
					while (reader.Read())
					{
						Propietario p = new Propietario
						{
							IdPropietario = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader["Telefono"].ToString(),
							Email = reader.GetString(5),
							Clave = reader.GetString(6),
						};
						res.Add(p);
					}
					conn.Close();
				}
			}
			return res;
		}

		

		public IList<Propietario> BuscarPorNombre(string nombre)
		{
			List<Propietario> res = new List<Propietario>();
			Propietario p = null;
			nombre = "%" + nombre + "%";
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave FROM Propietarios" +
					$" WHERE Nombre LIKE @nombre OR Apellido LIKE @nombre";
				using (SqlCommand comm = new SqlCommand(sql, conn))
				{
					comm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
					comm.CommandType = CommandType.Text;
					conn.Open();
					var reader = comm.ExecuteReader();
					while (reader.Read())
					{
						p = new Propietario
						{
							IdPropietario = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
							Clave = reader.GetString(6),
						};
						res.Add(p);
					}
					conn.Close();
				}
			}
			return res;
		}


	}
}
