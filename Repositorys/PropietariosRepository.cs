using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data
using System.Text.RegularExpressions;


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
    public Propietario GetPropietarioByDni(string dni)
    {
        Propietario propietario = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios WHERE dni=@dni";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
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

        if (!EsNumeroTelefonoValido(propietario.Telefono))
        {
            res=-2;
            return res;
        }

        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(propietario.Correo))
        {
            res=-3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(propietario.Dni))
        {
            res=-4;
            return res;
        }

        Propietario propietarioAux = GetPropietarioByDni(propietario.Dni);

        try
        {
            if (propietarioAux.Nombre == "")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO `propietarios`( `dni`, `apellido`, `nombre`, `telefono`, `correo`) 
            VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo);
            SELECT LAST_INSERT_ID()";

                    using (MySqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Dni", propietario.Dni);
                        command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                        command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                        command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                        command.Parameters.AddWithValue("@Correo", propietario.Correo);
                        connection.Open();
                        res = Convert.ToInt32(command.ExecuteScalar());
                        propietario.Id = res;
                        connection.Close();

                    }

                }
            }
            else
            {
                res = -2;
            }
        }

        catch (System.Exception)
        {
            throw;
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

    public int UpdatePropietario(Propietario propietario)
    {
        var res = -1;

       if (!EsNumeroTelefonoValido(propietario.Telefono))
        {
            res=-2;
            return res;
        }

        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(propietario.Correo))
        {
            res=-3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(propietario.Dni))
        {
            res=-4;
            return res;
        }

        Propietario propietarioAux = GetPropietarioByDni(propietario.Dni);

        if (propietarioAux.Nombre == ""||propietarioAux.Id==propietario.Id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE `propietarios` SET 
                        `dni` = @Dni,
                        `apellido` = @Apellido,
                        `nombre` = @Nombre,
                        `telefono` = @Telefono,
                        `correo` = @Correo
                        WHERE `id` = @Id";

                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", propietario.Id);
                    command.Parameters.AddWithValue("@Dni", propietario.Dni);
                    command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@Correo", propietario.Correo);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }
        else
        {
            return -2;
        }

        return res;

    }


    private bool EsNumeroTelefonoValido(string telefono)
    {

        string patron = @"^\+\d{2} \d{2,3}-\d{6,7}$";
        return Regex.IsMatch(telefono, patron);
    }

    private bool EsCorreoElectronicoValido(string correo)
    {
        // Patrón de expresión regular para validar una dirección de correo electrónico
        string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(correo, patron);
    }

    private bool EsDniValido(string dni)
    {
        // Patrón de expresión regular para validar un DNI argentino (formato: XX.XXX.XXX)
        string patron = @"^\d{2}\.\d{3}\.\d{3}$";
        return Regex.IsMatch(dni, patron);
    }
}
