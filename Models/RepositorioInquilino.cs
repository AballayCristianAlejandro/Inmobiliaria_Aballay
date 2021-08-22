using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_Aballay.Models
{
    public class RepositorioInquilino
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Db_Inmobiliaria_Aba;Trusted_Connection=True;MultipleActiveResultSets=true";
        public RepositorioInquilino()
        {

        }
        public int Alta(Inquilino i)
        {
            int res = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email) " +
                    $"VALUES (@nombre, @apellido, @dni, @telefono, @email)";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@nombre", i.Nombre);
                    comm.Parameters.AddWithValue("@apellido", i.Apellido);
                    comm.Parameters.AddWithValue("@dni", i.Dni);
                    comm.Parameters.AddWithValue("@telefono", i.Telefono);
                    comm.Parameters.AddWithValue("@email", i.Email);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    comm.CommandText = "SELECT SCOPE_IDENTITY()";
                    i.IdInquilino = (int)comm.ExecuteScalar();
                    conn.Close();
                }
            }

            return res;
        }
        public IList<Inquilino> ObtenerTodos()
        {
            IList<Inquilino> res = new List<Inquilino>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email" +
                    $" FROM Inquilinos" +
                    $" ORDER BY Apellido, Nombre";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino p = new Inquilino
                        {
                            IdInquilino = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }
        public int Modificacion(Inquilino i)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilinos SET " +
                    $"Nombre=@nombre', Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email " +
                    $"WHERE IdInquilino = @idInquilino";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@nombre", i.Nombre);
                    comm.Parameters.AddWithValue("@apellido", i.Apellido);
                    comm.Parameters.AddWithValue("@dni", i.Dni);
                    comm.Parameters.AddWithValue("@telefono", i.Telefono);
                    comm.Parameters.AddWithValue("@email", i.Email);
                    comm.Parameters.AddWithValue("@idInquilino", i.IdInquilino);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
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
                string sql = $"DELETE FROM Inquilinos WHERE IdPropieterio = {id}";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.CommandType = CommandType.Text;
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
    }
}
