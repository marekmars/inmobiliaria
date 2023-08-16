using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data

namespace Inmobiliaria.Models;
public class PropietariosRepository
{

    protected readonly string connectionString;
    public PropietariosRepository()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }
    public List<Propietario> GetAllPropietarios()
    {
        List<Propietario> Propietarios = new List<Propietario>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios ORDER BY apellido";

            using (MySqlCommand command = new(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario Propietario = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                        Propietarios.Add(Propietario);
                    }
                    connection.Close();
                }
            }
        }
        Console.WriteLine("PropietarioS" + Propietarios);
        return Propietarios;
    }

    public Propietario GetPropietarioById(int id)
    {
        Propietario propietario = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios WHERE id=@id ORDER BY apellido";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propietario = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                    }
                    connection.Close();
                }
            }
        }
        return propietario;
    }

    public int CreatePropietario(Propietario propietario)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO `propietarios`( `dni`, `apellido`, `nombre`, `telefono`, `correo`) 
            VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo);
            SELECT LAST_INSERT_ID()";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre",propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono",propietario.Telefono);
                command.Parameters.AddWithValue("@Correo",propietario.Correo);
                connection.Open();
                res=Convert.ToInt32(command.ExecuteScalar());
                propietario.Id=res;
                connection.Close();

            }

        }
        return res;
        
    }

    public int DeletePropietario(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"DELETE FROM `propietarios`
                        WHERE `id` = @id";

            using (MySqlCommand command = new(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        return res;

    }
}
